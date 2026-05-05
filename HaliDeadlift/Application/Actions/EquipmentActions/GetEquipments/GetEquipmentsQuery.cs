using AutoMapper;
using HalilDeadlift.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaliDeadlift.Application.Actions.EquipmentActions.GetEquipments
{
    public class GetEquipmentsQuery : IRequest<ICollection<GetEquipmentsResponse>>
    {
        public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, ICollection<GetEquipmentsResponse>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            public GetEquipmentsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ICollection<GetEquipmentsResponse>> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
            {
                var equipments = await _dbContext.Equipments.ToListAsync(cancellationToken);

                return _mapper.Map<ICollection<GetEquipmentsResponse>>(equipments);
            }
        }
    }
}
