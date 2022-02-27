using Microsoft.EntityFrameworkCore;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Models.Models;
using PriorityApp.Models.Models.MasterModels.Dispatch;
using PriorityApp.Models.Models.Dispatch;

namespace Repository.EntityFramework
{
    public class APPDBContext:DbContext
    {
        public APPDBContext(DbContextOptions<APPDBContext>options):base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<MainRegion> MainRegions { get; set; }
        public DbSet<SubRegion> SubReions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<Zone> Zones { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Priority> Priorities{ get; set; }
        public DbSet<Hold> Holds { get; set; }
        public DbSet<UpdateQuotaHistory> UpdateQuotaHistory { get; set; }

        public DbSet<SubmitNotification> SubmitNotifications { get; set; }

        public DbSet<OrderNotification> OrderNotification { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<OrderCategory> OrderCategories { get; set; }
        public DbSet<WarehouseOrderHold> WarehouseOrderHold { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hold>().HasKey(h => new { h.PriorityDate, h.userId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserNotification>().HasKey(u => new { u.userId, u.submitNotificationId });
            modelBuilder.Entity<WarehouseOrderHold>().HasKey(WH => new { WH.OrderId });

        }


    }
}
