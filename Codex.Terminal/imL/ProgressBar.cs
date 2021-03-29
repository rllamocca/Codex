#if (NETSTANDARD1_3)
using Codex.Struct;
#else
using System.Drawing;
#endif

using Codex.Terminal.Helper;

using System;
using System.Collections.Generic;

namespace Codex.Terminal
{
    public abstract class ProgressBar : IDisposable
    {
        private bool _DISPOSED = false;

        protected ProgressBar _PARENT;
        protected byte _BLOCKS = 50;
        protected Point _BAR_START;
        protected Point _BAR_END;
        protected List<decimal> _BAR = new List<decimal>();
        protected Point _LINE;
        protected Point _NEW_LINE;

        public void Init(byte _length)
        {
            this._BAR.Add(0);

            if (_length < this._BLOCKS)
                this._BLOCKS = _length;

            if (this._PARENT == null)
            {
                Console.CursorVisible = false;
                this._LINE = new Point(Console.CursorLeft, Console.CursorTop);
                this._NEW_LINE = new Point(this._LINE.X, this._LINE.Y + 1);
            }
            else
            {
                this._LINE = new Point(this._PARENT._LINE.X, this._PARENT._LINE.Y + 1);
                this._PARENT._NEW_LINE = new Point(this._LINE.X, this._LINE.Y + 1);
            }

            this._BAR_START = new Point(this._LINE.X, this._LINE.Y);
            this._BAR_END = new Point(this._BAR_START.X + this._BLOCKS + 1, this._BAR_START.Y);

            ConsoleHelper.Write(this._BAR_START, new string(' ', 100));
            ConsoleHelper.Write(this._BAR_START, '[');
            ConsoleHelper.Write(this._BAR_END, ']');

            this._BAR_START.X += 1;
            this._BAR_END.X += 2;
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
                this._BLOCKS = 0;
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
            this._DISPOSED = true;
        }
    }
}