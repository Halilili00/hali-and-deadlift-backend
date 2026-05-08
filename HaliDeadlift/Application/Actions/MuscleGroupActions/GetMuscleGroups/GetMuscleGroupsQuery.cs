using AutoMapper;
using HalilDeadlift.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaliDeadlift.Application.Actions.MuscleGroupActions.GetMuscleGroups
{
    public class GetMuscleGroupsQuery : IRequest<ICollection<GetMuscleGroupsResponse>>
    {
        public class GetMuscleGroupsQueryHandler : IRequestHandler<GetMuscleGroupsQuery, ICollection<GetMuscleGroupsResponse>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetMuscleGroupsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<ICollection<GetMuscleGroupsResponse>> Handle(GetMuscleGroupsQuery request, CancellationToken cancellationToken)
            {
                var muscleGroups = await _dbContext.MuscleGroups.ToListAsync(cancellationToken);

                return _mapper.Map<ICollection<GetMuscleGroupsResponse>>(muscleGroups);
            }
        }
    }
}
