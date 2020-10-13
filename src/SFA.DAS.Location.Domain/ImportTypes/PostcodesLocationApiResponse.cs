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
        // does the below return an authority name?

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("latitude")]
        public double Lat { get; set; }

        [JsonProperty("longitude")]
        public double Long { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("admin_district")]
        public string AdminDistrict { get; set; }

    }
}
