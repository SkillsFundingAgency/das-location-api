using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Location.Domain.ImportTypes
{
    public class LocationApiItem
    {
        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }
    
    public class Feature
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }
    
    public class Attributes
    {
        [JsonProperty("placeid")]
        public long Id { get; set; }

        [JsonProperty("place15nm")]
        public string LocationName { get; set; }

        [JsonProperty("cty15nm")]
        public string CountyName { get; set; }

        [JsonProperty("lad15nm")]
        public string LocalAuthorityName { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("long")]
        public double Long { get; set; }

    }
}