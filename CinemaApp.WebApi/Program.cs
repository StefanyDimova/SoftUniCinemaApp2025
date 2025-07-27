using CinemaApp.Data;
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static CinemaApp.GCommon.ApplicationConstants;

using CinemaApp.Web.Infrastructure.Extensions;

namespace CinemaApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<CinemaAppDbContext>();

            builder.Services.AddRepositories(typeof(IMovieRepository).Assembly);
            builder.Services.AddUserDefinedServices(typeof(IMovieService).Assembly);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(AllowAllDomainsPolicy, policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("https://localhost:7180")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            builder.Services.AddDbContext<CinemaAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(AllowAllDomainsPolicy);
            app.UseAuthorization();

            app.MapIdentityApi<IdentityUser>();
            app.MapControllers();

            app.Run();
        }
    }
}
