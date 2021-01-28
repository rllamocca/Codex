#if !(NET35 || NET40)

using Codex.Generic;
using Codex.Helper;

using Codex.ORM.Enum;
using Codex.ORM.Extension;
using Codex.ORM.Generic;
using Codex.ORM.Struct;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Codex.ORM.Data.Helper
{
    public static class SqlAsyncHelper
    {
        public static async Task<Return> Execute(
            OrmSqlConnection _conn,
            String _query,
            EExecute _exe = EExecute.NonQuery,
            OrmParameter[] _pmts = null
            )
        {
            try
            {
                using (SqlCommand _cmd = new SqlCommand(_query, _conn.Conexion))
                {
                    if (_pmts != null)
                    {
                        _cmd.Parameters.AddRange(_pmts.Sql());
                        VoidHelper.Set_DBNull(_cmd.Parameters);
                    }
                    _cmd.Transaction = _conn.Transaccion;
                    _cmd.CommandTimeout = _conn.Tiempo;
                    if (_conn.Preparar)
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
        public static async Task<Return> Get_DataSet(
            OrmSqlConnection _conn,
            String _query,
            OrmParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = await SqlAsyncHelper.Execute(_conn, _query, EExecute.Reader, _pmts);
                _exe.GatillarErrorExcepcion();

                DataSet _return = new DataSet("DataSet_0");
                _return.EnforceConstraints = _conn.Restricciones;
                UInt16 _n = 0;
                using (SqlDataReader _read = (SqlDataReader)_exe.Resultado)
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
        }
        public static async Task<Return> Get_DataTable(
            OrmSqlConnection _conn,
            String _query,
            OrmParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = await SqlAsyncHelper.Execute(_conn, _query, EExecute.Reader, _pmts);
                _exe.GatillarErrorExcepcion();

                DataTable _return = new DataTable("DataTable_0");
                using (SqlDataReader _read = (SqlDataReader)_exe.Resultado)
                {
                    _return.Load(_read, LoadOption.OverwriteChanges);
                }
                return new Return(true, _return);
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }

        public static async Task<Return> Set_DataSet(
            OrmSqlConnection _conn,
            DataSet _data
            )
        {
            using (SqlBulkCopy _bulk = new SqlBulkCopy(_conn.Conexion))
            {
                try
                {
                    foreach (DataTable _item in _data.Tables)
                    {
                        _bulk.DestinationTableName = _item.TableName;
                        _bulk.BatchSize = 1000;
                        await _bulk.WriteToServerAsync(_item);
                    }

                    return new Return(true);
                }
                catch (Exception _ex)
                {
                    return new Return(false, _ex);
                }
            }
        }
        public static async Task<Return> Set_DataTable(
            OrmSqlConnection _conn,
            DataTable _data
            )
        {
            using (SqlBulkCopy _bulk = new SqlBulkCopy(_conn.Conexion))
            {
                try
                {
                    _bulk.DestinationTableName = _data.TableName;
                    _bulk.BatchSize = 1000;
                    await _bulk.WriteToServerAsync(_data);

                    return new Return(true);
                }
                catch (Exception _ex)
                {
                    return new Return(false, _ex);
                }
            }
        }

        #region TRATAMIENTO
        private static String Prepare_Insert(STable _tv, Filter[] _ap)
        {
            String _sqlf = "INSERT INTO [{0}] ({1}) VALUES ({2});";
            if (_tv.Identidad) _sqlf += "SELECT SCOPE_IDENTITY();";
            String[] _iv = _ap.Into_Values();
            _sqlf = String.Format(_sqlf, _tv.Nombre, _iv[0], _iv[1]);
            _sqlf = StringHelper.Clean_Sql(_sqlf);
            return _sqlf;
        }
        public static async Task<Return> InsertAsync(OrmSqlConnection _cn, STable _tv, Filter[] _ap)
        {
            try
            {
                return await SqlAsyncHelper.Execute(_cn, SqlAsyncHelper.Prepare_Insert(_tv, _ap), EExecute.NonQuery, _ap.Parametros());
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }

        private static String Prepare_Update(STable _tv, Filter[] _ap)
        {
            String _sqlf = "UPDATE [{0}] SET {1} WHERE {2};";
            String[] _sw = _ap.Set_WhereKeys();
            _sqlf = String.Format(_sqlf, _tv.Nombre, _sw[0], _sw[1]);
            _sqlf = StringHelper.Clean_Sql(_sqlf);
            return _sqlf;
        }
        public static async Task<Return> UpdateAsync(OrmSqlConnection _cn, STable _tv, Filter[] _ap)
        {
            try
            {
                return await SqlAsyncHelper.Execute(_cn, SqlAsyncHelper.Prepare_Update(_tv, _ap), EExecute.NonQuery, _ap.Parametros());
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }

        private static String Prepare_Delete(STable _tv, Filter[] _ap)
        {
            String _sqlf = "DELETE FROM [{0}] WHERE {1};";
            _sqlf = String.Format(_sqlf, _tv.Nombre, _ap.WhereKeys());
            _sqlf = StringHelper.Clean_Sql(_sqlf);
            return _sqlf;
        }
        public static async Task<Return> DeleteAsync(OrmSqlConnection _cn, STable _tv, Filter[] _ap)
        {
            try
            {
                return await SqlAsyncHelper.Execute(_cn, SqlAsyncHelper.Prepare_Delete(_tv, _ap), EExecute.NonQuery, _ap.Parametros());
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }

        public static async Task<Return[]> FunctionAsync(
            OrmSqlConnection _conn,
            String _f,
            OrmParameter[] _ap = null,
            SqlTransaction _ts = null,
            CancellationTokenSource _cts = null
            )
        {
            try
            {
                using (SqlCommand _cmd = new SqlCommand(_f, _conn.Conexion))
                {
                    if (_ap != null)
                        _cmd.Parameters.AddRange(_ap.Sql());
                    UInt16 _cp = _ap.Retornos();
                    if (_ap.Retorno())
                    {
                        _cp++;
                        SqlParameter _pr = new SqlParameter();
                        _pr.ParameterName = "@Function_ReturnValue";
                        _pr.Direction = ParameterDirection.ReturnValue;
                        _cmd.Parameters.Add(_pr);
                    }
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Transaction = _ts;
                    _cmd.CommandTimeout = _conn.Tiempo;
                    if (_conn.Preparar)
                        _cmd.Prepare();

                    Int32 _e;
                    if (_cts == null) _e = await _cmd.ExecuteNonQueryAsync();
                    else _e = await _cmd.ExecuteNonQueryAsync(_cts.Token);

                    OrmSqlParameter[] _pto = new OrmSqlParameter[_cp];
                    _cp = 0;
                    foreach (SqlParameter _p in _cmd.Parameters)
                    {
                        switch (_p.Direction)
                        {
                            case ParameterDirection.Output:
                            case ParameterDirection.InputOutput:
                            case ParameterDirection.ReturnValue:
                                _pto[_cp] = new OrmSqlParameter(_p);
                                _cp++;
                                break;
                            default:
                                break;
                        }
                    }
                    Return[] _return = new Return[2];
                    _return[0] = new Return(true, _e);
                    _return[1] = new Return(true, _pto);
                    return _return;
                }
            }
            catch (Exception _ex)
            {
                return new Return[] { new Return(false, _ex) };
            }
        }
        #endregion
        //#region SELECCION
        //private static String Prepare_Count(STable _tv, Filter[] _ap = null)
        //{
        //    String _sqlf0 = "SELECT COUNT(*) FROM [{0}] WHERE {1};";
        //    String _sqlf1 = "SELECT COUNT(*) FROM [{0}];";
        //    String _w = _ap.Where();
        //    String _sql = String.Empty;
        //    if (_w.Length > 0) _sql = String.Format(_sqlf1, _tv.Nombre, _w);
        //    else _sql = String.Format(_sqlf0, _tv.Nombre);
        //    _sql = StringHelper.Clean_Sql(_sql);
        //    return _sql;
        //}
        //public static async Task<Return> CountAsync(OrmSqlConnection _cn, STable _tv, Filter[] _ap = null)
        //{
        //    try
        //    {
        //        return await SqlAsyncHelper.Execute(_cn, SqlAsyncHelper.Prepare_Count(_tv, _ap), EExecute.Scalar, _ap.Parametros());
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //#endregion
        //#region VARIOS
        //public static Return Databases(OrmSqlConnection _cn)
        //{
        //    try
        //    {
        //        DataTable _dtt = new DataTable();
        //        String[] _rest = new String[4];
        //        _rest[1] = "";
        //        _dtt = _cn.Conexion.GetSchema("DATABASES");
        //        _dtt.TableName = "DATABASES";
        //        return new Return(true, _dtt);
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //public static Return Tables(OrmSqlConnection _cn, String _database)
        //{
        //    try
        //    {
        //        DataTable _dtt = new DataTable();
        //        String[] _rest = new String[20];
        //        _rest[1] = _database;
        //        _dtt = _cn.Conexion.GetSchema("TABLES", _rest);
        //        _dtt.TableName = "TABLES";
        //        return new Return(true, _dtt);
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //public static Return Columns(OrmSqlConnection _cn, String _database, String _table)
        //{
        //    try
        //    {
        //        DataTable _dtt = new DataTable();
        //        String[] _rest = new String[17];
        //        _rest[1] = _database;
        //        _rest[2] = _table;
        //        _dtt = _cn.Conexion.GetSchema("COLUMNS", _rest);
        //        _dtt.TableName = "COLUMNS";
        //        return new Return(true, _dtt);
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //#endregion
    }
}

#endif