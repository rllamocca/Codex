using Codex.Generic;

using Codex.ORM.Enum;

using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Sql.Data.Helper
{
    public static class SqlHelper
    {
        public static Return Get_DataSet(
            String _query,
            OrmSqlConnection _conn,
            SqlParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = Sql.Helper.SqlHelper.Execute(_query, _conn, EExecute.Reader, _pmts);
                _exe.GatillarErrorExcepcion();

                DataSet _return = new DataSet("DataSet_0");
                _return.EnforceConstraints = _conn.Constraints;
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
        public static Return Get_DataTable(
            String _query,
            OrmSqlConnection _conn,
            SqlParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = Sql.Helper.SqlHelper.Execute(_query, _conn, EExecute.Reader, _pmts);
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

        public static Return Set_DataSet(
            DataSet _data,
            OrmSqlConnection _conn
            )
        {
            using (SqlBulkCopy _bulk = new SqlBulkCopy(_conn.Connection))
            {
                try
                {
                    foreach (DataTable _item in _data.Tables)
                    {
                        _bulk.DestinationTableName = _item.TableName;
                        _bulk.BatchSize = 1000;
                        _bulk.WriteToServer(_item);
                    }

                    return new Return(true);
                }
                catch (Exception _ex)
                {
                    return new Return(false, _ex);
                }
            }
        }
        public static Return Set_DataTable(
            DataTable _data,
            OrmSqlConnection _conn
            )
        {
            using (SqlBulkCopy _bulk = new SqlBulkCopy(_conn.Connection))
            {
                try
                {
                    _bulk.DestinationTableName = _data.TableName;
                    _bulk.BatchSize = 1000;
                    _bulk.WriteToServer(_data);

                    return new Return(true);
                }
                catch (Exception _ex)
                {
                    return new Return(false, _ex);
                }
            }
        }
    }
}