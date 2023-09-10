using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tools.Api.EndPoint;
using Tools.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Herramientas Services layer dependency injections
builder.Services.AddHerramientasServiceLayerDependencyInjections(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options => options.AddPolicy(name: "BackEndDanferOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        //policy.WithOrigins("http://localhost:83")
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
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
app.UseCors("BackEndDanferOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapEntitiesEndPoints();

app.Run();
