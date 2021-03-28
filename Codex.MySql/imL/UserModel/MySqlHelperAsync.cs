﻿#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)

#if (NET45 || NETSTANDARD2_0)
using System.Data;
#endif

using Codex.Enumeration;
using Codex.Contract;
using Codex.MySql.Extension;

using MySql.Data.MySqlClient;

using System;
using System.Threading.Tasks;

namespace Codex.MySql.UserModel
{
    public class MySqlHelperAsync : IDBHelperAsync
    {
        public async Task<Return> Execute(
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
                            return new Return(true, await _cmd.ExecuteNonQueryAsync(_conn.Token));
                        case EExecute.Scalar:
                            return new Return(true, await _cmd.ExecuteScalarAsync(_conn.Token));
                        case EExecute.Reader:
                            return new Return(true, await _cmd.ExecuteReaderAsync(_conn.Token));
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
                                    _returns[_r] = new Return(true, await _cmd.ExecuteNonQueryAsync(_conn.Token));
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteScalarAsync(_conn.Token));
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteReaderAsync(_conn.Token));
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

#if (NET45 || NETSTANDARD2_0)
        public async Task<Return> Get_DataSet(string _query, IConnection _conn, IParameter[] _pmts = null)
        {
            try
            {
                Return _exe = await Execute(_query, _conn, _pmts, EExecute.Reader);
                _exe.TriggerErrorException();

                DataSet _return = new DataSet("DataSet_0")
                {
                    EnforceConstraints = _conn.Constraints
                };
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
            throw new NotImplementedException();
        }
        public async Task<Return> Get_DataTable(string _query, IConnection _conn, IParameter[] _pmts = null)
        {
            try
            {
                Return _exe = await Execute(_query, _conn, _pmts, EExecute.Reader);
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
            throw new NotImplementedException();
        }
#else
        public Task<Return> Get_DataSet(string _query, IConnection _conn, IParameter[] _pmts = null)
        {
            throw new NotImplementedException();
        }
        public Task<Return> Get_DataTable(string _query, IConnection _conn, IParameter[] _pmts = null)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
#endif