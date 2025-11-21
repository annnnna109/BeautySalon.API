using BeautySalon.API.Data;
using BeautySalon.API.Middleware;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories;
using BeautySalon.API.Repositories.Interfaces;
using BeautySalon.API.Services;
using BeautySalon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EmployeeManagementService = BeautySalon.API.Services.EmployeeManagementService;
using ServiceManagementService = BeautySalon.API.Services.ServiceService;



namespace BeautySalon.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //app.Run();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //// Configure Entity Framework with PostgreSQL
            builder.Services.AddDbContext<BeautySalonContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("Admin", "Employee"));
                options.AddPolicy("ClientOnly", policy => policy.RequireRole("Admin", "Employee", "Client"));
            });

            // AutoMapper
            //builder.Services.AddAutoMapper(typeof(Program));

            // Repository Registration
            builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Service Registration
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
            builder.Services.AddScoped<IServiceService, ServiceManagementService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            // Global Exception Handling Middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();


            //var app = builder.Build();

            //app.Urls.Add("https://localhost:7000");
            //app.Urls.Add("http://localhost:5000");

            //Console.WriteLine("Application URLs:");
            //foreach (var url in app.Urls)
            //{
            //    Console.WriteLine($"  {url}");
            //}
        }
    }
}
