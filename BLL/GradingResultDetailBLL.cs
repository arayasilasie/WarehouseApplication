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
using System.Data.SqlClient;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public enum GradingResultDetailStatus { New = 1, Active, InActive };
    public class GradingResultDetailBLL : GeneralBLL
    {
        #region fileds
        private Guid _id;
        private Guid _gradingResultId;
        private Guid _gradingFactorId;
        private string _receivedValue;
        private GradingResultDetailStatus _status;
        private string _gradingFactorName;
        private string _dataType;
        private string _possibleValues;
        public bool isInTotalValue { get; set; }

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
        public Guid GradingFactorId
        {
            get
            {
                return this._gradingFactorId;
            }
            set
            {
                this._gradingFactorId = value;
            }
        }
        public Guid GradingResultId
        {
            get
            {
                return this._gradingResultId;
            }
            set
            {
                this._gradingResultId = value;
            }
        }
        public string RecivedValue
        {
            get
            {
                return this._receivedValue;
            }
            set
            {
                this._receivedValue = value;
            }
        }
        public GradingResultDetailStatus Status
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
        #endregion

        public bool Add(List<GradingResultDetailBLL> list, SqlTransaction trans, Nullable<Guid> Id)
        {
            bool isSaved = false;
            if (Id == null)
            {
                trans.Rollback();
                trans.Dispose();
                return false;
            }
            try
            {
                string str = "Grading Factors(";
                foreach (GradingResultDetailBLL obj in list)
                {
                    obj.GradingResultId = (Guid)Id;
                    obj.Id = Guid.NewGuid();
                    isSaved = GradingResultDetailDAL.InsertGradingResultDetail(obj, trans);
                    if (isSaved == false)
                    {
                        throw new Exception("Unable to insert Grading Factor Values");
                    }
                    else
                    {
                        str += "[Id-" + obj.Id.ToString() + " ,GradingResultId-" + obj.GradingResultId.ToString() +
                            " ,GradingFactorId-" + obj.GradingFactorId.ToString() + " , RecivedValue-" + obj.RecivedValue.ToString() +
                            " , Status-" + obj.Status.ToString() + ", CreateBy-" + UserBLL.GetCurrentUser() + "];";
                    }
                }
                str += ")";
                isSaved = true;

                AuditTrailBLL objAt = new AuditTrailBLL();
                int at = -1;
                at = objAt.saveAuditTrailStringFormat(str, "New data Added", WFStepName.AddGradingResult.ToString(), UserBLL.GetCurrentUser(), "Add Grading Factor Values");
                if (at != 1)
                {
                    isSaved = false;
                    throw new Exception("Unbale to add Audit Trail-Grading Factor Value.");
                }
                else
                {
                    isSaved = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }
        public List<GradingResultDetailBLL> GetGradingResultDetailByGradingResultId(Guid Id)
        {
            return GradingResultDetailDAL.GetGradingResultDetailByGradingResultId(Id);
        }
        public bool HasValidGradingResult(Guid GradingResultId, Guid CommodityGradeId)
        {
            int count = 0;
            GradingFactorBLL objHasFactorsInTotalValue = new GradingFactorBLL();
            count = objHasFactorsInTotalValue.GetCountOfTotalValueGradingFactors(CommodityGradeId);
            if (count == 0)
            {
                return true;
            }
            else
            {
                //get Total Value.
                Nullable<float> totalValue = null;
                totalValue = this.GetGradingResultDetialByGradingResdultIdTotalValue(GradingResultId);
                if (totalValue == null)
                {
                    return false;
                }
                else
                {
                    CommodityGradeFactorValueBLL objValue = new CommodityGradeFactorValueBLL();
                    objValue = objValue.GetActiveValueByCommodoityGradeId(CommodityGradeId);
                    if (objValue != null)
                    {
                        return false;
                    }
                    else
                    {
                        if (objValue.MaxValue == null || objValue.MinValue == null)
                        {
                            return false;
                        }
                        else
                        {
                            if ((float)objValue.MaxValue >= totalValue && (float)objValue.MinValue <= totalValue)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        public Nullable<float> GetGradingResultDetialByGradingResdultIdTotalValue(Guid GradingResultId)
        {

            Nullable<float> totalValue = null;
            List<GradingResultDetailBLL> list = new List<GradingResultDetailBLL>();
            list = GradingResultDetailDAL.GetGradingResultWithTotalValue(Id);
            if (list == null)
            {
                return null;
            }
            else
            {
                totalValue = 0;
                foreach (GradingResultDetailBLL i in list)
                {
                    totalValue += float.Parse(i.RecivedValue);

                }
                return totalValue;
            }

        }
        public bool UpdateEach(Guid Id, string Value)
        {
            //get the old
            SqlTransaction tran;
            SqlConnection conn = Connection.getConnection();

            bool isSaved = false;
            GradingResultDetailBLL objOld = GradingResultDetailDAL.GetGradingResultDetailById(Id);
            GradingResultDetailBLL objNew = objOld;
            objNew.RecivedValue = Value;
            tran = conn.BeginTransaction();
            try
            {
                isSaved = GradingResultDetailDAL.UpdateGradingResultDetailEach(Id, Value, tran);
                AuditTrailBLL objAt = new AuditTrailBLL();
                int x = objAt.saveAuditTrail(objOld, objNew, Utility.GetApplicationName(), UserBLL.GetCurrentUser(), "Update Grading factorValue");
                if (x == 1 && isSaved == true)
                {
                    tran.Commit();
                }
                else if (isSaved == true && x == -1)
                {
                    tran.Rollback();
                    isSaved = false;
                    objAt.RoleBack();
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }
            return isSaved;


        }
        //public bool PreInsertHasValidGradingResult(float TotalValue, Guid CommodityGradeId)
        //{
        //    int count = 0;
        //    GradingFactorBLL objHasFactorsInTotalValue = new GradingFactorBLL();
        //    count = objHasFactorsInTotalValue.GetCountOfTotalValueGradingFactors(CommodityGradeId);
        //    if (count == 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {


        //            CommodityGradeFactorValueBLL objValue = new CommodityGradeFactorValueBLL();
        //            objValue = objValue.GetActiveValueByCommodoityGradeId(CommodityGradeId);
        //            if (objValue == null)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                if (objValue.MaxValue == null || objValue.MinValue == null)
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    if ((float)objValue.MaxValue >= TotalValue && (float)objValue.MinValue <= TotalValue)
        //                    {
        //                        return true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //            }

        //    }
        //}
        public bool PreInsertHasValidGradingResult(float TotalValue, Guid CommodityGradeId, out string err)
        {
            err = "The sum of Grading Values does not match with the Value for the Commodity Grade";
            int count = 0;
            GradingFactorBLL objHasFactorsInTotalValue = new GradingFactorBLL();
            count = objHasFactorsInTotalValue.GetCountOfTotalValueGradingFactors(CommodityGradeId);
            if (count == 0)
            {
                return true;
            }
            else
            {


                CommodityGradeFactorValueBLL objValue = new CommodityGradeFactorValueBLL();
                objValue = objValue.GetActiveValueByCommodoityGradeId(CommodityGradeId);
                if (objValue == null)
                {
                    return false;
                }
                else
                {
                    if (objValue.MaxValue == null || objValue.MinValue == null)
                    {
                        return false;
                    }
                    else
                    {
                        if ((float)objValue.MaxValue >= TotalValue && (float)objValue.MinValue <= TotalValue)
                        {
                            return true;
                        }
                        else
                        {
                            err = err + ": ( " + objValue.MaxValue.ToString() + "-" + objValue.MinValue + ")";
                            return false;
                        }
                    }
                }

            }
        }


        public SqlDataReader GetGradingResultDetailByGradingIdDataReader(Guid GradingId, SqlConnection conn)
        {
            return GradingResultDetailDAL.GetGradingResultDetailByGradingIdDataReader(GradingId, ref conn);
        }
    }
}
