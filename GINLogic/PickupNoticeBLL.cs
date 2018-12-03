using System;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Data;
using WarehouseApplication.DALManager;
using System.Xml.Serialization;
using System.Data.SqlClient;

namespace WarehouseApplication.GINLogic
{
    public class PickupNoticeBLL : IPickupNotice
    {
        #region Static Members
        public static List<IDataIdentifier> SearchPickupNotice(IDataFilter filter)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PickupNotice").GetCatalog(filter);
        }

        public static List<IDataIdentifier> SearchPickupNotice()
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PickupNotice").GetCatalog();
        }

        public static IDataFilter GetCatalogFilter()
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PickupNotice").GetCatalogFilter();
        }

        public static IDataFilter GetCatalogFilter(string templateName)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PickupNotice").GetCatalogFilter(templateName);
        }

        public static ILookupSource StaticLookupSource
        {
            get { return new PickupNoticeLookupSource(); }
        }

        public static void CachePickupNotices()
        {
            List<Guid> cachedPunIds = new List<Guid>();
            ECXCD.WR wr = new WarehouseApplication.ECXCD.WR();
            List<ECXCD.CPickUpNotice> importedPuns = new List<WarehouseApplication.ECXCD.CPickUpNotice>(wr.GetPun());
            int importedPunsCount = importedPuns.Count;
            if (importedPunsCount == 0)
            {
                return;
            }
            List<PickupNoticeInformation> cachedPuns = new List<PickupNoticeInformation>(
                from ipun in importedPuns
                where ipun.WarehouseId == new Guid(StaticLookupSource.GetLookup("CurrentWarehouse")["Id"])
                select new PickupNoticeInformation()
                {
                    ClientId = ipun.ClientId,
                    MemberId = ipun.MemberId,
                    RepId = ipun.RepId,
                    CommodityGradeId = ipun.CommodityGradeId,
                    ProductionYear = ipun.ProductionYear,
                    ExpectedPickupDate = ipun.ExpectedPickupDate,
                    ExpirationDate = ipun.ExpirationDate,
                    PickupNoticeId = ipun.PickupNoticeId,
                    WarehouseId = ipun.WarehouseId,
                    Quantity = ipun.Quantity,
                    Weight = ipun.Weight,
                    Status = 0
                });
            int cachedPunsCount = cachedPuns.Count;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            cachedPuns.ForEach(
                delegate(PickupNoticeInformation pun)
                {
                    pun.PickupNoticeAgents.AddRange(
                    from ipun in importedPuns
                     where ipun.PickupNoticeId == pun.PickupNoticeId
                     from ipuna in ipun.PickupNoticeAgents
                     select new PickupNoticeInformation.PickupNoticeAgentInformation()
                     {
                         AgentName = ipuna.AgentName,
                         AgentTel = ipuna.AgentTel,
                         Id = Guid.NewGuid(),
                         NIDNumber = ipuna.NIDNumber,
                         NIDType = ipuna.NIDType,
                         PickupNoticeId = ipuna.PickupNoticeId,
                         Status = ipuna.Status
                     });
                    pun.WarehouseReceipts.AddRange(
                        from ipun in importedPuns
                        where ipun.PickupNoticeId == pun.PickupNoticeId
                        from iwr in ipun.WarehouseReciepts
                        select new PickupNoticeInformation.WarehouseReceiptInformation()
                        {
                            Bags = iwr.Bags,
                            BagType = iwr.BagType,
                            GRNID = iwr.GRNID,
                            GRNNo = iwr.GRNNo,
                            Id = iwr.Id,
                            PickupNoticeId = iwr.PickupNoticeId,
                            Quantity = Convert.ToDecimal(iwr.Quantity),
                            ShedId = iwr.ShedId,
                            WarehouseReceiptId = iwr.WarehouseRecieptId,
                            Weight = Convert.ToDecimal(iwr.Weight)
                        });

                    Queue<object[]> queue = new Queue<object[]>();
                    queue.Enqueue(new object[] { pun, "spImportPUN" });

                    pun.PickupNoticeAgents.ForEach(puna => queue.Enqueue(new object[] { puna, "spImportPUNAgent" }));
                    pun.WarehouseReceipts.ForEach(wrct => queue.Enqueue(new object[] { wrct, "spImportWarehouseReceipt" }));

                    SqlTransaction transaction = null;
                    try
                    {
                        transaction = PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(queue, contextParameters);
                        transaction.Commit();
                        cachedPunIds.Add(pun.PickupNoticeId);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (transaction != null)
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                });
            wr.AcknowledgePuns(cachedPunIds.ToArray());
           
        }
        #endregion

        #region Member Variables
        private PickupNoticeInformation punInformation;
        private ILookupSource lookupSource;
        #endregion

        #region Constructors
        public PickupNoticeBLL() { }

        public PickupNoticeBLL(Guid pickupNoticeId)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("PickupNoticeId", pickupNoticeId);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(PickupNoticeInformation));
                XmlDocument document = PersistenceTransactionFactory.CreatePersistenceTransaction().Open(
                    "spOpenPickupNotice", contextParameters);
                StringBuilder sb = new StringBuilder();
                TextWriter writer = new StringWriter(sb);
                document.Save(writer);

                punInformation = (PickupNoticeInformation)s.Deserialize(new StringReader(sb.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception("The database failed to load the requested Pickup Notice ", ex);
            }
            lookupSource = new PickupNoticeLookupSource(punInformation);
        }
        #endregion

        #region IPickupNotice Implementation
        public ILookupSource PUNLookupSource
        {
            get { return lookupSource; }
        }
        public PickupNoticeInformation PickupNoticeInformation
        {
            get { return punInformation; }
            set
            {
                punInformation = value;
                lookupSource = new PickupNoticeLookupSource(punInformation);
            }
        }

        public SqlTransaction AcknowledgePickupNotice(PUNAcknowledgementInformation acknowledgement)
        {
            if (!acknowledgement.PickupNoticeAgentVerified)
            {
                throw new Exception("Please confirm pickup notice agent has been verified.");
            }
            if (acknowledgement.ClientId == Guid.Empty)
            {
                throw new Exception("This pickup notice is issued to unknown client and couldnot be processed.");
            }
            if (acknowledgement.CommodityGradeId == Guid.Empty)
            {
                throw new Exception("The commodity grade in this pickup notice is unknown.");
            }
            acknowledgement.PledgedWeight = acknowledgement.Weight;
            PickupNoticeInformation.Status = (int)PUNStatusType.BeingIssued;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] {PickupNoticeInformation, acknowledgement }), contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your acknowledgement couldn't be saved to the database");
            }
        }

        public SqlTransaction AcknowledgePickupNotice(PUNAcknowledgementInformation acknowledgement, SqlTransaction transaction)
        {
            if (!acknowledgement.PickupNoticeAgentVerified)
            {
                throw new Exception("Please confirm pickup notice agent has been verified.");
            }
            if (acknowledgement.ClientId == Guid.Empty)
            {
                throw new Exception("This pickup notice is issued to unknown client and couldnot be processed.");
            }
            if (acknowledgement.CommodityGradeId == Guid.Empty)
            {
                throw new Exception("The commodity grade in this pickup notice is unknown.");
            }
            PickupNoticeInformation.Status = (int)PUNStatusType.BeingIssued;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { PickupNoticeInformation, acknowledgement }), 
                    contextParameters,
                    transaction);
            }
            catch (Exception)
            {
                try
                {
                    //transaction.Rollback();
                }
                catch (Exception)
                {
                }
                throw new Exception("Your acknowledgement couldn't be saved to the database");
            }
        }

        public void GINIssued(SqlTransaction transaction)
        {
            PickupNoticeInformation.Status = (int)PUNStatusType.Closed;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { PickupNoticeInformation }), contextParameters, transaction);
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                }
                throw new Exception("The completion of the PUN issuance couldn't be saved to the database");
            }
        }

        public void Aborted(SqlTransaction transaction)
        {
            PickupNoticeInformation.Status = (int)PUNStatusType.Aborted;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { PickupNoticeInformation }), contextParameters, transaction);
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                }

                throw new Exception("The PUN abortion couldn't be saved to the database");
            }
        }

        public SqlTransaction Aborted()
        {
            PickupNoticeInformation.Status = (int)PUNStatusType.Aborted;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { PickupNoticeInformation }), contextParameters);
            }
            catch (Exception)
            {
                throw new Exception("The PUN abortion couldn't be saved to the database");
            }
        }

        public ILookupSource LookupSource
        {
            get { return lookupSource; }
        }

        public PUNAcknowledgementInformation PUNAInformation
        {
            get
            {
                return new PUNAcknowledgementInformation(
                    Guid.NewGuid(),
                    DateTime.Today,
                    punInformation.PickupNoticeId,
                    punInformation.ClientId,
                    punInformation.MemberId,
                    punInformation.RepId,
                    punInformation.CommodityGradeId,
                    punInformation.ProductionYear,
                    punInformation.PickupNoticeAgents[0].Id,
                    (Guid)lookupSource.GetLookup("VerifyingClerk").Keys.ElementAt(0),
                    false,
                    (Guid)lookupSource.GetLookup("InventoryController").Keys.ElementAt(0),
                    false,
                    DateTime.Today,
                    0,
                    0,
                    0,
                    0,
                    false,
                    punInformation.Weight,
                    punInformation.Quantity,
                    0,
                    0,
                    punInformation.PickupNoticeAgents[0].AgentName,
                    string.Empty,
                    0);
            }
        }

        public PUNReportData GetPUNReport(Guid punId)
        {
            PickupNoticeInformation.PickupNoticeAgentInformation punAgent = PickupNoticeInformation.PickupNoticeAgents[0];
            BLL.CommodityGradeBLL commodityGrade = BLL.CommodityGradeBLL.GetCommodityGrade(PickupNoticeInformation.CommodityGradeId);
            BLL.ClientBLL clientBLL = BLL.ClientBLL.GetClinet(PickupNoticeInformation.ClientId);
            Membership.MemberShipLookUp memberLookup = new WarehouseApplication.Membership.MemberShipLookUp();
            Membership.Member member = memberLookup.GetMember(PickupNoticeInformation.MemberId);
            Membership.Rep rep = memberLookup.GetRep(new Guid(PickupNoticeInformation.RepId));
            string status = ((PUNStatusType)PickupNoticeInformation.Status).ToString();
            PUNReportData reportData = new PUNReportData()
                {
                    AgentName = punAgent.AgentName,
                    AgentTel=punAgent.AgentTel,
                    Client=clientBLL.ClientName,
                    ClientId = clientBLL.ClientId,
                    ExpectedPickupDate=PickupNoticeInformation.ExpectedPickupDate,
                    ExpirationDate=PickupNoticeInformation.ExpirationDate,
                    Member = (member==null)?null:member.Name,
                    MemberId = (member == null) ? null : member.IdNo,
                    NIDNumber=punAgent.NIDNumber,
                    NIDType=SystemLookup.LookupSource.GetLookup("NIDType")[punAgent.NIDType],
                    PickupNoticeId=PickupNoticeInformation.PickupNoticeId,
                    Rep=rep.RepName,
                    RepId=rep.IDNO,
                    Status=status
                };
            foreach (PickupNoticeInformation.WarehouseReceiptInformation wr in PickupNoticeInformation.WarehouseReceipts)
            {
                reportData.WRs.Add(new WRReportData()
                {
                    CommodityGrade = commodityGrade.GradeName,
                    NetWeight = wr.Weight,
                    PickupNoticeId = wr.PickupNoticeId,
                    Quantity = wr.Quantity,
                    WHR = wr.WarehouseReceiptId,
                    GRNNo = wr.GRNNo
                });
            }
            return reportData;
        }
        #endregion

        #region Nested Classes
        private class PickupNoticeLookupSource : ILookupSource
        {
            private PickupNoticeInformation punInformation;

            public PickupNoticeLookupSource(PickupNoticeInformation punInformation)
            {
                this.punInformation = punInformation;
            }

            public PickupNoticeLookupSource()
            {
                punInformation = null;
            }

            #region ILookupSource Members

            public IDictionary<object, string> GetLookup(string lookupName)
            {
                Dictionary<object, string> lookup = new Dictionary<object, string>();
                if (lookupName == "Status")
                {
                    PUNStatusType[] punStatusValues = (PUNStatusType[])Enum.GetValues(typeof(PUNStatusType));
                    foreach (PUNStatusType punStatusValue in punStatusValues)
                    {
                        lookup.Add((int)punStatusValue, Enum.GetName(typeof(PUNStatusType), punStatusValue));
                    }
                }
                else if (lookupName == "PUNAgent")
                {
                    if (punInformation == null)
                    {
                        throw new Exception("This PUN Lookup may not be used in this context");
                    }
                    foreach (PickupNoticeInformation.PickupNoticeAgentInformation agent in punInformation.PickupNoticeAgents)
                    {
                        lookup.Add(agent.Id, agent.AgentName);
                    }
                }
                else if (lookupName == "NIDType")
                {
                    return SystemLookup.LookupSource.GetLookup("NIDType");
                }
                else if (lookupName == "CommodityGrade")
                {
                    return SystemLookup.LookupSource.GetLookup("CommodityGrade");
                }
                else if (lookupName == "VerifyingClerk")
                {
                    return SystemLookup.LookupSource.GetLookup("VerifyingClerk");
                }
                else if (lookupName == "InventoryController")
                {
                    return SystemLookup.LookupSource.GetLookup("InventoryController");
                }
                else if (lookupName == "BagType")
                {
                    return SystemLookup.LookupSource.GetLookup("BagType");
                }
                else if (lookupName == "Client")
                {
                    return SystemLookup.LookupSource.GetLookup("Client");
                }
                else if (lookupName == "CurrentWarehouse")
                {
                    return SystemLookup.LookupSource.GetLookup("CurrentWarehouse");
                }
                return lookup;
            }

            public IDictionary<string, object> GetInverseLookup(string lookupName)
            {
                IDictionary<string, object> lookup = new Dictionary<string, object>();
                if (lookupName == "Status")
                {
                    lookup.Add("[Select Status]", 5);
                    PUNStatusType[] punStatusValues = (PUNStatusType[])Enum.GetValues(typeof(PUNStatusType));
                    foreach (PUNStatusType punStatusValue in punStatusValues)
                    {
                        lookup.Add(Enum.GetName(typeof(PUNStatusType), punStatusValue), (int)punStatusValue);
                    }
                }
                return lookup;
            }
            #endregion

        }
        #endregion
    }

    public enum PUNStatusType
    {
        Open = 0,
        BeingIssued,
        Expired,
        Closed,
        Aborted
    }
}
