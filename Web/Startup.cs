using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Helper.Configuration;
using Microsoft.EntityFrameworkCore;
using Data.Db;
using Web.Hubs;
using Web.Utils;
using Web.Services;
using Utils.Queue;
using Microsoft.Extensions.Options;
using IdentityServer4.AccessTokenValidation;
using Newtonsoft.Json.Serialization;

namespace Web
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
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var databaseCS = this.settings.Data.Model.ConnectionString;

            services.AddDbContext<GymOrganizerContext>(options => options.UseSqlServer(databaseCS));
            services.AddCors();
            services.AddSignalR();
            services.AddOptions();


            #region DI
            services.Configure<AppSettings>(this.Configuration);
            services.AddTransient((serviceProvider) => serviceProvider.GetService<IOptions<AppSettings>>().Value);
            services.AddScoped<TokenDataActionFilter>();
            services.AddScoped<ExceptionFilter>();
            services.AddTransient(typeof(OfficeService));
            services.AddTransient(typeof(CityService));
            services.AddTransient<IQueueHandler, RabbitMQHandler>();
            #endregion



            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(TokenDataActionFilter));
                options.Filters.Add(typeof(ExceptionFilter));                
            })
            .AddJsonOptions(options => 
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
              .AddJwtBearer(jwt =>
              {
                  jwt.Authority = "http://localhost:5000";
                  jwt.RequireHttpsMetadata = false;
                  jwt.Audience = "angular-client";
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            AutoMapperInitializer.Initialize();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("notifications");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
