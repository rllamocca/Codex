using Codex.Generic;

using Codex.ORM.Enum;

using System;
using System.Data.SqlClient;

namespace Codex.ORM.Sql.Helper
{
    public static class SqlHelper
    {
        public static Return Execute(
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
                            return new Return(true, _cmd.ExecuteNonQuery());
                        case EExecute.Scalar:
                            return new Return(true, _cmd.ExecuteScalar());
                        case EExecute.Reader:
                            return new Return(true, _cmd.ExecuteReader());
                        case EExecute.XmlReader:
                            return new Return(true, _cmd.ExecuteXmlReader());
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