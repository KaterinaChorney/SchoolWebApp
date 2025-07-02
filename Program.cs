using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolWebApplication.Data;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Data.Repositories;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Middlewares;
using SchoolWebApplication.Services;
using SchoolWebApplication.Services.Implementations;
using SchoolWebApplication.Services.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models; 

namespace SchoolWebApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SchoolWebApplication API",
                    Version = "v1"
                });

                // Схема безпеки для JWT Bearer
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введіть токен у форматі: Bearer {your JWT token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                       new OpenApiSecurityScheme
                       {
                          Reference = new OpenApiReference
                          {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                          }
                       },
                       new List<string>()
                   }
                });
            });

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "SchoolWebAPI",
                    ValidAudience = "SchoolWebAPI",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_123456789_super_safe_key_!!778899"))
                };
            });

            builder.Services.AddAuthorization();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // DI - Services & UoW
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<IJournalService, JournalService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // JWT Service
            builder.Services.AddScoped<JwtService>();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program)); 

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Сидінг 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.SeedAsync(userManager, roleManager);
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>(); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}