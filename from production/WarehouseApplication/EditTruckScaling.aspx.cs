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
    public partial class EditTruckScaling : System.Web.UI.Page
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
            TruckWeightEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("EditTruckScaling", "TruckWeight");
            ReturnedBagsDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "ReturnedBags");
            ReturnedBagsGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "ReturnedBagsGrid");
            AddedBagsDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "AddedBags");
            AddedBagsGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "AddedBagsGrid");

            TruckWeightEditor.IsNew = false;

            ReturnedBagsDataEditor.Ok += new EventHandler(ReturnedBagsDataEditor_Ok);
            ReturnedBagsDataEditor.Cancel += new EventHandler(ReturnedBagsDataEditor_Cancel);
            AddedBagsDataEditor.Ok += new EventHandler(AddedBagsDataEditor_Ok);
            AddedBagsDataEditor.Cancel += new EventHandler(AddedBagsDataEditor_Cancel);

            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);
                TruckWeightEditor.Lookup = ginProcess.LookupSource;
                ReturnedBagsDataEditor.Lookup = ginProcess.LookupSource;
                ReturnedBagsGridViewer.Lookup = ginProcess.LookupSource;
                AddedBagsDataEditor.Lookup = ginProcess.LookupSource;
                AddedBagsGridViewer.Lookup = ginProcess.LookupSource;

                TruckWeightEditor.Setup();
                ReturnedBagsDataEditor.Setup();
                AddedBagsDataEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnedBagsGridViewer.DataSource =
                from returnedBags in GINTruckInformation.Weight.ReturnedBags
                select new ReturnedBagsWrapper(returnedBags, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);

            ReturnedBagsGridViewer.DataBind();

            AddedBagsGridViewer.DataSource =
                from addedBags in GINTruckInformation.Weight.AddedBags
                select new ReturnedBagsWrapper(addedBags, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            AddedBagsGridViewer.DataBind();

            var gridCommands = from command in ReturnedBagsGridViewer.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            var addedGridCommands = from command in AddedBagsGridViewer.Driver.Columns
                                    where command.IsCommand
                                    select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in addedGridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                try
                {
                    TruckWeightEditor.DataSource = GINTruckInformation.Weight;
                    TruckWeightEditor.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditReturnedBags")
            {
                ReturnedBagsDataEditor.IsNew = false;
                var returnedBagsToEdit = from returnedBags in GINTruckInformation.Weight.ReturnedBags
                                         where returnedBags.ReturnedBagsId == new Guid((string)e.CommandArgument)
                                         select returnedBags;
                ReturnedBagsDataEditor.DataSource = new ReturnedBagsWrapper(returnedBagsToEdit.ElementAt(0), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
                ReturnedBagsDataEditor.DataBind();
                mpeReturnedBagsDataEditorExtender.Show();
            }
            if (e.CommandName == "EditAddedBags")
            {
                AddedBagsDataEditor.IsNew = false;
                var addedBagsToEdit = from addedBags in GINTruckInformation.Weight.AddedBags
                                      where addedBags.ReturnedBagsId == new Guid((string)e.CommandArgument)
                                      select addedBags;
                AddedBagsDataEditor.DataSource = new ReturnedBagsWrapper(addedBagsToEdit.ElementAt(0), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
                AddedBagsDataEditor.DataBind();
                mpeAddedBagsDataEditorExtender.Show();
            }
        }

        void ReturnedBagsDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeReturnedBagsDataEditorExtender.Hide();
        }

        void ReturnedBagsDataEditor_Ok(object sender, EventArgs e)
        {
            if (ReturnedBagsDataEditor.IsNew)
            {
                ginProcess.AddReturnedBags(GINTruckInformation.Weight.TruckId, ((ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource).RBInfo);
                ReturnedBagsGridViewer.DataBind();
                ReturnedBagsDataEditor.IsNew = false;
            }
            else
            {
                var editedReturnedBags = from returnedBags in GINTruckInformation.Weight.ReturnedBags
                                         where returnedBags.ReturnedBagsId == ((ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource).ReturnedBagsId
                                         select returnedBags;
                if (editedReturnedBags.Count() > 0)
                {
                    editedReturnedBags.ElementAt(0).Copy(((ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource).RBInfo);
                    ReturnedBagsGridViewer.DataBind();
                }
            }
            mpeReturnedBagsDataEditorExtender.Hide();
        }

        void AddedBagsDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeAddedBagsDataEditorExtender.Hide();
        }

        void AddedBagsDataEditor_Ok(object sender, EventArgs e)
        {
            if (((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).StackId == Guid.Empty)
            {
                errorDisplayer.ShowErrorMessage("Stack is required");
            }
            else if (AddedBagsDataEditor.IsNew)
            {
                ginProcess.AddReturnedBags(GINTruckInformation.Weight.TruckId, ((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).RBInfo);
                AddedBagsGridViewer.DataBind();
                AddedBagsDataEditor.IsNew = false;
            }
            else
            {
                var editedAddedBags = from addedBags in GINTruckInformation.Weight.AddedBags
                                      where addedBags.ReturnedBagsId == ((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).ReturnedBagsId
                                      select addedBags;
                if (editedAddedBags.Count() > 0)
                {
                    editedAddedBags.ElementAt(0).Copy(((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).RBInfo);
                    AddedBagsGridViewer.DataBind();
                }
            }
            mpeAddedBagsDataEditorExtender.Hide();
        }

        protected void btnAddReturnedBags_Click(object sender, EventArgs e)
        {
            ReturnedBagsDataEditor.IsNew = true;
            ReturnedBagsWrapper returnedBagsWrapper = new ReturnedBagsWrapper(ginProcess.GetBlankReturnedBags(GINTruckInformation.Weight.TruckId), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            returnedBagsWrapper.Returned = true;
            ReturnedBagsDataEditor.DataSource = returnedBagsWrapper;
            ReturnedBagsDataEditor.DataBind();
            mpeReturnedBagsDataEditorExtender.Show();
        }

        protected void btnAddAddedBags_Click(object sender, EventArgs e)
        {
            AddedBagsDataEditor.IsNew = true;
            ReturnedBagsWrapper addedBagsWrapper = new ReturnedBagsWrapper(ginProcess.GetBlankReturnedBags(GINTruckInformation.Weight.TruckId), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            addedBagsWrapper.Returned = false;
            AddedBagsDataEditor.DataSource = addedBagsWrapper;
            AddedBagsDataEditor.DataBind();
            mpeAddedBagsDataEditorExtender.Show();
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing);
                TruckWeightInfo originalWeight = new TruckWeightInfo();
                originalWeight.Copy(GINTruckInformation.Weight);
                GINTruckInformation.Weight.Copy((TruckWeightInfo)TruckWeightEditor.DataSource);
                //auditTrail.AddChange(originalWeight, GINTruckInformation.Weight);
                GINProcessWrapper.SaveScaling(GINTruckInformation.TruckId);//, auditTrail);
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        private GINTruckInfo GINTruckInformation
        {
            get
            {
                return ginProcess.GINProcessInformation.Trucks[0];
            }
        }

    }
}
