using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository iRegions;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository _iRegions, IMapper _imapper)
        {
            iRegions = _iRegions;
            mapper = _imapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await iRegions.GetAllAsync();


            var _DTOregions = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(_DTOregions);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await iRegions.GetAsync(id);

            var _DTOregion = mapper.Map<Models.DTO.Region>(region);

            if (_DTOregion == null)
            {
                return NotFound();
            }

            return Ok(_DTOregion);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.RegionAddRequest _addRegionRequest)
        {
            //convert DTO request to model
            var region = new Models.Domain.Region()
            {
                
                Code = _addRegionRequest.Code,
                Name = _addRegionRequest.Name,
                Area = _addRegionRequest.Area,
                Lat = _addRegionRequest.Lat,
                Long = _addRegionRequest.Long,
                Population = _addRegionRequest.Population,

            };

            //Send request to repository
            await iRegions.AddRegionAsync(region);

            //back to DTO
            var _regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            return CreatedAtAction(nameof(GetRegionAsync), new {id = _regionDTO.Id}, _regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveRegionAsync(Guid id)
        {
            //get the region
            var region = await iRegions.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var _regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            return Ok(_regionDTO);
        }



        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, Models.DTO.RegionUpdateRequest _regionUpdateRequest)
        {
            // Convert DTO to Domain model
            var _region = new Models.Domain.Region()
            {
                Id = id,
                Code = _regionUpdateRequest.Code,
                Name = _regionUpdateRequest.Name,
                Area = _regionUpdateRequest.Area,
                Lat = _regionUpdateRequest.Lat,
                Long = _regionUpdateRequest.Long,
                Population = _regionUpdateRequest.Population
            };

            //Update region using repository
            _region = await iRegions.UpdateRegionAsync(id, _region);

            //If null then not found
            if(_region == null)
            {
                return NotFound();
            }

            //Convert domain back to DTO
            var _regionDTO = new Models.DTO.Region()
            {
                Id = _region.Id,
                Code = _region.Code,
                Name = _region.Name,
                Area = _region.Area,
                Lat = _region.Lat,
                Long = _region.Long,
                Population = _region.Population
            };

            return Ok(_regionDTO);
        }
    }
}
