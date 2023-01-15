using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Area
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Area.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    /// <summary>
    /// CRUD operations for Area objects
    /// </summary>
    [Produces("application/json")]
    public class AreaController : ApiController
    {
        private readonly IAreaService areaService;
        private readonly ICurrentUserService currentUserService;

        public AreaController(IAreaService areaService, ICurrentUserService currentUserService)
        {
            this.areaService = areaService;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Get all areas from the database.
        /// </summary>
        /// <returns>IEnumerable object models with id and name params.</returns>
        /// <response code="200">Returns all areas as objects.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllAreasServiceModel>> GetAll()
        {
            return await this.areaService.GetAll();
        }

        /// <summary>
        /// Create new Area object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result and the id of the newly created Area.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Area
        ///     {
        ///        "name": "Test area name"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="403">No permissions.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Create(CreateAreaRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            int areaId = await this.areaService.Create(model, userId);

            return Created(nameof(this.Create), areaId);
        }

        /// <summary>
        /// Update specific Area object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the update was successfull.</response>
        /// <response code="400">Returns bad request if update fails.</response>
        /// <response code="403">No permissions.</response>
        /// <response code="404">Returns not found if area with the specified id does not exists.</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, string name)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.areaService.DoesAreaExists(id);
            
            if (!exists)
            {
                return NotFound();
            }

            var result = await this.areaService.Update(id, name, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Delete specific Area object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the delete was successfull.</response>
        /// <response code="400">Returns bad request if delete fails.</response>
        /// <response code="403">No permissions.</response>
        /// <response code="404">Returns not found if area with the specified id does not exists.</response>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var exists = await this.areaService.DoesAreaExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.areaService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
