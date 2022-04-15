namespace NationalLandmarks.Server.Features
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Infrastructure;
    using NationalLandmarks.Server.Models.Landmarks;
    using System.Security.Claims;

    public class LandmarkController: ApiController
    {
        private readonly NationalLandmarksDbContext dbContext;

        public LandmarkController(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateLandmarkRequestModel model)
        {
            //string username = this.User.Identity.Name;
            string userId = this.User.GetId();

            var landmark = new Landmark
            {
                Name = model.Name,
                IsNationalLandmark = model.IsNationalLandmark == "true" ? true : false,
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
                HasSeal = model.HasSeal == "true" ? true : false,
                ImageUrl = model.ImageUrl,
                UserId = userId
            };

            this.dbContext.Add(landmark);
            await this.dbContext.SaveChangesAsync();


            return Created(nameof(this.Create), landmark.Id);
        }
    }
}
