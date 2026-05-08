namespace HalilDeadlift.Domain.Excercises
{
    public class ExerciseMuscleGroup
    {
        public Guid ExerciseId { get; set; }
        public Guid MuscleGroupId { get; set; }
        public MuscleRole MuscleRole { get; set; }
        public string? MuscleName => MuscleGroup?.Name;

        public Exercise? Exercise { get; set; }
        public MuscleGroup? MuscleGroup { get; set; }
    }
}
