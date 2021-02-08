using Codex.Generic;

using Codex.ORM.Enum;
using Codex.ORM.Helper;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codex.ORM.Sql.Data.Helper
{
    public class SqlAsyncHelper : Sql.Helper.SqlAsyncHelper, IOrmAsyncHelper
    {
        public override async Task<Return> Get_DataSet(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = await Execute(_query, _conn, _pmts, EExecute.Reader);
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
        public override async Task<Return> Get_DataTable(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null
            )
        {
            try
            {
                Return _exe = await Execute(_query, _conn, _pmts, EExecute.Reader);
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
    }
}