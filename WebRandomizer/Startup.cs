using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Randomizer.Shared.Models;

namespace WebRandomizer {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc(options => {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson();

            services.AddDbContext<RandomizerContext>
                //(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
                (options => options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });

            services.Configure<KestrelServerOptions>(options => {
                options.AllowSynchronousIO = true;
                options.ConfigureEndpointDefaults(ep => {
                    ep.Protocols = HttpProtocols.Http1;
                });
            });

            services.Configure<IISServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });
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
            provider.Mappings[".rdc"] = "application/octet-stream";
            provider.Mappings[".lua"] = "text/x-lua";

            var attachments = new List<string> {
                ".lua",
            };

            var path = $"ClientApp/{(env.IsProduction() ? "build" : "public")}";
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, path)),
                ContentTypeProvider = provider,
                OnPrepareResponse = ctx => {
                    var ext = Path.GetExtension(ctx.File.Name);
                    if (attachments.Contains(ext))
                        ctx.Context.Response.Headers.Add("Content-Disposition", "attachment");
                },
            });

            app.UseStaticFiles(new StaticFileOptions {
                ContentTypeProvider = provider,
            });

            app.UseSpaStaticFiles(new StaticFileOptions {
                ContentTypeProvider = provider,
            });

            app.UseRouting();

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
