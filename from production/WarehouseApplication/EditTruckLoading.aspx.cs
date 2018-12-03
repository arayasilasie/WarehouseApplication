using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class EditTruckLoading : System.Web.UI.Page
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
            TruckLoadEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("EditTruckLoading", "TruckLoad");
            StackDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("EditTruckLoading", "Stack");
            StackGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("EditTruckLoading", "StackGrid");


            StackDataEditor.Ok += new EventHandler(StackDataEditor_Ok);
            StackDataEditor.Cancel += new EventHandler(StackDataEditor_Cancel);

            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                TruckLoadEditor.Lookup = ginProcess.LookupSource;
                StackDataEditor.Lookup = ginProcess.LookupSource;
                StackGridViewer.Lookup = ginProcess.LookupSource;

                TruckLoadEditor.Setup();
                StackDataEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StackGridViewer.DataSource =
                from stack in GINTruckInformation.Load.Stacks
                select new TruckStackWrapper(stack, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);

            StackGridViewer.DataBind();
            var gridCommands = from command in StackGridViewer.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                TruckLoadEditor.DataSource = GINTruckInformation.Load;
                TruckLoadEditor.DataBind();
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditStack")
            {
                StackDataEditor.IsNew = false;
                var stackToEdit = from stack in GINTruckInformation.Load.Stacks
                                  where stack.TruckStackId == new Guid((string)e.CommandArgument)
                                  select stack;
                StackDataEditor.DataSource = new TruckStackWrapper(stackToEdit.ElementAt(0), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
                StackDataEditor.DataBind();
                mpeStackDataEditorExtender.Show();
            }
        }


        void StackDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeStackDataEditorExtender.Hide();
        }

        void StackDataEditor_Ok(object sender, EventArgs e)
        {
            if (((TruckStackWrapper)StackDataEditor.DataSource).StackId == Guid.Empty)
            {
                errorDisplayer.ShowErrorMessage("Stack is required");
            }
            else if (StackDataEditor.IsNew)
            {
                ginProcess.AddStack(GINTruckInformation.Load.TruckId, ((TruckStackWrapper)StackDataEditor.DataSource).TSInfo);
                StackGridViewer.DataBind();
                TruckLoadEditor.DataSource = GINTruckInformation.Load;
                TruckLoadEditor.DataBind();
                StackDataEditor.IsNew = false;
            }
            else
            {
                var editedStack = from stack in GINTruckInformation.Load.Stacks
                                  where stack.TruckStackId == ((TruckStackWrapper)StackDataEditor.DataSource).TruckStackId
                                  select stack;
                if (editedStack.Count() > 0)
                {
                    editedStack.ElementAt(0).Copy(((TruckStackWrapper)StackDataEditor.DataSource).TSInfo);
                    StackGridViewer.DataBind();
                    TruckLoadEditor.DataSource = GINTruckInformation.Load;
                    TruckLoadEditor.DataBind();
                }
            }
            mpeStackDataEditorExtender.Hide();
        }

        private GINTruckInfo GINTruckInformation
        {
            get
            {
                return ginProcess.GINProcessInformation.Trucks[0];
            }
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading);
                if (TruckLoadEditor.DataSource != null)
                {
                    TruckLoadInfo originalLoad = new TruckLoadInfo();
                    originalLoad.Copy(GINTruckInformation.Load);
                    GINTruckInformation.Load.Copy((TruckLoadInfo)TruckLoadEditor.DataSource);
                    //auditTrail.AddChange(originalLoad, GINTruckInformation.Load);
                }
                GINProcessWrapper.SaveLoading(GINTruckInformation.TruckId);//, auditTrail);
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        protected void btnAddStack_Click(object sender, EventArgs e)
        {
            StackDataEditor.IsNew = true;
            StackDataEditor.DataSource = new TruckStackWrapper(ginProcess.GetBlankTruckStack(GINTruckInformation.Load.TruckId), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            StackDataEditor.DataBind();
            mpeStackDataEditorExtender.Show();
        }
    }
}
