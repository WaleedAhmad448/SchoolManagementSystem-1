using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Helpers;
using SchoolManagementSystem.Repositories;
using System.Text;

namespace SchoolManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DbContext configuration
            builder.Services.AddDbContext<SchoolDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity Configuration
            builder.Services.AddIdentity<User, IdentityRole>(cfg =>
            {
                // Configure token provider for authentication
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;

                // Require email confirmation for login
                cfg.SignIn.RequireConfirmedEmail = true;

                // Ensure unique email for each user
                cfg.User.RequireUniqueEmail = true;

                // Password settings
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredLength = 6;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<SchoolDbContext>();

            // Authentication services
            builder.Services.AddAuthentication()
                // Enable cookie-based authentication
                .AddCookie()
                // Enable JWT authentication
                .AddJwtBearer(cfg =>
                {
                    // Configure JWT token validation parameters
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["Tokens:Issuer"],
                        ValidAudience = builder.Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
                    };
                });

            // Repositories
            builder.Services.AddScoped<IAlertRepository, AlertRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ISchoolClassRepository, SchoolClassRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();



            // Generic repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Inject UserHelper and MailHelper
            builder.Services.AddScoped<IUserHelper, UserHelper>();
            builder.Services.AddTransient<IMailHelper, MailHelper>();
            builder.Services.AddScoped<IBlobHelper, BlobHelper>();
            builder.Services.AddScoped<IConverterHelper, ConverterHelper>();

            var app = builder.Build();

            // Create roles on startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userHelper = services.GetRequiredService<IUserHelper>();

                // Ensure the roles are created at startup
                await userHelper.CheckRoleAsync("Admin");
                await userHelper.CheckRoleAsync("Employee");
                await userHelper.CheckRoleAsync("Student");
                await userHelper.CheckRoleAsync("Anonymous");
                await userHelper.CheckRoleAsync("Pending");
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Required for authentication
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
