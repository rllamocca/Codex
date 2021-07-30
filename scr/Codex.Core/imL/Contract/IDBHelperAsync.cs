#if (NET45 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD2_0)

using Codex.Enumeration;

using System;
using System.Threading.Tasks;

namespace Codex.Contract
{
    public interface IDBHelperAsync
    {
        Task<Return> Execute(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery,
            bool _throw = false
            );
        Task<Return[]> Execute(
            string _query,
            IConnection _conn,
            IParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery,
            IProgress<int> _progress = null,
            bool _throw = false
            );

        Task<Return> Get_DataTable(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            bool _throw = false
            );
        Task<Return> Get_DataSet(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            IProgress<int> _progress = null,
            bool _throw = false
            );
    }
}
#endif