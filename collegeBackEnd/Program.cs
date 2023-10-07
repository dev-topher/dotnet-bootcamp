// 1 Usa el Entity Framework
using collegeBackEnd.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 2 Conecta con SQL Server Express

const string CONNECTIONNAME = "UniversityDb";
var conectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3 Añade context al services of builder

builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(conectionString));








builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
