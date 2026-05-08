using AutoMapper;
using HaliDeadlift.Application.Actions.MuscleGroupActions.GetMuscleGroups;
using HalilDeadlift.Domain.Excercises;

namespace HaliDeadlift.Application.Mappers
{
    public class MuscleGroupMapperProfile : Profile
    {
        public MuscleGroupMapperProfile()
        {
            CreateMap<MuscleGroup, GetMuscleGroupsResponse>();
        }
    }
}
