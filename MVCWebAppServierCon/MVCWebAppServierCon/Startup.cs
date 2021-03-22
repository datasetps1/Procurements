using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCWebAppServierCon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
//using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Extensions.FileProviders;
using System.IO;
using PathString = Microsoft.AspNetCore.Http.PathString;

namespace MVCWebAppServierCon
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
            //options.Filters.AddService<SetViewDataFilter>();

            #region snippet1
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;
            #endregion
            services.Configure<RequestLocalizationOptions>(
           opts =>
           {
               var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                            new CultureInfo("ar")
                        };

               opts.DefaultRequestCulture = new RequestCulture("en-US");

               opts.SupportedCultures = supportedCultures;
               opts.SupportedUICultures = supportedCultures;
           });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //  ////here
            //  services.AddLocalization(options => options.ResourcesPath = "Resources");
            //  services.Configure<RequestLocalizationOptions>(options =>
            //  {
            //      var supportedCultures = new List<CultureInfo>
            //  {
            //      new CultureInfo("en-US"),
            //      new CultureInfo("ar-SA"),

            //  };
            //      options.DefaultRequestCulture = new RequestCulture("en-US");
            //      options.SupportedCultures = supportedCultures;
            //      options.SupportedUICultures = supportedCultures;
            //  });
            //  //Localization Ends here
            ////  till here
            ///


            services.ConfigureApplicationCookie(Options =>

                Options.AccessDeniedPath = new PathString("/Home/Privacy")
            );


            services.AddDbContext<SecondConnClass>(options => options.UseSqlServer(Configuration.GetConnectionString("ProcurementConn")));
            services.AddDbContext<ConnnectionStringClass>(options => options.UseSqlServer(Configuration.GetConnectionString("Myconnection")));

            services.AddSingleton<IConfiguration>(Configuration);


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SecondConnClass>().AddDefaultTokenProviders();
            services.AddDirectoryBrowser();
            // services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ConnnectionStringClass>()
            //  .AddDefaultTokenProviders();


            //services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            //     services.AddMvc()
            //         .AddViewLocalization(
            //             LanguageViewLocationExpanderFormat.Suffix,
            //             opts => { opts.ResourcesPath = "Resources"; })
            //         .AddDataAnnotationsLocalization();
            //     services.Configure<RequestLocalizationOptions>(
            //        opts =>
            //       {
            //    var supportedCultures = new List<CultureInfo>
            //    {
            //         new CultureInfo("ar"),
            //         new CultureInfo("en-GB"),
            //         new CultureInfo("en-US"),
            //         //new CultureInfo("en"),
            //         //new CultureInfo("fr-FR"),
            //         //new CultureInfo("fr"),
            //    };

            //    opts.DefaultRequestCulture = new RequestCulture("en-GB");
            //     // Formatting numbers, dates, etc.
            //     opts.SupportedCultures = supportedCultures;
            //     // UI strings that we have localized.
            //     opts.SupportedUICultures = supportedCultures;
            //});
        }
        //public void Configure(IApplicationBuilder app)
        //{
        //    app.UseStaticFiles();

        //    var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        //    app.UseRequestLocalization(options.Value);

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
                // For more details on creating database during deployment,
                // see: http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        //serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                        //    .Database.Migrate();
                    }
                }
                catch { }
            }

            #region snippet2
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar"),
            };


            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            // To configure external authentication, 
            // see: http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseAuthentication();
            #endregion


            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            ;


            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/MyImages")
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/MyImages")
            });
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SecondConnClass>();
                context.Database.Migrate();
            }

        }
    }
    //Localization
    //public void ConfigureServices(IServiceCollection services)
    //{

    //}

}
