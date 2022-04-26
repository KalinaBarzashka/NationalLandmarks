namespace NationalLandmarks.Server.Features.Landmark
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Features.Visit;
    using NationalLandmarks.Server.Infrastructure.Services;
    using System.Collections.Generic;

    public class LandmarkService : ILandmarkService
    {
        private readonly NationalLandmarksDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public LandmarkService(NationalLandmarksDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<int> Create(CreateLandmarkRequestModel model, string userId)
        {
            var landmark = new Landmark
            {
                Name = model.Name,
                IsNationalLandmark = model.IsNationalLandmark,
                Description = model.Description,
                TownId = model.TownId,
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Opens = model.Opens,
                Closes = model.Closes,
                WorksOnWeekends = model.WorksOnWeekends,
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

        public async Task<IEnumerable<GetAllLandmarksServiceModel>> GetAll()
        {
            return 
                await this.dbContext
                .Landmarks
                .Select(l => new GetAllLandmarksServiceModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    IsNationalLandmark = l.IsNationalLandmark,
                    TownId = l.TownId,
                    TownName = l.Town.Name,
                    ImageUrl = l.ImageUrl,
                    Description = l.Description
                }).ToListAsync();
        }

        public async Task<LandmarkDetailsServiceModel> GetDetailsById(int id)
        {
            var userId = this.currentUserService.GetId();

            return
                await this.dbContext
                .Landmarks
                .Include(l => l.Visits)
                .Where(l => l.Id == id)
                .Select(l => new LandmarkDetailsServiceModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    IsNationalLandmark = l.IsNationalLandmark,
                    Description = l.Description,
                    TownId = l.TownId,
                    TownName = l.Town.Name,
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
                    UserName = l.User.UserName,
                    VisitsCount = l.Visits.Where(v => v.UserId == userId && v.LandmarkId == l.Id).Count(),
                    TotalVisits = l.Visits.Count(),
                    Grades = l.Visits.Where(v => v.LandmarkId == l.Id).Select(g => (int?)(g.Grade))
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Result> Update(int id, UpdateLandmarkRequestModel model, string userId)
        {
            var landmark = await this.GetLandmarkByIdAndUserId(id, userId);

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

        public async Task<Result> Delete(int id, string userId)
        {
            var landmark = await this.GetLandmarkByIdAndUserId(id, userId);

            if (landmark == null)
            {
                return "This user cannot delete this landmark!";
            }

            this.dbContext.Landmarks.Remove(landmark);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<Landmark> GetLandmarkByIdAndUserId(int id, string userId)
        {
            return 
                await this.dbContext
               .Landmarks
               .Where(l => l.Id == id && l.UserId == userId)
               .FirstOrDefaultAsync();
        }
    }
}
