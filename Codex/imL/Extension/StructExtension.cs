using System;

namespace Codex.Extension
{
    public static class StructExtension
    {
        public static bool IsDateTime(this DateTime _item)
        {
            return (_item.Hour > 0 || _item.Minute > 0 || _item.Second > 0 || _item.Millisecond > 0);
        }
        public static bool Excel18991231(this DateTime _item)
        {
            return (_item.Year == 1899 && _item.Month == 12 && _item.Day == 31);
        }

        public static DateTime ToDate(this DateTime _item)
        {
            return new DateTime(_item.Year, _item.Month, _item.Day);
        }
        public static DateTime ToExcelTime(this DateTime _item)
        {
            TimeSpan _tmp = _item - new DateTime(1899, 12, 31);
            return new DateTime(_tmp.Ticks);
        }


        public static DateTime ToExcelTime(this TimeSpan _item)
        {
            return new DateTime(1899, 12, 31).AddTicks(_item.Ticks);
        }
    }
}
