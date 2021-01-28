using Codex.Generic;
using Codex.Helper;

using Codex.ORM.Enum;
using Codex.ORM.Extension;
using Codex.ORM.Generic;
using Codex.ORM.Struct;

using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Data.Helper
{
    public static class SqlHelper
    {
        #region BASICO
        public static Return Execute(OrmSqlConnection _con, String _query, EExecute _exe = EExecute.NonQuery, OrmParameter[] _pmts = null)
        {
            try
            {
                using (SqlCommand _cmd = new SqlCommand(_query, _con.Conexion))
                {
                    if (_pmts != null)
                    {
                        _cmd.Parameters.AddRange(_pmts.Sql());
                        VoidHelper.Set_DBNull(_cmd.Parameters);
                    }
                    _cmd.Transaction = _con.Transaccion;
                    _cmd.CommandTimeout = _con.Tiempo;
                    if (_con.Preparar)
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
        public static Return Get_DataSet(OrmSqlConnection _con, String _query, OrmSqlParameter[] _pmts = null)
        {
            try
            {
                Return _exe = SqlHelper.Execute(_con, _query, EExecute.Reader, _pmts);
                _exe.GatillarErrorExcepcion();

                DataSet _return = new DataSet("DataSet_0");
                _return.EnforceConstraints = _con.Restricciones;
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
        public static Return Get_DataTable(OrmSqlConnection _con, String _query, OrmParameter[] _pmts = null)
        {
            try
            {
                Return _exe = SqlHelper.Execute(_con, _query, EExecute.Reader, _pmts);
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

        #endregion
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
        public static Return Insert(OrmSqlConnection _cn, STable _tv, Filter[] _ap)
        {
            try
            {
                //return SqlHelper.Script(_cn, SqlHelper.Prepare_Insert(_tv, _ap), !_tv.Identidad, _ap.Parametros());
                return SqlHelper.Execute(_cn, SqlHelper.Prepare_Insert(_tv, _ap), EExecute.NonQuery, _ap.Parametros());
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
        public static Return Update(OrmSqlConnection _cn, STable _tv, Filter[] _ap)
        {
            try
            {
                return SqlHelper.Execute(_cn, SqlHelper.Prepare_Update(_tv, _ap), EExecute.NonQuery, _ap.Parametros());
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
        public static Return Delete(OrmSqlConnection _cn, STable _tv, Filter[] _ap)
        {
            try
            {
                return SqlHelper.Execute(_cn, SqlHelper.Prepare_Delete(_tv, _ap), EExecute.NonQuery, _ap.Parametros());
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }

        public static Return[] Function(OrmSqlConnection _cn, String _f, OrmParameter[] _ap = null)
        {
            try
            {
                using (SqlCommand _cmd = new SqlCommand(_f, _cn.Conexion))
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
                    _cmd.Transaction = _cn.Transaccion;
                    _cmd.CommandTimeout = _cn.Tiempo;
                    if (_cn.Preparar)
                        _cmd.Prepare();

                    Int32 _e = _cmd.ExecuteNonQuery();
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

        public static Return[] Procedure(OrmSqlConnection _cn, String _p, OrmParameter[] _ap = null)
        {
            try
            {
                using (SqlDataAdapter _da = new SqlDataAdapter())
                {
                    using (SqlCommand _cmd = new SqlCommand(_p, _cn.Conexion))
                    {
                        if (_ap != null)
                            _cmd.Parameters.AddRange(_ap.Sql());
                        UInt16 _cp = _ap.Retornos();
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Transaction = _cn.Transaccion;
                        _cmd.CommandTimeout = _cn.Tiempo;
                        if (_cn.Preparar)
                            _cmd.Prepare();

                        _da.SelectCommand = _cmd;
                        DataSet _ds = new DataSet();
                        _ds.EnforceConstraints = _cn.Restricciones;
                        _da.Fill(_ds);
                        if (_ds.Tables.Count > 0)
                        {
                            for (Int32 _i = 0; _i < _ds.Tables.Count; _i++)
                            {
                                if (_ds.Tables[_i].TableName.Length == 0)
                                    _ds.Tables[_i].TableName = "Procedure_" + _i.ToString();
                            }
                        }
                        if (_ds.DataSetName.Length == 0)
                            _ds.DataSetName = _p;
                        Return[] _return = new Return[2];
                        _return[0] = new Return(true, _ds);
                        _return[1] = new Return(true);
                        if (_cp > 0)
                        {
                            OrmSqlParameter[] _pto = new OrmSqlParameter[_cp];
                            _cp = 0;
                            foreach (SqlParameter _pi in _cmd.Parameters)
                            {
                                switch (_pi.Direction)
                                {
                                    case ParameterDirection.Output:
                                    case ParameterDirection.InputOutput:
                                    case ParameterDirection.ReturnValue:
                                        _pto[_cp] = new OrmSqlParameter(_pi);
                                        _cp++;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            _return[1] = new Return(true, _pto);
                        }
                        return _return;
                    }
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
        //public static Return Count(OrmSqlConnection _cn, STable _tv, Filter[] _ap = null)
        //{
        //    try
        //    {
        //        return SqlHelper.Execute(_cn, SqlHelper.Prepare_Count(_tv, _ap), EExecute.Scalar, _ap.Parametros());
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}

        //public static Return Recover(OrmSqlConnection _cn, STable _tv, Filter[] _pk = null)
        //{
        //    try
        //    {
        //        String _sql = "SELECT TOP(1) {0} FROM [{1}] WHERE {2};";
        //        //String[] _swk = _pk.Select_WhereKeys();
        //        _sql = String.Format(_sql, "*", _tv.Nombre, _pk.WhereKeys());
        //        _sql = StringHelper.Clean_Sql(_sql);
        //        Return _cr0 = SqlHelper.Get_DataSet(_cn, _sql, (OrmSqlParameter[])_pk.Parametros());
        //        _cr0.GatillarErrorExcepcion();
        //        using (DataTable _dt = ((DataSet)_cr0.Resultado).Tables[0])
        //        {
        //            if (_dt.Rows.Count > 0)
        //                return new Return(true, _dt.Rows[0]);
        //        }
        //        return new Return(false, "NO ENCONTRADO, NO RECUPERADO");
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //public static Return Extract(OrmSqlConnection _cn, STable _tv, UInt64 _i = 1)
        //{
        //    try
        //    {
        //        String _sqlf = "SELECT TOP({0}) * FROM [{1}] EXCEPT SELECT TOP({2}) * FROM [{1}];";
        //        _sqlf = String.Format(_sqlf, _i.ToString(), _tv.Nombre, (_i - 1).ToString());
        //        _sqlf = StringHelper.Clean_Sql(_sqlf);
        //        Return _cr0 = SqlHelper.Get_DataSet(_cn, _sqlf);
        //        _cr0.GatillarErrorExcepcion();
        //        using (DataTable _dt = ((DataSet)_cr0.Resultado).Tables[0])
        //        {
        //            if (_dt.Rows.Count > 0)
        //                return new Return(true, _dt.Rows[0]);
        //        }
        //        return new Return(false, "NO ENCONTRADO, NO EXTRAIDO");
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //public static Return Select_dt(OrmSqlConnection _cn, STable _tv, Filter[] _ap = null)
        //{
        //    try
        //    {
        //        //String _sqlf0 = "SELECT TOP({0}) {1} FROM [{2}] WHERE {3} GROUP BY {4} ORDER BY {5};";
        //        String[] _swgo = _ap.Select_Where_GroupBy_OrderBy();
        //        String _sql = "SELECT ";
        //        if (_cn.Tope > 0) _sql += String.Format("TOP({0}) ", _cn.Tope);
        //        _sql += String.Format("{0} FROM[{1}] ", _swgo[0], _tv.Nombre);
        //        if (_swgo[1].Length > 0) _sql += String.Format("WHERE {0} ", _swgo[1]);
        //        if (_swgo[2].Length > 0) _sql += String.Format("GROUP BY {0} ", _swgo[2]);
        //        if (_swgo[3].Length > 0) _sql += String.Format("ORDER BY {0} ", _swgo[3]);
        //        _sql += ";";
        //        _sql = StringHelper.Clean_Sql(_sql);
        //        Return _cr0 = SqlHelper.Get_DataSet(_cn, _sql, (OrmSqlParameter[])_ap.Parametros());
        //        _cr0.GatillarErrorExcepcion();
        //        DataTable _dt = ((DataSet)_cr0.Resultado).Tables[0];
        //        return new Return(true, _dt);
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        //public static Return Select_dv(OrmSqlConnection _cn, STable _tv, Filter[] _ap = null)
        //{
        //    try
        //    {
        //        Return _cr0 = SqlHelper.Select_dt(_cn, _tv, _ap);
        //        _cr0.GatillarErrorExcepcion();
        //        DataView _dv = new DataView()
        //        {
        //            Table = (DataTable)_cr0.Resultado,
        //            AllowDelete = false,
        //            AllowEdit = false,
        //            AllowNew = false
        //        };
        //        return new Return(true, _dv);
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
