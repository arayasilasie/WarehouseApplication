using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseApplication.ECXLookUp;
using ECX.DataAccess;
using GINBussiness;
using System.Data;

namespace WarehouseApplication.BLL
{
    public class WarehouseRecieptBLL : GeneralBLL
    {
        #region private fileds
        private Guid _GRNId;
        private string _GRNNumber;
        private Guid _CommodityGradeId;
        private Guid _WarehouseId;
        private Guid _ShedId;
        private Guid _BagTypeId;
        private Guid _VoucherId;
        private Guid _UnloadingId;
        private Guid _ScalingId;
        private Guid _GradingId;
        private Guid _SampleTicketId;
        private DateTime _DateDeposited;
        private DateTime _DateApproved;
        private int _GRN_Status;
        private Guid _WRStatusId;
        private Double _GrossWeight;
        private Double _NetWeight;
        private Double _OriginalQuantity;
        private Double _CurrentQuantity;
        private Guid _DepositTypeId;
        private byte _Source;
        private Double _NetWeightAdjusted;
        private Guid _ClientId;
        private int _NoBags;
        private Guid _GRNType;
        private int _ProductionYear;

        public int ProductionYear
        {
            get { return _ProductionYear; }
            set { _ProductionYear = value; }
        }
        #endregion

        #region Properties




        public Guid GRNId
        {
            get { return _GRNId; }
            set { _GRNId = value; }
        }
        public string GRNNumber
        {
            get { return _GRNNumber; }
            set { _GRNNumber = value; }
        }
        public Guid CommodityGradeId
        {
            get { return _CommodityGradeId; }
            set { _CommodityGradeId = value; }
        }
        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }
        public Guid ShedId
        {
            get { return _ShedId; }
            set { _ShedId = value; }
        }
        public Guid BagTypeId
        {
            get { return _BagTypeId; }
            set { _BagTypeId = value; }
        }
        public Guid VoucherId
        {
            get { return _VoucherId; }
            set { _VoucherId = value; }
        }
        public Guid UnloadingId
        {
            get { return _UnloadingId; }
            set { _UnloadingId = value; }
        }
        public Guid ScalingId
        {
            get { return _ScalingId; }
            set { _ScalingId = value; }
        }
        public Guid GradingId
        {
            get { return _GradingId; }
            set { _GradingId = value; }
        }
        public Guid SampleTicketId
        {
            get { return _SampleTicketId; }
            set { _SampleTicketId = value; }
        }
        public DateTime DateDeposited
        {
            get { return _DateDeposited; }
            set { _DateDeposited = value; }
        }
        public DateTime DateApproved
        {
            get { return _DateApproved; }
            set { _DateApproved = value; }
        }
        public int GRN_Status
        {
            get { return _GRN_Status; }
            set { _GRN_Status = value; }
        }
        public Guid WRStatusId
        {
            get { return _WRStatusId; }
            set { _WRStatusId = value; }
        }
        public Double GrossWeight
        {
            get { return _GrossWeight; }
            set { _GrossWeight = value; }
        }
        public Double NetWeight
        {
            get { return _NetWeight; }
            set { _NetWeight = value; }
        }
        public Double OriginalQuantity
        {
            get { return _OriginalQuantity; }
            set { _OriginalQuantity = value; }
        }
        public Double CurrentQuantity
        {
            get { return _CurrentQuantity; }
            set { _CurrentQuantity = value; }
        }
        public Guid DepositTypeId
        {
            get { return _DepositTypeId; }
            set { _DepositTypeId = value; }
        }
        public byte Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        public Double NetWeightAdjusted
        {
            get { return _NetWeightAdjusted; }
            set { _NetWeightAdjusted = value; }
        }
        public Guid ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int NoBags
        {
            get { return _NoBags; }
            set { _NoBags = value; }
        }
        public Guid GRNType
        {
            get { return _GRNType; }
            set { _GRNType = value; }
        }
        public int ConsignmentType { get; set; }
        public decimal RawValue { get; set; }
        public decimal CupValue { get; set; }
        public decimal TotalValue { get; set; }
        public Guid Woreda { get; set; }
        public Boolean IsTracable { get; set; }
        public string Shade { get; set; }
        public string ProcessingCenter { get; set; }
        public string CarPlateNumber { get; set; }
        public string TrailerPlateNumber { get; set; }
        public string ArrivalCert { get; set; }


        //ConsignmentType, RawValue, CupValue, Woreda

        #endregion


        public WarehouseRecieptBLL()
        {
        }

        public WarehouseRecieptBLL(GRNBLL obj)
        {
            this.GRNId = obj.Id;
            this.GRNNumber = obj.GRN_Number;
            this.CommodityGradeId = obj.CommodityGradeId;
            this.WarehouseId = obj.WarehouseId;
            this.BagTypeId = obj.BagTypeId;
            //ToDO Remove 
            this.BagTypeId = new Guid();
            this.VoucherId = obj.VoucherId;
            this.UnloadingId = obj.UnLoadingId;
            this.ScalingId = obj.ScalingId;
            this.GradingId = obj.GradingId;
            this.SampleTicketId = obj.SamplingTicketId;
            this.DateDeposited = obj.DateDeposited;
            this.DateApproved = obj.ApprovedTimeStamp;
            this.GRN_Status = obj.Status;
            //TODO - Get New

            this.GrossWeight = obj.GrossWeight;
            this.NetWeight = obj.NetWeight;
            this.OriginalQuantity = obj.OriginalQuantity;
            this.CurrentQuantity = obj.CurrentQuantity;
            //TODO - Determine 
            this.DepositTypeId = new Guid();
            this.Source = 1;
            this.NetWeightAdjusted = this.NetWeight;
            this.CreatedBy = UserBLL.GetCurrentUser();
            this.CreatedTimestamp = DateTime.Now;
            //To Remove 
            this.LastModifiedBy = UserBLL.GetCurrentUser();
            this.LastModifiedTimestamp = DateTime.Now;
            this.ClientId = obj.ClientId;
            this.NoBags = obj.TotalNumberOfBags;
            this.GRNType = obj.GRNTypeId;
            this.ProductionYear = obj.ProductionYear;
            this.ClientId = obj.ClientId;

            this.BagTypeId = obj.BagTypeId;
            this.ConsignmentType = obj.ConsignmentType;
            this.RawValue=obj.RawValue;
            this.CupValue=obj.CupValue;
            if (obj.TotalValue <= 100)
            {
                this.TotalValue = obj.TotalValue;
            }
            else//>100 for speciallity coffee
            {
                this.TotalValue = obj.CupValue;
            }
            this.Woreda=obj.Woreda;
            this.Shade = obj.Shade;
            this.IsTracable = obj.IsTracable;
            this.CarPlateNumber = obj.TruckPlateNumber;
            this.TrailerPlateNumber = obj.TrailerPlateNumber;
            this.ProcessingCenter = obj.ProcessingCenter;
            this.ArrivalCert = obj.ArrivalCert;


        }

        public bool Save()
        {
            bool isSaved = false;
            try
            {
                if (this.CommodityGradeId == Guid.Empty)
                {
                    throw new Exception("Commodity Grade Can't be empty");
                }
                WarehouseApplication.ECXCD.WR mywarehouse = new WarehouseApplication.ECXCD.WR();
                isSaved = mywarehouse.SaveWareHouseReciept(this.GRNId, this.GRNNumber, this.CommodityGradeId, this.WarehouseId, this.BagTypeId,
                    this.VoucherId, this.UnloadingId, this.ScalingId, this.GradingId, this.SampleTicketId, this.DateDeposited, this.DateApproved, this.GRN_Status, this.GrossWeight,
                    (float)((Math.Round(this.NetWeight * 10000)) / 10000), (float)(Math.Round(this.OriginalQuantity * 10000, 0)) / 10000, (float)(Math.Round(this.CurrentQuantity * 10000, 0)) / 10000, DepositTypeId, Source, NetWeightAdjusted, this.CreatedBy, this.CreatedTimestamp,
                       this.ClientId, this.NoBags, this.ProductionYear, this.GRNType);


            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Create Warehouse Reciept", ex);
            }
            return isSaved;
        }

        public void EditWHRtoWarehouse(string GRNNo, string Shed)
        {
             WarehouseBaseModel con = new WarehouseBaseModel();
             SQLHelper.execNonQuery(con.CDConnectionString, "EditWHRtoWarehouse", GRNNo, Shed);
        }


        public static Guid EnglishGuid
        {
            get
            {
                return new Guid("9ad72f55-bc00-4382-873e-0c84d6eb3850");
            }
        }

        public void Save2()
        {
            //drived from CD SaveWareHouseReciept
            double WeightBeforeMoisture = NetWeight;
            double lossAmount = 0;
            ECXLookup ecxlookup = new ECXLookup();
            CCommodityGrade CG = ecxlookup.GetCommodityGrade(EnglishGuid, CommodityGradeId);
            CCommodityClass CC = ecxlookup.GetCommodityClass(EnglishGuid, CG.CommodityClassUniqueIdentifier);
            bool DeductMLA = false;

            //sesame
            if (CC.CommodityUniqueIdentifier.ToString().ToLower() == "d97fd8c1-8916-4e3d-89e2-bd50d556a32f".ToLower())
            {
                //TODO:GET FROM WEBSERVICE 
                lossAmount = 0.0015;
                DeductMLA = true;

            }//coffee
            else if (CC.CommodityUniqueIdentifier.ToString().ToLower() == "71604275-df23-4449-9dae-36501b14cc3b".ToLower())
            {
                DeductMLA = true;
                lossAmount = 0.001376;
            }
            else
            {
                lossAmount = 0;
                DeductMLA = false;
            }

            NetWeight = (float)((Math.Round(this.NetWeight * 10000)) / 10000);
            double netWeightLoss = 0;

            if (DeductMLA)
            {
                netWeightLoss = NetWeight - (lossAmount * NetWeight);
            }
            else
            {
                netWeightLoss = NetWeight;
            }

            NetWeight = netWeightLoss;


            OriginalQuantity = (float)(Math.Round(OriginalQuantity * 10000, 0)) / 10000;
            CurrentQuantity = (float)(Math.Round(CurrentQuantity * 10000, 0)) / 10000;
            WeightBeforeMoisture = (float)(Math.Round(WeightBeforeMoisture * 10000, 0)) / 10000;
            GrossWeight = (float)(Math.Round(GrossWeight * 10000, 0)) / 10000;

            OriginalQuantity = Math.Round(OriginalQuantity, 4);
            CurrentQuantity = Math.Round(CurrentQuantity, 4);

            //string WRStatus="New",Source="Manual";

            int WRStatus = 1, Source = 1;
            WarehouseBaseModel con = new WarehouseBaseModel();
            SQLHelper.execNonQuery(con.CDConnectionString, "tblWarehouseReciept_Insert2",
               GRNId, GRNNumber, CommodityGradeId, WarehouseId, BagTypeId,
                   VoucherId, UnloadingId, ScalingId, GradingId, SampleTicketId, DateDeposited, GRN_Status, WRStatus, GrossWeight,
                   NetWeight, OriginalQuantity, CurrentQuantity,
                   DepositTypeId, Source, NetWeightAdjusted, CreatedBy, CreatedTimestamp, ClientId, NoBags, ProductionYear, GRNType,
                   WeightBeforeMoisture, ConsignmentType, RawValue, CupValue, TotalValue, Woreda, Shade, IsTracable, CarPlateNumber, 
                   TrailerPlateNumber, ProcessingCenter,ArrivalCert);

        }

        public DataTable getexpiredWHRsOnTruck(Guid WarehouseID, int Status, string GRNNo)
        {
            WarehouseBaseModel con = new WarehouseBaseModel();
            return SQLHelper.getDataTable(con.CDConnectionString, "getexpiredWHRsOnTruck", WarehouseID, Status, GRNNo);
        }

        public DataTable getexpiredWHRsOnTruck(Guid WarehouseID)
        {
            WarehouseBaseModel con = new WarehouseBaseModel();
            return SQLHelper.getDataTable(con.CDConnectionString, "getexpiredWHRsOnTruck", WarehouseID);
        }

        public static bool RequestCancel(Guid GRNId, string TrackingNo, string Remark)
        {

            WarehouseApplication.ECXCD.WR o = new WarehouseApplication.ECXCD.WR();
            return o.RequestWHRCancel(GRNId, UserBLL.GetCurrentUser(), Remark, TrackingNo);

        }

        //public bool Cancel(Guid Id)
        //{
        //    bool isSaved = true;
        //    try
        //    {

        //        WarehouseApplication.ECXCD.WR mywarehouse = new WarehouseApplication.ECXCD.WR();
        //        isSaved = mywarehouse.CancelWarehouseReciept(Id, UserBLL.GetCurrentUser());


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to Create Warehouse Reciept", ex);
        //    }
        //    return true;
        //}
    }
}
