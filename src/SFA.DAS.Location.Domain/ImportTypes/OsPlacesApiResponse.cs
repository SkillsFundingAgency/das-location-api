using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Location.Domain.ImportTypes;

public class OsPlacesApiResponse
{
    [JsonProperty("header")]
    public Header Header { get; set; }
        
    [JsonProperty("results")]
    public List<ResultPlacesApiItem> Results { get; set; }
}

public class Header
{
    [JsonProperty("uri")]
    public string Uri { get; set; }
        
    [JsonProperty("query")]
    public string Query { get; set; }

    [JsonProperty("offset")]
    public long Offset { get; set; }

    [JsonProperty("totalresults")]
    public long TotalResults { get; set; }

    [JsonProperty("format")]
    public string Format { get; set; }
        
    [JsonProperty("dataset")]
    public string Dataset { get; set; }
        
    [JsonProperty("lr")]
    public string Lr { get; set; }

    [JsonProperty("maxresults")]
    public int MaxResults { get; set; }
        
    [JsonProperty("matchprecision")]
    public decimal MatchPrecision { get; set; }
           
    [JsonProperty("epoch")]
    public string Epoch { get; set; }

    [JsonProperty("output_srs")]
    public string OutputSrs { get; set; }
}

public class ResultPlacesApiItem
{
    [JsonProperty("DPA")]
    public DpaResultPlacesApiItem Dpa { get; set; }
}

public class DpaResultPlacesApiItem
{
    [JsonProperty("UPRN")]
    public string Uprn { get; set; }

    [JsonProperty("UDPRN")]
    public string Udprn { get; set; }

    [JsonProperty("ADDRESS")]
    public string Address { get; set; }

    [JsonProperty("PO_BOX_NUMBER")]
    public string PoBoxNumber { get; set; }

    [JsonProperty("ORGANISATION_NAME")]
    public string OrganisationName { get; set; }

    [JsonProperty("DEPARTMENT_NAME")]
    public string DepartmentName { get; set; }

    [JsonProperty("SUB_BUILDING_NAME")]
    public string SubBuildingName { get; set; }

    [JsonProperty("BUILDING_NAME")]
    public string BuildingName { get; set; }

    [JsonProperty("BUILDING_NUMBER")]
    public string BuildingNumber { get; set; }

    [JsonProperty("DEPENDENT_THOROUGHFARE_NAME")]
    public string DependentThoroughfareName { get; set; }

    [JsonProperty("THOROUGHFARE_NAME")]
    public string ThoroughfareName { get; set; }

    [JsonProperty("DOUBLE_DEPENDENT_LOCALITY")]
    public string DoubleDependentLocality { get; set; }

    [JsonProperty("DEPENDENT_LOCALITY")]
    public string DependentLocality { get; set; }

    [JsonProperty("POST_TOWN")]
    public string PostTown { get; set; }

    [JsonProperty("POSTCODE")]
    public string Postcode { get; set; }

    [JsonProperty("RPC")]
    public string Rpc { get; set; }

    [JsonProperty("X_COORDINATE")]
    public double? XCoordinate { get; set; }

    [JsonProperty("Y_COORDINATE")]
    public double? YCoordinate { get; set; }

    [JsonProperty("LNG")]
    public double? Lng { get; set; }

    [JsonProperty("LAT")]
    public double? Lat { get; set; }

    [JsonProperty("STATUS")]
    public string Status { get; set; }

    [JsonProperty("MATCH")]
    public double? Match { get; set; }

    [JsonProperty("MATCH_DESCRIPTION")]
    public string MatchDescription { get; set; }

    [JsonProperty("LANGUAGE")]
    public string Language { get; set; }

    [JsonProperty("LOCAL_CUSTODIAN_CODE")]
    public int? LocalCustodianCode { get; set; }

    [JsonProperty("LOCAL_CUSTODIAN_CODE_DESCRIPTION")]
    public string LocalCustodianCodeDescription { get; set; }
        
    [JsonProperty("COUNTRY_CODE")]
    public string CountryCode { get; set; }

    [JsonProperty("CLASSIFICATION_CODE")]
    public string ClassificationCode { get; set; }

    [JsonProperty("CLASSIFICATION_CODE_DESCRIPTION")]
    public string ClassificationCodeDescription { get; set; }

    [JsonProperty("POSTAL_ADDRESS_CODE")]
    public string PostalAddressCode { get; set; }

    [JsonProperty("POSTAL_ADDRESS_CODE_DESCRIPTION")]
    public string PostalAddressCodeDescription { get; set; }

    [JsonProperty("LOGICAL_STATUS_CODE")]
    public int? LogicalStatusCode { get; set; }

    [JsonProperty("BLPU_STATE_CODE")]
    public int? BlpuStateCode { get; set; }

    [JsonProperty("BLPU_STATE_CODE_DESCRIPTION")]
    public string BlpuStateCodeDescription { get; set; }

    [JsonProperty("TOPOGRAPHY_LAYER_TOID")]
    public string TopgrapghyLayerToID { get; set; }

    [JsonProperty("PARENT_UPRN")]
    public string ParentUprnARENT_UPR { get; set; }

    [JsonProperty("LAST_UPDATE_DATE")]
    public string LastUpdateDate { get; set; }

    [JsonProperty("ENTRY_DATE")]
    public string EntryDate { get; set; }

    [JsonProperty("LEGAL_NAME")]
    public string LegalName { get; set; }

    [JsonProperty("BLPU_STATE_DATE")]
    public string BlpuStateDate { get; set; }
}