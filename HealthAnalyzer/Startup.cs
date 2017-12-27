using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Db;
using Helper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Utils.Queue;

namespace HealthAnalyzer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.settings = configuration.Get<AppSettings>();
        }

        public IConfiguration Configuration { get; }

        private AppSettings settings;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var databaseCS = this.settings.Data.Model.ConnectionString;

            services.AddDbContext<GymOrganizerContext>(options => options.UseSqlServer(databaseCS));
            services.AddTransient<IQueueHandler, RabbitMQHandler>();

            services.Configure<AppSettings>(this.Configuration);
            services.AddTransient((serviceProvider) => serviceProvider.GetService<IOptions<AppSettings>>().Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
