using HalilDeadlift.Domain;
using HalilDeadlift.Domain.Excercises;
using HalilDeadlift.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HaliDeadlift.Infrastructure
{
    public static class DbInitializer
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task InitializeDatabaseData(ApplicationDbContext dbContext, ILogger logger)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                logger.LogInformation("Starting database seeding...");

                await InitializeEquipments(dbContext, logger);
                await InitializeMuscleGroups(dbContext, logger);

                // Save parent entities first
                await dbContext.SaveChangesAsync();

                await InitializeExercises(dbContext, logger);

                // Save exercises + relations
                await dbContext.SaveChangesAsync();

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                logger.LogInformation("Database seeding completed successfully.");
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();

                logger.LogError(ex, "Database seeding failed.");

                throw;
            }
            
        }

        public static async Task InitializeEquipments(ApplicationDbContext dbContext, ILogger logger)
        {
            if (await dbContext.Equipments.AnyAsync())
            {
                logger.LogInformation("Equipments already seeded.");
                return;
            }

            var equipments = await LoadJsonAsync<Equipment>("equipments.json");

            if (equipments is null || equipments.Count == 0)
            {
                logger.LogWarning("No equipments found in seed file.");
                return;
            }

            await dbContext.Equipments.AddRangeAsync(equipments);

            logger.LogInformation("Seeded {Count} equipments.", equipments.Count);
        }

        public static async Task InitializeMuscleGroups(ApplicationDbContext dbContext, ILogger logger)
        {
            if (await dbContext.MuscleGroups.AnyAsync())
            {
                logger.LogInformation("Muscle groups already seeded.");
                return;
            }
            var muscleGroups = await LoadJsonAsync<MuscleGroup>("muscle_groups.json");

            if (muscleGroups is null || muscleGroups.Count == 0)
            {
                logger.LogWarning("No muscle groups found in seed file.");
                return;
            }

            await dbContext.MuscleGroups.AddRangeAsync(muscleGroups);

            logger.LogInformation("Seeded {Count} muscle groups.", muscleGroups.Count);
        }

        public static async Task InitializeExercises(ApplicationDbContext dbContext, ILogger logger)
        {
            if (await dbContext.Exercises.AnyAsync())
            {
                logger.LogInformation("Exercises already seeded.");
                return;
            }

            var jsonExercises = await LoadJsonAsync<JsonExercise>("exercises.json");

            if (jsonExercises is null || jsonExercises.Count == 0)
            {
                logger.LogWarning("No exercises found in seed file.");
                return;
            }

            var equipments = await dbContext.Equipments.ToDictionaryAsync(equipment => equipment.Name);
            var muscleGroups = await dbContext.MuscleGroups.ToDictionaryAsync(muscleGroup => muscleGroup.Name);

            var exercises = new List<Exercise>();

            foreach (var item in jsonExercises)
            {
                var exercise = new Exercise
                {
                    Name = item.Name,
                    Description = item.Description,
                    Difficulty = item.Difficulty,
                    ExerciseType = item.ExerciseType,
                    SourceUrl = item.SourceUrl
                };

                // add matchend eqipments from database
                if (!string.IsNullOrWhiteSpace(item.Equipment) && equipments.TryGetValue(item.Equipment, out var equipment))
                {
                    exercise.Equipment = equipment;
                }

                // add matchend musclue groups from database
                foreach (var itemMuscleGroups in item.MuscleGroups)
                {
                    if(muscleGroups.TryGetValue(itemMuscleGroups.Name, out var muscleGroup))
                    {
                        exercise.ExerciseMuscleGroups.Add(
                            new ExerciseMuscleGroup
                            {
                                MuscleGroup = muscleGroup,
                                MuscleRole = itemMuscleGroups.MuscleRole
                            });
                    }
                }

                exercises.Add(exercise);
            }

            await dbContext.Exercises.AddRangeAsync(exercises);

            logger.LogInformation("Seeded {Count} exercises.", exercises.Count);
        }

        private static async Task<List<T>?> LoadJsonAsync<T>(string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", fileName);

            if (!File.Exists(path)) return null;

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<T>>(json, JsonOptions);
        }

        internal class JsonExercise
        {
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public Difficulty Difficulty { get; set; }
            public ExerciseType ExerciseType { get; set; }
            public string? SourceUrl { get; set; }
            public string? Equipment { get; set; }
            public List<JsonMuscleGroup> MuscleGroups { get; set; } = [];
        }

        internal class JsonMuscleGroup
        {
            public string Name { get; set; } = string.Empty;
            public MuscleRole MuscleRole { get; set; }
        }
    }
}
