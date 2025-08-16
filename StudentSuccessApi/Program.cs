using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// EF Core with SQLite
builder.Services.AddDbContext<StudentSuccessApi.Data.AppDbContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("Default")
		?? "Data Source=students.db";
	options.UseSqlite(connectionString);
});

builder.Services.AddScoped<StudentSuccessApi.Services.StudentSuccessService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
