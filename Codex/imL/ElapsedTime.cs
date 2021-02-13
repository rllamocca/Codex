#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Helper;

using System;
using System.Drawing;
using System.Timers;

namespace Codex
{
    public class ElapsedTime : IDisposable
    {
        private bool _DISPOSED = false;

        private readonly Timer _TIMER;
        private readonly ElapsedTime _PARENT;
        private const string _ANIMATION = @"+\|/";
        private byte _ANIMATIONINDEX = 0;
        private Point _BAR_START;
        private Point _LINE;
        private Point _NEW_LINE;
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
            ConsoleHelper.Write(this._BAR_START, new String(' ', 40));
            ConsoleHelper.Write(this._BAR_START, _text);

        }

        public ElapsedTime(ElapsedTime _parent = null)
        {
            this._PARENT = _parent;
            if (this._PARENT == null)
            {
                this._LINE = new Point(Console.CursorLeft, Console.CursorTop);
                this._NEW_LINE = new Point(this._LINE.X, this._LINE.Y + 1);
            }
            else
            {
                this._LINE = new Point(this._PARENT._LINE.X, this._PARENT._LINE.Y + 1);
                this._PARENT._NEW_LINE = new Point(this._LINE.X, this._LINE.Y + 1);
            }

            this._BAR_START = new Point(this._LINE.X, this._LINE.Y);

            if (this._PARENT == null)
                Console.CursorVisible = false;

            this._START = DateTime.Now;
            this._TIMER = new Timer(200);
            this._TIMER.Elapsed += this.OnElapsedEvent;
            this._TIMER.Start();
        }

        ~ElapsedTime()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
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
                this._BAR_START = new Point(0, 0);
                if (this._PARENT == null)
                {
                    ConsoleHelper.WriteLine(this._NEW_LINE, "");
                    Console.CursorVisible = true;
                }
                this._LINE = new Point(0, 0);
                this._NEW_LINE = new Point(0, 0);
                this._START = DateTime.MinValue;
            }
            this._DISPOSED = true;
        }
    }
}
#endif