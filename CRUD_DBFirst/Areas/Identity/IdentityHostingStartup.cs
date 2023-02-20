using System;
using CRUD_DBFirst.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CRUD_DBFirst.Areas.Identity.IdentityHostingStartup))]
namespace CRUD_DBFirst.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CRUD_DBFirstContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CRUD_DBFirstContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CRUD_DBFirstContext>();
            });
        }
    }
}
