using GymManagmentSystem.DataContext;
using GymManagmentSystem.Interfaces;
using GymManagmentSystem.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GymManagmentSystem.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:44320") 
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});*/

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<IGymRepository, ClassRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DataContext' is not configured.");
}


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
