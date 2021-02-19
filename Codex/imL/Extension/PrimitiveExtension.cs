#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)
using System.Net.Mail;
#endif

using System;

namespace Codex.Extension
{
    public static class PrimitiveExtension
    {
        public static bool HasValue(this string _this)
        {
            return (_this != null);
        }
        public static bool HasValueLength(this string _this)
        {
            return (_this != null && _this.Length > 0);
        }
        public static bool HasValueTrimLength(this string _this)
        {
            return (_this != null && _this.Trim().Length > 0);
        }
#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)
        public static bool IsMail(this string _this)
        {
            if (_this == null)
                return false;
            try
            {
                new MailAddress(_this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
#endif

        public static string CleanTB(this string _this)
        {
            if (_this == null)
                return null;

            if (_this.Length > 0)
            {
                char _sp = (char)32;

                char _tb = (char)9;

                _this = _this.Replace(_tb, _sp);
            }
            return _this;
        }
        public static string CleanLF(this string _this)
        {
            if (_this == null)
                return null;

            if (_this.Length > 0)
            {
                char _sp = (char)32;

                char _lf = (char)10;

                _this = _this.Replace(_lf, _sp);
            }
            return _this;
        }
        public static string CleanCR(this string _this)
        {
            if (_this == null)
                return null;

            if (_this.Length > 0)
            {
                char _sp = (char)32;

                char _cr = (char)13;

                _this = _this.Replace(_cr, _sp);
            }
            return _this;
        }
        public static string CleanTBLFCR(this string _this)
        {
            if (_this == null)
                return null;

            if (_this.Length > 0)
            {
                char _sp = (char)32;

                char _tb = (char)9;
                char _lf = (char)10;
                char _cr = (char)13;

                _this = _this.Replace(_tb, _sp);
                _this = _this.Replace(_lf, _sp);
                _this = _this.Replace(_cr, _sp);
            }
            return _this;
        }
    }
}
