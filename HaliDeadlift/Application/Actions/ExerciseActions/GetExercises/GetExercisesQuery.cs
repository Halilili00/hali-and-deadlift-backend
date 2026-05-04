using AutoMapper;
using HalilDeadlift.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaliDeadlift.Application.Actions.ExerciseActions.GetExercises
{
    public class GetExercisesQuery : IRequest<ICollection<GetExercisesResponse>>
    {
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
                var exercises = await _dbContext.Exercises.ToListAsync(cancellationToken);

                return _mapper.Map<ICollection<GetExercisesResponse>>(exercises);
            }
        }
    }
}
