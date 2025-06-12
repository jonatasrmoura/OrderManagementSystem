using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Infra;
using OrderManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o repositório
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Adicione isso antes de builder.Build()
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
