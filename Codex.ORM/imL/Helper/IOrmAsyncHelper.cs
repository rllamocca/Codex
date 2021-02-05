using Codex.Generic;
using Codex.ORM.Enum;

using System.Threading.Tasks;

namespace Codex.ORM.Helper
{
    public interface IOrmAsyncHelper
    {
        Task<Return> Execute(
            string _query,
            IOrmConnection _conn,
            EExecute _exe = EExecute.NonQuery,
            IOrmParameter[] _pmts = null
            );

        Task<Return> Get_DataSet(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null
            );
        Task<Return> Get_DataTable(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null
            );
    }
}
