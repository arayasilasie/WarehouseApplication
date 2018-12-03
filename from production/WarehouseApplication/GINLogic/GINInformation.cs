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
using System.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;
using WarehouseApplication.BLL;

namespace WarehouseApplication.GINLogic
{
    
    [Serializable]
    [XmlRootAttribute("PickupNotice", IsNullable = false)]
    public class PickupNoticeInformation
    {
        #region Private Members
        private Guid pickupNoticeId;
        private Guid clientId;
        private Guid memberId;
        private string repId;
        private Guid commodityGradeId;
        private int productionYear;
        private Guid warehouseId;
        private decimal quantity;
        private decimal weight;
        private DateTime expirationDate;
        private DateTime expectedPickupDate;
        private int status;
        private List<WarehouseReceiptInformation> warehouseReceipts = new List<WarehouseReceiptInformation>();
        private List<PickupNoticeAgentInformation> pickupNoticeAgents = new List<PickupNoticeAgentInformation>();
        #endregion

        #region Constructors
        public PickupNoticeInformation() { }
        public PickupNoticeInformation(
            Guid pickupNoticeId,
            Guid clientId,
            Guid memberId,
            string repId,
            Guid warehouseId,
            DateTime expirationDate,
            DateTime expectedPickupDate,
            Guid commodityGradeId,
            int productionYear,
            decimal quantity,
            decimal weight,
            int status)
        {
            this.pickupNoticeId = pickupNoticeId;
            this.clientId = clientId;
            this.memberId = memberId;
            this.repId = repId;
            this.warehouseId = warehouseId;
            this.expirationDate = expirationDate;
            this.expectedPickupDate = expectedPickupDate;
            this.commodityGradeId = commodityGradeId;
            this.productionYear = productionYear;
            this.quantity = quantity;
            this.weight = weight;
            this.status = status;
        }
        #endregion

        #region Properties
        [XmlAttribute("Id")]
        public Guid PickupNoticeId
        {
            get { return pickupNoticeId; }
            set { pickupNoticeId = value; }
        }

        [XmlAttribute]
        public Guid ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        [XmlAttribute]
        public Guid MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        [XmlAttribute]
        public string RepId
        {
            get { return repId; }
            set { repId = value; }
        }

        [XmlAttribute]
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        [XmlAttribute]
        public DateTime ExpectedPickupDate
        {
            get { return expectedPickupDate; }
            set { expectedPickupDate = value; }
        }

        [XmlAttribute]
        public Guid CommodityGradeId
        {
            get { return commodityGradeId; }
            set { commodityGradeId = value; }
        }

        [XmlAttribute]
        public int ProductionYear
        {
            get { return productionYear; }
            set { productionYear = value; }
        }

        [XmlAttribute]
        public Guid WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        [XmlAttribute]
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [XmlAttribute]
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlArrayAttribute("WarehouseReceipts")]
        [XmlArrayItem("WarehouseReceipt")]
        public List<WarehouseReceiptInformation> WarehouseReceipts
        {
            get
            {
                return warehouseReceipts;
            }
        }

        [XmlArrayAttribute("PickupNoticeAgents")]
        [XmlArrayItem("PickupNoticeAgent")]
        public List<PickupNoticeAgentInformation> PickupNoticeAgents
        {
            get
            {
                return pickupNoticeAgents;
            }
        }
        #endregion

        public void Copy(PickupNoticeInformation puni)
        {
            this.clientId = puni.ClientId;
            this.memberId = puni.memberId;
            this.repId = puni.repId;
            this.commodityGradeId = puni.CommodityGradeId;
            this.productionYear = puni.ProductionYear;
            this.expectedPickupDate = puni.ExpectedPickupDate;
            this.expirationDate = puni.ExpirationDate;
            this.pickupNoticeId = puni.PickupNoticeId;
            this.quantity = puni.Quantity;
            this.status = puni.Status;
            this.warehouseId = puni.WarehouseId;
            this.weight = puni.Weight;
        }

        #region Nested Classes

        [Serializable]
        public class WarehouseReceiptInformation
        {
            private Guid id;
            private int warehouseReceiptId;
            private Guid pickupNoticeId;
            private Guid grnId;
            private string grnNo;
            private Guid shedId;
            private Guid bagType;
            private decimal quantity;
            private decimal weight;
            private int bags;
            public WarehouseReceiptInformation() { }
            public WarehouseReceiptInformation(
                Guid id,
                int warehouseReceiptId,
                Guid pickupNoticeId,
                Guid grnId,
                string grnNo,
                Guid shedId,
                Guid bagType,
                decimal quantity,
                decimal weight,
                int bags)
            {
                this.id = id;
                this.warehouseReceiptId = warehouseReceiptId;
                this.pickupNoticeId = pickupNoticeId;
                this.grnId = grnId;
                this.grnNo = grnNo;
                this.shedId = shedId;
                this.bagType = bagType;
                this.quantity = quantity;
                this.weight = weight;
                this.bags = bags;
            }

            [XmlAttribute]
            public Guid Id
            {
                get { return id; }
                set { id = value; }
            }

            [XmlAttribute]
            public int WarehouseReceiptId
            {
                get { return warehouseReceiptId; }
                set { warehouseReceiptId = value; }
            }

            [XmlAttribute]
            public Guid PickupNoticeId
            {
                get { return pickupNoticeId; }
                set { pickupNoticeId = value; }
            }

            [XmlAttribute]
            public Guid GRNID
            {
                get { return grnId; }
                set { grnId = value; }
            }

            [XmlAttribute]
            public string GRNNo
            {
                get { return grnNo; }
                set { grnNo = value; }
            }

            [XmlAttribute]
            public Guid ShedId
            {
                get { return shedId; }
                set { shedId = value; }
            }

            [XmlAttribute]
            public Guid BagType
            {
                get { return bagType; }
                set { bagType= value; }
            }

            [XmlAttribute]
            public decimal Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            [XmlAttribute]
            public decimal Weight
            {
                get { return weight; }
                set { weight = value; }
            }

            [XmlAttribute]
            public int Bags
            {
                get { return bags; }
                set { bags = value; }
            }
        }

        [Serializable]
        public class PickupNoticeAgentInformation
        {
            private Guid id;
            private Guid pickupNoticeId;
            private string agentName;
            private string agentTel;
            private int nidType;
            private string nidNumber;
            private int status;

            public PickupNoticeAgentInformation() { }
            public PickupNoticeAgentInformation(
                Guid id,
                Guid pickupNoticeId,
                string agentName,
                string agentTel,
                int nidType,
                string nidNumber,
                int status)
            {
                this.id = id;
                this.pickupNoticeId = pickupNoticeId;
                this.agentName = agentName;
                this.agentTel = agentTel;
                this.nidType = nidType;
                this.nidNumber = nidNumber;
                this.status = status;
            }

            [XmlAttribute]
            public Guid Id
            {
                get { return id; }
                set { id = value; }
            }

            [XmlAttribute]
            public Guid PickupNoticeId
            {
                get { return pickupNoticeId; }
                set { pickupNoticeId = value; }
            }

            [XmlAttribute]
            public string AgentName
            {
                get { return agentName; }
                set { agentName = value; }
            }

            [XmlAttribute]
            public string AgentTel
            {
                get { return agentTel; }
                set { agentTel = value; }
            }

            [XmlAttribute]
            public int NIDType
            {
                get { return nidType; }
                set { nidType = value; }
            }

            [XmlAttribute]
            public string NIDNumber
            {
                get { return nidNumber; }
                set { nidNumber = value; }
            }

            [XmlAttribute]
            public int Status
            {
                get { return status; }
                set { status = value; }
            }

        }
        #endregion
    }

    [Serializable]
    public class PUNAcknowledgementInformation
    {
        private Guid punAcknowledgementId;
        private DateTime dateReceived;
        private Guid pickupNoticeId;
        private Guid clientId;
        private Guid memberId;
        private string repId;
        private Guid commodityGradeId;
        private int productionYear;
        private Guid pickupNoticeAgentId;
        private Guid pickupNoticeVerifier;
        private bool pickupNoticeAgentVerified;
        private Guid availabilityVerifier;
        private bool availabilityVerified;
        private DateTime availabilityVerifiedOn;
        private decimal availableInventoryBags;
        private decimal pledgedQuantity;
        private decimal pledgedWeight;
        private decimal issuedWeight;
        private bool gradingRequired;
        private decimal weight;
        private decimal quantity;
        private decimal remainingWeight;
        private decimal remainingQuantity;
        private string agentName;
        private string transactionId;
        private int status;

        public PUNAcknowledgementInformation(
            Guid punAcknowledgementId,
            DateTime dateReceived,
            Guid pickupNoticeId,
            Guid clientId,
            Guid memberId,
            string repId,
            Guid commodityGradeId,
            int productionYear,
            Guid pickupNoticeAgentId,
            Guid pickupNoticeVerifier,
            bool pickupNoticeAgentVerified,
            Guid availabilityVerifier,
            bool availabilityVerified,
            DateTime availabilityVerifiedOn,
            decimal availableInventoryBags,
            decimal pledgedQuantity,
            decimal pledgedWeight,
            decimal issuedWeight,
            bool gradingRequired,
            decimal weight,
            decimal quantity,
            decimal remainingWeight,
            decimal remainingQuantity,
            string agentName,
            string transactionId,
            int status)
        {
            this.punAcknowledgementId = punAcknowledgementId;
            this.dateReceived = dateReceived;
            this.commodityGradeId = commodityGradeId;
            this.productionYear = productionYear;
            this.pickupNoticeAgentId = pickupNoticeAgentId;
            this.pickupNoticeId = pickupNoticeId;
            this.clientId = clientId;
            this.memberId = memberId;
            this.repId = repId;
            this.pickupNoticeVerifier = pickupNoticeVerifier;
            this.pickupNoticeAgentVerified = pickupNoticeAgentVerified;
            this.availabilityVerifier = availabilityVerifier;
            this.availabilityVerified = availabilityVerified;
            this.availabilityVerifiedOn = availabilityVerifiedOn;
            this.availableInventoryBags = availableInventoryBags;
            this.pledgedQuantity = pledgedQuantity;
            this.pledgedWeight = pledgedWeight;
            this.issuedWeight = issuedWeight;
            this.gradingRequired = gradingRequired;
            this.weight = weight;
            this.quantity = quantity;
            this.remainingQuantity = remainingQuantity;
            this.remainingWeight = remainingWeight;
            this.agentName = agentName;
            this.transactionId = transactionId;
            this.status = status;
        }

        public PUNAcknowledgementInformation() { }
        [XmlAttribute("Id")]
        public Guid PUNAcknowledgementId
        {
            get { return punAcknowledgementId; }
            set { punAcknowledgementId = value; }
        }

        [XmlAttribute(DataType = "dateTime")]
        public DateTime DateReceived
        {
            get { return dateReceived; }
            set { dateReceived = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeAgentId
        {
            get { return pickupNoticeAgentId; }
            set { pickupNoticeAgentId = value; }
        }

        [XmlAttribute]
        public Guid CommodityGradeId
        {
            get { return commodityGradeId; }
            set { commodityGradeId = value; }
        }

        [XmlAttribute]
        public int ProductionYear
        {
            get { return productionYear; }
            set { productionYear = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeId
        {
            get { return pickupNoticeId; }
            set { pickupNoticeId = value; }
        }

        [XmlAttribute]
        public Guid ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        [XmlAttribute]
        public Guid MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        [XmlAttribute]
        public string RepId
        {
            get { return repId; }
            set { repId = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeVerifier
        {
            get { return pickupNoticeVerifier; }
            set { pickupNoticeVerifier = value; }
        }

        [XmlAttribute(DataType = "boolean")]
        public bool PickupNoticeAgentVerified
        {
            get { return pickupNoticeAgentVerified; }
            set { pickupNoticeAgentVerified = value; }
        }

        [XmlAttribute]
        public Guid AvailabilityVerifier
        {
            get { return availabilityVerifier; }
            set { availabilityVerifier = value; }
        }

        [XmlAttribute(DataType = "boolean")]
        public bool AvailabilityVerified
        {
            get { return availabilityVerified; }
            set { availabilityVerified = value; }
        }

        [XmlAttribute(DataType = "dateTime")]
        public DateTime AvailabilityVerifiedOn
        {
            get { return availabilityVerifiedOn; }
            set { availabilityVerifiedOn = value; }
        }

        [XmlAttribute]
        public decimal AvailableInventoryBags
        {
            get
            {
                decimal lotSize = Convert.ToDecimal(CommodityGradeBLL.GetCommodityGradeLotSizeInBagsById(CommodityGradeId));
                decimal weightTolerance = decimal.Parse(ConfigurationManager.AppSettings["WeightTolerance"]);
                decimal fractionAvailableQuantityAdjusted = (decimal)1;
                int wholeAvailableQuantity = (int)(availableInventoryBags / lotSize);
                decimal fractionAvailableQuantity = availableInventoryBags - wholeAvailableQuantity * lotSize;
                if (fractionAvailableQuantity < weightTolerance * lotSize)
                    fractionAvailableQuantityAdjusted = 0;
                else if (lotSize - fractionAvailableQuantity > weightTolerance * lotSize)
                    fractionAvailableQuantityAdjusted = fractionAvailableQuantity / lotSize;
                decimal availableQuantity = wholeAvailableQuantity + fractionAvailableQuantityAdjusted;

                return Math.Truncate(availableQuantity * 10000 + 0.9m) / 10000;
            }
            set { availableInventoryBags = value; }
        }

        [XmlAttribute]
        public decimal PledgedWeight
        {
            get { return pledgedWeight; }
            set { pledgedWeight = value; }
        }

        [XmlAttribute]
        public decimal IssuedWeight
        {
            get { return issuedWeight; }
            set { issuedWeight = value; }
        }

        [XmlAttribute]
        public decimal PledgedQuantity
        {
            get { return pledgedQuantity; }
            set { pledgedQuantity = value; }
        }

        [XmlAttribute]
        public bool GradingRequired
        {
            get { return gradingRequired; }
            set { gradingRequired = value; }
        }

        [XmlAttribute]
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        [XmlAttribute]
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [XmlAttribute]
        public decimal RemainingWeight
        {
            get { return remainingWeight; }
            set { remainingWeight = value; }
        }

        [XmlAttribute]
        public decimal RemainingQuantity
        {
            get { return remainingQuantity; }
            set { remainingQuantity = value; }
        }

        [XmlAttribute]
        public string AgentName
        {
            get { return agentName; }
            set { agentName = value; }
        }

        [XmlAttribute]
        public string TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public void Copy(PUNAcknowledgementInformation punaInfo)
        {
            this.punAcknowledgementId = punaInfo.punAcknowledgementId;
            this.dateReceived = punaInfo.dateReceived;
            this.commodityGradeId = punaInfo.commodityGradeId;
            this.productionYear = punaInfo.ProductionYear;
            this.pickupNoticeAgentId = punaInfo.pickupNoticeAgentId;
            this.pickupNoticeId = punaInfo.pickupNoticeId;
            this.clientId = punaInfo.clientId;
            this.memberId = punaInfo.memberId;
            this.repId = punaInfo.repId;
            this.pickupNoticeVerifier = punaInfo.pickupNoticeVerifier;
            this.pickupNoticeAgentVerified = punaInfo.pickupNoticeAgentVerified;
            this.availabilityVerifier = punaInfo.availabilityVerifier;
            this.availabilityVerified = punaInfo.availabilityVerified;
            this.availabilityVerifiedOn = punaInfo.availabilityVerifiedOn;
            this.availableInventoryBags = punaInfo.availableInventoryBags;
            this.pledgedQuantity = punaInfo.pledgedQuantity;
            this.pledgedWeight = punaInfo.pledgedWeight;
            this.weight = punaInfo.weight;
            this.quantity = punaInfo.quantity;
            this.remainingQuantity = punaInfo.remainingQuantity;
            this.remainingWeight = punaInfo.remainingWeight;
            this.agentName = punaInfo.agentName;
            this.transactionId = punaInfo.transactionId;
            this.status = punaInfo.status;
        }
    }

    [Serializable]
    [XmlRootAttribute("GINProcess", IsNullable = false)]
    public class GINProcessInfo
    {
        private Guid ginProcessId;
        private Guid clientId;
        private Guid memberId;
        private string repId;
        private DateTime dateReceived;
        private Guid pickupNoticeId;
        private Guid commodityGradeId;
        private int productionYear;
        private Guid pickupNoticeAgentId;
        private Guid pickupNoticeVerifier;
        private bool pickupNoticeAgentVerified;
        private DateTime pickupNoticeAgentVerifiedOn;
        private Guid availabilityVerifier;
        private bool availabilityVerified;
        private DateTime availabilityVerifiedOn;
        //private decimal availableInventoryWeight;
        private int availableInventoryBags;
        private decimal pledgedQuantity;
        private decimal pledgedWeight;
        private decimal issuedWeight;
        private bool gradingRequired;
        private decimal weight;
        private decimal quantity;
        private decimal remainingWeight;
        private decimal remainingQuantity;
        private string agentName;
        private string transactionId;
        private int status;
        private decimal adjustmentParcelWeight;
        private GradingInfo grading;
        private List<GINTruckInfo> trucks = new List<GINTruckInfo>();
        private List<SampleInfo> samples = new List<SampleInfo>();

        #region Constructors
        public GINProcessInfo(
            Guid ginProcessId,
            DateTime dateReceived,
            Guid pickupNoticeId,
            Guid commodityGradeId,
            int productionYear,
            Guid pickupNoticeAgentId,
            Guid pickupNoticeVerifier,
            bool pickupNoticeAgentVerified,
            DateTime pickupNoticeAgentVerifiedOn,
            Guid availabilityVerifier,
            bool availabilityVerified,
            DateTime availabilityVerifiedOn,
            //decimal availableInventoryWeight,
            int availableInventoryBags,
            decimal pledgedQuantity,
            decimal pledgedWeight,
            decimal issuedWeight,
            bool gradingRequired,
            decimal weight,
            decimal quantity,
            decimal remainingWeight,
            decimal remainingQuantity,
            string agentName,
            string transactionId,
            int status)
        {
            this.ginProcessId = ginProcessId;
            this.dateReceived = dateReceived;
            this.commodityGradeId = commodityGradeId;
            this.productionYear = productionYear;
            this.pickupNoticeAgentId = pickupNoticeAgentId;
            this.pickupNoticeId = pickupNoticeId;
            this.pickupNoticeVerifier = pickupNoticeVerifier;
            this.pickupNoticeAgentVerified = pickupNoticeAgentVerified;
            this.pickupNoticeAgentVerifiedOn = pickupNoticeAgentVerifiedOn;
            //this.availableInventoryWeight = availableInventoryWeight;
            this.availableInventoryBags = availableInventoryBags;
            this.availabilityVerifier = availabilityVerifier;
            this.availabilityVerified = availabilityVerified;
            this.pledgedQuantity = pledgedQuantity;
            this.pledgedWeight = pledgedWeight;
            this.issuedWeight = issuedWeight;
            this.gradingRequired = gradingRequired;
            this.weight = weight;
            this.availabilityVerified = availabilityVerified;
            this.quantity = quantity;
            this.remainingQuantity = remainingQuantity;
            this.remainingWeight = remainingWeight;
            this.agentName = agentName;
            this.transactionId = transactionId;
            this.status = status;
        }

        public GINProcessInfo()
        {
        }

        #endregion

        #region Properties

        [XmlAttribute("Id")]
        public Guid GINProcessId
        {
            get { return ginProcessId; }
            set { ginProcessId = value; }
        }

        [XmlAttribute("ClientId")]
        public Guid ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        [XmlAttribute("MemberId")]
        public Guid MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        [XmlAttribute("RepId")]
        public string RepId
        {
            get { return repId; }
            set { repId = value; }
        }

        [XmlAttribute(DataType = "dateTime")]
        public DateTime DateReceived
        {
            get { return dateReceived; }
            set { dateReceived = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeAgentId
        {
            get { return pickupNoticeAgentId; }
            set { pickupNoticeAgentId = value; }
        }

        [XmlAttribute]
        public Guid CommodityGradeId
        {
            get { return commodityGradeId; }
            set { commodityGradeId = value; }
        }

        [XmlAttribute]
        public int ProductionYear
        {
            get { return productionYear; }
            set { productionYear = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeId
        {
            get { return pickupNoticeId; }
            set { pickupNoticeId = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeVerifier
        {
            get { return pickupNoticeVerifier; }
            set { pickupNoticeVerifier = value; }
        }

        [XmlAttribute(DataType = "boolean")]
        public bool PickupNoticeAgentVerified
        {
            get { return pickupNoticeAgentVerified; }
            set { pickupNoticeAgentVerified = value; }
        }

        [XmlAttribute]
        public DateTime PickupNoticeAgentVerifiedOn
        {
            get { return pickupNoticeAgentVerifiedOn; }
            set { pickupNoticeAgentVerifiedOn = value; }
        }

        //[XmlAttribute]
        //public decimal AvailableInventoryWeight
        //{
        //    get { return availableInventoryWeight; }
        //    set { availableInventoryWeight = value; }
        //}
        [XmlIgnore]
        public decimal AvailableInventoryWeight
        {
            get 
            {
                float bagCapacity = BagTypeBLL.GetCommodityGradeBagTypes(commodityGradeId)[0].Capacity;
                return Math.Truncate(availableInventoryBags * Convert.ToDecimal(bagCapacity)*100 + 0.9m)/100; 
            }
        }

        [XmlAttribute]
        public int AvailableInventoryBags
        {
            get { return availableInventoryBags; }
            set { availableInventoryBags = value; }
        }

        [XmlAttribute]
        public Guid AvailabilityVerifier
        {
            get { return availabilityVerifier; }
            set { availabilityVerifier = value; }
        }

        [XmlAttribute]
        public bool AvailabilityVerified
        {
            get { return availabilityVerified; }
            set { availabilityVerified = value; }
        }

        [XmlAttribute]
        public DateTime AvailabilityVerifiedOn
        {
            get { return availabilityVerifiedOn; }
            set { availabilityVerifiedOn = value; }
        }

        [XmlAttribute]
        public decimal PledgedQuantity
        {
            get { return pledgedQuantity; }
            set { pledgedQuantity = value; }
        }

        [XmlAttribute]
        public decimal PledgedWeight
        {
            get { return pledgedWeight; }
            set { pledgedWeight = value; }
        }

        [XmlAttribute]
        public decimal IssuedWeight
        {
            get { return issuedWeight; }
            set { issuedWeight = value; }
        }

        [XmlAttribute]
        public bool GradingRequired
        {
            get { return gradingRequired; }
            set { gradingRequired = value; }
        }

        [XmlAttribute(DataType = "decimal")]
        public decimal Weight
        {
            get { return Math.Truncate(weight*100+0.9m)/100; }
            set { weight = value; }
        }

        [XmlAttribute(DataType = "decimal")]
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [XmlAttribute(DataType = "decimal")]
        public decimal RemainingWeight
        {
            get { return remainingWeight; }
            set { remainingWeight = Math.Truncate(value * 100 + 0.9M) / 100; }
        }

        [XmlAttribute(DataType = "decimal")]
        public decimal RemainingQuantity
        {
            get { return remainingQuantity; }
            set { remainingQuantity = value; }
        }

        [XmlAttribute]
        public string AgentName
        {
            get { return agentName; }
            set { agentName = value; }
        }

        [XmlAttribute]
        public string TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlAttribute]
        public Decimal AdjustmentParcelWeight
        {
            get { return adjustmentParcelWeight; }
            set { adjustmentParcelWeight = value; }
        }

        [XmlArray(ElementName = "Samples")]
        [XmlArrayItem("Sample")]
        public List<SampleInfo> Samples
        {
            get
            {
                return samples;
            }
        }

        [XmlArray(ElementName = "Trucks")]
        [XmlArrayItem("Truck")]
        public List<GINTruckInfo> Trucks
        {
            get
            {
                return trucks;
            }
        }

        [XmlElement]
        public GradingInfo Grading
        {
            get { return grading; }
            set { grading = value; }
        }

        [XmlIgnore]
        public PUNAcknowledgementInformation PUNAcknowledgement
        {
            get
            {
                return new PUNAcknowledgementInformation(
                    this.ginProcessId,
                    this.dateReceived,
                    this.pickupNoticeId,
                    this.clientId,
                    this.memberId,
                    this.repId,
                    this.commodityGradeId,
                    this.productionYear,
                    this.pickupNoticeAgentId,
                    this.pickupNoticeVerifier,
                    this.pickupNoticeAgentVerified,
                    this.availabilityVerifier,
                    this.availabilityVerified,
                    this.dateReceived,
                    this.AvailableInventoryBags,
                    this.pledgedQuantity,
                    this.pledgedWeight,
                    this.issuedWeight,
                    this.gradingRequired,
                    this.weight,
                    this.quantity,
                    this.remainingWeight,
                    this.remainingQuantity,
                    this.agentName,
                    this.transactionId,
                    this.status);
            }
        }

        [XmlIgnore]
        public List<GINTruckInfo> IssuedTrucks
        {
            get
            {
                var issuedTrucksQuery = from truck in trucks
                                        where (truck.GIN != null) && (truck.GIN.Status != (int)GINStatusType.ReadyToLoad)
                                        select truck;
                return new List<GINTruckInfo>(issuedTrucksQuery);
            }
        }

        [XmlIgnore]
        public List<GINTruckInfo> RegisteredTrucks
        {
            get
            {
                var registeredTrucksQuery = from truck in trucks
                                            where (truck.TransactionId==string.Empty) && (truck.Status == (int)GINStatusType.ReadyToLoad)
                                        select truck;
                return new List<GINTruckInfo>(registeredTrucksQuery);
            }
        }

        #endregion

        public void Copy(GINProcessInfo ginProcessInfo)
        {
            this.ginProcessId = ginProcessInfo.ginProcessId;
            this.dateReceived = ginProcessInfo.dateReceived;
            this.commodityGradeId = ginProcessInfo.commodityGradeId;
            this.productionYear = ginProcessInfo.productionYear;
            this.pickupNoticeAgentId = ginProcessInfo.pickupNoticeAgentId;
            this.pickupNoticeId = ginProcessInfo.pickupNoticeId;
            this.pickupNoticeVerifier = ginProcessInfo.pickupNoticeVerifier;
            this.pickupNoticeAgentVerified = ginProcessInfo.pickupNoticeAgentVerified;
            this.pickupNoticeAgentVerifiedOn = ginProcessInfo.pickupNoticeAgentVerifiedOn;
            this.availableInventoryBags = ginProcessInfo.availableInventoryBags;
            this.availabilityVerifier = ginProcessInfo.availabilityVerifier;
            this.availabilityVerified = ginProcessInfo.availabilityVerified;
            this.PledgedQuantity = ginProcessInfo.PledgedQuantity;
            this.pledgedWeight = ginProcessInfo.pledgedWeight;
            this.issuedWeight = ginProcessInfo.issuedWeight;
            this.weight = ginProcessInfo.weight;
            this.availabilityVerified = ginProcessInfo.availabilityVerified;
            this.quantity = ginProcessInfo.quantity;
            this.remainingQuantity = ginProcessInfo.remainingQuantity;
            this.remainingWeight = ginProcessInfo.remainingWeight;
            this.agentName = ginProcessInfo.agentName;
            this.transactionId = ginProcessInfo.transactionId;
            this.status = ginProcessInfo.status;
            this.adjustmentParcelWeight = ginProcessInfo.adjustmentParcelWeight;
        }
    }

    [Serializable]
    public class GINTruckInfo
    {
        private Guid truckId;
        private Guid ginProcessId;
        private Guid pickupNoticeId;
        private string transactionId;
        private string driverName;
        private string licenseNo;
        private string issuedBy;
        private Guid mainTruckId;
        private Guid trailerId;
        private string plateNo;
        private string trailerNo;
        private int status;
        private string remark;
        private TruckLoadInfo load;
        private TruckWeightInfo weight;
        private GINInfo gin;

        #region Constructors

        public GINTruckInfo()
        {
            load = new TruckLoadInfo(truckId, DateTime.Now,
                (Guid)SystemLookup.LookupSource.GetLookup("BagType").Keys.ElementAt(0),
                string.Empty);
            weight = new TruckWeightInfo(truckId, DateTime.Now, string.Empty, 0, 0, (Guid)SystemLookup.LookupSource.GetLookup("Weigher").Keys.ElementAt(0), string.Empty);
        }
        public GINTruckInfo(
            Guid truckId,
            Guid ginProcessId,
            Guid pickupNoticeId,
            string transactionId,
            string driverName,
            string licenseNo,
            string issuedBy,
            Guid mainTruckId,
            string plateNo,
            Guid trailerId,
            string trailerNo,
            int status,
            string remark)
            : this()
        {
            this.TruckId = truckId;
            this.ginProcessId = ginProcessId;
            this.pickupNoticeId = pickupNoticeId;
            this.transactionId = transactionId;
            this.driverName = driverName;
            this.licenseNo = licenseNo;
            this.issuedBy = issuedBy;
            this.mainTruckId = mainTruckId;
            this.plateNo = plateNo;
            this.trailerId = trailerId;
            this.trailerNo = trailerNo;
            this.status = status;
            this.remark = remark;
        }

        #endregion

        #region Properties

        [XmlAttribute("Id")]
        public Guid TruckId
        {
            get { return truckId; }
            set
            {
                truckId = value;
                load.TruckId = value;
                weight.TruckId = value;
            }
        }

        [XmlAttribute]
        public Guid GINProcessId
        {
            get { return ginProcessId; }
            set { ginProcessId = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeId
        {
            get { return pickupNoticeId; }
            set { pickupNoticeId = value; }
        }

        [XmlAttribute]
        public string TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }

        [XmlAttribute]
        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        [XmlAttribute]
        public string LicenseNo
        {
            get { return licenseNo; }
            set { licenseNo = value; }
        }

        [XmlAttribute]
        public string IssuedBy
        {
            get { return issuedBy; }
            set { issuedBy = value; }
        }

        [XmlAttribute]
        public Guid MainTruckId
        {
            get { return mainTruckId; }
            set { mainTruckId = value; }
        }

        [XmlAttribute]
        public Guid TrailerId
        {
            get { return trailerId; }
            set { trailerId = value; }
        }

        [XmlAttribute]
        public string PlateNo
        {
            get { return plateNo; }
            set { plateNo = value; }
        }

        [XmlAttribute]
        public string TrailerNo
        {
            get { return trailerNo; }
            set { trailerNo = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlIgnore]
        public int GINStatus
        {
            get 
            {
                if (GIN != null)
                    return GIN.Status;
                else
                    return (int)GINStatusType.ReadyToLoad;
            }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        [XmlIgnore]
        public decimal NetWeight
        {
            get 
            {
                if (gin != null)
                    return GIN.NetWeight;
                else
                    return 0M;
            }
        }

        [XmlElement("Load")]
        public TruckLoadInfo Load
        {
            get { return load; }
            set { load = value; }
        }

        [XmlElement("Weight")]
        public TruckWeightInfo Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        [XmlElement("GIN")]
        public GINInfo GIN
        {
            get { return gin; }
            set { gin = value; }
        }

        #endregion

        public void Copy(GINTruckInfo truckInfo)
        {
            this.truckId = truckInfo.truckId;
            this.ginProcessId = truckInfo.ginProcessId;
            this.pickupNoticeId = truckInfo.pickupNoticeId;
            this.transactionId = truckInfo.transactionId;
            this.driverName = truckInfo.driverName;
            this.licenseNo = truckInfo.licenseNo;
            this.issuedBy = truckInfo.issuedBy;
            this.mainTruckId = truckInfo.mainTruckId;
            this.trailerId = truckInfo.trailerId;
            this.plateNo = truckInfo.plateNo;
            this.trailerNo = truckInfo.trailerNo;
            this.status = truckInfo.status;
            this.remark = truckInfo.remark;
        }
    }


    [Serializable]
    public class TruckLoadInfo
    {
        private Guid truckId;
        private DateTime dateLoaded;
        private Guid bagType;
        private string remark;
        private List<TruckStackInfo> stacks;
        private List<WorkerInformation> loaders;


        #region Constructors
        public TruckLoadInfo()
        {
            this.stacks = new List<TruckStackInfo>();
            this.loaders = new List<WorkerInformation>();
        }
        public TruckLoadInfo(
            Guid truckId,
            DateTime dateLoaded,
            Guid bagType,
            string remark
        )
        {
            this.truckId = truckId;
            this.dateLoaded = dateLoaded;
            this.bagType = bagType;
            this.remark = remark;
            this.stacks = new List<TruckStackInfo>();
            this.loaders = new List<WorkerInformation>();
        }
        #endregion

        #region Properties

        [XmlAttribute]
        public Guid TruckId
        {
            get { return truckId; }
            set { truckId = value; }
        }

        [XmlAttribute]
        public DateTime DateLoaded
        {
            get { return dateLoaded; }
            set { dateLoaded = value; }
        }

        [XmlAttribute]
        public Guid BagType
        {
            get { return bagType; }
            set { bagType = value; }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        [XmlArrayItem("Stack")]
        [XmlArray(ElementName = "Stacks")]
        public List<TruckStackInfo> Stacks
        {
            get
            {
                return stacks;
            }
        }

        [XmlArray(ElementName = "Loaders")]
        [XmlArrayItem("Loader")]
        public List<WorkerInformation> Loaders
        {
            get
            {
                return loaders;
            }
        }

        [XmlIgnore]
        public int TotalLoad
        {
            get
            {
                var loadedBags = from stack in Stacks select stack.Bags;
                return loadedBags.Sum();
            }
        }

        #endregion


        public void Copy(TruckLoadInfo truckLoadInfo)
        {
            this.truckId = truckLoadInfo.truckId;
            this.dateLoaded = truckLoadInfo.dateLoaded;
            this.bagType = truckLoadInfo.bagType;
            this.remark = truckLoadInfo.remark;
        }
    }

    [Serializable]
    public class TruckStackInfo
    {
        private Guid truckStackId;
        private Guid truckId;
        private Guid stackId;
        private Guid shed;
        private Guid loadingSupervisor;
        private int bags;

        #region Constructors

        public TruckStackInfo() { }
        public TruckStackInfo(
            Guid truckStackId,
            Guid truckId,
            Guid stackId,
            Guid shed,
            Guid loadingSupervisor,
            int bags
            )
        {
            this.truckStackId = truckStackId;
            this.truckId = truckId;
            this.stackId = stackId;
            this.shed = shed;
            this.loadingSupervisor = loadingSupervisor;
            this.bags = bags;
        }
        #endregion

        #region Properties
        [XmlAttribute]
        public Guid TruckStackId
        {
            get { return truckStackId; }
            set { truckStackId = value; }
        }

        [XmlAttribute]
        public Guid TruckId
        {
            get { return truckId; }
            set { truckId = value; }
        }

        [XmlAttribute]
        public Guid StackId
        {
            get { return stackId; }
            set { stackId = value; }
        }

        [XmlAttribute]
        public Guid Shed
        {
            get { return shed; }
            set { shed = value; }
        }

        [XmlAttribute]
        public Guid LoadingSupervisor
        {
            get { return loadingSupervisor; }
            set { loadingSupervisor = value; }
        }

        [XmlAttribute]
        public int Bags
        {
            get { return bags; }
            set { bags = value; }
        }
        #endregion

        public void Copy(TruckStackInfo stackInfo)
        {
            this.truckStackId = stackInfo.truckStackId;
            this.truckId = stackInfo.truckId;
            this.stackId = stackInfo.stackId;
            this.loadingSupervisor = stackInfo.loadingSupervisor;
            this.shed = stackInfo.shed;
            this.bags = stackInfo.bags;
        }
    }

    [Serializable]
    public class ReturnedBagsInfo
    {
        private Guid returnedBagsId;
        private Guid truckId;
        private Guid stackId;
        private Guid shed;
        private decimal size;
        private bool returned;
        private int bags;

        #region Constructors

        public ReturnedBagsInfo() { }
        public ReturnedBagsInfo(
            Guid returnedBagsId,
            Guid truckId,
            Guid stackId,
            Guid shed,
            decimal size,
            bool returned,
            int bags
            )
        {
            this.returnedBagsId = returnedBagsId;
            this.truckId = truckId;
            this.stackId = stackId;
            this.shed = shed;
            this.size = size;
            this.returned = returned;
            this.bags = bags;
        }
        #endregion

        #region Properties
        [XmlAttribute]
        public Guid ReturnedBagsId
        {
            get { return returnedBagsId; }
            set { returnedBagsId = value; }
        }

        [XmlAttribute]
        public Guid TruckId
        {
            get { return truckId; }
            set { truckId = value; }
        }

        [XmlAttribute]
        public Guid StackId
        {
            get { return stackId; }
            set { stackId = value; }
        }

        [XmlAttribute]
        public Guid Shed
        {
            get { return shed; }
            set { shed = value; }
        }

        [XmlAttribute]
        public decimal Size
        {
            get { return size; }
            set { size = value; }
        }

        [XmlAttribute]
        public bool Returned
        {
            get { return returned; }
            set { returned = value; }
        }

        [XmlAttribute]
        public int Bags
        {
            get { return bags; }
            set { bags = value; }
        }
        #endregion

        public void Copy(ReturnedBagsInfo rbInfo)
        {
            this.returnedBagsId = rbInfo.returnedBagsId;
            this.truckId = rbInfo.truckId;
            this.stackId = rbInfo.stackId;
            this.size = rbInfo.size;
            this.returned = rbInfo.returned;
            this.shed = rbInfo.shed;
            this.bags = rbInfo.bags;
        }
    }

    [Serializable]
    public class TruckWeightInfo
    {
        private Guid truckId;
        private DateTime dateWeighed;
        private string scaleTicketNo;
        private decimal truckWeight;
        private decimal grossWeight;
        private Guid weighingSupervisor;
        private string remark;
        private List<ReturnedBagsInfo> returnedBags;
        private List<ReturnedBagsInfo> addedBags;
        private List<WorkerInformation> weighers;

        #region Constructors
        public TruckWeightInfo()
        {
            this.returnedBags = new List<ReturnedBagsInfo>();
            this.weighers = new List<WorkerInformation>();
            this.addedBags = new List<ReturnedBagsInfo>();
        }
        public TruckWeightInfo(
            Guid truckId,
            DateTime dateWeighed,
            string scaleTicketNo,
            decimal truckWeight,
            decimal grossWeight,
            Guid weighingSupervisor,
            string remark)
        {
            this.truckId = truckId;
            this.dateWeighed = dateWeighed;
            this.scaleTicketNo = scaleTicketNo;
            this.truckWeight = truckWeight;
            this.grossWeight = grossWeight;
            this.weighingSupervisor = weighingSupervisor;
            this.remark = remark;
            this.returnedBags = new List<ReturnedBagsInfo>();
            this.weighers = new List<WorkerInformation>();
            this.addedBags = new List<ReturnedBagsInfo>();
        }
        #endregion

        #region Properties

        [XmlAttribute]
        public Guid TruckId
        {
            get { return truckId; }
            set { truckId = value; }
        }

        [XmlAttribute]
        public DateTime DateWeighed
        {
            get { return dateWeighed; }
            set { dateWeighed = value; }
        }

        [XmlAttribute]
        public string ScaleTicketNo
        {
            get { return scaleTicketNo; }
            set { scaleTicketNo = value; }
        }

        [XmlAttribute]
        public decimal TruckWeight
        {
            get { return Math.Truncate(truckWeight*100+0.9M)/100; }
            set { truckWeight = value; }
        }

        [XmlAttribute]
        public decimal GrossWeight
        {
            get { return Math.Truncate(grossWeight*100+0.9M)/100; }
            set { grossWeight = value; }
        }

        [XmlAttribute]
        public Guid WeighingSupervisor
        {
            get { return weighingSupervisor; }
            set { weighingSupervisor = value; }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        [XmlArray(ElementName = "ReturnedBags")]
        [XmlArrayItem("ReturnedBag")]
        public List<ReturnedBagsInfo> ReturnedBags
        {
            get
            {
                return returnedBags;
            }
        }

        [XmlArray(ElementName = "AddedBags")]
        [XmlArrayItem("AddedBag")]
        public List<ReturnedBagsInfo> AddedBags
        {
            get
            {
                return addedBags;
            }
        }

        [XmlArray(ElementName = "Weighers")]
        [XmlArrayItem("Weigher")]
        public List<WorkerInformation> Weighers
        {
            get
            {
                return weighers;
            }
        }
        #endregion
        public void Copy(TruckWeightInfo truckWeightInfo)
        {
            this.truckId = truckWeightInfo.truckId;
            this.dateWeighed = truckWeightInfo.dateWeighed;
            this.scaleTicketNo = truckWeightInfo.scaleTicketNo;
            this.truckWeight = truckWeightInfo.truckWeight;
            this.grossWeight = truckWeightInfo.grossWeight;
            this.weighingSupervisor = truckWeightInfo.weighingSupervisor;
            this.remark = truckWeightInfo.remark;
        }
    }

    [Serializable]
    public class GINInfo
    {
        private Guid ginId;
        private Guid truckId;
        private string ginNo;
        private DateTime dateIssued;
        private bool signedByClient;
        private DateTime dateApproved;
        private Guid approvedBy;
		private Guid truckCheckedOutBy;
        private DateTime truckCheckedOutOn;
        private decimal quantity;
        private decimal netWeight;
        private decimal grossWeight;
        private int status;

        public GINInfo() { }
        public GINInfo(
            Guid ginId,
            Guid truckId,
            string ginNo,
            DateTime dateIssued,
            bool signedByClient,
            DateTime dateApproved,
            Guid approvedBy,
		    Guid truckCheckedOutBy,
            DateTime truckCheckedOutOn,
            decimal quantity,
            decimal netWeight,
            decimal grossWeight,
            int status)
        {
            this.ginId = ginId;
            this.truckId = truckId;
            this.ginNo = ginNo;
            this.dateIssued = dateIssued;
            this.signedByClient = signedByClient;
            this.dateApproved = dateApproved;
            this.approvedBy = approvedBy;
            this.truckCheckedOutBy = truckCheckedOutBy;
            this.truckCheckedOutOn = truckCheckedOutOn;
            this.quantity = quantity;
            this.netWeight = netWeight;
            this.grossWeight = grossWeight;
            this.status = status;
        }

        [XmlAttribute]
        public Guid GINId
        {
            get { return ginId; }
            set { ginId = value; }
        }

        [XmlAttribute]
        public Guid TruckId
        {
            get { return truckId; }
            set { truckId = value; }
        }

        [XmlAttribute]
        public string GINNo
        {
            get { return ginNo; }
            set { ginNo = value; }
        }

        [XmlAttribute]
        public DateTime DateIssued
        {
            get { return dateIssued; }
            set { dateIssued = value; }
        }

        [XmlAttribute]
        public bool SignedByClient
        {
            get { return signedByClient; }
            set { signedByClient = value; }
        }

        [XmlAttribute]
        public DateTime DateApproved
        {
            get { return dateApproved; }
            set { dateApproved = value; }
        }

        [XmlAttribute]
        public Guid ApprovedBy
        {
            get 
            {
                if (status == (int)GINStatusType.GINSigned)
                {
                    //return new Guid(SystemLookup.LookupSource.GetLookup("CurrentWarehouse")["WarehouseManagerId"]);
                    approvedBy= UserBLL.GetCurrentUser();
                }
                return approvedBy; 
            }
            set { approvedBy = value; }
        }

        [XmlAttribute]
        public Guid TruckCheckedOutBy
        {
            get { return truckCheckedOutBy; }
            set { truckCheckedOutBy = value; }
        }

        [XmlAttribute]
        public DateTime TruckCheckedOutOn
        {
            get { return truckCheckedOutOn; }
            set { truckCheckedOutOn = value; }
        }

        [XmlAttribute]
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [XmlAttribute]
        public decimal GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }

        [XmlAttribute]
        public decimal NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public void Copy(GINInfo ginInfo)
        {
            this.ginId = ginInfo.ginId;
            this.truckId = ginInfo.truckId;
            this.ginNo = ginInfo.ginNo;
            this.dateIssued = ginInfo.dateIssued;
            this.signedByClient = ginInfo.signedByClient;
            this.dateApproved = ginInfo.dateApproved;
            this.approvedBy = ginInfo.approvedBy;
            this.truckCheckedOutBy = ginInfo.truckCheckedOutBy;
            this.truckCheckedOutOn = ginInfo.truckCheckedOutOn;
            this.quantity = ginInfo.quantity;
            this.grossWeight = ginInfo.grossWeight;
            this.netWeight = ginInfo.netWeight;
            this.status = ginInfo.status;
        }
    }

    [Serializable]
    public class SampleInfo
    {
        private Guid id;
        private Guid ginProcessId;
        private int serialNo;
        private DateTime generatedDate;
        private int samplingCode;
        private string remark;
        private int status;
        private DateTime receivedOn;
        private List<WorkerInformation> samplers = new List<WorkerInformation>();
        private List<SamplingResultInfo> samplingResults = new List<SamplingResultInfo>();

        public SampleInfo() { }
        public SampleInfo(
            Guid id,
            Guid ginProcessId,
            int serialNo,
            DateTime generatedDate,
            int samplingCode,
            string remark,
            int status,
            DateTime receivedOn
            )
        {
            this.id = id;
            this.ginProcessId = ginProcessId;
            this.serialNo = serialNo;
            this.generatedDate = generatedDate;
            this.samplingCode = samplingCode;
            this.remark = remark;
            this.status = status;
            this.receivedOn = receivedOn;
        }

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [XmlAttribute]
        public Guid GINProcessId
        {
            get { return ginProcessId; }
            set { ginProcessId = value; }
        }

        [XmlAttribute]
        public int SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        [XmlAttribute]
        public DateTime GeneratedDate
        {
            get { return generatedDate; }
            set { generatedDate = value; }
        }

        [XmlAttribute]
        public int SamplingCode
        {
            get { return samplingCode; }
            set { samplingCode = value; }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlAttribute]
        public DateTime ReceivedOn
        {
            get { return receivedOn; }
            set { receivedOn = value; }
        }

        [XmlArray("Samplers")]
        [XmlArrayItem("Sampler")]
        public List<WorkerInformation> Samplers
        {
            get { return samplers; }
        }

        [XmlArray("SamplingResults")]
        [XmlArrayItem("SamplingResult")]
        public List<SamplingResultInfo> SamplingResults
        {
            get { return samplingResults; }
        }

        public void Copy(SampleInfo sampleInfo)
        {
            this.id = sampleInfo.id;
            this.ginProcessId = sampleInfo.ginProcessId;
            this.serialNo = sampleInfo.serialNo;
            this.generatedDate = sampleInfo.generatedDate;
            this.samplingCode = sampleInfo.samplingCode;
            this.remark = sampleInfo.remark;
            this.status = sampleInfo.status;
            this.receivedOn = sampleInfo.receivedOn;
        }
    }

    [Serializable]
    public class SamplingResultInfo
    {
        private Guid id;
        private Guid sampleId;
        private Guid samplerId;
        private int bags;
        private int separations;
        private string comment;
        private int status;
        private string remark;

        public SamplingResultInfo() { }
        public SamplingResultInfo(
            Guid id,
            Guid sampleId,
            Guid samplerId,
            int bags,
            int separations,
            string comment,
            int status,
            string remark
           )
        {
            this.id = id;
            this.sampleId = sampleId;
            this.samplerId = samplerId;
            this.bags = bags;
            this.separations = separations;
            this.comment = comment;
            this.status = status;
            this.remark = remark;
        }

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [XmlAttribute]
        public Guid SampleId
        {
            get { return sampleId; }
            set { sampleId = value; }
        }

        [XmlAttribute]
        public Guid SamplerId
        {
            get { return samplerId; }
            set { samplerId = value; }
        }

        [XmlAttribute]
        public int Bags
        {
            get { return bags; }
            set { bags = value; }
        }

        [XmlAttribute]
        public int Separations
        {
            get { return separations; }
            set { separations = value; }
        }

        [XmlAttribute]
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public void Copy(SamplingResultInfo samplingResultInfo)
        {
            this.id = samplingResultInfo.id;
            this.sampleId = samplingResultInfo.sampleId;
            this.bags = samplingResultInfo.bags;
            this.separations = samplingResultInfo.separations;
            this.comment = samplingResultInfo.comment;
            this.status = samplingResultInfo.status;
            this.remark = samplingResultInfo.remark;
        }
    }

    [Serializable]
    public class GradingInfo
    {
        private Guid id;
        private Guid ginProcessId;
        private Guid samplingId;
        private string gradingCode;
        private DateTime dateCoded;
        private int status;
        private List<GradingResultInfo> gradingResults = new List<GradingResultInfo>();

        public GradingInfo() { }

        public GradingInfo(
            Guid id,
            Guid ginProcessId,
            Guid samplingId,
            string gradingCode,
            DateTime dateCoded,
            int status
           )
        {
            this.id = id;
            this.ginProcessId = ginProcessId;
            this.samplingId = samplingId;
            this.gradingCode = gradingCode;
            this.dateCoded = dateCoded;
            this.status = status;
        }

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [XmlAttribute]
        public Guid GINProcessId
        {
            get { return ginProcessId; }
            set { ginProcessId = value; }
        }

        [XmlAttribute]
        public Guid SamplingId
        {
            get { return samplingId; }
            set { samplingId = value; }
        }

        [XmlAttribute]
        public string GradingCode
        {
            get { return gradingCode; }
            set { gradingCode = value; }
        }

        [XmlAttribute]
        public DateTime DateCoded
        {
            get { return dateCoded; }
            set { dateCoded = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlArray("GradingResults")]
        [XmlArrayItem("GradingResult")]
        public List<GradingResultInfo> GradingResults
        {
            get { return gradingResults; }
        }

        public void Copy(GradingInfo gradingInfo)
        {
            this.id = gradingInfo.id;
            this.ginProcessId = gradingInfo.ginProcessId;
            this.samplingId = gradingInfo.samplingId;
            this.gradingCode = gradingInfo.gradingCode;
            this.dateCoded = gradingInfo.dateCoded;
            this.status = gradingInfo.status;
        }
    }

    [Serializable]
    public class GradingResultInfo
    {
        private Guid id;
        private Guid gradingId;
        private int status;
        private string remark;
        private DateTime receiptTimeStamp;
        private DateTime clientAcceptanceTimeStamp;
        private WorkerInformation grader;
        private List<GradingResultDetailInfo> resultDetails = new List<GradingResultDetailInfo>();

        public GradingResultInfo() { }

        public GradingResultInfo(
            Guid id,
            Guid gradingId,
            int status,
            string remark,
            DateTime receiptTimeStamp,
            DateTime clientAcceptanceTimeStamp
           )
        {
            this.id = id;
            this.gradingId = gradingId;
            this.status = status;
            this.remark = remark;
            this.receiptTimeStamp = receiptTimeStamp;
            this.clientAcceptanceTimeStamp = clientAcceptanceTimeStamp;
        }

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [XmlAttribute]
        public Guid GradingId
        {
            get { return gradingId; }
            set { gradingId = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        [XmlAttribute]
        public DateTime ReceiptTimeStamp
        {
            get { return receiptTimeStamp; }
            set { receiptTimeStamp = value; }
        }

        [XmlAttribute]
        public DateTime ClientAcceptanceTimeStamp
        {
            get { return clientAcceptanceTimeStamp; }
            set { clientAcceptanceTimeStamp = value; }
        }

        [XmlElement]
        public WorkerInformation Grader
        {
            get { return grader; }
            set { grader = value; }
        }

        [XmlArray("ResultDetails")]
        [XmlArrayItem("ResultDetail")]
        public List<GradingResultDetailInfo> ResultDetails
        {
            get { return resultDetails; }
        }

        public void Copy(GradingResultInfo gradingResultInfo)
        {
            this.id = gradingResultInfo.id;
            this.gradingId = gradingResultInfo.gradingId;
            this.status = gradingResultInfo.status;
            this.remark = gradingResultInfo.remark;
            this.receiptTimeStamp = gradingResultInfo.receiptTimeStamp;
            this.clientAcceptanceTimeStamp = gradingResultInfo.clientAcceptanceTimeStamp;
        }
    }

    [Serializable]
    public class GradingResultDetailInfo
    {
        private Guid id;
        private Guid gradingResultId;
        private Guid gradingFactorId;
        private string gradeValue;
        private int status;

        public GradingResultDetailInfo() { }

        public GradingResultDetailInfo(
            Guid id,
            Guid gradingResultId,
            Guid gradingFactorId,
            string gradeValue,
            int status
            )
        {
            this.id = id;
            this.gradingResultId = gradingResultId;
            this.gradingFactorId = gradingFactorId;
            this.gradeValue = gradeValue;
            this.status = status;
        }

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [XmlAttribute]
        public Guid GradingResultId
        {
            get { return gradingResultId; }
            set { gradingResultId = value; }
        }

        [XmlAttribute]
        public Guid GradingFactorId
        {
            get { return gradingFactorId; }
            set { gradingFactorId = value; }
        }

        [XmlAttribute]
        public string GradeValue
        {
            get { return gradeValue; }
            set { gradeValue = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public void Copy(GradingResultDetailInfo resultDetailInfo)
        {
            this.id = resultDetailInfo.id;
            this.gradingResultId = resultDetailInfo.gradingResultId;
            this.gradingFactorId = resultDetailInfo.gradingFactorId;
            this.gradeValue = resultDetailInfo.gradeValue;
            this.status = resultDetailInfo.status;
        }
    }

    [Serializable]
    public class GINReportInfo
    {
        private Guid ginId;//from GINProcessInfo
        private string ginNo;//from GINProcessInfo
        private DateTime dateIssued;//from GINInfo
        private int status;//from GINInfo
        //---------------------------------------------------------//
        private Guid warehouseId;
        private Guid clientId;//from GINProcessInfo
        private Guid pickupNoticeAgentId;//from GINProcessInfo
        private string punaIdNo;
        private int punaNIDType;
        private bool pickupNoticeAgentVerified;//from GINProcessInfo
        private bool signedByClient;//from GINInfo
        //---------------------------------------------------------//
        private Guid commodityGradeId;//from GINProcessInfo
        private decimal quantity;//calculate (in lots)? from truck loading (scaling)
        private decimal weight;//from TruckWeightInfo
        private string scaleTicketNo;//from TruckWeightInfo
        private Guid bagType;//from GINProcessInfo
        private int bags;//from TruckLoadingInfo
        //---------------------------------------------------------//
        private Guid sampler;//from SampleInfo.Samplers[isSupervisor]
        private Guid grader;//differ until grading is implemented
        private Guid loader;//from TruckLoadingInfo.Loaders[isSupervisor]
        private Guid weigher;//from TruckScalingInfo.Weighers[isSupervisor]
        //---------------------------------------------------------//
        private string driverName;//from GINTruckInfo
        private string licenseNo;//from GINTruckInfo
        private string issuedBy;//from GINTruckInfo
        private string plateNo;//from GINTruckInfo
        private string trailerNo;

        public GINReportInfo() { }
        public GINReportInfo(
            Guid ginId,
            string ginNo,
            Guid warehouseId,
            Guid clientId,
            Guid pickupNoticeAgentId,
            string punaIdNo,
            int punaNIDType,
            bool pickupNoticeAgentVerified,
            bool signedByClient,
            Guid commodityGradeId,
            decimal quantity,
            decimal weight,
            string scaleTicketNo,
            Guid bagType,
            int bags,
            Guid sampler,
            Guid grader,
            Guid loader,
            Guid weigher,
            string driverName,
            string licenseNo,
            string issuedBy,
            string plateNo,
            string trailerNo,
            DateTime dateIssued,
            int status
            )
        {
            this.ginId = ginId;
            this.ginNo = ginNo;
            this.warehouseId = warehouseId;
            this.clientId = clientId;
            this.pickupNoticeAgentId = pickupNoticeAgentId;
            this.punaIdNo = punaIdNo;
            this.punaNIDType = punaNIDType;
            this.pickupNoticeAgentVerified = pickupNoticeAgentVerified;
            this.signedByClient = signedByClient;
            this.commodityGradeId = commodityGradeId;
            this.quantity = quantity;
            this.weight = weight;
            this.scaleTicketNo = scaleTicketNo;
            this.bagType = bagType;
            this.bags = bags;
            this.sampler = sampler;
            this.grader = grader;
            this.loader = loader;
            this.weigher = weigher;
            this.driverName = driverName;
            this.licenseNo = licenseNo;
            this.issuedBy = issuedBy;
            this.plateNo = plateNo;
            this.trailerNo = trailerNo;
            this.dateIssued = dateIssued;
            this.status = status;
        }

        [XmlAttribute]
        public Guid GINId
        {
            get { return ginId; }
            set { ginId = value; }
        }

        [XmlAttribute]
        public string GINNo
        {
            get { return ginNo; }
            set { ginNo = value; }
        }

        [XmlAttribute]
        public Guid WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        [XmlAttribute]
        public Guid ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        [XmlAttribute]
        public Guid PickupNoticeAgentId
        {
            get { return pickupNoticeAgentId; }
            set { pickupNoticeAgentId = value; }
        }

        [XmlIgnore]
        public string PUNAIdNo
        {
            get { return punaIdNo; }
            set { punaIdNo = value; }
        }

        [XmlIgnore]
        public int PUNANIDType
        {
            get { return punaNIDType; }
            set { punaNIDType = value; }
        }

        [XmlAttribute]
        public bool PickupNoticeAgentVerified
        {
            get { return pickupNoticeAgentVerified; }
            set { pickupNoticeAgentVerified = value; }
        }

        [XmlAttribute]
        public bool SignedByClient
        {
            get { return signedByClient; }
            set { signedByClient = value; }
        }

        [XmlAttribute]
        public Guid CommodityGradeId
        {
            get { return commodityGradeId; }
            set { commodityGradeId = value; }
        }

        [XmlAttribute]
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [XmlAttribute]
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        [XmlAttribute]
        public string ScaleTicketNo
        {
            get { return scaleTicketNo; }
            set { scaleTicketNo = value; }
        }

        [XmlAttribute]
        public Guid BagType
        {
            get { return bagType; }
            set { bagType = value; }
        }

        [XmlAttribute]
        public int Bags
        {
            get { return bags; }
            set { bags = value; }
        }

        [XmlAttribute]
        public Guid Sampler
        {
            get { return sampler; }
            set { sampler = value; }
        }

        [XmlAttribute]
        public Guid Grader
        {
            get { return grader; }
            set { grader = value; }
        }

        [XmlAttribute]
        public Guid Loader
        {
            get { return loader; }
            set { loader = value; }
        }

        [XmlAttribute]
        public Guid Weigher
        {
            get { return weigher; }
            set { weigher = value; }
        }

        [XmlAttribute]
        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        [XmlAttribute]
        public string LicenseNo
        {
            get { return licenseNo; }
            set { licenseNo = value; }
        }

        [XmlAttribute]
        public string IssuedBy
        {
            get { return issuedBy; }
            set { issuedBy = value; }
        }

        [XmlAttribute]
        public string PlateNo
        {
            get { return plateNo; }
            set { plateNo = value; }
        }

        [XmlAttribute]
        public string TrailerNo
        {
            get { return trailerNo; }
            set { trailerNo = value; }
        }

        [XmlAttribute]
        public DateTime DateIssued
        {
            get { return dateIssued; }
            set { dateIssued = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

    }

    public enum WorkType
    {
        GINSampling = 0,
        GINGrading = 1,
        GINLoading = 2,
        GINScaling = 3
    }

    [Serializable]
    public class WorkerKey
    {
        private Guid id;
        private Guid warehouseId;

        public WorkerKey(Guid id, Guid warehouseId)
        {
            this.id = id;
            this.warehouseId = warehouseId;
        }

        public WorkerKey() { }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public Guid WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        public override bool Equals(object obj)
        {
            WorkerKey keyObj = (WorkerKey)obj;
            return (keyObj.id == id) && (keyObj.WarehouseId == warehouseId);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }

    [Serializable]
    public class WorkerInformation
    {
        private Guid id;
        private Guid workerId;
        private Guid workId;
        private WorkType workType;
        private Guid employeeRoleId;
        private bool isSupervisor;
        private int status;
        private string remark = "";

        public WorkerInformation() { }
        public WorkerInformation(
            Guid id,
            Guid workerId,
            Guid workId,
            WorkType workType,
            Guid employeeRoleId,
            bool isSupervisor,
            int status,
            string remark)
        {
            this.id = id;
            this.workerId = workerId;
            this.workId = workId;
            this.workType = workType;
            this.employeeRoleId = employeeRoleId;
            this.isSupervisor = isSupervisor;
            this.status = status;
            this.remark = remark;
        }

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [XmlAttribute]
        public Guid WorkerId
        {
            get { return workerId; }
            set { workerId = value; }
        }

        [XmlAttribute]
        public Guid WorkId
        {
            get { return workId; }
            set { workId = value; }
        }

        [XmlAttribute]
        public WorkType WorkType
        {
            get { return workType; }
            set { workType = value; }
        }

        [XmlAttribute]
        public Guid EmployeeRoleId
        {
            get { return employeeRoleId; }
            set { employeeRoleId = value; }
        }

        [XmlAttribute]
        public bool IsSupervisor
        {
            get { return isSupervisor; }
            set { isSupervisor = value; }
        }

        [XmlAttribute]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlAttribute]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        [XmlIgnore]
        public string WorkTypeToDb
        {
            get { return Enum.GetName(typeof(WorkType), WorkType); }
        }

        public void Copy(WorkerInformation workerInfo)
        {
            this.id = workerInfo.id;
            this.workerId = workerInfo.workerId;
            this.workId = workerInfo.workId;
            this.workType = workerInfo.workType;
            this.employeeRoleId = workerInfo.employeeRoleId;
            this.isSupervisor = workerInfo.isSupervisor;
            this.status = workerInfo.status;
            this.remark = workerInfo.remark;
        }
    }

    public class TrackingReportData
    {
        private string trackingId;
        private string client;
        private string commodity;
        private DateTime dateReceived;
        private string warehouse;
        private decimal quantity;
        private decimal weight;

        public TrackingReportData() { }
        public TrackingReportData(
            string trackingId,
            string client,
            string commodity,
            DateTime dateReceived,
            string warehouse,
            decimal quantity,
            decimal weight)
        {
            this.trackingId = trackingId;
            this.client = client;
            this.commodity = commodity;
            this.dateReceived = dateReceived;
            this.warehouse = warehouse;
            this.quantity = quantity;
            this.weight = weight;
        }

        public string TrackingId
        {
            get { return trackingId; }
            set { trackingId = value; }
        }

        public string Client
        {
            get { return client; }
            set { client = value; }
        }

        public string Commodity
        {
            get { return commodity; }
            set { commodity = value; }
        }

        public DateTime DateReceived
        {
            get { return dateReceived; }
            set { dateReceived = value; }
        }

        public string Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }
    }

    public class GINTrackingReportData : TrackingReportData
    {
        public string PlateNo { get; set; }
        public string DriverName { get; set; }
    }

    public class GINReportData
    {
        private string ginNo;//from GINProcessInfo
        private DateTime dateIssued;//from GINInfo
        private string status;//from GINInfo
        //---------------------------------------------------------//
        private string warehouse;//from GINProcessInfo
        private string clientName;//from GINProcessInfo
        private string clientId;//from GINProcessInfo
        private string punAgent;//from GINProcessInfo
        private string punaIdNo;
        private string nidType;
        //---------------------------------------------------------//
        private string commodityGrade;//from GINProcessInfo
        private decimal quantity;//calculate (in lots)? from truck loading (scaling)
        private decimal weight;//from TruckWeightInfo
        private string scaleTicketNo;//from TruckWeightInfo
        private string bagType;//from GINProcessInfo
        private int bags;//from TruckLoadingInfo
        //---------------------------------------------------------//
        private string sampler;//from SampleInfo.Samplers[isSupervisor]
        private string grader;//differ until grading is implemented
        //---------------------------------------------------------//
        private string driverName;//from GINTruckInfo
        private string licenseNo;//from GINTruckInfo
        private string issuedBy;//from GINTruckInfo
        private string plateNo;//from GINTruckInfo
        private bool sampled;

        public GINReportData() { }
        public GINReportData(
            string ginNo,
            DateTime dateIssued,
            string warehouse,
            string clientName,
            string clientId,
            string punAgent,
            string punaIdNo,
            string nidType,
            string commodityGrade,
            decimal quantity,
            decimal weight,
            string scaleTicketNo,
            string bagType,
            int bags,
            bool sampled,
            string sampler,
            string grader,
            string driverName,
            string licenseNo,
            string issuedBy,
            string plateNo,
            string status
            )
        {
            this.ginNo = ginNo;
            this.warehouse = warehouse;
            this.clientName = clientName;
            this.clientId = clientId;
            this.punAgent = punAgent;
            this.punaIdNo = punaIdNo;
            this.nidType = nidType;
            this.commodityGrade = commodityGrade;
            this.quantity = quantity;
            this.weight = weight;
            this.scaleTicketNo = scaleTicketNo;
            this.bagType = bagType;
            this.bags = bags;
            this.sampled = sampled;
            this.sampler = sampler;
            this.grader = grader;
            this.driverName = driverName;
            this.licenseNo = licenseNo;
            this.issuedBy = issuedBy;
            this.plateNo = plateNo;
            this.dateIssued = dateIssued;
            this.status = status;
        }

        public string GINNo
        {
            get { return ginNo; }
            set { ginNo = value; }
        }

        public string Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        public string ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        public string PUNAgent
        {
            get { return punAgent; }
            set { punAgent = value; }
        }

        public string PUNAIdNo
        {
            get { return punaIdNo; }
            set { punaIdNo = value; }
        }

        public string NIDType
        {
            get { return nidType; }
            set { nidType = value; }
        }

        public string CommodityGrade
        {
            get { return commodityGrade; }
            set { commodityGrade = value; }
        }

        public decimal Quantity
        {
            get { return Math.Truncate(quantity*10000 +0.9M)/10000; }
            set { quantity = value; }
        }

        public decimal Weight
        {
            get { return Math.Truncate(weight*100 + 0.9M)/100; }
            set { weight = value; }
        }

        public string ScaleTicketNo
        {
            get { return scaleTicketNo; }
            set { scaleTicketNo = value; }
        }

        public string BagType
        {
            get { return bagType; }
            set { bagType = value; }
        }

        public int Bags
        {
            get { return bags; }
            set { bags = value; }
        }

        public bool Sampled
        {
            get { return sampled; }
            set { sampled = false; }
        }

        public string Sampler
        {
            get { return sampler; }
            set { sampler = value; }
        }

        public string Grader
        {
            get { return grader; }
            set { grader = value; }
        }

        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        public string LicenseNo
        {
            get { return licenseNo; }
            set { licenseNo = value; }
        }

        public string IssuedBy
        {
            get { return issuedBy; }
            set { issuedBy = value; }
        }

        public string PlateNo
        {
            get { return plateNo; }
            set { plateNo = value; }
        }

        public DateTime DateIssued
        {
            get { return dateIssued; }
            set { dateIssued = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }

    public class PUNReportData
    {
        private List<WRReportData> wrs = new List<WRReportData>();

        public Guid PickupNoticeId {get;set;}
        public string Member { get; set; }
        public string MemberId { get; set; }
        public string RepId { get; set; }
        public string Rep { get; set; }
        public string Client { get; set; }
        public string ClientId { get; set; }
        public string AgentName { get; set; }
        public string NIDType { get; set; }
        public string NIDNumber { get; set; }
        public string AgentTel { get; set; }
        public string Status { get; set; }

        public DateTime ExpectedPickupDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public DateTime DateApproved { get; set; }

        public List<WRReportData> WRs { get { return wrs; } }
    }

    public class WRReportData
    {
        public Guid PickupNoticeId { get; set; }
        public int WHR { get; set; }
        public string CommodityGrade { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetWeight { get; set; }
        public string GRNNo { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public class GINEditingRequest
    {
        [XmlAttribute]
        public Guid DeliveryReceivedId { get; set; }
        [XmlAttribute]
        public string TransactionId { get; set; }
        [XmlAttribute]
        public string TargetPage { get; set; }
        [XmlText]
        public string ProposedChange { get; set; }
        [XmlAttribute]
        public string OldTransactionId { get; set; }
    }
}