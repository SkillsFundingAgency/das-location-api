using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Location.Domain.ImportTypes
{
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
        [JsonProperty("LPI")]
        public LpiResultPlacesApiItem Lpi { get; set; }
    }

    public class LpiResultPlacesApiItem
    {
        [JsonProperty("UPRN")]
        public string Uprn { get; set; }

        [JsonProperty("ADDRESS")]
        public string Address { get; set; }
        
        [JsonProperty("USRN")]
        public string Usrn { get; set; }
        
        [JsonProperty("LPI_KEY")]
        public string LpiKey { get; set; }

        [JsonProperty("PAO_START_NUMBER")]
        public string PaoStartNumber { get; set; }

        [JsonProperty("PAO_TEXT")]
        public string PaoText { get; set; }

        [JsonProperty("STREET_DESCRIPTION")]
        public string StreetDescription { get; set; }

        [JsonProperty("TOWN_NAME")]
        public string TownName { get; set; }

        [JsonProperty("ADMINISTRATIVE_AREA")]
        public string AdministrativeArea { get; set; }
        
        [JsonProperty("POSTCODE_LOCATOR")]
        public string PostCodeLocator { get; set; }

        [JsonProperty("RPC")]
        public string Rpc { get; set; }

        [JsonProperty("X_COORDINATE")]
        public decimal XCoordinate { get; set; }

        [JsonProperty("Y_COORDINATE")]
        public decimal YCoordinate { get; set; }

        [JsonProperty("LNG")]
        public double Lng { get; set; }

        [JsonProperty("LAT")]
        public double Lat { get; set; }

        [JsonProperty("STATUS")]
        public string Status { get; set; }

        [JsonProperty("LOGICAL_STATUS_CODE")]
        public string LogicalStatusCode { get; set; }

        [JsonProperty("CLASSIFICATION_CODE")]
        public string ClassificationCode { get; set; }

        [JsonProperty("CLASSIFICATION_CODE_DESCRIPTION")]
        public string ClassificationCodeDescription { get; set; }

        [JsonProperty("LOCAL_CUSTODIAN_CODE")]
        public int LocalCustodianCode { get; set; }

        [JsonProperty("LOCAL_CUSTODIAN_CODE_DESCRIPTION")]
        public string LocalCustodianCodeDescription { get; set; }

        [JsonProperty("POSTAL_ADDRESS_CODE")]
        public string PostalAddressCode { get; set; }

        [JsonProperty("POSTAL_ADDRESS_CODE_DESCRIPTION")]
        public string PostalAddressCodeDescription { get; set; }

        [JsonProperty("BLPU_STATE_CODE")]
        public string BlpuStateCode { get; set; }

        [JsonProperty("BLPU_STATE_CODE_DESCRIPTION")]
        public string BlpuStateCodeDescrption { get; set; }

        [JsonProperty("TOPOGRAPHY_LAYER_TOID")]
        public string TopographyLayerToId { get; set; }

        [JsonProperty("LAST_UPDATE_DATE")]
        public string LastUpdateDate { get; set; }

        [JsonProperty("ENTRY_DATE")]
        public string EntryDate { get; set; }
        
        [JsonProperty("BLPU_STATE_DATE")]
        public string BlpuStaeDate { get; set; }
        [JsonProperty("STREET_STATE_CODE")]
        public string StreetStateCode { get; set; }

        [JsonProperty("STREET_STATE_CODE_DESCRIPTION")]
        public string StreetStateCodeDescription { get; set; }

        [JsonProperty("STREET_CLASSIFICATION_CODE")]
        public string StreepClassificationCode { get; set; }

        [JsonProperty("STREET_CLASSIFICATION_CODE_DESCRIPTION")]
        public string StreetClassificationCodeDescrption { get; set; }

        [JsonProperty("LPI_LOGICAL_STATUS_CODE")]
        public string LpiLogicalStatusCode { get; set; }

        [JsonProperty("LPI_LOGICAL_STATUS_CODE_DESCRIPTION")]
        public string LpiLogicalStatusCodeDescrption { get; set; }
        [JsonProperty("LANGUAGE")]
        public string Language { get; set; }
        
        [JsonProperty("MATCH")]
        public double Match { get; set; }

        [JsonProperty("MATCH_DESCRIPTION")]
        public string MatchDescrption { get; set; }
    }
}
