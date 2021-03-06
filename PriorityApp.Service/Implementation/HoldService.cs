using AutoMapper;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Auth;
using PriorityApp.Models.Models;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Auth;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Implementation
{

    public class HoldService : IHoldService
    {
        private readonly IRepository<Hold, int> _HoldRepository;
        private readonly IRepository<Hold, Hold> _repository2;
        private readonly IUserService _userService;
        private readonly ITerritoryService _territoryService;
        private readonly ILogger<HoldService> _logger;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IMapper _mapper;


        public HoldService(IRepository<Hold, int> HoldRepository,
            ILogger<HoldService> logger, IMapper mapper,
            IUserService userService,
            ITerritoryService territoryService,
            UserManager<AspNetUser> userManager,
            IRepository<Hold, Hold> repository2)
        {
            _HoldRepository = HoldRepository;
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _territoryService = territoryService;
            _userManager = userManager;
            _repository2 = repository2;

        }
        public Task<bool> CreateHold(HoldModel model)
        {
            try
            {
                Hold hold = new Hold();
                hold = _mapper.Map<Hold>(model);
                var response = _HoldRepository.Add(hold);
                return Task<bool>.FromResult<bool>(true);

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Task<bool>.FromResult<bool>(false);

        }

        public Task<List<HoldModel>> GetAllHolds()
        {
            throw new NotImplementedException();
        }

        public HoldModel GetHold(DateTime? priorityDate, string? userId)
        {
            var hold = _HoldRepository.Find(h => h.userId == userId && h.PriorityDate == priorityDate, false).FirstOrDefault();
            HoldModel model = new HoldModel();
            model = _mapper.Map<HoldModel>(hold);
            return model;
        }

        public List<HoldModel> GetHoldBypriorityDate(DateTime? priorityDate)
        {
            try
            {
                List<Hold> holds = _HoldRepository.Find(h => h.PriorityDate == priorityDate, false).ToList();
                List<HoldModel> models = new List<HoldModel>();
                models = _mapper.Map<List<HoldModel>>(holds);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public List<HoldModel> GetLastHoldByUserId(string? userId)
        {
            try
            {
                List<Hold> holds = _HoldRepository.Find(h => h.userId == userId, false).ToList();  //.OrderByDescending(h=>h.PriorityDate).Last();
                List<HoldModel> models = new List<HoldModel>();
                models = _mapper.Map<List<HoldModel>>(holds);
                return models;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public HoldModel GetLastHoldByUserIdAndPriorityDate(string? userId, DateTime? priorityDate)
        {
            try
            {
                Hold hold = _HoldRepository.Find(h => h.userId == userId && h.PriorityDate == priorityDate, false).First();  //.OrderByDescending(h=>h.PriorityDate).Last();
                HoldModel model = new HoldModel();
                model = _mapper.Map<HoldModel>(hold);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        //public DataTable prepareDataForHold(DataTable dt)
        //{
        //    try
        //    {
        //        DateTime priorityDate = (DateTime)dt.Rows[0]["Quota Date"];
        //        int tolerance = Convert.ToInt32(dt.Rows[0]["Tolerance"]);

        //        //List<HoldModel> models = GetHoldBypriorityDate(priorityDate);
        //        string userName;
        //        //if(models.Count() == 0)
        //        //{
        //        dt.Columns.Add("RemainingQuantity");
        //        dt.Columns.Add("TempReminingQuantity");
        //        dt.Columns.Add("PriorityDate");
        //        dt.Columns.Add("PreviousRemainingQuantity");
        //        DateTime previousDay = priorityDate.AddDays(-1);
        //        List<HoldModel> holdModels = GetHoldBypriorityDate(previousDay);
        //        for (int index = 0; index < dt.Rows.Count; index++)
        //        {
        //            userName = dt.Rows[index]["Salesman"].ToString();
        //            dt.Rows[index]["Salesman"] = _userManager.FindByNameAsync(userName).Result.Id;
        //            dt.Rows[index]["PriorityDate"] = priorityDate;
        //            dt.Rows[index]["Tolerance"] = tolerance;

        //            float previousRemainingQuota = holdModels.Where(h => h.userId == dt.Rows[index]["Salesman"].ToString()).First().ReminingQuantity;
        //            dt.Rows[index]["Assigned"] = dt.Rows[index]["Assigned"].ToString() + previousRemainingQuota;
        //            dt.Rows[index]["RemainingQuantity"] = dt.Rows[index]["Assigned"];
        //            dt.Rows[index]["TempReminingQuantity"] = dt.Rows[index]["Assigned"];

        //        }
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            HoldModel hold = GetLastHoldByUserIdAndPriorityDate(row["Salesman"].ToString(), priorityDate);
        //            if (hold != null)
        //            {
        //                row.Delete();
        //            }
        //        }
        //        return dt;
        //        //}

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.ToString());
        //    }
        //    return null;
        //}

        public DataTable prepareDataForHold(DataTable dt)
        {
            try
            {
                DateTime priorityDate = (DateTime)dt.Rows[0]["Quota Date"];
                int tolerance = Convert.ToInt32(dt.Rows[0]["Tolerance"]);

                //List<HoldModel> models = GetHoldBypriorityDate(priorityDate);
                string userName;
                //if(models.Count() == 0)
                //{
                dt.Columns.Add("RemainingQuantity");
                dt.Columns.Add("TempReminingQuantity");
                dt.Columns.Add("PriorityDate");
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    dt.Rows[index]["RemainingQuantity"] = dt.Rows[index]["Assigned"];
                    dt.Rows[index]["TempReminingQuantity"] = dt.Rows[index]["Assigned"];
                    dt.Rows[index]["PriorityDate"] = priorityDate;
                    dt.Rows[index]["Tolerance"] = tolerance;
                    userName = dt.Rows[index]["Salesman"].ToString();
                    dt.Rows[index]["Salesman"] = _userManager.FindByNameAsync(userName).Result.Id;

                }
                foreach (DataRow row in dt.Rows)
                {
                    HoldModel hold = GetLastHoldByUserIdAndPriorityDate(row["Salesman"].ToString(), priorityDate);
                    if (hold != null)
                    {
                        row.Delete();
                    }
                }
                return dt;
                //}

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }
        public async Task<bool> UpdateHold(HoldModel model)
        {
            try
            {
                Hold hold = _mapper.Map<Hold>(model);
                bool response = _HoldRepository.Update(hold);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }

        public async Task<bool> Update2Holds(HoldModel model1, HoldModel model2)
        {
            try
            {
                bool response = false;
                Hold hold1 = _mapper.Map<Hold>(model1);
                Hold hold2 = _mapper.Map<Hold>(model2);

                var updatedHold = _repository2.UpdateTwoEntities(hold1, hold2,false);
                if (updatedHold != null)
                {
                    //OrderModel2 newOrderModel = _mapper.Map<OrderModel2>(updatedOrder);
                    return true;

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }


        bool IHoldService.AddQuotaFile(DataTable dt, string SqlConnectionString)
        {
            try
            {
                dt.CaseSensitive = false;
                using (SqlConnection con = new SqlConnection(SqlConnectionString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Holds";

                        //[OPTIONAL]: Map the Excel columns with that of the database table.
                        sqlBulkCopy.ColumnMappings.Add("Assigned", "QuotaQuantity");

                        sqlBulkCopy.ColumnMappings.Add("PriorityDate", "PriorityDate");

                        sqlBulkCopy.ColumnMappings.Add("RemainingQuantity", "ReminingQuantity");
                        sqlBulkCopy.ColumnMappings.Add("TempReminingQuantity", "TempReminingQuantity");
                        sqlBulkCopy.ColumnMappings.Add("Salesman", "userId");
                        sqlBulkCopy.ColumnMappings.Add("Tolerance", "Tolerance");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return false;
        }
    }
}
