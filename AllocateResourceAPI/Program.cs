
using AllocateResourceAPI.Data;
using AllocateResourceAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AllocateResourceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ResourceAllocationContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("ResourceDb")));

            builder.Services.AddScoped<IResourceAllocatedRepository, ResourceAllocatedRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
