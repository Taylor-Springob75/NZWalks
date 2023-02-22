using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public WalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Models.Domain.Walk> AddAsync(Models.Domain.Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDBContext.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Models.Domain.Walk> DeleteAsync(Guid id)
        {
            // If walk exists from given id delete, otherwise return null
            var existingWalk = await nZWalksDBContext.Walks.FindAsync(id);

            if(existingWalk == null)
            {
                return null;
            }

            nZWalksDBContext.Walks.Remove(existingWalk);
            await nZWalksDBContext.SaveChangesAsync(true);

            return existingWalk;
        }

        public async Task<IEnumerable<Models.Domain.Walk>> GetAllAsync()
        {
            return await 
                nZWalksDBContext.Walks
                .Include( x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Models.Domain.Walk> GetAsync(Guid id)
        {
            return await
                nZWalksDBContext.Walks
                .Include( x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Models.Domain.Walk> UpdateAsync(Guid id, Models.Domain.Walk walk)
        {
            var existingWalk = await nZWalksDBContext.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                
                existingWalk.Name = walk.Name;
                existingWalk.Length = walk.Length;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId= walk.RegionId;
                await nZWalksDBContext.SaveChangesAsync();

                return existingWalk;
            }

            return null;
        }
    }
}
