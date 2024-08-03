using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Services;
using Newtonsoft;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MovieConnection");
var userConnectionString = builder.Configuration.GetConnectionString("UserConnection");

builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(userConnectionString));

// Add Identity
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>() // database communication
    .AddDefaultTokenProviders();                // authentication config

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
// foi preciso criar uma injecao de dependencia addscoped aqui no program.cs, o Imapper e o UserManager já vem com essa configuraçã por padrão, por isso n precisou 
builder.Services.AddScoped<RegisterUserService>();


builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
