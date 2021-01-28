#if !(NET35 || NET40)

using Codex.Generic;

using Codex.ORM.Enum;
using Codex.ORM.Generic;

using System;
using System.Threading.Tasks;

namespace Codex.ORM.Data.Helper
{
    public static class OrmAsyncHelper
    {
        public async static Task<Return> Execute(
            OrmConnection _con,
            String _query,
            EExecute _exe = EExecute.NonQuery,
            OrmParameter[] _pmts = null
            )
        {
            if (_con is OrmSqlConnection _cn)
            {
                return await SqlAsyncHelper.Execute(_cn, _query, _exe, (OrmSqlParameter[])_pmts);
            }

            return new Return(false);
        }
        public async static Task<Return> Get_DataSet(
            OrmConnection _con,
            String _query,
            OrmParameter[] _pmts = null
            )
        {
            if (_con is OrmSqlConnection _cn)
            {
                return await SqlAsyncHelper.Get_DataSet(_cn, _query, (OrmSqlParameter[])_pmts);
            }

            return new Return(false);
        }
        public async static Task<Return> Get_DataTable(
            OrmConnection _con,
            String _query,
            OrmParameter[] _pmts = null
            )
        {
            if (_con is OrmSqlConnection _cn)
            {
                return await SqlAsyncHelper.Get_DataTable(_cn, _query, (OrmSqlParameter[])_pmts);
            }

            return new Return(false);
        }
    }
}

#endif