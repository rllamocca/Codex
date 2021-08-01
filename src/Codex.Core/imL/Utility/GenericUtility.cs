using System;

namespace Codex.Utility
{
    public static class GenericUtility
    {
        public static T ToAssignReturn<T>(T _value, T _affect)
        {
            if (_value != null)
                return _value;

            return _affect;
        }
        public static void ToAssignRef<T>(T _value, ref T _affect)
        {
            if (_value != null)
                _affect = _value;
        }

        public static void ToAssignDelegate<T>(T _value, Action<T> _affect)
        {
            if (_value != null)
                _affect(_value);
        }
    }
}
