using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum ScalingStatus { New = 1, Approved, Cancelled}
    public class ScalingBLL : GeneralBLL
    {

        #region Fields & properties
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private Guid _receivigRequestId;
        public Guid ReceivigRequestId
        {
            get { return _receivigRequestId; }
            set { _receivigRequestId = value; }
        }
        private Guid _driverInformationId;
        public Guid DriverInformationId
        {
            get { return _driverInformationId; }
            set { _driverInformationId = value; }
        }
        private Guid _gradingResultId;
        public Guid GradingResultId
        {
            get { return _gradingResultId; }
            set { _gradingResultId = value; }
        }
        private string _scaleTicketNumber;
        public string ScaleTicketNumber
        {
            get { return _scaleTicketNumber; }
            set { _scaleTicketNumber = value; }
        }
        private DateTime _dateWeighed;
        public DateTime DateWeighed
        {
            get { return _dateWeighed; }
            set { _dateWeighed = value; }
        }
        private float _grossWeightWithTruck;
        public float GrossWeightWithTruck
        {
            get { return _grossWeightWithTruck; }
            set { _grossWeightWithTruck = value; }
        }
        private float _truckWeight;
        public float TruckWeight
        {
            get { return _truckWeight; }
            set { _truckWeight = value; }
        }
        private float _grossWeight;
        public float GrossWeight
        {
            get { return _grossWeight; }
            set { _grossWeight = value; }
        }
        private ScalingStatus _status;
        public ScalingStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private int _PreWeightQueueNo;
        public int PreWeightQueueNo
        {
            get { return _PreWeightQueueNo; }
            set { _PreWeightQueueNo = value; }
        }
        private int _QueueNo;
        public int QueueNo
        {
            get { return _QueueNo; }
            set { _QueueNo = value; }
        }
        private Guid _WarehouseId;
        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }
        private DateTime _QueueDate;
        public DateTime QueueDate
        {
            get { return _QueueDate; }
            set { _QueueDate = value; }
        }
        public string GradingCode { get; set; }
        public Guid WeigherId { get; set; }
        public Nullable<Guid> TruckWeightId { get; set; }

        #endregion

        public bool Add()
        {
            
            //get Driver Info 
            DriverInformationBLL objDriverInfo = new DriverInformationBLL();
            objDriverInfo = objDriverInfo.GetById(this.DriverInformationId);
            WarehouseApplication.BLL.TruckWeight objTruckWeight = null;
            if (objDriverInfo == null)
            {
                throw new Exception("Unbale To get Driver Information");
            }
            else
            {
               objTruckWeight = new TruckWeight();
                
                objTruckWeight.Id = Guid.NewGuid();
                if (string.IsNullOrEmpty(objDriverInfo.PlateNumber) != true)
                {
                    objTruckWeight.TruckPlateNo = objDriverInfo.PlateNumber;
                }
                else
                {
                    objTruckWeight.TruckPlateNo = String.Empty;
                }
                if (string.IsNullOrEmpty(objDriverInfo.TrailerPlateNumber) != true)
                {
                    objTruckWeight.TrailerPlateNo = objDriverInfo.TrailerPlateNumber;
                }
                else
                {
                    objTruckWeight.TrailerPlateNo = String.Empty;
                }
                if((string.IsNullOrEmpty(objDriverInfo.PlateNumber) == true) && (string.IsNullOrEmpty(objDriverInfo.TrailerPlateNumber)== true) )
                {
                     this.TruckWeightId = null;
                }
                else
                {          
                    objTruckWeight.DateWeighed = this.DateWeighed;
                    objTruckWeight.Weight = this.TruckWeight;
                    this.TruckWeightId = objTruckWeight.Id;
                }
      

            }
            
            
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = new SqlConnection();
            DateTime currDate = DateTime.Today;
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            string Code = currDate.Day.ToString() +  currDate.Year.ToString().Substring(2,2);
            int? QueueNo = null;
            int? PreWeightQueueNo = null;
            try
            {
                ScalingDAL.GetQueueNumber(Code, WarehouseId, currDate,out QueueNo,out PreWeightQueueNo);
                if (QueueNo == null || PreWeightQueueNo == null)
                {
                    throw new Exception("Invalid Queue.");
                }
                this.Id = Guid.NewGuid();
                this.QueueDate = currDate;
                this.QueueNo = (int)QueueNo;
                this.PreWeightQueueNo = (int)PreWeightQueueNo;
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                this.WarehouseId = WarehouseId;
                isSaved = ScalingDAL.InsertScalingInformation(this, tran);
                if( isSaved == true )
                {
                    if(this.TruckWeightId != null)
                    {
                      if(objTruckWeight != null)
                      {
                          if( objTruckWeight.Save(tran) != true )
                          {
                              if(isSaved == true )
                              {
                                  isSaved = false;
                              }
                          }
                      }
                    }
                }
                int at = -1;
                if (isSaved == true)
                {
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(this, WFStepsName.AddScalingInfo.ToString(), UserBLL.GetCurrentUser(), "Add Scaling");
                }
                if (at == 1)
                {
                    if (isSaved == true)
                    {
                        WFTransaction.WorkFlowManager(this.TrackingNo);
                 
                    }
                    isSaved = true;
                }
                else
                {
                    isSaved = false;
                }
                if (isSaved == true)
                {
                    tran.Commit();
                    tran.Dispose();
                    conn.Close();
                    return true; ;
                }
                else
                {
                    tran.Rollback();
                    tran.Dispose();
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {

                tran.Rollback();
                tran.Dispose();
                conn.Close();
                throw ex;
            }
        }

        public bool AddNew()
        {
            DriverInformationBLL objDriverInfo = new DriverInformationBLL();
            objDriverInfo = objDriverInfo.GetById(this.DriverInformationId);
            TruckWeight objTruckWeight = null;

            if (objDriverInfo == null)
                throw new Exception("Unbale To get Driver Information");

            else
            {
                objTruckWeight = new TruckWeight();
                objTruckWeight.Id = Guid.NewGuid();
                objTruckWeight.TruckPlateNo = objDriverInfo.PlateNumber;
                objTruckWeight.TrailerPlateNo = objDriverInfo.TrailerPlateNumber;
                if ((string.IsNullOrEmpty(objDriverInfo.PlateNumber) == true) && (string.IsNullOrEmpty(objDriverInfo.TrailerPlateNumber) == true))
                {
                    this.TruckWeightId = null;
                }
                else
                {
                    objTruckWeight.DateWeighed = this.DateWeighed;
                    objTruckWeight.Weight = this.TruckWeight;
                    this.TruckWeightId = objTruckWeight.Id;
                }
            }

            bool isSaved = false;

            DateTime currDate = DateTime.Today;
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();

            string Code = currDate.Day.ToString() + currDate.Year.ToString().Substring(2, 2);
            int? QueueNo = null;
            int? PreWeightQueueNo = null;

            try
            {
                ScalingDAL.GetQueueNumber(Code, WarehouseId, currDate, out QueueNo, out PreWeightQueueNo);
                if (QueueNo == null || PreWeightQueueNo == null)
                {
                    throw new Exception("Invalid Queue.");
                }
                this.Id = Guid.NewGuid();
                this.QueueDate = currDate;
                this.QueueNo = (int)QueueNo;
                this.PreWeightQueueNo = (int)PreWeightQueueNo;
                this.WarehouseId = WarehouseId;

                SqlParameter[] paramList = new SqlParameter[18];
                string spName = "spInsertScaling";
                paramList[0] = DataAccessPoint.param("@ReceivigRequestId", SqlDbType.UniqueIdentifier, this.ReceivigRequestId);
                paramList[1] = DataAccessPoint.param("@DriverInformationId", SqlDbType.UniqueIdentifier, this.DriverInformationId);
                paramList[2] = DataAccessPoint.param("@GradingResultId", SqlDbType.UniqueIdentifier, this.GradingResultId);
                paramList[3] = DataAccessPoint.param("@ScaleTicketNumber", SqlDbType.NChar, this.ScaleTicketNumber, 50);
                paramList[4] = DataAccessPoint.param("@DateWeighed", SqlDbType.DateTime, this.DateWeighed);
                paramList[5] = DataAccessPoint.param("@GrossWeightWithTruck", SqlDbType.Float, this.GrossWeightWithTruck);
                paramList[6] = DataAccessPoint.param("@TruckWeight", SqlDbType.Float, this.TruckWeight);
                paramList[7] = DataAccessPoint.param("@GrossWeight", SqlDbType.Float, this.GrossWeight);
                paramList[8] = DataAccessPoint.param("@Status", SqlDbType.Int, this.Status);
                paramList[9] = DataAccessPoint.param("@Remark", SqlDbType.Text, this.Remark);
                paramList[10] = DataAccessPoint.param("@CreatedBy", SqlDbType.UniqueIdentifier, UserBLL.GetCurrentUser());
                paramList[11] = DataAccessPoint.param("@WarehouseId", SqlDbType.UniqueIdentifier, this.WarehouseId);
                paramList[12] = DataAccessPoint.param("@PreWeightQueueNo", SqlDbType.Int, this.PreWeightQueueNo);
                paramList[13] = DataAccessPoint.param("@QueueNo", SqlDbType.Int, this.QueueNo);
                paramList[14] = DataAccessPoint.param("@QueueDate", SqlDbType.DateTime, this.QueueDate);
                paramList[15] = DataAccessPoint.param("@WeigherId", SqlDbType.UniqueIdentifier, this.WeigherId);
                paramList[16] = DataAccessPoint.param("@Id", SqlDbType.UniqueIdentifier, this.Id);
                paramList[17] = DataAccessPoint.param("@TruckWeightId", SqlDbType.UniqueIdentifier, this.TruckWeightId);
                DataAccessPoint dal = new DataAccessPoint();

                if (dal.ExcuteProcedure(spName, paramList))
                    isSaved = true;

                //THE FOLLOWING METHOD [SaveNew()] ADDED BY SINISHAW
                if (isSaved == true && this.TruckWeightId != null && objTruckWeight != null && objTruckWeight.SaveNew() != true)
                    isSaved = false;

                int at = -1;
                if (isSaved == true)
                {
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(this, WFStepsName.AddScalingInfo.ToString(), UserBLL.GetCurrentUser(), "Add Scaling");
                }
                if (at == 1 && isSaved == true)
                    WFTransaction.WorkFlowManager(this.TrackingNo);
                else
                    isSaved = false;

                if (isSaved == true)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCount()
        {
            int count = -1;
            try
            {
                count = ScalingDAL.GetNumberofActiveScalingByGradingResultId(this.GradingResultId);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public static float GetApprovedWeightByCommoditydeposite(Guid id)
        {
            ScalingBLL obj = new ScalingBLL();
            Nullable<float> weight = null;
            try
            {
                weight = ScalingDAL.GetApprovedWeightInformationByCommodityDepositeId(id);
                if (weight != null)
                {
                    return (float)weight;
                }
                else
                {
                    throw new Exception("Total Approved weight exception.");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public static float GetApprovedWeightByGradingId(Guid id)
        {
            ScalingBLL obj = new ScalingBLL();
            Nullable<float> weight = null;
            try
            {
                weight = ScalingDAL.GetApprovedWeightInformationByGradingId(id);
                if (weight != null)
                {
                    return (float)weight;
                }
                else
                {
                    throw new Exception("Total Approved weight exception.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Guid GetScalingIdByGradingId(Guid Id)
        {
            ScalingBLL obj = new ScalingBLL();
            Nullable<Guid> ScalingId = null;
            try
            {
                ScalingId = ScalingDAL.GetApprovedScalingIdByGradingId(Id);
                if (ScalingId != null)
                {
                    return (Guid)ScalingId;
                }
                else
                {
                    throw new Exception("Invalid Scaling Id.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ScalingBLL GetById(Guid Id)
        {
            try
            {
                return   ScalingDAL.GetById(Id);
            }
            catch( Exception ex )
            {
                throw ex;
            }
        }
        public List<ScalingBLL> Search(string ScaleTicketNumber, Nullable<DateTime> stratDateWeighed, Nullable<DateTime> endDateWeighed, string TrackingNo, string GradingCode)
        {
            if ((ScaleTicketNumber == "") && (stratDateWeighed == null) && (endDateWeighed == null) && (TrackingNo == "") && (GradingCode == "") )
            {
                throw new Exception("Please Provide Search Creteria.");
            }
            List<ScalingBLL> list = new List<ScalingBLL>();
            try
            {
                list = ScalingDAL.Search(ScaleTicketNumber, stratDateWeighed, endDateWeighed, TrackingNo, GradingCode);
            }
            catch( Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public bool Update()
        {
            bool isSaved = false;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            ScalingBLL objScaling = new ScalingBLL();
            objScaling = objScaling.GetById(this.Id);
            if (objScaling == null)
            {
                throw new Exception("Null Old Value Exception");
            }
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = ScalingDAL.Update(this,tran);
                if (isSaved == true)
                {
                    int at = -1;
                    AuditTrailBLL objat = new AuditTrailBLL();
                    at = objat.saveAuditTrail(objScaling, this, WFStepsName.EditScaling.ToString(), UserBLL.GetCurrentUser(), "Update Scaling Information");
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

            }
            catch (Exception ex)
            {
                throw new Exception("An Error has ccured please try again. If the error persists Contact the Administrator", ex);
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
            return isSaved;
        }
        public String[] GetScalingCodeBylistTrackingNo(string TrackingNo)
        {
            return ScalingDAL.GetScalingCodeBylistTrackingNo(TrackingNo);
        }
    }
}
