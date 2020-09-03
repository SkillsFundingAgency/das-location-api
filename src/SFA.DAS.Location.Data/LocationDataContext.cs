using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.Location.Domain.Configuration;

namespace SFA.DAS.Location.Data
{
    public interface ILocationDataContext
    {
        DbSet<Domain.Entities.Location> Locations { get; set; }
        DbSet<Domain.Entities.LocationImport> LocationImports { get; set; }
        DbSet<Domain.Entities.ImportAudit> ImportAudit { get; set; }
        int SaveChanges();
        string GetProviderName();
    }
    
    public class LocationDataContext : DbContext, ILocationDataContext
    {
        private const string AzureResource = "https://database.windows.net/";
        private readonly LocationApiConfiguration _configuration;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;

        public DbSet<Domain.Entities.Location> Locations { get; set; }
        public DbSet<Domain.Entities.LocationImport> LocationImports { get; set; }
        public DbSet<Domain.Entities.ImportAudit> ImportAudit { get; set; }

        public LocationDataContext()
        {
        }

        public LocationDataContext(DbContextOptions options) : base(options)
        {
        }
        
        public LocationDataContext(IOptions<LocationApiConfiguration> config, DbContextOptions options, AzureServiceTokenProvider azureServiceTokenProvider) :base(options)
        {
            _configuration = config.Value;
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        public string GetProviderName()
        {
            return base.Database.ProviderName;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration == null || _azureServiceTokenProvider == null)
            {
                return;
            }
            
            var connection = new SqlConnection
            {
                ConnectionString = _configuration.ConnectionString,
                AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync(AzureResource).Result
            };
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configuration.Location());
            modelBuilder.ApplyConfiguration(new Configuration.LocationImport());
            modelBuilder.ApplyConfiguration(new Configuration.ImportAudit());
        }
    }
}