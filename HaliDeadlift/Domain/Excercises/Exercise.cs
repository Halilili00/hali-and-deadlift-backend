namespace HalilDeadlift.Domain.Excercises
{
    public class Exercise : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Difficulty Difficulty { get; set; }
        public ExerciseType  ExerciseType { get; set; }
        public string? SourceUrl { get; set; }

        public Guid? EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }

        public ICollection<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; } = new List<ExerciseMuscleGroup>();
    }
}
