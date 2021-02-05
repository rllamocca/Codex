using Codex.Generic;

using Codex.ORM.Enum;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codex.ORM.Sql.Data.Helper
{
    public static class SqlAsyncHelper
    {
        public static async Task<Return> Get_DataSet(
            String _query,
            OrmSqlConnection _conn,
            SqlParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = await Sql.Helper.SqlAsyncHelper.Execute(_query, _conn, EExecute.Reader, _pmts);
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
        public static async Task<Return> Get_DataTable(
            String _query,
            OrmSqlConnection _conn,
            SqlParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = await Sql.Helper.SqlAsyncHelper.Execute(_query, _conn, EExecute.Reader, _pmts);
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
                    await _bulk.WriteToServerAsync(_data);

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