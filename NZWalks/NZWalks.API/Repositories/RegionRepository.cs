using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext context;

        public RegionRepository(NZWalksDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }
    }
}
