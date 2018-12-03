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
    public class GRNApprovalModel : WarehouseBaseModel
    {
        public static DataTable GetGRNForApproval(Guid WarehouseID, int Staus, Guid LICID, string GRNNo, string ClientID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNsForApproval", Staus, WarehouseID,LICID,GRNNo,ClientID);
        }

        public static DataTable GetGRNsForPreApproval(Guid WarehouseID,Guid LICID )
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNsForPreApproval", WarehouseID, LICID); 
        }

        public static void ApproveGRN(string GRNApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "ApproveGRN", GRNApprovalXML);
        }
        public static void ApproveGRNBySupervisor(string GRNApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "ApproveGRN_BySupervisor", GRNApprovalXML);
        }

        public static DataTable GetGRNForClientSign(Guid WarehouseID, string GRNNo, string ClientID,Guid LICID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNForClientSign", GRNNo,ClientID,WarehouseID, LICID);
        }

        public static void GRNSigned(string GRNApprovalXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "GRNSigned", GRNApprovalXML);
        }

        public static DataTable GetGRNApprovalForEdit(Guid WarehouseID, Guid GRNID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNApprovalForEdit", WarehouseID, GRNID);
        }

        public static DataTable GetGRNApprovalForSupervisorEdit(Guid WarehouseID, Guid  GRNID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNApprovalForSupervisorEdit", WarehouseID, GRNID);
        }

        public static void UpdateGRNApproval(Guid ID, Guid LICApprovedBy, int LICStatus, DateTime LICApprovedDateTime, DateTime LICApprovedTimeStamp)
        {
            SQLHelper.execNonQuery(ConnectionString, "UpdateGRNApproval", ID, LICApprovedBy, LICStatus, LICApprovedDateTime, LICApprovedTimeStamp);
        }

        public static void UpdateGRNSupervisorApproval(Guid ID, Guid WarehouseSupervisorApprovedBy, int WarehouseSupervisorStatus, DateTime WarehouseSupervisorApprovedDateTime, DateTime WarehouseSupervisorApprovedTimeStamp )
        {
            SQLHelper.execNonQuery(ConnectionString, "UpdateGRNSupervisorApproval", ID, WarehouseSupervisorApprovedBy, WarehouseSupervisorStatus, WarehouseSupervisorApprovedDateTime, WarehouseSupervisorApprovedTimeStamp);
        }

        public static DataTable GetLICsForGRNApproval(Guid WarehouseID, int Status)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetLICsForGRNApproval", WarehouseID,Status);
        }

        public static void ApproveGRNBySupervisor(Guid ID, Guid StackID,  Guid SupervisorApprovedBy, int SupervisorStatus, DateTime SupervisorApprovedDateTime, DateTime SupervisorApprovedTimeStamp)
        {
            SQLHelper.execNonQuery(ConnectionString, "ApproveGRNBySupervisor", ID, StackID, SupervisorApprovedBy, SupervisorStatus, SupervisorApprovedDateTime, SupervisorApprovedTimeStamp);
        }
    }      
}