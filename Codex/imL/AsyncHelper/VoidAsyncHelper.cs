/*

NETSTANDARD1_0
NETSTANDARD1_1
NETSTANDARD1_2
NETSTANDARD1_3
NETSTANDARD1_4
NETSTANDARD1_5
NETSTANDARD1_6
NETSTANDARD2_0 
NETSTANDARD2_1

*/


#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Codex.AsyncHelper
{

    public static class VoidAsyncHelper
    {
        public static Task Write(ref Stream _a, String _b, CancellationToken _token = default)
        {
            return Write(_a, _b, _token);
        }
        private async static Task Write(Stream _a, String _b, CancellationToken _token = default)
        {
            using (FileStream _sw = File.Create(_b, 1024, FileOptions.Asynchronous))
            {
                _a.Seek(0, SeekOrigin.Begin);
                await _a.CopyToAsync(_sw, 1024, _token);
            }
        }
    }

}

#endif
