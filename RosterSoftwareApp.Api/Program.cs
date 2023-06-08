

using RosterSoftwareApp.Api.Endpoints;
using RosterSoftwareApp.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// This is where to add or Register your dependency injection
builder.Services.AddSingleton<IEventsRepository, InMemEventsRepository>();

var connectionDBString = builder.Configuration.GetConnectionString("RosterAppContext");

var app = builder.Build();

app.MapEventsEndpoint();

app.Run();
