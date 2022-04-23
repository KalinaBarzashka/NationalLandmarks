namespace NationalLandmarks.Server.Features.Landmark
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Infrastructure;
    using NationalLandmarks.Server.Infrastructure.Extensions;

    public class LandmarkController : ApiController
    {
        private readonly NationalLandmarksDbContext dbContext;
        private readonly ILandmarkService landmarkService;

        public LandmarkController(
            NationalLandmarksDbContext dbContext,
            ILandmarkService landmarkService)
        {
            this.dbContext = dbContext;
            this.landmarkService = landmarkService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<GetAllLandmarksServiceModel>> GetAllLandmarks()
        {
            return await this.landmarkService.GetAllLandmarks();
        }

        //[Authorize]
        [HttpGet]
        [Route("{id}")]
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
            string userId = this.User.GetId();

            int landmarkId = await this.landmarkService.CreateLandmark(model, userId);

            return Created(nameof(this.Create), landmarkId);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update(int id)
        {

        }
    }
}
