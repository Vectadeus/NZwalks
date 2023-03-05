using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext context;

        public WalkDifficultyRepository(NZWalksDbContext _context)
        {
            context = _context;
        }

        public async Task<WalkDifficulty> AddWalk(WalkDifficulty difficulty)
        {
            difficulty.Id = Guid.NewGuid();
            await context.WalkDifficulty.AddRangeAsync(difficulty);
            await context.SaveChangesAsync();
            return difficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficulty(Guid id)
        {
            //Find which to delete
            var _walkDifficultyToDelete = await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            //Check if it is found
            if (_walkDifficultyToDelete == null)
            {
                return null;
            }

            //delete it
            context.WalkDifficulty.Remove(_walkDifficultyToDelete);

            //Save data
            context.SaveChanges();

            //return result
            return _walkDifficultyToDelete;


        }

        public async Task<IEnumerable<WalkDifficulty>> GetAll()
        {
            var _walkDifficulties = await context.WalkDifficulty.ToListAsync();
            return _walkDifficulties;

        }

        public async Task<WalkDifficulty> GetById(Guid id)
        {
            var _walkDifficultyFound = await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (_walkDifficultyFound == null)
            {
                return null;
            }
            return _walkDifficultyFound;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty difficulty)
        {
            //Find in database using ID
            var _walkDifficulty = await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            //If not null
            if (_walkDifficulty == null)
            {
                return null;
            }

            //Change it
            _walkDifficulty.Code = difficulty.Code;

            //Save it
            await context.SaveChangesAsync();

            //Return it to controller
            return _walkDifficulty;
        }
    }
}
