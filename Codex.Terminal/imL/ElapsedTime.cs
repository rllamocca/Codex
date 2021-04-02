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

        private void Report(DateTime _value)
        {
            TimeSpan _diff = _value - this._START;

            string _text = string.Format(
                "[{0}]  {1}",
                _ANIMATION[this._ANIMATIONINDEX++ % 4],
#if (NET35)
                _diff.ToString()
#else
                _diff.ToString("hh':'mm':'ss")
#endif
                );
            //ConsoleHelper.Write(this._BAR_START, new string(' ', 40));
            ConsoleHelper.Write(this._BAR_START, _text);
        }
        private void ElapsedEvent(object _source, ElapsedEventArgs _e)
        {
            this.Report(_e.SignalTime);
        }

        public ElapsedTime(ProgressBar _parent = null)
        {
            this._START = DateTime.Now;
            this._PARENT = _parent;

            this.Init(0);

            this.Report(this._START);

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
                this._ANIMATIONINDEX = 0;
                this._START = DateTime.MinValue;
            }
            this._DISPOSED = true;

            this.Dispose(_managed);
        }
    }
}
#endif