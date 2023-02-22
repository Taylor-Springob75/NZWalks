using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        
        

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fetch the data from the database
            var walks = await walkRepository.GetAllAsync();

            // Convert domain to a DTO
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            // Return OK response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // First get the data from the database
            var walks = await walkRepository.GetAsync(id);

            // If null retrun NotFound
            if (walks == null)
            {
                return NotFound();
            }

            // Convert data back to a dto
            var walksDTO = mapper.Map<Models.DTO.Walk>(walks);

            // Return the OK response
            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Convert DTO to domain object
            var walk = new Models.Domain.Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            // Pass the domain object to the repository
            walk = await walkRepository.AddAsync(walk);

            // Then, convert the domain object back to a DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            // Send the DTO response back
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, 
            [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to Domain object
            var walkDomain = new Models.Domain.Walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Pass details to Repository - Get Domain object in response (or null)
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            // Handle null case (not found)
            if (walkDomain == null)
            {
                return NotFound();
            }

            // If found convert Domain back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId

            };

            // Return the response
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // Call the repository to delete the walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            // If null then return not found
            if (walkDomain == null)
            {
                return NotFound();
            }

            // If the walk was found, convert back to DTO
           var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

           return Ok(walkDTO);
        }
    }
}
