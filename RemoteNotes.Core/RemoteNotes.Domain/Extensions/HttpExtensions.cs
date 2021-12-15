using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RemoteNotes.Domain.Tools;

namespace RemoteNotes.Domain.Extensions
{
    public static class HttpExtensions
    {
        public static HttpRequestBuilder CreateRequest(this HttpClient client)
            => new HttpRequestBuilder(client);

        public static HttpContent ToHttpContent(this object requestBody)
        {
            var result = default(HttpContent);
            requestBody ??= new object();

            if (requestBody is HttpContent content)
            {
                result = content;
            }
            else
            {
                var jsonString = JsonConvert.SerializeObject(requestBody, new IsoDateTimeConverter());
                result = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            return result;
        }
    }
}
