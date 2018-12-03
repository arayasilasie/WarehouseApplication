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
    public class SamplerAttendaceModel : WarehouseBaseModel
    {
        //#region Properties and Methods
        //public Guid ID { get; set; }
        //public Guid OperatorID { get; set; }
        //public bool Status { get; set; }
        //public string Reason { get; set; }
        //public Guid CreatedBy { get; set; }  
        //public DateTime CreatedTimestamp { get; set; }
        //public DateTime OperationDate { get; set; }
        //public Guid LastModifiedBy { get; set; }  
        //public DateTime LastModifiedDate { get; set; }
     
        //#endregion

        public static DataTable GetSamplers(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetSamplers", WarehouseID);
        }

        public static DataTable GetSamplersAttendance(Guid WarehouseID, DateTime OperationDate)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetSamplersAttendance", WarehouseID, OperationDate);
        }

        public static void AddSamplersAttendance(string SamplersAttendanceXML)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "AddSamplersAttendance", SamplersAttendanceXML);

        }

        public static void UpdateSamplersAttendance(Guid ID, bool Status, Guid LastModifiedBy, DateTime LastModifiedDate, string Reason)
        {
            ECX.DataAccess.SQLHelper.ExecuteSP(ConnectionString, "UpdateSamplersAttendance", ID, Status, LastModifiedBy, LastModifiedDate,Reason);

        }


    }
}