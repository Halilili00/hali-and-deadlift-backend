using AutoMapper;
using HaliDeadlift.Application.Actions.EquipmentActions.GetEquipments;
using HalilDeadlift.Domain.Excercises;

namespace HaliDeadlift.Application.Mappers
{
    public class EquipmentMapperProfile : Profile
    {
        public EquipmentMapperProfile()
        {
            CreateMap<Equipment, GetEquipmentsResponse>();
        }
    }
}
