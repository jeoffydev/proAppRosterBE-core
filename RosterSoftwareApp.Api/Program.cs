

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.JsonHandler();

var app = builder.Build();

// to automatically migrate the EF changes
await app.Services.InitializeDbMigrationAsync();



app.MapEventsEndpoint();
app.MapSongsEndpoint();
app.MapEventSongsEndpoint();


app.Run();
