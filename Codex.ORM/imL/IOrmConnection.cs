using System;
using System.Threading;
using System.Threading.Tasks;

namespace Codex.ORM
{
    public interface IOrmConnection : IDisposable
    {
        int TimeOut { set; get; }
        bool Constraints { set; get; }
        bool Prepare { set; get; }
        CancellationToken Token { set; get; }

        void Open();
        void Close();

        Task OpenAsync();
    }
}
