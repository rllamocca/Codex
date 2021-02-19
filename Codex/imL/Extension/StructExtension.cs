using System;

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

        public static DateTime ToDate(this DateTime _this)
        {
            return new DateTime(_this.Year, _this.Month, _this.Day);
        }
        public static DateTime ToExcelTime(this DateTime _this)
        {
            TimeSpan _tmp = _this - new DateTime(1899, 12, 31);
            return new DateTime(_tmp.Ticks);
        }


        public static DateTime ToExcelTime(this TimeSpan _this)
        {
            return new DateTime(1899, 12, 31).AddTicks(_this.Ticks);
        }
    }
}
