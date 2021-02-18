#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)
using System.Data;
#endif

using Codex.Enum;
using Codex.Contract;
using Codex.MySql.Extension;

using MySql.Data.MySqlClient;

using System;

namespace Codex.MySql.UserModel
{
    public class MySqlHelper : IDBHelper
    {
        public Return Execute(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery
            )
        {
            try
            {
                IMySqlConnection _conn_raw = (IMySqlConnection)_conn;
                MySqlParameter[] _pmts_raw = _pmts.GetSqlParameters().GetParameters();

                using (MySqlCommand _cmd = new MySqlCommand(_query, _conn_raw.Connection))
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
            IConnection _conn,
            IParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery
            )
        {
            try
            {
                IMySqlConnection _conn_raw = (IMySqlConnection)_conn;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                MySqlParameter[] _pmts_raw = _pmts[_r].GetSqlParameters().GetParameters();

                using (MySqlCommand _cmd = new MySqlCommand(_query, _conn_raw.Connection))
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

        public virtual Return Get_DataSet(string _query, IConnection _conn, IParameter[] _pmts = null)
        {
#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)
            try
            {
                Return _exe = Execute(_query, _conn, _pmts, EExecute.Reader);
                _exe.TriggerErrorException();

                DataSet _return = new DataSet("DataSet_0");
                _return.EnforceConstraints = _conn.Constraints;
                byte _n = 0;

                using (MySqlDataReader _read = (MySqlDataReader)_exe.Result)
                {
                    while (_read.IsClosed == false)
                    {
                        DataTable _dt = new DataTable("DataTable_" + Convert.ToString(_n));
                        _dt.Load(_read, LoadOption.OverwriteChanges);
                        _return.Tables.Add(_dt);
                        _n++;
                    }
                }

                return new Return(true, _return);
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
#else
            throw new NotImplementedException();
#endif
        }
        public virtual Return Get_DataTable(string _query, IConnection _conn, IParameter[] _pmts = null)
        {
#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)
            try
            {
                Return _exe = Execute(_query, _conn, _pmts, EExecute.Reader);
                _exe.TriggerErrorException();

                DataTable _return = new DataTable("DataTable_0");

                using (MySqlDataReader _read = (MySqlDataReader)_exe.Result)
                    _return.Load(_read, LoadOption.OverwriteChanges);

                return new Return(true, _return);
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
#else
            throw new NotImplementedException();
#endif
        }
    }
}