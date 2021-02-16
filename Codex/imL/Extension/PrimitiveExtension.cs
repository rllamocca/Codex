#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)
using System.Net.Mail;
#endif

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
#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)
        public static bool IsMail(this string _item)
        {
            if (_item == null)
                return false;
            try
            {
                new MailAddress(_item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
#endif

        public static string CleanTB(this string _item)
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
        public static string CleanLF(this string _item)
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
        public static string CleanCR(this string _item)
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
        public static string CleanTBLFCR(this string _item)
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
    }
}
