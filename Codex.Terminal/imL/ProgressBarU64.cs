using Codex.Enumeration;
using Codex.Extension;
using Codex.Terminal.Helper;

using System;

namespace Codex.Terminal
{
    public class ProgressBarU64 : ProgressBar
#if (NET35 || NET40)
        , Contract.IProgress<ulong>
#else
        , IProgress<ulong>
#endif
    {
        private bool _DISPOSED = false;

        private ulong _LENGTH;
        private ulong _VALUE = 0;

        public ProgressBarU64(ulong _length, ProgressBar _parent = null)
        {
            this._LENGTH = _length;
            base._PARENT = _parent;

            if (this._LENGTH == 0)
                throw new ArgumentOutOfRangeException(nameof(_length), "_length == 0");

            base.Init(Convert.ToByte(this._LENGTH));
        }

        public void Report(ulong _value = 0)
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

            if (_per.Between(0, 1, EInterval.Until))
            {
                decimal _round = Math.Round(_per * this._BLOCKS);

                if (this._BAR.Contains(_round) == false)
                {
                    this._BAR.Add(_round);

                    ConsoleHelper.Write(this._BAR_START, '■');
                    this._BAR_START.X += 1;
                }
            }
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