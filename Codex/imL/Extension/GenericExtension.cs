using System;

namespace Codex.Extension
{
    public static class GenericExtension
    {
        public static T[] Fisher_Yates<T>(this T[] _array)
        {
            if (_array == null)
                return null;

            T[] _return = _array;
            Random _rdm = new Random();
            for (int _k = _return.Length - 1; _k > 0; _k--)
            {
                int _az = _rdm.Next(_k);
                T _tmp = _return[_az];
                _return[_az] = _return[_k];
                _return[_k] = _tmp;
            }

            return _return;
        }
    }
}
