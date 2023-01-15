namespace NationalLandmarks.Server.Features.Visit
{
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Visit.Models;
    using System.Collections.Generic;

    public class VisitService : IVisitService
    {
        private readonly NationalLandmarksDbContext dbContext;

        public VisitService(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddVisitedLandmarkForUser(int landmarkId, int? grade, string? comment, string? userId)
        {
            var visitExists = this.dbContext
                .Visits
                .Where(v => v.LandmarkId == landmarkId && v.UserId == userId)
                .Count();

            if (visitExists > 0)
            {
                return 0;
            }

            var visit = new Visit
            {
                UserId = userId,
                LandmarkId = landmarkId,
                VisitedOn = DateTime.UtcNow,
                Grade = grade != null ? (Grade)grade : 0,
                Comment = comment != null ? comment : ""
            };

            this.dbContext.Add(visit);
            await this.dbContext.SaveChangesAsync();

            return visit.Id;
        }

        public async Task<IEnumerable<GetAllVisitsByUserServiceModel>> GetAllByUserId(string? userId)
        {
            return await this.dbContext
                .Visits
                .Where(v => v.UserId == userId)
                .Select(v => new GetAllVisitsByUserServiceModel
                {
                    Id = v.Id,
                    LandmarkId = v.LandmarkId,
                    Grade = v.Grade,
                    VisitedOn = v.VisitedOn,
                    Comment = v.Comment,
                    Landmark = new VisitedLandmarkServiceModel
                    {
                        Name = v.Landmark.Name,
                        IsNationalLandmark = v.Landmark.IsNationalLandmark,
                        PlaceName = v.Landmark.Place.Name,
                        ImagePath = v.Landmark.ImagePath
                    }
                })
                .OrderByDescending(v => v.VisitedOn)
                .ToListAsync();
        }
    }
}
