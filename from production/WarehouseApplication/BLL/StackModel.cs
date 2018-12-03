using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECX.DataAccess;
using GINBussiness;
using System.Data;
using WarehouseApplication.BLL;
namespace WarehouseApplication.BLL
{
    public class StackModel : WarehouseBaseModel
    {
        #region Properties and Methods

        public Guid ID { get; set; }
        public Guid ShedID { get; set; }
        public Guid LICID { get; set; }
        public Guid CommodityGradeID { get; set; }
        public Guid WarehouseID { get; set; }
        public Guid PhysicalAddressID { get; set; }
        public int ProductionYear { get; set; }
        public Guid BagTypeID { get; set; }
        public string StackNumber { get; set; }
        public DateTime DateStarted { get; set; }
        public int BeginingBalance { get; set; }
        public int CurrentBalance { get; set; }
        public float BeginingWeight { get; set; }
        public float CurrentWeight { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public int Status { get; set; }

        #endregion

        public StackModel()
        {
        }
        public object InsertStacks()
        {
            return SQLHelper.SaveAndReturn(ConnectionString, "AddStack", this);
        }

        public static void UpdateStacks(Guid ID, int Staus, Guid LastModifiedBy, DateTime LastModifiedTimestamp)
        {
            SQLHelper.execNonQuery(ConnectionString, "UpdateStackStaus", ID, Staus, LastModifiedBy, LastModifiedTimestamp);
        }

        public static void CloseStack(Guid ID, int Staus, Guid LastModifiedBy, DateTime LastModifiedTimestamp)
        {
            SQLHelper.execNonQuery(ConnectionString, "CloseStack", ID, Staus, LastModifiedBy, LastModifiedTimestamp);
        }
        public static void CancelStacks(Guid ID)
        {
            SQLHelper.execNonQuery(ConnectionString, "DeleteStacks", ID);
        }

        #region Stack Mangemet LookUps

        public static DataTable GetCommodyList()
        {
            return SQLHelper.getDataTable(ConnectionString, "GetCommodyList");
        }

        public static DataTable GetCommodyClassList(Guid WarehouseID, Guid CommodityID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetCommodyClassList", WarehouseID, CommodityID);
        }

        public static DataTable GetCommodySymbolList(Guid ClassID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetCommodySymbolList", ClassID);
        }

        public static DataTable GetWarehouseSheds(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetWarehouseSheds", WarehouseID);
        }

        public static DataTable GetShedByLIC(Guid WarehouseID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetShedByLIC", WarehouseID, LICID);
        }

        public static DataTable GetShedByLICForNewStack(Guid WarehouseID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetShedByLICForNewStack", WarehouseID, LICID);
        }

        public static DataTable GetWarehouseLICs(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetWarehouseLICs", WarehouseID);
        }

        public static DataTable GetPhysicalAddresses(Guid WarehouseShedID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetPhysicalAddresses", WarehouseShedID);
        }

        public static DataTable GetPhysicalAddressInStack(Guid WarehouseShedID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetPhysicalAddressInStack", WarehouseShedID);
        }
        public static DataTable GetBagTypeList(Guid ComodityID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetBagTypeList", ComodityID);
        }

        public static DataTable GetLICbyShed(Guid WarehouseID, Guid ShedID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetLICbyShed", WarehouseID, ShedID);
        }
        public static DataTable GetStacks(Guid WarehouseID, Guid ShedID, Guid PhysicalAddressID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetStacks", WarehouseID, ShedID, PhysicalAddressID);
        }
        #endregion
    }
}