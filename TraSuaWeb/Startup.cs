using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {                  
            var stringConnectdb = Configuration.GetConnectionString("dbtrasua");
            services.AddDbContext<DBtrasuaContext>(options => options.UseSqlServer(stringConnectdb));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSession();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
            //services.AddAuthentication("cookieAuthentication").AddCookie("cookieAuthentication", config =>
            //{
            //    config.Cookie.Name = "UserLoginCookie";
            //    config.ExpireTimeSpan = TimeSpan.FromDays(1);
            //    config.LoginPath = "/Adminlogin.html";
            //    config.LogoutPath = "/Adminlogout.html";
            //    config.AccessDeniedPath = "/not-found.html";
            //});
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = "AdministratorAuth";
            })
                .AddCookie("AdministratorAuth", "AdministratorAuth", option =>
                {
                    option.Cookie.Name = "AdministratorAuth";
                    option.LoginPath = new PathString("/Adminlogin.html");
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(14400);
                    option.AccessDeniedPath = "/not-found.html";
                    option.LogoutPath = "/Adminlogout.html";
                })
                .AddCookie("UsersAuth", "UsersAuth", option =>
                {
                    option.Cookie.Name = "UsersAuth";
                    option.LoginPath = new PathString("/dang-nhap.html");
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(144000);
                    option.AccessDeniedPath = "/not-found.html";
                    option.LogoutPath = "/dang-xuat.html";
                });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.ExpireTimeSpan = TimeSpan.FromDays(150);
            });
            services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
            services.AddAuthentication()
               .AddFacebook(facebookOpts =>
               {
                   facebookOpts.AppId = Configuration["Authentication:Facebook:AppId"];
                   facebookOpts.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                
               })
                .AddGoogle(googleOptions =>
                 {
                     // Đọc thông tin Authentication:Google từ appsettings.json
                     IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");

                     // Thiết lập ClientID và ClientSecret để truy cập API google
                     googleOptions.ClientId = googleAuthNSection["ClientId"];
                     googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
                     // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
                    // googleOptions.CallbackPath = "/dang-nhap-tu-google";

                 });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseNotyf();
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           
            
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
