namespace HaliDeadlift.Services
{
    public static class MediatRService
    {
        public static IServiceCollection AddMediatRService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.LicenseKey = configuration["LuckyPenny:LicenseKey"];
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            return services;
        }
    }
}
