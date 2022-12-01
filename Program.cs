using DevStudyNotes.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Memory Database
builder.Services.AddDbContext<StudyNoteDbContext>(
    o => o.UseInMemoryDatabase("StudyNoteDb")
);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
}).UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "DevStudyNotes.API",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "Luis Fernando Paganini",
                Email = "luisfernando_paganini@hotmail.com",
                Url = new Uri("https://www.linkedin.com/in/luis-fernando-paganini-68763b1a9/")
            }
        });

        var xmlFile = "DevStudyNotes.API.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    }    
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
