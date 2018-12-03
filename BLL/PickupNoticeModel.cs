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
    public class PickupNoticeModel : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid ClientID { get; set; }
        public string ClientIDNo { get; set; }
        public string ClientName { get; set; }
        public Guid MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberIDNo { get; set; }
        public Guid RepID { get; set; }
        public string RepName { get; set; }
        public string RepIDNo { get; set; }
        public string AgentName { get; set; }
        public string AgentIDNumber { get; set; }
        public int IDTypeID { get; set; }
        public string IDType { get; set; }
        public string AgentTel { get; set; }
        public Guid CommodityGradeID { get; set; }
        public string CommodityName { get; set; }
        public int ProductionYear { get; set; }
        public Guid WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public Guid ShedID { get; set; }
        public string ShedName { get; set; }
        public Guid WarehouseReceiptID { get; set; }
        public int WarehouseReceiptNo { get; set; }

        public string GRNNo { get; set; }
        public Double QuantityInLot { get; set; }
        public Double WeightInKg { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ExpectedPickupDateTime { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public double PreviousQty { get; set; }
        public string PreviousGIN { get; set; }
        public Double RemainingWeight { get; set; }
        public string Name { get; set; }
        public string GINNumber { get; set; }
        public Guid PUNPrintedBy { get; set; }
        public bool PUNPrinted { get; set; }
        public DateTime PUNPrintDateTime { get; set; }
        public Double MaxLimit { get; set; }
        public string Measurement { get; set; }
        public int UnApprovedGINCount { get; set; }
        public Guid Commodity { get; set; }
        public Guid LeadInventoryControllerID { get; set; }
        public Guid WeigherID { get; set; }
        public int WBServiceProviderID { get; set; }
        public string ScaleTicketNumber { get; set; }
        public decimal Cupvalue { get; set; }
        public decimal RawValue { get; set; }
        public string SellerName { get; set; }
        public string Woreda { get; set; }
        public string WashingMillingStation { get; set; }
        public string ConsignmentType { get; set; }
        public string PlateNumber { get; set; }
        public string SustainableCertification { get; set; }
        public Double GrossWeight { get; set; }
        public Double TruckWeight { get; set; }
        public int TotalNumberOfBags { get; set; }
        public string Quadrant { get; set; }
        public override string ToString()
        {
            return ID.ToString();
        }
        public bool Selected { get; set; }
        public string Remark { get; set; }
        public string MWarehouseReceiptNo { get; set; }
        public bool IsOpen
        {
            get
            {
                return StatusID == (int)PickupNoticeStatusEnum.OpenActive;
            }
        }
        public bool IsBeingIssued
        {
            get
            {
                return StatusID == (int)PickupNoticeStatusEnum.BeingIssued;
            }
        }
        public static List<PickupNoticeModel> PreparePUNId(string punIds)
        {
            List<PickupNoticeModel> punList = new List<PickupNoticeModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PreparePUN", punIds);
            foreach (DataRow dr in dt.Rows)
            {
                PickupNoticeModel o = new PickupNoticeModel();
                Common.DataRow2Object(dr, o);
                punList.Add(o);
            }
            return punList;
        }
        public static List<PickupNoticeModel> Search()
        {
            return new List<PickupNoticeModel>();
        }
        private List<PickupNoticeModel> lstSearch = new List<PickupNoticeModel>();
        public List<PickupNoticeModel> lstSerchList
        {
            set
            {
                this.lstSearch = value;
            }
            get
            {
                return lstSearch;
            }
        }
        public void Search(string clientIdNo, int whrNo, string status, Guid warehouseID, DateTime expirationDateFrom, DateTime expirationDateTo)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PickupNoticeSearch", clientIdNo, whrNo, status, warehouseID, expirationDateFrom.Date, expirationDateTo.Date);
            lstSearch = new List<PickupNoticeModel>();
            foreach (DataRow dr in dt.Rows)
            {
                PickupNoticeModel o = new PickupNoticeModel();
                Common.DataRow2Object(dr, o);
                lstSearch.Add(o);
            }
        }
        public static DataTable SearchExpieredList(Guid warehouseID, DateTime expirationDateFrom, DateTime expirationDateTo)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PickupNoticeExpiredList", warehouseID, expirationDateFrom.Date, expirationDateTo.Date);
            return dt;

        }
        public static DataTable SearchExpieredListAdmin(DateTime expirationDateFrom, DateTime expirationDateTo)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PickupNoticeExpiredListAdmin", expirationDateFrom.Date, expirationDateTo.Date);
            return dt;

        }
        public static DataTable PrintSubReportPUN(Guid Id)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "[subReportPUN]", Id);
            return dt;
        }
        public static List<PickupNoticeModel> PrintPUN(string Id)
        {
            List<PickupNoticeModel> lst = new List<PickupNoticeModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PrintPickupNotice", Id, WarehouseApplication.BLL.UserBLL.CurrentUser.UserId);
            Guid punId = Guid.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                PickupNoticeModel o = new PickupNoticeModel();
                Common.DataRow2Object(dr, o);

                lst.Add(o);
                punId = o.ID;

            }
            return lst;
        }
        public static List<PickupNoticeModel> PrintPUNChecking(string Id)
        {
            List<PickupNoticeModel> lst = new List<PickupNoticeModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "CheckunPrint", Id, WarehouseApplication.BLL.UserBLL.CurrentUser.UserId);
            Guid punId = Guid.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                PickupNoticeModel o = new PickupNoticeModel();
                Common.DataRow2Object(dr, o);

                lst.Add(o);
                punId = o.ID;

            }
            return lst;
        }
        public static DataTable PrintGIN(Boolean Edit, Guid GINId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PrintGoodIssuanceNote", GINId);           
            DataColumn dc = new DataColumn();
            dc = new DataColumn("Edit", typeof(Boolean));
            dt.Columns.Add(dc);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                dt.Rows[i]["Edit"] = Edit;
            }
            return dt;
        }
        public static DataTable PrintPSA(Guid PSAId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PrintPostSettelmentAdjustment", PSAId);
            return dt;
        }
        public static DataTable PrintPSA_Approved(Guid PSAId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getRemainigPSAApproval_Approved", PSAId);
            return dt;
        }
        public static void SelectPickupNotice(PickupNoticeModel notice, bool select, DateTime dnPresentedDate)
        {
            notice.Selected = select;
        }
        public void SelectPickupNotice(bool select)
        {
            Selected = select;
        }
        public static void FillPickupNoticeStatus(DropDownList ddl)
        {
            ListItem li = new ListItem("---- Select Status ----", ((int)PickupNoticeStatusEnum.Invalid).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Open Active", ((int)PickupNoticeStatusEnum.OpenActive).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Open Expired", ((int)PickupNoticeStatusEnum.OpenExpiered).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Being Issued Active", ((int)PickupNoticeStatusEnum.BeingIssued).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Being Issued Expiered", ((int)PickupNoticeStatusEnum.BeingIssuedExpiered).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Closed", ((int)PickupNoticeStatusEnum.Closed).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Aborted", ((int)PickupNoticeStatusEnum.Aborted).ToString());
            ddl.Items.Add(li);

        }
        public void SaveWHR()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "RePrintPUN", this);
        }

    }
}