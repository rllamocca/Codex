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
        public static byte[] To_Plain(DataTable _data, char _sep = '|', bool _columnnames = true, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;

            using (MemoryStream _ms = new MemoryStream())
            {
                using (StreamWriter _sw = new StreamWriter(_ms, _enc))
                {
                    List<string> _tmp = new List<string>();

                    if (_columnnames)
                    {
                        foreach (DataColumn _dc in _data.Columns)
                        {
                            if (_dc.Caption == null)
                                _tmp.Add(_dc.ColumnName);
                            else
                                _tmp.Add(_dc.Caption);
                        }
#if (NET35)
                        _sw.WriteLine(string.Join(Convert.ToString(_sep), _tmp.ToArray()));
#else
                        _sw.WriteLine(string.Join(Convert.ToString(_sep), _tmp));
#endif
                    }

                    foreach (DataRow _dr in _data.Rows)
                    {
                        _tmp = new List<string>();
                        foreach (DataColumn _dc in _data.Columns)
                        {
                            object _tmp2 = _dr[_dc.ColumnName];
                            _tmp.Add((_tmp2 == DBNull.Value) ? null : Convert.ToString(_tmp2));
                        }
#if (NET35)
                        _sw.WriteLine(string.Join(Convert.ToString(_sep), _tmp.ToArray()));
#else
                        _sw.WriteLine(string.Join(Convert.ToString(_sep), _tmp));
#endif
                    }

                    _sw.Flush();
                }

                _ms.Seek(0, SeekOrigin.Begin);
                return _ms.ToArray();
            }
        }
        public static byte[][] To_Plain(DataSet _data, char _sep = '|', bool _columnnames = true, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;

            List<byte[]> _return = new List<byte[]>();

            foreach (DataTable _item in _data.Tables)
                _return.Add(ByteHelper.To_Plain(_item, _sep, _columnnames, _enc));

            return _return.ToArray();
        }
    }
}
#endif