using Codex.Enumeration;

using System;
using System.Collections.Generic;

namespace Codex.Utility
{
    public static class GenericExtension
    {
        public static T[] RandomSort<T>(this T[] _array, ERandomSort _sort = ERandomSort.None)
        {
            if (_array == null)
                return null;

            T[] _return = _array;
            Random _r = new Random();

            switch (_sort)
            {
                case ERandomSort.Fisher_Yates:
                    for (int _k = _return.Length - 1; _k > 0; _k--)
                    {
                        int _az = _r.Next(_k);
                        T _tmp = _return[_az];
                        _return[_az] = _return[_k];
                        _return[_k] = _tmp;
                    }
                    break;
                default:
                    break;
            }

            return _return;
        }
        public static List<T> RandomSort<T>(this List<T> _array, ERandomSort _sort = ERandomSort.None)
        {
            if (_array == null)
                return null;

            List<T> _return = _array;
            Random _r = new Random();

            switch (_sort)
            {
                case ERandomSort.Fisher_Yates:
                    for (int _k = _return.Count - 1; _k > 0; _k--)
                    {
                        int _az = _r.Next(_k);
                        T _tmp = _return[_az];
                        _return[_az] = _return[_k];
                        _return[_k] = _tmp;
                    }
                    break;
                default:
                    break;
            }

            return _return;
        }
    }
}
