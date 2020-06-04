using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using PTW.DataAccess.Services;
using PTW.DataAccess.ServicesImpl;
using PTWWebsite.CustomMiddleware;

namespace PTWWebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("PTWSecurityScheme")
                   .AddCookie("PTWSecurityScheme", options =>
                   {
                       options.AccessDeniedPath = new PathString("/Security/Access");
                       options.LoginPath = "/Login/";
                       options.Cookie.HttpOnly = true;
                       options.ExpireTimeSpan = new TimeSpan(0, 60, 0); // 60 minutes
                       options.LogoutPath = "/Login/Logout/";
                       options.SlidingExpiration = true;
                       options.Events.OnValidatePrincipal = ClaimPrincipalValidator.ValidateAsync;
                   });
            
            services.AddTransient<ILoggerManager, LoggerManager>();
            services.AddTransient<IMasterService, MasterService>();
            //services.AddTransient<ITicketService, TicketService>();
            //services.AddTransient<IClientService, ClientService>();
            //services.AddTransient<IMasterService, MasterService>();
            //services.AddTransient<IProjectService, ProjectService>();
            //services.AddTransient<IGroupService, GroupService>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<ILogService, LogService>();

            //services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddApplicationInsightsTelemetry(Configuration);
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/{0}");
            }

          //  app.ConfigureCustomExceptionMiddleware();

            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            //app.UseApplicationInsightsRequestTelemetry();
            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}
