using System;
using System.Collections.Generic;

namespace Codex.Utility
{
    public static class ObjectExtension
    {
        public static bool HasValue(this object _this)
        {
            return (_this != null);
        }
        public static string DBToString(this object _this, bool _empty = false)
        {
            if (_this == null
#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)
                || _this == DBNull.Value
#endif
                )
            {
                if (_empty)
                    return string.Empty;
                return null;
            }
            return Convert.ToString(_this);
        }
        public static string[] DBToString(this object[] _array, bool _empty = false)
        {
            if (_array == null)
                return null;

            List<string> _return = new List<string>();

            foreach (object _item in _array)
                _return.Add(_item.DBToString(_empty));

            return _return.ToArray();
        }
    }
}
