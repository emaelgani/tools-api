using Tools.Api.EndPoint;
using Tools.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Herramientas Services layer dependency injections
builder.Services.AddHerramientasServiceLayerDependencyInjections(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
    {
        ["activated"] = false
    };
});

app.UseHttpsRedirection();

app.MapEntitiesEndPoints();

app.Run();
