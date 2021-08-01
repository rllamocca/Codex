using Codex.Enumeration;

using System;
using System.Collections.Generic;

namespace Codex.Utility
{
    public static class CharExtension
    {
        public static List<char> RemoveEndLine(this List<char> _array, EEndLine _re)
        {
            if (_array == null)
                return null;

            if (_array.Count > 0)
            {
                char _ht = (char)9;
                char _lf = (char)10;
                char _vt = (char)11;
                char _ff = (char)12;
                char _cr = (char)13;

                if (_re.HasFlag(EEndLine.HT)) _array.Remove(_ht);
                if (_re.HasFlag(EEndLine.LF)) _array.Remove(_lf);
                if (_re.HasFlag(EEndLine.VT)) _array.Remove(_vt);
                if (_re.HasFlag(EEndLine.FF)) _array.Remove(_ff);
                if (_re.HasFlag(EEndLine.CR)) _array.Remove(_cr);
            }

            return _array;
        }

        public static string[] ConvertToString(this char[] _array)
        {
            if (_array == null)
                return null;

            string[] _return = new string[_array.Length];

            for (int _i = 0; _i < _return.Length; _i++)
                _return[_i] = Convert.ToString(_array[_i]);

            return _return;
        }
    }
}
