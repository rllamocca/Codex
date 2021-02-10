using Codex;
using Codex.ORM.Enum;
using Codex.ORM.Helper;
using Codex.ORM.Sql.Extension;

using System;
using System.Data.SqlClient;

namespace Codex.ORM.Sql.Helper
{
    public class SqlHelper : IOrmHelper
    {
        public Return Execute(
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
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = _conn.TimeOut;

                    if (_pmts_raw != null)
                        _cmd.Parameters.AddRange(_pmts_raw);

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
        public Return[] Execute(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery
            )
        {
            try
            {
                OrmSqlConnection _conn_raw = (OrmSqlConnection)_conn;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                SqlParameter[] _pmts_raw = _pmts[_r].GetSqlParameters().GetParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = _conn.TimeOut;
                    _cmd.Parameters.AddRange(_pmts_raw);

                    int _c_p = _cmd.Parameters.Count;
                    int _c_r = _returns.Length;

                    _cmd.Prepare();
                    do
                    {
                        try
                        {
                            if (_r > 0)
                                for (int _i = 0; _i < _c_p; _i++)
                                    _cmd.Parameters[_i].Value = _pmts[_r][_i].Value;

                            switch (_exe)
                            {
                                case EExecute.NonQuery:
                                    _returns[_r] = new Return(true, _cmd.ExecuteNonQuery());
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, _cmd.ExecuteScalar());
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, _cmd.ExecuteReader());
                                    break;
                                case EExecute.XmlReader:
                                    _returns[_r] = new Return(true, _cmd.ExecuteXmlReader());
                                    break;
                                default:
                                    _returns[_r] = new Return(false);
                                    break;
                            }
                        }
                        catch (Exception _ex)
                        {
                            _returns[_r] = new Return(false, _ex);
                        }
                        _r++;
                    } while (_r < _c_r);
                }
                return _returns;
            }
            catch (Exception _ex)
            {
                return new Return[] { new Return(false, _ex) };
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