

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Authorization;
using RosterSoftwareApp.Api.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.JsonHandler();

//Authorization/Authentication
builder.Services.AddAuthentication()
            .AddJwtBearer()
            .AddJwtBearer("Auth0");

//Admin for authentication users but has separate access
builder.Services.AuthorizeRoleAdminExtensions();

// builder.Logging.AddJsonConsole(options =>
// {
//     options.JsonWriterOptions = new()
//     {
//         Indented = true
//     };
// });


// CORS 
builder.Services.AddRosterCors(builder.Configuration);

var app = builder.Build();

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
app.MapNoResultEndpoint();



app.UseCors();


app.Run();
