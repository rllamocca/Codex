#if (NET35 || NET40 || NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)

using Codex.Enum;
using Codex.Extension;

using System;
using System.Linq;

namespace Codex
{
    public class Password : IDisposable
    {
        //؟
        private bool _DISPOSED = false;

        private char[] _BASE;
        private char[] _GENERATED;

        public bool Numbers { set; get; } = true;
        public bool UpperCase { set; get; } = true;
        public bool LowerCase { set; get; } = true;
        public bool Specials { set; get; } = false;
        public string Aggregate { set; get; }

        public ERandomSort Sort { set; get; } = ERandomSort.None;

        public string Base { get { return string.Join("", this._BASE.ConvertToString()); } }
        public string Generated { get { return string.Join("", this._GENERATED.ConvertToString()); } }

        public Password(
            bool _numbers = true,
            bool _uppercase = true,
            bool _lowercase = true,
            bool _specials = false,
            string _aggregate = null,
            ERandomSort _sort = ERandomSort.None
            )
        {
            this.Numbers = _numbers;
            this.UpperCase = _uppercase;
            this.LowerCase = _lowercase;
            this.Specials = _specials;
            this.Aggregate = _aggregate;
            this.Sort = _sort;

            this._BASE = Password.Prepare(this.Numbers, this.UpperCase, this.LowerCase, this.Specials, this.Aggregate);
            switch (this.Sort)
            {
                case ERandomSort.Fisher_Yates:
                    this._BASE = this._BASE.Fisher_Yates();
                    break;
                default:
                    break;
            }
        }
        public char[] Generate(byte _largo = 8)
        {
            this._GENERATED = Password.Generate(this._BASE, _largo);
            return this._GENERATED;
        }

        public static char[] Prepare(bool _nu, bool _uc, bool _lc, bool _sp, string _ag = null)
        {
            string _tmp = string.Empty;
            if (_nu) _tmp += ReadOnly._NUMBERS;
            if (_uc) _tmp += ReadOnly._UPPERCASE;
            if (_lc) _tmp += ReadOnly._LOWERCASE;
            if (_sp) _tmp += ReadOnly._SPECIALS;
            if (_ag != null) _tmp += _ag;

            _tmp = _tmp.CleanTBLFCR();

            return _tmp.ToArray().Distinct().ToArray();
        }
        public static char[] Generate(char[] _base, byte _largo = 8)
        {
            char[] _return = new char[_largo];
            Random _r = new Random();
            for (byte _i = 0; _i < _return.Length; _i++)
                _return[_i] = _base[_r.Next(_base.Length)];

            return _return;
        }

        ~Password()
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
                this._BASE = null;
                this._GENERATED = null;

                this.LowerCase = false;
                this.UpperCase = false;
                this.Numbers = false;
                this.Specials = false;
                this.Aggregate = null;
                this.Sort = ERandomSort.None;
            }

            this._DISPOSED = true;
        }
    }
}
#endif