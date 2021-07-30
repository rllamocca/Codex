using Codex.Contract;
using Codex.Utility.MySql.UserModel;

using MySql.Data.MySqlClient;

using System.Collections.Generic;

namespace Codex.Utility.MySql
{
    public static class IMySqlParameterExtension
    {
        public static IMySqlParameter[] GetSqlParameters(this IParameter[] _array)
        {
            if (_array == null)
                return null;

            List<IMySqlParameter> _return = new List<IMySqlParameter>();

            foreach (IMySqlParameter _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }

        public static MySqlParameter[] GetParameters(this IMySqlParameter _this)
        {
            if (_this == null)
                return null;

            return new MySqlParameter[] { _this.Parameter };
        }
        public static MySqlParameter[] GetParameters(this IMySqlParameter[] _array)
        {
            if (_array == null)
                return null;

            List<MySqlParameter> _return = new List<MySqlParameter>();
            foreach (IMySqlParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}
