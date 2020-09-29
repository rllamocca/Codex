#if (NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Enum;
using Codex.Helper;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Codex.Http.Handler
{
    public class CompressHandler : DelegatingHandler
    {
        private static async Task DeCompressContent(HttpRequestMessage _r)
        {
            if (_r.Content != null)
            {
                ICollection<String> _ae = _r.Content.Headers.ContentEncoding;
                ECompress _decompress = ECompress.None;
                if (_ae.Contains("deflate")) _decompress = ECompress.Deflate;
                if (_ae.Contains("gzip")) _decompress = ECompress.Gzip;

                switch (_decompress)
                {
                    case ECompress.Gzip:
                    case ECompress.Deflate:
                        using (Stream _content = await _r.Content.ReadAsStreamAsync())
                        {
                            StreamContent _request = new StreamContent(StreamHelper.DeCompress(_content, _decompress));
                            foreach (KeyValuePair<String, IEnumerable<String>> _item in _r.Content.Headers)
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
        private static async Task CompressContent(HttpResponseMessage _r, HttpHeaderValueCollection<StringWithQualityHeaderValue> _ae)
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
                            foreach (KeyValuePair<String, IEnumerable<String>> _item in _r.Content.Headers)
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

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage _request, CancellationToken _cancel)
        {
            await CompressHandler.DeCompressContent(_request);

            HttpResponseMessage _response = await base.SendAsync(_request, _cancel);

            await CompressHandler.CompressContent(_response, _request.Headers.AcceptEncoding);

            return _response;
        }
    }
}

#endif