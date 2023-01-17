namespace NationalLandmarks.Server.Features.Place
{
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Place.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public class PlaceService : IPlaceService
    {
        private readonly NationalLandmarksDbContext dbContext;

        public PlaceService(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllPlacesServiceModel>> GetAll()
        {
            return await this.dbContext
                .Places
                .Select(t => new GetAllPlacesServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaName = t.Area.Name,
                    AreaId = t.AreaId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetAllPlacesServiceModel>> GetPlacesInSpecificArea(int id)
        {
            return await this.dbContext
                .Places
                .Where(t => t.AreaId == id)
                .Select(t => new GetAllPlacesServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaName = t.Area.Name,
                    AreaId = t.AreaId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetAllPlacesServiceModel>> FindPlacesByName(string placeName)
        {
            return await this.dbContext
                .Places
                .Where(p => p.Name.Contains(placeName))
                .Select(t => new GetAllPlacesServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    AreaName = t.Area.Name,
                    AreaId = t.AreaId
                })
                .ToListAsync();
        }

        public async Task<int> Create(CreatePlaceRequestModel model, string? userId)
        {
            var place = new Place
            {
                Name = model.Name,
                AreaId = model.AreaId
            };

            this.dbContext.Places.Add(place);

            await this.dbContext.SaveChangesAsync();
            return place.Id;
        }

        public async Task<Result> Update(int id, UpdatePlaceRequestModel model, string? userId)
        {
            var place = await this.dbContext
                .Places
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if(place == null)
            {
                return "Place does not exists!";
            }

            if(place.Name != model.Name && !string.IsNullOrWhiteSpace(model.Name))
            {
                place.Name = model.Name;
            }

            if (place.AreaId != model.AreaId && model.AreaId != 0)
            {
                place.AreaId = model.AreaId;
            }

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Result> Delete(int id, string? userId)
        {
            var place = await this.dbContext
                .Places
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (place == null)
            {
                return "Place does not exists!";
            }

            this.dbContext.Places.Remove(place);

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DoesPlaceExists(int id)
        {
            var place = await this.dbContext
                .Places
                .Where(a => a.Id == id)
                .CountAsync();

            return place != 0;
        }
    }
}
