

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.JsonHandler();

//Authorization/Authentication
builder.Services.AddAuthentication().AddJwtBearer();

//Admin for authentication users but has separate access
builder.Services.AuthorizeRoleAdminExtensions();

var app = builder.Build();

// to automatically migrate the EF changes
await app.Services.InitializeDbMigrationAsync();



app.MapEventsEndpoint();
app.MapSongsEndpoint();
app.MapEventSongsEndpoint();


app.Run();
