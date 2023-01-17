using AutoFixture;
using SFA.DAS.Location.Domain.ImportTypes;
using System.Net;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace SFA.DAS.Location.MockAPIs;

public class MockApiBuilder
{
    private readonly WireMockServer _server;
    private readonly Fixture _fixture;

    public MockApiBuilder(int port)
    {
        _server = WireMockServer.Start(new WireMockServerSettings
        {
            Port = port,
            UseSSL = true,
            StartAdminInterface = true,
            Logger = new WireMockConsoleLogger(),
        });

        _fixture = new Fixture();
    }

    public static MockApiBuilder Create(int port)
    {
        return new MockApiBuilder(port);
    }

    public MockApi Build()
    {
        return new MockApi(_server);
    }

    public MockApiBuilder StartEndPoints()
    {
        // for testing LocationImportService - ensure constant NationalOfficeOfStatisticsLocationUrl is set to
        // "https://localhost:5121/services1.arcgis.com/ESMARspQHYMw9BZ9/arcgis/rest/services/IPN_GB_2016/FeatureServer/0/query?where=ctry15nm%20%3D%20'ENGLAND'%20AND%20popcnt%20%3E%3D%20500%20AND%20popcnt%20%3C%3D%2010000000&outFields=placeid,place15nm,ctry15nm,cty15nm,ctyltnm,lad15nm,laddescnm,pcon15nm,lat,long,popcnt,descnm&returnDistinctValues=true&outSR=4326&f=json&resultRecordCount={0}&resultOffSet={1}";

        var locResp = _fixture.Build<OnsLocationApiResponse>().With(x => x.ExceededTransferLimit, false).Create();

        locResp.Features.ForEach(feature => {
            feature.Attributes.PlaceNameDescription = "MD";
            feature.Attributes.LocalAuthorityDistrictDescription = "MD";
        });

        _server.Given(Request.Create()
                        .WithPath("*services1.arcgis.com*")
                        .UsingGet())
               .RespondWith(Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBodyAsJson(locResp));

        // for testing OsPlacesApiService - ensure constant OsPlacesFindUrl is set to
        // "https://localhost:5121/api.os.uk/search/places/v1/find?key={0}&query={1}&dataset={2}&minmatch={3:0.0#}&matchprecision={4}&output_srs=EPSG:4326";

        var placeResp = _fixture.Create<OsPlacesApiResponse>();

        _server.Given(Request.Create()
                        .WithPath("*api.os.uk/search*")
                        .UsingGet())
               .RespondWith(Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBodyAsJson(placeResp));

        return this;
    }
}
