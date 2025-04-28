using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;

using Ocelot.DependencyInjection;

using Ocelot.Middleware;

using System.Text;



namespace Rescue.Gateway

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

            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // Configure JWT Authentication

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

              .AddJwtBearer(options =>

              {

                  options.TokenValidationParameters = new TokenValidationParameters

                  {

                      ValidateIssuer = true,

                      ValidateAudience = true,

                      ValidateIssuerSigningKey = true,

                      ValidIssuer = "microservices-auth-api", // Replace with your JWT issuer

                      ValidAudience = "microservices-client", // Replace with your JWT audience

                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TOP SECRET EXPANDING THIS BECUASE IT IS CAUSING ERROR AGAIN EXPANDING AGAIN ")) // Replace with your secret key

                  };

              });

            builder.Services.AddOcelot(builder.Configuration);



            builder.Services.AddCors(options =>

            {

                options.AddPolicy("AllowAngularApp",

                  policy => policy.WithOrigins("http://localhost:4200")

                          .AllowAnyMethod()

                          .AllowAnyHeader()

                          .AllowCredentials());

            });



            var app = builder.Build();

            app.UseCors("AllowAngularApp");



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

            app.UseOcelot().Wait();



            app.Run();

        }

    }

}