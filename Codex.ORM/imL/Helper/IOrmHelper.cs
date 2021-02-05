using Codex.Generic;
using Codex.ORM.Enum;

namespace Codex.ORM.Helper
{
    public interface IOrmHelper
    {
        Return Execute(
            string _query,
            IOrmConnection _conn,
            EExecute _exe = EExecute.NonQuery,
            IOrmParameter[] _pmts = null
            );

        Return Get_DataSet(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null
            );
        Return Get_DataTable(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null
            );
    }
}
