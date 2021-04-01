using Codex.Sealed;

using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace Codex.Data.Extension
{
    public static class GenericExtension
    {
        public static string To_Xml<T>(this T _this, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;

            if (_this is DataTable || _this is DataSet)
            {
                XmlWriterSettings _se = new XmlWriterSettings
                {
                    Encoding = _enc
                };

#if (NET45 || NETSTANDARD2_0)
            _se.Async = true;
#endif
                using (StringWriter _sw = new StringWriterWithEncoding(_enc))
                {
                    using (XmlWriter _w = XmlWriter.Create(_sw, _se))
                    {
                        if (_this is DataTable _dt)
                            _dt.WriteXml(_w);
                        if (_this is DataSet _ds)
                            _ds.WriteXml(_w);
                    }
                    return _sw.ToString();
                }
            }
            else
                return Codex.Extension.GenericExtension.To_Xml(_this, _enc);

        }
        public static void To_Xml<T>(this T _this, string _path, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;

            if (_this is DataTable || _this is DataSet)
            {
                XmlWriterSettings _se = new XmlWriterSettings
                {
                    Encoding = _enc
                };

#if (NET45 || NETSTANDARD2_0)
            _se.Async = true;
#endif

                using (XmlWriter _w = XmlWriter.Create(_path, _se))
                {
                    if (_this is DataTable _dt)
                        _dt.WriteXml(_w);
                    if (_this is DataSet _ds)
                        _ds.WriteXml(_w);
                }
            }
            else
                Codex.Extension.GenericExtension.To_Xml(_this, _path, _enc);
        }
    }
}
