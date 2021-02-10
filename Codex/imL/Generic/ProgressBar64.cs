#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Helper;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace Codex.Generic
{
    public class ProgressBar64 : IDisposable, IProgress<long>
    {
        private readonly Timer _TIMER;
        private readonly ProgressBar64 _PARENT;
        private const string _ANIMATION = @"+\|/";
        private byte _ANIMATIONINDEX = 0;
        private byte _BLOCKS = 50;
        private long _COUNT;
        private Point _BAR_START;
        private Point _BAR_END;
        private List<decimal> _BAR = new List<decimal>();
        private Point _LINE;
        private Point _NEW_LINE;
        private DateTime _START;

        private void DrawProgress(decimal _per, long _pro)
        {
            DateTime _e = DateTime.Now;
            TimeSpan _diff = (_e - this._START);
            _diff = TimeSpan.FromTicks(_diff.Ticks * (this._COUNT - _pro));
            this._START = _e;

            ConsoleHelper.Write(
                this._BAR_END,
                string.Format("{0} : {1} / {2} <-> {3}",
                _per.ToString("P"),
                _pro,
                this._COUNT,
                _diff.ToString("hh':'mm':'ss'.'fff"))
                );
        }

        private void OnElapsedEvent(object _source, ElapsedEventArgs _e)
        {
            TimeSpan _diff = _e.SignalTime - this._START;

            string _text = string.Format(
                "[{0}] {1} <-> {2}",
                _ANIMATION[this._ANIMATIONINDEX++ % 4],
                _e.SignalTime.ToString("HH:mm:ss.fff"),
                _diff.ToString("hh':'mm':'ss'.'fff")
                );
            ConsoleHelper.Write(this._BAR_START, new String(' ', 40));
            ConsoleHelper.Write(this._BAR_START, _text);
        }

        public ProgressBar64(long _count, ProgressBar64 _parent = null)
        {
            this._COUNT = _count;
            this._PARENT = _parent;
            this._START = DateTime.Now;
            //#####
            if (Console.IsOutputRedirected == false)
            {
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

                if (this._COUNT <= 0)
                {
                    this._TIMER = new Timer(200);
                    this._TIMER.Elapsed += this.OnElapsedEvent;
                    this._TIMER.Start();
                }
                else
                {
                    if (this._COUNT < this._BLOCKS)
                        this._BLOCKS = Convert.ToByte(this._COUNT);

                    this._BAR_END = new Point(this._BAR_START.X + this._BLOCKS + 1, this._BAR_START.Y);

                    ConsoleHelper.Write(this._BAR_START, new string(' ', 100));
                    ConsoleHelper.Write(this._BAR_START, '[');
                    ConsoleHelper.Write(this._BAR_END, ']');

                    this._BAR_START.X += 1;
                    this._BAR_END.X += 2;

                    this.DrawProgress(0, 0);
                }
            }
        }

        public void Report(long _progress)
        {
            if (Console.IsOutputRedirected == false)
            {
                if (this._COUNT > 0)
                {
                    decimal _p = (1.0m * _progress / this._COUNT);
                    this.DrawProgress(_p, _progress);

                    if (0 <= _p && _p <= 1)
                    {
                        decimal _f = Math.Round(_p * this._BLOCKS);

                        if (this._BAR.Contains(_f) == false)
                        {
                            this._BAR.Add(_f);

                            if (_f > 0)
                            {
                                ConsoleHelper.Write(this._BAR_START, '■');
                                this._BAR_START.X += 1;
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            if (this._TIMER != null)
            {
                this._TIMER.Stop();
                this._TIMER.Dispose();
            }
            this._ANIMATIONINDEX = 0;
            this._BLOCKS = 0;
            this._COUNT = 0;
            this._BAR_START = new Point(0, 0);
            this._BAR_END = new Point(0, 0);
            this._BAR.Clear();
            this._BAR = null;
            if (this._PARENT == null)
            {
                ConsoleHelper.WriteLine(this._NEW_LINE, "");
                Console.CursorVisible = true;
            }
            this._LINE = new Point(0, 0);
            this._NEW_LINE = new Point(0, 0);
            this._START = new DateTime();
        }
    }
}

#endif