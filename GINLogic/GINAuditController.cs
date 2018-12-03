using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WarehouseApplication.BLL;

namespace WarehouseApplication.GINLogic
{
    public class GINAuditController : IGINProcess
    {
        private IGINProcess currentGINProcess;

        public GINAuditController()
        {
            currentGINProcess = new GINProcessBLL();
        }

        public GINAuditController(Guid ginProcessId)
        {
            currentGINProcess = new GINProcessBLL(ginProcessId);
        }

        public GINAuditController(string transactionId)
        {
            currentGINProcess = new GINProcessBLL(transactionId);
        }

        public GINAuditController(string transactionId, bool isGinTransaction)
        {
            currentGINProcess = new GINProcessBLL(transactionId, isGinTransaction);
        }
        #region IGINProcess Members

        public WarehouseApplication.DALManager.ILookupSource LookupSource
        {
            get { return currentGINProcess.LookupSource; }
        }

        public GINProcessInfo GINProcessInformation
        {
            get
            {
                return currentGINProcess.GINProcessInformation;
            }
            set
            {
                currentGINProcess.GINProcessInformation = value;
            }
        }

        public SqlTransaction SaveAvailabilityVerification(PUNAcknowledgementInformation punaInformation)
        {
            IPickupNotice originalPUN = new PickupNoticeBLL(currentGINProcess.GINProcessInformation.PickupNoticeId);
            //ConfirmNoConcurrency(PUNAInformation);
            SqlTransaction transaction = currentGINProcess.SaveAvailabilityVerification(punaInformation);
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.PUNInitiation,
                     new object[][] { new object[] { originalPUN.PUNAInformation, punaInformation, AuditTrailWrapper.ExistingRecord } }, "PUN Process");
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveTruckRegistration()
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.SaveTruckRegistration();
            //ConfirmNoConcurrency(Trucks, Loads, Weight)
            //newly added trucks
            var newTrucks = (from cTruck in currentGINProcess.GINProcessInformation.Trucks
                             where !(from oTruck in originalGIN.GINProcessInformation.Trucks select oTruck.TruckId).Contains(cTruck.TruckId)
                             select new object[] {null, cTruck, AuditTrailWrapper.NewRecord});
            //existing trucks
            var existingTrucks = (from cTruck in currentGINProcess.GINProcessInformation.Trucks
                                  join oTruck in originalGIN.GINProcessInformation.Trucks on cTruck.TruckId equals oTruck.TruckId
                                  select new object[] { oTruck, cTruck, AuditTrailWrapper.ExistingRecord });
            //deleted trucks
            var deletedTrucks = (from oTruck in originalGIN.GINProcessInformation.Trucks
                                 where !(from cTruck in currentGINProcess.GINProcessInformation.Trucks select cTruck.TruckId).Contains(oTruck.TruckId)
                                 select oTruck);
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckRegistration, newTrucks.ToArray(), "PUN Process");
                atw.AddChange(existingTrucks.ToArray());
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;

        }

        public System.Data.SqlClient.SqlTransaction CreateTruck(GINTruckInfo truck)
        {
            return currentGINProcess.CreateTruck(truck);
        }

        public System.Data.SqlClient.SqlTransaction SaveTruck(GINTruckInfo truck)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.SaveTruck(truck);
            //ConfirmNoConcurrency(Truck, Load, Weight)
            var truckChanges = (from oTruck in originalGIN.GINProcessInformation.Trucks
                                  where oTruck.TruckId == truck.TruckId
                                  select new object[]{oTruck, truck, AuditTrailWrapper.ExistingRecord});

            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckRegistration, truckChanges.ToArray(), "GIN Process");
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveTruck(GINTruckInfo truck, System.Data.SqlClient.SqlTransaction transaction)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            currentGINProcess.SaveTruck(truck, transaction);
            //ConfirmNoConcurrency(Truck, Load, Weight)
            var truckChanges = (from oTruck in originalGIN.GINProcessInformation.Trucks
                                where oTruck.TruckId == truck.TruckId
                                select new object[] { oTruck, truck, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckRegistration, truckChanges.ToArray(), "GIN Process");
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveTruckInformation(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.SaveTruckInformation(truckId);
            //ConfirmNoConcurrency(Truck)
            var truckChanges = (from oTruck in originalGIN.GINProcessInformation.Trucks
                                join cTruck in currentGINProcess.GINProcessInformation.Trucks
                                on oTruck.TruckId equals cTruck.TruckId
                                where oTruck.TruckId == truckId
                                select new object[] { oTruck, cTruck, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckRegistration, truckChanges.ToArray(), "GIN Process");
                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveLoading(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.SaveLoading(truckId);
            //ConfirmNoConcurrency(Truck)
            var truckChanges = (from oTruck in originalGIN.GINProcessInformation.Trucks
                                join cTruck in currentGINProcess.GINProcessInformation.Trucks
                                on oTruck.TruckId equals cTruck.TruckId
                                where oTruck.TruckId == truckId
                                select new object[] { oTruck, cTruck, AuditTrailWrapper.ExistingRecord });
            GINTruckInfo origTruck = (GINTruckInfo)truckChanges.Single()[0];
            GINTruckInfo currTruck = (GINTruckInfo)truckChanges.Single()[1];
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading, truckChanges.ToArray(), "GIN Process");
                atw.AddChange(origTruck.GIN, currTruck.GIN, AuditTrailWrapper.ExistingRecord);
                atw.AddChange(origTruck.Load, currTruck.Load, AuditTrailWrapper.ExistingRecord);
                //newly added stacks
                var newStacks = (from cStack in currTruck.Load.Stacks
                                 where !(from oStack in origTruck.Load.Stacks select oStack.TruckId).Contains(cStack.TruckStackId)
                                 select new object[] { null, cStack, AuditTrailWrapper.NewRecord });
                atw.AddChange(newStacks.ToArray());
                //existing stacks
                var existingStacks = (from oStack in origTruck.Load.Stacks
                                      join cStack in currTruck.Load.Stacks
                                      on oStack.TruckStackId equals cStack.TruckStackId
                                      select new object[] { oStack, cStack, AuditTrailWrapper.ExistingRecord });
                atw.AddChange(existingStacks.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveScaling(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.SaveScaling(truckId);
            //ConfirmNoConcurrency(Truck)
            var truckChanges = (from oTruck in originalGIN.GINProcessInformation.Trucks
                                join cTruck in currentGINProcess.GINProcessInformation.Trucks
                                on oTruck.TruckId equals cTruck.TruckId
                                where oTruck.TruckId == truckId
                                select new object[] { oTruck, cTruck, AuditTrailWrapper.ExistingRecord });
            GINTruckInfo origTruck = (GINTruckInfo)truckChanges.Single()[0];
            GINTruckInfo currTruck = (GINTruckInfo)truckChanges.Single()[1];
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing, truckChanges.ToArray(), "GIN Process");
                atw.AddChange(origTruck.GIN, currTruck.GIN, AuditTrailWrapper.ExistingRecord);
                atw.AddChange(origTruck.Weight, currTruck.Weight, AuditTrailWrapper.ExistingRecord);
                //newly added returned bags and added bags
                var newRBs = (from cRB in currTruck.Weight.ReturnedBags
                              where !(from oRB in origTruck.Weight.ReturnedBags select oRB.TruckId).Contains(cRB.ReturnedBagsId)
                              select new object[] { null, cRB, AuditTrailWrapper.NewRecord });
                atw.AddChange(newRBs.ToArray());
                //existing returned bags and added bags
                var existingRBs = (from oRB in origTruck.Weight.ReturnedBags
                                   join cRB in currTruck.Weight.ReturnedBags
                                   on oRB.ReturnedBagsId equals cRB.ReturnedBagsId
                                   select new object[] { oRB, cRB, AuditTrailWrapper.ExistingRecord });
                atw.AddChange(existingRBs.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveGIN(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.SaveGIN(truckId);
            //ConfirmNoConcurrency(GIN)
            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                                join cGIN in currentGINProcess.GINProcessInformation.Trucks
                                on oGIN.TruckId equals cGIN.TruckId
                                where oGIN.TruckId == truckId
                                select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading, ginChanges.ToArray(), "GIN Process");


                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction SaveGIN(Guid truckId, System.Data.SqlClient.SqlTransaction transaction)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            currentGINProcess.SaveGIN(truckId, transaction);
            //ConfirmNoConcurrency(GIN)
            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                              join cGIN in currentGINProcess.GINProcessInformation.Trucks
                              on oGIN.TruckId equals cGIN.TruckId
                              where oGIN.TruckId == truckId
                              select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading, ginChanges.ToArray(), "GIN Process");


                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public void ValidateAvailabilityConfirmation(PUNAcknowledgementInformation punaInformation)
        {
            currentGINProcess.ValidateAvailabilityConfirmation(punaInformation);
        }

        public void ValidateTruckRegistration()
        {
            currentGINProcess.ValidateTruckRegistration();
        }

        public void ValidateGINProcess(GINProcessInfo ginProcess)
        {
            currentGINProcess.ValidateGINProcess(ginProcess);
        }

        public void ValidateTruck(GINTruckInfo truck)
        {
            currentGINProcess.ValidateTruck(truck);
        }

        public void ValidateGIN(GINInfo gin)
        {
            currentGINProcess.ValidateGIN(gin);
        }

        public void ValidateTruckLoading(TruckLoadInfo load)
        {
            currentGINProcess.ValidateTruckLoading(load);
        }

        public void ValidateTruckWeight(TruckWeightInfo weight)
        {
            currentGINProcess.ValidateTruckWeight(weight);
        }

        public void ValidateWorker(WorkerInformation worker)
        {
            currentGINProcess.ValidateWorker(worker);
        }

        public void ValidateStack(TruckStackInfo stack)
        {
            currentGINProcess.ValidateStack(stack);
        }

        public System.Data.SqlClient.SqlTransaction CompleteAvailabilityVerification()
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.CompleteAvailabilityVerification();
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.InventoryConfirmance, "PUN Process");
                atw.AddChange(originalGIN.GINProcessInformation, currentGINProcess.GINProcessInformation, AuditTrailWrapper.ExistingRecord);

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction CompleteLoading(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.CompleteLoading(truckId);

            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                              join cGIN in currentGINProcess.GINProcessInformation.Trucks
                              on oGIN.TruckId equals cGIN.TruckId
                              where oGIN.TruckId == truckId
                              select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading, "GIN Process");
                atw.AddChange(ginChanges.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction CompleteScaling(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.CompleteScaling(truckId);

            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                              join cGIN in currentGINProcess.GINProcessInformation.Trucks
                              on oGIN.TruckId equals cGIN.TruckId
                              where oGIN.TruckId == truckId
                              select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing, "GIN Process");
                atw.AddChange(ginChanges.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction GINGenerated(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.GINGenerated(truckId);

            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                              join cGIN in currentGINProcess.GINProcessInformation.Trucks
                              on oGIN.TruckId equals cGIN.TruckId
                              where oGIN.TruckId == truckId
                              select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing, "GIN Process");
                atw.AddChange(ginChanges.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            } 
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction GINSigned(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.GINSigned(truckId);

            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                              join cGIN in currentGINProcess.GINProcessInformation.Trucks
                              on oGIN.TruckId equals cGIN.TruckId
                              where oGIN.TruckId == truckId
                              select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.GINAcceptance, "GIN Process");
                atw.AddChange(ginChanges.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction GINApproved(Guid truckId)
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.GINApproved(truckId);

            var ginChanges = (from oGIN in originalGIN.GINProcessInformation.Trucks
                              join cGIN in currentGINProcess.GINProcessInformation.Trucks
                              on oGIN.TruckId equals cGIN.TruckId
                              where oGIN.TruckId == truckId
                              select new object[] { oGIN, cGIN, AuditTrailWrapper.ExistingRecord });
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.GINApproval, "GIN Process");
                atw.AddChange(ginChanges.ToArray());

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction CompleteGINProcess()
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.CompleteGINProcess();
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.PUNCompletion, "PUN Process");
                atw.AddChange(originalGIN.GINProcessInformation, currentGINProcess.GINProcessInformation, AuditTrailWrapper.ExistingRecord);

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public System.Data.SqlClient.SqlTransaction AbortGINProcess()
        {
            IGINProcess originalGIN = new GINProcessBLL(currentGINProcess.GINProcessInformation.TransactionId);
            SqlTransaction transaction = currentGINProcess.AbortGINProcess();
            try
            {
                AuditTrailWrapper atw = new AuditTrailWrapper(AuditTrailWrapper.GINAbortion, "PUN Process");
                atw.AddChange(originalGIN.GINProcessInformation, currentGINProcess.GINProcessInformation, AuditTrailWrapper.ExistingRecord);

                if (!atw.Save())
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save audit trail!");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return transaction;
        }

        public GINTruckInfo GetBlankTruck()
        {
            return currentGINProcess.GetBlankTruck();
        }

        public WorkerInformation GetBlankLoader(Guid truckId)
        {
            return currentGINProcess.GetBlankLoader(truckId);
        }

        public WorkerInformation GetBlankWeigher(Guid truckId)
        {
            return currentGINProcess.GetBlankWeigher(truckId);
        }

        public TruckStackInfo GetBlankTruckStack(Guid truckId)
        {
            return currentGINProcess.GetBlankTruckStack(truckId);
        }

        public ReturnedBagsInfo GetBlankReturnedBags(Guid truckId)
        {
            return currentGINProcess.GetBlankReturnedBags(truckId);
        }

        public SampleInfo GetBlankSample()
        {
            return currentGINProcess.GetBlankSample();
        }

        public void DeleteLoader(Guid id)
        {
            currentGINProcess.DeleteLoader(id);
        }

        public void DeleteWeigher(Guid id)
        {
            currentGINProcess.DeleteWeigher(id);
        }

        public void DeleteTruck(Guid id)
        {
            currentGINProcess.DeleteTruck(id);
        }

        public void DeleteTruckStack(Guid truckId, Guid id)
        {
            currentGINProcess.DeleteTruckStack(truckId, id);
        }

        public void AddLoader(Guid truckId, WorkerInformation loader)
        {
            currentGINProcess.AddLoader(truckId, loader);
        }

        public void AddStack(Guid truckId, TruckStackInfo stack)
        {
            currentGINProcess.AddStack(truckId, stack);
        }

        public void AddReturnedBags(Guid truckId, ReturnedBagsInfo returnedBags)
        {
            currentGINProcess.AddReturnedBags(truckId, returnedBags);
        }

        public void AddSample(SampleInfo sample)
        {
            currentGINProcess.AddSample(sample);
        }

        public void AddTruck(GINTruckInfo truck)
        {
            currentGINProcess.AddTruck(truck);
        }

        public void AddWeigher(Guid truckId, WorkerInformation weigher)
        {
            currentGINProcess.AddWeigher(truckId, weigher);
        }

        public GINReportInfo GetGINReport(Guid ginId)
        {
            return currentGINProcess.GetGINReport(ginId);
        }

        public decimal CalculateNetWeight(Guid truckId)
        {
            return currentGINProcess.CalculateNetWeight(truckId);
        }

        public System.Data.SqlClient.SqlTransaction SaveGINEditingRequest(GINEditingRequest request)
        {
            return currentGINProcess.SaveGINEditingRequest(request);
        }

        public TrackingReportData PUNTrackingReportData
        {
            get { return currentGINProcess.PUNTrackingReportData; }
        }

        public List<GINTrackingReportData> GINTrackingReportData
        {
            get { return currentGINProcess.GINTrackingReportData; }
        }

        #endregion


        private void ConfirmNoConcurrency(object original, object current)
        {
        }

    }

    public class AuditTrailWrapper
    {
        private string appMode;
        private string businessProcess;
        private List<object[]> changeObjects = new List<object[]>();

        public AuditTrailWrapper(string appMode, string businessProcess)
        {
            this.appMode = appMode;
            this.businessProcess = businessProcess;
        }

        public AuditTrailWrapper(string appMode, object[][] changes, string businessProcess)
            : this(appMode, businessProcess)
        {
            changeObjects.AddRange(changes);
        }

        public void AddChange(object originalObject, object editedObject, int recordStatus)
        {
            changeObjects.Add(new object[] { originalObject, editedObject, recordStatus });
        }

        public void AddChange(object[][] changes)
        {
            changeObjects.AddRange(changes);
        }
        public bool Save()
        {
            AuditTrailBLL objAt = new AuditTrailBLL();
            bool auditTrailSucceeded = true;
            foreach (object[] change in changeObjects)
            {
                if ((int)change[2] == ExistingRecord)
                {
                    auditTrailSucceeded = (-1 != objAt.saveAuditTrail(change[0], change[1], appMode, UserBLL.GetCurrentUser(), businessProcess));
                    if (!auditTrailSucceeded)
                    {
                        objAt.RoleBack();
                        break;
                    }
                }
                else if ((int)change[2] == NewRecord)
                {
                    auditTrailSucceeded = (-1 != objAt.saveAuditTrail(change[1], appMode, UserBLL.GetCurrentUser(), businessProcess));
                    if (!auditTrailSucceeded)
                    {
                        objAt.RoleBack();
                        break;
                    }
                }
            }
            return auditTrailSucceeded;
        }

        public const string ApproveGINEditRequest = "WH-PUN-AGER";
        public const string GINAcceptance = "WH-PUN-RCR";
        public const string GINApproval = "WH-PUN-AGIN";
        public const string InventoryConfirmance = "WH-PUN-CI";
        public const string TruckRegistration = "WH-PUN-RT";
        public const string TruckLoading = "WH-PUN-LOAD";
        public const string TruckWeighing = "WH-PUN-WEIGH";
        public const string PUNInitiation = "WH-PUN-IP";
        public const string GINEditingRequest = "WH-PUN-GER";
        public const string GINAbortion = "WH-PUN-ABRTGIN";
        public const string PUNCompletion = "WH-PUN-COMPLETED";
        public const string PUNAbortion = "WH-PUN-ABRTPUN";
        public const string InvCount = "WH-INV-COUNT";
        public const int NewRecord = 0;
        public const int ExistingRecord = 1;
        public const int DeletedRecord = 2;
    }

}
