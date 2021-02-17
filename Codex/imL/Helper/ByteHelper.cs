#if (NET35 || NET40 || NET45 || NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Codex.Helper
{
    public static class ByteHelper
    {
        public static byte[] To_Plain(DataTable _data, char _separator = '\0', bool _columnnames = true, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;

            string _sep = Convert.ToString(_separator);

            using (MemoryStream _ms = new MemoryStream())
            {
                using (StreamWriter _sw = new StreamWriter(_ms, _enc))
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
                            _line.Add((_item2 == DBNull.Value || _item2 == null) ? null : Convert.ToString(_item2));
#if (NET35)
                        _sw.WriteLine(string.Join(_sep, _line.ToArray()));
#else
                        _sw.WriteLine(string.Join(_sep, _line));
#endif
                    }
                    _line.Clear();
                }

                return _ms.ToArray();
            }
        }
        public static byte[][] To_Plain(DataSet _data, char _separator = '\0', bool _columnnames = true, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;

            List<byte[]> _return = new List<byte[]>();

            foreach (DataTable _item in _data.Tables)
                _return.Add(ByteHelper.To_Plain(_item, _separator, _columnnames, _enc));

            return _return.ToArray();
        }
    }
}
#endif