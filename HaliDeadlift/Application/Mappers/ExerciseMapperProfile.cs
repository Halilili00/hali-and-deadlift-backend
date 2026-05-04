using AutoMapper;
using HaliDeadlift.Application.Actions.ExerciseActions.GetExercises;
using HalilDeadlift.Domain.Excercises;

namespace HaliDeadlift.Application.Mappers
{
    public class ExerciseMapperProfile : Profile
    {
        public ExerciseMapperProfile() 
        {
            CreateMap<Exercise, GetExercisesResponse>();
        }
    }
}
