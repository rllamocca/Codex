#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Helper;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace Codex.Generic
{
    public class ProgressBar64 : IDisposable, IProgress<Int64>
    {
        private readonly Timer _TIMER;
        private readonly ProgressBar64 _PARENT;
        private const String _ANIMATION = @"+\|/";
        private Byte _ANIMATIONINDEX = 0;
        private Byte _BLOCKS = 50;
        private Int64 _COUNT;
        private Point _BAR_START;
        private Point _BAR_END;
        private List<Decimal> _BAR = new List<Decimal>();
        private Point _LINE;
        private Point _NEW_LINE;

        private void PercentProgress(Decimal _p, Int64 _progress)
        {
            ConsoleHelper.Write(this._BAR_END, String.Format("{0} : {1} / {2}"
                            , _p.ToString("P")
                            , _progress
                            , this._COUNT));
        }

        private void OnElapsedEvent(Object _source, ElapsedEventArgs _e)
        {
            String _text = String.Format("[{0}] {1}",
                    _ANIMATION[this._ANIMATIONINDEX++ % 4],
                    _e.SignalTime.ToString("HH:mm:ss.fff")
                    );
            ConsoleHelper.Write(this._BAR_START, new String(' ', 40));
            ConsoleHelper.Write(this._BAR_START, _text);
        }

        public ProgressBar64(Int64 _count, ProgressBar64 _parent = null)
        {
            this._COUNT = _count;
            this._PARENT = _parent;
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

                    ConsoleHelper.Write(this._BAR_START, new String(' ', 100));
                    ConsoleHelper.Write(this._BAR_START, '[');
                    ConsoleHelper.Write(this._BAR_END, ']');

                    this._BAR_START.X += 1;
                    this._BAR_END.X += 2;

                    this.PercentProgress(0, 0);
                }
            }
        }

        public void Report(Int64 _progress)
        {
            if (Console.IsOutputRedirected == false)
            {
                if (this._COUNT > 0)
                {
                    Decimal _p = (1.0m * _progress / this._COUNT);
                    this.PercentProgress(_p, _progress);

                    if (0 <= _p && _p <= 1)
                    {
                        Decimal _f = Math.Round(_p * this._BLOCKS);

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
        }
    }
}

#endif