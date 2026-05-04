using HaliDeadlift.Application.Actions.ExerciseActions.GetExercises;
using Microsoft.AspNetCore.Mvc;

namespace HaliDeadlift.Application.Controllers
{
    public class ExerciseController : ApiController
    {
        [HttpGet(Name = "GetExercises")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<GetExercisesResponse>))]
        public async Task<IActionResult> GetExercises()
        {
            return new OkObjectResult(await Mediator.Send(new GetExercisesQuery()));
        }
    }
}
