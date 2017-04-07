using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OITAppRoles.Data;
using OITAppRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OITAppRoles.Services;

namespace OITAppRoles
{
    public static class OITCoreExtensions
    {
        public static IServiceCollection AddOITIdenitity(this IServiceCollection serviceCollection, IConfigurationRoot Configuration, string ConnectionName)
        {
            serviceCollection.AddDbContext<OITApplicationDbContext>(options =>
            {
                var connect = Configuration.GetConnectionString(ConnectionName);
                options.UseSqlServer(connect);
            });

            serviceCollection.AddIdentity<OITApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<OITApplicationDbContext>()
                .AddDefaultTokenProviders();


            serviceCollection.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            // Add application services.
            serviceCollection.AddTransient<IEmailSender, AuthMessageSender>();
            serviceCollection.AddTransient<ISmsSender, AuthMessageSender>();

            return serviceCollection;
        }

        public static IApplicationBuilder UseOITIdenitity(this IApplicationBuilder app)
        {
            app.UseIdentity();
            return app;
        }
    }
}
