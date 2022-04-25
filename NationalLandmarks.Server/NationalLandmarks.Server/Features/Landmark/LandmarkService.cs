namespace NationalLandmarks.Server.Features.Landmark
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Infrastructure.Services;
    using System.Collections.Generic;

    public class LandmarkService : ILandmarkService
    {
        private readonly NationalLandmarksDbContext dbContext;

        public LandmarkService(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateLandmark(CreateLandmarkRequestModel model, string userId)
        {
            var landmark = new Landmark
            {
                Name = model.Name,
                IsNationalLandmark = model.IsNationalLandmark,
                Description = model.Description,
                Town = model.Town,
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Opens = model.Opens,
                Closes = model.Closes,
                WorksOnWeekends = model.WorksOnWeekends == "true" ? true : false,
                Email = model.Email,
                Phone = model.Phone,
                Website = model.Website,
                //HasSeal = model.HasSeal == "true" ? true : false,
                ImageUrl = model.ImageUrl,
                UserId = userId
            };

            this.dbContext.Add(landmark);
            await this.dbContext.SaveChangesAsync();

            return landmark.Id;
        }

        public async Task<IEnumerable<GetAllLandmarksServiceModel>> GetAllLandmarks()
        {
            return 
                await this.dbContext
                .Landmarks
                .Select(l => new GetAllLandmarksServiceModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    IsNationalLandmark = l.IsNationalLandmark,
                    Town = l.Town,
                    ImageUrl = l.ImageUrl
                }).ToListAsync();
        }

        public async Task<LandmarkDetailsServiceModel> GetLandmarkDetailsById(int id)
        {
            return
                await this.dbContext
                .Landmarks
                .Where(l => l.Id == id)
                .Select(l => new LandmarkDetailsServiceModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    IsNationalLandmark = l.IsNationalLandmark,
                    Description = l.Description,
                    Town = l.Town,
                    Address = l.Address,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Opens = l.Opens,
                    Closes = l.Closes,
                    WorksOnWeekends = l.WorksOnWeekends,
                    Email = l.Email,
                    Phone = l.Phone,
                    Website = l.Website,
                    ImageUrl = l.ImageUrl,
                    UserId = l.UserId,
                    UserName = l.User.UserName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Result> UpdateLandmark(int id, UpdateLandmarkRequestModel model, string userId)
        {
            var landmark = await this.GetlandmarkByIdAndUserId(id, userId);

            if (landmark == null)
            {
                return "This user cannot edit this landmark!";
            }

            landmark.IsNationalLandmark = model.IsNationalLandmark;
            landmark.Description = model.Description;
            landmark.Opens = model.Opens;
            landmark.Closes = model.Closes;
            landmark.WorksOnWeekends = model.WorksOnWeekends;
            landmark.Email = model.Email;   
            landmark.Phone = model.Phone;
            landmark.Website = model.Website;
            landmark.ImageUrl = model.ImageUrl;

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Result> DeleteLandmark(int id, string userId)
        {
            var landmark = await this.GetlandmarkByIdAndUserId(id, userId);

            if (landmark == null)
            {
                return "This user cannot delete this landmark!";
            }

            this.dbContext.Landmarks.Remove(landmark);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<Landmark> GetlandmarkByIdAndUserId(int id, string userId)
        {
            return 
                await this.dbContext
               .Landmarks
               .Where(l => l.Id == id && l.UserId == userId)
               .FirstOrDefaultAsync();
        }
    }
}
