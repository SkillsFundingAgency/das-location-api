using Newtonsoft.Json;
using System.Collections.Generic;

namespace SFA.DAS.Location.Domain.ImportTypes;
public record OsNearestApiResponse
{
    [JsonProperty("header")]
    public Header ResponseHeader { get; set; }

    [JsonProperty("results")]
    public List<Result> Results { get; set; }

    public class Result
    {
        [JsonProperty("DPA")]
        public Dpa Dpa { get; set; }
    }

    public class Header
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("totalresults")]
        public int Totalresults { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("dataset")]
        public string Dataset { get; set; }

        [JsonProperty("lr")]
        public string Lr { get; set; }

        [JsonProperty("maxresults")]
        public int Maxresults { get; set; }

        [JsonProperty("epoch")]
        public string Epoch { get; set; }

        [JsonProperty("lastupdate")]
        public string Lastupdate { get; set; }

        [JsonProperty("output_srs")]
        public string OutputSrs { get; set; }

        [JsonProperty("srs")]
        public string Srs { get; set; }
    }

    public class Dpa
    {
        [JsonProperty("UPRN")]
        public string Uprn { get; set; }

        [JsonProperty("UDPRN")]
        public string Udprn { get; set; }

        [JsonProperty("ADDRESS")]
        public string Address { get; set; }

        [JsonProperty("ORGANISATION_NAME")]
        public string Organisationname { get; set; }

        [JsonProperty("BUILDING_NAME")]
        public string Buildingname { get; set; }

        [JsonProperty("THOROUGHFARE_NAME")]
        public string Thoroughfarename { get; set; }

        [JsonProperty("POST_TOWN")]
        public string Posttown { get; set; }

        [JsonProperty("POSTCODE")]
        public string Postcode { get; set; }

        [JsonProperty("RPC")]
        public string Rpc { get; set; }

        [JsonProperty("X_COORDINATE")]
        public double Xcoordinate { get; set; }

        [JsonProperty("Y_COORDINATE")]
        public double Ycoordinate { get; set; }

        [JsonProperty("LNG")]
        public double Lng { get; set; }

        [JsonProperty("LAT")]
        public double Lat { get; set; }

        [JsonProperty("STATUS")]
        public string Status { get; set; }

        [JsonProperty("LOGICAL_STATUS_CODE")]
        public string Logicalstatuscode { get; set; }

        [JsonProperty("CLASSIFICATION_CODE")]
        public string Classificationcode { get; set; }

        [JsonProperty("CLASSIFICATION_CODE_DESCRIPTION")]
        public string Classificationcodedescription { get; set; }

        [JsonProperty("LOCAL_CUSTODIAN_CODE")]
        public int Localcustodiancode { get; set; }

        [JsonProperty("LOCAL_CUSTODIAN_CODE_DESCRIPTION")]
        public string Localcustodiancodedescription { get; set; }

        [JsonProperty("COUNTRY_CODE")]
        public string Countrycode { get; set; }

        [JsonProperty("COUNTRY_CODE_DESCRIPTION")]
        public string Countrycodedescription { get; set; }

        [JsonProperty("POSTAL_ADDRESS_CODE")]
        public string Postaladdresscode { get; set; }

        [JsonProperty("POSTAL_ADDRESS_CODE_DESCRIPTION")]
        public string Postaladdresscodedescription { get; set; }

        [JsonProperty("BLPU_STATE_CODE")]
        public string Blpustatecode { get; set; }

        [JsonProperty("BLPU_STATE_CODE_DESCRIPTION")]
        public string Blpustatecodedescription { get; set; }

        [JsonProperty("TOPOGRAPHY_LAYER_TOID")]
        public string Topographylayertoid { get; set; }

        [JsonProperty("WARD_CODE")]
        public string Wardcode { get; set; }

        [JsonProperty("LAST_UPDATE_DATE")]
        public string Lastupdatedate { get; set; }

        [JsonProperty("ENTRY_DATE")]
        public string Entrydate { get; set; }

        [JsonProperty("BLPU_STATE_DATE")]
        public string Blpustatedate { get; set; }

        [JsonProperty("LANGUAGE")]
        public string Language { get; set; }

        [JsonProperty("MATCH")]
        public double Match { get; set; }

        [JsonProperty("MATCH_DESCRIPTION")]
        public string Matchdescription { get; set; }

        [JsonProperty("DELIVERY_POINT_SUFFIX")]
        public string Deliverypointsuffix { get; set; }
    }
}