using System;

namespace Codex
{
    public static class PrimitiveExtension
    {
        public static bool HasValue(this string _item)
        {
            return (_item != null);
        }
        public static bool HasValueLength(this string _item)
        {
            return (_item != null && _item.Length > 0);
        }
        public static bool HasValueTrimLength(this string _item)
        {
            return (_item != null && _item.Trim().Length > 0);
        }
        public static string Clean_TB(this string _item)
        {
            if (_item == null)
                return null;

            if (_item.Length > 0)
            {
                char _sp = (char)32;

                char _tb = (char)9;

                _item = _item.Replace(_tb, _sp);
            }
            return _item;
        }
        public static string Clean_LF(this string _item)
        {
            if (_item == null)
                return null;

            if (_item.Length > 0)
            {
                char _sp = (char)32;

                char _lf = (char)10;

                _item = _item.Replace(_lf, _sp);
            }
            return _item;
        }
        public static string Clean_CR(this string _item)
        {
            if (_item == null)
                return null;

            if (_item.Length > 0)
            {
                char _sp = (char)32;

                char _cr = (char)13;

                _item = _item.Replace(_cr, _sp);
            }
            return _item;
        }
        public static string Clean_TBLFCR(this string _item)
        {
            if (_item == null) 
                return null;

            if (_item.Length > 0)
            {
                char _sp = (char)32;

                char _tb = (char)9;
                char _lf = (char)10;
                char _cr = (char)13;
                
                _item = _item.Replace(_tb, _sp);
                _item = _item.Replace(_lf, _sp);
                _item = _item.Replace(_cr, _sp);
            }
            return _item;
        }


        public static DateTime ToExcelDateTime(this TimeSpan _a)
        {
            return new DateTime(1899, 12, 31).AddTicks(_a.Ticks);
        }
    }
}
