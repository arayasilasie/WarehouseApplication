using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public class GINProcessWrapper
    {
        public static IGINProcess GetGINProcess(bool isPostBack)
        {
            IGINProcess ginProcess;
            if (!isPostBack)
            {
                PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
                string transactionId = (string)transferedData.GetTransferedData("TransactionId");
                Guid ginProcessId = Guid.Empty;
                if (transactionId == null)
                {
                    ginProcessId = (Guid)transferedData.GetTransferedData("GINProcessId");
                }
                object oIsGINTransaction = transferedData.GetTransferedData("IsGINTransaction");
                if (transactionId == null)
                {
                    ginProcess = new GINAuditController(ginProcessId);
                }
                else if (oIsGINTransaction != null)
                {
                    ginProcess = new GINAuditController(transactionId, (bool)oIsGINTransaction);
                }
                else
                {
                    ginProcess = new GINAuditController(transactionId);
                }

                HttpContext.Current.Session["GIN-Process-Information"] = ginProcess.GINProcessInformation;
            }
            else
            {
                ginProcess = new GINAuditController();
                ginProcess.GINProcessInformation = (GINProcessInfo)HttpContext.Current.Session["GIN-Process-Information"];
            }
            return ginProcess;
        }

        public static GINProcessInfo GetGINProcessInformation(bool isPostBack)
        {
            GINProcessInfo ginProcessInfo;
            if (!isPostBack)
            {
                PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
                string transactionId = (string)transferedData.GetTransferedData("TransactionId");
                IGINProcess ginProcess = new GINAuditController(transactionId);
                ginProcessInfo = ginProcess.GINProcessInformation;
                HttpContext.Current.Session["GIN-Process-Information"] = ginProcessInfo;
            }
            else
            {
                ginProcessInfo = (GINProcessInfo)HttpContext.Current.Session["GIN-Process-Information"];
            }
            return ginProcessInfo;
        }

        public static void RemoveGINProcessInformation()
        {
            HttpContext.Current.Session.Remove("GIN-Process-Information");
        }

        public static void SaveAvailabilityVerification(PUNAcknowledgementInformation acknowledgement)//, AuditTrailWrapper auditTrail)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.SaveAvailabilityVerification(acknowledgement);
                //if (!auditTrail.Save())
                //    throw new Exception("Failed to save audit trail!");
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void ClosePun()
        {
            IGINProcess ginProcess = GetGINProcess(true);
            bool bPunClosed = (ginProcess.GINProcessInformation.RemainingWeight <= 0);
            if (bPunClosed)
            {
                CompleteWorkflowTask(ginProcess.GINProcessInformation.TransactionId);
            }
        }

        public static void AbortPun()
        {
            IGINProcess ginProcess = GetGINProcess(true);
            IPickupNotice pun = new PickupNoticeBLL(ginProcess.GINProcessInformation.PickupNoticeId);
            PickupNoticeInformation puni = new PickupNoticeInformation();
            SqlTransaction transaction = null;
            try
            {
                puni.Copy(pun.PickupNoticeInformation);
                transaction = pun.Aborted();
                CompleteWorkflowTask(ginProcess.GINProcessInformation.TransactionId);
                transaction.Commit();
                AuditTrailWrapper punAudit = new AuditTrailWrapper(AuditTrailWrapper.PUNAbortion,
                    new object[][] { new object[] { puni, pun.PickupNoticeInformation } }, "PUN Abortion");
                if (!punAudit.Save())
                    throw new Exception("Failed to save audit trail!");
                UpdatePUNStatus(
                    pun.PickupNoticeInformation.PickupNoticeId,
                    pun.LookupSource.GetLookup("Status")[pun.PickupNoticeInformation.Status]);
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void SaveTruckRegistration()
        {
            IGINProcess ginProcess = GetGINProcess(true);
            bool bPunClosed = Math.Abs(ginProcess.GINProcessInformation.PledgedWeight - ginProcess.GINProcessInformation.IssuedWeight) <= 0.0001M;
            if (bPunClosed)
            {
                CompleteWorkflowTask(ginProcess.GINProcessInformation.TransactionId);
            }
            else
            {
                SqlTransaction transaction = null;
                string transactionId = null;
                if (ginProcess.GINProcessInformation.RegisteredTrucks.Count == 0)
                {
                    throw new Exception("No Truck has been registered");
                }
                foreach (GINTruckInfo truck in ginProcess.GINProcessInformation.RegisteredTrucks)
                {
                    try
                    {
                        transactionId = StartWorkflowTask(new Guid("e92633bb-3a48-4ddd-ae13-970bb32ddf36"));
                        if (transactionId != string.Empty)
                        {
                            truck.TransactionId = transactionId;
                        }
                        else
                        {
                            throw new Exception("Failed to start GIN Transaction");
                        }
                        transaction = ginProcess.SaveTruck(truck);
                        if (!WarehouseTrackingNoBLL.Save(transactionId, transaction))
                        {
                            throw new Exception("Failed to start GIN Transaction");
                        }
                        if (truck.Status == (int)GINStatusType.ReadyToLoad)
                        {
                            //ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                            //eng.OpenTransaction(,
                            //    new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]),
                            //    new string[] { "" }, "1", out transactionId);
                            //truck.TransactionId = transactionId;
                            //string transactionId = StartWorkflowTask(new Guid("e92633bb-3a48-4ddd-ae13-970bb32ddf36"));
                            //if (transactionId != string.Empty)
                            //{
                            //    truck.TransactionId = transactionId;
                            //}
                            //ginProcess.SaveTruck(truck, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transactionId != null)
                        {
                            RemoveWorkflowTask(transactionId);
                        }
                        try
                        {
                            if (transaction != null)
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex2)
                        {
                            throw ex2;
                        }
                        throw ex;
                    }
                }
            }
        }

        public static void RemoveWorkflowTask(string transactionId)
        {
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            eng.RemoveTransaction(transactionId);
        }

        public static void CompleteAvailabilityVerification()
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                GINProcessInfo originalGinProcessInfo = new GINProcessInfo();
                originalGinProcessInfo.Copy(ginProcess.GINProcessInformation);
                transaction = ginProcess.CompleteAvailabilityVerification();
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper("Inventory Verification", 
                //    new object[][]{new object[]{originalGinProcessInfo, ginProcess.GINProcessInformation}});
                //if (!auditTrail.Save())
                //    throw new Exception("Failed to save audit trail!");
                CompleteWorkflowTask(ginProcess.GINProcessInformation.TransactionId);
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }
        public static void CompleteLoading(Guid truckId)
        {
            //check if page being processed outside a workflow context
            PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);

            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading,"GIN Process");
                //GINInfo originalGIN = new GINInfo();
                //originalGIN.Copy(ginProcess.GINProcessInformation.Trucks[0].GIN);
                transaction = ginProcess.CompleteLoading(truckId);
                GINTruckInfo theTruck = ginProcess.GINProcessInformation.Trucks.Find(trk => trk.TruckId == truckId);
                foreach (var loadedStack in theTruck.Load.Stacks)
                {
                    InventoryServices.GetInventoryService().LoadFromStack(loadedStack.StackId, loadedStack.Bags, 0, transaction);
                    auditTrail.AddChange(
                        new TruckLoadInfo(),
                        new TruckLoadInfo(loadedStack.TruckId, theTruck.Load.DateLoaded, theTruck.Load.BagType, string.Empty),
                        AuditTrailWrapper.NewRecord);
                    if (!auditTrail.Save())
                        throw new Exception("Unable to log audit trail");
                }
                //auditTrail.AddChange(originalGIN, ginProcess.GINProcessInformation.Trucks[0].GIN);
                //if (!auditTrail.Save())
                //    throw new Exception("Failed to save audit trail!");
                CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);
                //CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void SaveGIN(Guid truckId)//, AuditTrailWrapper auditTrail)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.SaveGIN(truckId);
                //if (!auditTrail.Save())
                //    throw new Exception("Failed to save audit trail!");
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void GINGenerated(Guid truckId)
        {
            //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing);
            IGINProcess ginProcess = GetGINProcess(true);
            //GINInfo originalGin = new GINInfo();
            //originalGin.Copy(ginProcess.GINProcessInformation.Trucks[0].GIN);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.GINGenerated(truckId);
                //auditTrail.AddChange(originalGin, ginProcess.GINProcessInformation.Trucks[0].GIN);
                //if (!auditTrail.Save())
                //    throw new Exception("Failed to save audit trail!");
                CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }
        
        public static void CompleteScaling(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.CompleteScaling(truckId);
                TruckRegistrationInfo truckReistration = new TruckRegistrationInfo()
                {
                    GINTruck = ginProcess.GINProcessInformation.Trucks.Find(t => t.TruckId == truckId)
                };
                TruckWeight trackWeight= truckReistration.TruckWeight;
                trackWeight.Id = Guid.NewGuid();
                trackWeight.Save(transaction);
                CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void GINSigned(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.GINSigned(truckId);
                CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void GINApproved(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.GINApproved(truckId);
                NotifyGIN(truckId);
                CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);

                if (ginProcess.GINProcessInformation.RemainingWeight <= 0)
                {
                    IPickupNotice pun = new PickupNoticeBLL(ginProcess.GINProcessInformation.PickupNoticeId);
                    PickupNoticeInformation puni = new PickupNoticeInformation();
                    puni.Copy(pun.PickupNoticeInformation);
                    pun.GINIssued(transaction);
                    UpdatePUNStatus(
                        pun.PickupNoticeInformation.PickupNoticeId, 
                        pun.LookupSource.GetLookup("Status")[pun.PickupNoticeInformation.Status]);
                }
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }

        }

        public static void TruckLeftCompound(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.SaveGIN(truckId);
                CompleteWorkflowTask(ginProcess.GINProcessInformation.Trucks[0].TransactionId);
                ClosePun();
                transaction.Commit();
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
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
        }

        public static void SaveTruckInformation(Guid truckId)//, AuditTrailWrapper auditTrail)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            if (!PostGINEditRequest(ginProcess, truckId))//, auditTrail))
            {
                SqlTransaction transaction = null;
                try
                {
                    transaction = ginProcess.SaveTruckInformation(truckId);
                    transaction.Commit();
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
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                    throw ex;
                }
            }
        }

        public static void SaveLoading(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            if (!PostGINEditRequest(ginProcess, truckId))
            {
                SqlTransaction transaction = null;
                try
                {
                    transaction = ginProcess.SaveLoading(truckId);
                    transaction.Commit();
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
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                    throw ex;
                }
            }
        }

        public static void SaveScaling(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            if (!PostGINEditRequest(ginProcess, truckId))
            {
                SqlTransaction transaction = null;
                try
                {
                    transaction = ginProcess.SaveScaling(truckId);
                    transaction.Commit();
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
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                    throw ex;
                }
            }
        }

        public static string StartWorkflowTask(Guid transactionType)
        {
            string transactionNo = null;
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            eng.OpenTransaction(transactionType,
                new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]),
                new string[] { "" }, WarehouseBLL.GetWarehouseCode(UserBLL.GetCurrentWarehouse()), out transactionNo);
            return transactionNo;
        }

        public static void CompleteWorkflowTask(string transactionId)
        {
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            ECXWF.CMessage msg = (ECXWF.CMessage)HttpContext.Current.Session["msg"];
            msg.IsCompleted = true;
            eng.Response(transactionId, msg);
        }

        public static void NotifyGIN(Guid truckId)
        {
            IGINProcess ginProcess = GetGINProcess(true);
            ILookupSource lookup = ginProcess.LookupSource;
            GINProcessInfo ginProcessInfo = ginProcess.GINProcessInformation;
            GINTruckInfo truck = (from trk in ginProcessInfo.Trucks where trk.TruckId == truckId select trk).Single();

            ECXCD.WR wr = new WarehouseApplication.ECXCD.WR();
            wr.SaveGIN(
                truck.GIN.GINId,
                truck.GIN.GINNo,
                ginProcessInfo.PickupNoticeId,
                Convert.ToDouble(truck.GIN.GrossWeight),
                Convert.ToDouble(truck.GIN.NetWeight),
                Convert.ToDouble(truck.GIN.Quantity),
                truck.GIN.DateIssued,
                truck.GIN.SignedByClient,
                truck.GIN.DateApproved,
                truck.GIN.ApprovedBy,
                truck.Load.Remark + Environment.NewLine + truck.Weight.Remark,
                lookup.GetLookup("GINStatus")[truck.GIN.Status]);
        }

        public static void RejectGINProcess()
        {
            IGINProcess ginProcess = GetGINProcess(true);
            SqlTransaction transaction = null;
            try
            {
                transaction = ginProcess.AbortGINProcess();
                IPickupNotice pun = new PickupNoticeBLL(ginProcess.GINProcessInformation.PickupNoticeId);
                UpdatePUNStatus(
                    pun.PickupNoticeInformation.PickupNoticeId,
                    pun.LookupSource.GetLookup("Status")[pun.PickupNoticeInformation.Status]);
                CompleteWorkflowTask(ginProcess.GINProcessInformation.TransactionId);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw ex;
            }
        }

        public static void UpdatePUNStatus(Guid pickupNoticeId, string punStatus)
        {
            ECXCD.WR wr = new WarehouseApplication.ECXCD.WR();
            wr.UpdatePUNStatus(pickupNoticeId, punStatus);
        }

        public static bool PostGINEditRequest(IGINProcess ginProcess, Guid truckId)//, AuditTrailWrapper auditTrail)
        {
            GINProcessInfo ginProcessInformation = ginProcess.GINProcessInformation;
            var editedTruck = (from truck in ginProcessInformation.Trucks
                              where truck.TruckId == truckId
                              select truck).FirstOrDefault();
            if (editedTruck.GIN.Status == (int)GINStatusType.GINApproved)
            {
                StringBuilder sb = new StringBuilder();
                TextWriter writer = new StringWriter(sb);
                XmlSerializer s = new XmlSerializer(typeof(GINProcessInfo));
                s.Serialize(writer, ginProcessInformation);
                GINEditingRequest request = new GINEditingRequest()
                    {
                        DeliveryReceivedId = ginProcessInformation.GINProcessId,
                        TargetPage = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Substring(2),
                        ProposedChange = sb.ToString(),
                        OldTransactionId=editedTruck.TransactionId
                    };
                SqlTransaction transaction = null;
                try
                {
                    request.TransactionId = StartWorkflowTask(new Guid("18FC27D4-5905-4A21-84CC-719601FBAC70"));
                    transaction = ginProcess.SaveGINEditingRequest(request);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    if (request != null)
                    {
                        RemoveWorkflowTask(request.TransactionId);
                    }
                    throw ex;
                }
            }
            else
            {
                return false;
            }
        }

        public static void ApproveGINEditRequest()
        {
            IGINProcess originalGinProcess = GetGINProcess(true);
            PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
            GINEditingRequest request = (GINEditingRequest)transferedData.GetTransferedData("GINEditingRequest");
            XmlSerializer s = new XmlSerializer(typeof(GINProcessInfo));
            GINProcessInfo ginProcessInformation = (GINProcessInfo)s.Deserialize(new StringReader(request.ProposedChange));
            ginProcessInformation.Trucks[0].TransactionId = request.TransactionId;
            ginProcessInformation.Trucks[0].Status = (int)GINStatusType.GINGenerated;
            IGINProcess ginProcess = new GINAuditController();
            ginProcess.GINProcessInformation = ginProcessInformation;
            SqlTransaction transaction = null;
            try
            {
                switch (request.TargetPage)
                {
                    case "EditTruckInformation.aspx":
                        transaction = ginProcess.SaveTruckInformation(ginProcessInformation.Trucks[0].TruckId);
                        break;
                    case "EditTruckLoading.aspx":
                        transaction = ginProcess.SaveLoading(ginProcessInformation.Trucks[0].TruckId);
                        break;
                    case "EditTruckScaling.aspx":
                        transaction = ginProcess.SaveScaling(ginProcessInformation.Trucks[0].TruckId);
                        break;
                }
                transaction.Commit();
                CompleteWorkflowTask(request.TransactionId);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
        }

        public static void RejectGINEditRequest()
        {
            PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
            GINEditingRequest request = (GINEditingRequest)transferedData.GetTransferedData("GINEditingRequest");
            ECXWF.ECXEngine engine = new WarehouseApplication.ECXWF.ECXEngine();
            engine.CloseTransaction(request.TransactionId);
        }
    }

    public class PUNWrapper
    {
        public static IPickupNotice GetPUN(bool isPostBack)
        {
            IPickupNotice pun;
            if (!isPostBack)
            {
                PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
                Guid punId = (Guid)transferedData.GetTransferedData("PickupNoticeId");
                pun = new PickupNoticeBLL(punId);
                HttpContext.Current.Session["Pickup-Notice-Information"] = pun.PickupNoticeInformation;
            }
            else
            {
                pun = new PickupNoticeBLL();
                pun.PickupNoticeInformation = (PickupNoticeInformation)HttpContext.Current.Session["Pickup-Notice-Information"];
            }
            return pun;
        }

        public static PickupNoticeInformation GetPickupNoticeInformation(bool isPostBack)
        {
            PickupNoticeInformation punInfo;
            if (!isPostBack)
            {
                PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
                Guid punId = (Guid)transferedData.GetTransferedData("PickupNoticeId");
                IPickupNotice pun = new PickupNoticeBLL(punId);
                punInfo = pun.PickupNoticeInformation;
                HttpContext.Current.Session["Pickup-Notice-Information"] = punInfo;
            }
            else
            {
                punInfo = (PickupNoticeInformation)HttpContext.Current.Session["Pickup-Notice-Information"];
            }
            return punInfo;
        }

        public static void RemovePickupNoticeInformation()
        {
            HttpContext.Current.Session.Remove("Pickup-Notice-Information");
        }

        public static void AcknowledgePickupNotice(PUNAcknowledgementInformation puna)//, AuditTrailWrapper auditTrail)
        {
            IPickupNotice pun = GetPUN(true);
            AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.PUNInitiation, "PUN Process");
            PUNAcknowledgementInformation originalPUN = new PUNAcknowledgementInformation();
            originalPUN.Copy(pun.PUNAInformation);
            auditTrail.AddChange(new object[][] { new object[] { originalPUN, puna, AuditTrailWrapper.NewRecord } });
            SqlTransaction transaction = null;
            string transactionId = null;
            try
            {
                transaction = pun.AcknowledgePickupNotice(puna);
                transactionId = StartPUNWorkflow();
                if (transactionId != string.Empty)
                {
                    puna.TransactionId = transactionId;
                    pun.AcknowledgePickupNotice(puna, transaction);
                    if (!WarehouseTrackingNoBLL.Save(transactionId, transaction))
                    {
                        GINProcessWrapper.RemoveWorkflowTask(transactionId);
                        throw new Exception("Failed to start PUN Transaction");
                    }
                }
                else
                {
                    throw new Exception("Failed to start PUN Transaction");
                }
                if (!auditTrail.Save())
                    throw new Exception("Failed to save audit trail");
                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if ((transactionId != null) && (transactionId != string.Empty))
                    {
                        GINProcessWrapper.RemoveWorkflowTask(transactionId);
                    }
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                finally
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
                throw ex;
            }
        }

        public static string StartPUNWorkflow()
        {
            return GINProcessWrapper.StartWorkflowTask(new Guid("5ffa6b18-b4ce-4fb6-aab6-99ebea8a6ade"));
        }
    }

    public class InventoryServiceWrapper
    {
        public static PhysicalCountInfo GetPhysicalCountInformation(bool isPostBack)
        {
            if(!isPostBack)
            {
                PageDataTransfer transferedData = new PageDataTransfer(HttpContext.Current.Request.Path);
                Guid physicalCountId = (Guid)transferedData.GetTransferedData("PhysicalCountId");
                IInventoryServices invertoryServices  = InventoryServices.GetInventoryService();
                PhysicalCountInfo physicalCountInformation = invertoryServices.OpenPhysicalCount(physicalCountId);
                HttpContext.Current.Session["Physical-Count-Information"] = physicalCountInformation;
                return physicalCountInformation;
            }
            else
            {
                return (PhysicalCountInfo)HttpContext.Current.Session["Physical-Count-Information"];
            }
        }

        public static void RemovePhysicalCountInformation()
        {
            HttpContext.Current.Session.Remove("Physical-Count-Information");
        }
    }
}
