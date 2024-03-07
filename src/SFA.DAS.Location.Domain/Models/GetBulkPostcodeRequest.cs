using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Location.Domain.Models;

public class GetBulkPostcodeRequest
{
    [JsonProperty("postcodes")]
    public List<string> Postcodes { get; set; }
}