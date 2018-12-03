using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;
using System.Data.SqlClient;

namespace WarehouseApplication.BLL
{
    public enum GradingFactorStatus { Cancelled = 0, Active }
    public enum FailPointComparsion { NA = -1, LessThan, GreaterThan, EqualTo, LessorEqualTo, GreaterorequalTo }
    [Serializable]
    public class GradingFactorBLL : GeneralBLL
    {
        private Guid _id;
        private Guid _commodityGradeId;
        private Guid _gradingTypeId;
        private string _gradingFactorName;
        private string _possibleValues;
        private GradingFactorStatus _status;
        private int _rank;
        private string _dataType;
        private string _failPoint;
        private FailPointComparsion _isMax;
        private bool _isInTotalValue;
        private float _maximumvalue;
        private float _minimumvalue;








        #region Contructors
        public GradingFactorBLL()
        {
            this.IsMax = FailPointComparsion.NA;
        }
        public GradingFactorBLL(GradingFactorBLL source)
        {
            this.Id = source.Id;
            this.CommodityGradeId = source.CommodityGradeId;
            this.GradingTypeId = source.GradingTypeId;
            this.GradingFactorName = source.GradingFactorName;
            this.Status = source.Status;
            this.PossibleValues = source.PossibleValues;
            this.CreatedBy = source.CreatedBy;
            this.CreatedTimestamp = source.CreatedTimestamp;
            this.LastModifiedBy = source.LastModifiedBy;
            this.LastModifiedTimestamp = source.LastModifiedTimestamp;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public Guid CommodityGradeId
        {
            get
            {
                return this._commodityGradeId;
            }
            set
            {
                this._commodityGradeId = value;
            }
        }
        public Guid GradingTypeId
        {
            get
            {
                return this._gradingTypeId;
            }
            set
            {
                this._gradingTypeId = value;
            }
        }
        public String GradingFactorTypeName { get; set; }
        public string GradingFactorName
        {
            get
            {
                return this._gradingFactorName;
            }
            set
            {
                this._gradingFactorName = value;
            }
        }
        public GradingFactorStatus Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        public string PossibleValues
        {
            get
            {
                return this._possibleValues;
            }
            set
            {
                this._possibleValues = value;
            }
        }
        public string DataType
        {
            get
            {
                return this._dataType;
            }
            set
            {
                this._dataType = value;
            }

        }
        public string FailPoint
        {
            get { return _failPoint; }
            set { _failPoint = value; }
        }
        public FailPointComparsion IsMax
        {
            get { return _isMax; }
            set { _isMax = value; }
        }
        public bool IsInTotalValue
        {
            get { return _isInTotalValue; }
            set { _isInTotalValue = value; }
        }
        public int Rank
        {
            get
            {
                return this._rank;
            }
            set
            {
                this._rank = value;
            }
        }
        public float Maximumvalue
        {
            get { return _maximumvalue; }
            set { _maximumvalue = value; }
        }
        public float Minimumvalue
        {
            get { return _minimumvalue; }
            set { _minimumvalue = value; }
        }
        #endregion

        #region Public Methods
        public List<GradingFactorBLL> GetGradingFactors(Guid CommodityGradeId)
        {
            return GradingFactorDAL.GetGradingFactors(CommodityGradeId);

        }
        public int GetCountOfTotalValueGradingFactors(Guid CommodityGradeId)
        {
            Nullable<int> count = null;
            try
            {
                count = GradingFactorDAL.GetGradingFactorInTotalValueCount(CommodityGradeId);
                return (int)count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GradingResultStatus GetGradingResultStatus(string value, FailPointComparsion Comparision, string FailPoint, string Type)
        {
            bool isOkay = false; ;
            Nullable<float> fFailPoint = null;
            Nullable<float> fvalue = null;
            if (FailPoint == "")
            {
                return GradingResultStatus.New;
            }
            switch (Type)
            {

                case "Float":


                    try
                    {
                        fFailPoint = float.Parse(FailPoint);
                    }
                    catch
                    {
                        throw new Exception("Unable to Compare Grading Result");
                    }
                    try
                    {
                        fvalue = float.Parse(value);
                    }
                    catch
                    {
                        throw new Exception("Unable to Compare Grading Result");
                    }
                    isOkay = GradingFactorBLL.FloatComparer(Comparision, (float)fvalue, (float)fFailPoint);
                    break;
                case "int":

                    try
                    {
                        fFailPoint = float.Parse(FailPoint);
                    }
                    catch
                    {
                        return GradingResultStatus.New;
                    }
                    try
                    {
                        fvalue = float.Parse(value);
                    }
                    catch
                    {
                        throw new Exception("Unable to Compare Grading Result");
                    }
                    isOkay = GradingFactorBLL.FloatComparer(Comparision, (float)fvalue, (float)fFailPoint);
                    break;
                case "bit":
                    isOkay = GradingFactorBLL.StringEqualityComparer(value, FailPoint);
                    return GradingResultStatus.New;
                default:
                    isOkay = true;
                    break;
            }
            if (isOkay == false)
            {
                return GradingResultStatus.GeneralRequiementfail;
            }
            else
            {
                return GradingResultStatus.Approved;
            }
        }
        public bool Save()
        {
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;

            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = GradingFactorDAL.Insert(this, tran);
                int At = -1;
                if (isSaved == true)
                {
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    At = objAt.saveAuditTrail(this, WFStepsName.GradingFactorAdd.ToString(), UserBLL.GetCurrentUser(), "Add Grading Factor ");

                }
                if (At == 1)
                {
                    tran.Commit();
                    return true;
                }
                else
                {
                    tran.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Unable to saved reocrd", ex);
            }
            finally
            {
                tran.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public bool Update(GradingFactorBLL oldGF)
        {
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = GradingFactorDAL.Update(this, tran);
                int At = -1;
                if (isSaved == true)
                {

                    AuditTrailBLL objAT = new AuditTrailBLL();
                    At = objAT.saveAuditTrail(this, oldGF, WFStepsName.GradingFactorUpdate.ToString(), UserBLL.GetCurrentUser(), "Update Grading Factor");
                }
                if (At == 1)
                {

                    tran.Commit();
                    return true;
                }
                else
                {
                    tran.Rollback();
                    return false;
                }


            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Unable to saved reocrd", ex);
            }
            finally
            {
                tran.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        // Helper 
        public static bool FloatComparer(FailPointComparsion Comparision, float value, float FailPoint)
        {
            bool result = false;// failed
            switch (Comparision)
            {
                case FailPointComparsion.EqualTo:
                    if (value == FailPoint)
                    {
                        result = true;
                    }
                    break;
                case FailPointComparsion.GreaterorequalTo:
                    if (value >= FailPoint)
                    {
                        result = true;
                    }
                    break;
                case FailPointComparsion.GreaterThan:
                    if (value > FailPoint)
                    {
                        result = true;
                    }
                    break;
                case FailPointComparsion.LessorEqualTo:
                    if (value <= FailPoint)
                    {
                        result = true;
                    }
                    break;
                case FailPointComparsion.LessThan:
                    if (value < FailPoint)
                    {
                        result = true;
                    }
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }
        public static bool StringEqualityComparer(string value, string FailPoint)
        {
            if (value.ToUpper() == FailPoint.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int ParseTextGradingResultStatus(string value)
        {
            if (FailPointComparsion.NA.ToString() == value)
            {
                return -1;
            }
            else if (FailPointComparsion.LessThan.ToString() == value)
            {
                return 0;
            }
            else if (FailPointComparsion.GreaterThan.ToString() == value)
            {
                return 1;
            }
            else if (FailPointComparsion.EqualTo.ToString() == value)
            {
                return 2;
            }
            else if (FailPointComparsion.LessorEqualTo.ToString() == value)
            {
                return 3;
            }
            else if (FailPointComparsion.GreaterorequalTo.ToString() == value)
            {
                return 4;
            }
            else if (FailPointComparsion.NA.ToString() == value)
            {
                return -1;
            }
            return -1;
        }
        public List<GradingFactorBLL> Search(String Name, Nullable<Guid> GradingfactorTypeId, Nullable<GradingFactorStatus> Status)
        {
            return GradingFactorDAL.Search(Name, GradingfactorTypeId, Status);
        }
        #endregion

    }
    [Serializable]
    public class GradingFactorTypeBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public string GradingFactorTypeName { get; set; }
        public string ValueType { get; set; }
        //1 = Active , 0 = Inactive 
        public int Status { get; set; }
        public List<GradingFactorTypeBLL> GetActiveGradingfactorType()
        {
            List<GradingFactorTypeBLL> list = null;
            list = GradingFactorTypeDAL.GetActive();
            return list;
        }


    }

}
