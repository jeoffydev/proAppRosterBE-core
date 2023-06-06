

using RosterSoftwareApp.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapEventsEndpoint();

app.Run();
