using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Landmark
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Features.Place;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    /// <summary>
    /// CRUD operations for Landmark objects
    /// </summary>
    [Produces("application/json")]
    public class LandmarkController : ApiController
    {
        private const int itemsPerPage = 9;
        private readonly ILandmarkService landmarkService;
        private readonly ICurrentUserService currentUserService;
        private readonly IPlaceService placeService;

        public LandmarkController(
            ILandmarkService landmarkService,
            ICurrentUserService currentUserService,
            IPlaceService placeService)
        {
            this.landmarkService = landmarkService;
            this.currentUserService = currentUserService;
            this.placeService = placeService;
        }

        //[Authorize]
        /// <summary>
        /// Get all landmarks from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object model with landmark array and pagination information.</returns>
        /// <response code="200">Returns all landmarks as objects.</response>
        [HttpGet]
        [Authorize]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAllLandmarksPaginationServiceModel>> GetAll(int id = 1)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            //id is page number
            return await this.landmarkService.GetAll(id, itemsPerPage);
        }

        //[Authorize]
        /// <summary>
        /// Get landmark details for a specific landmark from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object model with landmark details.</returns>
        /// <response code="200">Returns landmark details as object.</response>
        /// <response code="404">Returns not found if no landmark exists in the database.</response>
        [HttpGet]
        [Authorize]
        [Route("details/" + RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LandmarkDetailsServiceModel>> Details(int id)
        {
            var landmark = await this.landmarkService.GetDetailsById(id);

            if(landmark == null)
            {
                return NotFound();
            }

            return landmark;//landmark.OrNotFound();
        }

        /// <summary>
        /// Create new Landmark object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result and the newly created Landmark.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">Returns bad request if place does not exists in the database.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(CreateLandmarkRequestModel model)
        {
            //string username = this.User.Identity.Name;
            var userId = this.currentUserService.GetId();//this.User.GetId();

            // check if place exists (without it if non existing id is passed it result in server error 500)
            var placeExists = await this.placeService.DoesPlaceExists(model.PlaceId);

            if (!placeExists)
            {
                return BadRequest("place not found!");
            }

            int landmarkId = await this.landmarkService.Create(model, userId);
            //var landmark = await this.landmarkService.GetDetailsById(landmarkId);

            //if (landmarkId == 0)
            //{
            //    return BadRequest("Create failed!");
            //}

            return CreatedAtAction(nameof(Create), landmarkId);
        }

        /// <summary>
        /// Update specific Landmark object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the update was successfull.</response>
        /// <response code="400">Returns bad request if update fails.</response>
        /// <response code="404">Returns not found if landmark with the specified id does not exists.</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateLandmarkRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.landmarkService.DoesLandmarkExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.landmarkService.Update(id, model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Delete specific Landmark object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the delete was successfull.</response>
        /// <response code="400">Returns bad request if delete fails.</response>
        /// <response code="404">Returns not found if landmark with the specified id does not exists.</response>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.landmarkService.DoesLandmarkExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.landmarkService.Delete(id, userId);

            if(result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        //string root = this.webHostEnvironment.WebRootPath;
        //var combinedPath = Path.Combine(root, Path.Combine(new string[] { "images", "test.jpg" }));
        //byte[] imageArray = System.IO.File.ReadAllBytes(combinedPath);
        //string base64ImageRepresentation = Convert.ToBase64String(imageArray);
    }
}
