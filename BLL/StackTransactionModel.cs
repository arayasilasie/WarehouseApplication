using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using ECX.DataAccess;
using WarehouseApplication.BLL;
namespace GINBussiness
{
    public class StackTransactionModel : WarehouseBaseModel
    {
        public Guid ID { get; set; }

        public Guid StackTransactionID { get; set; }
        public Guid StackID { get; set; }
        public Guid ShedID { get; set; }
        public string StackNumber { get; set; }
        public GINModel ParentGIN { get; private set; }
        public Guid GINID { get; set; }
        public Guid WeigherID { get; set; }
        public int WBServiceProviderID { get; set; }
        public string ServiceProviderName { get; set; }
        public DateTime DateTimeWeighed { get; set; }
        public string ScaleTicketNumber { get; set; }
        public double TruckWeight { get; set; }
        public double GrossWeight { get; set; }
        public Guid BagTypeID { get; set; }
        public double Tare
        {
            get
            {
                return (double)SQLHelper.ExecuteScalar(ConnectionString, "GetTareByStackID", StackID);
            }
        }
        public double NoOfBags { get; set; }
        public string LoadUnloadTicketNO { get; set; }
        public DateTime DateTimeLoaded { get; set; }
        public int AddReturnTypeID { get; set; }
        public double AddReturnAmount { get; set; }
        public int TruckTypeID { get; set; }
        public string TruckTypeName { get; set; }

        public double NetWeight
        {
            get
            {
                return GrossWeight - TruckWeight - NoOfBags * Tare + AddReturnAmount * AddReturnTypeID;
            }
        }
        public string ToXML
        {
            get
            {
                return "<stackInfo> " +
                        "<StackID>" + StackID.ToString() + "</StackID>" +
                        "<WeigherID>" + WeigherID + "</WeigherID>" +
                        "<WBServiceProviderID>" + WBServiceProviderID.ToString() + "</WBServiceProviderID>" +
                        "<DateWeighed>" + DateTimeWeighed.ToString() + "</DateWeighed>" +
                        "<ScaleTicketNumber>" + ScaleTicketNumber + "</ScaleTicketNumber>" +
                        "<TruckTypeID>" + TruckTypeID.ToString() + "</TruckTypeID>" +
                        "<GrossWeight>" + GrossWeight.ToString() + "</GrossWeight>" +
                        "<TruckWeight>" + TruckWeight.ToString() + "</TruckWeight>" +
                        "<NoOfBags>" + NoOfBags.ToString() + "</NoOfBags>" +
                        "<LoadUnloadTicketNO>" + LoadUnloadTicketNO + "</LoadUnloadTicketNO>" +
                        "<AddReturnTypeID>" + AddReturnTypeID.ToString() + "</AddReturnTypeID>" +
                        "<AddReturnAmount>" + AddReturnAmount.ToString() + "</AddReturnAmount>" +
                        "</stackInfo>";
            }
        }
        public StackTransactionModel(GINModel parentGin)
        {
            ParentGIN = parentGin;
            GINID = ParentGIN.ID;
        }
        public bool IsValid(bool isCoffee)
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;
            if (StackID == Guid.Empty && (!isCoffee))
            {
                //message.AppendLine("Stack No is required. <br/>");
                isValid = false;
                throw new System.ArgumentException("Stack No is required.", "original"); 
            }
            if (string.IsNullOrEmpty(ScaleTicketNumber))
            {
                //message.AppendLine("Please enter Scale Ticket Number.It is a required field.<br/>");
                isValid = false;
                throw new System.ArgumentException("Please enter Scale Ticket Number.It is a required field.", "original"); 
            }
            if (string.IsNullOrEmpty(LoadUnloadTicketNO) && isCoffee == false)
            {
                //message.AppendLine("Please enter Loading Ticket Number.It is a required field.<br/>");
                isValid = false;
                throw new System.ArgumentException("Please enter Loading Ticket Number.It is a required field.", "original"); 
            }
            if (ParentGIN.TruckRegisterTime > DateTimeWeighed && (!isCoffee))
            {
                //message.AppendLine("Invalid Weighing date");                
                isValid = false;
                throw new System.ArgumentException("Invalid Weighing date ", "original");  
            }
            if (WeigherID == Guid.Empty)
            {
                isValid = false;
                throw new System.ArgumentException("Please select the weigher.");              
              //  message.AppendLine("Please select the weigher <br/>");               
            }          
            if (TruckWeight < 0)
            {
                //.AppendLine("Please enter Truck Weight.<br/>");
                isValid = false;
                throw new System.ArgumentException("Please enter Truck Weight.", "original");          
            }
            if (GrossWeight <= 0)
            {
                message.AppendLine("Please enter  Gross Weight .<br/>");
                isValid = false;
            }
            if (GrossWeight - TruckWeight <= 0)
            {
                //message.AppendLine("Please enter correct the Truck Weight it is more than gross waight. <br/>");
                isValid = false;
                throw new System.ArgumentException("Please enter correct the Truck Weight it is more than gross waight. ", "original"); 
            }
            if (NoOfBags <= 0)
            {
               // message.AppendLine("Please enter No Of Bag.<br/>");
                isValid = false;
                throw new System.ArgumentException("Please enter No Of Bag. ", "original"); 
            }
            if (DateTimeWeighed < ParentGIN.TruckRegisterTime && (!isCoffee))
            {
                //message.AppendLine("Date Time Weighed cannot be earlier than truck provided time");
                isValid = false;
                throw new System.ArgumentException("Date Time Weighed cannot be earlier than truck provided time. ", "original"); 
            }
            //if (DateTimeWeighed > ParentGIN.DateIssued)
            //{
            //    message.AppendLine("Date Time Weighed cannot be later than loading time");
            //    isValid = false;
            //}
            ErrorMessage = message.ToString();
            return isValid;
        }
        public static List<StackTransactionModel> GetStackInShed(GINModel parentGin)
        {
            List<StackTransactionModel> StackByShedList;
            StackByShedList = new List<StackTransactionModel>();
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(
                ConnectionString,
                "GetStackByShedId",
                parentGin.LICShedID,
                parentGin.CommodityGradeID,
                parentGin.ProductionYear
                );
            foreach (DataRow r in dt.Rows)
            {
                StackTransactionModel pnm = new StackTransactionModel(parentGin);
                Common.DataRow2Object(r, pnm);
                StackByShedList.Add(pnm);
            }
            return StackByShedList;
        }
        public static List<StackTransactionModel> GetCurCoffBalance(GINModel parentGin)
        {
            List<StackTransactionModel> StackByShedList;
            StackByShedList = new List<StackTransactionModel>();
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(
                ConnectionString,
                "GetCurCoffBalance",
                parentGin.PickupNoticesList[0].WarehouseReceiptNo,
                parentGin.LICShedID
                );
            foreach (DataRow r in dt.Rows)
            {
                StackTransactionModel pnm = new StackTransactionModel(parentGin);
                Common.DataRow2Object(r, pnm);
                StackByShedList.Add(pnm);
            }
            return StackByShedList;
        }

        public static List<StackTransactionModel> GetWBServiceProvider(GINModel parentGin)
        {
            List<StackTransactionModel> ServiceProviderList;
            ServiceProviderList = new List<StackTransactionModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetWBServiceProvider", parentGin.WarehouseID);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                StackTransactionModel pnm = new StackTransactionModel(parentGin);
                Common.DataRow2Object(r, pnm);
                ServiceProviderList.Add(pnm);
            }
            return ServiceProviderList;
        }
        public static List<StackTransactionModel> GetTruckType(GINModel parentGin)
        {
            List<StackTransactionModel> ServiceProviderList;
            ServiceProviderList = new List<StackTransactionModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetTruckType");
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                StackTransactionModel pnm = new StackTransactionModel(parentGin);
                Common.DataRow2Object(r, pnm);
                ServiceProviderList.Add(pnm);
            }
            return ServiceProviderList;
        }
        public static DataRow GetShedCurrentBalance(GINModel parentGin)
        {
            List<StackTransactionModel> StackByShedList;
            StackByShedList = new List<StackTransactionModel>();
            DataRow dt = ECX.DataAccess.SQLHelper.getDataRow(
                ConnectionString,
                "GetShedCurrentBalance",
                parentGin.LICShedID,
                parentGin.CommodityGradeID,
                parentGin.ProductionYear
                );
            return dt;
        }

        public static DataRow GetShedCurrCoffBalance(GINModel parentGin)
        {
            List<StackTransactionModel> StackByShedList;
            StackByShedList = new List<StackTransactionModel>();
            DataRow dt = ECX.DataAccess.SQLHelper.getDataRow(
                ConnectionString,
                "GetCurCoffBalance",
                parentGin.PickupNoticesList[0].WarehouseReceiptNo,
                parentGin.LICShedID
                );
            return dt;
        }

    }
    
}