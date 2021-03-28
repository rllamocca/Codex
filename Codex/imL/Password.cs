#if (NET35 || NET40 || NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)

using Codex.Enumeration;
using Codex.Extension;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Codex
{
    public class Password : IDisposable
    {
        //؟
        private bool _DISPOSED = false;

        private const ERandomSort _SORT = ERandomSort.Fisher_Yates;
        private char[] _BASE;
        private char[] _GENERATED;

        public bool Numbers { set; get; } = true;
        public bool UpperCase { set; get; } = true;
        public bool LowerCase { set; get; } = true;
        public bool Specials { set; get; } = false;
        public char[] Aggregate { set; get; }

        public string Base { get { return new string(this._BASE); } }
        public string Generated { get { return new string(this._GENERATED); } }

        public Password(
            bool _numbers = true,
            bool _uppercase = true,
            bool _lowercase = true,
            bool _specials = false,
            char[] _aggregate = null
            )
        {
            this.Numbers = _numbers;
            this.UpperCase = _uppercase;
            this.LowerCase = _lowercase;
            this.Specials = _specials;
            this.Aggregate = _aggregate;
        }
        public void Generate(byte _length = 8)
        {
            this._BASE = Password.Prepare(this.Numbers, this.UpperCase, this.LowerCase, this.Specials, this.Aggregate, _length);
            this._GENERATED = Password.Generate(this._BASE, _length);
        }

        public static char[] Prepare(
            bool _numbers = true,
            bool _uppercase = true,
            bool _lowercase = true,
            bool _specials = false,
            char[] _aggregate = null,
            byte _length = 8
            )
        {
            List<char> _tmp = new List<char>();
            if (_numbers) _tmp.AddRange(ReadOnly._NUMBERS.RandomSort(Password._SORT).Take(_length));
            if (_uppercase) _tmp.AddRange(ReadOnly._UPPERCASE.RandomSort(Password._SORT).Take(_length));
            if (_lowercase) _tmp.AddRange(ReadOnly._LOWERCASE.RandomSort(Password._SORT).Take(_length));
            if (_specials) _tmp.AddRange(ReadOnly._SPECIALS.RandomSort(Password._SORT).Take(_length));
            if (_aggregate != null) _tmp.AddRange(_aggregate.RandomSort(Password._SORT));

            List<char> _tmp2 = _tmp.Distinct().ToList();
            _tmp2 = _tmp2.RemoveEndLine(EEndLine.All);

            _tmp2 = _tmp2.RandomSort(Password._SORT);

            return _tmp2.ToArray();
        }
        public static char[] Generate(char[] _base, byte _length = 8)
        {
            char[] _return = new char[_length];
            int _max = _base.Length;
            Random _r = new Random();
            for (byte _i = 0; _i < _return.Length; _i++)
                _return[_i] = _base[_r.Next(_max)];

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
            }

            this._DISPOSED = true;
        }
    }
}
#endif