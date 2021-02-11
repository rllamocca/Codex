using System;

namespace Codex.Extension
{
    public static class PrimitiveExtension
    {
        public static bool HasValue(this string _a)
        {
            return (_a != null);
        }
        public static bool HasValueAndLength(this string _a)
        {
            return (_a != null && _a.Length > 0);
        }
        public static bool HasValueAndTrimLength(this string _a)
        {
            return (_a != null && _a.Trim().Length > 0);
        }

        public static DateTime ToExcelDateTime(this TimeSpan _a)
        {
            return new DateTime(1899, 12, 31).AddTicks(_a.Ticks);
        }
    }
}
