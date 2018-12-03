using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseApplication.DAL;
using System.Collections;
using System.Data.SqlClient;

namespace WarehouseApplication.BLL
{
    public enum GradingDisputeStatus { New=1, Approved, Cancelled, Closed };
    [Serializable]
    public class GradingDisputeBLL : GeneralBLL
    {
        #region Fields
        private Guid _Id;
        private Guid _GradingId;
        private Guid _GradingResultId;
        private Guid _PreviousCommodityGradeId;
        private Guid _ExpectedCommodityGradeId;
        private DateTime _DateTimeRecived;
        private string _Remark;
        private int _Status;
        #endregion

        #region properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Guid GradingId
        {
            get { return _GradingId; }
            set { _GradingId = value; }
        }
        public Guid GradingResultId
        {
            get { return _GradingResultId; }
            set { _GradingResultId = value; }
        }
        public Guid PreviousCommodityGradeId
        {
            get { return _PreviousCommodityGradeId; }
            set { _PreviousCommodityGradeId = value; }
        }
        public Guid ExpectedCommodityGradeId
        {
            get { return _ExpectedCommodityGradeId; }
            set { _ExpectedCommodityGradeId = value; }
        }
        public DateTime DateTimeRecived
        {
            get { return _DateTimeRecived; }
            set { _DateTimeRecived = value; }
        }
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        #endregion

        public bool Add()
        {
            Guid TransactionTypeId = Guid.Empty;
            try
            {
                TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId("RegularCoffeeGradeDisputeId");
            }
            catch
            {
                throw new Exception("Missing Configuration Setting");
            }
            if (HasOpenGradingDispute(this.GradingId) == true)
            {
                throw new Exception("an open Grading dispute alrady exists.");
            }
            SqlConnection conn; 
            SqlTransaction tran;
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            string TransactionNo; 
            bool isSaved = false;
            try
            {

                TransactionNo = WFTransaction.GetTransaction(TransactionTypeId,tran);
                this.TrackingNo = TransactionNo;
                this.CreatedBy = UserBLL.GetCurrentUser();
                this.CreatedTimestamp = DateTime.Now;
                isSaved = GradingDisputeDAL.InsertGradingDisputeBLL(this, tran);
                int At = -1;
                AuditTrailBLL objAt = new AuditTrailBLL();
                if (isSaved == true)
                {
                    At = objAt.saveAuditTrail(this, WFStepsName.AddGradingResult.ToString(), UserBLL.GetCurrentUser(), "Add Grade Dispute");
                    if (At != 1)
                    {
                        isSaved = false;
                    }
                }


                if (isSaved == true)
                {
                    HttpContext.Current.Session["CoffeeRegradeTranNo"] = this.TrackingNo;
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
                throw ex;
            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }
        }
        public bool Edit(GradingDisputeBLL objOld)
        {
            bool isSaved = false;
            SqlConnection conn;
            SqlTransaction tran;
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            try
            {

                isSaved = GradingDisputeDAL.UpdateGradingDisputeBLL(this, tran);
                if (isSaved == true)
                {
                    //Update the WF step after cheking the status is set to approved.
                    if (this.Status == 2)
                    {
                       
                        WFTransaction.WorkFlowManager(this.TrackingNo);
                        HttpContext.Current.Session["EditGradeDisputeTranNo"] = this.TrackingNo;
                    }
                    else if (this.Status == 3)
                    {
                        
                        HttpContext.Current.Session["EditGradeDisputeTranNo"] = this.TrackingNo;
                    }
                  
                    AuditTrailBLL ATobj = new AuditTrailBLL();
                    ATobj.saveAuditTrail(objOld, this, WFStepsName.EditGradeDispute.ToString(), UserBLL.GetCurrentUser(), "Edit Grade Dispute");
                    tran.Commit();
                    tran.Dispose();
                    conn.Close();
                    return true;
                }
                else
                {
                    tran.Rollback();
                    tran.Dispose();
                    conn.Close();
                    return false;
                }
            }
            catch( Exception ex)
            {
                tran.Rollback();
                tran.Dispose();
                conn.Close();
                throw ex;
            }
        }
        public bool HasOpenGradingDispute(Guid GradingId)
        {
            int count = 0;
            try
            {
                count= GradingDisputeDAL.GetCountGradingDisputeCountByGradingId(GradingId);
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public GradingDisputeBLL GetById(Guid Id)
        {
            GradingDisputeBLL obj = new GradingDisputeBLL();
            try
            {
                obj = GradingDisputeDAL.GetById(Id);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GradingDisputeBLL GetByTransactionNo(string  TranNo)
        {
            GradingDisputeBLL obj = new GradingDisputeBLL();
            try
            {
                obj = GradingDisputeDAL.GetByTranNo(TranNo);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GradingDisputeBLL> GetGradingDisputePendingCoding(Guid WarehouseId)
        {
            List<GradingDisputeBLL> list = new List<GradingDisputeBLL>();
            GradingDisputeBLL obj = new GradingDisputeBLL();
            list = obj.GetGradingDisputePendingCoding(WarehouseId);
            return list;
        }
        public SamplingResultBLL GetGradingDisputePendingCodingByTrackingNo(string TrackingNo)
        {
            SamplingResultBLL obj = new SamplingResultBLL();
            obj = GradingDisputeDAL.GetGradingDisputePendingCodingByTrackingNo(TrackingNo);
            return obj;
        }
        public string GetTrackingNumberBySamplingResultId(Guid SamplingId)
        {
            string TrakingNo = "";
            try
            {
                TrakingNo = GradingDisputeDAL.GetTrackingNumberBySamplingResultId(SamplingId);
                return TrakingNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String[] GetReGradingRequestbyTrackingNo(string TrackingNo)
        {
            return ReSamplingDAL.GetReGradingRequestbyTrackingNo(TrackingNo);
        }
    }
}
