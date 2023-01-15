using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Visit
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Visit.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    /// <summary>
    /// CRUD operations for Visit objects
    /// </summary>
    [Produces("application/json")]
    public class VisitController : ApiController
    {
        private readonly IVisitService visitService;
        private readonly ICurrentUserService currentUserService;

        public VisitController(ICurrentUserService currentUserService, IVisitService visitService)
        {
            this.currentUserService = currentUserService;
            this.visitService = visitService;
        }

        /// <summary>
        /// Get all visits from the database by user id.
        /// </summary>
        /// <returns>IEnumerable object models with details of the visited landmarks.</returns>
        /// <response code="200">Returns all visits as objects.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllVisitsByUserServiceModel>> GetAll()
        {
            var userId = this.currentUserService.GetId();

            return await this.visitService.GetAllByUserId(userId);
        }

        /// <summary>
        /// Create new Visit object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result and the id of the newly created Visit.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="409">Returns conflict if user has already visited the landmark.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create(CreateVisitRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var visitId = await this.visitService.AddVisitedLandmarkForUser(model.LandmarkId, model.Grade, model.Comment, userId);

            if(visitId == 0)
            {
                return Conflict("User has already visited the provided landmark!");
            }

            return Created(nameof(this.Create), visitId);
        }
    }
}
