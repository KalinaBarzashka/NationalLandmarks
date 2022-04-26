namespace NationalLandmarks.Server.Features.Area
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NationalLandmarks.Server.Features.Area.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    public class AreaController : ApiController
    {
        private readonly IAreaService areaService;
        private readonly ICurrentUserService currentUserService;

        public AreaController(IAreaService areaService, ICurrentUserService currentUserService)
        {
            this.areaService = areaService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllAreasServiceModel>> GetAllAreas()
        {
            return await this.areaService.GetAll();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateAreaRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            int areaId = await this.areaService.Create(model, userId);

            return Created(nameof(this.Create), areaId);
        }

        [HttpPut]
        [Authorize]
        [Route(RouteId)]
        public async Task<ActionResult> Update(int id, string name)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.areaService.Update(id, name, userId);

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

            var result = await this.areaService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
