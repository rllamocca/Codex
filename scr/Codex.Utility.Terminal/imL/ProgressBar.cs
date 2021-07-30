#if (NETSTANDARD1_3)
using Codex.Struct;
#else
using System.Drawing;
#endif

using Codex.Utility.Terminal.Helper;
using Codex.Enumeration;

using System;
using System.Collections.Generic;

namespace Codex.Utility.Terminal
{
    public abstract class ProgressBar : IDisposable
    {
        private bool _DISPOSED = false;

        protected ProgressBar _PARENT;
        protected Point _DRAW_END;
        protected DateTime _START;

        private const char _CHAR = '■';
        private const string _BUCLE = @"+\|/";
        private byte _BLOCKS = 50;
        private Point _LINE;
        private Point _NEW_LINE;
        private Point _DRAW_START;
        private List<decimal> _BAR = new List<decimal>();

        protected void Init(byte _blocks = 50)
        {
            if (this._PARENT == null)
            {
                this._LINE = new Point(Console.CursorLeft, Console.CursorTop);
                Console.CursorVisible = false;
            }
            else
                this._LINE = new Point(this._PARENT._LINE.X + 2, this._PARENT._LINE.Y + 1);

            this._DRAW_START = new Point(this._LINE.X, this._LINE.Y);

            if (_blocks < this._BLOCKS)
                this._BLOCKS = _blocks;

            if (this._BLOCKS > 0)
            {
                this._DRAW_END = new Point(this._DRAW_START.X + this._BLOCKS + 4, this._DRAW_START.Y);

                ConsoleHelper.Write(this._DRAW_START, string.Format("[{0}]{1}", new string(' ', this._BLOCKS), new string(' ', 75)));
                this._DRAW_START.X += 1;

                this._BAR.Add(0);
            }

            this._NEW_LINE = new Point(this._LINE.X, this._LINE.Y + 1);
        }
        protected void DrawBar(decimal _per)
        {
            if (_per.Between(0, 1, EInterval.Until))
            {
                decimal _rou = Math.Round(_per * this._BLOCKS);

                if (this._BAR.Contains(_rou) == false)
                {
                    this._BAR.Add(_rou);

                    ConsoleHelper.Write(this._DRAW_START, ProgressBar._CHAR);
                    this._DRAW_START.X += 1;
                }
            }
        }
        protected void DrawTime(DateTime _value)
        {
            TimeSpan _diff = _value - this._START;

            string _text = string.Format(
                "[{0}]  {1}",
                ProgressBar._BUCLE[this._BLOCKS++ % 4],
#if (NET35)
                _diff.ToString()
#else
                _diff.ToString("hh':'mm':'ss")
#endif
                );

            ConsoleHelper.Write(this._DRAW_START, _text);
        }

        ~ProgressBar() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                if (this._PARENT == null)
                {
                    Console.CursorVisible = true;
                    ConsoleHelper.WriteLine(this._NEW_LINE, ' ');
                }
                else
                    this._PARENT._NEW_LINE = new Point(this._NEW_LINE.X, this._NEW_LINE.Y);

                this._DRAW_END = new Point(0, 0);
                this._START = DateTime.MinValue;

                this._BLOCKS = 0;
                this._LINE = new Point(0, 0);
                this._NEW_LINE = new Point(0, 0);
                this._DRAW_START = new Point(0, 0);
                this._BAR.Clear();
                this._BAR = null;
            }
            this._DISPOSED = true;
        }
    }
}