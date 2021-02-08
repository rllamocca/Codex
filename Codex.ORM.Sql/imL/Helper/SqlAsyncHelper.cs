using Codex.Generic;
using Codex.ORM.Enum;
using Codex.ORM.Helper;
using Codex.ORM.Sql.Extension;

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codex.ORM.Sql.Helper
{
    public class SqlAsyncHelper : IOrmAsyncHelper
    {
        public async Task<Return> Execute(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery
            )
        {
            try
            {
                OrmSqlConnection _conn_raw = (OrmSqlConnection)_conn;
                SqlParameter[] _pmts_raw = _pmts.GetSqlParameters().GetParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
                {
                    if (_pmts_raw != null && _pmts_raw.Length > 0)
                        _cmd.Parameters.AddRange(_pmts_raw);

                    _cmd.Transaction = _conn_raw.Transaction;
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

        public virtual Task<Return> Get_DataSet(string _query, IOrmConnection _conn, IOrmParameter[] _pmts = null)
        {
            throw new NotImplementedException();
        }
        public virtual Task<Return> Get_DataTable(string _query, IOrmConnection _conn, IOrmParameter[] _pmts = null)
        {
            throw new NotImplementedException();
        }
    }
}