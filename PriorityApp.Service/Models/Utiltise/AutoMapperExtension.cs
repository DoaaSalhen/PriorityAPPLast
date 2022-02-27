using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PriorityApp.Models.Auth;
using PriorityApp.Models.Models;
using PriorityApp.Models.Models.Dispatch;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Models.Models.MasterModels.Dispatch;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.Dispatch;
using PriorityApp.Service.Models.MasterModels;

namespace Cemex.Task.Utilities.Mapping
{
    public static class AutoMapperExtension
    {
        public static void ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssembleType));
        }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ItemModel, Item>();
                CreateMap<Item, ItemModel> ();
                CreateMap<MainRegionModel, MainRegion>();
                CreateMap<MainRegion, MainRegionModel>();
                CreateMap<SubRegion, SubRegionModel>();
                CreateMap<SubRegionModel, SubRegion>();
                CreateMap<State, StateModel>();
                CreateMap<StateModel, State>();
                CreateMap<Territory, TerritoryModel>();
                CreateMap<TerritoryModel, Territory>();
                CreateMap<Zone, ZoneModel>();
                CreateMap<ZoneModel, Zone>();
                CreateMap<Customer, CustomerModel>();
                CreateMap<CustomerModel, Customer>();
                CreateMap<Warehouse, WarehouseModel>();
                CreateMap<WarehouseModel, Warehouse>();
                CreateMap<Priority, PriorityModel>();
                CreateMap<PriorityModel, Priority>();
                CreateMap<Order, OrderModel2>();
                CreateMap<OrderModel2, Order>();
                CreateMap<Hold, HoldModel>();
                CreateMap<HoldModel, Hold>();
                CreateMap<OrderCategory, OrderCategoryModel>();
                CreateMap<OrderCategoryModel, OrderCategory>();
                CreateMap<SubmitNotification, SubmitNotificationModel>();
                CreateMap<SubmitNotificationModel, SubmitNotification>();
                CreateMap<OrderNotification, OrderNotificationModel>();
                CreateMap<OrderNotificationModel, OrderNotification>();
                CreateMap<UserNotificationModel, UserNotification>();
                CreateMap<UserNotification, UserNotificationModel>();
                CreateMap<WarehouseOrderHoldModel, WarehouseOrderHold>();
                CreateMap<WarehouseOrderHold, WarehouseOrderHoldModel>();
                CreateMap<UserModel, AspNetUser>();
                CreateMap<AspNetUser,UserModel>();
                CreateMap<RoleModel, IdentityRole>();
                CreateMap<IdentityRole, RoleModel>();
            }
        }
    }
    public class AssembleType
    {
    }
}