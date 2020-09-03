using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace SFA.DAS.Location.Domain.ImportTypes
{
    public class PostcodesLocationApiResponse
    {
        [JsonProperty("result")]
        public List<PostcodesLocationApiItem> Result { get; set; }
    }

    public class PostcodeLocationApiResponse
    {
        [JsonProperty("result")]
        public PostcodesLocationApiItem Result { get; set; }
    }

    public class PostcodesLocationApiItem
    {
        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("latitude")]
        public double Lat { get; set; }

        [JsonProperty("longitude")]
        public double Long { get; set; }
    }
}
