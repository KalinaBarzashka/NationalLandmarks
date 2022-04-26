namespace NationalLandmarks.Server.Features.Town
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Town.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    public class TownController : ApiController
    {
        private readonly ITownService townService;
        private readonly ICurrentUserService currentUserService;

        public TownController(ITownService townService, ICurrentUserService currentUserService)
        {
            this.townService = townService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllTownsServiceModel>> GetAll()
        {
            return await this.townService.GetAll();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateTownRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            int townId = await this.townService.Create(model, userId);

            return Created(nameof(this.Create), townId);
        }

        [Authorize]
        [HttpPut]
        [Route(RouteId)]
        public async Task<ActionResult> Update(int id, UpdateTownRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.townService.Update(id, model, userId);

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

            var result = await this.townService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
