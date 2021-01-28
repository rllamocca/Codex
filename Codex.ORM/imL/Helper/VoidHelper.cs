using System;
using System.Data.SqlClient;

namespace Codex.ORM.Helper
{
    public static class VoidHelper
    {
        public static void Set_DBNull(SqlParameterCollection _array)
        {
            foreach (SqlParameter _item in _array)
            {
                if (_item.Value == null)
                    _item.Value = DBNull.Value;
            }
        }
    }
}
