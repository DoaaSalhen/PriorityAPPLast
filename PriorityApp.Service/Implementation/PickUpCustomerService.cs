using AutoMapper;
using Data.Repository;
using @enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace PriorityApp.Service.Implementation
{
   public class PickUpCustomerService : IPickUpCustomerService
    {

        private readonly IRepository<Order, long> _pickupRepository;
        private readonly ILogger<PickUpCustomerService> _logger;
        private readonly IMapper _mapper;
        private readonly IDeliveryCustomerService _deliveryCustomerService;
        private readonly IItemService _itemService;
        private readonly IHoldService _holdService;
        private readonly UserManager<AspNetUser> _userManager;

        public PickUpCustomerService(IRepository<Order, long> pickupRepository,
           ILogger<PickUpCustomerService> logger, 
           IMapper mapper,
          IDeliveryCustomerService deliveryCustomerService,
          IItemService itemService,
          IHoldService holdService)
        {
            _pickupRepository = pickupRepository;
            _logger = logger;
            _mapper = mapper;
            _deliveryCustomerService = deliveryCustomerService;
            _itemService = itemService;
            _holdService = holdService;
        }
        public DataTable PreprocessInsertedPickOrders(DataTable dt, int itemStartIndex)
        {

            try
            {
                DataTable newDT = new DataTable("pickup");
                newDT.Columns.AddRange(new DataColumn[7] { new DataColumn("CustomerNumber"),
                                            new DataColumn("ItemNumber"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Priority"),
                                            new DataColumn("PriorityDate"),
                                            new DataColumn("Comment"),
                                            new DataColumn("Truck"),

                                           });

                for (int index = 0; index < dt.Columns.Count; index++)
                {
                    dt.Columns[index].ColumnName = dt.Columns[index].ColumnName.Trim();
                    dt.Columns[index].ColumnName = dt.Columns[index].ColumnName.Replace("#", ".");
                }
                for (int index = itemStartIndex; index < dt.Columns.Count; index++)
                {
                    ItemModel itemModel = _itemService.GetItemByName(dt.Columns[index].ColumnName.ToString()).Result;
                    dt.Columns[index].ColumnName = itemModel.Id.ToString();
                }
                float tempRemaining = 0;

                    foreach (DataRow row in dt.Rows)
                {
                    string customerNumber = row["CustomerNumber"].ToString();
                    if (customerNumber != "")
                    {
                        double totalQuantityForCustomer = 0;
                        var xxx = (DateTime.Compare((DateTime)row["PriorityDate"], DateTime.Today));
                        var xxxx = (DateTime)row["PriorityDate"];
                        var xx = (double)row["Priority"];
                        var xxxxxx = (double)row["Priority"] == (double)CommanData.Priorities.Norm;
                        if (((double)row["Priority"] == (double)CommanData.Priorities.Norm || (double)row["Priority"] == (double)CommanData.Priorities.Extra || row["Priority"].ToString() =="") && (DateTime.Compare((DateTime)row["PriorityDate"], DateTime.Today) >= 0) || (row["PriorityDate"].ToString()==""))
                        {
                            CustomerModel customer = _deliveryCustomerService.GetDeliveryCustomer(Convert.ToInt64(customerNumber));
                            var hold = _holdService.GetHold((DateTime)row["PriorityDate"], customer.zone.Territory.userId);
                            if (hold != null && customer != null)
                            {
                                for (int index = itemStartIndex; index < dt.Columns.Count; index++)
                                {
                                    var x = row[index].ToString();

                                    if (row[index].ToString() != "" && row["Priority"].ToString() !="" && row["PriorityDate"].ToString() != "")
                                    {
                                        if ((double)row["Priority"] == (double)CommanData.Priorities.Norm)
                                        {
                                            totalQuantityForCustomer = totalQuantityForCustomer + Convert.ToDouble(row[index]);
                                        }
                                        newDT.Rows.Add(row[0], dt.Columns[index].ColumnName, row[index], row["Priority"], (DateTime)row["PriorityDate"], row["Comment"], row["Truck"]);
                                    }
                                }
                                tempRemaining = hold.TempReminingQuantity - (float)totalQuantityForCustomer;
                                if (tempRemaining < 0)
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                return newDT;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }


    }
}
