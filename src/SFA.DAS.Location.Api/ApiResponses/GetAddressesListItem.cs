using SFA.DAS.Location.Domain.Models;
using System.Linq;

namespace SFA.DAS.Location.Api.ApiResponses;

public class GetAddressesListItem
{
    public string Uprn { get; set; }
    public string Organisation { get; set; }
    public string Premises { get; set; }
    public string Thoroughfare  { get; set; }
    public string Locality { get; set; }
    public string PostTown { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public double? Match { get; set; }

    public static GetAddressesListItem From(SuggestedAddress source)
    {
        // when the majority of the premises are digit characters combine with the thoroughfare and if the combination
        // are a majority of digit characters combine with the locality
        var premises = !MajorityIsDigit(source.Premises) 
            ? source.Premises 
            : string.Empty;
            
        var thoroughfare = MajorityIsDigit(source.Premises)
            ? string.Join(" ", (new string[] { source.Premises, source.Thoroughfare }).Where(s => !string.IsNullOrEmpty(s)))
            : source.Thoroughfare;
        
        var locality = MajorityIsDigit(thoroughfare)
            ? string.Join(" ", (new string[] { thoroughfare, source.Locality }).Where(s => !string.IsNullOrEmpty(s)))
            : source.Locality;
        
        thoroughfare = !MajorityIsDigit(thoroughfare)
            ? thoroughfare
            : string.Empty;
        
        var addressLines = new string[] 
        {
            premises,
            thoroughfare,
            locality
        }.Where(p => !string.IsNullOrEmpty(p));
            
        return new GetAddressesListItem
        {
            Uprn = source.Uprn,
            Organisation = source.Organisation,
            Premises = source.Premises,
            Thoroughfare  = source.Thoroughfare,
            Locality = source.Locality,
            PostTown = source.PostTown,
            County = source.County,
            Postcode = source.Postcode,
            Country = source.Country,
            AddressLine1 = addressLines.Count() >= 1 ? addressLines.ElementAt(0) : string.Empty,
            AddressLine2 = addressLines.Count() >= 2 ? addressLines.ElementAt(1) : string.Empty,
            AddressLine3 = addressLines.Count() >= 3 ? addressLines.ElementAt(2) : string.Empty,
            Longitude = source.Longitude,
            Latitude = source.Latitude,
            Match = source.Match
        };
    }

    private static bool MajorityIsDigit(string input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        var digits = input.Where(char.IsDigit);
        return digits.Count() > (input.Length / 2);
    }
}