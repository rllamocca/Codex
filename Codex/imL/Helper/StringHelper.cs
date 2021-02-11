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

        public static String To_Xml(Object _a, Encoding _b)
        {
            if (_a == null) return null;

            XmlWriterSettings _s = new XmlWriterSettings();
            _s.Encoding = _b;

            _s.Async = true;

            using (StringWriter _sw = new StringWriter())
            {
                using (XmlWriter _xw = XmlWriter.Create(_sw, _s))
                {
                    if (_a is DataSet _ds)
                    {
                        _ds.WriteXml(_xw);
                    }
                    else if (_a is DataTable _dt)
                    {
                        _dt.WriteXml(_xw);
                    }
                    else
                    {
                        XmlSerializer _xs = new XmlSerializer(_a.GetType());
                        _xs.Serialize(_xw, _a);
                    }
                    return _sw.ToString();
                }
            }
        }
    }
}

#endif