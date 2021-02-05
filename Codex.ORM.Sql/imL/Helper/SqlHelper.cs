using Codex.Generic;
using Codex.ORM.Enum;
using Codex.ORM.Helper;

using System;
using System.Data.SqlClient;

using Codex.ORM.Sql.Extension;

namespace Codex.ORM.Sql.Helper
{
    public class SqlHelper : IOrmHelper
    {
        public Return Execute(
            string _query,
            IOrmConnection _conn,
            EExecute _exe = EExecute.NonQuery,
            IOrmParameter[] _pmts = null
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

        public virtual Return Get_DataSet(string _query, IOrmConnection _conn, IOrmParameter[] _pmts = null)
        {
            throw new NotImplementedException();
        }
        public virtual Return Get_DataTable(string _query, IOrmConnection _conn, IOrmParameter[] _pmts = null)
        {
            throw new NotImplementedException();
        }
    }
}