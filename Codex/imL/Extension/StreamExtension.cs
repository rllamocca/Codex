#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
using System.Threading;
using System.Threading.Tasks;
#endif

using System.IO;

namespace Codex.Extension
{
    public static class StreamExtension
    {
        public static void OldCopy(this Stream _item, Stream _to)
        {
            byte[] _buffer = new byte[1024];
            int _read;
            while ((_read = _item.Read(_buffer, 0, _buffer.Length)) > 0)
                _to.Write(_buffer, 0, _read);
        }

#if (NET35 || NET40 || NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
        public static void FileCreate(this Stream _item, string _path)
        {
            using (FileStream _sw = File.Create(_path))
            {
#if (NET35)
                _item.OldCopy(_sw);
#else
                _item.CopyTo(_sw);
#endif
            }
        }
#endif

#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
        public async static Task FileCreateAsync(this Stream _item, string _path, CancellationToken _token = default)
        {
            using (FileStream _sw = File.Create(_path, 1024, FileOptions.Asynchronous))
                await _item.CopyToAsync(_sw, 1024, _token);
        }
#endif
    }
}
