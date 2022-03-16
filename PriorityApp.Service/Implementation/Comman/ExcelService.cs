using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Comman;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;


namespace PriorityApp.Service.Implementation.Comman
{
    public class ExcelService : IExcelService
    {
        private readonly ILogger<ExcelService> _logger ;
        private readonly IOrderService _orderService;

        public ExcelService(ILogger<ExcelService> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public MemoryStream ExportToExcel(List<OrderModel2> models)
        {
            try
            {
                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[21] { new DataColumn("Order Type"),
                                            new DataColumn("Priority Date"),
                                            new DataColumn("Area"),
                                            new DataColumn("Territory"),
                                            new DataColumn("Customer Number"),
                                            new DataColumn("Customer Name"),
                                            new DataColumn("Order Number"),
                                            new DataColumn("Ty"),
                                            new DataColumn("Line"),
                                            new DataColumn("Item"),
                                            new DataColumn("POD Alfa Name"),
                                            new DataColumn("Address"),
                                            new DataColumn("Zone"),
                                            new DataColumn("State"),
                                            new DataColumn("Qty"),
                                            new DataColumn("Priority"),
                                            new DataColumn("Comments"),
                                            new DataColumn("Truck"),
                                            new DataColumn("Status"),
                                            new DataColumn("SubmitDate"),
                                            new DataColumn("SubmitTime"),});


                foreach (var order in models)
                {
                    DateTime date = (DateTime) order.SubmitTime;
                    
                    dt.Rows.Add(order.OrderCategory.Name,order.PriorityDate,order.Customer.zone.Territory.state.Name, order.Customer.zone.Territory.Name, order.Customer.Id, 
                        order.Customer.CustomerName, order.OrderNumber,order.OrderDocument, order.LineID,
                        order.ItemId, order.PODName, order.PODZoneAddress,
                        order.PODZoneName, order.PODZoneState, order.PriorityQuantity,
                        order.Priority.Name, order.Comment, order.Comment, order.Status, date.Date.ToString("dd-MM-yyyy"), date.ToShortTimeString());
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream;
                        //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                    }
                }

            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
           // Area Customer Number Customer Name Order .	Ty Line    Item POD .	POD Alfa Name Address Zone State   Qty Priority    Comments Truck.	Status SubmitDate  SubmitTime

        }


        public MemoryStream ExportQuotaToExcel(List<HoldModel> models)
        {
            try
            {
                DataTable dt = new DataTable("DailyQuota");
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("PriorityDate"),
                                            new DataColumn("User"),
                                            new DataColumn("TodayHoldQuantity"),
                                            new DataColumn("YeasterdayHoldQuantity"),
                                             new DataColumn("ExtraQuantity"),
                                             new DataColumn("AssignedQuantity"),
                                            new DataColumn("QuotaQuantity"),
                                            new DataColumn("Transferred"),
                                            });


                foreach (var hold in models)
                {
                    float Assigned = hold.QuotaQuantity - hold.ReminingQuantity;

                    dt.Rows.Add(hold.PriorityDate, hold.UserName,  hold.ReminingQuantity,  hold.YeasterdayReminingQuantity ,  hold.ExtraQuantity, Assigned, hold.QuotaQuantity, hold.RemainingTranferred);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream;
                        //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
            // Area Customer Number Customer Name Order .	Ty Line    Item POD .	POD Alfa Name Address Zone State   Qty Priority    Comments Truck.	Status SubmitDate  SubmitTime

        }

        public DataTable ReadExcelData(string filePath, string excelConnectionString)
        {
            try
            {
                DataTable dt = new DataTable();
                excelConnectionString = string.Format(excelConnectionString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(excelConnectionString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                return dt;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public MemoryStream WritePickUpTemplateToExcel(List<ItemModel> models, List<CustomerModel> customerModels)
        {
            try
            {
                DataTable dt = new DataTable("PickupTemplate");

                if (customerModels != null)
                {
                    dt.Columns.AddRange(new DataColumn[8] { new DataColumn("CustomerNumber"),
                                            new DataColumn("CustomerName"),
                                            new DataColumn("Zone"),
                                            new DataColumn("State"),
                                            new DataColumn("PriorityDate"),
                                            new DataColumn("Priority"),
                                            new DataColumn("Comment"),
                                            new DataColumn("Truck"),
                                           });
                    foreach (var item in models)
                    {
                        dt.Columns.Add(new DataColumn(item.Name.ToString()));
                    }
                    foreach (var customer in customerModels)
                    {

                        dt.Rows.Add(customer.Id, customer.CustomerName, customer.zone.Name, customer.zone.Territory.state.Name);
                    }
                }
                else
                {
                    dt.Columns.AddRange(new DataColumn[6] {  new DataColumn("CustomerNumber"),     
                                            new DataColumn("CustomerName"),
                                            new DataColumn("PriorityDate"),
                                            new DataColumn("Priority"),
                                            new DataColumn("Comment"),
                                            new DataColumn("Truck"),
                                           });
                    foreach (var item in models)
                    {
                        dt.Columns.Add(new DataColumn(item.Name.ToString()));
                    }


                }
               

                

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream;
                        //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
            // Area Customer Number Customer Name Order .	Ty Line    Item POD .	POD Alfa Name Address Zone State   Qty Priority    Comments Truck.	Status SubmitDate  SubmitTime

        }


        public MemoryStream WritQuotaTemplateToExcel(List<AspNetUser> salesUsers)
        {
            try
            {
                DataTable dt = new DataTable("QuotaTemplate");

                if (salesUsers != null)
                {
                    dt.Columns.AddRange(new DataColumn[4] { 
                                            new DataColumn("Salesman"),
                                            new DataColumn("Assigned"),
                                            new DataColumn("Quota Date",System.Type.GetType("System.DateTime")),
                                            new DataColumn("Tolerance"),
                                           });
                    
                    foreach (var salesUser in salesUsers)
                    {

                        dt.Rows.Add(salesUser.UserName);
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream;
                        //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
            // Area Customer Number Customer Name Order .	Ty Line    Item POD .	POD Alfa Name Address Zone State   Qty Priority    Comments Truck.	Status SubmitDate  SubmitTime

        }


    }
}
