using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksRepository iwalksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository _iWalksRepository, IMapper _mapper)
        {
            iwalksRepository = _iWalksRepository;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalkAsync()
        {
            //Get data domain from database
            var _walksDomain = await iwalksRepository.GetAllAsync();

            //Conert to DTO
            var _walksDTO = mapper.Map<List<Models.DTO.Walk>>(_walksDomain);

            //Send back to user
            return Ok(_walksDTO);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            //Get domain object from database
            var _walk = await iwalksRepository.GetAsync(id);

            //Check if it is null
            if (_walk == null)
            {
                return NotFound();
            }

            //Convert it to DTO
            var _walkDTO  = mapper.Map<Models.DTO.Walk>(_walk);

            //Return it
            return Ok(_walkDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkSync([FromBody] Models.DTO.WalkAddRequest _walkRequest)
        {
            //Conver to domain
            var _walkDomain = new Models.Domain.Walk()
            {
                Name = _walkRequest.Name,
                Length = _walkRequest.Length,
                RegionId = _walkRequest.RegionId,
                WalkDifficultyId = _walkRequest.WalkDifficultyId,
            };

            //Add it to server through repository
            _walkDomain= await iwalksRepository.AddWalkAsync(_walkDomain);


            //Back to DTO
            var _walkDTO = new Models.DTO.Walk()
            {
                Id = _walkDomain.Id,
                Name = _walkDomain.Name,
                Length = _walkDomain.Length,
                RegionId = _walkDomain.RegionId,
                WalkDifficultyId = _walkDomain.WalkDifficultyId,
            };


            //Send data back to client
            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = _walkDTO.Id }, _walkDTO);

        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Models.DTO.WalkUpdateRequest _updateRequest, Guid id)
        {
            //Convert it to Domain
            var _walksDomain = new Models.Domain.Walk()
            {
                Name = _updateRequest.Name,
                Length = _updateRequest.Length,
                RegionId = _updateRequest.RegionId,
                WalkDifficultyId = _updateRequest.WalkDifficultyId,
            };

            //Send it to repository
             _walksDomain = await iwalksRepository.UpdateWalkAsync(_walksDomain, id);

            //Check if it is null
            if(_walksDomain == null)
            {
                return NotFound();
            }

            //Convert to dto
            var _walksDTO = new Models.DTO.Walk()
            {
                Id = _walksDomain.Id,
                Name = _walksDomain.Name,
                Length = _walksDomain.Length,
                RegionId = _walksDomain.RegionId,
                WalkDifficultyId = _walksDomain.WalkDifficultyId,
            };

            //Send back to user
            return Ok(_walksDomain);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkByIdAsync(Guid id)
        {
            //go to irepository , it returns domain object
            var _walkToDelete = await iwalksRepository.DeleteWalkAsync(id);
            if(_walkToDelete == null)
            {
                return NotFound();
            }
            //Convert it to DTO
            var _walkToDeleteDto = mapper.Map<Models.DTO.Walk>(_walkToDelete);
            return Ok(_walkToDeleteDto);
        }
    }
}
