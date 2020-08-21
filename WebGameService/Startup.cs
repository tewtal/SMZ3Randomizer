using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Randomizer.Shared.Models;
using WebGameService.Hubs;

namespace WebGameService
{
    public class Startup
    {
        readonly string MultiworldOrigins = "_multiworldOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MultiworldOrigins,
                builder =>
                {
                    builder.WithOrigins("https://localhost:5001", "https://*.samus.link")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
                           
                });
            });

            services.AddDbContext<RandomizerContext>
            //(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
                (options => options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
            services.AddControllers();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MultiworldOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MultiworldHub>("/multiworldHub");
                endpoints.MapControllers();
            });

        }
    }
}
