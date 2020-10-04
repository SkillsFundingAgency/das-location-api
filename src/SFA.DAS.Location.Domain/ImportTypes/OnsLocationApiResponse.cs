using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Location.Domain.ImportTypes
{
    public class OnsLocationApiResponse
    {
        [JsonProperty("features")]
        public List<LocationApiItem> Features { get; set; }
        [JsonProperty("exceededTransferLimit")]
        public bool ExceededTransferLimit { get; set; }
    }
    
    public class LocationApiItem
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }
    
    public class Attributes
    {
        [JsonProperty("placeid")]
        public int Id { get; set; }

        [JsonProperty("place15nm")]
        public string LocationName { get; set; }

        [JsonProperty("cty15nm")]
        public string CountyName { get; set; }

        [JsonProperty("ctyltnm")]
        public string LocalAuthorityName { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("long")]
        public double Long { get; set; }

    }
}