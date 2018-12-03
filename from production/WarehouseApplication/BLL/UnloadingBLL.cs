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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public enum UnloadingStatus { New =1, Active,Rejected , Cancelled}
    public class UnloadingBLL : GeneralBLL
    {
        #region fields
            private Guid _id;
            private Guid _gradingId;
            private Guid _bagTypeId;
            private Guid _gradingResultId;    
            private Guid _receivigRequestId;
            private int _totalNumberOfBags;
            private DateTime _dateDeposited;
            private string _remark;
            private UnloadingStatus _status;
            private Guid _commodityGradeId;
            private string _gradingCode;
    
        #endregion

        #region Contr's
            public UnloadingBLL()
            {

            }
            public UnloadingBLL(UnloadingBLL source )
            {
                this._id = source._id;
                this._gradingResultId = source._gradingResultId;
                this._receivigRequestId = source._receivigRequestId;
                this._totalNumberOfBags = source._totalNumberOfBags;
                this._dateDeposited = source._dateDeposited;
                this._remark = source._remark;
                this._status = source._status;
                this.TrackingNo = source.TrackingNo;
                this.CreatedBy = source.CreatedBy;
                this.CreatedTimestamp = source.CreatedTimestamp;
                this.LastModifiedBy = source.LastModifiedBy;
                this.LastModifiedTimestamp = source.LastModifiedTimestamp;
            }
        #endregion

        #region Properties
            public Guid Id
            {
                get { return _id; }
                set { _id = value; }
            } 
            public Guid GradingId
            {
                get { return _gradingId; }
                set { _gradingId = value; }
            }
            public Guid GradingResultId
            {
                get { return _gradingResultId; }
                set { _gradingResultId = value; }
            }
            public Guid ReceivigRequestId
            {
                get { return _receivigRequestId; }
                set { _receivigRequestId = value; }
            }
            public int TotalNumberOfBags
            {
                get { return _totalNumberOfBags; }
                set { _totalNumberOfBags = value; }
            }
            public DateTime DateDeposited
            {
                get { return _dateDeposited; }
                set { _dateDeposited = value; }
            }
            public string Remark
            {
                get { return _remark; }
                set { _remark = value; }
            }
            public UnloadingStatus Status
            {
                get { return _status; }
                set { _status = value; }
            }
            public Guid CommodityGradeId
            {
                get { return _commodityGradeId; }
                set { _commodityGradeId = value; }
            }
            public string GradingCode
            {
                get { return _gradingCode; }
                set { _gradingCode = value; }
            }
            public Guid BagTypeId
            {
                get { return _bagTypeId; }
                set { _bagTypeId = value; }
            }
        #endregion

        public bool Add(List<StackUnloadedBLL> list )
        {
     
            Nullable<Guid> Id  = null;
            int Count = 1;
            Count = UnloadingDAL.GetNumberofUnloadingByGradeingResultId(this.GradingResultId);
            if (Count == 0)
            {
                SqlTransaction tran;
                SqlConnection conn = new SqlConnection();
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                try
                {
                    Id = UnloadingDAL.InsertUnloadingInformation(this, tran);

                    if (Id == null)
                    {
                        tran.Rollback();
                        conn.Close();
                        return false;
                    }
                    else
                    {
                        // add the Stack information
                        this.Id = (Guid)Id;
                        StackUnloadedBLL stackObj = new StackUnloadedBLL();
                        bool isSavedList = false;
                        isSavedList = stackObj.Add(tran, list, (Guid)Id);
                        if (isSavedList == true)
                        {
                            int at = -1;
                            AuditTrailBLL objAt = new AuditTrailBLL();
                            at = objAt.saveAuditTrail(this, WFStepsName.AddUnloadingInfo.ToString(), UserBLL.GetCurrentUser(), "Add Unloading Information");
                            if (at == 1)
                            {
                                isSavedList = true;
                            }
                            else
                            {
                                isSavedList = false;
                            }

                        }

                        if (isSavedList == true)
                        {

                            WFTransaction.WorkFlowManager(this.TrackingNo);
                        }
                        if (isSavedList == true)
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    conn.Close();
                }
            }
            else
            {
                return false; 
            }
           
           
        }
        public UnloadingBLL GetById(Guid Id)
        {
            return new UnloadingBLL();
        }
        //public List<UnloadingBLL> GetUnloadingInformation()
        //{
        //}
        private int getNumberOfUnloadingsByGradingId()
        {
            int number = 0;
            return number;
        }
        public List<UnloadingBLL> Search(string code, string trackingno)
        {

            return UnloadingDAL.SearchUnloadingInformation(code, trackingno);
        }
        public UnloadingBLL GetById()
        {
            try
            {
                return UnloadingDAL.GetUnloadingInformationById(this.Id);
            }
            catch (Exception ex)
            {

                ErrorLogger.Log(ex);
                // Redirect to error page.
                return null;
            }
        }
        public bool Update()
        {
            bool issaved = false;
            SqlTransaction tran = null; ;
            SqlConnection conn = null;
            conn = Connection.getConnection();
            
            UnloadingBLL objOld = new UnloadingBLL();
            objOld = objOld.GetById(this.Id);
            if (objOld == null)
            {
                throw new Exception("Null Old Value Exception");
            }
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                if (UnloadingDAL.UpdateUnloading(this, tran) == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(objOld, this, WFStepsName.EditUnloading.ToString(), UserBLL.GetCurrentUser(), "Edit Unloading");
                    if (at == 1)
                    {
                        tran.Commit();
                        issaved = true;
                    }
                    else
                    {
                        tran.Rollback();
                        issaved = false;

                    }
                }
                else
                {
                    tran.Rollback();
                    issaved = false;
                }
            }
            catch
            {
                tran.Rollback();
                throw new Exception("Unable to Update the record.");

            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Dispose();
            }
            return issaved;
           
        }
        public UnloadingBLL GetApprovedUnloadingByCommodityDepositeId(Guid CommodityDepositeId)
        {
            try
            {
                return UnloadingDAL.GetApprovedUnloadingInformationByCommodityDepositeId(CommodityDepositeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UnloadingBLL GetApprovedUnloadingByGradingId(Guid GradingId)
        {
            try
            {
                return UnloadingDAL.GetApprovedUnloadingInformationByGradingId(GradingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UnloadingBLL GetApprovedUnloadingByGradingResultId(Guid GradingResultId)
        {
            try
            {
                return UnloadingDAL.GetApprovedUnloadingInformationByGradingResultId(GradingResultId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(List<StackUnloadedBLL> listNew, List<StackUnloadedBLL> listOriginal)
        {
            bool issaved = false;
            SqlTransaction tran = null; ;
            SqlConnection conn = null;
            conn = Connection.getConnection();

            UnloadingBLL objOld = new UnloadingBLL();
            objOld = objOld.GetById(this.Id);
            if (objOld == null)
            {
                throw new Exception("Null Old Value Exception");
            }
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                if (UnloadingDAL.UpdateUnloading(this, tran) == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(objOld, this, WFStepsName.EditUnloading.ToString(), UserBLL.GetCurrentUser(), "Edit Unloading");
                    foreach (StackUnloadedBLL i in listNew)
                    {
                        bool isFound = false;
                        foreach (StackUnloadedBLL j in listOriginal)
                        {
                            if (i.Id == j.Id)
                            {
                                isFound = true;
                                break;
                            }
                        }
                        if (isFound == true)
                        {
                            if (i.Update(tran) == true)
                            {
                                at = 1;
                            }
                            else
                            {
                                at = -1;
                            }
                        }
                        else
                        {
                            if (i.Add(tran) == true)
                            {
                                at = 1;
                            }
                            else
                            {
                                at = -1;
                            }
                        }
                    }





                    if (at == 1)
                    {
                        tran.Commit();
                        issaved = true;
                    }
                    else
                    {
                        tran.Rollback();
                        issaved = false;

                    }
                }
                else
                {
                    tran.Rollback();
                    issaved = false;
                }
            }
            catch
            {
                tran.Rollback();
                throw new Exception("Unable to Update the record.");

            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Dispose();
            }
            return issaved;

        }

    }
}
