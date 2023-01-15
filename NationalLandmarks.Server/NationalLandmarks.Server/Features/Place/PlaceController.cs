using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Place
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Place.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    /// <summary>
    /// CRUD operations for Place objects
    /// </summary>
    [Produces("application/json")]
    public class PlaceController : ApiController
    {
        private readonly IPlaceService placeService;
        private readonly ICurrentUserService currentUserService;

        public PlaceController(IPlaceService placeService, ICurrentUserService currentUserService)
        {
            this.placeService = placeService;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Get all places from the database.
        /// </summary>
        /// <returns>IEnumerable object models with id, name and area name params.</returns>
        /// <response code="200">Returns all places as objects.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllPlacesServiceModel>> GetAll()
        {
            return await this.placeService.GetAll();
        }

        /// <summary>
        /// Get all places in a specific area from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable object models with id, name and area name params.</returns>
        /// <response code="200">Returns all places as objects.</response>
        [HttpGet]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllPlacesServiceModel>> GetPlacesInSpecificArea(int id)
        {
            return await this.placeService.GetPlacesInSpecificArea(id);
        }

        /// <summary>
        /// Create new Place object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result and the id of the newly created Place.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Place
        ///     {
        ///        "name": "Test area name",
        ///        "areaId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreatePlaceRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            int placeId = await this.placeService.Create(model, userId);

            return Created(nameof(this.Create), placeId);
        }

        /// <summary>
        /// Update specific Place object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the update was successfull.</response>
        /// <response code="400">Returns bad request if update fails.</response>
        /// <response code="404">Returns not found if place with the specified id does not exists.</response>
        [HttpPut]
        [Authorize]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdatePlaceRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.placeService.DoesPlaceExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.placeService.Update(id, model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Delete specific place object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the delete was successfull.</response>
        /// <response code="400">Returns bad request if delete fails.</response>
        /// <response code="404">Returns not found if place with the specified id does not exists.</response>
        [Authorize]
        [HttpDelete]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.placeService.DoesPlaceExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.placeService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
