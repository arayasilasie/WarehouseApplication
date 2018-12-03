using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseApplication.DAL;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace WarehouseApplication.BLL
{
    [Serializable]
    public class MesseageTracking
    {
        public ECXWF.CMessage message;
        public string trackingNo;
    }
    public enum TrucksForSamplingStatus { New, Confirmed, TruckMissingOnSamplingQueue, Cancelled, Other };
    public class TrucksForSamplingBLL : GeneralBLL
    {
        #region Properties & fields
        private Guid _Id;

        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _ReceivigRequestId;

        public Guid ReceivigRequestId
        {
            get { return _ReceivigRequestId; }
            set { _ReceivigRequestId = value; }
        }
        private Guid _DriverInformationId;

        public Guid DriverInformationId
        {
            get { return _DriverInformationId; }
            set { _DriverInformationId = value; }
        }
        private Guid _WarehouseId;

        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }
        private DateTime _DateGenerated;

        public DateTime DateGenerated
        {
            get { return _DateGenerated; }
            set { _DateGenerated = value; }
        }
        private Guid _SamplerInspectorId;

        public Guid SamplerInspectorId
        {
            get { return _SamplerInspectorId; }
            set { _SamplerInspectorId = value; }
        }
        private TrucksForSamplingStatus _Status;

        public TrucksForSamplingStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }


        public string PlateNo { get; set; }
        public string TrailerPlateNo { get; set; }
        #endregion
        public static List<TrucksForSamplingBLL> GetRandomSample(Guid WarehouseId, int NumberOfTrucks)
        {

            List<TrucksForSamplingBLL> list = null;
            List<TrucksForSamplingBLL> listAll = null;
            List<TrucksForSamplingBLL> filteredList = new List<TrucksForSamplingBLL>();
            list = TrucksForSamplingDAL.GetRandomSamplingIdWithin2Hours(WarehouseId, NumberOfTrucks);
            if (list == null)
            {
                list = new List<TrucksForSamplingBLL>();
            }
                if (list.Count < NumberOfTrucks)
                {
                    int numberofRandomRequired = 0;
                    numberofRandomRequired = NumberOfTrucks - list.Count;
                    listAll = TrucksForSamplingDAL.GetRandomTrucksForSamplingId(WarehouseId, NumberOfTrucks * 3);
                    if (listAll != null)
                    {
                        if (listAll.Count <= numberofRandomRequired)
                        {
                            foreach (TrucksForSamplingBLL i in listAll)
                            {
                                list.Add(i);
                            }
                        }
                        else
                        {
                            while (list.Count < NumberOfTrucks)
                            {
                                System.Random rnd = new Random();
                                int myRnd = rnd.Next(0, numberofRandomRequired);
                                list.Add(listAll[myRnd]);
                            }

                        }






                    }

                }
            if (list != null)
            {    
            }
            else
            {
                list = TrucksForSamplingDAL.GetRandomTrucksForSamplingId(WarehouseId, NumberOfTrucks);
            }
            bool isSaved = false;
            SqlTransaction tran;
            SqlConnection connect = Connection.getConnection();
            tran = connect.BeginTransaction();
            AuditTrailBLL objAt = new AuditTrailBLL();
            if (list == null)
            {
                return null;
            }
            try
            {
                int i = 0;

                foreach (TrucksForSamplingBLL obj in list)
                {
                    if (string.IsNullOrEmpty(obj.TrackingNo) == true)
                    {
                        break;
                    }
                    bool curSaved = false;
                    obj.Id = Guid.NewGuid();
                    WFTransaction.UnlockTask(obj.TrackingNo);
                    ECXWF.CMessage msg = WFTransaction.Request(obj.TrackingNo);
                    if (msg == null)
                    {
                        // list.RemoveAt(i);
                    }
                    else
                    {
                        if (msg.Name == "GetTrucksReadyForSam")
                        {

                            curSaved = TrucksForSamplingDAL.InsertTruksForSampling(obj, tran);
                            if (curSaved == true)
                            {
                                int at = -1;

                                at = objAt.saveAuditTrail(obj, WFStepsName.GetTrucksReadyForSam.ToString(), UserBLL.GetCurrentUser(), "Add Trucks For sampling");
                                if (at == 1)
                                {
                                    curSaved = true;
                                }
                                else
                                {
                                    curSaved = false;
                                }
                            }
                            if (curSaved == true)
                            {
                                //  ECXWF.CMessage msg = WFTransaction.Request(obj.TrackingNo);
                                if (msg != null)
                                {
                                    if (msg.Name != "GetTrucksReadyForSam")
                                    {
                                        isSaved = false;
                                        throw new Exception("Invalid Task");
                                    }
                                    else
                                    {
                                        HttpContext.Current.Session["msg"] = msg;
                                        WFTransaction.WorkFlowManager(obj.TrackingNo);
                                        filteredList.Add(obj);
                                    }
                                }
                                else
                                {
                                    objAt.RoleBack();
                                    //list.RemoveAt(i);
                                }
                            }
                            else
                            {
                                // list.RemoveAt(i);
                            }

                            if (isSaved == false)
                            {
                                if (i == 0)
                                {
                                    isSaved = curSaved;
                                }
                                else
                                {
                                    isSaved = false;
                                }
                            }
                            else
                            {
                                isSaved = curSaved;

                            }
                            i++;
                        }
                        else
                        {
                            // list.RemoveAt(i);
                        }
                    }

                }
                if (isSaved == true)
                {
                    tran.Commit();
                }
                else
                {
                    filteredList = null;
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                objAt.RoleBack();
                tran.Rollback();
                filteredList = null;
                throw ex;
            }
            finally
            {
                tran.Dispose();
                connect.Close();
            }

            return filteredList;

        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is TrucksForSamplingBLL))
                return false;
            TrucksForSamplingBLL objTrucks = obj as TrucksForSamplingBLL;
            return objTrucks.TrackingNo == this.TrackingNo;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public List<TrucksForSamplingBLL> GetTruckspendingConfirmation()
        {
            List<MesseageTracking> msgList = new List<MesseageTracking>();
            List<TrucksForSamplingBLL> Newlist = new List<TrucksForSamplingBLL>();
            List<TrucksForSamplingBLL> list = new List<TrucksForSamplingBLL>();
            try
            {
                list = TrucksForSamplingDAL.GetTrucksPendingConfirmation(UserBLL.GetCurrentWarehouse());
                if (list == null)
                {
                    return null;
                }
                foreach (TrucksForSamplingBLL i in list)
                {
                    //MesseageTracking objMs = new MesseageTracking();
                    //ECXWF.CMessage msg = WFTransaction.Request(i.TrackingNo);
                    Newlist.Add(i);
                    //if (msg != null)
                    //{
                    //    i.Message = msg;
                    //    objMs.trackingNo = i.TrackingNo;
                    //    objMs.message = i.Message;
                    //    msgList.Add(objMs);


                    //}

                }
                // HttpContext.Current.Session["msgList"] = msgList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Newlist;
        }
        public bool Confirm(List<TrucksForSamplingBLL> list)
        {

            SqlTransaction tran;
            SqlConnection conn;
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();

            //List<MesseageTracking> msgList = new List<MesseageTracking>();
            //msgList = (List<MesseageTracking>)HttpContext.Current.Session["msgList"];
            try
            {
                bool isSaved = false;

                foreach (TrucksForSamplingBLL obj in list)
                {
                    if (obj.Status == TrucksForSamplingStatus.Confirmed)
                    {
                        ECXWF.CMessage msg = null;
                        try
                        {
                            msg = WFTransaction.Request(obj.TrackingNo.ToString().Trim());
                        }
                        catch
                        {
                            msg = null;
                        }
                        if (msg != null)
                        {
                            if (msg.Name.Trim().ToUpper() == "ConfirmTrucksForSamp".ToUpper().Trim())
                            {
                                isSaved = TrucksForSamplingDAL.UpdateConfirmation(obj, tran);
                                if (isSaved == true)
                                {
                                    string strNew = "(Id=" + obj.Id.ToString() + "),(Status-" + obj.Status.ToString() + "),(Remark-" + obj.Remark.ToString() + "),(LastModifiedBy-" + UserBLL.GetCurrentUser().ToString() + "),(LastModifiedTimeStamp-" + DateTime.Now.ToString() + ")";
                                    int at = -1;
                                    AuditTrailBLL objAt = new AuditTrailBLL();
                                    at = objAt.saveAuditTrailStringFormat("Truck Ready For Sampling Confirmed", strNew, WFStepsName.ConfirmTrucksForsamp.ToString(), UserBLL.GetCurrentUser(), "Confirm Trucks for sampling");
                                    if (at == 1)
                                    {
                                        #region depricatedcode
                                        //var xmsg = (from c in msgList 
                                        //        where c.trackingNo == obj.TrackingNo
                                        //        select c.message).Single();
                                        //ECXWF.CMessage msg = (ECXWF.CMessage)xmsg;


                                        //foreach (MesseageTracking i in msgList)
                                        //{
                                        //    if (i.trackingNo.Trim().ToUpper() == obj.TrackingNo.Trim().ToUpper())
                                        //    {
                                        //        msg = i.message;
                                        //    }
                                        //}
                                        //if (msg == null)
                                        //{
                                        //    throw new Exception("Invalid Message Exception");
                                        //}
                                        #endregion

                                        WFTransaction.WorkFlowManager(obj.TrackingNo.Trim(), msg);

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
                            }
                            else
                            {
                                WFTransaction.UnlockTask(obj.TrackingNo.Trim());
                            }
                        }
                    }
                    else
                    {
                        TrucksMissingOnSamplingBLL objTM = new TrucksMissingOnSamplingBLL();
                        objTM.Id = Guid.NewGuid();
                        objTM.TrucksForSamplingId = obj.Id;
                        objTM.DateTimeReported = DateTime.Now;
                        objTM.Remark = obj.Remark;
                        objTM.Status = TrucksMissingOnSamplingStatus.New;
                        objTM.CreatedBy = UserBLL.GetCurrentUser();
                        objTM.CreatedTimestamp = DateTime.Now;
                        objTM.WarehouseId = UserBLL.GetCurrentWarehouse();
                        objTM.TrackingNo = obj.TrackingNo;
                        isSaved = objTM.Save(tran);

                    }

                    if (isSaved == false)
                    {
                        tran.Rollback();
                        return false;
                    }
                }

                tran.Commit();
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
            return true;
        }



    }
}
