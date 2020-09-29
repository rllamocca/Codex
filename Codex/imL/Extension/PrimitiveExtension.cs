using System;

namespace Codex.Extension
{
    public static class PrimitiveExtension
    {
        public static Boolean HasValue(this String _a)
        {
            return (_a != null);
        }
        public static Boolean HasValueAndLength(this String _a)
        {
            return (_a != null && _a.Length > 0);
        }
        public static DateTime ToExcelDateTime(this TimeSpan _a)
        {
            DateTime _return = new DateTime(1899, 12, 31).AddTicks(_a.Ticks);
            return _return;
        }
    }
}
