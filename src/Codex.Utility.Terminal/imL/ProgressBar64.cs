using System;

namespace Codex.Utility.Terminal
{
    public class ProgressBar64 : ProgressBar
#if (NET35 || NET40)
        , Contract.IProgress<long>
#else
        , IProgress<long>
#endif
    {
        private bool _DISPOSED = false;

        private long _LENGTH;
        private long _VALUE = 0;

        public long Length { get { return this._LENGTH; } }
        public long Value { get { return this._VALUE; } }

        public ProgressBar64(long _length = 50, ProgressBar _parent = null)
        {
            this._LENGTH = _length;
            this._PARENT = _parent;

            if (this._LENGTH == 0)
                throw new ArgumentOutOfRangeException(nameof(_length), "_length == 0");

            if (this._LENGTH < 50)
                this.Init(Convert.ToByte(this._LENGTH));
            else
                this.Init();
        }

        public void Report(long _value = 0)
        {
            if (_value == 0)
                this._VALUE++;
            else
                this._VALUE = _value;

            decimal _per = 1.0m;

            if (this._VALUE.Between(0, this._LENGTH))
            {
                _per = (1.0m * this._VALUE / this._LENGTH);

                this.DrawBar(_per);
            }

            string _text = string.Format("{0}  {1}/{2}",
                _per.ToString("P"),
                this._VALUE,
                this._LENGTH);

            ConsoleHelper.Write(this._DRAW_END, _text);
        }

        protected override void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                this._LENGTH = 0;
                this._VALUE = 0;
            }
            this._DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}