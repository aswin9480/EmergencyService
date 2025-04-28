using Microsoft.EntityFrameworkCore;
using VictimAPI.Models;
using System.Web.Http.Cors;
using VictimAPI.Data;
using VictimAPI.Repositories;

namespace VictimAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<VictimContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("csVictimDB")));
            builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable CORS policy for cross-origin requests
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

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
        }
    }
}
