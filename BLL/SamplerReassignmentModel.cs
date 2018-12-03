using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WarehouseApplication.BLL;
using ECX.DataAccess;
using GINBussiness;
namespace WarehouseApplication.BLL
{
    public class SamplerReassignmentModel : WarehouseBaseModel
    {

        #region Properties and Methods

        public Guid ID { get; set; }
        public string SampleCode { get; set; }
        public Guid OldSampler { get; set; }
        public Guid NewSampler { get; set; }
        public string SamplerName { get; set; }
        public string Remark { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }

        #endregion

        public static DataTable GetSamplingCodes(Guid WarehouseID)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetSamplingCodes", WarehouseID);
        }

        public static DataRow GetSamplersByCode(string SampleCode)
        {
            return SQLHelper.getDataRow(ConnectionString, "GetSamplersByCode", SampleCode);
        }

        public static DataTable GetSamplersForReassingment(Guid WarehouseID, Guid OldSampler, DateTime OperationDate)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetSamplersForReassingment", WarehouseID, OldSampler, OperationDate);
        }

        public object InsertSamplerReassignment()
        {
            return SQLHelper.SaveAndReturn(ConnectionString, "AddSamplerReassignment", this);
        }
    }
}