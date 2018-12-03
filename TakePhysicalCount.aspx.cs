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
    public partial class TakePhysicalCount : System.Web.UI.Page
    {
        private IInventoryServices inventoryService;
        private PhysicalCountInfo physicalCountInformation;
        private PageDataTransfer transferedData;
        private ErrorMessageDisplayer errorDisplayer;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            errorDisplayer = new ErrorMessageDisplayer(lblMessage);
            errorDisplayer.ClearErrorMessage();

            transferedData = new PageDataTransfer(Request.Path);
            PhysicalCountEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TakePhysicalCount", "PhysicalCount");
            InspectorDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TakePhysicalCount", "Inspector");
            StackCountDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TakePhysicalCount", "StackCount");
            InspectorGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TakePhysicalCount", "InspectorGrid");
            PhysicalCountEditor.IsNew = false;

            InspectorDataEditor.Ok += new EventHandler(InspectorDataEditor_Ok);
            InspectorDataEditor.Cancel += new EventHandler(InspectorDataEditor_Cancel);

            StackCountDataEditor.Ok += new EventHandler(StackCountDataEditor_Ok);
            StackCountDataEditor.Cancel += new EventHandler(StackCountDataEditor_Cancel);
            try
            {
                inventoryService = InventoryServices.GetInventoryService();
                physicalCountInformation = InventoryServiceWrapper.GetPhysicalCountInformation(IsPostBack);

                PhysicalCountEditor.Lookup = inventoryService.LookupSource;
                InspectorDataEditor.Lookup = inventoryService.LookupSource;
                StackCountDataEditor.Lookup = inventoryService.LookupSource;
                InspectorGridViewer.Lookup = inventoryService.LookupSource;

                PhysicalCountEditor.Setup();
                InspectorDataEditor.Setup();
                StackCountDataEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            errorDisplayer.ClearErrorMessage();
            InspectorGridViewer.DataSource = physicalCountInformation.Inspectors;
            InspectorGridViewer.DataBind();
            var gridCommands = from command in InspectorGridViewer.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                PhysicalCountEditor.DataSource = PhysicalCountInformation;
                PhysicalCountEditor.DataBind();
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditInspector")
            {
                InspectorDataEditor.IsNew = false;
                var inspectorToEdit = from inspector in PhysicalCountInformation.Inspectors
                                      where inspector.Id == new Guid((string)e.CommandArgument)
                                      select inspector;
                InspectorDataEditor.DataSource = inspectorToEdit.ElementAt(0);
                InspectorDataEditor.DataBind();
                mpeInspectorDataEditorExtender.Show();
            }
        }


        void InspectorDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeInspectorDataEditorExtender.Hide();
        }

        void InspectorDataEditor_Ok(object sender, EventArgs e)
        {
            try
            {
                if (((PhysicalCountInspectorInfo)InspectorDataEditor.DataSource).UserId == Guid.Empty)
                {
                    errorDisplayer.ShowErrorMessage("Inspector is required");
                }
                else if (InspectorDataEditor.IsNew)
                {
                    inventoryService.AddInspector(physicalCountInformation, (PhysicalCountInspectorInfo)InspectorDataEditor.DataSource);
                    InspectorGridViewer.DataBind();
                    InspectorDataEditor.IsNew = false;
                }
                else
                {
                    var editedInspector = from inspector in PhysicalCountInformation.Inspectors
                                          where inspector.Id == ((PhysicalCountInspectorInfo)InspectorDataEditor.DataSource).Id
                                          select inspector;
                    if (editedInspector.Count() > 0)
                    {
                        editedInspector.ElementAt(0).Copy((PhysicalCountInspectorInfo)InspectorDataEditor.DataSource);
                        InspectorGridViewer.DataBind();
                    }
                }
                btnCancel.Text = "  Cancel  ";
                mpeInspectorDataEditorExtender.Hide();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        void StackCountDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeStackCountDataEditorExtender.Hide();
        }

        void StackCountDataEditor_Ok(object sender, EventArgs e)
        {
            try
            {
                if (((StackPhysicalCountInfo)StackCountDataEditor.DataSource).StackId == Guid.Empty)
                {
                    errorDisplayer.ShowErrorMessage("Stack is required");
                }
                else if (StackCountDataEditor.IsNew)
                {
                    inventoryService.AddStackPhysicalCount(PhysicalCountInformation, ((StackPhysicalCountInfo)StackCountDataEditor.DataSource));
                    StackGrid.DataBind();
                    StackCountDataEditor.IsNew = false;
                }
                else
                {
                    var editedStackCount = from stackCount in PhysicalCountInformation.Stacks
                                           where stackCount.Id == ((StackPhysicalCountInfo)StackCountDataEditor.DataSource).Id
                                           select stackCount;
                    if (editedStackCount.Count() > 0)
                    {
                        editedStackCount.ElementAt(0).Copy((StackPhysicalCountInfo)StackCountDataEditor.DataSource);
                        StackGrid.DataBind();
                    }
                }
                mpeStackCountDataEditorExtender.Hide();
                btnCancel.Text = "  Cancel  ";
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        private PhysicalCountInfo PhysicalCountInformation
        {
            get
            {
                return physicalCountInformation;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PhysicalCountInformation.Copy((PhysicalCountInfo)PhysicalCountEditor.DataSource);
                inventoryService.SavePhysicalCount(PhysicalCountInformation);
                btnCancel.Text = "  Close  ";
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
                transferedData.Return();
        }

        protected void btnAddStackCount_Click(object sender, EventArgs e)
        {
            StackCountDataEditor.IsNew = true;
            StackPhysicalCountInfo blankStack = inventoryService.GetBlankStackCount(physicalCountInformation);
            object oPreviousStack = StackCountDataEditor.DataSource;
            if (oPreviousStack != null)
            {
                blankStack.ShedId = ((StackPhysicalCountInfo)oPreviousStack).ShedId;
                blankStack.StackId = ((StackPhysicalCountInfo)oPreviousStack).StackId;
            }
            StackCountDataEditor.DataSource = blankStack;
            StackCountDataEditor.DataBind();
            mpeStackCountDataEditorExtender.Show();
        }

        protected void btnAddInspector_Click(object sender, EventArgs e)
        {
            InspectorDataEditor.IsNew = true;
            InspectorDataEditor.DataSource = inventoryService.GetBlankInspector(physicalCountInformation);
            InspectorDataEditor.DataBind();
            mpeInspectorDataEditorExtender.Show();
        }

        protected string GetShedNo(object shedId)
        {
            if (shedId == null || !(shedId is Guid) || ((Guid)shedId == Guid.Empty))
                return string.Empty;
            return inventoryService.LookupSource.GetLookup("Shed")[shedId];
        }

        protected string GetStackNo(object stackId)
        {
            if (stackId == null || !(stackId is Guid) || ((Guid)stackId == Guid.Empty))
                return string.Empty;
            return inventoryService.LookupSource.GetLookup("Stack")[stackId];
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            StackCountDataEditor.IsNew = false;
            var stackCountToEdit = from stack in PhysicalCountInformation.Stacks
                                  where stack.Id == new Guid(((LinkButton)sender).CommandArgument)
                                   select stack;
            StackCountDataEditor.DataSource = stackCountToEdit.ElementAt(0);
            errorDisplayer.ClearErrorMessage();
            mpeStackCountDataEditorExtender.Show();
            StackCountDataEditor.DataBind();
        }
    }


}
