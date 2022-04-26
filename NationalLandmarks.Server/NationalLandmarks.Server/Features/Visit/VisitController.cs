namespace NationalLandmarks.Server.Features.Visit
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Visit.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    public class VisitController : ApiController
    {
        private readonly IVisitService visitService;
        private readonly ICurrentUserService currentUserService;

        public VisitController(ICurrentUserService currentUserService, IVisitService visitService)
        {
            this.currentUserService = currentUserService;
            this.visitService = visitService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<GetAllVisitsByUserServiceModel>> GetAll()
        {
            var userId = this.currentUserService.GetId();

            return await this.visitService.GetAllByUserId(userId);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateVisitRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var visitId = await this.visitService.AddVisitedLandmarkForUser(model.LandmarkId, model.Grade, userId);

            if(visitId == 0)
            {
                return Conflict("User has already visited landmark!");
            }

            return Created(nameof(this.Create), visitId);
        }
    }
}
