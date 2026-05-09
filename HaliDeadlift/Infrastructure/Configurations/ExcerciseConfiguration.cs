using HalilDeadlift.Domain.Excercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HalilDeadlift.Infrastructure.Configurations
{
    public class ExcerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasIndex(e => e.Name).IsUnique();

            builder.HasMany(e => e.ExerciseMuscleGroups)
                   .WithOne(emg => emg.Exercise)
                   .HasForeignKey(emg => emg.ExerciseId);

            builder.HasOne(e => e.Equipment)
                   .WithMany(eq => eq.Exercises)
                   .HasForeignKey(e => e.EquipmentId);
        }
    }
}
