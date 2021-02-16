#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Codex.Helper
{
    public static class VoidHelper
    {
        public static void To_plain(DataTable _data, string _path, char _separator = '\0', bool _columnnames = true)
        {
            using (StreamWriter _sw = new StreamWriter(_path, false))
            {
                List<string> _line = new List<string>();

                if (_columnnames)
                {
                    foreach (DataColumn _item in _data.Columns)
                    {
                        if (_item.Caption == null) 
                            _line.Add(_item.ColumnName);
                        else 
                            _line.Add(_item.Caption);
                    }
#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)
#if (NET35)
                    _sw.WriteLine(string.Join(Convert.ToString(_separator), _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(Convert.ToString(_separator), _line));
#endif
#else
                    _sw.WriteLine(string.Join(_separator, _line));
#endif

                }

                foreach (DataRow _item in _data.Rows)
                {
                    _line.Clear();
                    foreach (object _item2 in _item.ItemArray)
                        _line.Add(Convert.ToString(_item2));

#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)
#if (NET35)
                    _sw.WriteLine(string.Join(Convert.ToString(_separator), _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(Convert.ToString(_separator), _line));
#endif
#else
                    _sw.WriteLine(string.Join(_separator, _line));
#endif
                }
                _line.Clear();
            }
        }
    }
}
#endif