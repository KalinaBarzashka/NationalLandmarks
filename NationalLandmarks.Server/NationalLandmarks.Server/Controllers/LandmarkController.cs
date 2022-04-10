namespace NationalLandmarks.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Models.Landmarks;

    public class LandmarkController: ApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateLandmarkRequestModel model)
        {
            return 0;
        }
    }
}
