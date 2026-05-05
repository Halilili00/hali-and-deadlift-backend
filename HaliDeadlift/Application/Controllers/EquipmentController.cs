using HaliDeadlift.Application.Actions.EquipmentActions.GetEquipments;
using Microsoft.AspNetCore.Mvc;

namespace HaliDeadlift.Application.Controllers
{
    public class EquipmentController : ApiController
    {
        [HttpGet(Name = "GetEquipments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<GetEquipmentsResponse>))]
        public async Task<IActionResult> GetEquipments()
        {
            return new OkObjectResult(await Mediator.Send(new GetEquipmentsQuery()));
        }
    }
}
