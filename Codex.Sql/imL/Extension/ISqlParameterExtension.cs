using Codex.Contract;
using Codex.Sql.UserModel;

using System.Collections.Generic;
using System.Data.SqlClient;

namespace Codex.Sql.Extension
{
    public static class ISqlParameterExtension
    {
        public static ISqlParameter[] GetSqlParameters(this IParameter[] _array)
        {
            if (_array == null)
                return null;

            List<ISqlParameter> _return = new List<ISqlParameter>();

            foreach (ISqlParameter _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }

        public static SqlParameter[] GetParameters(this ISqlParameter _item)
        {
            if (_item == null)
                return null;

            return new SqlParameter[] { _item.Parameter };
        }
        public static SqlParameter[] GetParameters(this ISqlParameter[] _array)
        {
            if (_array == null)
                return null;

            List<SqlParameter> _return = new List<SqlParameter>();
            foreach (ISqlParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}
