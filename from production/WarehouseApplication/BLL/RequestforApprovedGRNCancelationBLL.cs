using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum RequestforApprovedGRNCancelationStatus { New = 1,Completed,  Cancelled , Rejected}
    public class RequestforApprovedGRNCancelationBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid GRNId { get; set; }
        public Guid RequestedBy { get; set; }
        public DateTime DateRequested { get; set; }
        public string Remark { get; set; }
        public RequestforApprovedGRNCancelationStatus Status { get; set; }
        public string GRN_Number { get; set; }


        public bool Add()
        {
            int At = -1;
            bool isSaved = false;
            SqlConnection conn = new SqlConnection();
            SqlTransaction tran;
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            AuditTrailBLL objAt = new AuditTrailBLL();
            try
            {
                string TrackingNo = "";
                TrackingNo = WFTransaction.GetTransaction("ApprovedGRNCancel", tran);
                this.Id = Guid.NewGuid();
                this.TrackingNo = TrackingNo;
                this.CreatedBy = UserBLL.GetCurrentUser();
                this.CreatedTimestamp = DateTime.Now ;
                isSaved = RequestforApprovedGRNCancelationDAL.save(tran, this);
                isSaved = WarehouseRecieptBLL.RequestCancel(this.GRNId, TrackingNo, this.Remark);
               
               
                if (isSaved == true)
                {

                    At = objAt.saveAuditTrail(this, WFStepsName.CancelGRN.ToString(), UserBLL.GetCurrentUser(), "Add Approved GRN Cancelation Request");
                    if (At == 1 )
                    {
                        tran.Commit();
                    }
                    else
                    {
                        isSaved = false;
                    }
                }
                else
                {
                    isSaved = false;
                }


                if (isSaved != true)
                {
                    if (TrackingNo != "")
                    {
                        WFTransaction.Close(TrackingNo);
                    }
                    tran.Rollback();
                    if (At == 1)
                    {
                        objAt.RoleBack();
                    }
                }

            }
            catch (Exception ex)
            {
                tran.Rollback();
                if (At == 1)
                {
                    objAt.RoleBack();
                }
                throw new Exception("Unable to Add New record", ex);
            }
            finally
            {
                tran.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
            return isSaved;

        }
        public bool Update()
        {
            bool isSaved = false;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            RequestforApprovedGRNCancelationBLL objEdit = new RequestforApprovedGRNCancelationBLL();
            objEdit = objEdit.GetById(Id);
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = RequestforApprovedGRNCancelationDAL.Update(tran, this);
                if (isSaved == true)
                {
                    string AppMode = Utility.GetApplicationName() + " - AppGRNCancel";
                    AuditTrailBLL objAT = new AuditTrailBLL();
                    if (objAT.saveAuditTrail(objEdit, this, AppMode, UserBLL.GetCurrentUser(), "Update-ApprovedGRNCancelRequest") == 1)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }

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
                throw new Exception("Unable to update Data.", ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (tran != null)
                {
                    tran.Dispose();
                }

            }
            return isSaved;
        }
        public List<RequestforApprovedGRNCancelationBLL> Search(string GRNNo, string TrackingNo, Nullable<RequestforEditGRNStatus> status, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            if ((string.IsNullOrEmpty(GRNNo) == true) && (string.IsNullOrEmpty(TrackingNo) == true) &&
                (status == null) && (from == null) && (to == null)
                )
            {
                throw new Exception("Please provide search parameter.");
            }
            List<RequestforApprovedGRNCancelationBLL> list = null;
            try
            {
                list = RequestforApprovedGRNCancelationDAL.Search(GRNNo, TrackingNo, status, from, to);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Search please try agian", ex);
            }
            return list;
        }
        public RequestforApprovedGRNCancelationBLL GetById(Guid Id)
        {

            return RequestforApprovedGRNCancelationDAL.GetById(Id);
        }
        public RequestforApprovedGRNCancelationBLL GetByTrackingNo(string TrackingNo)
        {

            return RequestforApprovedGRNCancelationDAL.GetByTrackingNo(TrackingNo);
        }

        public bool CancelGRNCancellationRequest ( string TrackingNo )
        {
            Utility.LogException(new Exception(TrackingNo));
            bool isSaved = false;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            RequestforApprovedGRNCancelationBLL objEdit = new RequestforApprovedGRNCancelationBLL();
            objEdit = objEdit.GetByTrackingNo(TrackingNo);
            this.Id  = objEdit.Id;
            this.GRNId = objEdit.GRNId;
            this.RequestedBy = objEdit.RequestedBy;
            this.DateRequested = objEdit.DateRequested; ;
            this.Remark = objEdit.Remark;
            // set status to cancelled
            this.Status = RequestforApprovedGRNCancelationStatus.Cancelled;
            this.GRN_Number = objEdit.GRN_Number;


           
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = RequestforApprovedGRNCancelationDAL.Update(tran, this);
                if (isSaved == true)
                {
                    WFTransaction.Close(TrackingNo);
                    string AppMode = Utility.GetApplicationName() + " - AppGRNCancel";
                    AuditTrailBLL objAT = new AuditTrailBLL();
                    if (objAT.saveAuditTrail(objEdit, this, AppMode, UserBLL.GetCurrentUser(), "Update-ApprovedGRNCancelRequest") == 1)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }

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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (tran != null)
                {
                    tran.Dispose();
                }

            }
            return isSaved;
        }
    }
}
