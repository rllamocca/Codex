using Codex.Utility.Http.Helper;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Codex.Utility.Http.Handler
{
    public class CompressHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage _request, CancellationToken _token)
        {
            await HttpHelper.DecompressContent(_request);

            HttpResponseMessage _response = await base.SendAsync(_request, _token);

            await HttpHelper.CompressContent(_response, _request.Headers.AcceptEncoding);

            return _response;
        }
    }
}