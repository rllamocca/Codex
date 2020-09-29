#if (NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.IO;
using System.IO.Compression;
using System.Text;

using Codex.Enum;

namespace Codex.Helper
{
    public static class StreamHelper
    {
        public static Stream DeCompress(Stream _s, ECompress _compress)
        {
            _s.Seek(0, SeekOrigin.Begin);
            Stream _ms = new MemoryStream();
            Stream _com = null;
            switch (_compress)
            {
                case ECompress.Gzip:
                    _com = new GZipStream(_s, CompressionMode.Decompress, true);
                    break;
                case ECompress.Deflate:
                    _com = new DeflateStream(_s, CompressionMode.Decompress, true);
                    break;
                default:
                    break;
            }
            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _com.CopyTo(_ms);
                    _com.Dispose();
                    _ms.Seek(0, SeekOrigin.Begin);
                    break;
                default:
                    return _s;
            }
            return _ms;
        }
        public static Stream Compress(Stream _s, ECompress _compress)
        {
            _s.Seek(0, SeekOrigin.Begin);
            Stream _ms = new MemoryStream();
            Stream _com = null;
            switch (_compress)
            {
                case ECompress.Gzip:
                    _com = new GZipStream(_ms, CompressionMode.Compress, true);
                    break;
                case ECompress.Deflate:
                    _com = new DeflateStream(_ms, CompressionMode.Compress, true);
                    break;
                default:
                    break;
            }
            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _s.CopyTo(_com);
                    _com.Dispose();
                    _ms.Seek(0, SeekOrigin.Begin);
                    break;
                default:
                    return _s;
            }
            return _ms;
        }
    }
}

#endif