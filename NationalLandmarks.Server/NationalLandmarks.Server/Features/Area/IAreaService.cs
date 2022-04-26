namespace NationalLandmarks.Server.Features.Area
{
    using NationalLandmarks.Server.Features.Area.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public interface IAreaService
    {
        Task<IEnumerable<GetAllAreasServiceModel>> GetAll();

        Task<int> Create(CreateAreaRequestModel model, string? userId);

        Task<Result> Update(int id, string name, string? userId);

        Task<Result> Delete(int id, string? userId);
    }
}
