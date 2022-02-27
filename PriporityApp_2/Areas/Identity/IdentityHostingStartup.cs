using System;
using authtest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(authtest.Areas.Identity.IdentityHostingStartup))]
namespace authtest.Areas.Identity
{
    public class IdentityHostingStartup //: IHostingStartup
    {
        //public void Configure(IWebHostBuilder builder)
        //{
        //    //builder.ConfigureServices((context, services) => {
        //    //    services.AddDbContext<authtestContext>(options =>
        //    //        options.UseSqlServer(
        //    //            context.Configuration.GetConnectionString("SqlCon")));

        //    //    services.AddDefaultIdentity<AspNetUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    //    . AddRoles<IdentityRole>()
        //    //    .AddEntityFrameworkStores<authtestContext>();
                    
        //    //});
        //}
    }
}