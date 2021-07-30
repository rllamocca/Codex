#if (NET35 || NET40 || NET45|| NETSTANDARD2_0)
using System.Xml.Serialization;
#endif

using Codex.Enumeration;
using Codex.Sealed;

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Codex.Utility
{
    public static class GenericExtension
    {
        public static T[] RandomSort<T>(this T[] _array, ERandomSort _sort = ERandomSort.None)
        {
            if (_array == null)
                return null;

            T[] _return = _array;
            Random _r = new Random();

            switch (_sort)
            {
                case ERandomSort.Fisher_Yates:
                    for (int _k = _return.Length - 1; _k > 0; _k--)
                    {
                        int _az = _r.Next(_k);
                        T _tmp = _return[_az];
                        _return[_az] = _return[_k];
                        _return[_k] = _tmp;
                    }
                    break;
                default:
                    break;
            }

            return _return;
        }
        public static List<T> RandomSort<T>(this List<T> _array, ERandomSort _sort = ERandomSort.None)
        {
            if (_array == null)
                return null;

            List<T> _return = _array;
            Random _r = new Random();

            switch (_sort)
            {
                case ERandomSort.Fisher_Yates:
                    for (int _k = _return.Count - 1; _k > 0; _k--)
                    {
                        int _az = _r.Next(_k);
                        T _tmp = _return[_az];
                        _return[_az] = _return[_k];
                        _return[_k] = _tmp;
                    }
                    break;
                default:
                    break;
            }

            return _return;
        }

#if (NET35 || NET40 || NET45|| NETSTANDARD2_0)
        public static string To_Xml<T>(this T _this, Encoding _enc = null)
        {
            if (_this == null)
                return null;
            if (_enc == null)
                _enc = Encoding.Unicode;

            XmlWriterSettings _se = new XmlWriterSettings
            {
                Encoding = _enc
            };

#if (NET45 || NETSTANDARD2_0)
            _se.Async = true;
#endif
            XmlSerializer _xs = new XmlSerializer(_this.GetType());

            using (StringWriter _sw = new StringWriterWithEncoding(_enc))
            {
                using (XmlWriter _w = XmlWriter.Create(_sw, _se))
                    _xs.Serialize(_w, _this);
                return _sw.ToString();
            }
        }
        public static void To_Xml<T>(this T _this, string _path, Encoding _enc = null)
        {
            if (_this == null)
                return;
            if (_enc == null)
                _enc = Encoding.Unicode;

            XmlWriterSettings _se = new XmlWriterSettings
            {
                Encoding = _enc
            };

#if (NET45 || NETSTANDARD2_0)
            _se.Async = true;
#endif

            XmlSerializer _xml = new XmlSerializer(_this.GetType());

            using (XmlWriter _w = XmlWriter.Create(_path, _se))
                _xml.Serialize(_w, _this);
        }
#endif
    }
}
