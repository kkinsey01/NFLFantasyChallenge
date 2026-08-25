using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NFLFantasyChallenge.API.Services;
using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Middleware;
using NFLFantasyChallenge.Models;
using Resend;

namespace NFLFantasyChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ILineupControlService, LineupControlService>();
            builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.LogoutPath = "/Home/Logout";
                    options.AccessDeniedPath = "/Error";
                    options.ExpireTimeSpan = TimeSpan.FromDays(3);
                    options.SlidingExpiration = true;

                    options.Cookie.Name = "NFLFantasyChallenge.Auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                });

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<FantasyDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddOptions();
            builder.Services.AddHttpClient<ResendClient>();
            builder.Services.Configure<ResendClientOptions>(o =>
            {
                o.ApiToken = builder.Configuration["ApiKeys:Resend"] ?? "";
            });
            builder.Services.AddTransient<IResend, ResendClient>();

            // for render
            if (!builder.Environment.IsDevelopment())
            {            
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope()) 
            {
                var db = scope.ServiceProvider.GetRequiredService<FantasyDbContext>();
                try
                {
                    db.Database.Migrate();
                }
                catch
                {
                }
                DbSeeder.Seed(db);               
            }            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Leaderboard}/{id?}");

            app.Run();
        }
    }
}
