using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Workshop.App
{
    public class BlogSettings
    {
        public string Title { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BlogType BlogType { get; set; }

        public IDictionary<string, string> Properties { get; set; }
    }

    public enum BlogType
    {
        Games,
        Education
    }
}
