using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using WarehouseApplication.DAL;



namespace WarehouseApplication.BLL
{
    public enum ReSamplingStatus { New = 0, Approved, Cancelled,ReSamplingComplete };
    public class ReSamplingBLL : GeneralBLL
    {
        public ReSamplingBLL()
        {

        }
        #region private Fields
        private Guid _Id;
        private Guid _SamplingId;
        private Guid _SamplingResultId;
        private Guid _ReceivigRequestId;
        private DateTime _DateTimeRequested;
        private String _Remark;
        private int _SampleCode;
        private ReSamplingStatus _Status;

        #endregion
        #region Properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Guid SamplingId
        {
            get { return _SamplingId; }
            set { _SamplingId = value; }
        }
        public Guid SamplingResultId
        {
            get { return _SamplingResultId; }
            set { _SamplingResultId = value; }
        }
        public Guid ReceivigRequestId
        {
            get { return _ReceivigRequestId; }
            set { _ReceivigRequestId = value; }
        }
        public DateTime DateTimeRequested
        {
            get { return _DateTimeRequested; }
            set { _DateTimeRequested = value; }
        }
        public String Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public int SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        public ReSamplingStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        #endregion
        public bool isUnique(Guid SamplingResultId)
        {
            int count = -1;
            try
            {
                count = ReSamplingDAL.GetCountBySamplingResult(SamplingResultId);
            }
            catch
            {
                throw new Exception("Unable to get Count");
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Save()
        {
            SqlTransaction trans;
            Guid TransactionTypeId = Guid.Empty;
            string TransactionNo = "";
            SqlConnection conn;
            conn = Connection.getConnection();
            trans = conn.BeginTransaction();
            bool isSaved = false; ;
            try
            {
                try
                {
                    TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId("CoffeeResample");
                }
                catch
                {
                    throw new Exception("Unable to get Transaction Type.Please contact the Administrator");
                }
                try
                {
                    TransactionNo = WFTransaction.GetTransaction(TransactionTypeId, trans);
                    this.TrackingNo = TransactionNo;

                }
                catch
                {
                    throw new Exception("Unable to get Tracking Nunber.Please contact the Administrator");
                }
                //entered as New.
                this.Status = ReSamplingStatus.New;
                this.Id = Guid.NewGuid();
                isSaved = ReSamplingDAL.Insert(this, trans);
                if (isSaved == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(this, WFStepsName.AddReSamplingRequest.ToString(), UserBLL.GetCurrentUser(), "Add Re-sampling Request");
                    if (at == 1)
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("An Error Has occured please try Again.If the error persists Contact Administrator", ex);
            }
            finally
            {
                trans.Dispose();
                if (conn != null)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

        }
        public List<ReSamplingBLL> GetPendingResampling(Guid WarehouseId)
        {
            List<ReSamplingBLL> list = new List<ReSamplingBLL>();
            try
            {
                list = ReSamplingDAL.GetMoistureFailedSamples(WarehouseId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Has Occured Please Try Again.If the error Persists contact The Administrator", ex);
            }


        }
        public void LoadSamplingRealtedData(Guid SamplingResultId, DateTime RecivedDateTime)
        {
            ReSamplingBLL obj = new ReSamplingBLL();
            obj = ReSamplingDAL.GetSamplingRelatedDataBySamplingResultId(SamplingResultId);
            this.SamplingId = obj.SamplingId;
            this.SamplingResultId = obj.SamplingResultId;
            this.ReceivigRequestId = obj.ReceivigRequestId;
            this.DateTimeRequested = RecivedDateTime;
        }
        public List<ReSamplingBLL> Search(string TrackingNo, Nullable<int> previousSampleCode, Nullable<DateTime> from, Nullable<DateTime> to, Nullable<ReSamplingStatus> status)
        {
            try
            {
                return ReSamplingDAL.Search(TrackingNo, previousSampleCode, from, to, status);
            }
            catch (NULLSearchParameterException ex)
            {
                throw new Exception("Please Provide Search Parameter", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Error has occured please try again.if the error persists Contact the administrator", ex);
            }
        }
        public ReSamplingBLL GetById(Guid Id)
        {
            try
            {
                return ReSamplingDAL.GetById(Id);
            }
            catch
            {
                throw new Exception("An  error has occured please try again.if the error persists contact the adminstrator");
            }
        }
        public ReSamplingBLL GetByTrackingNo(string TrackingNo)
        {
            
            try
            {
                return ReSamplingDAL.GetByTrackingNo(TrackingNo);
            }
            catch
            {
                throw new Exception("An  error has occured please try again.if the error persists contact the adminstrator");
            }

        }
        public bool Update()
        {
            bool isSaved = false;
            SqlTransaction trans;
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            trans = conn.BeginTransaction();
            ReSamplingBLL objold = new ReSamplingBLL();
            objold = objold.GetById(this.Id);
            if (objold == null)
            {
                throw new Exception("Invalid Old Value exception");
            }
            try
            {
                isSaved = ReSamplingDAL.Update(this, trans);
               
               
                if (isSaved == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(objold, this, WFStepsName.EditResampling.ToString(), UserBLL.GetCurrentUser(), "Update Resampling");
                    if (at == 1)
                    {
                        if (this.Status == ReSamplingStatus.Approved)
                        {
                            WFTransaction.WorkFlowManager(this.TrackingNo);
                        }
                        else if (this.Status == ReSamplingStatus.Cancelled)
                        {
                            WFTransaction.Close(this.TrackingNo);
                        }
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Commit();
                        return false;
                    }
                    
                }
                else
                {
                    trans.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("Unable to Update Data", ex);
            }
            finally
            {
                trans.Dispose();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            
        }
    }
}
