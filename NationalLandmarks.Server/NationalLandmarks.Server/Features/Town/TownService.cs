namespace NationalLandmarks.Server.Features.Town
{
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Town.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public class TownService : ITownService
    {
        private readonly NationalLandmarksDbContext dbContext;

        public TownService(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllTownsServiceModel>> GetAll()
        {
            return await this.dbContext
                .Towns
                .Select(t => new GetAllTownsServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaName = t.Area.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetAllTownsServiceModel>> GetTownsInSpecificArea(int id)
        {
            return await this.dbContext
                .Towns
                .Where(t => t.AreaId == id)
                .Select(t => new GetAllTownsServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaName = t.Area.Name
                })
                .ToListAsync();
        }

        public async Task<int> Create(CreateTownRequestModel model, string? userId)
        {
            var town = new Town
            {
                Name = model.Name,
                AreaId = model.AreaId
            };

            this.dbContext.Towns.Add(town);

            await this.dbContext.SaveChangesAsync();
            return town.Id;
        }

        public async Task<Result> Update(int id, UpdateTownRequestModel model, string? userId)
        {
            var town = await this.dbContext
                .Towns
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if(town == null)
            {
                return "Town does not exists!";
            }

            if(town.Name != model.Name && !string.IsNullOrWhiteSpace(model.Name))
            {
                town.Name = model.Name;
            }

            if (town.AreaId != model.AreaId && model.AreaId != 0)
            {
                town.AreaId = model.AreaId;
            }

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Result> Delete(int id, string? userId)
        {
            var town = await this.dbContext
                .Towns
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (town == null)
            {
                return "Town does not exists!";
            }

            this.dbContext.Towns.Remove(town);

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DoesTownExists(int id)
        {
            var town = await this.dbContext
                .Towns
                .Where(a => a.Id == id)
                .CountAsync();

            return town != 0;
        }
    }
}
