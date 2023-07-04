

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Authorization;
using RosterSoftwareApp.Api.Cors;
using RosterSoftwareApp.Api.Middleware;
using RosterSoftwareApp.Api.ErrorHandling;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using RosterSoftwareApp.Api.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.JsonHandler();

//Authorization/Authentication
builder.Services.AddAuthentication()
            .AddJwtBearer()
            .AddJwtBearer("Auth0");

//Admin for authentication users but has separate access
builder.Services.AuthorizeRoleAdminExtensions();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
})
.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

// CORS 
builder.Services.AddRosterCors(builder.Configuration);

builder.Services.AddSwaggerGen()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddEndpointsApiExplorer();

builder.Logging.AddAzureWebAppDiagnostics();

var app = builder.Build();

// Error handler built-in exception * from folder ErrorHandling/
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
// Custom middleware for timer
app.UseMiddleware<RequestTimingMiddleware>();

// to automatically migrate the EF changes
await app.Services.InitializeDbMigrationAsync();

// added Http Logging and setup the logger into appsettings.json
app.UseHttpLogging();

app.MapEventsEndpoint();
app.MapSongsEndpoint();
app.MapEventSongsEndpoint();
app.MapInstrumentsEndpoint();
app.MapNotificationsEndpoint();
app.MapMemberInstrumentsEndpoint();
app.MapMemberEventsEndpoint();
app.MapNoResultEndpoint();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}




app.UseCors();


app.Run();
