namespace SFA.DAS.Location.Domain.Entities
{
    public class LocationBase
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string CountyName { get; set; }
        public string LocalAuthorityName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}