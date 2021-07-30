using Codex.Enumeration;

using System;

namespace Codex.Contract
{
    public interface IDBHelper
    {
        Return Execute(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery,
            bool _throw = false
            );
        Return[] Execute(
            string _query,
            IConnection _conn,
            IParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery,
            IProgress<int> _progress = null,
            bool _throw = false
            );

        Return Get_DataTable(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            bool _throw = false
            );
        Return Get_DataSet(
            string _query,
            IConnection _conn,
            IParameter[] _pmts = null,
            IProgress<int> _progress = null,
            bool _throw = false
            );
    }
}
