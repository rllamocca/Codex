#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Codex.Helper
{
    public static class StringHelper
    {
        //public static String Clean_Sql(String _a)
        //{
        //    if (_a == null) return null;

        //    while (_a.Contains("[["))
        //    {
        //        _a = _a.Replace("[[", "[");
        //    }
        //    while (_a.Contains("]]"))
        //    {
        //        _a = _a.Replace("]]", "]");
        //    }
        //    _a = StringHelper.Clean_TBLFCR(_a);
        //    return _a.Trim();
        //}

        public static string To_Xml(object _data, Encoding _enc = null)
        {
            if (_data == null) 
                return null;
            if (_enc == null)
            {
                _enc = Encoding.UTF8;
            }

            XmlWriterSettings _s = new XmlWriterSettings();
            _s.Encoding = _enc;

            _s.Async = true;

            using (StringWriter _sw = new StringWriter())
            {
                using (XmlWriter _xw = XmlWriter.Create(_sw, _s))
                {
                    if (_data is DataSet || _data is DataTable)
                    {
                        if (_data is DataSet _ds)
                            _ds.WriteXml(_xw);
                        if (_data is DataTable _dt)
                            _dt.WriteXml(_xw);
                    }
                    else
                    {
                        XmlSerializer _xs = new XmlSerializer(_data.GetType());
                        _xs.Serialize(_xw, _data);
                    }
                    return _sw.ToString();
                }
            }
        }

        //        //ESTO NO DEBERIA ESTAR AQUI
        //        private static String Formato_Rut(String _a)
        //        {
        //            if (_a == null) return null;
        //            _a = _a.Replace(".", "").Replace("-", "");
        //            if (_a.Length == 0) return null;

        //            List<Char> _l = new List<Char>();
        //            List<Char> _lr = _a.Reverse().ToList();
        //            for (Int32 _n = 0; _n < _lr.Count; ++_n)
        //            {
        //                _l.Add(_lr[_n]);
        //                if (_n == 0)
        //                    _l.Add('-');
        //                else if (_n % 3 == 0 && _n != _lr.Count - 1)
        //                    _l.Add('.');
        //            }
        //            _l.Reverse();
        //            return String.Join("", _l);
        //        }
    }
}

#endif