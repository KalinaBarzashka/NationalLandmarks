namespace NationalLandmarks.Server.Features.Town
{
    using NationalLandmarks.Server.Features.Town.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public interface ITownService
    {
        Task<IEnumerable<GetAllTownsServiceModel>> GetAll();

        Task<int> Create(CreateTownRequestModel model, string? userId);

        Task<Result> Update(int id, UpdateTownRequestModel model, string? userId);

        Task<Result> Delete(int id, string? userId);
    }
}
