namespace NationalLandmarks.Server.Features.Landmark
{
    using Data.Models;
    using NationalLandmarks.Server.Data;

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
    }
}
