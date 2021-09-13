namespace SFA.DAS.Location.Domain.Configuration
{
    public static class Constants
    {
        public const string NationalOfficeOfStatisticsLocationUrl = "https://services1.arcgis.com/ESMARspQHYMw9BZ9/arcgis/rest/services/IPN_GB_2016/FeatureServer/0/query?where=ctry15nm%20%3D%20'ENGLAND'%20AND%20popcnt%20%3E%3D%20500%20AND%20popcnt%20%3C%3D%2010000000&outFields=placeid,place15nm,ctry15nm,cty15nm,ctyltnm,lad15nm,laddescnm,pcon15nm,lat,long,popcnt,descnm,rgn15nm&returnDistinctValues=true&outSR=4326&f=json&resultRecordCount={0}&resultOffSet={1}";
        public const string PostcodesUrl = "https://api.postcodes.io/postcodes?q={0}&limit={1}";
        public const string PostcodeUrl = "https://api.postcodes.io/postcodes/{0}";
        public const string DistrictNameUrl = "https://api.postcodes.io/outcodes/{0}";
    }
}