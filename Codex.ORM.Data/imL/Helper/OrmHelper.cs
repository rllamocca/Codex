using System;

using Codex.Generic;

using Codex.ORM.Enum;
using Codex.ORM.Generic;

namespace Codex.ORM.Data.Helper
{
    public static class OrmHelper
    {
        public static Return Execute(
            OrmConnection _con,
            String _query,
            EExecute _exe = EExecute.NonQuery,
            OrmParameter[] _pmts = null
            )
        {
            if (_con is OrmSqlConnection _cn)
            {
                return SqlHelper.Execute(_cn, _query, _exe, (OrmSqlParameter[])_pmts);
            }

            return new Return(false);
        }
        public static Return Get_DataSet(
            OrmConnection _con,
            String _query,
            OrmParameter[] _pmts = null
            )
        {
            if (_con is OrmSqlConnection _cn)
            {
                return SqlHelper.Get_DataSet(_cn, _query, (OrmSqlParameter[])_pmts);
            }

            return new Return(false);
        }
        public static Return Get_DataTable(
            OrmConnection _con,
            String _query,
            OrmParameter[] _pmts = null
            )
        {
            if (_con is OrmSqlConnection _cn)
            {
                return SqlHelper.Get_DataTable(_cn, _query, (OrmSqlParameter[])_pmts);
            }

            return new Return(false);
        }
    }
}
