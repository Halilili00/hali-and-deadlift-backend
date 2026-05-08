using HaliDeadlift.Services;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
const string appCorsPolicy = "AppCORSPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(builder.Configuration, appCorsPolicy);
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddOpenApiService();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.RegisterMappers(builder.Configuration);
builder.Services.AddMediatRService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UseCors(appCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
