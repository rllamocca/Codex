#if NET35
using System;

namespace Codex.Utility
{
    public static class EnumExtension
    {
        public static bool HasFlag(this Enum _this, Enum _flag)
        {
            if (_this.GetType() == _flag.GetType())
                return (Convert.ToInt64(_this) == Convert.ToInt64(_flag));

            return false;
        }
    }
}
#endif