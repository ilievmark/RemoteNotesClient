using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RemoteNotes.Domain.Extensions;

namespace RemoteNotes.Domain.Tools
{
    public class HttpRequestBuilder
    {
        private readonly HttpClient _client;

        private HttpRequestMessage _buildingRequest;
        private CancellationToken _cancellationToken;

        public HttpRequestBuilder(HttpClient client)
        {
            _client = client;

            _buildingRequest = new HttpRequestMessage();
        }

        public HttpRequestBuilder ByResource(string address, string resource, params object[] parameters)
        {
            var url = address + string.Format(resource, parameters);
            _buildingRequest.RequestUri = new Uri(url);
            return this;
        }

        public HttpRequestBuilder WithMethod(HttpMethod method)
        {
            _buildingRequest.Method = method;
            return this;
        }

        public HttpRequestBuilder WithContent(object content)
        {
            _buildingRequest.Content = content.ToHttpContent();
            return this;
        }

        public HttpRequestBuilder WithCancellationToken(CancellationToken token)
        {
            _cancellationToken = token;
            return this;
        }

        public HttpRequestBuilder WithAuthorization(string type, string token)
        {
            var auth = string.Format("{0} {1}", type, token);
            _buildingRequest.Headers.Add("Authorization", auth);
            return this;
        }

        public Task<HttpResponseMessage> MakeRequestAsync()
        {
            return _client.SendAsync(_buildingRequest, _cancellationToken);
        }
    }
}