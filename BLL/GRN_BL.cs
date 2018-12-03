using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ECX.DataAccess;

namespace WarehouseApplication.BLL
{
    public enum GRNStatusNew
    {
        NotCreated = 0,
        New = 1,
        LICApproved = 2,
        WarehouseSupervisorApproved = 3,
        Cancelled = 4
    }

    public class GRN_BL 
    {
        public enum WareHouseOperatorTypeEnum
        {
            Sampler = 5,
            Grader = 3,
            LIC = 2,
            Weigher = 7,
            Loader = 11
        }

        #region Public Properties
        public string TrackingNumber { get; set; }//Get it from Query String
        public Guid ID { get; set; }//Generate New
        public int TotalNumberOfBags { get; set; }//Interface
        public int RebagingQuantity { get; set; }//Interface
        public string UnloadingTicketNumber { get; set; }
        public DateTime DateDeposited { get; set; }//Interface
        //public float Quantity { get; set; }//????????
        public Guid UserID { get; set; }//UserBLL
        public Guid StackTransactionID { get; set; }//Generate new
        public Guid ShedID { get; set; }//Interface
        public Guid StackID { get; set; }//Interface
        public Guid LeadInventoryController { get; set; }//Interface
        public Guid WeigherID { get; set; }//Interface
        public int WBServiceProviderID { get; set; }//Interface
        public int LabourerGroup { get; set; }//Interface        
        public DateTime DateTimeWeighed { get; set; }//Interface
        public string ScaleTicketNumber { get; set; }//Interface
        public float GrossWeight { get; set; }//Interface
        public float TruckWeight { get; set; }//Interface
        public float NetWeight { get; set; }
        public Guid BagTypeID { get; set; }
        public float NoOfBags { get; set; }//Interface
        public float Tare { get; set; }//Calculated
        public float Tolerance { get; set; }//Configuration AppSetting
        public string Remark { get; set; }//Interface
        public string GradingIdsWithSameGradeResult { get; set; }//used to handle multiple gradings in one GRN
        public Guid ArrivalId { get; set; }
        public bool IsScaleTicketSigned { get; set; }
        public int TruckTypeID { get; set; }
        public int ConsignmentType { get; set; }
        public int PlacedTo { get; set; }
        public string ArrivalCert { get; set; }
        public decimal RawValue { get; set; }
        public decimal CupValue { get; set; }
        public decimal TotalValue { get; set; }
        public Guid WoredaID { get; set; }
        public Boolean IsTracable { get; set; }
        public string ProcessingCenter { get; set; }
        public string Quadrant { get; set; }
        public string TrailerPlateNumber { get; set; }
        public string TruckPlateNumber { get; set; }
        public string Shade { get; set; }

        public string GradesToBeClosedQuery { get; set; }
        public string GradingsMergedAfterSegreggationXML { get; set; }

        //public Guid CommodityGradeId { get; set; }
        #endregion

        #region Business Methods
        string conn = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ToString();
        public void SaveGRN()
        {
            try
            {
                ECX.DataAccess.SQLHelper.Save(conn, "GRNsSave", this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        /// <summary>
        /// Gets history information about GRN, Grading info, Truck info, client are included
        /// </summary>
        /// <param name="TrackingNumber"> Trucking number of GradingResult/not CDR's used as a selection key</param>
        /// <returns></returns>
        public DataTable GetDisplayInfo(string TrackingNumber)
        {
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_UnloadingDisplayInfo", TrackingNumber);//GetUnloadingDisplayInfo
            return dt;
        }

        public double getmoisturecontentbytrNo(string GradingCode)
        {
            double moiscont;
            moiscont = Convert.ToDouble(ECX.DataAccess.SQLHelper.ExecuteScalar(conn, "GetMoistContentByGradingcode", GradingCode));
            return moiscont;
        }

        public DataTable GetDisplayInfoByGradingCode(string GradingCode)
        {
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_UnloadingDisplayInfoByGradingCode", GradingCode);//GetUnloadingDisplayInfo
            return dt;
        }
        /// <summary>
        /// Gets records with same arrival id and same grading result after a suggrigation/branch out @ grading level
        /// </summary>
        /// <param name="TrackingNumber"> Trucking number of GradingResults/not Arrival used as a selection key</param>
        /// <returns></returns>
        public DataTable GetGradingsWithSameResults(string TrackingNumber)
        {
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_SameGradingResults", TrackingNumber);//GetSameGradingResults
            return dt;
        }
        /// <summary>
        /// Gets all WB Service Providers with only Id and NameOfServiceProvider. 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllWBServiceProviders(Guid WHID )
        {
            DataTable dt;
            try
            {
                 dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "GetAllWBServiceProvidersByWHID", WHID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }

        public DataTable getGradingsDetailbyGRCode(string gradingcode)
        {
            DataTable dt;
            dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "sp_getGradingsDetailbyGRCode", gradingcode);
            return dt;
        }


        /// <summary>
        /// Get all Labourer groups, with their id and Group name
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLabourerGroup()
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "GetAllLabourerGroup", UserBLL.GetCurrentWarehouse());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Get all Trucks
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTrucks()
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_AllTrcuks");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Get all inventory controllers by warehouse and their operation type(LIC,Weigher..)
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInventoryControllers(Guid warehouse)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "spGetActiveOperatorsPerWarehouse", warehouse, WareHouseOperatorTypeEnum.LIC);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Get all inventory controllers by warehouse and their operation type(LIC,Weigher..)
        /// </summary>
        /// <returns></returns>
        public DataTable GetInventoryControllersByShed(Guid ShedID)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_LIC_ByShed", ShedID, WareHouseOperatorTypeEnum.LIC);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Get all inventory controllers by warehouse and their operation type(LIC,Weigher..)
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllWeighers(Guid warehouse)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "spGetActiveOperatorsPerWarehouse", warehouse, WareHouseOperatorTypeEnum.Weigher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Gets all apropriate bag types based on the commodity grade
        /// </summary>
        /// <param name="CommodityGradeId"></param>
        /// <returns></returns>
        public DataTable GetBagTypesByGradeId(Guid CommodityGradeId)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_BagTypesByCommodityGradeId", CommodityGradeId);//GetBagTypes_By_CommodityGradeId
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
            
        }
        /// <summary>
        /// Get all Shades under warehouse, which contains stacks with known production year and grade
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllAvailableSheds(Guid WarehouseId, Guid CommodityGradeId, int ProductionYear)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_All_Available_Sheds", WarehouseId, CommodityGradeId, ProductionYear);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Get all Stacks under selected sheds, with same CommodityGradeId and ProductionYear 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllAvailableStacks(Guid ShedId, Guid CommodityGradeId, int ProductionYear)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_All_Available_Stacks", ShedId, CommodityGradeId, ProductionYear);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        /// <summary>
        /// Gets GRN recordby id
        /// </summary>
        /// <returns>GrnId</returns>
        /// 
        public DataTable PopulateGrnById(Guid GrnId)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_GRNById", GrnId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;

        }
        /// <summary>
        /// Get GRN report by GRN id
        /// </summary>
        /// <returns></returns>
        public DataTable GetGRNReport(Guid GrnId)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_GRNReport", GrnId);//GetGRN_Report_New
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;            
        }

        public DataTable GetGradingsWithSameGRNReport(string GRNNumber)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_GradingResultsWithSameGRN", GRNNumber);//GetGRN_Same_Gradings
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }

        public DataTable GetGradingResultFactorReport(string GradingCode)
        {
            DataTable dt;
            try 
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_GradingResultFactorsReport", GradingCode);//Get_GradingResult_Report
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt; 
        }

        public DataTable SearchRecords(string selectedCriteria, string value)
        {

            string warehouseId = " And WarehouseId = " + "'" + UserBLL.GetCurrentWarehouse().ToString() + "'";

            DataTable dt = null;
            string searchingQuery = "";
            SqlParameter param = null;
            try
            {
                if (selectedCriteria == "8")
                {
                    param = new SqlParameter("@DepositTicketNumber", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = "SELECT *, " + value + " As SerchingParameter FROM vwSearch2 WHERE LoadUnloadTicketNO = @DepositTicketNumber" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }

                else if (selectedCriteria == "7")
                {
                    param = new SqlParameter("@ScaleTicketNumber", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = "SELECT *, '" + value + "' As SerchingParameter FROM vwSearch2 WHERE ScaleTicketNumber = @ScaleTicketNumber" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }

                else if (selectedCriteria == "6")
                {
                    param = new SqlParameter("@VoucherNumber", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = "SELECT *, '" + value + "' As SerchingParameter FROM vwSearch2 WHERE VoucherNumber = @VoucherNumber" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }
                else if (selectedCriteria == "5")
                {
                    param = new SqlParameter("@ClientID", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = @"SELECT dbo.vwSearch2.*, '" + value + "' As SerchingParameter FROM dbo.vwSearch2 INNER JOIN dbo.clMemberClients ON dbo.vwSearch2.ClientID = dbo.clMemberClients.ID " +
                                        "WHERE dbo.clMemberClients.IDNo = @ClientID" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }
                else if (selectedCriteria == "4")
                {
                    param = new SqlParameter("@GrnNumber", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = "SELECT *, '" + value + "' As SerchingParameter FROM vwSearch2 WHERE GRN_Number = @GrnNumber" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }
                else if (selectedCriteria == "3")
                {
                    param = new SqlParameter("@GradingCode", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = @"SELECT *, '" + value + "' As SerchingParameter FROM vwSearch2 WHERE GradingCode = @GradingCode" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }
                else if (selectedCriteria == "2")
                {
                    param = new SqlParameter("@SampleCode", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = @"SELECT *, '" + value + "' As SerchingParameter FROM vwSearch2 WHERE SampleCode = @SampleCode" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }
                else if (selectedCriteria == "1")
                {
                    param = new SqlParameter("@TrackingNumber", SqlDbType.NVarChar, 50);
                    param.Value = value;
                    searchingQuery = @"SELECT *, '" + value + "' As SerchingParameter FROM vwSearch2 WHERE TrackingNumber = @TrackingNumber" + warehouseId;
                    dt = ECX.DataAccess.SQLHelper.ExecuteSqlCommand(conn, searchingQuery, param);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public DataTable getArrivalCert(Guid ArrivalId)
        {
           return ECX.DataAccess.SQLHelper.getDataTable(conn, "GetArrivalCertificate", ArrivalId);
        }
        
        public GRNBLL GetWarehouseReciptByGRNNumber(string GRNNumber)
        {          
            GRNBLL objGrn = new GRNBLL();
            DataRow dr;
            try
            {                
                dr = ECX.DataAccess.SQLHelper.getDataRow(conn, "Get_WarehouseReceiptRelatedByGRNNumber", GRNNumber);//GetGRN_Report_New
                objGrn.CommodityRecivingId=new Guid(dr["ArrivalID"].ToString());
                objGrn.Id=new Guid(dr["GRNID"].ToString());
                objGrn.GRN_Number=dr["GRNNumber"].ToString();
                objGrn.CommodityGradeId=new Guid(dr["CommodityGradeID"].ToString());
                objGrn.WarehouseId=new Guid(dr["WarehouseID"].ToString());
                objGrn.BagTypeId=new Guid(dr["BagTypeID"].ToString());
                objGrn.GradingId=new Guid(dr["GradingID"].ToString());
                objGrn.DateDeposited=DateTime.Parse(dr["DateDeposited"].ToString());

                //objGrn.ApprovedTimeStamp=DateTime.Parse(dr["ApprovedTimeStamp"].ToString());   
                objGrn.ApprovedTimeStamp = DateTime.Now;
                objGrn.Status = 4;// int.Parse(dr["GRNStatus"].ToString());
                objGrn.GrossWeight=float.Parse(dr["GrossWeight"].ToString());
                
                objGrn.NetWeight=float.Parse(dr["NetWeight"].ToString());
                objGrn.OriginalQuantity=float.Parse(dr["OriginalQuantity"].ToString());
                objGrn.CurrentQuantity=float.Parse(dr["CurrentQuantity"].ToString());
      
                objGrn.ClientId=new Guid(dr["ClientID"].ToString());
                objGrn.TotalNumberOfBags=int.Parse(dr["TotalNumberOfBags"].ToString());
                objGrn.ProductionYear=int.Parse(dr["ProductionYear"].ToString());
                objGrn.ConsignmentType = int.Parse(dr["ConsignmentType"].ToString());
                objGrn.RawValue = decimal.Parse(dr["RawValue"].ToString());
                objGrn.IsTracable = Boolean.Parse(dr["IsTraceable"].ToString());
                objGrn.Shade = dr["Shade"].ToString();
                objGrn.CupValue = decimal.Parse(dr["CupValue"].ToString());
                objGrn.TotalValue = decimal.Parse(dr["RawValue"].ToString()) + decimal.Parse(dr["CupValue"].ToString());
                objGrn.Woreda = new Guid(dr["Woreda"].ToString());
                objGrn.ProcessingCenter = dr["ProcessingCenter"].ToString();
                objGrn.TruckPlateNumber = dr["TruckPlateNumber"].ToString();
                objGrn.TrailerPlateNumber = dr["TrailerPlateNumber"].ToString();
                objGrn.ArrivalCert = dr["ArrivalCert"].ToString();
                //this.DepositTypeId = new Guid();
                //NetWeight as NetWeightAdjested, ???
                //objGrn.Source = 1;
                //NetWeight as NetWeightAdjested, ???

                objGrn.CreatedBy = UserBLL.GetCurrentUser();
                objGrn.CreatedTimestamp = DateTime.Now;

                objGrn.LastModifiedBy = UserBLL.GetCurrentUser();
                objGrn.LastModifiedTimestamp = DateTime.Now;
                objGrn.GRNTypeId = new Guid("03a15f0a-5cc0-4a10-9751-983b4a002665");

                objGrn.VoucherId = Guid.Empty;
                objGrn.ScalingId = Guid.Empty;
                objGrn.UnLoadingId = Guid.Empty;

                objGrn.SamplingTicketId = Guid.Empty;
                
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return objGrn;         
        }
        /// <summary>
        /// Gets StockBalance Report and filter with 3 specified parameters listed
        /// </summary>
        /// <returns></returns>
        public DataTable GetStockBalanceReport(Guid warehouse, Guid shed, Guid LIC, DateTime StartDate, DateTime EndDate)
        {
            DataTable dt;
            try 
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_ReportStockBalance", warehouse, shed, LIC, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt; 
        }
        /// <summary>
        /// Gets WBService Providers Report
        /// </summary>
        /// <returns></returns>
        public DataTable GetWBServiceProviderReport(Guid warehouseId, int wbServiceProviderId, DateTime startDate, DateTime endDate, int serviceType)
        {
            DataTable dt;
            try 
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_ReportWBServiceProvider", warehouseId, wbServiceProviderId, startDate, endDate, serviceType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt; 
        }
        /// <summary>
        /// Gets Waiting Trucks Report 
        /// </summary>
        /// <returns></returns>
        public DataTable GetWaitingTrucksReport()
        {
            DataTable dt;
            try 
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_ReportWaitingTrucks");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt; 
        }
        
        /// <summary>
        /// Gets GIN Operations and Service Line Aggrement Report 
        /// </summary>
        /// <returns></returns>
        public DataTable GetGINOperationReport()
        {
            DataTable dt;
            try 
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_ReportGINOperation");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt; 
        }

        /// <summary>
        /// Get GRN Stack Balance report 
        /// </summary>
        /// <returns></returns>
        public DataTable GetGRNStackBalanceReport()
        {
            return SQLHelper.getDataTable(conn, "[getGRNStackBalance]");
        }

        /// <summary>
        /// Get GRN Stack Balance report 
        /// </summary>
        /// <returns></returns>
        public DataTable GetGRNStackBalanceReport(Guid WareHouse, Guid ShedID, Guid LICID, Guid ID)
        {
            return SQLHelper.getDataTable(conn, "[getGRNStackBalance]", WareHouse, ShedID, LICID, ID);
        }
        public DataTable GetTotalGRNStackById(Guid ID)
        {
            return SQLHelper.getDataTable(conn, "[TotalGRNStackById]", ID);
        }        
        public DataTable getLabReport()
        {
            return SQLHelper.getDataTable(conn, "[getLabReport]");
        }
        public DataTable getLabGrader(string GradingCode)
        {
            return SQLHelper.getDataTable(conn, "[getLabGrader]", GradingCode);
        }
        public DataTable getDailyLaborersPaymentReport()
        {
            return SQLHelper.getDataTable(conn, "[getDailyLaborersPaymentReport]");
        }

        public  DataTable GetStockBalanceReportGIN(Guid warehouse, Guid shed, Guid LIC, DateTime StartDate, DateTime EndDate)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(conn, "Get_ReportStockBalanceGIN", warehouse, shed, LIC, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }

        public DataTable SearchDailyDepositList(string datefrom, string dateto)
        {
            DataTable dt = SQLHelper.getDataTable(conn, "spGetDailyDeposit", datefrom, dateto);
            return dt;
        }
        #endregion
    }
}