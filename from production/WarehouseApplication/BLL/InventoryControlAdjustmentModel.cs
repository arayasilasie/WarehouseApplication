using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GINBussiness;
using System.Data;
using ECX.DataAccess;
using WarehouseApplication.BLL;
using System.Text;

namespace InventoryControlBussiness
{
    [Serializable]
    public class InventoryControlModel : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid ShedID { get; set; }
        public DateTime InventoryDate { get; set; }
        public Guid LICID { get; set; }
        public int InventoryReasonID { get; set; }
        public Guid WarehouseID { get; set; }
        public Guid ApprovedByID { get; set; }
        public DateTime ApprovalDate { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public Guid PSAID { get; set; }
        public string psaApprovalInfoXML { get; set; }
        public Guid PSAStackID { get; set; }

        public List<InventoryDetail> inventoryDetailList;
        public void addInventoryDetail(InventoryDetail inventoryDetail)
        {
            if (inventoryDetailList == null)
                inventoryDetailList = new List<InventoryDetail>();
            inventoryDetailList.Add(inventoryDetail);

        }
        public string inventoryDetail
        {
            get
            {
                string inventoryDetailinfoXML;

                if (inventoryDetailList == null || inventoryDetailList.Count == 0)
                {
                    inventoryDetailinfoXML = "<Inventory></Inventory>";
                }
                else
                {

                    IEnumerable<string> inventoryDetailNods = inventoryDetailList.Select(s => s.ToXML);

                    inventoryDetailinfoXML = "<Inventory>" + inventoryDetailNods.Aggregate((str, next) => str + next) + "</Inventory>";
                }
                return inventoryDetailinfoXML;
            }
        }

        public string inventoryDetailApproval
        {
            get
            {
                string inventoryDetailinfoXML;

                if (inventoryDetailList == null || inventoryDetailList.Count == 0)
                {
                    inventoryDetailinfoXML = "<Inventory></Inventory>";
                }
                else
                {

                    IEnumerable<string> inventoryDetailNods = inventoryDetailList.Select(s => s.ToXMLForApprove);

                    inventoryDetailinfoXML = "<Inventory>" + inventoryDetailNods.Aggregate((str, next) => str + next) + "</Inventory>";
                }
                return inventoryDetailinfoXML;
            }
        }

        public string inventoryDetailForPSA
        {
            get
            {
                string inventoryDetailinfoXML;

                if (inventoryDetailList == null || inventoryDetailList.Count == 0)
                {
                    inventoryDetailinfoXML = "<Inventory></Inventory>";
                }
                else
                {

                    IEnumerable<string> inventoryDetailNods = inventoryDetailList.Select(s => s.ToXMLForPSA);

                    inventoryDetailinfoXML = "<Inventory>" + inventoryDetailNods.Aggregate((str, next) => str + next) + "</Inventory>";
                }
                return inventoryDetailinfoXML;
            }
        }

        public List<InventoryInspector> inventoryInspectorList;
        public void addInventoryInspector(InventoryInspector inventoryInspector)
        {
            if (inventoryInspectorList == null)
                inventoryInspectorList = new List<InventoryInspector>();
            inventoryInspectorList.Add(inventoryInspector);

        }
        public string inventoryInspectors
        {
            get
            {
                string inventoryInspectorinfoXML;

                if (inventoryInspectorList == null || inventoryInspectorList.Count == 0)
                {
                    inventoryInspectorinfoXML = "<Inventory></Inventory>";
                }
                else
                {

                    IEnumerable<string> inventoryInspectorNods = inventoryInspectorList.Select(s => s.ToXML);

                    inventoryInspectorinfoXML = "<Inventory>" + inventoryInspectorNods.Aggregate((str, next) => str + next) + "</Inventory>";
                }
                return inventoryInspectorinfoXML;
            }
        }

        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveInventoryControl", this);
        }

        public void SaveForPSA()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveInventoryControlForPSA", this);
        }

        //------ Update START NOV 27 - 2013
        public void SaveForPSA_Rev()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveInventoryControlForPSA_Rev", this);
        }
        //------ update END
        public void Approve()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "ApproveInventoryControl", this);
        }

        public static DataTable GetShedByWarehouse(Guid warehouseID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllSheds", warehouseID);
            return dt;
        }

        public static DataTable GetShedForApproval(Guid warehouseID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetShedForApproval", warehouseID);
            return dt;
        }

        public static DataTable GetLIC(Guid shedID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetLICForInventory", null, shedID);
            return dt;
        }

        public static DataTable GetLICForApproval(Guid shedID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetLICForInventoryApproval", null, shedID);
            return dt;
        }

        public static DataTable GetInvDatesForInvApproval(Guid shedID, Guid LICID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInvDatesForInvApproval", null, shedID, LICID);
            return dt;
        }

        public static DataTable GetInventoryReason()
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInventoryReasons");
            return dt;
        }

        public static DataTable GetInventoryInspectors(Guid warehouseID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInventoryInspectors", warehouseID);
            return dt;
        }
        public static DataTable GetOperationControllers(Guid warehouseID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetOperationControllers", warehouseID);
            return dt;
        }

        public static DataTable GetInventoryDetail(Guid shedID, Guid LICID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInventoryDetailByShedAndLIC", shedID, LICID);
            return dt;
        }

        //------- Update start NOV 27 - 2013
        public static DataTable GetInventoryDetail(Guid shedID, Guid LICID, Guid CGID, string PY)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInventoryDetailByShedLICcGIDandPY", shedID, LICID, CGID, PY);
            return dt;
        }
        //------  end
        public static DataTable GetInvDetailByForApproval(Guid shedID, Guid LICID , DateTime inventoryDate)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInvDetailByForApproval", shedID, LICID, inventoryDate);
            return dt;
        }

        public override bool IsValid()
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;
            if (this.inventoryDetailList.Count <= 0)
            {
                message.AppendLine("The inventory control must have atleast one stack in its detail. <br/>");
                isValid = false;
            }
            if(this.inventoryInspectorList.Count <= 0)
            {
                message.AppendLine("The inventory control must have atleast one inspector. <br/>");
                isValid = false;
            }
            ErrorMessage = message.ToString();
            return isValid;

        }

        public static bool InventoryControlExits(InventoryControlModel icm)
        {
            bool exits = false;
            bool.TryParse(SQLHelper.ExecuteScalar(ConnectionString, "InventoryControlExits", icm.ShedID, icm.LICID, icm.InventoryDate).ToString(), out exits);
            return exits;
        }
    }

    [Serializable]
    public class InventoryDetail : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid InventoryID { get; set; }
        public Guid StackID { get; set; }
        public float PhysicalCount { get; set; }
        public float SystemCount { get; set; }
        public float PhysicalWeight { get; set; }
        public float SystemWeight { get; set; }
        public float AdjustmentCount { get; set; }
        public float AdjustmentWeight { get; set; }
        public Guid ApprovedByID { get; set; }
        public DateTime ApprovalDate { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public int Status { get; set; }

        public string ToXML
        {
            get
            {
                return "<InventoryDetail> " +
                        "<InventoryID>" + InventoryID.ToString() + "</InventoryID>" +
                        "<StackID>" + StackID.ToString() + "</StackID>" +
                        "<PhysicalCount>" + PhysicalCount.ToString() + "</PhysicalCount>" +
                        "<SystemCount>" + SystemCount.ToString() + "</SystemCount>" +
                        "<PhysicalWeight>" + PhysicalWeight.ToString() + "</PhysicalWeight>" +
                        "<SystemWeight>" + SystemWeight.ToString() + "</SystemWeight>" +
                        "<AdjustmentCount>" + AdjustmentCount.ToString() + "</AdjustmentCount>" +
                        "<AdjustmentWeight>" + AdjustmentWeight.ToString() + "</AdjustmentWeight>" +
                        "</InventoryDetail>";
            }
        }

        public string ToXMLForApprove
        {
            get
            {
                return "<InventoryDetail> " +
                       "<ID>" + ID.ToString() + "</ID>" +
                        "<InventoryID>" + InventoryID.ToString() + "</InventoryID>" +
                        "<StackID>" + StackID.ToString() + "</StackID>" +
                        "<ApprovedByID>" + ApprovedByID.ToString() + "</ApprovedByID>" +
                        "<ApprovalDate>" + ApprovalDate.ToString() + "</ApprovalDate>" +
                        "<LastModifiedBy>" + LastModifiedBy.ToString() + "</LastModifiedBy>" +
                        "<LastModifiedTimestamp>" + LastModifiedTimestamp.ToString() + "</LastModifiedTimestamp>" +
                        "<Status>" + Status.ToString() + "</Status>" +
                        "</InventoryDetail>";
            }
        }

        public string ToXMLForPSA
        {
            get
            {
                return "<InventoryDetail> " +
                        "<InventoryID>" + InventoryID.ToString() + "</InventoryID>" +
                        "<StackID>" + StackID.ToString() + "</StackID>" +
                        "<PhysicalCount>" + PhysicalCount.ToString() + "</PhysicalCount>" +
                        "<SystemCount>" + SystemCount.ToString() + "</SystemCount>" +
                        "<PhysicalWeight>" + PhysicalWeight.ToString() + "</PhysicalWeight>" +
                        "<SystemWeight>" + SystemWeight.ToString() + "</SystemWeight>" +
                        "<AdjustmentCount>" + AdjustmentCount.ToString() + "</AdjustmentCount>" +
                        "<AdjustmentWeight>" + AdjustmentWeight.ToString() + "</AdjustmentWeight>" +
                        "<ApprovedByID>" + ApprovedByID.ToString() + "</ApprovedByID>" +
                        "<ApprovalDate>" + ApprovalDate.ToString() + "</ApprovalDate>" +
                        "<Status>" + Status.ToString() + "</Status>" +
                        "</InventoryDetail>";
            }
        }

    }

    [Serializable]
    public class InventoryInspector
    {
        public Guid InventoryID { get; set; }
        public Guid InspectorID { get; set; }
        public string InspectorName { get; set; }
        public string Position { get; set; }

        public string ToXML
        {
            get
            {
                return "<InventoryInspectors> " +
                        "<InventroyID>" + InventoryID.ToString() + "</InventroyID>" +
                        "<InspectorID>" + InspectorID.ToString() + "</InspectorID>" +
                        "<InspectorName>" + InspectorName.ToString() + "</InspectorName>" +
                        "<Position>" + Position.ToString() + "</Position>" +
                        "</InventoryInspectors>";
            }
        }
    }

    public enum InventoryDetailStatus
    {
        Approved = 2,
        Rejected = 3
    }
}