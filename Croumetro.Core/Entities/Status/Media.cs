using Newtonsoft.Json;

namespace Croumetro.Core.Entities.Status
{

    public class Media
    {

        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
