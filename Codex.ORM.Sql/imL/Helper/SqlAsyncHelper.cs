using Codex.Generic;

using Codex.ORM.Enum;

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codex.ORM.Sql.Helper
{
    public static class SqlAsyncHelper
    {
        public static async Task<Return> Execute(
            String _query,
            OrmSqlConnection _conn,
            EExecute _exe = EExecute.NonQuery,
            SqlParameter[] _pmts = null
            )
        {
            try
            {
                using (SqlCommand _cmd = new SqlCommand(_query, _conn.Connection))
                {
                    if (_pmts != null && _pmts.Length > 0)
                        _cmd.Parameters.AddRange(_pmts);

                    _cmd.Transaction = _conn.Transaction;
                    _cmd.CommandTimeout = _conn.TimeOut;

                    if (_conn.Prepare)
                        _cmd.Prepare();

                    switch (_exe)
                    {
                        case EExecute.NonQuery:
                            return new Return(true, await _cmd.ExecuteNonQueryAsync(_conn.Token));
                        case EExecute.Scalar:
                            return new Return(true, await _cmd.ExecuteScalarAsync(_conn.Token));
                        case EExecute.Reader:
                            return new Return(true, await _cmd.ExecuteReaderAsync(_conn.Token));
                        case EExecute.XmlReader:
                            return new Return(true, await _cmd.ExecuteXmlReaderAsync(_conn.Token));
                        default:
                            return new Return(false);
                    }
                }
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }
    }
}