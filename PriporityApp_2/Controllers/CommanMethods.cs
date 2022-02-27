using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriorityApp.Controllers.CustomerService;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Comman;
using PriorityApp.Service.Contracts.Dispatch;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers
{
    public class CommanMethods
    {

        private  readonly IRegionService _regionService;
        private  IStateService _stateService;
        private  IOrderService _orderService;
        private  ITerritoryService _territoryService;
        private  IZoneService _zoneService;
        private  IDeliveryCustomerService _deliveryCustomerService;
        private  IHoldService _holdService;
        private  IItemService _itemService;
        private  ISubmitNotificationService _submitNotificationService;
        private  UserManager<AspNetUser> _userManager;

        public CommanMethods(IRegionService regionService,
                                IStateService stateService,
                                IOrderService orderService,
                                ITerritoryService territoryService,
                                IZoneService zoneService,
                                IDeliveryCustomerService deliveryCustomerService,
                                IHoldService holdService,
                                IItemService itemService,
                                ISubmitNotificationService submitNotificationService,
                               UserManager<AspNetUser> userManager
                               )

        {
            _regionService = regionService;
            _stateService = stateService;
            _orderService = orderService;
            _territoryService = territoryService;
            _zoneService = zoneService;
            _deliveryCustomerService = deliveryCustomerService;
            _holdService = holdService;
            _itemService = itemService;
            _submitNotificationService = submitNotificationService;
            _userManager = userManager;
        }

        public  GeoFilterModel StartData()
        {
            try
            {
                GeoFilterModel geoFilterModel = new GeoFilterModel { };
                List<ItemModel> itemModels = new List<ItemModel>();

                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                geoFilterModel.SubRegions = subRegionModels;
                itemModels = _itemService.GetAllItems().Result;
                geoFilterModel.Items = itemModels;

                geoFilterModel.SubRegions = subRegionModels;

                geoFilterModel.SelectedPriorityDate = DateTime.Today;
                return geoFilterModel;
            }
            catch (Exception e)
            {
            }
            return null;

        }




       

    }
}

