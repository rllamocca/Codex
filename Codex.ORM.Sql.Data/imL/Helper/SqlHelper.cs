using Codex.Generic;
using Codex.ORM.Enum;
using Codex.ORM.Helper;

using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Sql.Data.Helper
{
    public class SqlHelper : Sql.Helper.SqlHelper, IOrmHelper
    {
        public override Return Get_DataSet(string _query, IOrmConnection _conn, IOrmParameter[] _pmts = null)
        {
            try
            {
                Return _exe = Execute(_query, _conn, _pmts, EExecute.Reader);
                _exe.GatillarErrorExcepcion();

                DataSet _return = new DataSet("DataSet_0");
                _return.EnforceConstraints = _conn.Constraints;
                UInt16 _n = 0;
                using (SqlDataReader _read = (SqlDataReader)_exe.Result)
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

        public override Return Get_DataTable(string _query, IOrmConnection _conn, IOrmParameter[] _pmts = null)
        {
            try
            {
                Return _exe = Execute(_query, _conn, _pmts, EExecute.Reader);
                _exe.GatillarErrorExcepcion();

                DataTable _return = new DataTable("DataTable_0");
                using (SqlDataReader _read = (SqlDataReader)_exe.Result)
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