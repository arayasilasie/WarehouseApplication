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
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.DAL ;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic ;

namespace WarehouseApplication.BLL
{
    [Serializable]
    public class CommodityDepositeRequestBLL : GeneralBLL
    {
        private Guid _id;
        private string _transactionId;
        private Guid _clientId;
        private string _clinetName;
        private Guid _commodityId;
        private string _CommodityName;
        private Guid _warehouseId;
        private Guid _representativeId;
        private Guid _woredaId;
        private int  _productionYear;
        private int _numberofBags;
        private DateTime _dateTimeRecived;
        private float _weight;
        private string _remark;
        private int _status;
        private string _voucherNo;
        
#region Properties
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
        public string TransactionId
        {
            get
            {
                return this._transactionId;
            }
            set
            {
                this._transactionId = value;
            }
        }
        public Guid ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
            }
        }
        public string ClientName
        {
            get
            {
                return this._clinetName;
            }
            set
            {
                this._clinetName = value;
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
        public Guid RepresentativeId
        {
            get
            {
                return this._representativeId;
            }
            set
            {
                this._representativeId = value;
            }
        }
        public Guid WoredaId
        {
            get
            {
                return this._woredaId;
            }
            set
            {
                this._woredaId = value;
            }
        }
        public int ProductionYear
        {
            get
            {
                return this._productionYear;
            }
            set
            {
                this._productionYear = value;
            }
        }
        public int NumberofBags
        {
            get
            {
                return this._numberofBags;
            }
            set
            {
                this._numberofBags = value;
            }
        }
        public DateTime  DateTimeRecived
        {
            get
            {
                return this._dateTimeRecived;
            }
            set
            {
                this._dateTimeRecived = value;
            }
        }
        public float Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                this._weight = value;
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
        public int Status
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
       
        
        
        public Guid CommodityId
        {
            get
            {
                return this._commodityId;
            }
            set
            {
                this._commodityId = value;
            }

        }
        public String CommodityName
        {
            get
            {
                return this._CommodityName;
            }
            set
            {
                this._CommodityName = value;
            }
        }
        public String VoucherNo
        {
            get
            {
                return this._voucherNo;
            }
            set
            {
                this._voucherNo = value;
            }
        }

        #endregion
#region Constructors 
        public CommodityDepositeRequestBLL()
        {
        }

        public CommodityDepositeRequestBLL(Guid id,string  TranId,Guid clientId,Guid warehouseid,Guid RepId,
            Guid woredaId, int prodYear,int noBags, DateTime dateTimeRec, float weight, string remark,
            int status , Guid createdBy, DateTime createdDate, Guid lastModifiedby, DateTime lastModifiedDate
            )
        {
            this.Id = id;
            this.TransactionId = TranId;
            this.ClientId = clientId;
            this.WarehouseId = warehouseid;
            this.RepresentativeId = RepId;
            this.WoredaId = woredaId;
            this.ProductionYear = prodYear;
            this.NumberofBags = noBags;
            this.DateTimeRecived = dateTimeRec;
            this.Weight = weight;
            this.Remark = this.Remark;
            this.Status = status;
            this.CreatedBy = createdBy;
            this.CreatedTimestamp = createdDate;
           

        }
        /// <summary>
        /// For the use of saving new Entry.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="commodityId"></param>
        /// <param name="warehouseid"></param>
        /// <param name="RepId"></param>
        /// <param name="woredaId"></param>
        /// <param name="prodYear"></param>
        /// <param name="noBags"></param>
        /// <param name="dateTimeRec"></param>
        /// <param name="weight"></param>
        /// <param name="remark"></param>
        /// <param name="status"></param>
        /// <param name="lastModifiedBy"></param>
        public CommodityDepositeRequestBLL(string TransactionId, Guid clientId, Guid commodityId, Guid warehouseid, Guid RepId,
             Guid woredaId, int prodYear, int noBags, DateTime dateTimeRec, float weight, string remark,
            int status, Guid lastModifiedBy )
        {

            this._transactionId = TransactionId;
            this.ClientId = clientId;
            this.WarehouseId = warehouseid;
            this.RepresentativeId = RepId;
            this.WoredaId = woredaId;
            this.ProductionYear = prodYear;
            this.NumberofBags = noBags;
            this.DateTimeRecived = dateTimeRec;
            this.Weight = weight;
            this.Remark = remark ;
            this.Status = status;
            this.CommodityId = commodityId;
            this.CreatedBy  = UserBLL.GetCurrentUser();


        }
#endregion

        /// <summary>
        /// A Method to insert Commodity Deposite request.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="clientId"></param>
        /// <param name="commodityId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="reprsentativeId"></param>
        /// <param name="woredaId"></param>
        /// <param name="productionYear"></param>
        /// <param name="NumberOfBags"></param>
        /// <param name="dateTimeRecived"></param>
        /// <param name="weight"></param>
        /// <param name="remark"></param>
        /// <param name="status">Status of the CDR , 1- Apporved 0, Cancelled</param>
        /// <param name="createdBy">The user who created the Record </param>
        /// <returns>if Sucessfull returns True.</returns>
        public Nullable<Guid> AddCommodityDepositRequest(Guid TransactionTypeId)
        {
            // validate
            // 0-not saved
            Nullable<Guid> Id = null;
            SqlTransaction tran;
            SqlConnection conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            CommodityDepositRequest ObjCommDepositeRequest = new CommodityDepositRequest();
            string TransactionNo = String.Empty;
            int auditTrailStatus = -1;
                
                
                try
                {
                    TransactionNo = WFTransaction.GetTransaction(TransactionTypeId, tran);
                    if( string.IsNullOrEmpty(TransactionNo))
                    {
                        throw new Exception("Unbale to Get Tracking No for Transaction Type " + TransactionTypeId);
                    }
                    this.TransactionId = TransactionNo;
                    Id = ObjCommDepositeRequest.InsertCommodityDepositeRequest(this, tran);
                    this.Id = (Guid)Id;
                    if (Id != null)
                    {
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        auditTrailStatus = objAt.saveAuditTrail(this,WFStepsName.AddArrival.ToString(), UserBLL.GetCurrentUser(), "Insert Arrival");
                        if (auditTrailStatus == -1)
                        {
                            if (TransactionNo != "")
                            {
                                WFTransaction.Remove(TransactionNo);
                            }
                            return null;
                        }
                        else
                        {

                            HttpContext.Current.Session["TransactionNo"] = TransactionNo;
                            tran.Commit();
                            return Id;
                        }
                    }
                    else
                    {
                        if (TransactionNo != "")
                        {
                            WFTransaction.Remove(TransactionNo);
                        }
                        tran.Rollback();

                    }
            
                    
                }
                catch (Exception ex)
                {
                    if (TransactionNo != "")
                    {
                        WFTransaction.Remove(TransactionNo);
                    }
                    tran.Rollback();
                    throw new Exception("TransactionNo -" + TransactionNo + "TransactionTypeId " + TransactionTypeId , ex);
                }
                finally
                {
                    conn.Close();
                    tran.Dispose();
                }

                return Id;


           

        }
        public bool EditCommodityDepositRequest(Guid ClientId , Guid commodityDepositRequestId, Guid commodityId, Guid representativeId,
                int productionYear, Guid woreda, float weight, int numberofBags, DateTime dateTimeRecived, string remark,
                int status, Guid lastModifiedBy , CommodityDepositeRequestBLL OldObject 
            )
        {
            int isSaved = 0;


            #region Validations
            
            
            //if (CommodityDepositRequestId == null)
            //{
            //    throw new Exception("Invalid Commodity Deposit Request Id");
            //}
            //if (commodityId == null )
            //{
            //    throw new Exception("Invalid Commodity Id");
            //}
            //if (representativeId == null)
            //{
            //    throw new Exception("Invalid Representative Id");
            //}
            //if (woreda == null)
            //{
            //    throw new Exception("Invalid Woreda");
            //}
            //if (dateTimeRecived == null)
            //{
            //    throw new Exception("Invalid Date Time Recived");
            //}

            //if (remark == null || remark == "")
            //{
            //    throw new Exception("Invalid Remark");
            //}

            //if (status == null || status == "")
            //{
            //    throw new Exception("Invalid status");
            //}
            //if (lastModifiedBy == null )
            //{
            //    throw new Exception("Invalid last Modified by");
            //}
            #endregion

            // has GRN on Rdit mood
            bool canBeEdited = false;
            canBeEdited = CommodityDepositeRequestBLL.isGRNEditable(commodityDepositRequestId);
            if (canBeEdited == false)
            {
                throw new GRNNotOnUpdateStatus("A GRN has already been created for this Deposit request, please request GRN the manger to edit this GRN.");
            }

            CommodityDepositeRequestBLL objEdit = new CommodityDepositeRequestBLL();
            //NoClient
            objEdit = objEdit.GetCommodityDepositeDetailById(commodityDepositRequestId);
            string trNo = objEdit.TrackingNo;

            objEdit.Id = commodityDepositRequestId;
            objEdit.CommodityId = commodityId;
            objEdit.RepresentativeId = representativeId;
            objEdit.ProductionYear = productionYear;
            objEdit.WoredaId = woreda;
            objEdit.Weight = weight;
            objEdit.NumberofBags = numberofBags;
            objEdit.DateTimeRecived = dateTimeRecived;
            objEdit.Remark = remark;
            objEdit.Status = status;
            objEdit.LastModifiedBy = lastModifiedBy;
            objEdit.ClientId = ClientId;

            bool returnstatus = false;
            int atStatus = 0;
            SqlTransaction tran;
            SqlConnection conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            try
            {
                isSaved = CommodityDepositRequest.UpdateCommodityDepositeRequest(objEdit,tran);
                if (isSaved == 0)
                {
                    returnstatus = false;
                }
                else
                {
                    //NoClient
                    if (WFTransaction.GetMessage(trNo) == "UpdateClientNo")
                    {
                        if (objEdit.ClientId != Guid.Empty)
                        {
                            WFTransaction.UnlockTask(trNo);
                            WarehouseApplication.ECXWF.CMessage msg = WFTransaction.Request(trNo);
                            WFTransaction.WorkFlowManager(trNo, msg);
                        }
                    }
                    
                    AuditTrailBLL objAT = new AuditTrailBLL();
                    atStatus = objAT.saveAuditTrail(OldObject, objEdit, WFStepsName.ArrivalUpdate.ToString(), UserBLL.GetCurrentUser(), "Update Arrival");
                    if (atStatus == 1)
                    {
                        returnstatus = true;
                        tran.Commit();
                    }
                    

                }
            }
            catch
            {
                tran.Rollback();
               
            }
            finally
            {
                tran.Dispose();
                conn.Close();
                
            }
            return returnstatus;
        }

        /// <summary>
        /// Search Commodity deposite by TransqactionId
        /// </summary>
        /// <param name="trackingNo">The Transaction Id assiged and used by Traking no.</param>
        /// <returns></returns>
        public  List<CommodityDepositeRequestBLL> SearchCommodityDeposite(string trackingNo, string VoucherNo, Nullable<Guid> Clientid,  Nullable<Guid> CommodityId, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            List<CommodityDepositeRequestBLL> list = new List<CommodityDepositeRequestBLL>();
            list = CommodityDepositRequest.SearchCommodityDeposite(trackingNo, VoucherNo, Clientid, CommodityId, from, to);
            if (list != null)
            {
                List<CommodityDepositeRequestBLL> listMerged = new List<CommodityDepositeRequestBLL>();
                for( int x = 0; x < list.Count ; x++ )
                {
                    list[x].ClientName = ClientBLL.GetClinetNameById(list[x].ClientId);
                    list[x].CommodityName = CommodityGradeBLL.GetCommodityById(list[x].CommodityId).Commodity;

                }               
                return list;
            }
            else
            {
                return null;
            }
        }
        public CommodityDepositeRequestBLL GetCommodityDepositeDetailById(Guid CommodityDepositeId)
        {
            CommodityDepositRequest objCommDepReq = new CommodityDepositRequest();
            DataSet dsResult = new DataSet();
            dsResult = objCommDepReq.getCommodityDepositRequestById(CommodityDepositeId);
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
                obj.ClientId  = new Guid(dsResult.Tables[0].Rows[0]["ClientId"].ToString());
                if (dsResult.Tables[0].Rows[0]["ProductionYear"] != DBNull.Value)
                {
                    obj.ProductionYear = Convert.ToInt32(dsResult.Tables[0].Rows[0]["ProductionYear"].ToString());
                }
                obj.DateTimeRecived = Convert.ToDateTime(dsResult.Tables[0].Rows[0]["DateTimeRecived"].ToString());
                obj.TrackingNo = dsResult.Tables[0].Rows[0]["TransactionId"].ToString();
                obj.WarehouseId = new Guid(dsResult.Tables[0].Rows[0]["WarehouseId"].ToString());
                obj.CommodityId = new Guid(dsResult.Tables[0].Rows[0]["CommodityId"].ToString());
                if (dsResult.Tables[0].Rows[0]["WoredaId"] != DBNull.Value)
                {
                    obj.WoredaId = new Guid(dsResult.Tables[0].Rows[0]["WoredaId"].ToString());
                }
                return obj;
            }
            else
            {
                return null;
            }
        }
        public CommodityDepositeRequestBLL GetCommodityDepositeDetailByTrackingNo(string  TrackingNo)
        {
            CommodityDepositRequest objCommDepReq = new CommodityDepositRequest();
            DataSet dsResult = new DataSet();
            dsResult = objCommDepReq.getCommodityDepositRequestByTrackingNo(TrackingNo);
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
                obj.Id = new Guid(dsResult.Tables[0].Rows[0]["Id"].ToString());
                obj.ClientId = new Guid(dsResult.Tables[0].Rows[0]["ClientId"].ToString());
                if (dsResult.Tables[0].Rows[0]["ProductionYear"] != DBNull.Value)
                {
                    obj.ProductionYear = Convert.ToInt32(dsResult.Tables[0].Rows[0]["ProductionYear"].ToString());
                }
                obj.DateTimeRecived = Convert.ToDateTime(dsResult.Tables[0].Rows[0]["DateTimeRecived"].ToString());
                obj.TrackingNo = dsResult.Tables[0].Rows[0]["TransactionId"].ToString();
                obj.WarehouseId = new Guid(dsResult.Tables[0].Rows[0]["WarehouseId"].ToString());
                obj.CommodityId = new Guid(dsResult.Tables[0].Rows[0]["CommodityId"].ToString());

                return obj;
            }
            else
            {
                return null;
            }
        }

        private static List<CommodityDepositeRequestBLL> MergeWithClient(List<CommodityDepositeRequestBLL> myList)
        {
            //Get Client list.
            List<CommodityDepositeRequestBLL> CDList = new List<CommodityDepositeRequestBLL>();
            List<ClientBLL> ClientList = new List<ClientBLL>();
            ClientList = ClientBLL.GetAllClient();


            List<CommodityGradeBLL> CommGradeList = new List<CommodityGradeBLL>();
            CommGradeList = getAllCommodities();


            if (ClientList == null)
            {
                //throw new ClientInformationException("Can not get Clinet Information");
            }
            else
            {
                var q = from CD in myList
                        join client in ClientList on CD.ClientId equals client.ClientUniqueIdentifier 
                        select new { CD.Id, CD.TrackingNo, CD.VoucherNo, client.ClientName, CD.CommodityId, CD.DateTimeRecived };

                foreach (var i in q)
                {
                    CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
                    obj.Id = i.Id;
                    obj.TrackingNo = i.TrackingNo;
                    obj.VoucherNo = i.VoucherNo;
                    obj.ClientName = i.ClientName;
                    obj.CommodityId = i.CommodityId;
                    obj.DateTimeRecived = i.DateTimeRecived;
                    CDList.Add(obj);
                }


                return CDList;

            }
            return null;
        }
        private static List<CommodityDepositeRequestBLL> MergeWithCommodity(List<CommodityDepositeRequestBLL> myList)
        {
            List<CommodityDepositeRequestBLL> CDList = new List<CommodityDepositeRequestBLL>();
            List<CommodityGradeBLL> CList = new List<CommodityGradeBLL>();
            CList = CommodityGradeBLL.GetAllCommodity();
            var q = from CD in myList
                    join c in CList on CD.CommodityId equals c.CommodityId
                    select new { CD.Id, CD.TrackingNo, CD.VoucherNo,CD.ClientId, CD.ClientName, CD.CommodityId, CD.DateTimeRecived , c.Commodity};

            foreach (var i in q)
            {
                CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
                obj.Id = i.Id;
                obj.TrackingNo = i.TrackingNo;
                obj.VoucherNo = i.VoucherNo;
                obj.ClientName = i.ClientName;
                obj.ClientId = i.ClientId;
                obj.CommodityId = i.CommodityId;
                obj.DateTimeRecived = i.DateTimeRecived;
                obj.CommodityName = i.Commodity;
                CDList.Add(obj);
            }
            return CDList;

        }
        private static List<CommodityGradeBLL> getAllCommodities()
        {
            List<CommodityGradeBLL> myList = new List<CommodityGradeBLL>();
            myList = CommodityGradeBLL.GetAllCommodity();
            return myList;
        }
        private static bool isGRNEditable(Guid CommodityDepositeId)
        {
            GRNBLL objGRN = new GRNBLL();
            return objGRN.IsEditableGRN("CommodityRecivingId='" + CommodityDepositeId.ToString() + "'");

        }


        /// <summary>
        /// A function that collects values of CDR, Driver and Voucher information and Save it using
        /// DataAccessPoint Class from DAL through Stored Procedure called "ExecuteProcedure"
        /// </summary>
        /// <param name="TransactionTypeId"></param>
        /// <param name="objDriver"></param>
        /// <param name="objVoucher"></param>
        /// <returns>
        /// Id with type Guid to form. Id is PK for CDR and FK for Driver and Voucher Information
        /// </returns>
        public Guid AddArrival(Guid TransactionTypeId, DriverInformationBLL objDriver, VoucherInformationBLL objVoucher)
        {
            try
            {
                this.Id = Guid.NewGuid();
                string TransactionNo = WFTransaction.GetTransaction(TransactionTypeId);
                this.TransactionId = TransactionNo;
                DataAccessPoint objDAL = new DataAccessPoint();
                objDAL.ExcuteProcedure("spInsertArrival", ParamList(this, objDriver, objVoucher));
                return Id;
                //1st argument is the procedure name
                //2nd arguemnt is a method found in this class that returns all tables parameter list
                //SaveByText found at DAL and it executes the procedure 
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save arrival information! " + ex.Message.ToString());
            }
        }
        /// <summary>
        /// Collectes all existing values of selected information and assign to scalar variables
        /// And Update old values using DataAccessPoint class from DAL through stored Procedure 
        /// called "ExecuteProcedure"
        /// </summary>
        /// <param name="objDriver"></param>
        /// <param name="objVoucher"></param>
        public void UpdateArrival(DriverInformationBLL objDriver, VoucherInformationBLL objVoucher)
        {
            SqlParameter[] paramList = new SqlParameter[23];
            //COMMODITY DEPOSIT REQUEST PARAMETERS
            paramList[0] = this.param("@CommodityDepositRequestId", SqlDbType.UniqueIdentifier, this.Id);
            paramList[1] = this.param("@ClientId", SqlDbType.UniqueIdentifier, this.ClientId);
            paramList[2] = this.param("@CommodityId", SqlDbType.UniqueIdentifier, this.CommodityId);
            paramList[3] = this.param("@WoredaId", SqlDbType.UniqueIdentifier, this.WoredaId);
            paramList[4] = this.param("@ProductionYear", SqlDbType.Int, this.ProductionYear);
            paramList[5] = this.param("@Weight", SqlDbType.Float, this.Weight);
            paramList[6] = this.param("@DateTimeRecived", SqlDbType.DateTime, this.DateTimeRecived);
            paramList[7] = this.param("@StatusC", SqlDbType.Int, this.Status);
            paramList[8] = this.param("@RemarkC", SqlDbType.Text, this.Remark);
            paramList[9] = this.param("@LastModifiedBy", SqlDbType.UniqueIdentifier, this.LastModifiedBy);
            //DRIVER INFORMATION PARAMETERS
            paramList[10] = this.param("@DriverName", SqlDbType.NVarChar, objDriver.DriverName, 50);
            paramList[11] = this.param("@LicenseNumber", SqlDbType.NVarChar, objDriver.LicenseNumber, 50);
            paramList[12] = this.param("@PlateNumber", SqlDbType.NVarChar, objDriver.PlateNumber, 50);
            paramList[13] = this.param("@LicenseIssuedPlace", SqlDbType.NVarChar, objDriver.LicenseIssuedPlace, 50);
            paramList[14] = this.param("@TrailerPlateNumber", SqlDbType.NVarChar, objDriver.TrailerPlateNumber, 50);
            paramList[15] = this.param("@RemarkD", SqlDbType.Text, objDriver.Remark);
            paramList[16] = this.param("@StatusD", SqlDbType.Int, objDriver.Status);//????????????????
            //VOUCHER INFORMATION PARAMETERS
            paramList[17] = this.param("@VoucherNo", SqlDbType.NVarChar, objVoucher.VoucherNo, 50);
            paramList[18] = this.param("@CoffeeTypeId", SqlDbType.UniqueIdentifier, objVoucher.CoffeeTypeId);
            paramList[19] = this.param("@SpecificArea", SqlDbType.NVarChar, objVoucher.SpecificArea);
            paramList[20] = this.param("@NumberOfBags", SqlDbType.Int, objVoucher.NumberofBags);
            paramList[21] = this.param("@NumberOfPlomps", SqlDbType.Int, objVoucher.NumberOfPlomps);
            paramList[22] = this.param("@NumberOfPlompsTrailer", SqlDbType.Int, objVoucher.NumberOfPlompsTrailer);
            DataAccessPoint dal = new DataAccessPoint();
            try
            {
                dal.ExcuteProcedure("spUpdateArrival", paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update arrival! " + ex.Message.ToString());
            }
        }
        /// <summary>
        /// Create new parameter with supplied Parameter-Name, SqlDbType and Value intended with
        /// the parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns>
        /// Returns New parameter with name and value
        /// </returns>
        private SqlParameter param(string paramName, SqlDbType dbType, object paramValue)
        {
            SqlParameter param = new SqlParameter(paramName, dbType);
            param.Value = paramValue;
            return param;
        }
        /// <summary>
        /// Create new parameter with supplied Parameter-Name, SqlDbType, Value intended with
        /// the parameter and the size if the parameter has string data type
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns>
        /// Returns New parameter with name, size, value
        /// </returns>
        private SqlParameter param(string paramName, SqlDbType dbType, object paramValue, int size)
        {
            SqlParameter param = new SqlParameter(paramName, dbType, size);
            param.Value = paramValue;
            return param;
        }
        /// <summary>
        /// Declare and Initialize SQL Scalars for CommodityDepositRequest, DriverInformation and VoucherInformation 
        /// Fields so that it would be sent to DAL to execute the procedure
        /// </summary>
        /// <param name="comodityObj"></param>
        /// <param name="objDriver"></param>
        /// <param name="objVoucher"></param>
        /// <returns>
        /// sql parameter array
        /// </returns>
        private SqlParameter[] ParamList(CommodityDepositeRequestBLL objCommodityDepositRequest, DriverInformationBLL objDriver, VoucherInformationBLL objVoucher)
        {
            SqlParameter[] arPar = new SqlParameter[29];
            //use arPar [] to hold all 3 tables parameter to insert
            //COMMODITY-DEPOSIT-REQUEST PARAMETERSS
            arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[0].Value = objCommodityDepositRequest.TransactionId;
            arPar[1] = new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = objCommodityDepositRequest.ClientId;
            arPar[2] = new SqlParameter("@CommodityId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = objCommodityDepositRequest.CommodityId; ;
            arPar[3] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[3].Value = objCommodityDepositRequest.WarehouseId;
            arPar[4] = new SqlParameter("@RepresentativeId", SqlDbType.UniqueIdentifier);
            arPar[4].Value = objCommodityDepositRequest.RepresentativeId;
            arPar[5] = new SqlParameter("@WoredaId", SqlDbType.UniqueIdentifier);
            if (objCommodityDepositRequest.WoredaId != Guid.Empty)
            {
                arPar[5].Value = objCommodityDepositRequest.WoredaId;
            }
            else
            {
                arPar[5].Value = null;
            }

            arPar[6] = new SqlParameter("@productionYear", SqlDbType.Int);
            if (objCommodityDepositRequest.ProductionYear != 0)
            {
                arPar[6].Value = objCommodityDepositRequest.ProductionYear;
            }
            else
            {
                arPar[6].Value = null;
            }
            arPar[7] = new SqlParameter("@StatusC", SqlDbType.Int);
            arPar[7].Value = objCommodityDepositRequest.Status;
            arPar[8] = new SqlParameter("@CreatedByC", SqlDbType.UniqueIdentifier);
            arPar[8].Value = objCommodityDepositRequest.CreatedBy;
            arPar[9] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[9].Value = objCommodityDepositRequest.Remark;
            arPar[10] = new SqlParameter("@Weight", SqlDbType.Decimal);
            arPar[10].Value = objCommodityDepositRequest.Weight;
            arPar[11] = new SqlParameter("@NumberofBags", SqlDbType.Int);
            arPar[11].Value = objCommodityDepositRequest.NumberofBags;
            arPar[12] = new SqlParameter("@DateTimeRecived", SqlDbType.DateTime);
            arPar[12].Value = objCommodityDepositRequest.DateTimeRecived;

            arPar[13] = new SqlParameter("@CommodityDepositRequestId", SqlDbType.UniqueIdentifier);
            arPar[13].Value = this.Id;

            //DRIVER PARAMETERS
            //arPar[14] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
            //arPar[14].Value = objDriver.ReceivigRequestId;
            arPar[14] = new SqlParameter("@DriverName", SqlDbType.NVarChar, 50);
            arPar[14].Value = objDriver.DriverName;
            arPar[15] = new SqlParameter("@LicenseNumber", SqlDbType.NVarChar, 50);
            arPar[15].Value = objDriver.LicenseNumber;
            arPar[16] = new SqlParameter("@LicenseIssuedPlace", SqlDbType.NVarChar, 50);
            arPar[16].Value = objDriver.LicenseIssuedPlace;
            arPar[17] = new SqlParameter("@PlateNumber", SqlDbType.NVarChar, 50);
            arPar[17].Value = objDriver.PlateNumber;
            arPar[18] = new SqlParameter("TrailerPlateNumber", SqlDbType.NVarChar, 50);
            arPar[18].Value = objDriver.TrailerPlateNumber;
            //arPar[20] = new SqlParameter("@CreatedByD", SqlDbType.UniqueIdentifier);
            //arPar[20].Value = objDriver.CreatedBy;
            arPar[19] = new SqlParameter("@StatusD", SqlDbType.Int);
            arPar[19].Value = 1;
            //arPar[22] = new SqlParameter("@DriverInformationId", SqlDbType.UniqueIdentifier);
            //arPar[22].Direction = ParameterDirection.Output;

            //VOUCHER PARAMETERS
            //arPar[23] = new SqlParameter("@DepositRequestId", SqlDbType.UniqueIdentifier);
            //arPar[23].Value = objVoucher.DepositRequestId;
            arPar[20] = new SqlParameter("@VoucherNo", SqlDbType.NVarChar, 50);
            arPar[20].Value = objVoucher.VoucherNo;
            arPar[21] = new SqlParameter("@CoffeeTypeId", SqlDbType.UniqueIdentifier);
            arPar[21].Value = objVoucher.CoffeeTypeId;
            arPar[22] = new SqlParameter("@SpecificArea", SqlDbType.NVarChar);
            arPar[22].Value = objVoucher.SpecificArea;
            //arPar[27] = new SqlParameter("@NumberOfBagsV", SqlDbType.Int);
            //arPar[27].Value = objVoucher.NumberofBags;
            arPar[23] = new SqlParameter("@NumberOfPlomps", SqlDbType.Int);
            arPar[23].Value = objVoucher.NumberOfPlomps;
            arPar[24] = new SqlParameter("@NumberOfPlompsTrailer", SqlDbType.Int);
            arPar[24].Value = objVoucher.NumberOfPlompsTrailer;
            arPar[25] = new SqlParameter("@CertificateNo", SqlDbType.NVarChar, 50);
            arPar[25].Value = "";// objVoucher.CertificateNo;
            //arPar[31] = new SqlParameter("@CreatedByV", SqlDbType.UniqueIdentifier);
            //arPar[31].Value = objVoucher.CreatedBy;
            arPar[26] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
            arPar[26].Value = DateTime.Now;
            arPar[27] = new SqlParameter("@StatusV", SqlDbType.Int);
            arPar[27].Value = objVoucher.Status;
            //????????
            arPar[28] = new SqlParameter("@VoucherId", SqlDbType.UniqueIdentifier);
            arPar[28].Direction = ParameterDirection.Output;
            return arPar;
        }
    }
}
