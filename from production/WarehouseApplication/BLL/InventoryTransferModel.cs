using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ECX.DataAccess;
//using WarehouseApplication.BLL;
using GINBussiness;
namespace WarehouseApplication.BLL
{
    public class InventoryTransferModel : WarehouseBaseModel
    {
        #region Properties and Methods

        public Guid ID{get;set;}
        public Guid WarehouseID {get;set;}
        public Guid LICID{get;set;}
        public Guid ShedID{get;set;}
        public Guid StackNo { get; set; }
        public int PhysicalCount{get;set;}
        public int SystemCount{get;set;}
        public float PhysicalWeight{get;set;}
        public float SystemWeight{get;set;}
        public Guid LICIDTo{get;set;}
        public Guid ShedIDTo{get;set;}
        public Guid StackNoTo { get; set; }
        public int PhysicalCountTo{get;set;}
        public int SystemCountTo{get;set;}
        public float PhysicalWeighTo { get; set; }
        public float SystemWeighTo{get;set;}
        public Guid CreatedBy{get;set;}
        public DateTime CreatedTimestamp{get;set;}             
        public int Status { get; set; }
        public int InventoryTransferReasonID { get; set; }
        public DateTime TransitionDate { get; set; }
        //public Guid LastModifiedBy{get;set;}
        //public DateTime LastModifiedTimestamp{get;set;}
        //public Guid ApprovedByID{get;set;}
        //public DateTime DateApproved{get;set;}

        #endregion

        public InventoryTransferModel()
        {
        }
        public object InsertInventoryTransfer()
        {
            return SQLHelper.SaveAndReturn(ConnectionString, "AddInventoryTransfer", this);
        }

        public static void ApproveInventorysTransfer(Guid ID, Guid ApprovedByID, DateTime DateApproved,
            Guid StackID,Guid StackID2, Guid LICID,int CurrentBalance, float CurrentWeight )
        {
            SQLHelper.execNonQuery(ConnectionString, "ApproveInventoryTransfer", ID, ApprovedByID, DateApproved,
                StackID, StackID2,LICID, CurrentBalance, CurrentWeight);
        }

        public static void ApproveInvTransferLICResign(Guid ID, Guid ApprovedByID, DateTime DateApproved)
        {
            SQLHelper.execNonQuery(ConnectionString, "ApproveInvTransferLICResign", ID, ApprovedByID, DateApproved);
        }

        public static void CancelInventoryTransfer(Guid ID)
        {
            SQLHelper.execNonQuery(ConnectionString, "DeleteInventoryTransfer", ID);
        }

        public static void CancelInvTransferLICResign(Guid ID)
        {
            SQLHelper.execNonQuery(ConnectionString, "DeleteInvTransferLICResign", ID);
        }

        public static DataTable GetInventoryTransferForApproval(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInventoryTransferForApproval", WarehouseID);
        }
        public static DataTable GetInventoryTransferForEdit(Guid WarehouseID, DateTime TransferDate)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInventoryTransferForEdit", WarehouseID, TransferDate);
        }
        public static DataRow GetStackInfo(Guid ID)
        {
            return SQLHelper.getDataRow(ConnectionString, "GetStackInfo",ID);
        }       

        public static DataRow GetInventoryTransaction(Guid ID)
        {
            return SQLHelper.getDataRow(ConnectionString, "GetInventoryTransaction", ID);
        }

        public static DataTable GetInventoryTransferByLIC(Guid WarehouseID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInventoryTransferByLIC", WarehouseID, LICID);
        }

        public static void InventoryTransferLICResign(string InventoryTransferXML, string TransferDetailXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "AddInventoryTransferLICResign", InventoryTransferXML, TransferDetailXML);

        }

        public static DataTable GetInvTransferLICResign(Guid ID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInvTransferLICResign",ID);
        }

        public static DataTable GetInvTransferDetail(Guid ID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInvTransferDetal", ID);
        }

        public static DataTable GetInvTransferLICRsnForEdit(Guid WarehouseID, DateTime TransferDate)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInvTransferLICRsnForEdit", WarehouseID, TransferDate);
        }

        public static DataTable GetInvTransferForApprovalLICResign(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInvTransferLICRsnForApproval", WarehouseID);
        }

        #region InventoryTransfer LookUps

        public static DataTable GetLICsForInventoryTransfer(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetLICsForInventoryTransfer", WarehouseID);
        }           

        public static DataTable GetShedsForInventoryTransfer(Guid WarehouseID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetShedsForInventoryTransfer", WarehouseID, LICID);
        }

        public static DataTable GetStackNos(Guid WarehouseID, Guid ShedID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetStackNos", WarehouseID, ShedID, LICID);
        }

        public static DataTable GetStackNosTo(Guid WarehouseID, Guid ShedID, int ProductionYear, Guid Symbol)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetStackNosTo", WarehouseID, ShedID,ProductionYear, Symbol);
        }

        public static DataTable GetInventoryTransferReasons()
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInventoryTransferReason");
        }

        public static DataTable GetLICsInventoryTransferTo(Guid WarehouseID, int ProductionYear, Guid Symbol, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetLICsInventoryTransferTo", WarehouseID, ProductionYear, Symbol, LICID);
        }

        public static DataTable GetShedsInventoryTransferTo(Guid WarehouseID, int ProductionYear, Guid Symbol, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetShedsInventoryTransferTo", WarehouseID, ProductionYear, Symbol, LICID);
        }

        public static DataTable GetLICsToAssignInventory(Guid WarehouseID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetLICsToAssignInventory", WarehouseID, LICID);
        }

        #endregion

        public static DataTable GetInventoryBalance(Guid WarehouseID, Guid ShedID, Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInventoryBalaceModified", WarehouseID, ShedID, LICID);
        }


    }
}