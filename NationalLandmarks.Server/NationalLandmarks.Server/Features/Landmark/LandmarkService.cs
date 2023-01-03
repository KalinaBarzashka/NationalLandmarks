namespace NationalLandmarks.Server.Features.Landmark
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Features.Town;
    using NationalLandmarks.Server.Infrastructure.Services;

    public class LandmarkService : ILandmarkService
    {
        private readonly NationalLandmarksDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly ITownService townService;

        public LandmarkService(NationalLandmarksDbContext dbContext, ICurrentUserService currentUserService, ITownService townService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.townService = townService;
        }

        public async Task<int> Create(CreateLandmarkRequestModel model, string userId)
        {
            // check if town exists (without it if non existing id is passed it result in server error 500)

            var townExists = await this.townService.CheckIfIdExists(model.TownId);
            if (townExists.Failure)
            {
                return 0;
            }

            // create landmark
            var landmark = new Landmark
            {
                RegistrationNumber = model.RegistrationNumber,
                Name = model.Name,
                IsNationalLandmark = model.IsNationalLandmark,
                Description = model.Description,
                TownId = model.TownId,
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                WorkingTime = model.WorkingTime,
                //Opens = model.Opens,
                //Closes = model.Closes,
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

        public async Task<GetAllLandmarksPaginationServiceModel> GetAll(int pageNumber, int itemsPerPage)
        {
            var landmarksData = await this.dbContext
                .Landmarks
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(l => new GetAllLandmarksServiceModel
                {
                    Id = l.Id,
                    RegistrationNumber = l.RegistrationNumber,
                    Name = l.Name,
                    IsNationalLandmark = l.IsNationalLandmark,
                    TownId = l.TownId,
                    TownName = l.Town.Name,
                    ImageUrl = l.ImageUrl,
                    Description = l.Description,
                    UserId = l.UserId
                }).ToListAsync();

            var landmarks = new GetAllLandmarksPaginationServiceModel
            {
                Landmarks = landmarksData,
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                TotalItemsCount = GetAllLandmarksCount()
            };

            return landmarks;
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
                    RegistrationNumber = l.RegistrationNumber,
                    Name = l.Name,
                    IsNationalLandmark = l.IsNationalLandmark,
                    Description = l.Description,
                    TownId = l.TownId,
                    TownName = l.Town.Name,
                    Address = l.Address,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    WorkingTime = l.WorkingTime,
                    //Opens = l.Opens,
                    //Closes = l.Closes,
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

            landmark.RegistrationNumber = model.RegistrationNumber;
            landmark.IsNationalLandmark = model.IsNationalLandmark;
            landmark.Description = model.Description;
            landmark.WorkingTime = model.WorkingTime;
            //landmark.Opens = model.Opens;
            //landmark.Closes = model.Closes;
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

        private int GetAllLandmarksCount()
        {
            return this.dbContext.Landmarks.Select(x => x.Id).Count();
        }
    }
}
