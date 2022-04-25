namespace NationalLandmarks.Server.Features.Landmark
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Infrastructure.Extensions;
    using NationalLandmarks.Server.Infrastructure.Services;
    using static Infrastructure.WebConstants;

    public class LandmarkController : ApiController
    {
        private readonly ILandmarkService landmarkService;
        private readonly ICurrentUserService currentUserService;

        public LandmarkController(
            ILandmarkService landmarkService,
            ICurrentUserService currentUserService)
        {
            this.landmarkService = landmarkService;
            this.currentUserService = currentUserService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<GetAllLandmarksServiceModel>> GetAllLandmarks()
        {
            return await this.landmarkService.GetAllLandmarks();
        }

        //[Authorize]
        [HttpGet]
        [Route(RouteId)]
        public async Task<ActionResult<LandmarkDetailsServiceModel>> Details(int id)
        {
            var landmark = await this.landmarkService.GetLandmarkDetailsById(id);

            return landmark;//landmark.OrNotFound();
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateLandmarkRequestModel model)
        {
            //string username = this.User.Identity.Name;
            var userId = this.currentUserService.GetId();//this.User.GetId();

            int landmarkId = await this.landmarkService.CreateLandmark(model, userId);

            return Created(nameof(this.Create), landmarkId);
        }

        [Authorize]
        [HttpPut]
        [Route(RouteId)]
        public async Task<ActionResult> Update(int id, UpdateLandmarkRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.landmarkService.UpdateLandmark(id, model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route(RouteId)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.landmarkService.DeleteLandmark(id, userId);

            if(result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
