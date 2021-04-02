#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)

using System;
using System.Timers;

namespace Codex.Terminal
{
    public class ElapsedTime : ProgressBar
    {
        private bool _DISPOSED = false;

        private readonly Timer _TIMER;

        private void ElapsedEvent(object _source, ElapsedEventArgs _e)
        {
            this.DrawTime(_e.SignalTime);
        }

        public ElapsedTime(ProgressBar _parent = null)
        {
            this._START = DateTime.Now;
            this._PARENT = _parent;

            this.Init(0);

            this.DrawTime(this._START);

            this._TIMER = new Timer(1000);
            this._TIMER.Elapsed += this.ElapsedEvent;
            this._TIMER.Start();
        }

        protected override void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                if (this._TIMER != null)
                {
                    this._TIMER.Stop();
                    this._TIMER.Dispose();
                }
            }
            this._DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}
#endif