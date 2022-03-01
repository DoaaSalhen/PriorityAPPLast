﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.EntityFramework;

namespace Repository.EntityFramework.Migrations
{
    [DbContext(typeof(APPDBContext))]
    [Migration("20220228123623_CustomerId-Notnullable-migration")]
    partial class CustomerIdNotnullablemigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PriorityApp.Models.Models.Dispatch.OrderNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("submitNotificationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("submitNotificationId");

                    b.ToTable("OrderNotification");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.Hold", b =>
                {
                    b.Property<DateTime>("PriorityDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("ExtraQuantity")
                        .HasColumnType("real");

                    b.Property<float>("QuotaQuantity")
                        .HasColumnType("real");

                    b.Property<float>("ReminingQuantity")
                        .HasColumnType("real");

                    b.Property<float>("TempReminingQuantity")
                        .HasColumnType("real");

                    b.Property<int>("Tolerance")
                        .HasColumnType("int");

                    b.HasKey("PriorityDate", "userId");

                    b.ToTable("Holds");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Customer", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("CustomerType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CutomerNameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ZoneId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Dispatch.SubmitNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSubmittedOrders")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SubmitNotifications");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Item", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HMP")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("OldItemNumber")
                        .HasColumnType("bigint");

                    b.Property<bool>("QutaCalc")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.MainRegion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MainRegions");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.OrderCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("OrderCategories");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Priority", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.State", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("SubregionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SubregionId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.SubRegion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<int>("MainRegionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("RegionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MainRegionId");

                    b.ToTable("SubReions");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Territory", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Territories");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.UserNotification", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("submitNotificationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("userId", "submitNotificationId");

                    b.HasIndex("submitNotificationId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Warehouse", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Zone", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TerritoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TerritoryId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<int?>("CustomerType")
                        .HasColumnType("int");

                    b.Property<bool?>("Dispatched")
                        .HasColumnType("bit");

                    b.Property<long?>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<int?>("LineID")
                        .HasColumnType("int");

                    b.Property<int?>("OrderCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderDocument")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OrderNumber")
                        .HasColumnType("bigint");

                    b.Property<float?>("OrderQuantity")
                        .HasColumnType("real");

                    b.Property<int?>("OrginalQuantity")
                        .HasColumnType("int");

                    b.Property<string>("PODName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PODNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("PODZoneAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PODZoneName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PODZoneState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PriorityDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<float?>("PriorityQuantity")
                        .HasColumnType("real");

                    b.Property<int?>("SDMCU")
                        .HasColumnType("int");

                    b.Property<bool?>("SavedBefore")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubmitNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SubmitTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Submitted")
                        .HasColumnType("bit");

                    b.Property<string>("Truck")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WHSavedID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WHSubmittedID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderCategoryId");

                    b.HasIndex(new[] { "CustomerId" }, "Order_CustomerId_idx");

                    b.HasIndex(new[] { "OrderDate" }, "Order_OrderDate_idx");

                    b.HasIndex(new[] { "PriorityId" }, "Order_PriorityId_idx");

                    b.HasIndex(new[] { "ItemId" }, "Order_item_idx");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.UpdateQuotaHistory", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentQuantity")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OldQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("PriorityDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("territoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("territoryId");

                    b.ToTable("UpdateQuotaHistory");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.WarehouseOrderHold", b =>
                {
                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("HoldPriorityDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HolduserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("OrderId1")
                        .HasColumnType("bigint");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderId1");

                    b.HasIndex("HoldPriorityDate", "HolduserId");

                    b.ToTable("WarehouseOrderHold");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.Dispatch.OrderNotification", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.Order", "order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PriorityApp.Models.Models.MasterModels.Dispatch.SubmitNotification", "submitNotification")
                        .WithMany()
                        .HasForeignKey("submitNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("submitNotification");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Customer", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.Zone", "zone")
                        .WithMany()
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("zone");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.State", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.SubRegion", "Subregion")
                        .WithMany()
                        .HasForeignKey("SubregionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subregion");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.SubRegion", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.MainRegion", "MainRegion")
                        .WithMany()
                        .HasForeignKey("MainRegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainRegion");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Territory", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.UserNotification", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.Dispatch.SubmitNotification", "submitNotification")
                        .WithMany()
                        .HasForeignKey("submitNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("submitNotification");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Warehouse", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.MasterModels.Zone", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Territory");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.Order", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PriorityApp.Models.Models.MasterModels.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("PriorityApp.Models.Models.MasterModels.OrderCategory", "OrderCategory")
                        .WithMany()
                        .HasForeignKey("OrderCategoryId");

                    b.HasOne("PriorityApp.Models.Models.MasterModels.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.Navigation("Customer");

                    b.Navigation("Item");

                    b.Navigation("OrderCategory");

                    b.Navigation("Priority");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.UpdateQuotaHistory", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.MasterModels.Territory", "territory")
                        .WithMany()
                        .HasForeignKey("territoryId");

                    b.Navigation("territory");
                });

            modelBuilder.Entity("PriorityApp.Models.Models.WarehouseOrderHold", b =>
                {
                    b.HasOne("PriorityApp.Models.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PriorityApp.Models.Models.Hold", "Hold")
                        .WithMany()
                        .HasForeignKey("HoldPriorityDate", "HolduserId");

                    b.Navigation("Hold");

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
