using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Implementation;
using PriorityApp.Service.Implementation.Auth;

using Data.Repository;
using Cemex.Task.Utilities.Mapping;
using authtest.Data;
using Microsoft.AspNetCore.Identity;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PriorityApp.Service.Contracts.CustomerService;
using PriorityApp.Service.Implementation.CustomerService;
using System.Security.Claims;
using PriorityApp.Service.Contracts.Comman;
using PriorityApp.Service.Implementation.Comman;
using PriorityApp.Service.Contracts.Dispatch;
using PriorityApp.Service.Implementation.Dispatch;
using PriorityApp.Controllers.CustomerService;
using Microsoft.AspNetCore.Identity.UI.Services;
using PriorityApp.Service.Models.Email;
using PriorityApp.Service.Implementation.Email;
using Microsoft.AspNetCore.Http.Features;

namespace PriporityApp_2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           
            services.AddControllersWithViews();
            services.AddDbContext<APPDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlCon")));

            var connectionString = Configuration.GetConnectionString("SqlCon");
            services.AddDbContext<authtestContext>(options =>
            {
                options.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(authtestContext).Assembly.FullName));
            }, ServiceLifetime.Transient);

            services.AddIdentity<AspNetUser, IdentityRole>(options =>
            {
                //options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;

            })
            .AddEntityFrameworkStores<authtestContext>()
            .AddRoles<IdentityRole>()
            .AddDefaultUI().AddDefaultTokenProviders();

            services.AddScoped<DbContext, APPDBContext>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<UserManager<AspNetUser>>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ITerritoryService, TerritoryService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IDeliveryCustomerService, DeliveryCustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPickUpCustomerService, PickUpCustomerService>();
            services.AddScoped<ISubmitNotificationService, SubmitNotificationService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IHoldService, HoldService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPendService, PendService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IOrderCategoryService, OrderCategoryService>();
            services.AddScoped<IWarehouseOrderHoldService, WarehouseOrderHoldService>();
            services.ConfigAutoMapper();
            services.AddTransient<IEmailSender, MailKitEmailSenderService>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.Host_Address = Configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.Host_Port = Convert.ToInt32(Configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.Host_Username = Configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.Host_Password = Configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.Sender_EMail = Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
                options.Sender_Name = Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
                options.Username = Configuration["ExternalProviders:MailKit:SMTP:Username"];
                options.Host_Username = Configuration["ExternalProviders:MailKit:SMTP:Host_Username"];

            });
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddMvc(options => options.EnableEndpointRouting = false );
            services.AddMvc(option =>
            {
                option.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            });

            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
           
            services.AddSignalR();
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            services.AddMvc().AddMvcOptions(options =>
    {
        options.MaxModelValidationErrors = 999999;
    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
           app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{PriorityTool}/{controller=Item}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<NotificationHub>("PriorityTool/NotificationHub");

            });

           
        }
    }
}
