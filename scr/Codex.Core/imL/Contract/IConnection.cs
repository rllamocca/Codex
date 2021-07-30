#if (NET45 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD2_0)
using System.Threading;
using System.Threading.Tasks;
#endif

using System;

namespace Codex.Contract
{
    public interface IConnection : IDisposable
    {
        int TimeOut { set; get; }
        bool Constraints { set; get; }
#if (NET45 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD2_0)
        CancellationToken Token { set; get; }
#endif

        void Open();
        void Close();

#if (NET45 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD2_0)
        Task OpenAsync();
#endif
    }
}
