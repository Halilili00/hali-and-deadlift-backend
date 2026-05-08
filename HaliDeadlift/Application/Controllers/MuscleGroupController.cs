using HaliDeadlift.Application.Actions.MuscleGroupActions.GetMuscleGroups;
using Microsoft.AspNetCore.Mvc;

namespace HaliDeadlift.Application.Controllers
{
    public class MuscleGroupController : ApiController
    {
        [HttpGet(Name = "GetMuscleGroups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<GetMuscleGroupsResponse>))]
        public async Task<IActionResult> GetMuscleGroups()
        {
            return new OkObjectResult(await Mediator.Send(new GetMuscleGroupsQuery()));
        }
    }
}
