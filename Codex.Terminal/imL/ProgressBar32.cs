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
    public class ProgressBar32 : IDisposable
#if (NET35 || NET40)
        , Contract.IProgress<int>
#else
        , IProgress<int>
#endif
    {
        private bool _DISPOSED = false;

        private readonly ProgressBar32 _PARENT;
        private byte _BLOCKS = 50;
        private int _LENGTH;
        private int _VALUE = 0;
        private Point _BAR_START;
        private Point _BAR_END;
        private List<decimal> _BAR = new List<decimal>();
        private Point _LINE;
        private Point _NEW_LINE;

        public ProgressBar32(int _length, ProgressBar32 _parent = null)
        {
            this._LENGTH = _length;
            this._PARENT = _parent;

            if (this._LENGTH == 0)
                throw new ArgumentOutOfRangeException(nameof(_length), "_length == 0");

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

            if (this._LENGTH < this._BLOCKS)
                this._BLOCKS = Convert.ToByte(this._LENGTH);

            this._BAR_END = new Point(this._BAR_START.X + this._BLOCKS + 1, this._BAR_START.Y);

            ConsoleHelper.Write(this._BAR_START, new string(' ', 100));
            ConsoleHelper.Write(this._BAR_START, '[');
            ConsoleHelper.Write(this._BAR_END, ']');

            this._BAR_START.X += 1;
            this._BAR_END.X += 2;

            this._BAR.Add(0);
            this.Report(0);
        }

        public void Report(int _value = 0)
        {
            if (_value == 0)
                this._VALUE++;
            else
                this._VALUE = _value;

            decimal _per = (1.0m * this._VALUE / this._LENGTH);
            string _text = string.Format(
                            "{0}  {1}/{2}",
                            _per.ToString("P"),
                            _value,
                            this._LENGTH
                            );

            ConsoleHelper.Write(this._BAR_END, _text);

            if (0 < _per && _per <= 1)
            {
                decimal _f = Math.Round(_per * this._BLOCKS);

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

        ~ProgressBar32()
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
                this._BLOCKS = 0;
                this._LENGTH = 0;
                this._VALUE = 0;
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