

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

// to automatically migrate the EF changes
app.Services.InitializeDbMigration();

app.MapEventsEndpoint();

app.Run();
