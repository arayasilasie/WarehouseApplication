using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GINBussiness;
using System.Data;
using ECX.DataAccess;
using WarehouseApplication.BLL;

namespace SamplingBussiness
{
    [Serializable]
    public class SamplingModel : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid ArrivalID { get; set; }
        public int SerialNo { get; set; }
        public string SampleCode { get; set; }
        public Guid SamplerID { get; set; }
        public string SamplerName { get; set; }
        public Guid SamplingInspectorID { get; set; }
        public DateTime SampleCodeGeneratedTimeStamp { get; set; }
        public DateTime? ResultReceivedDateTime { get; set; }
        public int? NumberOfBags { get; set; }
        public int? ArrivalNumberOfBags { get; set; }
        public Guid? BagTypeID { get; set; }
        public string SamplerComments { get; set; }
        public int SamplingStatusID { get; set; }
        public int? PlompStatusID { get; set; }
        public Guid? SupervisorID { get; set; }
        public string SupervisorApprovalRemark { get; set; }
        public DateTime? SupervisorApprovalDateTime { get; set; }
        public bool? HasLiveInsect { get; set; }
        public bool? HasMoldOrFungus { get; set; }
        public bool? HasChemicalOrPetrol { get; set; }
        public Guid? PreviousGradingResultID { get; set; }
        public Guid? PreviousSamplingID { get; set; }
        public Guid WarehouseID { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedTimestamp { get; set; }

        public Guid? CommodityID{ get; set; }
        public Guid? VoucherCommodityTypeID { get; set; }

        public bool IsReSampled { get; set; }
        public bool IsGraded { get; set; }
        public string SampleType { get; set; }

        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SamplingSave", this);
        }

        public static List<ArrivalForSampling> GetArrivalsReadyForSampling(Guid warehouseID, int searchCase, string trackingCode,
                           string preSampleCode, string perGradingCode)
        {
            List<ArrivalForSampling> arrivalsForSamplingList = new List<ArrivalForSampling>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetArrivalsReadyForSamplingNEW", warehouseID, searchCase,
                    trackingCode, preSampleCode, perGradingCode);
            foreach (DataRow dr in dt.Rows)
            {
                ArrivalForSampling o = new ArrivalForSampling();
                Common.DataRow2Object(dr, o);
                arrivalsForSamplingList.Add(o);
            }
            return arrivalsForSamplingList;
        }

        public static List<string> SelectSampler(Guid warehouseID)
        {
            List<string> str = new List<string>();
            DataRow dr = ECX.DataAccess.SQLHelper.getDataRow(ConnectionString, "GetFreeLessLoadSamplerInWHModified", warehouseID);
            if (dr != null)
            {
                str.Add(dr["SamplerID"].ToString());
                str.Add(dr["SamplerName"].ToString());
            }
            return str;
        }

        public static List<string> SelectSampleInspector(Guid warehouseID, Guid exceptionOperator)
        {
            List<string> str = new List<string>();
            DataRow dr = ECX.DataAccess.SQLHelper.getDataRow(ConnectionString, "GetFreeLessLoadSamplingInspectorInWH", warehouseID, exceptionOperator);
            if (dr != null)
                str.Add(dr["SampleInspectorID"].ToString());
            return str;
        }

        public static string GetLastSerialNoForDate(string date, Guid warehouseID)
        {
            return ECX.DataAccess.SQLHelper.ExecuteScalar(ConnectionString, "GetLastSerialNoForDateWH", date, warehouseID).ToString();
        }

        public static DataTable GetSampleTicketReport(Guid sampleId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetSampleTicketReportForSample", sampleId);
            return dt;
        }

        public static List<SamplingModel> GetSamples(Guid warehouseId, string TrackNo, string SampleCode, int status)
        {
            List<SamplingModel> sampleList = new List<SamplingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetSamples", warehouseId, SampleCode, status);
            foreach (DataRow dr in dt.Rows)
            {
                SamplingModel o = new SamplingModel();
                Common.DataRow2Object(dr, o);
                sampleList.Add(o);
            }
            return sampleList;
        }

        public static DataTable GetBagTypes()
        {
            List<SamplingModel> sampleList = new List<SamplingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetBagTypes");

            return dt;
        }

        public static DataTable GetBagTypes(Guid? CommodityID, Guid? commodityTypeID)
        {
            List<SamplingModel> sampleList = new List<SamplingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetBagTypesByCommodity", CommodityID, commodityTypeID);

            return dt;
        }

        public static List<SamplingModelDetail> GetSamplesDetail(Guid warehouseId, string TrackNo, string SampleCode, int status)
        {
            List<SamplingModelDetail> sampleList = new List<SamplingModelDetail>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetSamples", warehouseId, TrackNo, SampleCode, status);
            foreach (DataRow dr in dt.Rows)
            {
                SamplingModelDetail o = new SamplingModelDetail();
                Common.DataRow2Object(dr, o);
                sampleList.Add(o);
            }
            return sampleList;
        }

        public static List<tblSamplingStatus> GetSamplingStatus(int ID, string Description)
        {
            List<tblSamplingStatus> sampleStatusList = new List<tblSamplingStatus>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetSamplingStatus", ID, Description);
            foreach (DataRow dr in dt.Rows)
            {
                tblSamplingStatus o = new tblSamplingStatus();
                Common.DataRow2Object(dr, o);
                sampleStatusList.Add(o);
            }
            return sampleStatusList;
        }

        public static List<WHOperators> GetWareHouseOperatorsByType(int type, Guid warehouse)
        {
            List<WHOperators> operatorsList = new List<WHOperators>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetWHOperatorsByTypeForWH", type, warehouse);
            foreach (DataRow dr in dt.Rows)
            {
                WHOperators o = new WHOperators();
                Common.DataRow2Object(dr, o);
                operatorsList.Add(o);
            }
            return operatorsList;
        }

        public static SamplingModel GetSampleById(Guid id)
        {

            DataRow dr = SQLHelper.getDataRow(ConnectionString, "GetSamplesById", id);
            SamplingModel sampleObj = new SamplingModel();
            Common.DataRow2Object(dr, sampleObj);
            return sampleObj;
        }

        public static SamplingModel GetSampleBySampleCode(string sampleCode)
        {

            DataRow dr = SQLHelper.getDataRow(ConnectionString, "GetSamplesBySampleCode", sampleCode);
            SamplingModel sampleObj = new SamplingModel();
            Common.DataRow2Object(dr, sampleObj);
            return sampleObj;
        }

        public static void MoveToNextArrival(string TrackNo, Guid UserId, Guid warehouseId)
        {
            SQLHelper.execNonQuery(ConnectionString, "MoveToSuccessStep", TrackNo, UserId, warehouseId);
        }
    }

    public class SamplingModelDetail : SamplingModel
    {
        private string _status;
        public String Status
        {
            get
            {
                if (_status == null || _status.Trim().Length <= 0)
                {
                    _status = SamplingModel.GetSamplingStatus(this.SamplingStatusID, "")[0].Description;
                }
                return _status;
            }
        }
    }

    public class ArrivalForSampling
    {
        public Guid ArrivalId { get; set; }
        public string TrackingNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid SampleId { get; set; }
        public Guid GradeId { get; set; }
        public int SamplingStatusID { get; set; }
        public string SampleCode { get; set; }
        public string GradingCode { get; set; }
        public string GradingStatus { get; set; }
        public Guid PreSamplerID { get; set; }
        public string PreSamplerName { get; set; }
        public Guid PreSampleInspectorID { get; set; }
        public int GradingStatusID { get; set; }
    }


    public class WHOperators
    {
        public Guid OperatorId { get; set; }
        public string Name { get; set; }
        public int type { get; set; }
    }

    public enum SamplingStatus
    {
        None = 0,
        SampleCodeGenerated = 1,
        SampleResultReceived = 2,
        SeparationRequested = 3,
        DriverNotFound = 4
    }

    public class tblSamplingStatus
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int NextStep { get; set; }
    }

    public enum PlompStatus
    {
        None = 0,
        PlompOK = 1,
        PlompNotOk = 2,
        PlompNotOkManagerApproved = 3
    }
}