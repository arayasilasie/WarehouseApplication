using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum RequestforEditGRNStatus { New = 1, Approved, Cancelled, Completed }
[Serializable]
    public class RequestforEditGRNBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid GRNId { get; set; }
        public Guid RequestedBy { get; set; }
        public DateTime DateRequested { get; set; }
        public string Remark { get; set; }
        public RequestforEditGRNStatus Status { get; set; }
        public string GRN_Number { get; set; }

        public bool Add()
        {
            bool isSaved = false;
            SqlConnection conn = new SqlConnection();
            SqlTransaction tran;
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            try
            {
                string TrackingNo = "";
                TrackingNo = WFTransaction.GetTransaction("WHEditAppGRN", tran);
                this.TrackingNo = TrackingNo;
                this.Id = Guid.NewGuid();
                isSaved = RequestforEditGRNDAL.save(tran, this);

                if (isSaved == true)
                {
                    int At = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    At = objAt.saveAuditTrail(this, WFStepsName.RequestforEditGRN.ToString(), UserBLL.GetCurrentUser(), "Add Approved GRN Edit");
                    if (At == 1)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }

                }
                else
                {
                    if (TrackingNo != "")
                    {
                        WFTransaction.Close(TrackingNo);
                    }
                    tran.Rollback();
                }
                
            }
            catch (Exception ex)
            {
                tran.Rollback();
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
            RequestforEditGRNBLL objEdit = new RequestforEditGRNBLL();
            objEdit = objEdit.GetById(Id);
            AuditTrailBLL objAt = new AuditTrailBLL();
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = RequestforEditGRNDAL.Update(tran, this);
                if (isSaved == true)
                {
                    string AppMode = WFStepsName.EditAppGRN.ToString();
                    
                    if (objAt.saveAuditTrail(objEdit, this, AppMode, UserBLL.GetCurrentUser(), "Update-ApprovedGRNCancelRequest") == 1)
                    {
                        if (objEdit.Status == RequestforEditGRNStatus.New && this.Status == RequestforEditGRNStatus.Approved)
                        {
                            WFTransaction.WorkFlowManager(this.TrackingNo);
                        }
                        else if (objEdit.Status == RequestforEditGRNStatus.New && this.Status == RequestforEditGRNStatus.Approved)
                        {
                            WFTransaction.Close(this.TrackingNo);
                        }
                        tran.Commit();
                        return true;
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
            
        }
        public List<RequestforEditGRNBLL> Search(string GRNNo,string TrackingNo, Nullable<RequestforEditGRNStatus> status, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            if ((string.IsNullOrEmpty(GRNNo) == true) && (string.IsNullOrEmpty(TrackingNo) == true) &&
                (status == null) && (from == null)  && ( to == null)
                )
            {
                throw new Exception("Please provide search parameter.");
            }
            List<RequestforEditGRNBLL> list = null;
            try
            {
                list = RequestforEditGRNDAL.Search(GRNNo,TrackingNo, status, from, to);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Search please try agian", ex);
            }
            return list;
        }
        public RequestforEditGRNBLL GetById(Guid Id)
        {
  
            return RequestforEditGRNDAL.GetById(Id);
        }
        public RequestforEditGRNBLL GetByTrackingNo(string  TrackingNo)
        {

            return RequestforEditGRNDAL.GetByTrackingNo(TrackingNo);
        }
        public bool AllowGRNEdit( RequestforEditGRNStatus oldStatus, RequestforEditGRNBLL old )
        {           
            //Update Status accordingly.
            //Set GRN Status to On Edit
            bool isSaved = false;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = RequestforEditGRNDAL.Update(tran, this);
                isSaved =  GRNDAL.UpdateGRNStatus(this.GRN_Number, GRNStatus.OpenForEdit, tran);
                if (this.Status == RequestforEditGRNStatus.Approved && oldStatus == RequestforEditGRNStatus.Approved)
                {
                    // Update GRN Status to OpenForEdit
                    

                    if (isSaved == true)
                    {
                        int at = -1;
                        AuditTrailBLL objat = new AuditTrailBLL();

                        at = objat.saveAuditTrail(old, this, WFStepsName.RequestforEditGRN.ToString(), UserBLL.GetCurrentUser(), "Open GRN for Edit");
                        if (at == 1)
                        {
                            WFTransaction.WorkFlowManager(this.TrackingNo);
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                            isSaved = false;
                        }

                    }
                    else
                    {
                        isSaved = false;
                        tran.Rollback();
                    }                   

                }
                else if (this.Status == RequestforEditGRNStatus.Cancelled)
                {
                    WFTransaction.Close(this.TrackingNo);
                    isSaved = true;
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
    }
}
