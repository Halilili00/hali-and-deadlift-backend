using HalilDeadlift.Domain.Excercises;

namespace HaliDeadlift.Application.Actions.ExerciseActions.GetExercises
{
    public class ExerciseMuscleGroupDto
    {
        public Guid MuscleGroupId { get; set; }
        public string? MuscleName { get; set; }
        public MuscleRole MuscleRole { get; set; }
    }
}
