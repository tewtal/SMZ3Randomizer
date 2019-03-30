using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using WebRandomizer.Hubs;
using WebRandomizer.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace WebRandomizer {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddDbContext<RandomizerContext>
                (options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });

            services.Configure<KestrelServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".ips"] = "application/octet-stream";

            app.UseStaticFiles(new StaticFileOptions {
                ContentTypeProvider = provider
            });

            app.UseSpaStaticFiles(new StaticFileOptions {
                ContentTypeProvider = provider
            });

            app.UseSignalR(routes => {
                routes.MapHub<MultiworldHub>("/multiworldHub");
            });

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

    }

}
