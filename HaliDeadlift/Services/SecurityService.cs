namespace HaliDeadlift.Services
{
    public static class SecurityService
    {
        public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration, string corsPolicy)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    policy.WithOrigins(configuration["AllowedOrigins"]?.Split(',') ?? Array.Empty<string>())
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
