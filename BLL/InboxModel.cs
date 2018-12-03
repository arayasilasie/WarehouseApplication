using System;
using System.Data;
//using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseApplication.BLL;
using GINBussiness;
using ECX.DataAccess;
namespace WarehouseApplication.BLL
{
    public class InboxModel : WarehouseBaseModel
    {  
    
        #region Properties and Methods
   
        public string Task{get;set;}
        public int Count{get;set;}

        #endregion      

        public InboxModel()
        {
        }
        /// <summary>
        ///  Get Inbox list for GRN or GIN
        /// </summary>
        /// <param name="WarehouseID"></param>
        /// <param name="TypeID"></param> GRN or GIN
        /// <returns></returns>
        public static DataTable GetInboxList(Guid WarehouseID, int TypeID)
        {
           return SQLHelper.getDataTable(ConnectionString, "GetInboxList", TypeID, WarehouseID); 
        }

        public static DataTable GetInboxDetailList(Guid WarehouseID, int StepID, int TypeID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInboxDetailList",TypeID,StepID, WarehouseID);
        }

        public static DataTable GetInboxListForGIN(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetInboxListForGIN", WarehouseID);
        }

        public static DataRow GetInboxDetailForClosing(Guid WarehouseID, string TrackingNo, int StepID, Guid ID )
        {
            return SQLHelper.getDataRow(ConnectionString, "GetInboxDetailForClosing", WarehouseID,TrackingNo, StepID, ID);
        }

        public static void CloseWorkflow(Guid ID, Guid UpdatedBy , DateTime UpdateTimeStamp,Guid ArrivalID, string Reason)
        {
            SQLHelper.execNonQuery(ConnectionString, "Close_Workflow", ID, UpdatedBy, UpdateTimeStamp,ArrivalID,Reason);
        }

        public static void Close_WorkflowModified(Guid ID, Guid UpdatedBy, DateTime UpdateTimeStamp, Guid ArrivalID, string Reason, int StepID)
        {
            SQLHelper.execNonQuery(ConnectionString, "Close_WorkflowModified", ID, UpdatedBy, UpdateTimeStamp, ArrivalID, Reason, StepID);
        }

        public static DataRow GetInboxDetailForClosingModified(Guid WorkFlowID, int StepID, Guid ID)
        {
            return SQLHelper.getDataRow(ConnectionString, "GetInboxDetailForClosingModified", WorkFlowID, StepID, ID);
        }

    }
}