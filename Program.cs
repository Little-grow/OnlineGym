using Microsoft.EntityFrameworkCore;
using OnlineGym.Models;
using OnlineGym.Repo;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineGym.Authentication.Helper;
using OnlineGym.Authentication.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineGym", Version = "v1" });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<NotificationService>(); // better to be with interface

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineGym V1"));
}

app.UseHangfireDashboard();
app.UseHangfireServer();

RecurringJob.AddOrUpdate<NotificationJob>("SendWeeklyRequestWeightUpdateNotifications",
    job => job.SendWeeklyRequestWeightUpdateNotifications(),
    Cron.Weekly(DayOfWeek.Friday, hour: 0, minute: 0));

RecurringJob.AddOrUpdate<NotificationJob>("SendWeeklyWeightUpdateNotifications",
        job => job.SendWeeklyWeightUpdateNotifications(),
        Cron.Weekly(DayOfWeek.Saturday, hour: 0, minute: 0));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
