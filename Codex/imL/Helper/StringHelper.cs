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

        //        //ESTO NO DEBERIA ESTAR AQUI
        //        private static Encoding Codificacion(String _a)
        //        {
        //            Byte[] _b = new Byte[5];
        //            using (FileStream _fs = new FileStream(_a, FileMode.Open, FileAccess.Read))
        //                _fs.Read(_b, 0, 5);

        //            if (_b[0] == 0xFF && _b[1] == 0xFE && _b[2] == 0x00 && _b[3] == 0x00) return Encoding.UTF32;
        //            if (_b[0] == 0x00 && _b[1] == 0x00 && _b[2] == 0xFE && _b[3] == 0xFF) return new UTF32Encoding(true, true);

        //            if (_b[0] == 0xFF && _b[1] == 0xFE) return Encoding.Unicode;
        //            if (_b[0] == 0xFE && _b[1] == 0xFF) return Encoding.BigEndianUnicode;

        //            if (_b[0] == 0xEF && _b[1] == 0xBB && _b[2] == 0xBF) return Encoding.UTF8;

        //            if (_b[0] == 0x2B && _b[1] == 0x2F && _b[2] == 0x76 && _b[3] == 0x38 && _b[4] == 0x2D) return Encoding.UTF7;

        //            return Encoding.ASCII;
        //        }
    }
}

#endif