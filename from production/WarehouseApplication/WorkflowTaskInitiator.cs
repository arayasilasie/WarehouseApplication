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
using WarehouseApplication.GINLogic;

namespace WarehouseApplication
{
    public class WorkflowTaskInitiator
    {
        public static void InitiateTask(string msg, string transactionId)
        {
            WorkflowTaskType workflowTask = (WorkflowTaskType)Enum.Parse(typeof(WorkflowTaskType), msg);
            if (workflowTask == WorkflowTaskType.VerifyPUN)
            {
                PageDataTransfer confirmationTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/PickupNoticeAcknowledged.aspx");
                confirmationTransfer.RemoveAllData();
                GINProcessWrapper.RemoveGINProcessInformation();
                confirmationTransfer.TransferData["TransactionId"] = transactionId;
                confirmationTransfer.TransferData["IsGINTransaction"] = false;
                confirmationTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.ApplicationPath + "/ListInbox.aspx";
                confirmationTransfer.Navigate();
            }
            else if (workflowTask == WorkflowTaskType.ConfirmInventory)
            {
                PageDataTransfer confirmationTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/VerifyGINAvailability.aspx");
                confirmationTransfer.RemoveAllData();
                GINProcessWrapper.RemoveGINProcessInformation();
                confirmationTransfer.TransferData["TransactionId"] = transactionId;
                confirmationTransfer.TransferData["IsGINTransaction"] = false;
                confirmationTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.ApplicationPath + "/ListInbox.aspx";
                confirmationTransfer.Navigate();
            }
            else if (workflowTask == WorkflowTaskType.RegisterTrucks)
            {
                PageDataTransfer confirmationTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckRegistration.aspx");
                confirmationTransfer.RemoveAllData();
                GINProcessWrapper.RemoveGINProcessInformation();
                confirmationTransfer.TransferData["TransactionId"] = transactionId;
                confirmationTransfer.TransferData["IsGINTransaction"] = false;
                confirmationTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.ApplicationPath + "/ListInbox.aspx";
                confirmationTransfer.Navigate();
            }
            else if (workflowTask == WorkflowTaskType.ApproveGINEditingRequest)
            {
                PageDataTransfer agerTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ApproveGINEditRequest.aspx");
                agerTransfer.RemoveAllData();
                GINEditingRequest ger = GINProcessBLL.GetGINEditingRequest(transactionId);
                agerTransfer.TransferData["GINEditingRequest"] = ger;
                agerTransfer.TransferData["TransactionId"] = ger.OldTransactionId;
                agerTransfer.TransferData["IsGINTransaction"] = true;
                agerTransfer.TransferData["WorkflowTask"] = workflowTask;
                agerTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.ApplicationPath + "/ListInbox.aspx";
                agerTransfer.Navigate();
            }
            else if ((workflowTask == WorkflowTaskType.GINPreWeighTruck) ||
                 (workflowTask == WorkflowTaskType.LoadTruck) ||
                 (workflowTask == WorkflowTaskType.GINPostWeighTruck) ||
                 (workflowTask == WorkflowTaskType.GenerateGIN) ||
                 (workflowTask == WorkflowTaskType.RecordClientResponse) ||
                 (workflowTask == WorkflowTaskType.ApproveGIN)||
                 (workflowTask == WorkflowTaskType.LeftCompound))
            {
                PageDataTransfer loadDataTransfer = null;
                switch (workflowTask)
                {
                    case WorkflowTaskType.GINPreWeighTruck:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckLoading.aspx");
                        break;
                    case WorkflowTaskType.LoadTruck:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckLoading.aspx");
                        break;
                    case WorkflowTaskType.GINPostWeighTruck:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckScaling.aspx");
                        break;
                    case WorkflowTaskType.GenerateGIN:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckScaling.aspx");
                        break;
                    case WorkflowTaskType.RecordClientResponse:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ClientAcknowledgeGIN.aspx");
                        break;
                    case WorkflowTaskType.ApproveGIN:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ApproveGIN.aspx");
                        break;
                    case WorkflowTaskType.LeftCompound:
                        loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckLeftCompound.aspx");
                        break;
                }
                loadDataTransfer.RemoveAllData();
                GINProcessWrapper.RemoveGINProcessInformation();
                loadDataTransfer.TransferData["TransactionId"] = transactionId;
                loadDataTransfer.TransferData["IsGINTransaction"] = true;
                loadDataTransfer.TransferData["WorkflowTask"] = workflowTask;
                loadDataTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.ApplicationPath + "/ListInbox.aspx";
                loadDataTransfer.Navigate();
            }
        }
    }

    public enum WorkflowTaskType
    {
        VerifyPUN,
        ConfirmInventory,
        RegisterTrucks,
        GINPreWeighTruck,
        LoadTruck,
        GINPostWeighTruck,
        GenerateGIN,
        RecordClientResponse,
        ApproveGIN,
        LeftCompound,
        ApproveGINEditingRequest
    }
}
