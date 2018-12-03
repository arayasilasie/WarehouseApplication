using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum SamplingStatus { Active = 1, Canceled, Complete, Regenerated };
    public class SamplingBLL : GeneralBLL
    {
        #region Fields
        private Guid _Id;
        private Guid _receivigRequestId;
        private SamplingStatus _samplingStatusId;
        private int _serialNo;
        private DateTime _generatedDate;
        private string _sampleCode;
        private Guid _warehouseId;
        private Guid _createdBy;
        private DateTime _createdTimestamp;
        private Guid _lastModifiedBy;
        private DateTime _lastModifiedTimeStamp;

        public SamplerBLL _sampler;

        #endregion

        #region Properties
        public Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        public Guid ReceivigRequestId
        {
            get
            {
                return this._receivigRequestId;
            }
            set
            {
                this._receivigRequestId = value;
            }
        }
        public SamplingStatus SamplingStatusId
        {
            get
            {
                return this._samplingStatusId;
            }
            set
            {
                this._samplingStatusId = value;
            }
        }
        public int SerialNo
        {
            get
            {
                return this._serialNo;
            }
            set
            {
                this._serialNo = value;
            }
        }
        public DateTime GeneratedTimeStamp
        {
            get
            {
                return this._generatedDate;
            }
            set
            {
                this._generatedDate = value;
            }
        }
        public string SampleCode
        {
            get
            {
                return this._sampleCode;
            }
            set
            {
                this._sampleCode = value;
            }
        }
        public Guid WarehouseId
        {
            get
            {
                return this._warehouseId;
            }
            set
            {
                this._warehouseId = value;
            }
        }
        public Guid CreatedBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
            }
        }
        public DateTime CreatedTimeStamp
        {
            get
            {
                return this._createdTimestamp;
            }
            set
            {
                this._createdTimestamp = value;
            }
        }
        public Guid LastModifiedBy
        {
            get
            {
                return this._lastModifiedBy;
            }
            set
            {
                this._lastModifiedBy = value;
            }
        }
        public DateTime LastModifiedTimeStamp
        {
            get
            {
                return this._lastModifiedTimeStamp;
            }
            set
            {
                this._lastModifiedTimeStamp = value;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Serial number by incrementing one on the numeber of Samplings generated per a given date.
        /// </summary>
        /// <returns>Integer Serial number.</returns>
        public Nullable<int> GetSerialNo(Guid WarehouseId)
        {
            try
            {
                return SamplingDAL.GetSerial(WarehouseId);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Generate Serial no.", ex);
            }
        }
        /// <summary>
        /// Generates a sampling Code.
        /// </summary>
        /// <param name="WarehouseId">The Warehouse Id of the current warehouse.</param>
        /// <returns></returns>
        private string GetSamplingCode(Guid WarehouseId)
        {
            return "";
        }
        #endregion
        #region Methods
        public Nullable<Guid> InsertSample(SamplingBLL objSampling, SamplerBLL objSampler, bool isMoisture)
        {
            bool issaved = false;

            SqlConnection conn = null;
            SqlTransaction trans = null;
            int at = -1;
            Nullable<Guid> DepositeRequestId = null;
            try
            {
                conn = Connection.getConnection();
                trans = conn.BeginTransaction();
                //using (TransactionScope scope = new TransactionScope())
                //{
                DepositeRequestId = new Guid(SamplingDAL.InsertSample(objSampling, trans).ToString());
                if (DepositeRequestId == null)
                {
                    trans.Rollback();
                    return null;
                    //throw new Exception("Invalid Sample Id.");
                }
                objSampling.Id = (Guid)DepositeRequestId;
                AuditTrailBLL objAt = new AuditTrailBLL();
                at = objAt.saveAuditTrail(objSampling, WFStepsName.AddSampleCoding.ToString(), UserBLL.GetCurrentUser(), "Get Sample Ticket");
                if (at == 1)
                {
                    at = -1;
                    objSampler.SampleingTicketId = (Guid)DepositeRequestId;
                    objSampler.Id = Guid.NewGuid();
                    issaved = SamplerDAL.InsertSampler(objSampler, trans);
                    if (issaved == true)
                    {
                        HttpContext.Current.Session["msg"] = null;
                        WFTransaction.UnlockTask(objSampling.TrackingNo);
                        ECXWF.CMessage mess = WFTransaction.Request(objSampling.TrackingNo);
                        HttpContext.Current.Session["msg"] = mess;
                        if (mess.Name.Trim() == "GetSampleTicket".Trim())
                        {
                            WFTransaction.WorkFlowManager(objSampling.TrackingNo);
                            trans.Commit();


                        }
                        else
                        {
                            objAt.RoleBack();
                            trans.Rollback();
                            DepositeRequestId = null;
                        }


                    }
                    else
                    {
                        objAt.RoleBack();
                        trans.Rollback();
                        DepositeRequestId = null;
                    }
                }
                else
                {
                    trans.Rollback();
                    DepositeRequestId = null;
                    throw new Exception("Unable to log Audit Trail");
                }
                //    scope.Complete();
                // }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                DepositeRequestId = null;
                throw ex;
            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
            return DepositeRequestId;
        }
        /// <summary>
        /// This method picks a single Sample from the Commodity Deposite Requests based upon DateTime reciced.
        /// 
        /// </summary>
        /// <param name="WarehouseId"></param>
        /// <returns>Sampling Id</returns>
        public static Nullable<Guid> GetRandomSample(Guid WarehouseId)
        {
            Nullable<Guid> id = null;
            // id = SamplingDAL.GetRandomSamplingIdWithin2Hours(WarehouseId);
            //if (id == null)
            //{
            id = SamplingDAL.GetRandomSamplingId(WarehouseId);
            if (id == null)
            {
                throw new Exception("There are no enteries pending Sampling.");
            }
            //}
            return id;
        }
        public static Nullable<Guid> GetRandomReSampling(Guid WarehouseId, out Guid MoistureId, out string TransactionId)
        {
            Nullable<Guid> id = null;
            try
            {
                id = SamplingDAL.GetRandomReSamplingId(WarehouseId, out MoistureId, out TransactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
        public static int GetSerial(Guid WarehouseId)
        {
            try
            {
                return Convert.ToInt32(SamplingDAL.GetSerial(WarehouseId));
            }
            catch (Exception ex)
            {
                throw new Exception("Inavlid Serial Number exception", ex);

            }
        }
        public SamplingBLL GetSampleDetail(Guid SampleTicketId)
        {
            return SamplingDAL.GetSampleById(SampleTicketId);
        }
        //18-01-2012
        public List<SamplingBLL> GetSamplesPenndingResult(Guid WarehouseId, string TranNo)
        {
            try
            {
                return SamplingDAL.GetSamplesPendingResult(WarehouseId, TranNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SamplingBLL> GetSamplesPendingCoding(Guid WarehouseId)
        {
            try
            {
                List<SamplingBLL> list = new List<SamplingBLL>();
                list = SamplingDAL.GetSamplesPendingCoding(WarehouseId);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public SamplingBLL GetApprovedSamplesByReceivigRequestId(Guid ReceivigRequestId)
        {
            try
            {
                return SamplingDAL.GetApprovedSampleByReceivigRequestId(ReceivigRequestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public SamplingBLL GetApprovedSamplesByGradingId(Guid GradingId)
        {
            try
            {
                return SamplingDAL.GetApprovedSampleByGradingId(GradingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SamplingBLL> GetSamplesPenndingResultByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            try
            {
                return SamplingDAL.GetSamplesPendingResultByTrackingNo(WarehouseId, TrackingNo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static string GetTransactionNumber(Guid Id)
        {
            return SamplingDAL.GetTransactionNumber(Id);
        }
        /// <summary>
        /// Searchs sampling by either of the  
        /// </summary>
        /// <param name="TrackingNo"></param>
        /// <param name="SamplingCode"></param>
        /// <returns></returns>
        public static List<SamplingBLL> SearchSampling(string TrackingNo, string SamplingCode, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            List<SamplingBLL> list = null;
            list = SamplingDAL.Search(TrackingNo, SamplingCode, from, to);
            return list;
        }
        #endregion
        private int GetRandom(int max)
        {
            Random rd = new Random();
            if (Convert.ToInt32(rd) > max)
            {
                return GetRandom(max);
            }
            else
            {
                return Convert.ToInt32(rd);
            }
        }
        public String[] GetSamplingCodeBylistTrackingNo(string TrackingNo)
        {
            return SamplingDAL.GetSamplingCodeBylistTrackingNo(TrackingNo);
        }
        public String[] GetMixedSamplingCodeBylistTrackingNo(string TrackingNo)
        {
            return SamplingDAL.GetSamplingCodeForMixedBylistTrackingNo(TrackingNo);
        }
        //SampleDate 
        public bool UpdateDateCoded()
        {
            bool isSaved = false;
            isSaved = SamplingDAL.UpdateDateSampled(this.Id, this.GeneratedTimeStamp);
            return isSaved;
        }
    }
}
