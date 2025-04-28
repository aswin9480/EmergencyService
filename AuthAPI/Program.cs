using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Service.IService;
using AuthAPI.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI
{
    namespace Microserviceproject.Services.AuthAPI
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add CORS services
                

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

                builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
                //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
                builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

                builder.Services.AddTransient<IAuthService, AuthService>();
                builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
                builder.Services.AddScoped<IAuthService, AuthService>();

                // Add services to the container.
                builder.Services.AddControllers();

                // Add Swagger/OpenAPI
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Use CORS middleware
                app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                ; // Apply CORS policy

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();
                Applymigration(app);

                app.Run();
            }

            static void Applymigration(WebApplication app)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    if (_db.Database.GetPendingMigrations().Count() > 0)
                    {
                        _db.Database.Migrate();
                    }
                }
            }
        }
    }
}
