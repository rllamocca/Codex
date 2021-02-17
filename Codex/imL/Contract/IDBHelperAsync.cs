#if (NET45 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD2_0)

using Codex.Enum;

using System.Threading.Tasks;

namespace Codex.Contract
{
    public interface IDBHelperAsync
    {
        Task<Return> Execute(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery
            );
        Task<Return[]> Execute(
            string _query,
            IConnection _conn,
            IParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery
            );

        Task<Return> Get_DataSet(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null
            );
        Task<Return> Get_DataTable(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null
            );
    }
}
#endif