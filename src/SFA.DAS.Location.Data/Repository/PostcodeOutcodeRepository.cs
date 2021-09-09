using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Data.Repository
{
    public class PostcodeOutcodeRepository: IPostcodeOutcodeRepository
    {
        private readonly ILocationDataContext _dataContext;

        public PostcodeOutcodeRepository(ILocationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<PostcodeOutcode> GetOutcode(string outcode)
        {
            return await _dataContext.Outcodes
                .FirstOrDefaultAsync(o => o.Outcode.Equals(outcode));
        }
    }
}