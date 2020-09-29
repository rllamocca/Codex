#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Codex.Helper
{
    public static class ByteHelper
    {
        public static Byte[] To_Plain(DataTable _a, Encoding _b, Char _c = '|', Boolean _d = true)
        {
            using (MemoryStream _ms = new MemoryStream())
            {
                using (StreamWriter _sw = new StreamWriter(_ms, _b))
                {
                    List<String> _tmp = new List<String>();

                    if (_d)
                    {
                        foreach (DataColumn _dc in _a.Columns)
                        {
                            if (_dc.Caption == null)
                                _tmp.Add(_dc.ColumnName);
                            else
                                _tmp.Add(_dc.Caption);
                        }

                        _sw.WriteLine(String.Join(Convert.ToString(_c), _tmp.ToArray()));
                    }

                    foreach (DataRow _dr in _a.Rows)
                    {
                        _tmp = new List<String>();
                        foreach (DataColumn _dc in _a.Columns)
                        {
                            Object _tmp2 = _dr[_dc.ColumnName];
                            _tmp.Add((_tmp2 == DBNull.Value) ? null : Convert.ToString(_tmp2));
                        }
                        _sw.WriteLine(String.Join(Convert.ToString(_c), _tmp.ToArray()));
                    }
                    _sw.Flush();
                    _ms.Seek(0, SeekOrigin.Begin);
                }
                return _ms.ToArray();
            }
        }
        public static Byte[][] To_Plain(DataSet _a, Encoding _b, Char _c = '|', Boolean _d = true)
        {
            List<Byte[]> _return = new List<Byte[]>();
            foreach (DataTable _item in _a.Tables)
                _return.Add(ByteHelper.To_Plain(_item, _b, _c, _d));
            return _return.ToArray();
        }
    }
}

#endif