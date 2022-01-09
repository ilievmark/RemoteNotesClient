using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RemoteNotes.Domain.Extensions
{
    public static class StringExtensions
    {
        public static T ParseAsJson<T>(this string str)
            => JsonConvert.DeserializeObject<T>(str, converters: new JsonConverter[] { new IsoDateTimeConverter() });

        public static string ToJson<T>(this T obj)
            => JsonConvert.SerializeObject(obj);
    }
}
