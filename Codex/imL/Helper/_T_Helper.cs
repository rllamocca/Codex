using System;

namespace Codex.Helper
{
    public static class _T_Helper
    {
        public static void Fisher_Yates<T>(T[] _array)
        {
            Random _rdm = new Random();
            Int32 _c = _array.Length;
            for (Int32 _k = _c - 1; _k > 0; _k--)
            {
                Int32 _az = _rdm.Next(0, _k);
                T _tmp = _array[_az];
                _array[_az] = _array[_k];
                _array[_k] = _tmp;
            }
        }
    }
}
