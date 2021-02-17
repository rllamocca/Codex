using Codex.Enum;

namespace Codex.Helper
{
    public interface IOrmHelper
    {
        Return Execute(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[] _pmts = null,
            EExecute _exe = EExecute.NonQuery
            );
        Return[] Execute(
            string _query,
            IOrmConnection _conn,
            IOrmParameter[][] _pmts,
            EExecute _exe = EExecute.NonQuery
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
