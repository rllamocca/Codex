using System;

namespace Codex.Extension
{
    public static class StructExtension
    {
        public static DateTime ToExcelDateTime(this TimeSpan _a)
        {
            return new DateTime(1899, 12, 31).AddTicks(_a.Ticks);
        }
    }
}
