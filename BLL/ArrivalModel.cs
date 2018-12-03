using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WarehouseApplication.BLL
{
    public class ArrivalModel : BaseModel
    {
      
        private static Dictionary<Guid, LookupValue> _CommodityTypeLu;
        private static Dictionary<Guid, LookupValue> _RepsLu;
        private static Dictionary<Guid,  LookupValue> _LocationLu;
        
        //public Guid RepresentativeID { get; set; }
        //public String RepresentativeName
        //{
        //    get
        //    {
        //        if (ArrivalModel._RepsLu == null)
        //            return String.Empty;
        //        return ArrivalModel._RepsLu[RepresentativeID].Description;
        //    }
        //}

        public Guid CommodityID { get; set; }
        public String CommodityCategory
        {
            get
            {
                if (ArrivalModel._CommodityTypeLu == null)
                    return String.Empty;
                return ArrivalModel._CommodityTypeLu[CommodityID].Description;
            }
        }

        public bool IsLocationKnown { get; set; }
        public Guid WoredaID { get; set; }
        public String SpecificArea { get; set; }
        public string ProcessingCenter { get; set; }
        public int ProductionYear { get; set; }
        public int NumberofBags { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public bool IsNonTruck { get; set; }

        public bool IsTruckInCompound { get; set; }
        public String DriverName { get; set; }
        public String LicenseNumber { get; set; }
        public String LicenseIssuedPlace { get; set; }
        public String TruckPlateNumber { get; set; }
        public String TrailerPlateNumber { get; set; }
        public Guid WashingStation { get; set; }
        public double VehicleSize { get; set; }

        public bool IsBiProduct { get; set; }
        public bool HasVoucher { get; set; }
        public String VoucherNumber { get; set; }
        public Guid VoucherCommodityTypeID { get; set; }
        public int VoucherNumberOfBags { get; set; }
        public int VoucherNumberOfPlomps { get; set; }
        public int VoucherNumberOfPlompsTrailer { get; set; }
        //public String VoucherCertificateNo { get; set; }

        public decimal VoucherWeight { get; set; }
        public string Remark { get; set; }

        public string CommandType { get; set; }
        public int GRNStatus { get; set; }
             
        public ArrivalModel() 
        { 
            IsNew = true;               
        }
        public void RefreshAll()
        {
            GetByTrackingNo();
        }

        public ArrivalModel GetByTrackingNo()
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(TrackingNumber))
                ECX.DataAccess.SQLHelper.PopulateTable(ConnectionString, dt, "ArrivalGetByTrackingNo", TrackingNumber);
            else if (ID != Guid.Empty)
                ECX.DataAccess.SQLHelper.PopulateTable(ConnectionString, dt, "ArrivalGetByID", ID);
            else
                return this;
            foreach (DataRow dr in dt.Rows)
            {
                ECX.DataAccess.Common.DataRow2Object(dr, this);
                break;
            }
            IsNew = false;
            return this;
        }

        public DataTable getwashstation(string ReID)
        {
            DataTable dt;
            return ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "spgetwashstation", ReID);
        }

        public DataTable getClientCertificates()
        {
            return ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "spgetclientcertificates");
        }

        public ArrivalModel SavePreArrival()
        {
           DataRow dr = ECX.DataAccess.SQLHelper.SaveAndReturnRow(ConnectionString, "ArrivalSavePre", this);
           ECX.DataAccess.Common.DataRow2Object(dr, this);
            return this;
        }

        public DataTable getallvehiclesize()
        {
            return ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetvehicleSize");
        }
        public ArrivalModel Save()
        {
            //Do validation  such as it cannot be new etc.
            if (!IsValid())
            {
                throw new Exception(ErrorMessage);
            }
            DataRow dr = ECX.DataAccess.SQLHelper.SaveAndReturnRow(ConnectionString, "ArrivalSave", this);
            ECX.DataAccess.Common.DataRow2Object(dr, this);
            return this;
        }

        public void saveArrivalCertificate(Guid ArrivalID, Guid CertID, string CertificateName)
        {
            Guid userID =  this.UserID;
            ECX.DataAccess.SQLHelper.execNonQuery(ConnectionString, "saveArrivalCertificate", ArrivalID, CertID, CertificateName, userID);
        }

        public override bool IsValid()
        {
            ErrorMessage = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool isvalid = true;
            if (IsLocationKnown && WoredaID == Guid.Empty)
            {
                isvalid = false;
                sb.Append("Woreda location missing");
            }
            return isvalid;
        }
        //added by Behailu for daily arrival
        public DataTable SearchDailyArrivalList(string datefrom, string dateto)
        {
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "spGetDailyArrival", datefrom, dateto);
            return dt;
        }
    }


}