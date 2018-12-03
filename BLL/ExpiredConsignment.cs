using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataDynamics.ActiveReports.Design;
using ECX.DataAccess;
using GINBussiness;

namespace WarehouseApplication.BLL
{
    public class ExpiredConsignment:WarehouseBaseModel
    {
        public ExpiredConsignment()
        {
            
        }
        public static DataTable SearchConsExpieredList(Guid warehouseID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "spConsignmentExpiredList", warehouseID);
            
            return dt;

        }
    }
}