using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAll();
        Task<WalkDifficulty> GetById(Guid id);
        Task<WalkDifficulty> AddWalk(WalkDifficulty difficulty);
        Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty difficulty);
        Task<WalkDifficulty> DeleteWalkDifficulty(Guid id);
    }
}
