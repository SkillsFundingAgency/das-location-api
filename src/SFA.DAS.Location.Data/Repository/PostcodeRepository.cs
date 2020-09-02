using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Data.Repository
{
    public class PostcodeRepository : IPostcodeRepository
    {
        private readonly HttpClient _client;
        public PostcodeRepository(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<SuggestedLocation>> GetAllStartingWithOutcode(string query, int resultCount = 10)
        {
            // var result = await ...
            return null;
        }
    }
}
