using System;

namespace Codex.Enum
{
    [Flags]
    public enum EEndLine : byte
    {
        HT = 0, // 9 Horizontal Tab
        LF = 1, // 10 Line Feed
        VT = 2, // 11 Vertical Tab
        FF = 4, // 12 Form Feed
        CR = 8, // 13 Carriage Return

        All = HT | LF | VT | FF | CR
    }
}
