using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Town
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Town.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    /// <summary>
    /// CRUD operations for Town objects
    /// </summary>
    [Produces("application/json")]
    public class TownController : ApiController
    {
        private readonly ITownService townService;
        private readonly ICurrentUserService currentUserService;

        public TownController(ITownService townService, ICurrentUserService currentUserService)
        {
            this.townService = townService;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Get all towns from the database.
        /// </summary>
        /// <returns>IEnumerable object models with id, name and area name params.</returns>
        /// <response code="200">Returns all towns as objects.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllTownsServiceModel>> GetAll()
        {
            return await this.townService.GetAll();
        }

        /// <summary>
        /// Get all towns in a specific area from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumerable object models with id, name and area name params.</returns>
        /// <response code="200">Returns all towns as objects.</response>
        [HttpGet]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllTownsServiceModel>> GetTownsInSpecificArea(int id)
        {
            return await this.townService.GetTownsInSpecificArea(id);
        }

        /// <summary>
        /// Create new Town object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result and the id of the newly created Town.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Town
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
        public async Task<ActionResult> Create(CreateTownRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            int townId = await this.townService.Create(model, userId);

            return Created(nameof(this.Create), townId);
        }

        /// <summary>
        /// Update specific Town object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the update was successfull.</response>
        /// <response code="400">Returns bad request if update fails.</response>
        /// <response code="404">Returns not found if town with the specified id does not exists.</response>
        [HttpPut]
        [Authorize]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateTownRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.townService.DoesTownExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.townService.Update(id, model, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Delete specific Town object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the delete was successfull.</response>
        /// <response code="400">Returns bad request if delete fails.</response>
        /// <response code="404">Returns not found if town with the specified id does not exists.</response>
        [Authorize]
        [HttpDelete]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.townService.DoesTownExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.townService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
