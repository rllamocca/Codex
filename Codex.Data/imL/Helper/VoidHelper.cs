using Codex.Extension;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Codex.Data.Helper
{
    public static class VoidHelper
    {
        public static void To_Plain(DataTable _data, string _path, char _separator = '\0', bool _columnnames = true, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;

            string _sep = Convert.ToString(_separator);

            using (StreamWriter _sw = new StreamWriter(_path, false, _enc))
            {
                List<string> _line = new List<string>();

                if (_columnnames)
                {
                    foreach (DataColumn _item in _data.Columns)
                        _line.Add(_item.Caption ?? _item.ColumnName);
#if (NET35)
                    _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(_sep, _line));
#endif

                }

                foreach (DataRow _item in _data.Rows)
                {
                    _line.Clear();
                    foreach (object _item2 in _item.ItemArray)
                        _line.Add(_item2.DBToString());

#if (NET35)
                    _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(_sep, _line));
#endif
                }
                _line.Clear();
            }
        }
    }
}