using AutoMapper;
using HalilDeadlift.Domain;
using HalilDeadlift.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaliDeadlift.Application.Actions.ExerciseActions.GetExercises
{
    public class GetExercisesQuery : IRequest<ICollection<GetExercisesResponse>>
    {
        public IReadOnlyList<Guid>? MuscleGroupIds { get; set; }
        public IReadOnlyList<Guid>? EquipmentIds { get; set; }
        public Difficulty? Difficulty { get; set; }
        public string? Name { get; set; }

        public class GetExercisesQueryHandler : IRequestHandler<GetExercisesQuery, ICollection<GetExercisesResponse>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetExercisesQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<ICollection<GetExercisesResponse>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
            {
                var exercisesQuery = _dbContext.Exercises
                    .Include(exercise => exercise.ExerciseMuscleGroups)
                    .ThenInclude(exerciseMuscleGroup => exerciseMuscleGroup.MuscleGroup)
                    .AsNoTracking();

                if(request.MuscleGroupIds is not null && request.MuscleGroupIds.Any())
                {
                    exercisesQuery = exercisesQuery.Where(exercise =>
                        exercise.ExerciseMuscleGroups.Any(emg => request.MuscleGroupIds.Contains(emg.MuscleGroupId)));
                }

                if(request.EquipmentIds is not null && request.EquipmentIds.Any())
                {
                    exercisesQuery = exercisesQuery.Where(exercise =>
                        exercise.EquipmentId.HasValue && request.EquipmentIds.Contains(exercise.EquipmentId.Value));
                }

                if(request.Difficulty.HasValue)
                {
                    exercisesQuery = exercisesQuery.Where(exercise => exercise.Difficulty == request.Difficulty.Value);
                }

                if (request.Name != null)
                {
                    exercisesQuery = exercisesQuery.Where(exercis => exercis.Name.Contains(request.Name));
                }

                var exercises = await exercisesQuery
                    .OrderBy(exercise => exercise.Name)
                    .ToListAsync(cancellationToken);

                return _mapper.Map<ICollection<GetExercisesResponse>>(exercises);
            }
        }
    }
}
