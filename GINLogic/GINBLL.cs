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
using WarehouseApplication.DALManager;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using WarehouseApplication.BLL;

namespace WarehouseApplication.GINLogic
{
    public class GINProcessBLL : IGINProcess
    {
        #region Static Members
        public static List<IDataIdentifier> SearchGINProcess(IDataFilter filter)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("GINProcess").GetCatalog(filter);
        }

        public static List<IDataIdentifier> SearchGINProcess()
        {
            return SQLXmlDALManagerFactory.CreateDALManager("GINProcess").GetCatalog();
        }

        public static IDataFilter GetCatalogFilter()
        {
            return SQLXmlDALManagerFactory.CreateDALManager("GINProcess").GetCatalogFilter();
        }

        public static IDataFilter GetCatalogFilter(string templateName)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("GINProcess").GetCatalogFilter(templateName);
        }

        public static ILookupSource StaticLookupSource
        {
            get { return new GINProcessLookup(); }
        }

        public static List<IDataIdentifier> SearchGIN(IDataFilter filter)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("GIN").GetCatalog(filter);
        }

        public static IDataFilter GetGINCatalogFilter(string templateName)
        {
            return SQLXmlDALManagerFactory.CreateDALManager("GIN").GetCatalogFilter(templateName);
        }

        public static GINEditingRequest GetGINEditingRequest(string transactionId)
        {
            GINEditingRequest ginEditingRequest = null;
            try
            {
                Dictionary<string, object> contextParameters = new Dictionary<string, object>();
                contextParameters.Add("TransactionId", transactionId);
                XmlDocument document = PersistenceTransactionFactory.CreatePersistenceTransaction().Open(
                        "spOpenGINEditingRequest", contextParameters);
                XmlSerializer s = new XmlSerializer(typeof(GINEditingRequest));
                StringBuilder sb = new StringBuilder();
                TextWriter writer = new StringWriter(sb);
                document.Save(writer);
                //ginEditingRequest = (GINEditingRequest)s.Deserialize(new StringReader(sb.ToString()));
                XDocument xDocument = XDocument.Load(new StringReader(sb.ToString()));
                ginEditingRequest = (
                    from ger in xDocument.Descendants("GINEditingRequest")
                    select new GINEditingRequest()
                    {
                        DeliveryReceivedId = new Guid(ger.Attribute("Id").Value),
                        OldTransactionId = ger.Attribute("OldTransactionId").Value,
                        ProposedChange = ger.Descendants("GINProcess").First().ToString(),
                        TargetPage = ger.Attribute("TargetPage").Value,
                        TransactionId = ger.Attribute("TransactionId").Value
                    }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("The database failed to load the requested GIN process ", ex);
            }
            return ginEditingRequest;
        }
        #endregion

        private GINProcessInfo ginProcessInfomation;
        private ILookupSource lookupSource;

        public GINProcessBLL(Guid ginProcessId)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("GINProcessId", ginProcessId);
            LoadGINProcess(contextParameters);
        }

        public GINProcessBLL(String transactionId)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("TransactionId", transactionId);
            LoadGINProcess(contextParameters);
        }

        public GINProcessBLL(string transactionId, bool isGinTransaction)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("TransactionId", transactionId);
            contextParameters.Add("IsGINTransaction", isGinTransaction);
            LoadGINProcess(contextParameters);
            if ((isGinTransaction) && ginProcessInfomation.Trucks.Count == 0)
            {
                throw new Exception("The provided transaction doesn't represent any ongoing GIN");
            }
        }
        public GINProcessBLL()
        {
        }

        private void LoadGINProcess(Dictionary<string, object> contextParameters)
        {
            try
            {
                XmlDocument document = PersistenceTransactionFactory.CreatePersistenceTransaction().Open(
                        "spOpenGINProcess", contextParameters);
                    XmlSerializer s = new XmlSerializer(typeof(GINProcessInfo));
                    StringBuilder sb = new StringBuilder();
                    TextWriter writer = new StringWriter(sb);
                    document.Save(writer);
                    ginProcessInfomation = (GINProcessInfo)s.Deserialize(new StringReader(sb.ToString()));
            }
            catch(Exception ex)
            {
                throw new Exception("The database failed to load the requested GIN process ", ex);
            }
            lookupSource = new GINProcessLookup(ginProcessInfomation);
        }

        #region IGINProcess Members

        public ILookupSource LookupSource
        {
            get { return lookupSource; }
        }

        public GINProcessInfo GINProcessInformation
        {
            get { return ginProcessInfomation; }
            set
            {
                ginProcessInfomation = value;
                lookupSource = new GINProcessLookup(ginProcessInfomation);
            }
        }

        public SqlTransaction SaveAvailabilityVerification(PUNAcknowledgementInformation punaInformation)
        {
            decimal fractionPledgedQuantityAdjusted = (decimal)1;
            decimal weightTolerance = decimal.Parse(ConfigurationManager.AppSettings["WeightTolerance"]);
            int wholePledgedQuantity = (int)(punaInformation.PledgedWeight / CorrectedLotSize);
            decimal fractionPledgedQuantity = punaInformation.PledgedWeight - wholePledgedQuantity * CorrectedLotSize;
            if (fractionPledgedQuantity < weightTolerance * CorrectedLotSize)
                fractionPledgedQuantityAdjusted = 0;
            else if (CorrectedLotSize - fractionPledgedQuantity > weightTolerance * CorrectedLotSize)
                fractionPledgedQuantityAdjusted = fractionPledgedQuantity / CorrectedLotSize;
            decimal pledgedQuantity = wholePledgedQuantity + fractionPledgedQuantityAdjusted;

            punaInformation.PledgedQuantity = wholePledgedQuantity + fractionPledgedQuantityAdjusted;
            ValidateAvailabilityConfirmation(punaInformation);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { punaInformation });
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {

                throw new Exception("Your commodity availability confirmation couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveTruckRegistration()
        {
            ValidateTruckRegistration();
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>();
            foreach (GINTruckInfo truck in GINProcessInformation.Trucks)
            {
                queue.Enqueue(truck);
                queue.Enqueue(truck.Load);
                queue.Enqueue(truck.Weight);
            }
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {

                throw new Exception("Trucks couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction CreateTruck(GINTruckInfo truck)
        {
            GINProcessInformation.Trucks.Add(truck);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { truck, truck.Load, truck.Weight }), contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your truck information couldn't be saved to the database", ex);
            }

        }

        public SqlTransaction SaveTruck(GINTruckInfo truck)
        {
            ValidateTruck(truck);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { truck, truck.Load, truck.Weight }), contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your truck information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveTruckInformation(Guid truckId)
        {
            GINTruckInfo truck =
                (from t in GINProcessInformation.Trucks
                 where t.TruckId == truckId
                 select t).FirstOrDefault();
            ValidateTruck(truck);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { truck }), contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your truck information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveTruck(GINTruckInfo truck, SqlTransaction transaction)
        {
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { truck}), contextParameters, transaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Your truck information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveLoading(Guid truckId)
        {
            var trucksLoaded = from truck in ginProcessInfomation.Trucks
                               where truck.TruckId == truckId
                               select truck;
            GINTruckInfo truckLoaded = trucksLoaded.ElementAt(0);
            ValidateTruck(truckLoaded);
            ValidateTruckLoading(truckLoaded.Load);
            RecalculateGIN(truckLoaded);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            contextParameters.Add("WarehouseCode", SystemLookup.LookupSource.GetLookup("CurrentWarehouse")["WarehouseCode"]);
            Queue<object> queue = new Queue<object>(new object[] { truckLoaded, truckLoaded.Load, truckLoaded.GIN });
            foreach (TruckStackInfo loadedStack in truckLoaded.Load.Stacks)
            {
                queue.Enqueue(loadedStack);
            }
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your truck load information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveScaling(Guid truckId)
        {
            var trucksWeighed = from truck in ginProcessInfomation.Trucks
                                where truck.TruckId == truckId
                                select truck;
            GINTruckInfo truckWeighed = trucksWeighed.ElementAt(0);
            ValidateTruck(truckWeighed);
            ValidateTruckWeight(truckWeighed.Weight);
            RecalculateGIN(truckWeighed);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            contextParameters.Add("WarehouseCode", SystemLookup.LookupSource.GetLookup("CurrentWarehouse")["WarehouseCode"]);
            Queue<object> queue = new Queue<object>(new object[] { truckWeighed, truckWeighed.Weight, truckWeighed.GIN });
            foreach (ReturnedBagsInfo returnedBag in truckWeighed.Weight.ReturnedBags)
            {
                queue.Enqueue(returnedBag);
            }
            foreach (ReturnedBagsInfo addedBag in truckWeighed.Weight.AddedBags)
            {
                queue.Enqueue(addedBag);
            }
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your truck scaling information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction CompleteGINProcess()
        {
            GINProcessInformation.Status = (int)GINProcessStatusType.Completed;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            SqlTransaction transaction = null;
            try
            {
                transaction = PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { GINProcessInformation }), contextParameters);
                IPickupNotice pun = new PickupNoticeBLL(GINProcessInformation.PickupNoticeId);
                pun.GINIssued(transaction);
                return transaction;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                throw new Exception("The PUN completed status couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction AbortGINProcess()
        {
            GINProcessInformation.Status = (int)GINProcessStatusType.Aborted;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            SqlTransaction transaction = null;
            try
            {
                transaction = PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { GINProcessInformation }), contextParameters);
                //IPickupNotice pun = new PickupNoticeBLL(GINProcessInformation.PickupNoticeId);
                //pun.Aborted(transaction);
                return transaction;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                throw new Exception("The PUN abortion status couldn't be saved to the database", ex);
            }
        }

        public GINTruckInfo GetBlankTruck()
        {
            GINTruckInfo truck = new GINTruckInfo(
                Guid.NewGuid(),
                GINProcessInformation.GINProcessId,
                GINProcessInformation.PickupNoticeId,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                Guid.NewGuid(),
                string.Empty,
                Guid.NewGuid(),
                string.Empty,
                (int)lookupSource.GetLookup("TruckStatus").Keys.ElementAt(0),
                string.Empty);
            truck.Load = new TruckLoadInfo(truck.TruckId, DateTime.Today,
                (Guid)lookupSource.GetLookup("BagType").Keys.ElementAt(0),
                string.Empty);
            truck.Weight = new TruckWeightInfo(truck.TruckId, DateTime.Now, string.Empty, 0, 0, (Guid)lookupSource.GetLookup("Weigher").Keys.ElementAt(0), string.Empty);
            return truck;
        }


        public WorkerInformation GetBlankLoader(Guid truckId)
        {
            Guid workerId = (Guid)lookupSource.GetLookup("AvailableLoader").Keys.ElementAt(0);
            return new WorkerInformation(
                Guid.NewGuid(),
                workerId,
                truckId,
                WorkType.GINLoading,
                new Guid(lookupSource.GetLookup("EmployeeRole")[WorkType.GINLoading]),
                false,
                (int)lookupSource.GetLookup("WorkerStatus").Keys.ElementAt(0),
                string.Empty);
        }

        public WorkerInformation GetBlankWeigher(Guid truckId)
        {
            Guid workerId = (Guid)lookupSource.GetLookup("AvailableWeigher").Keys.ElementAt(0);
            return new WorkerInformation(
                Guid.NewGuid(),
                workerId,
                truckId,
                WorkType.GINScaling,
                new Guid(lookupSource.GetLookup("EmployeeRole")[WorkType.GINScaling]),
                false,
                (int)lookupSource.GetLookup("WorkerStatus").Keys.ElementAt(0),
                string.Empty);
        }

        public TruckStackInfo GetBlankTruckStack(Guid truckId)
        {
            string[] idPair = ((string)lookupSource.GetLookup("Shed").Keys.ElementAt(0)).Split('_');
            Guid shedId = new Guid(idPair[0]);
            Guid stackId = Guid.Empty;
            foreach (StackBLL stack in new StackBLL().GetActiveStackbyShedId(shedId))
            {
                if (stack.CommodityGradeid == GINProcessInformation.CommodityGradeId)
                {
                    stackId = stack.Id;
                    break;
                }
            }
            return new TruckStackInfo(
                Guid.NewGuid(),
                truckId,
                stackId,
                shedId,
                (Guid)lookupSource.GetLookup("Loader").Keys.ElementAt(0),
                0);
        }

        public ReturnedBagsInfo GetBlankReturnedBags(Guid truckId)
        {
            string[] idPair = ((string)lookupSource.GetLookup("Shed").Keys.ElementAt(0)).Split('_');

            Guid shedId = new Guid(idPair[0]);
            Guid stackId = Guid.Empty;
            foreach (StackBLL stack in new StackBLL().GetActiveStackbyShedId(shedId))
            {
                if (stack.CommodityGradeid == GINProcessInformation.CommodityGradeId)
                {
                    stackId = stack.Id;
                    break;
                }
            }
            return new ReturnedBagsInfo(
                Guid.NewGuid(),
                truckId,
                stackId,
                shedId,
                0m,
                true,
                0);
        }

        public SampleInfo GetBlankSample()
        {
            return new SampleInfo(
                Guid.NewGuid(),
                GINProcessInformation.GINProcessId,
                new Random().Next(),
                DateTime.Now,
                new Random().Next(),
                string.Empty,
                (int)lookupSource.GetLookup("SamplingStatus").Keys.ElementAt(0),
                DateTime.Now);
        }

        public void DeleteLoader(Guid id)
        {
            //WorkerInformation loader = GINProcessInformation.Loaders.Find(new WorkerByIdFinder(id).FindWorker);
            //GINProcessInformation.Loaders.Remove(loader);
        }

        public void DeleteWeigher(Guid id)
        {
            //WorkerInformation weigher = GINProcessInformation.Weighers.Find(new WorkerByIdFinder(id).FindWorker);
            //GINProcessInformation.Weighers.Remove(weigher);
        }

        public void DeleteTruck(Guid id)
        {
            //GINTruckInfo truck = GINProcessInformation.Trucks.Find(new GINTruckByIdFinder(id).FindTruck);
            //GINProcessInformation.Trucks.Remove(truck);
        }
        public void DeleteTruckStack(Guid truckId, Guid id)
        {
            //GINTruckInfo truck = GINProcessInformation.Trucks.Find(new GINTruckByIdFinder(truckId).FindTruck);
            //TruckStackInfo stack = truck.Load.Stacks.Find(new TruckStackByIdFinder(id).FindTruckStack);
            //truck.Load.Stacks.Remove(stack);
        }

        public void AddLoader(Guid truckId, WorkerInformation loader)
        {
            var truckToAddTo = from truck in GINProcessInformation.Trucks
                               where truck.TruckId == truckId
                               select truck.Load;
            truckToAddTo.ElementAt(0).Loaders.Add(loader);
        }

        public void AddStack(Guid truckId, TruckStackInfo stack)
        {
            var truckToAddTo = from truck in GINProcessInformation.Trucks
                               where truck.TruckId == truckId
                               select truck.Load;
            truckToAddTo.ElementAt(0).Stacks.Add(stack);
        }

        public void AddReturnedBags(Guid truckId, ReturnedBagsInfo returnedBags)
        {
            var truckToAddTo = from truck in GINProcessInformation.Trucks
                               where truck.TruckId == truckId
                               select truck.Weight;
            if (returnedBags.Returned)
            {
                truckToAddTo.ElementAt(0).ReturnedBags.Add(returnedBags);
            }
            else
            {
                truckToAddTo.ElementAt(0).AddedBags.Add(returnedBags);
            }
        }

        public void AddSample(SampleInfo sample)
        {
            GINProcessInformation.Samples.Add(sample);
        }

        public void AddTruck(GINTruckInfo truck)
        {
            ValidateTruck(truck);
            GINProcessInformation.Trucks.Add(truck);
            truck.Load = new TruckLoadInfo(truck.TruckId, DateTime.Now,
                (Guid)lookupSource.GetLookup("BagType").Keys.ElementAt(0),
                string.Empty);
            truck.Weight = new TruckWeightInfo(truck.TruckId, DateTime.Now, string.Empty, 0, 0, (Guid)lookupSource.GetLookup("Weigher").Keys.ElementAt(0), string.Empty);
        }

        public void AddWeigher(Guid truckId, WorkerInformation weigher)
        {
            var truckToAddTo = from truck in GINProcessInformation.Trucks
                               where truck.TruckId == truckId
                               select truck.Weight;
            truckToAddTo.ElementAt(0).Weighers.Add(weigher);
        }

        public GINReportInfo GetGINReport(Guid ginId)
        {
            var truckForGin = from truck in GINProcessInformation.Trucks
                              where truck.GIN.GINId == ginId
                              select truck;
            GINTruckInfo theTruck = truckForGin.ElementAt(0);
            GINInfo theGIN = theTruck.GIN;


            //var loaderForGin = from loader in theTruck.Load.Loaders
            //                   where loader.IsSupervisor
            //                   select loader;
            Guid loaderId = Guid.Empty;
            if(theTruck.Load.Stacks.Count > 0)
                loaderId = theTruck.Load.Stacks[0].LoadingSupervisor;

            //var weigherForGin = from weigher in theTruck.Weight.Weighers
            //                    where weigher.IsSupervisor
            //                    select weigher;
            Guid weigherId = theTruck.Weight.WeighingSupervisor;

/*
            var samplerForGin = from sampler in GINProcessInformation.Samples.ElementAt(0).Samplers
                                where sampler.IsSupervisor
                                select sampler;
            Guid samplerId = samplerForGin.ElementAt(0).WorkerId;

            var graderForGin = from gradingResults in GINProcessInformation.Grading.GradingResults
                               where gradingResults.Grader.IsSupervisor
                               select gradingResults.Grader;
            Guid graderId = graderForGin.ElementAt(0).WorkerId;
 */
//Temporary Use
            Guid samplerId = (Guid)lookupSource.GetLookup("Sampler").Keys.ElementAt(0);
            Guid graderId = (Guid)lookupSource.GetLookup("Grader").Keys.ElementAt(0);
//End of Temporary Use
            IPickupNotice pickupNotice = new PickupNoticeBLL(GINProcessInformation.PickupNoticeId);
            var punAgents = from puna in pickupNotice.PickupNoticeInformation.PickupNoticeAgents
                            where puna.Id == GINProcessInformation.PickupNoticeAgentId
                            select puna;
            PickupNoticeInformation.PickupNoticeAgentInformation punAgent = punAgents.ElementAt(0);
            int TotalReturnedBags = 0;
            if (theTruck.Weight.ReturnedBags.Count > 0)
            {
                TotalReturnedBags = (from rb in theTruck.Weight.ReturnedBags select rb.Bags).Aggregate((aggregateValue, next) => aggregateValue + next);
            }
            int TotalAddedBags = 0;
            if (theTruck.Weight.AddedBags.Count > 0)
            {
                TotalAddedBags = (from rb in theTruck.Weight.AddedBags select rb.Bags).Aggregate((aggregateValue, next) => aggregateValue + next);
            }
            return new GINReportInfo(
                theGIN.GINId,
                theGIN.GINNo,
                new Guid(lookupSource.GetLookup("CurrentWarehouse")["Id"]),
                GINProcessInformation.ClientId,
                GINProcessInformation.PickupNoticeAgentId,
                punAgent.NIDNumber,
                punAgent.NIDType,
                GINProcessInformation.PickupNoticeAgentVerified,
                theGIN.SignedByClient,
                GINProcessInformation.CommodityGradeId,
                theGIN.Quantity,
                theGIN.NetWeight,
                theTruck.Weight.ScaleTicketNo,
                theTruck.Load.BagType,
                theTruck.Load.TotalLoad + TotalAddedBags - TotalReturnedBags,
                samplerId,
                graderId,
                loaderId,
                weigherId,
                theTruck.DriverName,
                theTruck.LicenseNo,
                theTruck.IssuedBy,
                theTruck.PlateNo,
                theTruck.TrailerNo,
                theGIN.DateIssued,
                theGIN.Status);

        }

        //public Guid CurrentUser
        //{
        //    get { return new Guid("03254921-ee27-4406-97ce-846b47b1cd75"); }
        //}

        public decimal CalculateNetWeight(Guid truckId)
        {
            var truckToCalculate = from truck in GINProcessInformation.Trucks
                                   where truck.TruckId == truckId
                                   select truck;
            GINTruckInfo theTruck = truckToCalculate.ElementAt(0);
            BagTypeBLL bagType = BagTypeBLL.GetBagType(theTruck.Load.BagType);
            CommodityGradeBLL comGrade = CommodityGradeBLL.GetCommodityGrade(ginProcessInfomation.CommodityGradeId);
            CommodityGradeBLL com = CommodityGradeBLL.GetCommodityById(comGrade.CommodityId);
            decimal bagPerUnit = 1m;
            switch (com.UnitOfMeasure)
            {
                case "Quintal":
                    bagPerUnit=100m;
                    break;
            }
            decimal bagWeight = Convert.ToDecimal(bagType.Tare) / bagPerUnit;
            decimal rawNetWeight = theTruck.Weight.GrossWeight - theTruck.Weight.TruckWeight - theTruck.Load.TotalLoad * bagWeight;
            decimal correctionWeight = 0;
            var addedBagsQuery = from addedBags in theTruck.Weight.AddedBags select addedBags.Size - addedBags.Bags * bagWeight;
            if (addedBagsQuery.Count() > 0)
                correctionWeight += addedBagsQuery.Aggregate((sum, next) => sum + next);
            var returnedBagsQuery = from returnedBags in theTruck.Weight.ReturnedBags select returnedBags.Size - returnedBags.Bags * bagWeight;
            if(returnedBagsQuery.Count() > 0)
                correctionWeight-=returnedBagsQuery.Aggregate((sum, next) => sum + next);
            //return Math.Truncate((rawNetWeight+correctionWeight) * 10000 + 0.9m) / 10000;
            return Math.Truncate((rawNetWeight + correctionWeight) * 100 + 0.9m) / 100;
        }


        public SqlTransaction SaveGIN(Guid truckId)
        {
            var gins = from truck in ginProcessInfomation.Trucks
                       where truck.GIN.TruckId == truckId
                       select truck.GIN;
            GINInfo gin = gins.ElementAt(0);
            ValidateGIN(gin);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            contextParameters.Add("WarehouseCode", SystemLookup.LookupSource.GetLookup("CurrentWarehouse")["WarehouseCode"]);
            Queue<object> queue = new Queue<object>(new object[] { gin });
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your GIN information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveGIN(Guid truckId, SqlTransaction transaction)
        {
            var gins = from truck in ginProcessInfomation.Trucks
                       where truck.GIN.TruckId == truckId
                       select truck.GIN;
            GINInfo gin = gins.ElementAt(0);
            ValidateGIN(gin);
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            Queue<object> queue = new Queue<object>(new object[] { gin });
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(queue, 
                    contextParameters,
                    transaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Your GIN information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction CompleteAvailabilityVerification()
        {
            GINProcessInformation.Status = (int)GINProcessStatusType.Ok_to_Load;
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    new Queue<object>(new object[] { GINProcessInformation }), contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("The Inventory Confirmation completed status couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction CompleteLoading(Guid truckId)
        {
            return SetGinStatus(truckId, GINStatusType.Loaded);
        }

        public SqlTransaction CompleteScaling(Guid truckId)
        {
            return SetGinStatus(truckId, GINStatusType.Scaled);
        }

        public SqlTransaction GINGenerated(Guid truckId)
        {
            return SetGinStatus(truckId, GINStatusType.GINGenerated);
        }

        public SqlTransaction GINSigned(Guid truckId)
        {
            return SetGinStatus(truckId, GINStatusType.GINSigned);
        }

        public SqlTransaction GINApproved(Guid truckId)
        {
            return SetGinStatus(truckId, GINStatusType.GINApproved);
        }

        public void ValidateAvailabilityConfirmation(PUNAcknowledgementInformation punaInformation)
        {
            if (punaInformation.PledgedQuantity < 0)
            {
                throw new Exception("Invalid Planned to be Issued Value: Planned to be Issued shall be positive decimal");
            }
            if (punaInformation.PledgedWeight > punaInformation.Weight)
            {
                throw new Exception("Invalid Planned to be Issued Value: Planned to be Issued couldn't be greater than Requested");
            }

            if (punaInformation.PledgedQuantity > punaInformation.AvailableInventoryBags)
            {
                throw new Exception(string.Format("Invalid Planned to be Issued Value: Planned to be Issued: {0} couldn't be greater than Available Inventory: {1}",
                    punaInformation.PledgedQuantity, punaInformation.AvailableInventoryBags));
            }
            if (!punaInformation.AvailabilityVerified)
            {
                throw new Exception("Please confirm commodity availability by checking the verified box");
            }
        }

        public void ValidateTruckRegistration()
        {
            var registeredTrucksQuery = from truck in GINProcessInformation.Trucks
                                        where (truck.Status == (int)GINStatusType.ReadyToLoad)
                                        select truck;
            if (registeredTrucksQuery.Count()== 0)
            {
                throw new Exception("At least one truck is required to be registered");
            }
        }

        public void ValidateGINProcess(GINProcessInfo ginProcess)
        {
            if (ginProcess.PledgedWeight > ginProcess.Weight)
            {
                throw new Exception("The pledged weight could not be more than the PUN Weight");
            }
            if (ginProcess.PledgedWeight < 0.0001M)
            {
                throw new Exception("The pledged weight shall be a positive decimal");
            }
        }
        public void ValidateTruck(GINTruckInfo truck)
        {
            if ((truck.DriverName.Trim() == string.Empty) ||
                (truck.LicenseNo.Trim() == string.Empty) ||
                (truck.PlateNo.Trim() == string.Empty))
            {
                throw new Exception("Driver Name, License No. and Plate No. are required");
            }
        }

        public void ValidateGIN(GINInfo gin)
        {
            switch((GINStatusType)gin.Status)
            {
                case GINStatusType.Loaded:
                    if (NullFinder.IsNull(gin.DateIssued, "System.DateTime"))
                    {
                        throw new Exception("Invalid Issue Date: Please specify the Issue Date of the GIN");
                    }
                    if (gin.DateIssued > DateTime.Now)
                    {
                        throw new Exception("Invalid Issue Date: Issue date couldn't be a future time"); 
                    }
                    GINTruckInfo truck = GINProcessInformation.Trucks.Find(trk => trk.TruckId == gin.TruckId);
                    
                    if (gin.DateIssued < truck.Weight.DateWeighed)
                    {
                        throw new Exception("Invalid Issue Date: GIN issue date couldn't be earlier than truck weighing date");
                    }
                    break;
                case GINStatusType.GINGenerated:
                    if(!gin.SignedByClient)
                    {
                        throw new Exception("Please Confirm that the client has signed the GIN");
                    }
                    break;
                case GINStatusType.GINSigned:
                    if(NullFinder.IsNull(gin.DateApproved, "System.DateTime"))
                    {
                        throw new Exception("Please specify the Approval Date of the GIN");
                    }
                    if (gin.DateApproved > DateTime.Now)
                    {
                        throw new Exception("Invalid Approval Date: Approval date couldn't be a future time");
                    }

                    if (gin.DateApproved < gin.DateIssued)
                    {
                        throw new Exception("Invalid Approval Date: GIN approval date couldn't be earlier than its issuance date");
                    }
                    break;
                case GINStatusType.GINApproved:
                    if(NullFinder.IsNull(gin.TruckCheckedOutOn, "System.DateTime"))
                    {
                        throw new Exception("Please specify the Truck Checkout Date of the GIN");
                    }
                    if (gin.TruckCheckedOutOn > DateTime.Now)
                    {
                        throw new Exception("Invalid Truck Checkout Date: Truck Checkout date couldn't be a future time");
                    }

                    if (gin.TruckCheckedOutOn < gin.DateApproved)
                    {
                        throw new Exception("Invalid Truck Checkout Date: GIN truck checkout date couldn't be earlier than its approval date");
                    }
                    break;

            }
        }

        public void ValidateTruckLoading(TruckLoadInfo load)
        {
            //var supervisorLoaders = from loader in load.Loaders
            //                        where (loader.IsSupervisor)
            //                        select loader;
            //if (supervisorLoaders.Count() != 1)
            //{
            //    throw new Exception("Loading requires exactly one supervisor");
            //}
            if (load.DateLoaded < GINProcessInformation.AvailabilityVerifiedOn)
            {
                throw new Exception("Invalid Loading Date: Loading couldn't be done on date earlier than the PUN Inverntory Verification");
            }
            if (load.DateLoaded > DateTime.Now)
            {
                throw new Exception("Invalid Loading Date: Loading date couldn't be a future time");
            }
            var loadedBags = from stack in load.Stacks
                               select stack.Bags;
            if (loadedBags.Sum() <= 0)
            {
                throw new Exception("Your record for the truck indicates that it has not been loaded");
            }
        }
        public void ValidateTruckWeight(TruckWeightInfo weight)
        {
            GINTruckInfo truck = GINProcessInformation.Trucks.Find(trk=>trk.TruckId == weight.TruckId);
            if (weight.DateWeighed < truck.Load.DateLoaded)
            {
                throw new Exception("Invalid Weighing Date: Weighing couldn't be done on date earlier than the truck is loaded");
            }
            if (weight.DateWeighed > DateTime.Now)
            {
                throw new Exception("Invalid Weighing Date: Weighing date couldn't be a future time");
            }

            if ((weight.GrossWeight <= 0) ||
                (weight.TruckWeight <= 0) ||
                (weight.GrossWeight <= weight.TruckWeight))
            {
                throw new Exception("Invalid Weight: Gross weight and TruckWeight shall be positive numbers.<br/> Gross weight shall always be greater than the truck weight"); 
            }
            int nReturnedBags = weight.ReturnedBags.Sum(rb => rb.Bags);
            if (nReturnedBags < 0)
            {
                throw new Exception("Invalid Weight: The sum of all returned bags cannot be negative");
            }
            decimal wReturnedBags = weight.ReturnedBags.Sum(rb => rb.Size);
            if ((wReturnedBags != 0.0M) && (wReturnedBags < 0.0001M))
            {
                throw new Exception("Invalid Weight: The total weight of all the returned bags cannot be negative");
            }
            if (weight.ScaleTicketNo.Trim() == string.Empty)
            {
                throw new Exception("ScaleTicketNo could not be empty");
            }
            decimal netWeight = CalculateNetWeight(weight.TruckId);
            decimal expectedWeight = GINProcessInformation.PledgedWeight - GINProcessInformation.IssuedWeight;
            if (netWeight <= 0)
            {
                throw new Exception(string.Format("Invalid Weight: The calculated net weight ({0}Kg) shall be positive", netWeight));
            }
            decimal trancNetWeight = Math.Truncate(netWeight * 100 + 0.9m) / 100;
            decimal trancExpectedWeight = Math.Truncate(expectedWeight * 100 + 0.9m) / 100;
            if ((trancExpectedWeight - trancNetWeight) < 0)
            {
                throw new Exception(string.Format("Invalid Weight: The calculated net weight({0}Kg) shall not be greater than " +
                    "the PUN balance({1}Kg)", trancNetWeight, trancExpectedWeight));
            }
            //var supervisorWeighers = from weigher in weight.Weighers
            //                         where (weigher.IsSupervisor)
            //                         select weigher;
            //if (supervisorWeighers.Count() != 1)
            //{
            //    throw new Exception("Scaling requires exactly on supervisor");
            //}

        }
        public void ValidateWorker(WorkerInformation worker)
        {

        }

        public void ValidateStack(TruckStackInfo stack)
        {
            if (stack.Bags <= 0)
            {
                throw new Exception("Number of loaded bags shall be a positive number"); 
            }
        }

        #endregion
        private float? lotSize = null;
        private decimal LotSize
        {
            get {
                if (lotSize == null)
                {
                    lotSize = CommodityGradeBLL.GetCommodityGradeLotSizeById(GINProcessInformation.CommodityGradeId);
                }
                return Convert.ToDecimal(lotSize.HasValue ? lotSize.Value : 0.0); 
            }
        }

        private decimal CorrectedLotSize
        {
            get
            {
                decimal lotSize = LotSize;
                if (lotSize > 0.0m)
                {
                    CommodityGradeBLL comGrade = CommodityGradeBLL.GetCommodityGrade(ginProcessInfomation.CommodityGradeId);
                    CommodityGradeBLL com = CommodityGradeBLL.GetCommodityById(comGrade.CommodityId);
                    if (com.UnitOfMeasure == "Quintal")
                    {
                        lotSize /= 100;
                    }
                }
                return lotSize;
            }
        }

        private float? lotSizeInBags = null;
        private decimal LotSizeInBags
        {
            get {
                if (lotSizeInBags == null)
                    lotSizeInBags = CommodityGradeBLL.GetCommodityGradeLotSizeInBagsById(ginProcessInfomation.CommodityGradeId);
                return Convert.ToDecimal(lotSizeInBags.HasValue ? lotSizeInBags.Value : 0.0); 
            }
        }

        private void ConfirmSupervisor(List<WorkerInformation> workers)
        {
            var supervisors = from worker in workers
                              where worker.IsSupervisor
                              select worker;
            if (supervisors.Count() != 1)
            {
                throw new Exception("Requires exactly one supervisor");
            }
        }

        private void RecalculateGIN(GINTruckInfo theTruck)
        {
            GINInfo gin = theTruck.GIN;
            gin.NetWeight = CalculateNetWeight(theTruck.TruckId);
            gin.GrossWeight = theTruck.Weight.GrossWeight - theTruck.Weight.TruckWeight;
            decimal fractionGINQuantityAdjusted = (decimal)1;
            decimal weightTolerance = decimal.Parse(ConfigurationManager.AppSettings["WeightTolerance"]);
            int wholeGINQuantity = (int)(gin.NetWeight / CorrectedLotSize);
            decimal fractionGINQuantity = gin.NetWeight - wholeGINQuantity * CorrectedLotSize;
            if (fractionGINQuantity < weightTolerance * CorrectedLotSize)
                fractionGINQuantityAdjusted = 0;
            else if (CorrectedLotSize - fractionGINQuantity > weightTolerance * CorrectedLotSize)
                fractionGINQuantityAdjusted = fractionGINQuantity / CorrectedLotSize;
            gin.Quantity = Math.Truncate((wholeGINQuantity + fractionGINQuantityAdjusted * 10000 + 0.9m)) / 10000;

        }

        private SqlTransaction SetGinStatus(Guid truckId, GINStatusType status)
        {
            var trucks = from truck in ginProcessInfomation.Trucks
                       where truck.TruckId == truckId
                       select truck;
            GINTruckInfo theTruck = trucks.ElementAt(0);
            WorkType workerType = WorkType.GINLoading;
            GINInfo gin = theTruck.GIN;
            gin.Status = (int)status;
            Queue<object> queue = new Queue<object>(new object[] { gin });
            Dictionary<string, object> contextParameters = new Dictionary<string, object>();
            contextParameters.Add("CurrentUser", new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]));
            contextParameters.Add("WarehouseCode", SystemLookup.LookupSource.GetLookup("CurrentWarehouse")["WarehouseCode"]);
            switch(status)
            {
                case GINStatusType.Loaded:
                    try
                    {
                        workerType = WorkType.GINLoading;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("{0} {1}", workerType, ex.Message));
                    }
                break;
                case GINStatusType.Scaled:
                    try
                    {
                        workerType = WorkType.GINScaling;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("{0} {1}", workerType, ex.Message));
                    }
                break;
            }
            try
            {
                return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                    queue, contextParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Your GIN information couldn't be saved to the database", ex);
            }
        }

        public SqlTransaction SaveGINEditingRequest(GINEditingRequest request)
        {
            return PersistenceTransactionFactory.CreatePersistenceTransaction().Persist(
                new Queue<object>(new object[] { request }),
                new Dictionary<string, object>());
        }

        public TrackingReportData PUNTrackingReportData
        {
            get
            {
                return new TrackingReportData(
                    GINProcessInformation.TransactionId,
                    lookupSource.GetLookup("PUNAgent")[GINProcessInformation.PickupNoticeAgentId],
                    lookupSource.GetLookup("CommodityGrade")[GINProcessInformation.CommodityGradeId],
                    GINProcessInformation.DateReceived,
                    lookupSource.GetLookup("Warehouse")[new Guid(lookupSource.GetLookup("CurrentWarehouse")["Id"])],
                    ginProcessInfomation.Quantity,
                    ginProcessInfomation.Weight
                    );
            }
        }

        public List<GINTrackingReportData> GINTrackingReportData
        {
            get
            {
                return (from truck in GINProcessInformation.Trucks
                        where (truck.GIN == null) && (truck.Status == (int)GINStatusType.ReadyToLoad)
                        select new GINTrackingReportData()
                        {
                            Client = GINProcessInformation.AgentName,
                            Commodity = lookupSource.GetLookup("CommodityGrade")[GINProcessInformation.CommodityGradeId],
                            DateReceived = GINProcessInformation.DateReceived,
                            Warehouse = lookupSource.GetLookup("Warehouse")[new Guid(lookupSource.GetLookup("CurrentWarehouse")["Id"])],
                            DriverName = truck.DriverName,
                            PlateNo = truck.PlateNo,
                            TrackingId = truck.TransactionId
                        }).ToList();
            }
        }

        #region Nested Classes
        [Serializable]
        private class GINProcessLookup : ILookupSource
        {
            private PickupNoticeBLL pickupNotice;
            private GINProcessInfo ginProcessInfomation;
            public GINProcessLookup(GINProcessInfo ginProcessInfomation)
            {
                this.ginProcessInfomation = ginProcessInfomation;
            }
            public GINProcessLookup() { }

            public PickupNoticeBLL PickupNotice
            {
                get
                {
                    if (pickupNotice == null)
                    {
                        pickupNotice = new PickupNoticeBLL(ginProcessInfomation.PickupNoticeId);
                    }
                    return pickupNotice;
                }
            }

            #region ILookupSource Members

            public System.Collections.Generic.IDictionary<object, string> GetLookup(string lookupName)
            {

                IDictionary<object, string> lookup = new Dictionary<object, string>();
                switch (lookupName)
                {
                    case "InventoryController":
                        lookup = SystemLookup.LookupSource.GetLookup("InventoryController");
                        break;
                    case "CommodityGrade":
                        lookup = SystemLookup.LookupSource.GetLookup("CommodityGrade");
                        break;
                    case "Client":
                        lookup = SystemLookup.LookupSource.GetLookup("Client");
                        break;
                    case "ClientId":
                        lookup = SystemLookup.LookupSource.GetLookup("ClientId");
                        break;
                    case "NIDType":
                        lookup = SystemLookup.LookupSource.GetLookup("NIDType");
                        break;
                    case "Warehouse":
                        lookup = SystemLookup.LookupSource.GetLookup("Warehouse");
                        break;
                    case "WarehouseManager":
                        lookup = SystemLookup.LookupSource.GetLookup("WarehouseManager");
                        break;
                    case "VerifyingClerk":
                        lookup = SystemLookup.LookupSource.GetLookup("VerifyingClerk");
                        break;
                    case "PUNAgent":
                        if (ginProcessInfomation != null)
                        {
                            return PickupNotice.PUNLookupSource.GetLookup("PUNAgent");
                        }
                        else
                        {
                            throw new Exception("This GIN Lookup may not be used in this context");
                        }
                    case "Status":
                        GINProcessStatusType[] statusValues = (GINProcessStatusType[])Enum.GetValues(typeof(GINProcessStatusType));
                        foreach (GINProcessStatusType statusValue in statusValues)
                        {
                            lookup.Add((int)statusValue, Enum.GetName(typeof(GINProcessStatusType), statusValue));
                        }
                        break;
                    case "Loader":
                        return SystemLookup.LookupSource.GetLookup("Loader");
                    case "Weigher":
                        return SystemLookup.LookupSource.GetLookup("Weigher");
                    case "Sampler":
                        return SystemLookup.LookupSource.GetLookup("Sampler");
                    case "Grader":
                        return SystemLookup.LookupSource.GetLookup("Grader");
                    case "Gatekeeper":
                        return SystemLookup.LookupSource.GetLookup("Gatekeeper");
                    case "BagType":
                        BagTypeBLL.GetCommodityGradeBagTypes(ginProcessInfomation.CommodityGradeId).
                            ForEach(bagType=>lookup.Add(bagType.Id, bagType.BagTypeName));
                        return lookup;
                        //return SystemLookup.LookupSource.GetLookup("BagType");
                    case "BagWeight":
                        return SystemLookup.LookupSource.GetLookup("BagWeight");
                    case "WorkerStatus":
                        return SystemLookup.LookupSource.GetLookup("WorkerStatus");
                    case "CurrentWarehouse":
                        return SystemLookup.LookupSource.GetLookup("CurrentWarehouse");
                    case "Shed":
                        IDictionary<object,string> sheds = SystemLookup.LookupSource.GetLookup("Shed");
                        foreach (Guid shedId in sheds.Keys)
                        {
                            lookup.Add(string.Format("{0}_{1}_{2}", shedId, ginProcessInfomation.CommodityGradeId, ginProcessInfomation.ProductionYear), sheds[shedId]);
                        }
                        break;
                    case "Stack":
                        return SystemLookup.LookupSource.GetLookup("Stack");
                    case "AvailableWeigher":
                        return SystemLookup.LookupSource.GetLookup("Weigher");
                    case "AvailableLoader":
                        return SystemLookup.LookupSource.GetLookup("Loader");
                    case "SamplingStatus":
                        lookup.Add((int)0, "New");
                        lookup.Add((int)1, "Waiting");
                        lookup.Add((int)2, "Completed");
                        break;
                    case "SamplingResultStatus":
                        lookup.Add((int)0, "New");
                        lookup.Add((int)1, "Waiting");
                        lookup.Add((int)2, "Completed");
                        break;
                    case "GINStatus":
                        GINStatusType[] ginStatusValues = (GINStatusType[])Enum.GetValues(typeof(GINStatusType));
                        foreach (GINStatusType ginStatusValue in ginStatusValues)
                        {
                            lookup.Add((int)ginStatusValue, Enum.GetName(typeof(GINStatusType), ginStatusValue));
                        }
                        break;
                    case "TruckStatus":
                        lookup.Add((int)0, "On Queue");
                        lookup.Add((int)1, "Weighed");
                        lookup.Add((int)2, "Loaded");
                        lookup.Add((int)3, "GIN Issued");
                        lookup.Add((int)4, "Approved");
                        break;
                    case "EmployeeRole":
                        return SystemLookup.LookupSource.GetLookup("EmployeeRole");
                }
                return lookup;
            }

            public System.Collections.Generic.IDictionary<string, object> GetInverseLookup(string lookupName)
            {
                IDictionary<string, object> lookup = new Dictionary<string, object>();
                if (lookupName == "Status")
                {
                    lookup.Add("[Select Status]", 7);
                    GINProcessStatusType[] statusValues = (GINProcessStatusType[])Enum.GetValues(typeof(GINProcessStatusType));
                    foreach (GINProcessStatusType statusValue in statusValues)
                    {
                        lookup.Add(Enum.GetName(typeof(GINProcessStatusType), statusValue), (int)statusValue);
                    }
                }
                else if (lookupName == "GINStatus")
                {
                    lookup.Add("[Select Status]", 6);
                    GINStatusType[] statusValues = (GINStatusType[])Enum.GetValues(typeof(GINStatusType));
                    foreach (GINStatusType statusValue in statusValues)
                    {
                        lookup.Add(Enum.GetName(typeof(GINStatusType), statusValue), (int)statusValue);
                    }
                }
                else if (lookupName == "CommodityGrade")
                {
                    lookup.Add("[Select Commodity Grade]", string.Empty);
                    foreach(CommodityGradeBLL comGrade in CommodityGradeBLL.GetAllCommodityDetail())
                    {
                        if(!lookup.ContainsKey(comGrade.GradeName))
                            lookup.Add(comGrade.GradeName, comGrade.CommodityGradeId);
                    }
                }
                return lookup;
            }

            #endregion
        }

        #endregion

    }

    public enum GINStatusType : int
    {
        ReadyToLoad = 0,
        Loaded,
        Scaled,
        GINGenerated,
        GINSigned,
        GINApproved
    }

    public enum GINProcessStatusType : int
    {
        New=0,
        Suspended,
        Ok_to_Load,
        Sampled,
        Graded,
        Completed,
        Aborted,
        Canceled
    }
}
