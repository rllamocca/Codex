using System;
#if (NET35 == false && NET40 == false)
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Codex.ORM
{
    public interface IOrmConnection : IDisposable
    {
        int TimeOut { set; get; }
        bool Constraints { set; get; }
#if (NET35 == false && NET40 == false)
        CancellationToken Token { set; get; }
#endif

        void Open();
        void Close();

#if (NET35 == false && NET40 == false)
        Task OpenAsync();
#endif
    }
}
