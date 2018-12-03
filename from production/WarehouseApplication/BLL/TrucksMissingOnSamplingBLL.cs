using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum TrucksMissingOnSamplingStatus { New=1 , ReAllowed, Cancelled}
    public class TrucksMissingOnSamplingBLL : GeneralBLL
    {
        #region Fields
        private Guid _Id;
        private Guid _TrucksForSamplingId;
        private DateTime _DateTimeReported;
        private TrucksMissingOnSamplingStatus _Status;
        private String _Remark;
        private Guid _WarehouseId;
        private DateTime _ReRequestedTime;
        private bool _isRequested;
        public string TrailerPlateNo { get; set; }
        public string PlateNo { get ;set; }
        #endregion


        #region properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Guid TrucksForSamplingId
        {
            get { return _TrucksForSamplingId; }
            set { _TrucksForSamplingId = value; }
        }
        public DateTime DateTimeReported
        {
            get { return _DateTimeReported; }
            set { _DateTimeReported = value; }
        }
        public TrucksMissingOnSamplingStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public String Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }
        public DateTime ReRequestedTime
        {
            get { return _ReRequestedTime; }
            set { _ReRequestedTime = value; }
        }
        public bool IsRequested
        {
            get { return _isRequested; }
            set { _isRequested = value; }
        }
        #endregion

        #region Methods
        public   TrucksMissingOnSamplingBLL Copy(TrucksMissingOnSamplingBLL obj )
        {
            TrucksMissingOnSamplingBLL objC = new TrucksMissingOnSamplingBLL();
            objC.Id = obj.Id;
            objC.TrucksForSamplingId = obj.TrucksForSamplingId;
            objC.DateTimeReported = obj.DateTimeReported;
            objC.Status = obj.Status;
            objC.Remark = obj.Remark;
            objC.WarehouseId = obj.WarehouseId;
            objC.ReRequestedTime = obj.ReRequestedTime;
            objC.IsRequested = obj.IsRequested;
            objC.TrailerPlateNo = obj.TrailerPlateNo;
            objC.PlateNo = obj.PlateNo;
            objC.TrackingNo = obj.TrackingNo;
            objC.CreatedBy = obj.CreatedBy;
            objC.CreatedTimestamp = obj.CreatedTimestamp;
            objC.LastModifiedBy = obj.LastModifiedBy;
            objC.LastModifiedTimestamp = obj.LastModifiedTimestamp;

            return objC;
            
        }
        public bool Save(SqlTransaction tran)
        {
            bool isSaved = false;
        
            try
            {
                isSaved = TrucksMissingOnSamplingDAL.Insert(this, tran);
                if (isSaved == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(this, WFStepsName.ConfirmTrucksForsamp.ToString(), UserBLL.GetCurrentUser(), "Missing On Confirmation");
                    if (at != 1)
                    {
                        isSaved = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return isSaved;
        }
        public List<TrucksMissingOnSamplingBLL> GetAbsentTrucks(Guid Warehouseid )
        {
            return TrucksMissingOnSamplingDAL.GetAbsentTrucks(Warehouseid);
        }
        public bool UpdateStatus()
        {
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            int at = -1;
            try
            {
                TrucksMissingOnSamplingBLL objNew = new TrucksMissingOnSamplingBLL();
                TrucksMissingOnSamplingBLL objOld = TrucksMissingOnSamplingDAL.GetById(this.Id);
                objNew = objOld.Copy(objOld);
                objNew.Id = this.Id;
                objNew.Status = this.Status ;
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = TrucksMissingOnSamplingDAL.SetStatus(objNew.Id, objNew.Status, tran);
                if (isSaved == true)
                {
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(objNew, objOld, WFStepsName.TrucksMissingForSamp.ToString(), UserBLL.GetCurrentUser(), "Update Status Missing Traucks");
                    if (at == 1)
                    {
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return isSaved;
        }
        #endregion

    }
}
