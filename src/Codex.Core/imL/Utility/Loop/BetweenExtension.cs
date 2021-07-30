using Codex.Enumeration;

namespace Codex.Utility
{
    public static class BetweenExtension
    {
        public static bool Between(this sbyte _this, sbyte _a, sbyte _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this byte _this, byte _a, byte _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this short _this, short _a, short _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this ushort _this, ushort _a, ushort _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this int _this, int _a, int _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this uint _this, uint _a, uint _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this long _this, long _a, long _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this ulong _this, ulong _a, ulong _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }

        public static bool Between(this float _this, float _a, float _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this double _this, double _a, double _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
        }
        public static bool Between(this decimal _this, decimal _a, decimal _z, EInterval _interval = EInterval.Inclusive)
        {
            switch (_interval)
            {
                case EInterval.Exclusive:
                    return _a < _this && _this < _z;
                case EInterval.Until:
                    return _a < _this && _this <= _z;
                case EInterval.Since:
                    return _a <= _this && _this < _z;
                case EInterval.Inclusive:
                    return _a <= _this && _this <= _z;
                default:
                    return _a <= _this && _this <= _z;
            }
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
