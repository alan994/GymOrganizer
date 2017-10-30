using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Quickstart.UI;
using Helper.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.Models;
using Auth.Configuration;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.DbContexts;
using Data.Db;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Host.Services;

namespace Auth
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
                
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IEmailSender, EmailSender>();


            var databaseCS = Configuration[Constants.DatabaseConnectionString];
            var operationCS = Configuration[Constants.AuthOperationConnectionString];
            var configurationCS = Configuration[Constants.AuthConfigurationConnectionString];

            services.AddDbContext<GymOrganizerContext>( options => options.UseSqlServer(databaseCS));


            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
            })
                .AddEntityFrameworkStores<GymOrganizerContext>()
                .AddDefaultTokenProviders()
                .AddIdentityServer();


            services.AddIdentityServer()
                //To be validated
                .AddDeveloperSigningCredential()                

                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(configurationCS, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(operationCS, sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddAspNetIdentity<User>(); //TODO: fix this

            services.AddCors();


            EnsureSeedData(services);
            EnsureDbCreation(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void EnsureDbCreation(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<GymOrganizerContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private static void EnsureSeedData(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {                
                using (var context = scope.ServiceProvider.GetService<ConfigurationDbContext>())
                {
                    context.Database.Migrate();
                    EnsureSeedData(context);
                }

                using (var context = scope.ServiceProvider.GetService<PersistedGrantDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private static void EnsureSeedData(IConfigurationDbContext context)
        {
            context.Clients.RemoveRange(context.Clients);
            context.SaveChanges();
            if (!context.Clients.Any())
            {
                foreach (var client in Clients.Get().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Auth.Configuration.Resources.GetIdentityResources().ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Auth.Configuration.Resources.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
