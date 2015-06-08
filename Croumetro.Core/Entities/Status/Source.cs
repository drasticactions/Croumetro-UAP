using Newtonsoft.Json;

namespace Croumetro.Core.Entities.Status
{

    public class Source
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
