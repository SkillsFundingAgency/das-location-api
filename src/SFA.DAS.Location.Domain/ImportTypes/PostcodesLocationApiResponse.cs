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

    public class PostcodeLocationDistrictApiResponse
    {
        [JsonProperty("result")]
        public PostcodeDistrictLocationApiResponse Result { get; set; }
    }

    public class PostcodesLocationApi
    {
        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("latitude")]
        public double Lat { get; set; }

        [JsonProperty("longitude")]
        public double Long { get; set; }

        [JsonProperty("outcode")]
        public string Outcode { get; set; }
    }
    public class PostcodesLocationApiItem : PostcodesLocationApi
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class PostcodeDistrictLocationApiResponse : PostcodesLocationApi
    {
        [JsonProperty("admin_district")]
        public string[] AdminDistrict { get; set; }
        [JsonProperty("country")]
        public string[] Country { get; set; }
    }
}
