using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {

        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddWalkAsync(Walk walk);
        Task<Walk> UpdateWalkAsync(Walk walk, Guid walkId);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
