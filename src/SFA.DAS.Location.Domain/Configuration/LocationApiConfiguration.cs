namespace SFA.DAS.Location.Domain.Configuration
{
    public class LocationApiConfiguration
    {
        public string ConnectionString { get ; set ; }

        public string OsPlacesApiKey { get; set; }
        public string LocationImportFilePath { get; set; }
        public string LocationImportFileName { get; set; }
    }
}