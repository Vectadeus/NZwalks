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
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await iRegions.GetAllAsync();

            //var DTOregions = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(regions =>
            //{

            //    var DTOregion = new Models.DTO.Region()
            //    {
            //        Id = regions.Id,
            //        Code = regions.Code,
            //        Name = regions.Name,
            //        Area = regions.Area,
            //        Lat = regions.Lat,
            //        Long = regions.Long,
            //        Population = regions.Population,
            //        Walks = regions.Walks
            //    };
            //    DTOregions.Add(DTOregion);
            //});



            var _DTOregions = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(_DTOregions);
        }
    }
}
