using System;
using System.Collections.Generic;

namespace Codex.Extension
{
    public static class GenericExtension
    {
        public static T[] Fisher_Yates<T>(this T[] _array)
        {
            if (_array == null)
                return null;

            T[] _return = _array;
            Random _r = new Random();
            for (int _k = _return.Length - 1; _k > 0; _k--)
            {
                int _az = _r.Next(_k);
                T _tmp = _return[_az];
                _return[_az] = _return[_k];
                _return[_k] = _tmp;
            }

            return _return;
        }

        public static List<T> Fisher_Yates<T>(this List<T> _array)
        {
            if (_array == null)
                return null;

            List<T> _return = _array;
            Random _r = new Random();
            for (int _k = _return.Count - 1; _k > 0; _k--)
            {
                int _az = _r.Next(_k);
                T _tmp = _return[_az];
                _return[_az] = _return[_k];
                _return[_k] = _tmp;
            }

            return _return;
        }
    }
}
