#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)
using System.Net.Mail;
#endif

using System;
using System.Linq;

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
            return Bases._TIMESTAMP.AddSeconds(_this);
        }

        public static bool Between(this sbyte _this, sbyte _a, sbyte _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this byte _this, byte _a, byte _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this short _this, short _a, short _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this ushort _this, ushort _a, ushort _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this int _this, int _a, int _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this uint _this, uint _a, uint _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this long _this, long _a, long _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this ulong _this, ulong _a, ulong _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }

        public static bool Between(this float _this, float _a, float _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this double _this, double _a, double _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
        public static bool Between(this decimal _this, decimal _a, decimal _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }

        //public static bool IsXXX<T>(this T _this, T _a, T _z, bool _inclusive = true) where T : struct,
        //    IComparable<T>,
        //    IEquatable<T>
        //{
        //    return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;

        //    return false;
        //}

        //public static decimal Add<T>(T a, T b) where T : struct,
        //    IComparable,
        //    IComparable<T>,
        //    IConvertible,
        //    IEquatable<T>,
        //    IFormattable
        //{
        //    return a.ToDecimal(null) + b.ToDecimal(null);
        //}
    }
}
