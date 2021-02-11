using System.Collections.Generic;
using System.Data.SqlClient;

namespace Codex.ORM.Sql
{
    public static class OrmSqlParameterExtension
    {
        public static OrmSqlParameter[] GetSqlParameters(this IOrmParameter[] _array)
        {
            if (_array == null)
                return null;

            List<OrmSqlParameter> _return = new List<OrmSqlParameter>();

            foreach (OrmSqlParameter _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }

        public static SqlParameter[] GetParameters(this OrmSqlParameter _item)
        {
            if (_item == null)
                return null;

            return new SqlParameter[] { _item.Parameter };
        }
        public static SqlParameter[] GetParameters(this OrmSqlParameter[] _array)
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
