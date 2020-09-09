using ApartmentSearch.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ApartmentSearch.Areas.Identity.IdentityHostingStartup))]
namespace ApartmentSearch.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
         //       services.AddDbContext<ApartmentsDbContext>(options =>
           //         options.UseSqlServer(
             //           context.Configuration.GetConnectionString("ApartmentSearchDbContextConnection")));

                services.AddDefaultIdentity<ApartmentsUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                })
                    .AddEntityFrameworkStores<ApartmentsDbContext>();
            });
        }
    }
}