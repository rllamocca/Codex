using Codex.Enum;
using Codex.Helper;

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Codex.Http.Helper
{
    public static class HttpHelper
    {
        public static async Task CompressContent(HttpResponseMessage _r, HttpHeaderValueCollection<StringWithQualityHeaderValue> _ae)
        {
            if (_r.Content != null)
            {
                ECompress _compress = ECompress.None;
                if (_ae.Contains(new StringWithQualityHeaderValue("deflate"))) _compress = ECompress.Deflate;
                if (_ae.Contains(new StringWithQualityHeaderValue("gzip"))) _compress = ECompress.Gzip;

                switch (_compress)
                {
                    case ECompress.Gzip:
                    case ECompress.Deflate:
                        using (Stream _content = await _r.Content.ReadAsStreamAsync())
                        {
                            StreamContent _response = new StreamContent(StreamHelper.Compress(_content, _compress));
                            foreach (KeyValuePair<string, IEnumerable<string>> _item in _r.Content.Headers)
                                if (_item.Key != "Content-Length")
                                    _response.Headers.TryAddWithoutValidation(_item.Key, _item.Value);
                            _response.Headers.ContentEncoding.Add((_compress == ECompress.Gzip) ? "gzip" : "deflate");

                            _r.Content = _response;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public static async Task DecompressContent(HttpRequestMessage _r)
        {
            if (_r.Content != null)
            {
                ICollection<string> _ce = _r.Content.Headers.ContentEncoding;
                ECompress _decompress = ECompress.None;
                if (_ce.Contains("deflate")) _decompress = ECompress.Deflate;
                if (_ce.Contains("gzip")) _decompress = ECompress.Gzip;

                switch (_decompress)
                {
                    case ECompress.Gzip:
                    case ECompress.Deflate:
                        using (Stream _content = await _r.Content.ReadAsStreamAsync())
                        {
                            StreamContent _request = new StreamContent(StreamHelper.Decompress(_content, _decompress));
                            foreach (KeyValuePair<string, IEnumerable<string>> _item in _r.Content.Headers)
                            {
                                if (_item.Key != "Content-Length")
                                    _request.Headers.TryAddWithoutValidation(_item.Key, _item.Value);
                            }
                            _request.Headers.ContentEncoding.Remove((_decompress == ECompress.Gzip) ? "gzip" : "deflate");

                            _r.Content = _request;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
