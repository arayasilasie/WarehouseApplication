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
    public enum GradingStatus { Coded=1,Completed,Cancelled }
    public class GradingBLL : GeneralBLL
    {
        private Guid _id;
        private Guid _samplingResultId;
        private Guid _commodityRecivingId;
        private string _gradingCode;
        private DateTime _dateCoded;
        private GradingStatus _status;
        private Nullable<bool> _IsCodeReceivedAtLab;
        private Nullable<DateTime> _CodeReceivedTimeStamp;
        private String _labTechRemark;

        #region Constructors
        public GradingBLL()
        {

        }
        public GradingBLL(GradingBLL source)
        {
            this._id = source._id;
            this._samplingResultId = source._samplingResultId;
            this._commodityRecivingId = source._commodityRecivingId;
            this._gradingCode = source._gradingCode;
            this._dateCoded = source._dateCoded;
            this._status = source._status;
        }
        #endregion

        #region properties
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value ;
            }
        }
        public Guid SamplingResultId
        {
            get
            {
                return this._samplingResultId;
            }
            set
            {
                this._samplingResultId = value; ;
            }
        }
        public Guid CommodityRecivingId
        {
            get
            {
                return this._commodityRecivingId;
            }
            set
            {
                this._commodityRecivingId = value; ;
            }
        }
        public string GradingCode
        {
            get
            {
                return this._gradingCode;
            }
            set
            {
                this._gradingCode = value; ;
            }
        }
        public DateTime DateCoded
        {
            get
            {
                return this._dateCoded;
            }
            set
            {
                this._dateCoded = value;
            }
        }
        public GradingStatus Status
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
        public Nullable<bool> IsCodeReceivedAtLab
        {
            get
            {
                return _IsCodeReceivedAtLab;
            }
            set
            {
                _IsCodeReceivedAtLab = value;
            }
        }
        public Nullable<DateTime> CodeReceivedTimeStamp
        {
            get
            {
                return this._CodeReceivedTimeStamp;
            }
            set
            {
                this._CodeReceivedTimeStamp = value;
            }
        }
        public String LabTechRemark
        {
            set
            {
                this._labTechRemark = value;
            }
            get
            {
                return this._labTechRemark;
            }
        }

        #endregion

        #region private method
        private string GetRandomCode(string WarehouseNo)
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
        #endregion

        #region public method
        public bool Add(string WarehouseNo,string TrackingNo, List<GradingByBLL> list)
        {
            //Utility.LogException(new Exception(TrackingNo.ToString()));
            SqlTransaction trans;
            SqlConnection conn = Connection.getConnection();
             bool isSaved = false;
            trans = conn.BeginTransaction();
            try
            {
                SamplingResultBLL obj = new SamplingResultBLL();
                obj = obj.GetSamplingResultById(this.SamplingResultId);

               
                //Tod Do if the code is from Regrading then the Tracking Number should change.
                string TranNoGradedispute = "";
                TranNoGradedispute = TrackingNo;
                //if not from regrading.


                if (TranNoGradedispute == "")
                {
                    throw new Exception("Unable to get Tracking Number.");
                }


                this.Id = Guid.NewGuid();
                this.GradingCode = GetRandomCode(WarehouseNo);
                // TODO - Complete Work flow intgeration.
                //this.TrackingNo = TranNoGradedispute;


                this.TrackingNo = TrackingNo;
                isSaved = GradingDAL.InsertGrading(this, trans);
                
                if (isSaved == true)
                {
                    // add Graders.
                    GradingByBLL objGradingBy = new GradingByBLL();
                    isSaved = objGradingBy.Add(this.Id, list, trans);
                    if (isSaved == false)
                    {
                        throw new CodeGenerationException("Can Not generarte Grading Code Exception");
                    }
                    else
                    {
                        int at = -1;
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        at = objAt.saveAuditTrail(this, WFStepName.GenerateGradingCode.ToString(), UserBLL.GetCurrentUser(), "Get Grading Code");
                        if (at == 1)
                        {
                            isSaved = true;
                        }
                        else
                        {
                            isSaved = false;
                        }
                    }

                }
                else
                {
                    throw new CodeGenerationException("Can Not generarte Grading Code Exception");
                }



                if (isSaved == true)
                {
                   
                    HttpContext.Current.Session["msg"] = WFTransaction.Request(TrackingNo);
                    WFTransaction.WorkFlowManager(TrackingNo);
                    trans.Commit();
                    

                }
                return isSaved;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
          
            
        }
        public bool UpdateSampleCodeReceived()
        {
            //check necessary data is Suplied.
            if ((this.Id == null) && (this.Id == Guid.Empty))
            {
                throw new Exception("Invalid Parameter");
            }
            if (IsCodeReceivedAtLab == null)
            {
                throw new Exception("Invalid Parameter,IsCodeReceivedAtLab Can't be null");
            }
            if (CodeReceivedTimeStamp == null)
            {
                throw new Exception("Invalid Parameter, CodeReceivedTimeStamp Can't be null");
            }
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            int at = -1;
            AuditTrailBLL objAT = new AuditTrailBLL();
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved =  GradingDAL.UpdateSampleCodeReceived(this.Id, (bool)this.IsCodeReceivedAtLab,(DateTime) this.CodeReceivedTimeStamp, this.LabTechRemark, tran);
                if (isSaved == true)
                {
                    isSaved = false;
                    at = objAT.saveAuditTrail(this, WFStepsName.CodeSampRec.ToString(), UserBLL.GetCurrentUser(), "Receive Sample Code");
                    WFTransaction.WorkFlowManager(this.TrackingNo);
                    if (at == -1)
                    {
                        isSaved = false;
                    }
                }

                if (isSaved == true)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }

                // update the Database
            }
            catch (Exception ex)
            {
               
                if (at != -1)
                {
                    objAT.RoleBack();
                }
                tran.Rollback();
                throw ex;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }


            
            return isSaved;
        }
        public List<GradingBLL> GetGradingsPendingResult(Guid WarehouseId)
        {
            try
            {
                return GradingDAL.GetGradingsPendingResult(WarehouseId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<GradingBLL> GetGradingsPendingResultByGradingId(Guid WarehouseId, Guid GradingId )
        {
            try
            {
                return GradingDAL.GetGradingsPendingResultByGradingId(WarehouseId, GradingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<GradingBLL> GetGradingsPendingResultByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            try
            {
                return GradingDAL.GetGradingsPendingResultByTrackingNo(WarehouseId, TrackingNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<GradingBLL> GetGradingsPendingCodeReceivingByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            try
            {
                return GradingDAL.GetGradingsPendingCodeReceivingByTrackingNo(WarehouseId, TrackingNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public GradingBLL GetById(Guid Id)
        {
            GradingBLL obj = new GradingBLL();
            try
            {
                obj = GradingDAL.GetGradingById(Id);
                return obj;
            }
            catch
            {
                return null;
            }
        }
        public List<GradingBLL> GetGradingBySamplingResultId(Guid SamplingResultId)
        {
            try
            {
                return GradingDAL.GetGradingBySamplingResultId(SamplingResultId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<GradingBLL> GetGradingPenndingDispute()
        {
            try
            {
                return GradingDAL.GetGradingsPendingDispute(UserBLL.GetCurrentWarehouse());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String[] GetGradingResultCodeBylistTrackingNo(string TrackingNo)
        {
            return GradingDAL.GetGradingCodeBylistTrackingNo(TrackingNo);
        }
        public String[] GetGradingResultCodeBylistGRNEditTrackingNo(string TrackingNo)
        {
            return GradingDAL.GetGradingCodeBylistTrackingNo(TrackingNo);
        }
        public List<GradingBLL> Search(String TrackingNo, string GradingCode, string SamplingResultCode, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            try
            {
                List<GradingBLL> list = new List<GradingBLL>();
                list = GradingDAL.Search(TrackingNo, GradingCode, SamplingResultCode, from, to);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
