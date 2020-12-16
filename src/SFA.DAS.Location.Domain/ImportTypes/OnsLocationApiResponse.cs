using System;
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
        
        [JsonProperty("descnm")] 
        public string PlaceNameDescription { get; set; }

        [JsonProperty("laddescnm")] 
        public string LocalAuthorityDistrictDescription { get; set; }
        
        [JsonProperty("lad15nm")]
        public string LocationAuthorityDistrict { get; set; }
        
        [JsonIgnore]
        public PlaceNameDescription PlaceName => MapDescription<PlaceNameDescription>(PlaceNameDescription);

        [JsonIgnore]
        public LocalAuthorityDistrict LocalAuthorityDistrict => MapDescription<LocalAuthorityDistrict>(LocalAuthorityDistrictDescription);

        private TEnum MapDescription<TEnum>(string localAuthorityDistrictDescription) where TEnum : struct
        {
            Enum.TryParse<TEnum>(localAuthorityDistrictDescription,out var result);
            return result;
        }
    }

    public enum PlaceNameDescription
    {
        None = 0,
        BUA = 1,
        BUASD = 2,
        LONB = 3,
        NMD = 4,
        WD = 5,
        MD = 6,
        LOC = 7
    }

    public enum LocalAuthorityDistrict
    {
        NMD=1,
        LONB=2,
        MD=3,
        UA=4
    }
    
}