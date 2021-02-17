using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Codex.ETL.Helper
{
    public static class StringHelper
    {
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

#if (NET45 || NETSTANDARD2_0)
            _s.Async = true;
#endif

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
    }
}