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

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await context.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var _regionToDelete = await context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if(_regionToDelete == null)
            {
                return null;
            }
            context.Regions.Remove(_regionToDelete);
            await context.SaveChangesAsync();
            return _regionToDelete;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var _existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (_existingRegion == null)
            {
                return null;
            }
            _existingRegion.Code = region.Code;
            _existingRegion.Name = region.Name;
            _existingRegion.Area = region.Area;
            _existingRegion.Lat = region.Lat;
            _existingRegion.Long = region.Long;
            _existingRegion.Population = region.Population;

            await context.SaveChangesAsync();
            return _existingRegion;
        }
    }
}
