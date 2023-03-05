using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository _difficultyRepository, IMapper mapper)
        {
            difficultyRepository = _difficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //Mivmartot repository-s
            var _walkDifficulties = await difficultyRepository.GetAll();

            if (_walkDifficulties == null)
            {
                return NotFound();
            }
            //Conver to DTO
            var _walkDifficultiesDTO = mapper.Map<IEnumerable<Models.DTO.WalkDifficulty>>(_walkDifficulties);


            //Return to client
            return Ok(_walkDifficultiesDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyByIdAsync")]
        public async Task<IActionResult> GetWalkDifficultyByIdAsync(Guid id)
        {
            //FindWalkDifficulty By id with repository
            var _foundWalkDifficulty = await difficultyRepository.GetById(id);

            //Check if null
            if (_foundWalkDifficulty == null)
            {
                return NotFound();
            }

            //convert to DTO
            var _foundWalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(_foundWalkDifficulty);


            //Send back to client
            return Ok(_foundWalkDifficultyDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty(WalkDifficultyAddRequest _request)
        {

            //Convert ADD Request to Domain
            var _walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Id = _request.Id,
                Code = _request.Code
            };

            //Add it to database using repository
            _walkDifficultyDomain = await difficultyRepository.AddWalk(_walkDifficultyDomain);

            //Convert to DTO
            var _walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = _walkDifficultyDomain.Id,
                Code = _walkDifficultyDomain.Code
            };

            //Return result to client
            return CreatedAtAction(nameof(GetWalkDifficultyByIdAsync), new { id = _walkDifficultyDTO.Id }, _walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty(Guid id, WalkDifficultyUpdateRequest _request)
        {
            //Convert Request to domain
            var _walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = _request.Code,
            };

            //Send to repository
            var _walkDifficultyEdited = await difficultyRepository.UpdateWalkDifficulty(id, _walkDifficultyDomain);

            //Check if it is null
            if (_walkDifficultyEdited == null)
            {
                return NotFound();
            }

            //Return to Client
            return Ok(_walkDifficultyDomain);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var _deletedWalkDifficulty = await difficultyRepository.DeleteWalkDifficulty(id);
            if (_deletedWalkDifficulty == null)
            {
                return NotFound();
            }
            return Ok(_deletedWalkDifficulty);
        }
    }
}

