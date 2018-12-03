using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using ECX.DataAccess;
using WarehouseApplication.BLL;
namespace GINBussiness
{
    
    public class GINModel : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public string GINNumber { get; set; }
        public int AutoNumber { get; set; }
        public string TransactionId { get; set; }
        public string DriverName { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseIssuedBy { get; set; }
        public string PlateNumber { get; set; }
        public string TrailerPlateNumber { get; set; }
        public DateTime TruckRequestTime { get; set; }
        public DateTime TruckRegisterTime { get; set; }
        public string LeadInventoryController { get; set; }
        public Guid LeadInventoryControllerID { get; set; }
        public DateTime DateTimeLoaded { get; set; }
        public int AdjustmentTypeID { get; set; }
        public double WeightAdjustment { get; set; }
        public double BagAdjustment { get; set; }
        public double NetWeight { get; set; }
        public DateTime DateIssued { get; set; }
        public double NoOfRebags { get; set; }
        public int NoOfBags { get; set; }
        public string Remark { get; set; }
        public Single RemainingWeight { get; set; }
        public Guid WarehouseID { get; set; }
        public Guid LICShedID { get; set; }
        public Guid LastModifiedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public int GINStatusID { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
        public DateTime ClientSignedDate { get; set; }
        public string ClientSignedName { get; set; }
        public DateTime LICSignedDate { get; set; }
        public string LICSignedName { get; set; }
        public DateTime ManagerSignedDate { get; set; }
        public string ManagerSignedName { get; set; }
        public string GINClientStatusName { get; set; }
        public string GINLICStatusName { get; set; }
        public Guid CommodityGradeID { get; set; }
        public int ProductionYear { get; set; }
        public DateTime PUNPrintDateTime { get; set; }
        public Guid PUNPrintedBy { get; set; }
        public Boolean IsPSA { get; set; }
        public int DailyLabourersAssociationID { get; set; }

        public Guid Commodity { get; set; }
        public decimal Cupvalue { get; set; }
        public decimal RawValue { get; set; }
        public string SellerName { get; set; }
        public string Woreda { get; set; }
        public string WashingMillingStation { get; set; }
        public string ConsignmentType { get; set; }
        public string SustainableCertification { get; set; }
        public Double GrossWeight { get; set; }
        public Double TruckWeight { get; set; }
        public string Quadrant { get; set; }
        //public Guid LeadInventoryControllerID { get; set; }
        public Guid WeigherID { get; set; }
        public int WBServiceProviderID { get; set; }
        public string ScaleTicketNumber { get; set; }
        public string LoadUnloadTicketNO { get; set; }
        public int TotalNumberOfBags { get; set; }
       

        private List<PickupNoticeModel> punList = new List<PickupNoticeModel>();
        private List<PickupNoticeModel> wareHouseOperator = new List<PickupNoticeModel>();
        #region stack related

        public double StackWeight
        {
            get
            {
                if (stackList == null) return 0;
                if (stackList.Count == 0) return 0;
                double totalWeight = 0;
                foreach (StackTransactionModel sm in stackList)
                {
                   totalWeight += sm.NetWeight;
                }
                return totalWeight;
            }
        }
      

        public List<StackTransactionModel> stackList = new List<StackTransactionModel>();
        public List<StackTransactionModel> GetStacksLoaded()
        {
            if (stackList != null)
                return stackList;
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "tblStackLoadingGet", ID);
            stackList = new List<StackTransactionModel>();
            foreach (DataRow dr in dt.Rows)
            {
                StackTransactionModel o = new StackTransactionModel(this);
                Common.DataRow2Object(dr, o);
                AddStack(o);
            }


            return stackList;
        }

        public void AddStack(Guid stackId, Guid weigherID, string ticketNo, string LoadUnloadTicketNO,
                            DateTime dateWeighed,
                            double truckWeight,
                            double grossWeight,
                            double bags,
                            DateTime dateTimeLoaded,
                            int truckTypeID,
                            int wbServiceProviderID
            )
        {

            StackTransactionModel o = new StackTransactionModel(this);
            o.ID = Guid.NewGuid();
            o.StackID = stackId;
            o.GINID = ID;
            o.WeigherID = weigherID;
            o.DateTimeWeighed = dateWeighed;
            o.TruckWeight = truckWeight;
            o.GrossWeight = grossWeight;
            o.NoOfBags = bags;
            o.LoadUnloadTicketNO = LoadUnloadTicketNO;
            o.ScaleTicketNumber = ticketNo;
            o.DateTimeLoaded = dateTimeLoaded;
            o.TruckTypeID = truckTypeID;
            o.WBServiceProviderID = wbServiceProviderID;
            AddStack(o);
        }
        public void AddStack(StackTransactionModel sm)
        {
            if (stackList != null)
            {
                if (stackList.Exists(s => s.StackID == sm.StackID))
                    throw new Exception("Cannot add from the same stack");
                if (stackList.Exists(s => s.ScaleTicketNumber == sm.ScaleTicketNumber))
                    throw new Exception("Duplicate scale ticket no");
                if (stackList.Exists(s => s.LoadUnloadTicketNO == sm.LoadUnloadTicketNO))
                    throw new Exception("Duplicate Loading Ticket no");
            }
            stackList.Add(sm);
        }
        public List<StackTransactionModel> GetStacksList()
        {
            return stackList;
        }
        public void RemoveStack(Guid stackId)
        {
            if (stackList == null)
                GetStacksLoaded();
            if (!stackList.Exists(s => s.StackID == stackId))
                return;
            stackList.Remove(stackList.FindLast(s => s.StackID == stackId));
        }

        #endregion

        public override bool IsValid()
        {
            bool isValid = true;
            StringBuilder sb = new StringBuilder();

            //foreach (StackTransactionModel sm in StackInfosList)
            //{
            //    isValid = sm.IsValid();
            //    if (!isValid)
            //        sb.AppendLine(sm.ErrorMessage);
            //}
            if (StackInfosList.Count == 0)
            {
                isValid = false;
                throw new System.ArgumentException("You need to add to at least single stack.", "original"); 
                //sb.AppendLine("You need to add to at least single stack");
            }
            if (TruckRequestTime > TruckRegisterTime)
            {
               // sb.AppendLine("DN Submitted Time must be less than or equal to Truck Provided Time.");
                isValid = false;
                throw new System.ArgumentException("DN Submitted Time must be less than or equal to Truck Provided Time.", "original"); 
            }
            if (DateTimeLoaded < TruckRegisterTime)
            {
                //sb.AppendLine("Truck Provided Time should be earlier than Date loaded");
                isValid = false;
                throw new System.ArgumentException("Truck Provided Time should be earlier than Date loaded.", "original"); 
            }
            ErrorMessage = sb.ToString();
            return isValid;
        }



        public bool IsValidForPSA()
        {
            bool isValid = true;
            StringBuilder sb = new StringBuilder();

            if (StackInfosList.Count == 0)
            {
                isValid = false;
                sb.AppendLine("You need to add a stack");
            }

            ErrorMessage = sb.ToString();
            return isValid;
        }

        public String PickupNoticeId
        {
            get
            {

                string ids = punList.Select(s => s.ID.ToString()).Aggregate((str, next) => str + "," + next);
                return ids;
            }

        }
        public string PickupNotices
        {
            get
            {
                string punsXML;


                if (PickupNoticesList.Count == 0)
                {
                    punsXML = "<puns></puns>";
                }
                else
                {

                    IEnumerable<string> punNods = PickupNoticesList.Select(s => "<pungin>" +
                                                                "<PUNID>" + s.ID + "</PUNID>" +
                                                                "<Quantity>" + s.QuantityInLot + "</Quantity>" +
                                                                "<Weight>" + s.RemainingWeight + "</Weight>" +
                                                            "</pungin>");

                    punsXML = "<puns>" + punNods.Aggregate((str, next) => str + next) + "</puns>";
                }

                return punsXML;
            }
        }
        public List<PickupNoticeModel> PickupNoticesList
        {
            set
            {
                this.punList = value;
            }
            get
            {
                return punList;
            }
        }

        string stackInfoXML;
        public string StackInfos
        {            
            get
            {

                if (StackInfosList.Count == 0)
                {
                    stackInfoXML = "<stacks></stacks>";
                }
                else
                {
                    IEnumerable<string> stackNods = null; ;
                    if (this.StackInfosList != null)
                    {
                         stackNods = StackInfosList.Select(s => s.ToXML);
                    }
                    else
                    {
                       
                        stackInfoXML = "<stacks></stacks>";
                    }


                    stackInfoXML = "<stacks>" + stackNods.Aggregate((str, next) => str + next) + "</stacks>";
                }

                return stackInfoXML;
            }
            set
            {               
                stackInfoXML = "<stacks></stacks>";
            }
        }
        public List<StackTransactionModel> StackInfosList
        {
            get
            {
                return stackList;
            }
            set
            {
                stackList = value;
            }
        }
        public List<string> GetNewGINNumber()
        {
            List<string> str = new List<string>();
            DataRow dr = ECX.DataAccess.SQLHelper.getDataRow(ConnectionString, "spGetGINSNumber", UserBLL.GetCurrentWarehouse(), UserBLL.GetCurrentWarehouseCode());
            str.Add(dr["GINNumber"].ToString());
            str.Add(dr["AutoNumber"].ToString());
            return str;
        }
        public void PrepareGIN(string punIds)
        {
            DataTable PUNTable = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetPickupNotices", punIds);
            punList = new List<PickupNoticeModel>();
            foreach (DataRow r in PUNTable.Rows)
            {
                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(r, pnm);
                punList.Add(pnm);
            }

        }

        public void PrepareGINEdit(string punIds, Guid ID)
        {
            DataTable PUNTable = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetPickupNoticesEdit", punIds, ID);
            punList = new List<PickupNoticeModel>();
            foreach (DataRow r in PUNTable.Rows)
            {
                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(r, pnm);
                punList.Add(pnm);
            }

        }        
        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "GINSave", this);
        }

        public void SaveBondedYard()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "GINSaveBondedYard", this);
        }
        public void SavePSA()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "GINSavePSA", this);
        }
        public static void ApproveManager(string ApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "GINApproveManager", ApprovalXML);

        }
        public static void Approve(string ApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "GINApprove", ApprovalXML);

        }
        public static void ApproveManagerPSA(string ApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "PSAApproveManager", ApprovalXML);

        }
        public static void ApprovePSA(string ApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "PSAApprove", ApprovalXML);

        }

        public DataTable GetDailyPSA(string datefrom, string dateto)
        {
            return SQLHelper.getDataTable(ConnectionString, "[spGetDailyPSAReport]", datefrom, dateto);

        }

        public static void CancelGIN(string ginIds)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "CancelGINs", ginIds);

        }
        public static List<GINModel> Search()
        {
            return new List<GINModel>();
        }
        public DataTable PrintGIN()
        {
            return SQLHelper.getDataTable(ConnectionString, "PrintGoodIssuanceNote", ID);
        }
        public static DataTable PrintGINApprovalReport(Guid warehouseId, Guid selectedLIC,string LICName)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetAllUnapprovedGINReport", warehouseId, selectedLIC, LICName);
        }
        public static DataTable GetRemainingPUNBalance(string ID)
        {
            return SQLHelper.getDataTable(ConnectionString, "PreparePUN", ID);
        }
        public static DataTable GetRemainingGin(Guid ID,bool edit)
        {
            if(edit==false)
                return SQLHelper.getDataTable(ConnectionString, "getRemainigGIN", ID);
            else
                return SQLHelper.getDataTable(ConnectionString, "getRemainigGINPrint", ID);
        }
        public static DataTable GetRemainingGinByScale(Guid ID)
        {
            return SQLHelper.getDataTable(ConnectionString, "[getRemainigGINByScale]", ID);
        }
        public static DataTable GetRemainingPAS(Guid ID)
        {
            return SQLHelper.getDataTable(ConnectionString, "getRemainigPSA", ID);
        }
        
//-------- Update START ------- NOV 27 2013
        public static DataTable GetRemainingPASApproval(Guid ID)
        {
            return SQLHelper.getDataTable(ConnectionString, "getRemainigPSAApproval", ID);
        }
//-------- Update END ------- NOV 27 2013
        public static List<GINModel> SearchGIN(string clientIdNo, int whrNo, string status, string GINNo, Guid warehouseId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GINSearch", clientIdNo, GINNo, whrNo, status, warehouseId);
            List<GINModel> gmList = new List<GINModel>();
            foreach (DataRow dr in dt.Rows)
            {
                GINModel gm = new GINModel();
                Common.DataRow2Object(dr, gm);
                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(dr, pnm);
                gm.punList.Add(pnm);
                gmList.Add(gm);
            }

            return gmList;
        }

     //------- Create Start ------- March 20 2015

        public static DataTable SearchGIN2(string clientIdNo, int whrNo, string status, string GINNo, Guid warehouseId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GINSearch2", clientIdNo, GINNo, whrNo, status, warehouseId);
            return dt;
        }

    //-------- Create END   ------- March 20 2015

        public static GINModel GetGIN(string GINNo)
        {
            GINModel gm = new GINModel();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GINGet", GINNo);
            Common.DataRow2Object(dt.Rows[0], gm);
            List<Guid> pnIdList = new List<Guid>();
            List<Guid> sIdList = new List<Guid>();
            foreach (DataRow dr in dt.Rows)
            {
                Guid pnID = new Guid(dr["PUNID"].ToString());
                if (!pnIdList.Exists(s => s == pnID))
                {
                    pnIdList.Add(pnID);
                    PickupNoticeModel pnm = new PickupNoticeModel();
                    Common.DataRow2Object(dr, pnm);
                    pnm.ID = pnID;
                    gm.punList.Add(pnm);
                }

            }
            foreach (DataRow dr in dt.Rows)
            {
                StackTransactionModel sm = new StackTransactionModel(gm);
                Common.DataRow2Object(dr, sm);
                if (sm.StackTransactionID == Guid.Empty) break;
                if (!sIdList.Exists(s => s == sm.StackTransactionID))
                {
                    sIdList.Add(sm.StackTransactionID);
                    sm.ID = sm.StackTransactionID;
                    gm.stackList.Add(sm);
                }

            }
            return gm;
        }
        public static List<GINModel> getUnApprovedGinManager(string clientIdNo, int whrNo, string LICName, string GINNo, Guid warehouseId)
        {

            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getUnApprovedGinManager", clientIdNo, GINNo, whrNo, LICName, warehouseId);
            List<GINModel> gmList = new List<GINModel>();
            foreach (DataRow dr in dt.Rows)
            {
                GINModel gm = new GINModel();
                Common.DataRow2Object(dr, gm);

                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(dr, pnm);

                gm.punList.Add(pnm);
                gmList.Add(gm);
            }
            return gmList;
        }
        //public static DataTable getUnApprovedGinManager(string clientIdNo, int whrNo, Guid licId, string GINNo, Guid warehouseId)
        //{
        //    DataTable dt = SQLHelper.getDataTable(ConnectionString, "getUnApprovedGinManager", clientIdNo, GINNo, whrNo, licId, warehouseId);
        //    return dt;
        //}
        public static List<GINModel> getUnApprovedGin(string clientIdNo, int whrNo, string LICName, string GINNo, Guid warehouseId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getUnApprovedGin", clientIdNo, GINNo, whrNo, LICName, warehouseId);
            List<GINModel> gmList = new List<GINModel>();
            foreach (DataRow dr in dt.Rows)
            {
                GINModel gm = new GINModel();
                Common.DataRow2Object(dr, gm);
                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(dr, pnm);
                gm.punList.Add(pnm);
                gmList.Add(gm);
            }
            return gmList;
        }
        public static List<GINModel> getUnApprovedPSAManager(string clientIdNo, int whrNo, Guid? licId, string PSANo, Guid warehouseId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getUnApprovedPSAManager", clientIdNo, PSANo, whrNo, licId, warehouseId);
            List<GINModel> gmList = new List<GINModel>();
            foreach (DataRow dr in dt.Rows)
            {
                GINModel gm = new GINModel();
                Common.DataRow2Object(dr, gm);
                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(dr, pnm);
                gm.punList.Add(pnm);
                gmList.Add(gm);
            }
            return gmList;
        }
        public static List<GINModel> getUnApprovedPSA(string clientIdNo, int whrNo, Guid? licID, string PSANo, Guid warehouseId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getUnApprovedPSA", clientIdNo, PSANo, whrNo, licID, warehouseId);
            List<GINModel> gmList = new List<GINModel>();
            foreach (DataRow dr in dt.Rows)
            {
                GINModel gm = new GINModel();
                Common.DataRow2Object(dr, gm);
                PickupNoticeModel pnm = new PickupNoticeModel();
                Common.DataRow2Object(dr, pnm);
                gm.punList.Add(pnm);
                gmList.Add(gm);
            }
            return gmList;
        }
        public static void FillGINStatus(DropDownList ddl)
        {
            ListItem li = new ListItem("---- Select Status ----", ((int)GINStatusTypeEnum.New).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Client Accept", ((int)GINStatusTypeEnum.ClientAccept).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Client Reject", ((int)GINStatusTypeEnum.ClientReject).ToString());
            ddl.Items.Add(li);

            li = new ListItem("LIC Accept", ((int)GINStatusTypeEnum.LICAccept).ToString());
            ddl.Items.Add(li);

            li = new ListItem("LIC Reject", ((int)GINStatusTypeEnum.LICReject).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Manager Accept", ((int)GINStatusTypeEnum.ManagerAccept).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Manager Reject", ((int)GINStatusTypeEnum.ManagerReject).ToString());
            ddl.Items.Add(li);

        }
        public DataTable GetGINStackBalance(Guid WareHouseId, Guid ShedId, Guid LICID, Guid StackID)
        {
            return SQLHelper.getDataTable(ConnectionString, "[getGINStackBalance]", WareHouseId, ShedId, LICID, StackID);
        }
        public DataTable GetTotalGINStackById(Guid StackID)
        {
            return SQLHelper.getDataTable(ConnectionString, "[TotalGINStackById]", StackID);
        }

        public DataTable SearchDailyDeliveryList(string datefrom, string dateto)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "spGetDailyDelivery", datefrom, dateto);
            return dt;
        }
//-------- Update START ------NOV 27 2013
        public static DataTable GetPSA(string GINNumber, string ClientIDNo, int WHReceiptNo, Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetPSA", GINNumber, ClientIDNo, WHReceiptNo, WarehouseID);
        }
//-------- Update END --------NOV 27 2013
    }
    
    
}

public enum PickupNoticeStatusEnum
{
    Invalid = -1,
    OpenActive = 0,
    OpenExpiered = 5,
    BeingIssued = 1,
    BeingIssuedExpiered = 6,
    Closed = 3,
    Aborted = 4
}
public enum WareHouseOperatorTypeEnum
{
    Sampler = 5,
    Grader = 3,
    LIC = 2,
    Weigher = 7,
    Loader = 11
}
public enum GINStatusTypeEnum
{
    Start=0,
    New = 1,

    ClientAccept = 2,
    ClientReject = 3,

    LICAccept = 5,
    LICReject = 7,

    ManagerAccept = 11,
    ManagerReject = 13,
    Cancel = 17

}
public enum CommodityMaximumRangeEnum
{
    MaximumRange = 613
}



