
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
    public enum GradingResultStatus { New = 0, Approved, Cancel, ClientAccepted, ClientRejected, MoistureFailed, GeneralRequiementfail, Undertermined }
    public class GradingResultBLL : GeneralBLL
    {
        #region  Feilds
        private Guid _id;
        private Guid _gradingId;
        private Guid _commodityGradeId;
        private GradingResultStatus _status;
        private string _remark;
        private DateTime _gradeRecivedTimestamp;
        private bool _isSupervisor;
        private Nullable<DateTime> _clientAcceptanceTimeStamp;
        private string _gradingCode;
        private Guid _CommodityDepositRequestId;
        private Guid _ClientId;
        private Guid _WarehouseId;
        private int _PreWeightQueueNo;
        private int _QueueNo;
        private DateTime _QueueDate;
        private string _commodityGradeName;
        private GradingResultStatus _GradingResult;






        #endregion

        #region Constructors

        public GradingResultBLL()
        {

        }
        public GradingResultBLL(GradingResultBLL source)
        {
            this._id = source._id;
            this._gradingId = source._gradingId;
            this._commodityGradeId = source._commodityGradeId;
            this._status = source._status;
            this._remark = source._remark;
            this._gradeRecivedTimestamp = source._gradeRecivedTimestamp;
            this._isSupervisor = source._isSupervisor;
            this.CreatedBy = source.CreatedBy;
            this.CreatedTimestamp = source.CreatedTimestamp;
            this.LastModifiedBy = source.LastModifiedBy;
            this.LastModifiedTimestamp = source.LastModifiedTimestamp;
        }
        #endregion

        #region Properties
        public Guid ID
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
        public Guid GradingId
        {
            get
            {
                return this._gradingId;
            }
            set
            {
                this._gradingId = value;
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
        public GradingResultStatus Status
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
        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
        public DateTime GradeRecivedTimeStamp
        {
            get
            {
                return this._gradeRecivedTimestamp;
            }
            set
            {
                this._gradeRecivedTimestamp = value;
            }
        }
        public bool IsSupervisor
        {
            get
            {
                return this._isSupervisor;
            }
            set
            {
                this._isSupervisor = value;
            }
        }
        public Nullable<DateTime> ClientAcceptanceTimeStamp
        {
            get
            {
                return this._clientAcceptanceTimeStamp;
            }
            set
            {
                this._clientAcceptanceTimeStamp = value;
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
                this._gradingCode = value;
            }
        }
        public Guid CommodityDepositRequestId
        {
            get { return _CommodityDepositRequestId; }
            set { _CommodityDepositRequestId = value; }
        }
        public Guid ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }
        public int PreWeightQueueNo
        {
            get { return _PreWeightQueueNo; }
            set { _PreWeightQueueNo = value; }
        }
        public int QueueNo
        {
            get { return _QueueNo; }
            set { _QueueNo = value; }
        }
        public DateTime QueueDate
        {
            get { return _QueueDate; }
            set { _QueueDate = value; }
        }
        public string CommodityGradeName
        {
            get { return _commodityGradeName; }
            set { _commodityGradeName = value; }
        }
        public GradingResultStatus GradingResult
        {
            get { return _GradingResult; }
            set { _GradingResult = value; }
        }
        public int ProductionYear;
        #endregion

        #region Public Methods
        public Nullable<Guid> Add(List<GradingResultDetailBLL> list, string TrackingNo)
        {




            bool isSaved = false;
            if (this.IsSupervisor == true)
            {
                int count = 1;
                count = GradingResultDAL.GetNumberofSupervisorResults(this.GradingId);
                if (count > 0)
                {

                    throw new Exception("Multiple Supervisor Grading result exception.");
                }
            }
            Nullable<Guid> Id = null;

            SqlConnection conn = Connection.getConnection();
            SqlTransaction tran = conn.BeginTransaction();
            this.WarehouseId = UserBLL.GetCurrentWarehouse();
            this.ID = Guid.NewGuid();
            this.CreatedBy = UserBLL.GetCurrentUser();
            this.CreatedTimestamp = DateTime.Now;
            try
            {
                Id = (Guid)GradingResultDAL.InsertGradingResult(this, tran);

                if (Id != null)
                {
                    // add audit trail 

                    int At = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();

                    At = objAt.saveAuditTrail(this, WFStepsName.AddGradingResult.ToString(), UserBLL.GetCurrentUser(), "Add Grading result");
                    if (At == 1)
                    {

                        GradingResultDetailBLL objDetail = new GradingResultDetailBLL();
                        isSaved = objDetail.Add(list, tran, Id);
                        if (isSaved == true)
                        {
                            isSaved = true;
                        }
                        else
                        {
                            if (At == 1)
                            {
                                objAt.RoleBack();
                                isSaved = false;
                            }
                        }
                        if (isSaved == true)
                        {
                            GradingBLL objGrading = new GradingBLL();
                            objGrading = objGrading.GetById(this.GradingId);
                            if (objGrading == null)
                            {
                                throw new Exception("Invalid Code.Please try again");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(objGrading.TrackingNo) == true)
                                {
                                    throw new Exception("Invalid Code.Please try again");
                                }
                            }
                            if (this.Status == GradingResultStatus.MoistureFailed || this.Status == GradingResultStatus.GeneralRequiementfail)
                            {
                                // Client Should Be Informed of the Grading Result.
                                // WFTransaction.Close(objGrading.TrackingNo);
                                WFTransaction.WorkFlowManager(objGrading.TrackingNo);
                            }
                            else
                            {
                                WFTransaction.WorkFlowManager(objGrading.TrackingNo);
                            }
                            HttpContext.Current.Session["AddGradingRecivedTranNo"] = objGrading.TrackingNo;
                        }
                    }
                    else
                    {
                        isSaved = false;

                    }
                }

                if (isSaved == true)
                {
                    tran.Commit();
                    return Id;
                }
                else
                {
                    isSaved = false;
                    tran.Rollback();
                    return null;
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

        public bool UpdateGradingResult(List<GradingResultDetailBLL> list, Nullable<Guid> Id)
        {

            bool isSaved = false;


            SqlConnection conn = Connection.getConnection();
            SqlTransaction tran = conn.BeginTransaction();
            this.WarehouseId = UserBLL.GetCurrentWarehouse();


            if (Id != null)
            {
                try
                {
                    this.ID = (Guid)Id;
                    if (GradingResultDAL.InactivateGradingResultDetails(this.ID, tran) == true)
                    {
                        int at = -1;
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        string strNew = "(Grading Result Id -" + this.ID.ToString() + "),(LastModifiedby-" + UserBLL.GetCurrentUser().ToString() + "),(LastModifiedTimeStamp-" + DateTime.Now.ToString() + ")";
                        at = objAt.saveAuditTrailStringFormat("Grading Factor Details Cancelled", strNew, WFStepsName.EditGradingResultDet.ToString(), UserBLL.GetCurrentUser(), "Cancel Grading Result Detail");
                        if (at == -1)
                        {
                            tran.Rollback();
                            isSaved = false;
                        }
                        else
                        {
                            GradingResultBLL objGR = new GradingResultBLL();
                            objGR = objGR.GetGradingResultById(this.ID);
                            if (GradingResultDAL.UpdateGradingResult(this, tran) == true)
                            {
                                AuditTrailBLL objAtGR = new AuditTrailBLL();
                                at = objAtGR.saveAuditTrail(objGR, this, WFStepsName.EditGradingResult.ToString(), UserBLL.GetCurrentUser(), "Updating Grading Result");
                                if (at == -1)
                                {
                                    tran.Rollback();
                                    isSaved = false;
                                    return isSaved;
                                }
                                GradingResultDetailBLL objDetail = new GradingResultDetailBLL();
                                isSaved = objDetail.Add(list, tran, Id);
                                if (isSaved == false)
                                {
                                    tran.Rollback();
                                    isSaved = false;

                                }
                                else
                                {
                                    tran.Commit();
                                    isSaved = true;
                                }
                            }
                            else
                            {
                                tran.Rollback();
                                isSaved = false;

                            }
                        }
                    }
                    else
                    {
                        tran.Rollback();
                        isSaved = false;
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

            return isSaved;
        }
        public bool UpdateGradingResult(List<GradingResultDetailBLL> list, Nullable<Guid> Id, string TrackingNo)
        {

            bool isSaved = false;


            SqlConnection conn = Connection.getConnection();
            SqlTransaction tran = conn.BeginTransaction();
            this.WarehouseId = UserBLL.GetCurrentWarehouse();


            if (Id != null)
            {
                try
                {
                    this.ID = (Guid)Id;
                    if (GradingResultDAL.InactivateGradingResultDetails(this.ID, tran) == true)
                    {
                        int at = -1;
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        string strNew = "(Grading Result Id -" + this.ID.ToString() + "),(LastModifiedby-" + UserBLL.GetCurrentUser().ToString() + "),(LastModifiedTimeStamp-" + DateTime.Now.ToString() + ")";
                        at = objAt.saveAuditTrailStringFormat("Grading Factor Details Cancelled", strNew, WFStepsName.EditGradingResultDet.ToString(), UserBLL.GetCurrentUser(), "Cancel Grading Result Detail");
                        if (at == -1)
                        {
                            tran.Rollback();
                            isSaved = false;
                        }
                        else
                        {
                            GradingResultBLL objGR = new GradingResultBLL();
                            objGR = objGR.GetGradingResultById(this.ID);
                            if (GradingResultDAL.UpdateGradingResult(this, tran) == true)
                            {
                                AuditTrailBLL objAtGR = new AuditTrailBLL();
                                at = objAtGR.saveAuditTrail(objGR, this, WFStepsName.EditGradingResult.ToString(), UserBLL.GetCurrentUser(), "Updating Grading Result");
                                if (at == -1)
                                {
                                    tran.Rollback();
                                    isSaved = false;
                                    return isSaved;
                                }
                                GradingResultDetailBLL objDetail = new GradingResultDetailBLL();
                                isSaved = objDetail.Add(list, tran, Id);
                                if (isSaved == true)
                                {
                                    //Request
                                   ECXWF.CMessage msg =  WFTransaction.Request(TrackingNo);
                                   if (msg != null)
                                   {
                                       if (msg.Name == "EditGradingResult")
                                       {
                                           WFTransaction.WorkFlowManager(TrackingNo, msg);
                                       }
                                   }
                                }


                                if (isSaved == false)
                                {
                                    tran.Rollback();
                                    isSaved = false;

                                }
                                else
                                {
                                    tran.Commit();
                                    isSaved = true;
                                }
                            }
                            else
                            {
                                tran.Rollback();
                                isSaved = false;

                            }
                        }
                    }
                    else
                    {
                        tran.Rollback();
                        isSaved = false;
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

            return isSaved;
        }
        public bool Update(SqlTransaction tran)
        {
            bool isSaved = false;
            try
            {
                isSaved = GradingResultDAL.UpdateGradingResult(this, tran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSaved;
        }
        public List<GradingResultBLL> Search(string trackingNo, string gradingCode)
        {
            List<GradingResultBLL> list;
            try
            {
                list = new List<GradingResultBLL>();
                list = GradingResultDAL.GetGradingResultSearch(trackingNo, gradingCode);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (GradingResultBLL i in list)
                        {
                            i.CommodityGradeName = CommodityGradeBLL.GetCommodityGradeNameById(i.CommodityGradeId);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("An error has occured please try again.If the error persists contact the administrator.", ex);
            }

        }
        public List<GradingResultBLL> GetGradingResultPendingClientAcceptance(Guid WarehouseId)
        {
            List<GradingResultBLL> list = new List<GradingResultBLL>();
            try
            {
                list = GradingResultDAL.GetGradingResultPendinngResult(WarehouseId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public GradingResultBLL GetGradingResultByGradingId(Guid Id)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetGradingResultByGradingId(Id);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public GradingResultBLL GetGradingResultById(Guid Id)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetGradingResultById(Id);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public GradingResultBLL GetGradingResultByTrackingNo(string TrackingNo)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetGradingResultByTrackingNo(TrackingNo);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Grade Dispute
        /// </summary>
        /// <param name="TrackingNo"></param>
        /// <returns></returns>
        public GradingResultBLL GetGradingResultByTrackingNoForGradeDispute(string TrackingNo)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetGradingResultByTrackingNoForGradeDispute(TrackingNo);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public GradingResultBLL GetGradingResultById(Guid Id, SqlTransaction tran)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetGradingResultById(Id, tran);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<GradingResultBLL> GetAcceptedresultsPendingUnloading(Guid warehouseId)
        {
            try
            {
                return GradingResultDAL.GetAccpetedResultsPendingUnloading(warehouseId);
            }
            catch (Exception ex)
            {
              
                throw new Exception("Unable to get the requested data.");
            }
        }
        public List<GradingResultBLL> GetAcceptedresultsPendingUnloadingByTrackingNo(Guid warehouseId, string TrackingNo)
        {
            try
            {
                return GradingResultDAL.GetAccpetedResultsPendingUnloadingByTrackingNo(warehouseId, TrackingNo);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex, 2, "Warehouse", "GetAcceptedresultsPendingUnloading");
                throw new Exception("Unable to get the requested data.");
            }
        }
        public List<GradingResultBLL> GetAcceptedresultsPendingScaling(Guid warehouseId)
        {
            try
            {
                return GradingResultDAL.GetAccpetedResultsPendingScaling(warehouseId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GradingResultBLL> GetAcceptedresultsPendingScalingByGradingCode(Guid warehouseId, string GradingCode)
        {
            try
            {
                return GradingResultDAL.GetAccpetedResultsPendingScalingByGradingCode(warehouseId, GradingCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GradingResultBLL> GetAcceptedresultsPendingScalingByTrackingNo(Guid warehouseId, string TrackingNo)
        {
            try
            {
                return GradingResultDAL.GetAccpetedResultsPendingScalingByTrackingNo(warehouseId, TrackingNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GradingResultBLL GetApprovedGradingResultByCommdityDepositerequest(Guid Id)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetApprovedGradingResultByCommdityDepositerequest(Id);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public GradingResultBLL GetApprovedGradingResultByGradingId(Guid Id)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetApprovedGradingResultByGradingId(Id);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool ClientAcceptance(Guid Id, int Status, GradingResultStatus GradeRecivedStatatus, Nullable<DateTime> ClientAccpetedTimeStamp, out string strQueueNo)
        {
            DateTime dt;
            GradingResultBLL objUpdate = new GradingResultBLL();
            objUpdate.ID = Id;
            objUpdate.Status = (GradingResultStatus)Status;
            try
            {
                dt = (DateTime)ClientAccpetedTimeStamp;
            }
            catch
            {
                throw new Exception("Invalid Client Acceptance Date.");
            }
            objUpdate.ClientAcceptanceTimeStamp = ClientAccpetedTimeStamp;
            // Get Queue No.
            DateTime currDate = DateTime.Today;
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            string Code = currDate.Day.ToString() + currDate.Year.ToString().Substring(2, 2);
            int? QueueNo = null;
            int? PreWeightQueueNo = null;

            try
            {
                GradingResultDAL.GetQueueNumber(Code, WarehouseId, currDate, out QueueNo, out PreWeightQueueNo);
                if (QueueNo == null || PreWeightQueueNo == null)
                {
                    throw new Exception("Can not get queue number");
                }
            }
            catch
            {
                new Exception("Can not get queue number.");
            }
            objUpdate.QueueDate = currDate;
            objUpdate.QueueNo = (int)QueueNo;
            strQueueNo = PreWeightQueueNo.ToString();
            objUpdate.PreWeightQueueNo = (int)PreWeightQueueNo;



            bool isSaved = false;
            SqlTransaction tran;
            SqlConnection conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            GradingResultBLL objGradingResult = new GradingResultBLL();
            objGradingResult = objGradingResult.GetGradingResultById(Id, tran);
            AuditTrailBLL objAt = new AuditTrailBLL();
            int At = -1;
            try
            {

                isSaved = GradingResultDAL.ClientAcceptanceGradingResult(objUpdate, tran);
                if (objAt.saveAuditTrail(objGradingResult, objUpdate, WFStepsName.ClientAcceptance.ToString(), UserBLL.GetCurrentUser(), "Grading Result Client Respose") == 1)
                {
                    isSaved = true;
                    At = 1;
                }

                string TranNo;
                TranNo = objGradingResult.TrackingNo;
                if (string.IsNullOrEmpty(TranNo) == true)
                {
                    tran.Rollback();
                    if (At == 1)
                    {
                        objAt.RoleBack();
                    }
                    isSaved = false;
                }
                else
                {
                    if ((int)GradingResultStatus.ClientAccepted == Status && (GradeRecivedStatatus == GradingResultStatus.New || GradeRecivedStatatus == GradingResultStatus.Approved))
                    {
                        WFTransaction.WorkFlowManager(TranNo);
                        HttpContext.Current.Session["CATranNo"] = TranNo;
                    }
                    else
                    {
                        WFTransaction.Close(TranNo);
                        //Create a new Grading Dispute task or General reqiurment fail.
                    }
                }
                if (isSaved == true)
                {
                    tran.Commit();
                    return true;
                }
                else
                {
                    tran.Rollback();
                    objAt.RoleBack();
                    HttpContext.Current.Session["CATranNo"] = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                if (At == 1)
                {
                    objAt.RoleBack();
                }
                throw ex;
            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }

        }
        public GradingResultBLL GetClientRejectedGradingResultByGradingId(Guid Id)
        {
            GradingResultBLL obj = new GradingResultBLL();
            try
            {

                obj = GradingResultDAL.GetClientRejectedGradingResultByGradingId(Id);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public String[] GetGradingResultResultCodeBylistTrackingNo(string TrackingNo)
        {
            return GradingResultDAL.GetGradingResultCodeBylistTrackingNo(TrackingNo);
        }

        #endregion


    }
}

