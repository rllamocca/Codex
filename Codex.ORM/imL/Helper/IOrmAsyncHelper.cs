using Codex.ORM.Enum;

using System.Threading.Tasks;

namespace Codex.ORM.Helper
{
    public interface IOrmAsyncHelper
    {
        Task<Return> Execute(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery
            );
        Task<Return[]> Execute(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery
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
