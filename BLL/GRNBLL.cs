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
using System.Data.SqlClient;
using WarehouseApplication.DAL;
using WarehouseApplication.GINLogic;

namespace WarehouseApplication.BLL
{
    public enum GRNStatus { New = 1, Active, Cancelled, ClientAccepted, ClientRejected, ManagerApproved, OpenForEdit }
    public class GRNBLL : GeneralBLL
    {
        #region Fields & Properties

        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _GRN_Number;
        public string GRN_Number
        {
            get { return _GRN_Number; }
            set { _GRN_Number = value; }
        }

        private Guid _CommodityId;
        public Guid CommodityId
        {
            get { return _CommodityId; }
            set { _CommodityId = value; }
        }

        private Guid _CommodityClassId;
        public Guid CommodityClassId
        {
            get { return _CommodityClassId; }
            set { _CommodityClassId = value; }
        }

        private Guid _CommodityGradeId;
        public Guid CommodityGradeId
        {
            get { return _CommodityGradeId; }
            set { _CommodityGradeId = value; }
        }

        private Guid _CommodityRecivingId;
        public Guid CommodityRecivingId
        {
            get { return _CommodityRecivingId; }
            set { _CommodityRecivingId = value; }
        }

        private Guid _WarehouseId;
        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }

        private Guid _BagTypeId;
        public Guid BagTypeId
        {
            get { return _BagTypeId; }
            set { _BagTypeId = value; }
        }

        private Guid _VoucherId;
        public Guid VoucherId
        {
            get { return _VoucherId; }
            set { _VoucherId = value; }
        }


        public int ConsignmentType { get; set; }
        public decimal RawValue { get; set; }
        public decimal CupValue { get; set; }
        public decimal TotalValue { get; set; }
        public Boolean IsTracable { get; set; }
        public Guid Woreda { get; set; }
        //public Boolean IsTracable { get; set; }
        public string Shade { get; set; }
        public string ProcessingCenter { get; set; }
        public string ArrivalCert { get; set; }
        public string TruckPlateNumber { get; set; }
        public string TrailerPlateNumber { get; set; }


        private Guid _UnLoadingId;
        public Guid UnLoadingId
        {
            get { return _UnLoadingId; }
            set { _UnLoadingId = value; }
        }

        private Guid _ScalingId;
        public Guid ScalingId
        {
            get { return _ScalingId; }
            set { _ScalingId = value; }
        }

        private Guid _GradingId;
        public Guid GradingId
        {
            get { return _GradingId; }
            set { _GradingId = value; }
        }

        private Guid _SamplingTicketId;
        public Guid SamplingTicketId
        {
            get { return _SamplingTicketId; }
            set { _SamplingTicketId = value; }
        }

        private Guid _GRNTypeId;
        public Guid GRNTypeId
        {
            get { return _GRNTypeId; }
            set { _GRNTypeId = value; }
        }

        private DateTime _DateDeposited;
        public DateTime DateDeposited
        {
            get { return _DateDeposited; }
            set { _DateDeposited = value; }
        }

        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private int _TotalNumberOfBags;
        public int TotalNumberOfBags
        {
            get { return _TotalNumberOfBags; }
            set { _TotalNumberOfBags = value; }
        }



        private float _GrossWeight;
        public float GrossWeight
        {
            get { return _GrossWeight; }
            set { _GrossWeight = value; }
        }

        private float _NetWeight;
        public float NetWeight
        {
            get { return _NetWeight; }
            set { _NetWeight = value; }
        }

        private float _OriginalQuantity;
        public float OriginalQuantity
        {
            get { return _OriginalQuantity; }
            set { _OriginalQuantity = value; }
        }

        private float _CurrentQuantity;
        public float CurrentQuantity
        {
            get { return _CurrentQuantity; }
            set { _CurrentQuantity = value; }
        }
        private string _GradingCode;

        public string GradingCode
        {
            get { return _GradingCode; }
            set { _GradingCode = value; }
        }
        private Guid _ClientId;
        public Guid ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        private String _Remark;

        public String Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private int _productionYear;

        public int ProductionYear
        {
            get { return _productionYear; }
            set { _productionYear = value; }
        }
        private bool _ClientAccepted;

        public bool ClientAccepted
        {
            get { return _ClientAccepted; }
            set { _ClientAccepted = value; }
        }
        private DateTime _ClientAcceptedTimeStamp;

        public DateTime ClientAcceptedTimeStamp
        {
            get { return _ClientAcceptedTimeStamp; }
            set { _ClientAcceptedTimeStamp = value; }
        }

        private DateTime _ApprovedTimeStamp;

        public DateTime ApprovedTimeStamp
        {
            get { return _ApprovedTimeStamp; }
            set { _ApprovedTimeStamp = value; }
        }
        private Guid _ApprovedBy;

        public Guid ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }

        public DateTime GRNCreatedDate { get; set; }
        public DateTime ManagerApprovedDateTime { get; set; }
   


        public List<GRNBLL> GetPendingGRN(Guid Warehouseid)
        {
            if (this.WarehouseId == null)
            {
                throw new Exception("Invalid WarehouseId Exception");

            }
            try
            {
                List<GRNBLL> list = new List<GRNBLL>();
                list = GRNDAL.GetGRNPendingCreation(Warehouseid);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public List<GRNBLL> GetPendingGRNByTrackingNo(Guid Warehouseid, string TrackingNo)
        {
            if (this.WarehouseId == null)
            {
                throw new Exception("Invalid WarehouseId Exception");

            }
            try
            {
                List<GRNBLL> list = new List<GRNBLL>();
                list = GRNDAL.GetGRNPendingCreationByTrackingNoByTrackingNo(Warehouseid, TrackingNo);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        #endregion

        /// <summary>
        /// Gets the net weight by deducting the tare 
        /// </summary>
        /// <param name="GrossWeight"></param>
        /// <param name="BagTypeId"></param>
        /// <param name="NoBags"></param>
        /// <returns></returns>
        /// 
        public Nullable<Guid> Add(List<GRNServiceBLL> listGRNService)
        {
            bool IsSaved = false;
            bool canCreate = false; ;
            try
            {
                canCreate = this.CanCreateGRNforGradingId(this.GradingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (canCreate == false)
            {
                return null;
            }

            Nullable<Guid> id = null;
            SqlTransaction tran;
            string warehousecode;
            warehousecode = WarehouseBLL.GetWarehouseCode(this.WarehouseId);
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            try
            {
                id = GRNDAL.InsertNewGRN(this, warehousecode, tran);

                if (id != null)
                {


                    if (listGRNService != null)
                    {
                        GRNServiceBLL objSer = new GRNServiceBLL();
                        IsSaved = objSer.Save((Guid)id, listGRNService, tran);
                        //Audit Trail Both GRN And GRN Service.
                        int at = -1;
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        this.Id = (Guid)id;
                        at = objAt.saveAuditTrail(this, WFStepsName.AddGRN.ToString(), UserBLL.GetCurrentUser(), "Add New GRN");
                        if (at == 1)
                        {
                            IsSaved = true;
                        }
                        else
                        {
                            IsSaved = false;
                        }

                    }
                    else
                    {
                        IsSaved = true;
                    }

                    if (IsSaved == true)
                    {
                       
                        WFTransaction.WorkFlowManager(this.TrackingNo);
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Rollback();
                }


            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Unable to update database.", ex);
            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }
            return id;


        }
        public bool CanCreateGRNforGradingId(Guid GradingId)
        {
            int count = -1;
            count = GRNDAL.GetCountByGradingId(GradingId);
            if (count == -1)
            {
                throw new IndeterminateGRNCountException("Can Not determine if a GRN is Created for this Code.");

            }
            else if (count > 0)
            {
                throw new MultipleGRNForSingleGradingCodeException();

            }
            else
            {
                return true;
            }
        }
        public Nullable<float> CalculateNetWeight(float GrossWeight, Guid BagTypeId, int NoBags)
        {
            float Tare, netWeight;
            BagTypeBLL objbagType = new BagTypeBLL();
            objbagType.GetBagTypeById(BagTypeId);

            if (objbagType != null)
            {
                Tare = objbagType.Tare;
                if (Tare == -1)
                {
                    throw new InvalidTareException("Tare for the bag type is not provided");

                }
                netWeight = GrossWeight - (Tare * NoBags);
                if (netWeight <= 0)
                {
                    throw new Exception("Net weight can not be below 0.");

                }
                netWeight = (float)(Math.Round(netWeight * 100) / 100);
                return (float) (Math.Round(netWeight * 100,0))/100;
            }
            else
            {
                throw new InvalidTareException("Tare for the bag type is not provided.");
                
            }

        }
        public Nullable<float> CalculateNetWeight(float GrossWeight, Guid BagTypeId, int NoBags, Guid CommodityGradeId)
        {
          
            //Get Commodity Id 
            Guid CommodityID = CommodityGradeBLL.GetCommodityGrade(CommodityGradeId).CommodityId;
            Guid CoffeeId  =  new Guid(Utility.GetCommodityId("Coffee").ToString());
            

            float Tare, netWeight;
            BagTypeBLL objbagType = new BagTypeBLL();
            objbagType.GetBagTypeById(BagTypeId);

            if (objbagType != null)
            {
                Tare = objbagType.Tare;
                if (Tare == -1)
                {
                    throw new InvalidTareException("Tare for the bag type is not provided");

                }
             // Utility.LogException(new Exception("CC : CoffeeId=" + CoffeeId.ToString() + ", CommodityID=" + CommodityID.ToString()));
                if (CoffeeId != CommodityID)
                {
                    Tare = Tare / 100; // change to quintals
                }
                netWeight = GrossWeight - (Tare * NoBags);
               // Utility.LogException(new Exception("Tare:" + Tare.ToString() + ", NoBags=" + NoBags + ", GrossWeight=" + GrossWeight.ToString() + ", netWeight=" + netWeight.ToString()));

                if (netWeight <= 0)
                {
                    throw new Exception("Net weight can not be below 0.");

                }
                netWeight = (float)(Math.Round(netWeight * 100) / 100);
                return (float)(Math.Round(netWeight * 100, 0)) / 100;
            }
            else
            {
                throw new InvalidTareException("Tare for the bag type is not provided.");

            }

        }
        /// <summary>
        /// Returns the qunatity in lot size.
        /// </summary>
        /// <param name="NetWeight">Weight after Tare is deducted</param>
        /// <param name="CommodityGrade">To Get the one lot size</param>
        /// <returns></returns>
        public  static float CalculateGRNQuantity(float NetWeight, int NoBags, Guid CommodityGradeId)
        {
           
            float tolerance = -1;
            Nullable<float> lotSizeKg = null;
            int OneLotNumberBags = -1;
            float GRNQty = 0;
            // get Lot size for the qty.
            Guid CoffeeId = new Guid(Utility.GetCommodityId("Coffee").ToString());
            Guid CommodityID = CommodityGradeBLL.GetCommodityGrade(CommodityGradeId).CommodityId;
            string t = CommodityGradeBLL.GetCommodityGrade(CommodityGradeId).Commodity;
            if (CoffeeId != CommodityID)
            {
                NetWeight = NetWeight * 100;
            }


            lotSizeKg = CommodityGradeBLL.GetCommodityGradeLotSizeById(CommodityGradeId);
            OneLotNumberBags = CommodityGradeBLL.GetCommodityGradeLotSizeInBagsById(CommodityGradeId);
            tolerance = Utility.WeightTolerance();
            if (lotSizeKg == null)
            {
                throw new InvalidLotSizeException("Invalid lot Size.");
            }
            else
            {
                GRNQty = (float)ECXLotCalclulate((float)NetWeight, NoBags, OneLotNumberBags, (float)lotSizeKg, tolerance);
                GRNQty = (float)(Math.Round(GRNQty * 10000) / 10000);
            }

            return GRNQty;
        }
        private void CalculateUpandDown(float NetWeight, float lotSize, float weightTolerance)
        {



        }
        //private Decimal ECXLotCalclulate(float NetWeight, float lotSize)
        //{
        //    Decimal qty;           
        //    // check is within 2% range.
        //    string temp, fullqty, decimalqty;
        //    string[] tempArr;
        //    temp = (NetWeight / lotSize).ToString();
        //    // get the 
        //    tempArr = temp.Split('.');
        //    if (tempArr.Length == 2)
        //    {
        //        fullqty = tempArr[0];
        //        decimalqty = tempArr[1];
        //        if (decimalqty.Length < 5)
        //        {
        //            decimalqty = decimalqty;
        //        }
        //        else
        //        {
        //            string fifth;
        //            fifth = decimalqty.Substring(4, 1);
        //            if (int.Parse(fifth) <= 0)
        //            {
        //                decimalqty = decimalqty;
        //            }
        //            else
        //            {
        //                decimalqty = (float.Parse("0." + decimalqty) + 0.0001F).ToString();
        //                decimalqty = decimalqty.Substring(0, 6);
        //            }
        //        }
        //        qty = Decimal.Parse(fullqty) + Decimal.Parse(decimalqty);


        //    }
        //    else
        //    {
        //        qty = Decimal.Parse(temp); 
        //    }
        //    return qty;
        //}
        /// Depricated.
        //public Decimal ECXLotCalclulate(double NetWeight, float onelots)
        //{
        //    double lotSize = 0;
        //    double tolerancepercent = .04;
        //    double tempLot = NetWeight / onelots;
        //    double upperLot = Math.Ceiling(tempLot);
        //    double LowerLot = Math.Floor(tempLot);
        //    if (NetWeight > onelots)
        //    {
        //        if (LowerLot == upperLot)
        //        {
        //            lotSize = LowerLot;
        //        }
        //        else
        //        {
        //            lotSize = LowerLot;
        //            double minLotWeightAdjusted = (onelots * LowerLot) + ((LowerLot * onelots) * tolerancepercent);
        //            double GRVWeight = NetWeight - minLotWeightAdjusted;
        //            if (GRVWeight > 0)
        //            {
        //                double GRVLot = GRVWeight / onelots;
        //                lotSize = lotSize + GRVLot;
        //                lotSize = Math.Round(lotSize * 100000) / 100000;
        //            }
        //            else
        //            {
        //                lotSize = LowerLot;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        double minLotWeightAdjusted = (onelots * 1) - ((1 * onelots) * tolerancepercent);
        //        if (NetWeight < minLotWeightAdjusted)
        //        {
        //            double GRVLot = NetWeight / onelots;
        //            lotSize = lotSize + GRVLot;
        //            lotSize = Math.Round(lotSize * 100000) / 100000;
        //        }
        //        else
        //        {
        //            lotSize = 1;
        //        }

        //    }
        //    lotSize = lotSize * 10000;
        //    lotSize = Math.Ceiling(lotSize) / 10000;
        //    return Decimal.Parse((lotSize.ToString()));


        //}
        public static Decimal ECXLotCalclulate(double NetWeight, int NumberOfBags, int onelotNumberOfbags, float oneLotSizeInKg, float tolerance)
        {
            float lotsizeKgForOptimalLot, MinLotSizeAdjusted, MaxLotSizeAdjusted;
            if (NumberOfBags < onelotNumberOfbags)
            {
                //check if the lot size is within tolerance limit.
                lotsizeKgForOptimalLot = 1 * oneLotSizeInKg;
                MinLotSizeAdjusted = lotsizeKgForOptimalLot - (tolerance * lotsizeKgForOptimalLot);
                MaxLotSizeAdjusted = lotsizeKgForOptimalLot + (tolerance * lotsizeKgForOptimalLot);
                if (lotsizeKgForOptimalLot == NetWeight)
                {
                    return (Decimal)1;
                }
                else if (NetWeight >= MinLotSizeAdjusted && NetWeight <= MaxLotSizeAdjusted)// within
                {
                    return (Decimal)1;
                }
                else
                {
                    return (Decimal)((Double)NetWeight / (Double)oneLotSizeInKg);
                }
            }
            float optimallotSize;
            if (oneLotSizeInKg == 5000)
            {
                onelotNumberOfbags = 50;
            }
            optimallotSize = (float)Math.Round(((decimal)NumberOfBags / (decimal)onelotNumberOfbags), 0);
            lotsizeKgForOptimalLot = optimallotSize * oneLotSizeInKg;
            MinLotSizeAdjusted = lotsizeKgForOptimalLot - (tolerance * lotsizeKgForOptimalLot);
            MaxLotSizeAdjusted = lotsizeKgForOptimalLot + (tolerance * lotsizeKgForOptimalLot);




            if (lotsizeKgForOptimalLot == NetWeight)
            {
                return (Decimal)optimallotSize;
            }
            else if (NetWeight >= MinLotSizeAdjusted && NetWeight <= MaxLotSizeAdjusted)// within
            {
                return (Decimal)optimallotSize;
            }
            else
            {
                optimallotSize = (float)(NetWeight / oneLotSizeInKg);
                optimallotSize = (float)(Math.Round(optimallotSize * 10000) / 10000);
                return (Decimal)optimallotSize;

            }
        }
        public GRNBLL GetbyGRN_Number(Guid GRN_number)
        {
            GRNBLL obj = new GRNBLL();

            try
            {
                obj = GRNDAL.GetGRNbyGRN_Number(GRN_number);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ClientAcceptance(string GRN_number, DateTime AccptedTimeStamp, bool CAStatus, string TrackingNo, GRNStatus prevStatus)
        {
            bool isSaved = false;
            SqlTransaction trans;
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            trans = conn.BeginTransaction();
            int status;
            if (CAStatus == true)
            {
                status = 4;
            }
            else
            {
                status = 5;
            }
            try
            {
                isSaved = GRNDAL.ClientAcceptance(GRN_number, AccptedTimeStamp, CAStatus, status, trans);
                if (isSaved == true)
                {
                    int At = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    string oldVal = "(GRNNo-" + GRN_number + ") , (Status-" + prevStatus.ToString() + ")";
                    string NewVal = "(GRNNo-" + GRN_number + ") , (Status-" + Status.ToString() + ")";
                    At = objAt.saveAuditTrailStringFormat(oldVal, NewVal, WFStepName.EditGRN.ToString(), UserBLL.GetCurrentUser(), "Change GRN Status");
                    if (At == 1)
                    {
                        WFTransaction.WorkFlowManager(TrackingNo);
                    }
                    else
                    {
                        isSaved = false;
                    }
                }
                if (isSaved == true)
                {
                    trans.Commit();
                    trans.Dispose();
                    conn.Close();
                    return true;
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                trans.Dispose();
                conn.Close();
                throw ex;

            }

            return false;
        }
        public bool Update(string GRN_number, GRNStatus Status, GRNBLL objGRN, string TrackingNo, DateTime ManagerApprovedDateTime)
        {
            bool isSaved = false;
            SqlTransaction trans;
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            trans = conn.BeginTransaction();
            GRNStatus PreviousStatus;
            PreviousStatus = (GRNStatus)objGRN.Status;
            if (objGRN == null)
            {
                throw new Exception("Unable to Load GRN Data");

            }
            else
            {
                if (objGRN.Id == null || objGRN.Id == Guid.Empty)
                {
                    throw new Exception("Invalid Grading Information ");
                }
            }
            GradingBLL objGrading = new GradingBLL();
            objGrading = objGrading.GetById(objGRN.GradingId);
            objGrading.TrackingNo = TrackingNo;
            if (Status != GRNStatus.Cancelled)
            {
                if (objGrading == null)
                {
                    throw new Exception("Unable to Load GRN Data");
                }
                else
                {

                    if (objGrading.TrackingNo == "")
                    {
                        throw new Exception("Unable to Load GRN Data");
                    }
                    else
                    {
                        TrackingNo = objGrading.TrackingNo;
                    }
                }
            }

            try
            {
                if (Status == GRNStatus.ManagerApproved)
                {
                    isSaved = GRNDAL.SetGRNStatus(GRN_number, Status, trans, ManagerApprovedDateTime);
                    if (isSaved == true)
                    {
                        int At = -1;
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        string oldVal = "(GRNNo-" + GRN_number + ") , (Status-" + PreviousStatus.ToString() + ")";
                        string NewVal = "(GRNNo-" + GRN_number + ") , (Status-" + Status.ToString() + ")";
                        At = objAt.saveAuditTrailStringFormat(oldVal, NewVal, WFStepName.EditGRN.ToString(), UserBLL.GetCurrentUser(), "Change GRN Status");
                        if (At == 1)
                        {
                            WarehouseRecieptBLL objWarehouseReciept = new WarehouseRecieptBLL(objGRN);

                            if (objWarehouseReciept.Save() == true)
                            {
                                WFTransaction.WorkFlowManager(TrackingNo);
                                isSaved = true;
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
                        isSaved = false;
                    }
                }
                else if (Status == GRNStatus.Cancelled )
                {
                    //Canceling Update 
                    isSaved = false;
                    isSaved = GRNDAL.SetGRNStatus(GRN_number, Status, trans, ManagerApprovedDateTime);
                    


                    if (isSaved == true)
                    {
                        int At = -1;
                        AuditTrailBLL objAt = new AuditTrailBLL();
                        string oldVal = "(GRNNo-" + GRN_number + ") , (Status-" + PreviousStatus.ToString() + ")";
                        string NewVal = "(GRNNo-" + GRN_number + ") , (Status-" + Status.ToString() + ")";
                        At = objAt.saveAuditTrailStringFormat(oldVal, NewVal, WFStepName.EditGRN.ToString(), UserBLL.GetCurrentUser(), "Change GRN Status");
                        if (At == 1)
                        {
                            ECXWF.CMessage msg = WFTransaction.Request(TrackingNo);
                            if (msg == null)
                            {
                                throw new Exception("Unable to get WF Data.");
                            }
                            else if(msg.Name.Trim() == "EditGRN" )
                            {
                                WFTransaction.WorkFlowManager(TrackingNo,msg);
                                isSaved = true;
                            }
                        }
                    }
                }
                else
                {
                    isSaved = false;
                }
                #region Depricatedcode
                //// Cancel Implemented by web service.
                //if (Status == GRNStatus.Cancelled)
                //{
                //    WarehouseRecieptBLL objWarehouseReciept = new WarehouseRecieptBLL();
                //    isSaved = GRNDAL.UpdateGRN(GRN_number, Status, trans);
                //    if (isSaved == true)
                //    {
                //        //isSaved = objWarehouseReciept.Cancel(this.Id);
                //        if (isSaved == true)
                //        {
                //            int At = -1;
                //            AuditTrailBLL objAt = new AuditTrailBLL();
                //            string oldVal = "(GRNNo-" + GRN_number + ") , (Status-" + PreviousStatus.ToString() + ")";
                //            string NewVal = "(GRNNo-" + GRN_number + ") , (Status-" + Status.ToString() + ")";
                //            At = objAt.saveAuditTrailStringFormat(oldVal, NewVal, WFStepName.EditGRN.ToString(), UserBLL.GetCurrentUser(), "Change GRN Status");
                //            if (At == 1)
                //            {
                //                isSaved = true;
                //            }
                //            else
                //            {
                //                isSaved = false;
                //            }
                //        }

                //    }
                //    else
                //    {
                //        throw new Exception("Unable to Cancel GRN.");
                //    }
                //    if (isSaved == true)
                //    {
                //        WFTransaction.WorkFlowManager(TrackingNo);
                //    }
                //}
                #endregion
                if (isSaved == true)
                {
                    trans.Commit();
                    trans.Dispose();
                    conn.Close();
                    return true;
                }
                else
                {
                    trans.Rollback();
                    trans.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;

            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
            return false;
        }
        public GRNBLL GetbyByTrackingNo(string TrackingNo)
        {
            GRNBLL obj = new GRNBLL();

            try
            {
                obj = GRNDAL.GetGRNbyTrackingNo(TrackingNo);
                if (obj == null)
                {
                    obj = GRNDAL.GetGRNbyGRNEditTrackingNo(TrackingNo);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GRNBLL ApprovedGRNCancelGetbyByTrackingNo(string TrackingNo)
        {
            GRNBLL obj = new GRNBLL();

            try
            {
                obj = GRNDAL.GetGRNbyGRNApprovedGRNCancelByTrackingNo(TrackingNo);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool HasGRN(string tableName, Guid Id)
        {

            int count = -1;
            try
            {
                count = GRNDAL.HasGRN(tableName, Id);
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }

        }
        public bool ReCreateGRN()
        {
            bool isSaved = false;
            SqlTransaction trans;
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            trans = conn.BeginTransaction();
            string TrakingNo = "";
            TrakingNo = this.TrackingNo;
            //RequestforEditGRNBLL objRequest = null;
            this.Status = (int)GRNStatus.New;
            //objRequest = RequestforEditGRNDAL.GetApprovedRequestByGRNID(this.Id);
            //if (objRequest != null)
            //{
            //    if (string.IsNullOrEmpty(objRequest.TrackingNo) == false)
            //    {
            //        TrakingNo = objRequest.TrackingNo;
            //    }
            //    else
            //    {
            //        throw new Exception("Unable to get GRN edit request data");
            //    }

            //}
            //else
            //{

            //    throw new Exception("Unable to get GRN edit request data");
            //}
            try
            {
                isSaved = GRNDAL.ReCreate(this, trans);
                if (isSaved == true)
                {
                    WFTransaction.WorkFlowManager(TrakingNo);
                }
                if (isSaved == true)
                {
                    trans.Commit();
                    trans.Dispose();
                    conn.Close();
                    return true;
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                trans.Dispose();
                conn.Close();
                throw ex;
            }

            return false;
        }
        public String[] GetGradingResultResultCodeBylistTrackingNo(string TrackingNo)
        {
            return GRNDAL.GetParentCodeBylistTrackingNo(TrackingNo);
        }
        public String[] GetGRNNoBylistTrackingNo(string TrackingNo)
        {
            return GRNDAL.GetGRNNoBylistTrackingNoForGRN(TrackingNo);
        }
        public String[] GetGRNNoBylistEditTrackingNo(string TrackingNo)
        {
            return GRNDAL.GetGRNNoBylistEditTrackingNoForGRN(TrackingNo);
        }
        public bool IsEditableGRN(string whereClause)
        {

            return GRNDAL.IsEditableGRN(whereClause);
        }
        public static bool IsEditableGRNByTrackingNo(string TrackingNo)
        {

            return GRNDAL.IsEditableGRNByTrackingNo(TrackingNo);
        }
        public bool UpdateGRNNumber(Guid Id, String oldSystemGRNNumber, string newSystemGRNNumber, string TrackingNo)
        {
            bool isSaved = true;
            SqlTransaction trans;
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            trans = conn.BeginTransaction();
            try
            {
                isSaved = GRNDAL.UpDateGRNNumber(Id, oldSystemGRNNumber, newSystemGRNNumber, trans);
                if (isSaved == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    if (TrackingNo != "-1")
                    {

                        WFTransaction.WorkFlowManager(TrackingNo);
                    }
                    at = objAt.saveAuditTrailStringFormat("New System:GRN_Number = " + newSystemGRNNumber, "Old System :GRN_Number = " + oldSystemGRNNumber, "GRN No. Update ", UserBLL.GetCurrentUser(), WFStepName.UpdateGRNNo.ToString());
                    if (at == 1)
                    {

                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                else
                {
                    trans.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {

                        conn.Close();
                    }
                }
            }

            return isSaved;
        }
        private bool DeductBalanceForCancellingGRN(Guid UnloadingId, SqlTransaction tran )
        {
            bool isSaved = false;
            // get stacks unloaded.
            List<StackUnloadedBLL> list = null;
            StackUnloadedBLL o = new StackUnloadedBLL();
            o.UnloadingId = UnLoadingId;
            list = o.GetStackInformationByUnloadingId();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    foreach (StackUnloadedBLL i in list)
                    {
                        InventoryServices.GetInventoryService().LoadFromStack(i.StackId, i.NumberOfbags, 0, tran);
                    }
                }
            }
            else
            {
                isSaved = false;
            }
            return isSaved;
        }



    }
}
