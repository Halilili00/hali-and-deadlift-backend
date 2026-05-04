using HalilDeadlift.Domain.Excercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HalilDeadlift.Infrastructure.Configurations
{
    public class ExerciseMuscleGroupConfiguration : IEntityTypeConfiguration<ExerciseMuscleGroup>
    {
        public void Configure(EntityTypeBuilder<ExerciseMuscleGroup> builder)
        {
            builder.HasKey(emg => new { emg.MuscleGroupId, emg.ExerciseId });

            builder.HasOne(emg => emg.Exercise)
                .WithMany(e => e.ExerciseMuscleGroups)
                .HasForeignKey(emg => emg.ExerciseId);

            builder.HasOne(emg => emg.MuscleGroup)
                .WithMany(mg => mg.ExerciseMuscleGroups)
                .HasForeignKey(emg => emg.MuscleGroupId);
        }
    }
}
