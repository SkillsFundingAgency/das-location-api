namespace SFA.DAS.Location.Domain.Configuration
{
    public static class Constants
    {
        public const string NationalOfficeOfStatisticsLocationUrl = "https://ons-inspire.esriuk.com/arcgis/rest/services/Other_Products/Index_of_Place_Names_July_2016/MapServer/0/query?outSR=4326&f=json&outFields=placeid,place15nm,ctry15nm,cty15nm,ctyltnm,lad15nm,pcon15nm,lat,long_,popcnt&where=ctry15nm%20%3D%20%27England%27%20AND%20descnm%20%3D%20%27LOC%27&resultRecordCount={0}&resultOffSet={1}";
        public const string PostcodesUrl = "https://api.postcodes.io/postcodes?q={0}&limit={1}";
        public const string PostcodeUrl = "https://api.postcodes.io/postcodes/{0}";
        public const string DistrictNameUrl = "https://api.postcodes.io/outcodes/{0}";
    }
}