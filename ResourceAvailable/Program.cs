using Microsoft.EntityFrameworkCore;
using ResourceAvailableAPI.Data;
using ResourceAvailableAPI.Repositories;
using Microsoft.AspNetCore.Cors;

namespace ResourceAvailableAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ResourceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("csResourceDB")));
            builder.Services.AddScoped<IResourceRepository, ResourceRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

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
