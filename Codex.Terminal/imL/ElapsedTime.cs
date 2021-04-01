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
            this._TIMER.Stop();
            //################################
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
            ConsoleHelper.Write(this._BAR_START, new string(' ', 40));
            ConsoleHelper.Write(this._BAR_START, _text);
            //################################
            this._TIMER.Start();
        }

        public ElapsedTime(ProgressBar _parent = null)
        {
            this._PARENT = _parent;

            this.Init(0);

            this._START = DateTime.Now;

            this._TIMER = new Timer();
            this._TIMER.Interval = 500;
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

            this.Dispose(_managed);
        }
    }
}
#endif