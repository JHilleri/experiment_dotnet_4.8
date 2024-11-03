using todo.application;
using todo.infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder
    .Services.AddOpenApi()
    .AddMediatR(configuration =>
        configuration.RegisterServicesFromAssembly(ApplicationAssembly.Assembly)
    )
    .AddInfrastructureDependencies()
    .AddLogging(actions => actions.AddCustomLogger());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
