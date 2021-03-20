﻿using Codex.Extension;

using System;

namespace Codex.Helper
{
    public static class StringHelper
    {
        public static string MyFortune()
        {
            Random _r = new Random();
            int _n = _r.Next(Bases._SUITS.Length);
            string _fortune = Bases._SUITS[_n];
            if (_n.Between(0, 3))
            {
                _n = _r.Next(Bases._SQUAD.Length);
                _fortune = string.Format("{1} {0}", _fortune, Bases._SQUAD[_n]);
            }

            return _fortune;
        }
        public static string MyFortuneCard()
        {
            Random _r = new Random();
            int _n = _r.Next(Bases._SUITS.Length);
            string _fortune = Bases._SUITS[_n];
            if (_n.Between(0, 3))
            {
                _n = _r.Next(Bases._SQUAD.Length);
                _fortune = string.Format(Bases._CARD, _fortune, Bases._SQUAD[_n]);
            }
            else
                _fortune = string.Format(Bases._JCARD, _fortune, _fortune);

            return _fortune;
        }
    }
}
