using AggregatorService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AggregatorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddLogging();
            builder.Services.AddControllers();

            // Register HttpClient for communicating with external APIs (VictimAPI and ResourceAPI)
            builder.Services.AddHttpClient<DataAggregatorService>()
                .ConfigureHttpClient(client =>
                {
                    // Set the base URLs for the microservices
                    client.BaseAddress = new Uri("https://localhost:7129/api");  // VictimAPI base URL
                });

            builder.Services.AddHttpClient<DataAggregatorService>("ResourceAPI")
                .ConfigureHttpClient(client =>
                {
                    // Set the base URLs for the microservices
                    client.BaseAddress = new Uri("https://localhost:7240/api");  // ResourceAPI base URL
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable CORS
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use HTTPS Redirection and Map Controllers
            app.UseHttpsRedirection();
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
