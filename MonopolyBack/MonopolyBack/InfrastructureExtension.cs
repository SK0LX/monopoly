using MonopolyBack.Application;
using MonopolyBack.EndPoints;
using MonopolyBack.Infrastructure;
using MonopolyBack.Infrastructure.Auth;

namespace MonopolyBack;

public static class InfrastructureExtension
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplication();

        var connectionString = builder.Configuration.GetConnectionString("Postgres")
                               ?? throw new InvalidOperationException("Connection string 'Postgres' is not configured.");

        builder.Services.AddInfrastructure(connectionString);

        return builder;
    }

    public static WebApplication UseApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseMiddleware<AuthenticationMiddleware>();
        app.UseAuthenticationEndpoints();
        app.UseMainEndpoints();
        return app;
    }
}
