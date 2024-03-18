using Microsoft.EntityFrameworkCore;
using VivesActivities.Core;
using VivesActivities.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<VivesActivityService>();
builder.Services.AddScoped<LocationService>();

builder.Services.AddDbContext<VivesActivitiesDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesActivitiesDbContext));
    //options.UseSqlServer("Server=.\\SqlExpress;Database=VivesActivities;Trusted_Connection=True;TrustServerCertificate=True");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<VivesActivitiesDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
