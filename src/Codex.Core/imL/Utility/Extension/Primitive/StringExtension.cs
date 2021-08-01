using Codex.Enumeration;

using System.Linq;

namespace Codex.Utility
{
    public static class StringExtension
    {
        public static bool HasValue(this string _this)
        {
            return (_this != null);
        }
        public static bool HasValueLength(this string _this)
        {
            return (_this.HasValue() && _this.Length > 0);
        }
        public static bool HasValueTrim(this string _this)
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
