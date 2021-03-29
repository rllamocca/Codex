#if (NET35 || NET40)
using Codex.Contract;
#endif

using Codex.Extension;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Codex.Data.Extension
{
    public static class DataTableExtension
    {
        public static void To_Plain(this DataTable _this, string _path, char _separator = '\0', bool _columnnames = true, Encoding _enc = null, IProgress<int> _progress = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;
            bool _report = (_progress != null);

            string _sep = Convert.ToString(_separator);

            using (StreamWriter _sw = new StreamWriter(_path, false, _enc))
            {
                List<string> _line = new List<string>();

                if (_columnnames)
                {
                    foreach (DataColumn _item in _this.Columns)
                        _line.Add(_item.Caption ?? _item.ColumnName);
#if (NET35)
                    _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(_sep, _line));
#endif

                }

                foreach (DataRow _item in _this.Rows)
                {
                    _line.Clear();
                    foreach (object _item2 in _item.ItemArray)
                        _line.Add(_item2.DBToString());

#if (NET35)
                    _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(_sep, _line));
#endif

                    if (_report)
                        _progress.Report(0);
                }
                _line.Clear();
            }
        }

        public static void To_Plain(this DataTable _this, out Stream _out, char _separator = '\0', bool _columnnames = true, Encoding _enc = null, IProgress<int> _progress = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;
            bool _report = (_progress != null);

            string _sep = Convert.ToString(_separator);

            _out = new MemoryStream();
            using (StreamWriter _sw = new StreamWriter(_out, _enc))
            {
                List<string> _line = new List<string>();

                if (_columnnames)
                {
                    foreach (DataColumn _item in _this.Columns)
                        _line.Add(_item.Caption ?? _item.ColumnName);
#if (NET35)
                    _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(_sep, _line));
#endif
                }

                foreach (DataRow _item in _this.Rows)
                {
                    _line.Clear();
                    foreach (object _item2 in _item.ItemArray)
                        _line.Add(_item2.DBToString());
#if (NET35)
                    _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                    _sw.WriteLine(string.Join(_sep, _line));
#endif
                    if (_report)
                        _progress.Report(0);
                }
                _line.Clear();
            }
        }
    }
}
