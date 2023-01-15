namespace NationalLandmarks.Server.Features.Place
{
    using NationalLandmarks.Server.Features.Place.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public interface IPlaceService
    {
        Task<IEnumerable<GetAllPlacesServiceModel>> GetAll();

        Task<IEnumerable<GetAllPlacesServiceModel>> GetPlacesInSpecificArea(int id);

        Task<bool> DoesPlaceExists(int id);

        Task<int> Create(CreatePlaceRequestModel model, string? userId);

        Task<Result> Update(int id, UpdatePlaceRequestModel model, string? userId);

        Task<Result> Delete(int id, string? userId);
    }
}
