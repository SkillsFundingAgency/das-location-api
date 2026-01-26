using System.Text.Encodings.Web;
using FluentAssertions;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient;

public class WhenBuildingOsPlacesUrl
{
    private const string Postcode = "SW1A 1AA";
    
    [Test, MoqAutoData]
    public void Then_Url_Is_Built_With_Default_Parameters()
    {
        // act
        var result = OsPlacesUrlBuilder.Create(Postcode);

        // assert
        result.Should().Be($"https://api.os.uk/search/places/v1/postcode?postcode={UrlEncoder.Default.Encode(Postcode)}&dataset=dpa&output_srs={UrlEncoder.Default.Encode("EPSG:4326")}&fq={UrlEncoder.Default.Encode("COUNTRY_CODE:E")}");
    }
    
    [Test, MoqAutoData]
    public void Then_MaxResults_Is_Added_To_The_Query_Parameters()
    {
        // act
        var result = OsPlacesUrlBuilder.Create(Postcode, maxResults: 10);

        // assert
        result.Should().Contain("&maxresults=10");
    }
    
    [Test, MoqAutoData]
    public void Then_Filter_Can_Be_Overridden()
    {
        // act
        var result = OsPlacesUrlBuilder.Create(Postcode, filter: "TEST");

        // assert
        result.Should().Contain("&fq=TEST");
    }
    
    [Test, MoqAutoData]
    public void Then_Dataset_Can_Be_Overridden()
    {
        // act
        var result = OsPlacesUrlBuilder.Create(Postcode, dataset: "TEST");

        // assert
        result.Should().Contain("&dataset=TEST");
    }
    
    [Test, MoqAutoData]
    public void Then_OutputSrs_Can_Be_Overridden()
    {
        // act
        var result = OsPlacesUrlBuilder.Create(Postcode, outputSrs: "TEST");

        // assert
        result.Should().Contain("&output_srs=TEST");
    }
}