using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using WarehouseApplication.DALManager;

namespace WarehouseApplication.GINLogic
{
    public interface IInventoryServices
    {
        void CreatePhysicalCount(PhysicalCountInfo physicalCount);
        void AddStackPhysicalCount(PhysicalCountInfo physicalCount, StackPhysicalCountInfo stackPhysicalCount);
        void AddInspector(PhysicalCountInfo physicalCount, PhysicalCountInspectorInfo inspector);
        PhysicalCountInfo OpenPhysicalCount(Guid id);
        void SavePhysicalCount(PhysicalCountInfo physicalCount);
        StackPhysicalCountInfo GetBlankStackCount(PhysicalCountInfo physicalCount);
        PhysicalCountInspectorInfo GetBlankInspector(PhysicalCountInfo physicalCount);
        SqlTransaction UnloadToStack(Guid stackId, int bags, float weight);
        SqlTransaction UnloadToStack(Guid stackId, int bags, float weight, SqlTransaction transction);
        SqlTransaction LoadFromStack(Guid stackId, int bags, float weight);
        SqlTransaction LoadFromStack(Guid stackId, int bags, float weight, SqlTransaction transction);
        ILookupSource LookupSource { get; }
    }

    [XmlRoot(ElementName = "StackInventoryStatusInfo")]
    public class StackInventoryStatusInfo
    {
        [XmlAttribute]
        public int ExpectedBalance { get; set; }
        [XmlAttribute]
        public int CummulatedShortage { get; set; }
        [XmlAttribute]
        public int CumulatedOverage { get; set; }
    }

    [XmlRoot(ElementName="PhysicalCount")]
    [Serializable]
    public class PhysicalCountInfo
    {
        private List<PhysicalCountInspectorInfo> inspectors=new List<PhysicalCountInspectorInfo>();
        private List<StackPhysicalCountInfo> stacks = new List<StackPhysicalCountInfo>();

        [XmlAttribute]
        public Guid Id { get; set; }
        [XmlAttribute]
        public Guid WarehouseId { get; set; }
        [XmlAttribute]
        public DateTime PhysicalCountDate { get; set; }
        [XmlAttribute]
        public bool IsBeginingCount { get; set; }
        [XmlArray(ElementName = "PhysicalCountInspectors")]
        [XmlArrayItem(ElementName = "PhysicalCountInspector")]
        public List<PhysicalCountInspectorInfo> Inspectors
        {
            get { return inspectors; }
        }
        [XmlArray(ElementName = "StackPysicalCounts")]
        [XmlArrayItem(ElementName = "StackPysicalCount")]
        public List<StackPhysicalCountInfo> Stacks
        {
            get { return stacks; }
        }

        public void Copy(PhysicalCountInfo count)
        {
            Id = count.Id;
            WarehouseId = count.WarehouseId;
            PhysicalCountDate = count.PhysicalCountDate;
            IsBeginingCount = count.IsBeginingCount;
        }
    }

    [Serializable]
    public class PhysicalCountInspectorInfo
    {
        [XmlAttribute]
        public Guid Id { get; set; }
        [XmlAttribute]
        public Guid PhysicalCountId { get; set; }
        [XmlAttribute]
        public Guid UserId { get; set; }
        [XmlAttribute]
        public bool IsSupervisor { get; set; }

        public void Copy(PhysicalCountInspectorInfo inspector)
        {
            Id = inspector.Id;
            PhysicalCountId = inspector.PhysicalCountId;
            UserId = inspector.UserId;
            IsSupervisor = inspector.IsSupervisor;
        }
    }
    [Serializable]
    public class StackPhysicalCountInfo
    {
        [XmlAttribute]
        public Guid Id { get; set; }
        [XmlAttribute]
        public Guid PhysicalCountId { get; set; }
        [XmlAttribute]
        public Guid ShedId { get; set; }
        [XmlAttribute]
        public Guid StackId { get; set; }
        [XmlAttribute]
        public int ExpectedBalance { get; set; }
        [XmlAttribute]
        public int Balance { get; set; }
        [XmlAttribute]
        public int CummulatedShortage { get; set; }
        [XmlAttribute]
        public int CumulatedOverage { get; set; }

        public void Copy(StackPhysicalCountInfo stack)
        {
            Id = stack.Id;
            PhysicalCountId = stack.PhysicalCountId;
            ShedId = stack.ShedId;
            StackId = stack.StackId;
            ExpectedBalance = stack.ExpectedBalance;
            Balance = stack.Balance;
            CummulatedShortage = stack.CummulatedShortage;
            CumulatedOverage = stack.CumulatedOverage;
        }
    }

    public class StackLoadInfo
    {
        public StackLoadInfo() { }
        public StackLoadInfo(Guid stackId, int bags, float weight)
        {
            StackId = stackId;
            Bags = bags;
            Weight = weight;
        }
        public Guid StackId { get; set; }
        public int Bags { get; set; }
        public float Weight { get; set; }
    }

    public class StackUnloadInfo
    {
        public StackUnloadInfo() { }
        public StackUnloadInfo(Guid stackId, int bags, float weight)
        {
            StackId = stackId;
            Bags = bags;
            Weight = weight;
        }
        public Guid StackId { get; set; }
        public int Bags { get; set; }
        public float Weight { get; set; }
   }

    public class InventoryServices : IInventoryServices
    {
        public static List<IDataIdentifier> SearchPhysicalCount(IDataFilter filter)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PhysicalCount").GetCatalog(filter);
        }

        public static List<IDataIdentifier> SearchPhysicalCount()
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PhysicalCount").GetCatalog();
        }

        public static IDataFilter GetPhysicalCountCatalogFilter(string templateName)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("PhysicalCount").GetCatalogFilter(templateName);
        }

        public static ILookupSource StaticLookupSource
        {
            get { return new InventoryServiceLookup(); }
        }

        private static IInventoryServices singleton;
        private InventoryServices() { }
        public static IInventoryServices GetInventoryService()
        {
            if (singleton == null)
            {
                singleton = new InventoryServices();
            }
            return singleton;
        }

        #region IInventoryServices Members
        public PhysicalCountInfo OpenPhysicalCount(Guid id)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("Id", id);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(PhysicalCountInfo));
                XmlDocument document = PersistenceTransactionFactory.CreatePersistenceTransaction().Open(
                    "spOpenPhysicalInventory", contextParameters);
                StringBuilder sb = new StringBuilder();
                TextWriter writer = new StringWriter(sb);
                document.Save(writer);

                return (PhysicalCountInfo)s.Deserialize(new StringReader(sb.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception("The database failed to load the requested Physical Count ", ex);
            }
        }

        public void SavePhysicalCount(PhysicalCountInfo physicalCount)
        {
            PhysicalCountInfo originalPhysicalCount = OpenPhysicalCount(physicalCount.Id);
            AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.InvCount,
                 new object[][] { new object[] {originalPhysicalCount, physicalCount, AuditTrailWrapper.ExistingRecord } }, "Inventory Control");
            foreach (PhysicalCountInspectorInfo inspector in physicalCount.Inspectors)
            {
                PhysicalCountInspectorInfo originalInspector = originalPhysicalCount.Inspectors.Find(oi => oi.Id == inspector.Id);
                if (originalInspector == null)
                {
                    atw.AddChange(null, inspector, AuditTrailWrapper.NewRecord);
                }
                else
                {
                    atw.AddChange(originalInspector, inspector, AuditTrailWrapper.ExistingRecord);
                }
            }
            foreach (StackPhysicalCountInfo stack in originalPhysicalCount.Stacks)
            {
                StackPhysicalCountInfo originalStack = physicalCount.Stacks.Find(os => os.Id == stack.Id);
                if (originalStack == null)
                {
                    atw.AddChange(null, stack, AuditTrailWrapper.NewRecord);
                }
                else
                {
                    atw.AddChange(originalStack, stack, AuditTrailWrapper.ExistingRecord);
                }
            }

            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { physicalCount });
            foreach (PhysicalCountInspectorInfo inspector in physicalCount.Inspectors)
            {
                queue.Enqueue(inspector);
            }
            foreach (StackPhysicalCountInfo stack in physicalCount.Stacks)
            {
                queue.Enqueue(stack);
            }
            try
            {
                SqlTransaction transaction = PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Physical Count couldn't be saved to the database", ex);
            }
        }

        
        public void CreatePhysicalCount(PhysicalCountInfo physicalCount)
        {
            AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.InvCount,
                 new object[][] { new object[] { null, physicalCount, AuditTrailWrapper.NewRecord } }, "Inventory Control");
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                SqlTransaction transaction = PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { physicalCount }), contextParameters);
                transaction.Commit();
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Physical Count couldn't be saved to the database", ex);
            }
        }

        public void AddStackPhysicalCount(PhysicalCountInfo physicalCount, StackPhysicalCountInfo stackPhysicalCount)
        {
            if (physicalCount.Stacks.Any(s => (s.StackId == stackPhysicalCount.StackId)))
            {
                throw new Exception("Invalid Stack Count : Stack count shall not be duplicated");
            }
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("StackId", stackPhysicalCount.StackId);
            contextParameters.Add("PhysicalCountId", stackPhysicalCount.PhysicalCountId);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(StackInventoryStatusInfo));
                XmlDocument document = PersistenceTransactionFactory.CreatePersistenceTransaction().Open(
                    "spGetStackInventoryStatus", contextParameters);
                StringBuilder sb = new StringBuilder();
                TextWriter writer = new StringWriter(sb);
                document.Save(writer);

                StackInventoryStatusInfo sis = (StackInventoryStatusInfo)s.Deserialize(new StringReader(sb.ToString()));
                stackPhysicalCount.CummulatedShortage = sis.CummulatedShortage;
                stackPhysicalCount.CumulatedOverage = sis.CumulatedOverage;
                stackPhysicalCount.ExpectedBalance = sis.ExpectedBalance;
            }
            catch (Exception ex)
            {
                throw new Exception("The database failed to load a required Stack Inventory Status ", ex);
            }
            physicalCount.Stacks.Add(stackPhysicalCount);
        }

        public void AddInspector(PhysicalCountInfo physicalCount, PhysicalCountInspectorInfo inspector)
        {
            if (physicalCount.Inspectors.Any(i => (i.UserId == inspector.UserId) || (i.IsSupervisor && inspector.IsSupervisor)))
            {
                throw new Exception("Invalid Inspector : Inspector shall not be duplicated and only 1 supervisor is admitted");
            }
            physicalCount.Inspectors.Add(inspector);
        }

        public StackPhysicalCountInfo GetBlankStackCount(PhysicalCountInfo physicalCount)
        {
            return new StackPhysicalCountInfo() { Balance = 0, CummulatedShortage = 0, ExpectedBalance = 0, Id = Guid.NewGuid(), PhysicalCountId = physicalCount.Id, ShedId = Guid.Empty, StackId = Guid.Empty };
        }

        public PhysicalCountInspectorInfo GetBlankInspector(PhysicalCountInfo physicalCount)
        {
            Guid inspectorId = (Guid)LookupSource.GetLookup("Inspector").Keys.FirstOrDefault();
            if ((inspectorId == null) || (inspectorId == Guid.Empty))
            {
                throw new Exception("No Physical Count Inspector has been configured");
            }
            return new PhysicalCountInspectorInfo() { Id = Guid.NewGuid(), PhysicalCountId=physicalCount.Id, IsSupervisor = false, UserId = inspectorId };
        }
        public SqlTransaction UnloadToStack(Guid stackId, int bags, float weight)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { new StackUnloadInfo(stackId, bags, weight) });
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save stack unloading to the database", ex);
            }
        }


        public SqlTransaction UnloadToStack(Guid stackId, int bags, float weight, SqlTransaction transction)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { new StackUnloadInfo(stackId, bags, weight) });
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters, transction);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save stack unloading to the database", ex);
            }
        }

        public SqlTransaction LoadFromStack(Guid stackId, int bags, float weight)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { new StackLoadInfo(stackId, bags, weight) });
            try
            {
                SqlTransaction transaction = PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);

                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save stack unloading to the database", ex);
            }
        }

        public SqlTransaction LoadFromStack(Guid stackId, int bags, float weight, SqlTransaction transction)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { new StackLoadInfo(stackId, bags, weight) });
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters, transction);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save stack unloading to the database", ex);
            }
        }

        public ILookupSource LookupSource { get { return new InventoryServiceLookup(); } } 

        #endregion

        #region Nested Classes
        [Serializable]
        private class InventoryServiceLookup : ILookupSource
        {
            public InventoryServiceLookup()
            {
            }

            #region ILookupSource Members

            public System.Collections.Generic.IDictionary<object, string> GetLookup(string lookupName)
            {

                IDictionary<object, string> lookup = new Dictionary<object, string>();
                switch (lookupName)
                {
                    case "CurrentWarehouse":
                        return SystemLookup.LookupSource.GetLookup("CurrentWarehouse");
                    default:
                        return SystemLookup.LookupSource.GetLookup(lookupName);
                }
            }

            public System.Collections.Generic.IDictionary<string, object> GetInverseLookup(string lookupName)
            {
                return null;
            }

            #endregion
        }

        #endregion

    }
}
