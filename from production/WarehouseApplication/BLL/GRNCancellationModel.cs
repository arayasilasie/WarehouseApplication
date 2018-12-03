using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECX.DataAccess;
using GINBussiness;
using System.Data;
namespace WarehouseApplication.BLL
{
    public class GRNCancellationModel :WarehouseBaseModel
    {
        public static DataTable GetGRNsForCancellation(Guid WarehouseID, Guid LICID, string GRNNo, string ClientID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNsForCancellation", WarehouseID, LICID, GRNNo, ClientID);
        }

        public static DataTable GetGRNsForCancellation(Guid WarehouseID,  string GRNNo)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNsForCancellation", WarehouseID, GRNNo);
        }

        public static DataTable GetLICsForGRNCancellation(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetLICsForGRNCancellation", WarehouseID);
        }

        public static void GRNCancelationRequest(Guid ID, int Staus, Guid RequestedBy, DateTime DateRequested, string Remark)
        {
            SQLHelper.execNonQuery(ConnectionString, "GRNCancelationRequest", ID, RequestedBy, DateRequested, Remark);
        }

        public static DataTable GetGRNCancellationRequestSearch(Guid WarehouseID, int Status, string GRNNo, DateTime DateRequested, DateTime DateRequested2, DateTime DateApproved, DateTime DateApproved2)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNCancellationRequestSearch", WarehouseID, Status, GRNNo, DateRequested, DateRequested2, DateApproved, DateApproved2);
        }             

        public static DataTable GetGRNCancellationStatus()
        {
            return SQLHelper.getDataTable(ConnectionString, "GetGRNCancellationStatus");
        }
    }
}