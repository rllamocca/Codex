#if (NET35 || NET40 || NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)

using System;
using System.IO;
using System.Text;

namespace Codex
{
    public static class EncodingHelper
    {
        public static Encoding Get_Encoding(string _path)
        {
            Byte[] _b = new Byte[5];
            using (FileStream _fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
                _fs.Read(_b, 0, 5);

            if (_b[0] == 0xFF && _b[1] == 0xFE && _b[2] == 0x00 && _b[3] == 0x00) return Encoding.UTF32;
            if (_b[0] == 0x00 && _b[1] == 0x00 && _b[2] == 0xFE && _b[3] == 0xFF) return new UTF32Encoding(true, true);

            if (_b[0] == 0xFF && _b[1] == 0xFE) return Encoding.Unicode;
            if (_b[0] == 0xFE && _b[1] == 0xFF) return Encoding.BigEndianUnicode;

            if (_b[0] == 0xEF && _b[1] == 0xBB && _b[2] == 0xBF) return Encoding.UTF8;

            if (_b[0] == 0x2B && _b[1] == 0x2F && _b[2] == 0x76 && _b[3] == 0x38 && _b[4] == 0x2D) return Encoding.UTF7;

            return Encoding.ASCII;
        }
    }
}
#endif
