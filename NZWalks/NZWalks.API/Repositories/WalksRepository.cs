using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext _context;
        public WalksRepository(NZWalksDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {

            walk.Id = Guid.NewGuid();
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var _walkToDelete = await _context.Walks.FindAsync(id);
            if (_walkToDelete == null)
            {
                return null;
            }
            //Delete
            _context.Walks.Remove(_walkToDelete);
            await _context.SaveChangesAsync();
            return _walkToDelete;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await
                _context.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            var _walk = await _context.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).FirstOrDefaultAsync(x => x.Id == id);
            return _walk;
        }

        public async Task<Walk> UpdateWalkAsync(Walk walk, Guid walkId)
        {
            //Find which walk to update
            var _walkToUpdate = await _context.Walks.FirstOrDefaultAsync(walk => walk.Id == walkId);

            //Check if null
            if (_walkToUpdate != null)
            {
                //Edit it
                _walkToUpdate.Name = walk.Name;
                _walkToUpdate.Length = walk.Length;
                _walkToUpdate.RegionId = walk.RegionId;
                _walkToUpdate.WalkDifficultyId = walk.WalkDifficultyId;

                //Save to database
                await _context.SaveChangesAsync();

                //Return response
                return _walkToUpdate;
            }

            return null;

        }
    }
}
