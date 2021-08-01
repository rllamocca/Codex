using System;

namespace Codex
{
    public static class IntHelper
    {
        public static int Random(int _min, int _max)
        {
            Random _r = new Random();
            return _r.Next(_min, _max + 1);
        }
    }
}
