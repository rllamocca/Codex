using Codex.ORM.Enum;
using Codex.ORM.Helper;

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
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = _conn.TimeOut;

                    if (_pmts_raw != null)
                        _cmd.Parameters.AddRange(_pmts_raw);

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
        public async Task<Return[]> Execute(
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
                                    _returns[_r] = new Return(true, await _cmd.ExecuteNonQueryAsync(_conn.Token));
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteScalarAsync(_conn.Token));
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteReaderAsync(_conn.Token));
                                    break;
                                case EExecute.XmlReader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteXmlReaderAsync(_conn.Token));
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