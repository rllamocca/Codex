using System;

namespace Codex.Extension
{
    public static class EnumExtension
    {
        public static bool HasFlag(this System.Enum _item, System.Enum _flag)
        {
            if (_item.GetType() == _flag.GetType())
                return (Convert.ToUInt64(_item) == Convert.ToUInt64(_flag));

            return false;
        }
    }
}
