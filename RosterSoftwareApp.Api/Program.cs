

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Authorization;
using RosterSoftwareApp.Api.Cors;
using RosterSoftwareApp.Api.Middleware;
using RosterSoftwareApp.Api.ErrorHandling;

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
});

// CORS 
builder.Services.AddRosterCors(builder.Configuration);

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



app.UseCors();


app.Run();
