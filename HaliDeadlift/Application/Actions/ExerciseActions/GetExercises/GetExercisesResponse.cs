using HalilDeadlift.Domain;

namespace HaliDeadlift.Application.Actions.ExerciseActions.GetExercises
{
    public class GetExercisesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Difficulty Difficulty { get; set; }
        public ExerciseType ExerciseType { get; set; }

        public ICollection<ExerciseMuscleGroupDto> ExerciseMuscleGroups { get; set; } = new List<ExerciseMuscleGroupDto>();
    }
}
