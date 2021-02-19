using System;

namespace Codex.Extension
{
    public static class EnumExtension
    {
        public static bool HasFlag(this System.Enum _this, System.Enum _flag)
        {
            if (_this.GetType() == _flag.GetType())
                return (Convert.ToUInt64(_this) == Convert.ToUInt64(_flag));

            return false;
        }
    }
}
