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
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum SamplingResultStatus { New = 1, Approved, Cancelled }
    [Serializable]
    public class SamplingResultBLL : GeneralBLL
    {
        private Guid _id;
        private Guid _samplingId;
        private Guid _employeeId;
        private int _numberOfBags;
        private int _numberOfSeparations;
        private string _samplerComments;
        private bool _isSupervisor;
        private SamplingResultStatus _status;
        private string _remark;
        private Guid _createdBy;
        private DateTime _createdTimestamp;
        private Guid _lastModifiedBy;
        private DateTime _lastModifiedTimestamp;
        private long _sampleCode;
        private Guid _receivigRequestId;
        private string _SamplingResultCode;
        private bool _isPlompOk;
        private string _managerApprovalRemark;
        private string _managerApprovalDateTime;
        private DateTime _ResultReceivedDateTime;
        //View properties
        #region Properites
        public long SamplingCode
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
        public string SamplingResultCode
        {
            get
            {
                return this._SamplingResultCode;
            }
            set
            {
                this._SamplingResultCode = value;
            }
        }

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
        public Guid SamplingId
        {
            get
            {
                return this._samplingId;
            }
            set
            {
                this._samplingId = value;
            }
        }
        public Guid EmployeeId
        {
            get
            {
                return this._employeeId;
            }
            set
            {
                this._employeeId = value;
            }
        }
        public int NumberOfBags
        {
            get
            {
                return this._numberOfBags;
            }
            set
            {
                this._numberOfBags = value;
            }
        }
        public int NumberOfSeparations
        {
            get
            {
                return this._numberOfSeparations;
            }
            set
            {
                this._numberOfSeparations = value;
            }
        }
        public String SamplerComments
        {
            get
            {
                return this._samplerComments;
            }
            set
            {
                this._samplerComments = value;
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
        public SamplingResultStatus Status
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
        public Guid ReceivigRequestId
        {
            get { return _receivigRequestId; }
            set { _receivigRequestId = value; }
        }
        public bool IsPlompOk
        {
            get { return _isPlompOk; }
            set { _isPlompOk = value; }
        }
        public string ManagerApprovalRemark
        {
            get
            {
                return this._managerApprovalRemark;
            }
            set
            {
                this._managerApprovalRemark = value;
            }
        }
        public string ManagerApprovalDateTime
        {
            get
            {
                return this._managerApprovalDateTime;
            }
            set
            {
                this._managerApprovalDateTime = value;
            }
        }
        public DateTime ResultReceivedDateTime
        {
            get
            {
                return this._ResultReceivedDateTime;
            }
            set
            {
                this._ResultReceivedDateTime = value;
            }
        }

        #endregion

        #region Constrs

        public SamplingResultBLL()
        {

        }
        public SamplingResultBLL(SamplingResultBLL source)
        {
            this._id = source._id;
            this._samplingId = source._samplingId;
            this._employeeId = source._employeeId;
            this._numberOfBags = source._numberOfBags;
            this._numberOfSeparations = source._numberOfSeparations;
            this._samplerComments = source._samplerComments;
            this._isSupervisor = source._isSupervisor;
            this._isSupervisor = source._isSupervisor;
            this._status = source._status;
            this._remark = source._remark;
            this._createdBy = source._createdBy;
            this._createdTimestamp = source._createdTimestamp;
            this._lastModifiedBy = source._lastModifiedBy;
            this._lastModifiedTimestamp = source._lastModifiedTimestamp;
        }
        #endregion
        #region public Methods

        public Boolean Add(List<SamplingResultBLL> list, Guid SamplingId)
        {
            if (list == null)
            {
                throw new Exception("No sampling Result to save.");
            }
            SamplingBLL objSampling = new SamplingBLL();
            objSampling = objSampling.GetSampleDetail(SamplingId);

            if (objSampling == null)
            {
                throw new Exception("Invalid Tracking No.Plase Tray Again.");
            }
            if (String.IsNullOrEmpty(objSampling.TrackingNo) == true)
            {
                throw new Exception("Invalid Tracking No.Plase Tray Again.");
            }
            string OldTrackingNo = String.Empty;
            int count = 0;
            count = list.Count;
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;

            List<string> tranlist = new List<string>();
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                foreach (SamplingResultBLL i in list)
                {

                    if (count == 1)
                    {
                        i.Id = Guid.NewGuid();
                        i.TrackingNo = objSampling.TrackingNo;
                        i.ResultReceivedDateTime = this.ResultReceivedDateTime;
                        i.IsPlompOk = this.IsPlompOk;
                        if (this.IsPlompOk == true)
                        {
                            i.Status = SamplingResultStatus.Approved;
                        }
                        else
                        {
                            i.Status = SamplingResultStatus.New;
                        }
                        isSaved = SamplingResultDAL.InsertSamplingResult(i, tran);
                        int at = -1;
                        if (isSaved == true)
                        {
                            AuditTrailBLL objAt = new AuditTrailBLL();
                            at = objAt.saveAuditTrail(i, WFStepsName.AddSamplingResult.ToString(), UserBLL.GetCurrentUser(), "Add Sampling Result");
                            if (at == 1)
                            {
                                if (i.Status == SamplingResultStatus.Approved)
                                {
                                    WFTransaction.WorkFlowManager(objSampling.TrackingNo);
                                }
                            }
                            else
                            {
                                tran.Rollback();
                                isSaved = false;
                            }
                        }

                    }
                    else if (count > 1)
                    {
                        //Close previous Tracking No.
                        OldTrackingNo = objSampling.TrackingNo;
                        Guid TransactionTypeId = Guid.Empty;
                        i.ResultReceivedDateTime = this.ResultReceivedDateTime;
                        try
                        {
                            TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId("RegularCoffeeMixed");
                        }
                        catch (InvalidTransactionType ex)
                        {
                            throw new Exception("Can Not open Mixed Transaction Type please Contact the Administrator.", ex);
                        }
                        string tranNo = WFTransaction.GetTransaction(TransactionTypeId, tran);
                        if (string.IsNullOrEmpty(tranNo) == true)
                        {
                            throw new Exception("Can Not get Transaction Number please Contact the Administrator.");
                        }
                        else
                        {
                            i.Id = Guid.NewGuid();
                            i.TrackingNo = tranNo;
                            i.IsPlompOk = this.IsPlompOk;
                            i.ResultReceivedDateTime = this.ResultReceivedDateTime;
                            tranlist.Add(tranNo);
                            isSaved = SamplingResultDAL.InsertSamplingResult(i, tran);
                            int at = -1;
                            if (i.IsPlompOk == true)
                            {
                                i.Status = SamplingResultStatus.Approved;
                            }
                            else
                            {
                                i.Status = SamplingResultStatus.New;
                            }
                            if (i.Status == SamplingResultStatus.Approved)
                            {
                                //Move One Step
                                ECXWF.CMessage mess = null;
                                mess = WFTransaction.Request(tranNo);
                                if (mess == null)
                                {
                                    throw new Exception("Can Not get Message for the Tracking No.");
                                }
                                else
                                {
                                    if (WFStepName.AddSamplingResult.ToString().Trim().ToUpper() == mess.Name.Trim().ToUpper())
                                    {
                                        WFTransaction.WorkFlowManager(tranNo, mess);
                                    }
                                    else
                                    {
                                        throw new Exception("Can Not get Message for the Tracking No.");
                                    }
                                }


                            }
                            AuditTrailBLL objAt = new AuditTrailBLL();
                            at = objAt.saveAuditTrail(i, WFStepsName.AddSamplingResult.ToString(), UserBLL.GetCurrentUser(), "Add Sampling Result");
                            if (at == -1)
                            {

                                isSaved = false;
                                break;
                            }
                            if (isSaved == false)
                            {
                                break;
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(OldTrackingNo) != true)
                {
                    if ((isSaved == true) && (list.Count > 0))
                    {
                        WFTransaction.Close(OldTrackingNo);
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
                    return false;
                }




            }
            catch (Exception ex)
            {
                RemoveTransaction(tranlist);
                tran.Rollback();
                throw ex;

            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public Boolean Update()
        {
            // TODO : Check Sampling is Completed - check if Code is generated.
            int count = 0;
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            SamplingResultBLL objSamplingResultold = new SamplingResultBLL();
            objSamplingResultold = objSamplingResultold.GetSamplingResultById(this.Id);
            if (objSamplingResultold == null)
            {
                throw new Exception("Null Old Value Exception");
            }
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                count = SamplingResultDAL.GetNumberofSupervisorResults(this.Id);
                if (count > 0)
                {
                    throw new Exception("A supervisor result with new or Approved already exists.");
                }
                else
                {
                    isSaved = SamplingResultDAL.UpdateSamplingResult(this, tran);
                    if (isSaved == true)
                    {
                        int at = -1;
                        AuditTrailBLL objat = new AuditTrailBLL();
                        objat.saveAuditTrail(objSamplingResultold, this, WFStepsName.EditSamplingResult.ToString(), UserBLL.GetCurrentUser(), "Edit Sampling Result");
                        if (at == 1)
                        {
                            tran.Commit();
                        }
                        else
                        {
                            isSaved = false;
                            tran.Rollback();
                        }
                    }
                    return isSaved;
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
        public bool IsSingleSupervisorResult(Guid Id)
        {

            int count = 0;
            try
            {
                count = SamplingResultDAL.GetNumberofSupervisorResults(Id);
                if (count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SamplingResultBLL> Search(string TrackingNo, string SampleCode)
        {

            List<SamplingResultBLL> list = new List<SamplingResultBLL>();
            try
            {
                list = SamplingResultDAL.SearchSamplingResult(TrackingNo, SampleCode);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public SamplingResultBLL GetSamplingResultById(Guid Id)
        {

            try
            {
                return SamplingResultDAL.GetSamplingResultById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SamplingResultBLL GetSamplingResultBySamplingResultCode(String Code)
        {

            try
            {
                return SamplingResultDAL.GetSamplingResultBySamplingResultCode(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SamplingResultBLL GetActiveSamplingResultbySamplingId(Guid samplingId)
        {
            try
            {
                return SamplingResultDAL.GetActiveSamplingResultBySamplingId(samplingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetNumberOfSeparations(Guid SamplingResultId)
        {
            SamplingResultBLL obj = new SamplingResultBLL();
            int numberofSeparation = 0;
            obj = SamplingResultDAL.GetActiveSamplingResultBySamplingId(SamplingResultId);
            numberofSeparation = obj.NumberOfSeparations;
            return numberofSeparation;
        }
        //GradingDate
        public List<SamplingResultBLL> GetSamplesResultsPendingCoding(Guid WarehouseId, string SamplingCode)
        {
            List<SamplingResultBLL> list = new List<SamplingResultBLL>();
            //GradingDate
            list = SamplingResultDAL.GetSamplesResultsPendingCoding(WarehouseId, SamplingCode);

            List<SamplingResultBLL> listGradeDispute = new List<SamplingResultBLL>();
            //GradingDate
            listGradeDispute = GradingDisputeDAL.GetGradingDisputePendingCoding(WarehouseId, SamplingCode);

            if (listGradeDispute != null)
            {
                if (listGradeDispute.Count > 0)
                {
                    if (list != null)
                    {
                        foreach (SamplingResultBLL i in listGradeDispute)
                        {

                            list.Add(i);
                        }
                    }
                    else
                    {
                        list = listGradeDispute;
                    }
                }
            }
            return list;
        }
        public SamplingResultBLL GetSamplesResultsPendingCodingByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            SamplingResultBLL list = new SamplingResultBLL();
            list = SamplingResultDAL.GetSamplesResultsPendingCodingByTrackingNo(WarehouseId, TrackingNo);
            if (list == null)
            {
                GradingDisputeBLL objGD = new GradingDisputeBLL();
                list = objGD.GetGradingDisputePendingCodingByTrackingNo(TrackingNo);
            }
            return list;
        }
        public String[] GetSamplingResultCodeBylistTrackingNo(string TrackingNo)
        {
            return SamplingResultDAL.GetSamplingCodeBylistTrackingNo(TrackingNo);
        }
        public SamplingResultBLL GetByTrackingNo(string TrackingNo)
        {
            return SamplingResultDAL.GetSamplingResultByTrackingNo(TrackingNo);
        }
        public Boolean UpdateManagerApproval(SamplingResultBLL oldObj)
        {
            // TODO : Check Sampling is Completed - check if Code is generated.

            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            AuditTrailBLL objat = new AuditTrailBLL();
            int at = -1;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                this.LastModifiedBy = UserBLL.GetCurrentUser();
                isSaved = SamplingResultDAL.UpdateManagerApproval(this, tran);
                if (isSaved == true)
                {


                    if (oldObj == null)
                    {
                        throw new Exception("Invalid Old object-UpdateManagerApproval Method.");
                    }
                    string strOld = "(Id-" + oldObj.Id.ToString() + ")," + "(Status-" + oldObj.Status.ToString() + "),(LastModifiedBy-" + oldObj.LastModifiedBy.ToString() + ")";
                    string strNew = "(Id-" + this.Id.ToString() + ")," + "(Status-" + this.Status.ToString() + "),(LastModifiedBy-" + this.LastModifiedBy.ToString() + ")";
                    strNew += "(ManagerApprovalRemark-" + this.ManagerApprovalRemark + "),(LastModifiedTimestamp-" + this.LastModifiedTimestamp.ToString() + ")";
                    at = objat.saveAuditTrailStringFormat(strOld, strNew, WFStepsName.EditSamplingResult.ToString(), UserBLL.GetCurrentUser(), "Edit Sampling Result");
                    if (at == 1)
                    {
                        if ((oldObj.Status == SamplingResultStatus.New) && (this.Status == SamplingResultStatus.Approved))
                        {
                            ECXWF.CMessage mess = WFTransaction.Request(this.TrackingNo);
                            HttpContext.Current.Session["msg"] = mess;
                            if (mess.Name.Trim().ToUpper() == "AddSamplingResult".ToUpper())
                            {
                                WFTransaction.WorkFlowManager(this.TrackingNo);
                            }
                            else
                            {
                                throw new Exception("The Task has already been done.");
                            }
                        }
                        else if ((oldObj.Status == SamplingResultStatus.New) && (this.Status == SamplingResultStatus.Cancelled))
                        {
                            ECXWF.CMessage mess = WFTransaction.Request(this.TrackingNo);
                            HttpContext.Current.Session["msg"] = mess;
                            if (mess.Name.Trim().ToUpper() == "AddSamplingResult".ToUpper())
                            {
                                WFTransaction.Close(this.TrackingNo);
                            }
                            else
                            {
                                throw new Exception("The Task has already been done.");
                            }

                        }

                    }
                    else
                    {
                        objat.RoleBack();
                        tran.Rollback();
                        isSaved = false;
                    }



                    if (at == 1)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        isSaved = false;
                        tran.Rollback();
                    }
                }
                return isSaved;

            }
            catch (Exception ex)
            {
                if (at == 1)
                {
                    objat.RoleBack();
                }
                tran.Rollback();
                throw ex;

            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }

        }
        #endregion

        private void RemoveTransaction(List<string> list)
        {
            foreach (string s in list)
            {
                WFTransaction.Close(s);
            }

        }

    }
}
