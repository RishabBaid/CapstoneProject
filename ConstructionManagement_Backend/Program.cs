using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ConstructionManagement_Backend.Configurations;
using MongoDB.Driver;
using ConstructionManagement_Backend.Repositories;
using ConstructionManagement_Backend.Services;
using System.Text;
using ConstructionManagement_Backend.Middleware;

namespace ConstructionManagement_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure MongoDB
            builder.Services.Configure<DatabaseSettings>(
                builder.Configuration.GetSection("MongoDB"));

            builder.Services.AddSingleton<IMongoClient>(s =>
            {
                var settings = builder.Configuration.GetSection("MongoDB").Get<DatabaseSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            builder.Services.AddScoped<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IConfiguration>().GetSection("MongoDB").Get<DatabaseSettings>();
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });

            // Register Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
            builder.Services.AddScoped<IVendorRepository, VendorRepository>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IWorkforceRepository, WorkforceRepository>();
            builder.Services.AddScoped<ISafetyInspectionRepository, SafetyInspectionRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();


            // Register Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IFinanceService, FinanceService>();
            builder.Services.AddScoped<IVendorService, VendorService>();
            builder.Services.AddScoped<IMaterialService, MaterialService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IWorkforceService, WorkforceService>();
            builder.Services.AddScoped<ISafetyInspectionService, SafetyInspectionService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IEquipmentService, EquipmentService>();
            builder.Services.AddScoped<IReportService, ReportService>();

            // Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // Add services to the container.
            builder.Services.AddControllers();

            //Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")  // Replace with your frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Add Security Definition for JWT
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your token in the text box below.\nExample: Bearer xxxxxx.yyyyyyy.zzzzzz"
                });

                // Add Security Requirement for JWT
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            app.UseCors("AllowFrontend");  // Use CORS policy

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Add authentication middleware
            app.UseAuthorization();   // Add authorization middleware

            app.UseMiddleware<JwtMiddleware>(); // Custom middleware

            app.MapControllers(); // Map the controllers to the request pipeline

            app.MapGet("/", () => "Construction Management API is running...");

            app.Run();
        }
    }
}