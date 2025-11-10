using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Infrastructure.ApiClient;

public class PostCodeApiV2Service(HttpClient httpClient): IPostcodeApiV2Service
{
    private static readonly JsonSerializerOptions ApiSerialisationOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
    
    public async Task<PostcodeDataV2> LookupPostcodeAsync(string query, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(new Uri(string.Format(Constants.PostcodeUrl, query)), cancellationToken);
        if (response.StatusCode.Equals(HttpStatusCode.NotFound))
        {
            return null;
        }
        
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var item = JsonSerializer.Deserialize<PostcodeLookupResponse>(jsonResponse, ApiSerialisationOptions);
        return PostcodeDataV2.From(item.Result);
    }
}