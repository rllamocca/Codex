#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)

using Codex.Terminal.Helper;

using System;
using System.Timers;

namespace Codex.Terminal
{
    public class ElapsedTime : ProgressBar
    {
        private bool _DISPOSED = false;

        private const string _ANIMATION = @"+\|/";
        private readonly Timer _TIMER;
        private byte _ANIMATIONINDEX = 0;
        private DateTime _START;

        private void OnElapsedEvent(object _source, ElapsedEventArgs _e)
        {
            TimeSpan _diff = _e.SignalTime - this._START;

            string _text = string.Format(
                "[{0}]  {1}",
                _ANIMATION[this._ANIMATIONINDEX++ % 4],
#if (NET35)
                _diff.ToString()
#else
                _diff.ToString("hh':'mm':'ss'.'fff")
#endif
                );
            ConsoleHelper.Write(base._BAR_START, new string(' ', 40));
            ConsoleHelper.Write(base._BAR_START, _text);
        }

        public ElapsedTime(ProgressBar _parent = null)
        {
            base._PARENT = _parent;

            base.Init(0);

            this._START = DateTime.Now;
            this._TIMER = new Timer(200);
            this._TIMER.Elapsed += this.OnElapsedEvent;
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
                this._ANIMATIONINDEX = 0;
                this._START = DateTime.MinValue;
            }
            this._DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}
#endif