using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class GINProcess : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        private ErrorMessageDisplayer errorDisplayer;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            errorDisplayer = new ErrorMessageDisplayer(lblMessage);
            errorDisplayer.ClearErrorMessage();

            transferedData = new PageDataTransfer(Request.Path);

            GINGridViewer1.Driver = TruckGridViewDriver;
            GINDataEditor1.Driver = GINViewConfigurationReader.GetViewConfiguration("GINProcess", "PUNSummary");
            GINDataEditor2.Driver = GINViewConfigurationReader.GetViewConfiguration("GINProcess", "Truck");
            GINDataEditor2.Ok += new EventHandler(GINDataEditor2_Ok);
            GINDataEditor2.Cancel += new EventHandler(GINDataEditor2_Cancel);
            btnAddTruck.Visible = ((WorkflowTaskType)transferedData.GetTransferedData("WorkflowTask") == WorkflowTaskType.LoadTruck) && 
                ((transferedData.GetTransferedData("IsGINTransaction") == null) || 
                 (bool)(transferedData.GetTransferedData("IsGINTransaction")));
            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);
                GINGridViewer1.Lookup = ginProcess.LookupSource;
                GINDataEditor1.Lookup = ginProcess.LookupSource;
                GINDataEditor2.Lookup = ginProcess.LookupSource;
                GINGridViewer1.DataSource = ginProcess.GINProcessInformation.Trucks;
                GINDataEditor1.Setup();
                GINDataEditor2.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GINGridViewer1.DataBind();
            var gridCommands = from command in GINGridViewer1.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }
            if (!IsPostBack)
            {
                GINDataEditor1.DataSource = ginProcess.GINProcessInformation;
                GINDataEditor1.DataBind();
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditTruck")
            {
                GINDataEditor2.IsNew = false;
                var truckToEdit = from truck in ginProcess.GINProcessInformation.Trucks
                                  where truck.TruckId == new Guid((string)e.CommandArgument)
                                  select truck;
                GINDataEditor2.DataSource = truckToEdit.ElementAt(0);
                GINDataEditor2.DataBind();
                mpeTruckDataEditorExtender.Show();
            }
            else if (e.CommandName == "LoadTruck")
            {
                try
                {
                    if (GINDataEditor2.DataSource != null)
                    {
                        ginProcess.SaveTruck((GINTruckInfo)GINDataEditor2.DataSource);
                    }
                    PageDataTransfer truckTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckLoading.aspx");
                    truckTransfer.RemoveAllData();
                    truckTransfer.TransferData["TruckId"] = new Guid((string)e.CommandArgument);
                    truckTransfer.TransferData["GINProcessId"] = ginProcess.GINProcessInformation.GINProcessId;
                    truckTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                    truckTransfer.TransferData["WorkflowTask"] = transferedData.GetTransferedData("WorkflowTask");
                    GINProcessWrapper.RemoveGINProcessInformation();
                    transferedData.RemoveAllData();
                    truckTransfer.Navigate();
                }
                catch (Exception ex)
                {
                    errorDisplayer.ShowErrorMessage(ex.Message);
                }

            }
            else if (e.CommandName == "ScaleTruck")
            {
                try
                {
                    if (GINDataEditor2.DataSource != null)
                    {
                        ginProcess.SaveTruck((GINTruckInfo)GINDataEditor2.DataSource);
                    }
                    PageDataTransfer truckTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckScaling.aspx");
                    truckTransfer.TransferData["TruckId"] = new Guid((string)e.CommandArgument);
                    truckTransfer.TransferData["GINProcessId"] = ginProcess.GINProcessInformation.GINProcessId;
                    truckTransfer.TransferData["WorkflowTask"] = transferedData.GetTransferedData("WorkflowTask");
                    truckTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                    GINProcessWrapper.RemoveGINProcessInformation();
                    transferedData.RemoveAllData();
                    truckTransfer.Navigate();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            else if (e.CommandName == "GenerateGIN")
            {
                try
                {
                    if (GINDataEditor2.DataSource != null)
                    {
                        ginProcess.SaveTruck((GINTruckInfo)GINDataEditor2.DataSource);
                    }
                    PageDataTransfer truckTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/GenerateGIN.aspx");
                    truckTransfer.TransferData["TruckId"] = new Guid((string)e.CommandArgument);
                    truckTransfer.TransferData["GINProcessId"] = ginProcess.GINProcessInformation.GINProcessId;
                    truckTransfer.TransferData["WorkflowTask"] = transferedData.GetTransferedData("WorkflowTask");
                    truckTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                    GINProcessWrapper.RemoveGINProcessInformation();
                    transferedData.RemoveAllData();
                    truckTransfer.Navigate();
                }
                catch (Exception ex)
                {
                    errorDisplayer.ShowErrorMessage(ex.Message);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveTruckInfo();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnCompleteLoading_Click(object sender, EventArgs e)
        {
            try
            {
                SaveTruckInfo();
                ginProcess.CompleteGINProcess();
                //PageDataTransfer transfer = new PageDataTransfer((string)transferedData.GetTransferedData("ReturnPage"));
                //GINProcessWrapper.RemoveGINProcessInformation();
                //transferedData.RemoveAllData();
                //transfer.Navigate();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }
        void GINDataEditor2_Cancel(object sender, EventArgs e)
        {
        }

        void GINDataEditor2_Ok(object sender, EventArgs e)
        {
            if (GINDataEditor2.IsNew)
            {
                ginProcess.AddTruck((GINTruckInfo)GINDataEditor2.DataSource);
                GINGridViewer1.DataBind();
            }
            else
            {
                var editedTruck = from truck in ginProcess.GINProcessInformation.Trucks
                                  where truck.TruckId == ((GINTruckInfo)GINDataEditor2.DataSource).TruckId
                                  select truck;
                if (editedTruck.Count() > 0)
                {
                    editedTruck.ElementAt(0).Copy((GINTruckInfo)GINDataEditor2.DataSource);
                    GINGridViewer1.DataBind();
                }
            }
            SaveTruckInfo();
        }

        protected void btnAddTruck_Click(object sender, EventArgs e)
        {
            GINDataEditor2.IsNew = true;
            GINTruckInfo blankTruck = ginProcess.GetBlankTruck();
            blankTruck.TransactionId = (string)transferedData.GetTransferedData("TransactionId");
            GINDataEditor2.DataSource = blankTruck;
            GINDataEditor2.DataBind();
            mpeTruckDataEditorExtender.Show();
        }

        private void SaveTruckInfo()
        {
            try
            {
                if (GINDataEditor2.DataSource != null)
                {
                    ginProcess.SaveTruck((GINTruckInfo)GINDataEditor2.DataSource);
                }
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        private GINGridViewerDriver TruckGridViewDriver
        {
            get
            {
                WorkflowTaskType task = (WorkflowTaskType)transferedData.GetTransferedData("WorkflowTask");
                string[] workflowTaskNames = Enum.GetNames(typeof(WorkflowTaskType));
                string taskName = Enum.GetName(typeof(WorkflowTaskType), task);

                GINGridViewerDriver truckGridDriver = GINViewConfigurationReader.CopyViewConfiguration("GINProcess", "Truck");
                var commandColumns = from column in truckGridDriver.Columns
                                     where (column.IsCommand)
                                     select column;
                List<GINColumnDescriptor> commandsToRemove = new List<GINColumnDescriptor>();
                foreach (GINColumnDescriptor commandColumn in commandColumns)
                {
                    if (workflowTaskNames.Contains(commandColumn.Name) &&
                        (commandColumn.Name != taskName))
                    {
                        commandsToRemove.Add(commandColumn);
                    }
                }
                foreach (GINColumnDescriptor commandColumn in commandsToRemove)
                {
                    truckGridDriver.Columns.Remove(commandColumn);
                }
                return truckGridDriver;
            }
        }

    }
}

