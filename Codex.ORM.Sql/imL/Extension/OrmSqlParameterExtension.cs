using System.Collections.Generic;
using System.Data.SqlClient;

namespace Codex.ORM.Sql.Extension
{
    public static class OrmSqlParameterExtension
    {
        public static SqlParameter[] GetSqlParameters(this OrmSqlParameter _item)
        {
            if (_item == null)
                return null;

            return new SqlParameter[] { _item.Parameter };
        }
        public static SqlParameter[] GetSqlParameters(this OrmSqlParameter[] _array)
        {
            if (_array == null)
                return null;

            List<SqlParameter> _return = new List<SqlParameter>();
            foreach (OrmSqlParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}
