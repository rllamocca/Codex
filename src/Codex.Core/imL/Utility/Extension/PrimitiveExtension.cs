using System;
using System.Linq;
using System.Collections.Generic;

using Codex.Enumeration;

namespace Codex.Utility
{
    public static class PrimitiveExtension
    {
        public static bool HasValue(this object _this)
        {
            return (_this != null);
        }

        public static bool HasValue(this string _this)
        {
            return (_this != null);
        }
        public static bool HasValueLength(this string _this)
        {
            return (_this.HasValue() && _this.Length > 0);
        }
        public static bool HasValueTrimLength(this string _this)
        {
            return (_this.HasValue() && _this.Trim().Length > 0);
        }

        public static string ReplaceEndLine(this string _this, EEndLine _re)
        {
            if (_this == null)
                return null;

            if (_this.Length > 0)
            {
                char _sp = (char)32;

                char _ht = (char)9;
                char _lf = (char)10;
                char _vt = (char)11;
                char _ff = (char)12;
                char _cr = (char)13;

                if (_re.HasFlag(EEndLine.HT)) _this = _this.Replace(_ht, _sp);
                if (_re.HasFlag(EEndLine.LF)) _this = _this.Replace(_lf, _sp);
                if (_re.HasFlag(EEndLine.VT)) _this = _this.Replace(_vt, _sp);
                if (_re.HasFlag(EEndLine.FF)) _this = _this.Replace(_ff, _sp);
                if (_re.HasFlag(EEndLine.CR)) _this = _this.Replace(_cr, _sp);
            }

            return _this;
        }

        public static List<char> RemoveEndLine(this List<char> _array, EEndLine _re)
        {
            if (_array == null)
                return null;

            if (_array.Count > 0)
            {
                char _ht = (char)9;
                char _lf = (char)10;
                char _vt = (char)11;
                char _ff = (char)12;
                char _cr = (char)13;

                if (_re.HasFlag(EEndLine.HT)) _array.Remove(_ht);
                if (_re.HasFlag(EEndLine.LF)) _array.Remove(_lf);
                if (_re.HasFlag(EEndLine.VT)) _array.Remove(_vt);
                if (_re.HasFlag(EEndLine.FF)) _array.Remove(_ff);
                if (_re.HasFlag(EEndLine.CR)) _array.Remove(_cr);
            }

            return _array;
        }

        public static bool ArgAppear(this string[] _array, params string[] _synonym)
        {
            bool _return = false;

            foreach (string _item in _synonym)
            {
                //_return = _return || (_array.Count(_c => _c.CompareTo(_item) == 1) > 0);
                _return = _return || (_array.Count(_c => _c.ToUpper() == _item.ToUpper()) > 0);

                if (_return)
                    return _return;
            }

            return _return;
        }
        public static string ArgValue(this string[] _array, params string[] _key)
        {
            foreach (string _item in _key)
            {
                string _return = _array.SkipWhile(_sw => _sw.ToUpper() != _item.ToUpper()).Skip(1).FirstOrDefault();

                if (_return != null)
                    return _return;
            }

            return null;
        }

        public static DateTime TimeStampToDateTime(this double _this)
        {
            return ReadOnly._TIMESTAMP.AddSeconds(_this);
        }

        public static string[] ConvertToString(this char[] _array)
        {
            if (_array == null)
                return null;

            string[] _return = new string[_array.Length];

            for (int _i = 0; _i < _return.Length; _i++)
                _return[_i] = Convert.ToString(_array[_i]);

            return _return;
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

        public static string Replace(this string _this, char[] _old, char _new)
        {
            if (_this == null)
                return null;

            string _return = _this;
            _old = _old.Distinct().ToArray();
            foreach (char _item in _old)
                _return = _return.Replace(_item, _new);

            return _return;
        }
        public static string Replace(this string _this, string[] _old, string _new)
        {
            if (_this == null)
                return null;

            string _return = _this;
            _old = _old.Distinct().ToArray();
            foreach (string _item in _old)
                _return = _return.Replace(_item, _new);

            return _return;
        }
    }
}
