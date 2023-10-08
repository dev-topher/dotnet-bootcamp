// 1 Usa el Entity Framework
using collegeBackEnd.DataAccess;
using collegeBackEnd.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 2 Conecta con SQL Server Express

const string CONNECTIONNAME = "UniversityDb";
var conectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3 Añade context al services of builder

builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(conectionString));

// add services to the container

builder.Services.AddControllers();

// 4 add custom services

builder.Services.AddScoped<IStudentsService, StudentsService>();

//TODO: add the rest of services




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5 habilitar cors

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
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

// 6 tell app to use coors

app.UseCors("CorsPolicy");

app.Run();
