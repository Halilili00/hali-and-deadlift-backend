using System.Text.Json.Nodes;

namespace HaliDeadlift.Services
{
    public static class OpenApiService
    {
        public static IServiceCollection AddOpenApiService(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddSchemaTransformer((schema, context, cancellationToken) =>
                {
                    // convert enum values to strings in the OpenAPI schema
                    if (context.JsonTypeInfo.Type.IsEnum)
                    {
                        var enumNames = Enum.GetNames(context.JsonTypeInfo.Type);

                        schema.Type = Microsoft.OpenApi.JsonSchemaType.String;
                        schema.Enum = enumNames
                            .Select(name => (JsonNode)JsonValue.Create(name))
                            .ToList();
                    }

                    return Task.CompletedTask;
                });
            });
            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
