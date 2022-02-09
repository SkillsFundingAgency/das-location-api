using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IPostcodeOutcodeRepository
    {
        Task<PostcodeOutcode> GetOutcode(string outcode);
    }
}