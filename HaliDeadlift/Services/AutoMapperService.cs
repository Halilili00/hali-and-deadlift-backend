using HaliDeadlift.Application.Mappers;

namespace HaliDeadlift.Services
{
    public static class AutoMapperService
    {
        public static IServiceCollection RegisterMappers(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddAutoMapper(config => 
            {
                config.LicenseKey = configuration["LuckyPenny:LicenseKey"];
                config.AddProfile<ExerciseMapperProfile>();
                config.AddProfile<MuscleGroupMapperProfile>();
                config.AddProfile<EquipmentMapperProfile>();
            });

            return services;
        }
    }
}
