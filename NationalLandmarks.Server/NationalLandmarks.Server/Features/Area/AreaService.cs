namespace NationalLandmarks.Server.Features.Area
{
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Features.Area.Models;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public class AreaService : IAreaService
    {
        private readonly NationalLandmarksDbContext dbContext;

        public AreaService(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllAreasServiceModel>> GetAll()
        {
            return await this.dbContext.Areas.Select(x => new GetAllAreasServiceModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<bool> DoesAreaExists(int id)
        {
            var area = await this.dbContext
                .Areas
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return area != null;
        }

        public async Task<int> Create(CreateAreaRequestModel model, string? userId)
        {
            var area = new Area
            {
                Name = model.Name
            };

            this.dbContext.Areas.Add(area);
            await this.dbContext.SaveChangesAsync();

            return area.Id;
        }

        public async Task<Result> Update(int id, string name, string? userId)
        {
            var area = await this.dbContext
                .Areas
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (area == null)
            {
                return "Area does not exists!";
            }

            if(area.Name != name && !string.IsNullOrWhiteSpace(name))
            {
                area.Name = name;
            }

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Result> Delete(int id, string? userId)
        {
            var area = await this.dbContext
                .Areas
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (area == null)
            {
                return "Area does not exists!";
            }

            this.dbContext.Areas.Remove(area);

            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
