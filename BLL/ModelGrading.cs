using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECX.DataAccess;
using System.Xml;
using System.Configuration;
using WarehouseApplication.BLL;
using GINBussiness;
using System.Data;
using System.Text;

namespace GradingBussiness
{
    public class GradingModel : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid SamplingsID { get; set; }
        public Guid ArrivalID { get; set; }
        public Guid WoredaID { get; set; }
        public Guid CommodityID { get; set; }
        public string CommodityName { get; set; }
        public string TrackingNumber { get; set; }
        public Guid GradingFactorGroupID { get; set; }
        public decimal TotalValue { get; set; }
        public string GradeRecived { get; set; }
        public DateTime GradeRecivedDateTime { get; set; }
        public Guid CommodityClassID { get; set; }
        public Guid VoucherCommodityTypeID { get; set; }
        public Guid CommodityGradeID { get; set; }
        public string CommodityGradeName { get; set; }
        public string ShedNo { get; set; }
        public string ClientName { get; set; }
        public Guid ShedID { get; set; }
        public int NumberofSeparations { get; set; }
        public int GradingResultStatusID { get; set; }
        public Guid GRNID { get; set; }
        public int GradingsStatusID { get; set; }
        public string GradingsStatus { get; set; }
        public Guid ResultCreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
        public int? ClassificationNo { get; set; }
        public string CashReceiptNo { get; set; }
        public decimal Amount { get; set; }
        public Guid SecurityMarshalID { get; set; }
        public bool NewSampleCodeGenerated { get; set; }
        public bool GRNCreated { get; set; }
        public Guid LICID { get; set; }
        public int ProductionYear { get; set; }
        public bool Edit { get; set; }
        public bool CodeGenerated { get; set; }
        private List<GradingDetail> gradingDetailList;
        public List<GradingDetail> gradingDetailinfoList
        {
            get
            {
                return gradingDetailList;
            }
            set
            {
                gradingDetailList = value;
            }
        }
        public void addGradingDetail(GradingDetail gd)
        {
            if (gradingDetailList == null)
                gradingDetailList = new List<GradingDetail>();
            gradingDetailList.Add(gd);

        }
        public string gradingDetail
        {
            get
            {
                string gradingDetailinfoXML;

                if (gradingDetailinfoList == null || gradingDetailinfoList.Count == 0)
                {
                    gradingDetailinfoXML = "<Grading></Grading>";
                }
                else
                {

                    IEnumerable<string> gradingDetailNods = gradingDetailList.Select(s => s.ToXML);

                    gradingDetailinfoXML = "<Grading>" + gradingDetailNods.Aggregate((str, next) => str + next) + "</Grading>";
                }
                return gradingDetailinfoXML;
            }
        }

        public Boolean Isnormaldeposit { get; set; }
        public string GradingCode { get; set; }
        public string GradingResultStatus { get; set; }
        public DateTime DateTimeCoded { get; set; }
        public Guid WarehouseId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime CodeReceivedDateTime { get; set; }
        public DateTime ClientAcceptanceTimeStamp { get; set; }
        public Guid CodeReceivedBy { get; set; }
        public string Name { get; set; }
        public string SampleCode { get; set; }
        public Guid UserId { get; set; }
        private List<GraderModel> gradersList;
        public string GetRandomCode(string WarehouseNo)
        {
            int r;
            Random rnd = new Random();
            r = rnd.Next(9999);
            string code, second;
            second = DateTime.Now.Second.ToString();
            if (second.Length < 2)
            {
                if (second.Length == 0)
                {
                    second = "00";
                }
                if (second.Length == 1)
                {
                    second = "0" + second;
                }
            }
            #region Conneneted out code
            //Removed because the grading Code is dimmed to be too long.
            //microsecond = DateTime.Now.Second.ToString();
            //if (microsecond.Length < 2)
            //{
            //    if (microsecond.Length == 0)
            //    {
            //        microsecond = "00";
            //    }
            //    if (microsecond.Length == 1)
            //    {
            //        microsecond = microsecond + "0";
            //    }
            //}
            //code = WarehouseNo.ToString() +"-"+ r.ToString() + second.ToString() + microsecond.ToString();
            #endregion
            code = WarehouseNo.ToString() + "-" + r.ToString() + second.ToString();
            return code;
        }
        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveGradingCode", this);
        }
        public void UpdateAcceptance()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "UpdateAcceptance", this);
        }
        public void UpdateAcceptanceForSeg()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "UpdateClientAcceptanceForSeg", this);
        }
        public void SaveGradingResult()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveGradingResultModified", this);
        }
        public bool CheckGradeExits(Guid CommodityClassID, string GradeRecived, int? ClassificationNo)
        {
            return (bool)ECX.DataAccess.SQLHelper.ExecuteScalar(ConnectionString, "GradeExits", CommodityClassID, GradeRecived, ClassificationNo);
        }
        public void UpdateRecivedCode()
        {

            ECX.DataAccess.SQLHelper.Save(ConnectionString, "UpdaterecivedCode", this);
        }

        public static DataTable GetWoredaName(Guid WoredaId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "[getWoredaName]", WoredaId);
            return dt;
        }
        public static List<GradingModel> getSamplingInfo(string sampleCode)
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getSamplingForgrading", sampleCode);
            foreach (DataRow dr in dt.Rows)
            {
                GradingModel o = new GradingModel();
                Common.DataRow2Object(dr, o);
                gradingList.Add(o);
            }
            return gradingList;
        }
        public static List<GradingModel> GetCodedDateandCode(Guid ID)
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCodeRecivedDate", ID);
            foreach (DataRow dr in dt.Rows)
            {
                GradingModel o = new GradingModel();
                Common.DataRow2Object(dr, o);
                gradingList.Add(o);
            }
            return gradingList;
        }
        public static List<GradingModel> GetCodeInformation(Guid ID)
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCodeInformation", ID);
            foreach (DataRow dr in dt.Rows)
            {
                GradingModel o = new GradingModel();
                Common.DataRow2Object(dr, o);
                gradingList.Add(o);
            }

            return gradingList;
        }
        public static List<GradingModel> GetCodeInformationByCode(string Code)
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCodeInformationByCodeNew", Code);
            foreach (DataRow dr in dt.Rows)
            {
                GradingModel o = new GradingModel();
                Common.DataRow2Object(dr, o);
                gradingList.Add(o);
            }

            return gradingList;
        }
        //Elias Getachew - July 23 2012
        //Integrate with inBOX
        public static GradingModel GetGradingInformationByGradingCode(string GradingCode)
        {
            GradingModel objGradingModel = null;
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "getGradingsByCode", GradingCode);
            foreach (DataRow dr in dt.Rows)
            {
                objGradingModel = new GradingModel();
                Common.DataRow2Object(dr, objGradingModel);
            }
            return objGradingModel;
        }

        public static DataTable GetSampleDate(string SamplingCode)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "[GetSamplingDate]", SamplingCode);
            return dt;
        }
        public List<GraderModel> gradinginfoList
        {
            get
            {
                return gradersList;
            }
            set
            {
                gradersList = value;
            }
        }
        public string graders
        {
            get
            {
                string gradinginfoXML;

                if (gradinginfoList.Count == 0)
                {
                    gradinginfoXML = "<Grading></Grading>";
                }
                else
                {
                    IEnumerable<string> gradeNods = gradersList.Select(s => s.ToXML);
                    gradinginfoXML = "<Grading>" + gradeNods.Aggregate((str, next) => str + next) + "</Grading>";
                }
                return gradinginfoXML;
            }
        }
        public static DataTable printCode(Guid ID, Guid VoucherCommodityTypeID, Guid CommodityID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "PrintGradiingCode", ID, VoucherCommodityTypeID, CommodityID);
            return dt;
        }
        public static DataTable GetComodityClass(Guid ID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetGradiingClass", ID);
            return dt;
        }
        public static DataTable GetCommodity()
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodity");
            return dt;
        }
        public static DataTable GetCommodityClass(Guid? commodityId, Guid? weradaId, Guid? FactorGroupId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityClass", commodityId, weradaId, FactorGroupId, null);
            return dt;
        }
        public static DataTable GetCommodityClass(Guid? commodityId, Guid? weradaId, Guid? factorGroupId, Guid? commodityTypeID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityClass", commodityId, weradaId, factorGroupId, commodityTypeID);
            return dt;
        }
        public static DataTable GetCommodityGrade(Guid commodityFactorGroupId, Guid classId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityGrade",
                commodityFactorGroupId, classId);
            return dt;
        }


        public static DataTable GradingResultreport(string GradingCode)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GradingResultreport", GradingCode);
            return dt;
        }

        public static DataTable GetInspectionTestResult(string GradingCode, Guid CurrentWarehouse)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInspectionTestResult", GradingCode, CurrentWarehouse);
            return dt;
        }
        public static DataTable GetInspectionTestResultDetail(string GradingCode)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetInspectionTestResultDetail", GradingCode );
            return dt;
        }

        public static DataTable GradingResultreportForSegrigation(string GradingCode)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GradingResultreportForSegrigation", GradingCode);
            return dt;
        }

        public static DataTable GetCommodityGrade(Guid commodityFactorGroupId, Guid classId, Guid? weradaId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityGrade",
                commodityFactorGroupId, classId, weradaId);
            return dt;
        }
        public static DataTable GetCommodityFactorGroup(Guid? commodityClassId, Guid CommodityId, Guid? weradaId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityFactoryGroup", commodityClassId, CommodityId, weradaId);
            return dt;
        }
        public static DataTable GetCommodityFactor(Guid commodityFactorGroupId, Guid? gradingsID, string isdeposit)//
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityFactoryModified", commodityFactorGroupId, gradingsID, "export");//
            return dt;//"GetCommodityFactor"
        }
        public static DataTable GetGradingResultStatusForFactorGroup()
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetGradingResultStatusForFactorGroup");
            return dt;
        }

        public static GradingModel GetGradingResultForEdit(Guid ID)
        {
            GradingModel gradingResult = new GradingModel();
            DataRow dr = SQLHelper.getDataRow(ConnectionString, "GetGradingResultForEdit", ID);

            Common.DataRow2Object(dr, gradingResult);
            return gradingResult;
        }

        public static GradingModel GetGradingResultForEditByCode(string gradingCode)
        {
            GradingModel gradingResult = new GradingModel();
            DataRow dr = SQLHelper.getDataRow(ConnectionString, "GetGradingResultForEditByGradingCode", gradingCode);

            Common.DataRow2Object(dr, gradingResult);
            return gradingResult;
        }


        public void AddGrader(Guid graderID, bool isSupervisor)
        {
            GraderModel o;
            if (gradersList == null)
                gradersList = new List<GraderModel>();
            if (gradersList.Count > 0 && gradersList.Exists(g => g.UserId == graderID) || gradersList.Exists(g => g.isSupervisor == isSupervisor))
            {
                if (isSupervisor == true)
                {
                    if (gradersList.Exists(g => g.isSupervisor == isSupervisor))
                    {
                        throw new Exception("Cannot add  More Than one Supervisor");
                    }
                }
                else if (gradersList.Exists(g => g.UserId == graderID))
                {
                    throw new Exception("Cannot add the same data More Than one");
                }               
                else if (gradersList.Exists(g => g.isSupervisor == false))
                {
                    o = new GraderModel(this);
                    o.UserId = graderID;
                    o.UserName = UserBLL.GetName(graderID).ToString();
                    o.UserName = o.UserName.Replace(".", " ");
                    o.isSupervisor = isSupervisor;
                    gradersList.Add(o);
                }
                else
                    throw new Exception("Cannot add from the same Grader or More Than one Supervisor");
                return;
            }
            o = new GraderModel(this);
            o.UserId = graderID;
            o.UserName = UserBLL.GetName(graderID).ToString();
            o.UserName = o.UserName.Replace(".", " ");
            o.isSupervisor = isSupervisor;
            gradersList.Add(o);           
        }
        public override bool IsValid()
        {
            bool isValid = true;
            StringBuilder sb = new StringBuilder();

            foreach (GraderModel sm in gradinginfoList)
            {
                isValid = sm.IsValid();
                if (!isValid)
                    sb.AppendLine(sm.ErrorMessage);
            }
            if (gradinginfoList.Count == 0)
            {
                isValid = false;
                sb.AppendLine("You need to add at least single grading detail by");
            }

            ErrorMessage = sb.ToString();
            return isValid;
        }
        public bool IsValidForGradingResult()
        {
            bool isValid = true;
            StringBuilder sb = new StringBuilder();

            if (NumberofSeparations <= 0)
            {
                isValid = false;
                sb.AppendLine("Enter positive value less than 100 to specify number of segregation!");
            }

            ErrorMessage = sb.ToString();
            return isValid;
        }
        public static List<GradingModel> GetgradingsInfo(string Gradingcode)
        {
            List<GradingModel> licList;
            licList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "getgradingInfoClientAcceptance", Gradingcode);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                licList.Add(pnm);
            }
            return licList;
        }
        public static List<GradingModel> GetGradingsEdit(string Gradingcode)
        {
            List<GradingModel> licList;
            licList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetGradingInfoEdit", Gradingcode);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                licList.Add(pnm);
            }
            return licList;
        }
        public static List<GradingModel> LICs(Guid ShedID)
        {
            List<GradingModel> licList;
            licList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetLICForGrading", UserBLL.GetCurrentWarehouse(), ShedID);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                licList.Add(pnm);
            }
            return licList;
        }
        public static List<GradingModel> Shed(Guid CwareHouseId,int ProductionYear,Guid CommodityGradeID)
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetAllSheds", CwareHouseId, ProductionYear, CommodityGradeID);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                gradingList.Add(pnm);
            }
            return gradingList;
        }
        public static List<GradingModel> GetGradingResultStatus()
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "Get_AllGradingResultStatus");
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                gradingList.Add(pnm);
            }
            return gradingList;
        }
        public static List<GradingModel> GetGradingsStatus()
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "Get_GradingsStatus");
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                gradingList.Add(pnm);
            }
            return gradingList;
        }
    }
    public class GraderModel : WarehouseBaseModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool isSupervisor { get; set; }
        public GradingModel Grading { get; private set; }
        public GraderModel(GradingModel theParent)
        {
            Grading = theParent;
        }
        public string ToXML
        {
            get
            {
                return "<Grader> " +
                        "<UserId>" + UserId.ToString() + "</UserId>" +
                        "<isSupervisor>" + isSupervisor.ToString() + "</isSupervisor>" +
                        "</Grader>";
            }
        }
        public override bool IsValid()
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;
            if (UserId == Guid.Empty)
            {
                message.AppendLine("Please enter Grader. Grader is required. <br/>");
                isValid = false;
            }
            ErrorMessage = message.ToString();
            return isValid;

        }
    }

    public class GradingDetail : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid GradingsID { get; set; }
        public Guid GradingFactorID { get; set; }
        public string ReceivedValue { get; set; }
        public int Status { get; set; }
        public Guid GradeDisputeID { get; set; }
        public GradingModel Grading { get; private set; }
        public GradingDetail(GradingModel theParent)
        {
            Grading = theParent;
        }
        public string ToXML
        {
            get
            {
                return "<GradingDetail> " +
                        "<GradingFactorID>" + GradingFactorID.ToString() + "</GradingFactorID>" +
                        "<ReceivedValue>" + ReceivedValue.ToString() + "</ReceivedValue>" +
                        "<Status>" + Status.ToString() + "</Status>" +
                        "</GradingDetail>";
            }
        }
        public override bool IsValid()
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;
            if (ReceivedValue == string.Empty)
            {
                message.AppendLine("has no ReceivedValue. Grading factor is required. <br/>");
                isValid = false;
            }
            ErrorMessage = message.ToString();
            return isValid;

        }
    }

    public class GradingFactorModel : WarehouseBaseModel
    {
        public Guid Id { get; set; }
        public Guid GradingTypeId { get; set; }
        public string GradingFactorName { get; set; }
        public int Status { get; set; }
        public int Rank { get; set; }
        public string Type { get; set; }
        public string TypeValueType { get; set; }
        public int TypeStatus { get; set; }
    }
    public enum GradingConstants
    {
        CommodityId = 1,
        WeradaId = 2,
        GradingID = 3,
        moisturefail = 4,
        CommodityTypeID = 5,
        IsNonCoffee = 6
    }
    public enum GradingResultStatus
    {
        NONE = 0,
        CodeGenrated = 1,
        MoistureFailed = 2,
        GeneralRequirmentfail = 3,
        SegrigationRequested = 4,
        AcceptableResult = 5,
        Sort = 6
    }
}
