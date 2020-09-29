#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Codex.Helper
{
    public static class VoidHelper
    {
        public static void Write(ref Stream _a, String _b)
        {
            using (FileStream _sw = File.Create(_b))
            {
                _a.Seek(0, SeekOrigin.Begin);
                _a.CopyTo(_sw);
            }
        }

        public static void To_plain(DataTable _data, String _path, Char _separator = '\0', Boolean _fistrow = true)
        {
            using (StreamWriter _sw = new StreamWriter(_path, false))
            {
                List<String> _line = new List<String>();

                if (_fistrow == true)
                {
                    foreach (DataColumn _dc in _data.Columns)
                    {
                        if (_dc.Caption == null) _line.Add(_dc.ColumnName);
                        else _line.Add(_dc.Caption);
                    }
#if (NETSTANDARD2_1)
                    _sw.WriteLine(String.Join(_separator, _line));
#else
                    _sw.WriteLine(String.Join(Convert.ToString(_separator), _line));
#endif

                }

                foreach (DataRow _dr in _data.Rows)
                {
                    _line.Clear();
                    foreach (Object _o in _dr.ItemArray)
                        _line.Add(Convert.ToString(_o));

#if (NETSTANDARD2_1)
                    _sw.WriteLine(String.Join(_separator, _line));
#else
                    _sw.WriteLine(String.Join(Convert.ToString(_separator), _line));
#endif
                }
                _line.Clear();
            }
        }
    }
}

#endif