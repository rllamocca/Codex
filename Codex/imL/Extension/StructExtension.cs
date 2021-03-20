using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Codex.Extension
{
    public static class StructExtension
    {
        public static bool IsDateTime(this DateTime _this)
        {
            return (_this.Hour > 0 || _this.Minute > 0 || _this.Second > 0 || _this.Millisecond > 0);
        }
        public static bool Excel18991231(this DateTime _item)
        {
            return (_item.Year == 1899 && _item.Month == 12 && _item.Day == 31);
        }

        public static DateTime ToExcelTime(this DateTime _this)
        {
            TimeSpan _tmp = _this - new DateTime(1899, 12, 31);
            return new DateTime(_tmp.Ticks);
        }

        public static DateTime[] DaysBetween(this DateTime _this, DateTime _z, DayOfWeek _day = DayOfWeek.Sunday)
        {
            DateTime _a = _this;
            List<DateTime> _return = new List<DateTime>();

            while (_a <= _z)
            {
                if (_a.DayOfWeek == _day)
                {
                    _return.Add(_a);
                    _a = _a.AddDays(6);
                }
                else
                    _a = _a.AddDays(1);
            }

            return _return.ToArray();
        }
        public static double ToTimeStamp(this DateTime _this)
        {
            TimeSpan _diff = _this.ToUniversalTime() - ReadOnly._TIMESTAMP;
            return _diff.TotalSeconds;
        }

        public static bool Between(this DateTime _this, DateTime _a, DateTime _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }

        public static DateTime ToExcelTime(this TimeSpan _this)
        {
            return new DateTime(1899, 12, 31).AddTicks(_this.Ticks);
        }

        //public static bool Between<T>(this T _this, T _a, T _z, bool _inclusive = true) where T : struct
        //{
        //    switch (_this)
        //    {
        //        case sbyte _this2:
        //            sbyte _a1 = (sbyte)Convert.ChangeType(_a, typeof(sbyte));
        //            sbyte _z1 = (sbyte)_z;
        //            return _inclusive ? _a <= _this2 && _this2 <= _z : _a < _this2 && _this2 < _z;
        //        case byte _B:
        //            return _inclusive ? _a <= _B && _B <= _z : _a < _B && _B < _z;
        //        default:
        //            throw new ArgumentException("is not a recognized", nameof(_this));
        //    }
        //    return false;
        //}
    }
}
