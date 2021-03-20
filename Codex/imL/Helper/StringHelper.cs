using Codex.Extension;

using System;

namespace Codex.Helper
{
    public static class StringHelper
    {
        public static string MyFortune()
        {
            Random _r = new Random();
            int _n = _r.Next(ReadOnly._SUITS.Length);
            string _fortune = Convert.ToString(ReadOnly._SUITS[_n]);
            if (_n.Between(0, 3))
            {
                _n = _r.Next(ReadOnly._SQUAD.Length);
                _fortune = string.Format("{1} {0}", _fortune, ReadOnly._SQUAD[_n]);
            }

            return _fortune;
        }
        public static string MyFortuneCard()
        {
            Random _r = new Random();
            int _n = _r.Next(ReadOnly._SUITS.Length);
            string _fortune = Convert.ToString(ReadOnly._SUITS[_n]);
            if (_n.Between(0, 3))
            {
                _n = _r.Next(ReadOnly._SQUAD.Length);
                _fortune = string.Format(ReadOnly._CARD, _fortune, ReadOnly._SQUAD[_n]);
            }
            else
                _fortune = string.Format(ReadOnly._JCARD, _fortune, _fortune);

            return _fortune;
        }
    }
}
