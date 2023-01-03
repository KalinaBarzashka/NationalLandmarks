namespace NationalLandmarks.Server.Features.Landmark
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    public class LandmarkController : ApiController
    {
        private const int itemsPerPage = 9;
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
        [Route(RouteId)]
        public async Task<GetAllLandmarksPaginationServiceModel> GetAll(int id = 1)
        {
            //id is page number
            return await this.landmarkService.GetAll(id, itemsPerPage);
        }

        //[Authorize]
        [HttpGet]
        [Route("details/" + RouteId)]
        public async Task<ActionResult<LandmarkDetailsServiceModel>> Details(int id)
        {
            var landmark = await this.landmarkService.GetDetailsById(id);

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

            int landmarkId = await this.landmarkService.Create(model, userId);

            if (landmarkId == 0)
            {
                return BadRequest("Town not found!");
            }

            return Created(nameof(this.Create), landmarkId);
        }

        [Authorize]
        [HttpPut]
        [Route(RouteId)]
        public async Task<ActionResult> Update(int id, UpdateLandmarkRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.landmarkService.Update(id, model, userId);

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

            var result = await this.landmarkService.Delete(id, userId);

            if(result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
