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
    public partial class SampleGIN : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            transferedData = new PageDataTransfer(Request.Path);
            ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

            GINProcessDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("SampleGIN", "PUNSummary");
            SampleDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("SampleGIN", "Sample");
            SampleGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("SampleGIN", "Sample");

            GINProcessDataEditor.Lookup = ginProcess.LookupSource;
            SampleDataEditor.Lookup = ginProcess.LookupSource;
            SampleGridViewer.Lookup = ginProcess.LookupSource;

            GINProcessDataEditor.IsNew = false;

            SampleDataEditor.Ok += new EventHandler(SampleDataEditor_Ok);
            SampleDataEditor.Cancel += new EventHandler(SampleDataEditor_Cancel);

            GINProcessDataEditor.Setup();
            SampleDataEditor.Setup();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SampleGridViewer.DataSource = ginProcess.GINProcessInformation.Samples;
            SampleGridViewer.DataBind();
            var sampleGridCommands = from command in SampleGridViewer.Driver.Columns
                                     where command.IsCommand
                                     select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in sampleGridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                GINProcessDataEditor.DataSource = ginProcess.GINProcessInformation;
                GINProcessDataEditor.DataBind();
            }

        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Samplers")
            {
                PageDataTransfer sampleTransfer = new PageDataTransfer("/GINSamplers.aspx");
                sampleTransfer.TransferData["SampleId"] = new Guid((string)e.CommandArgument);
                sampleTransfer.TransferData["TransactionId"] = transferedData.GetTransferedData("TransactionId");
                sampleTransfer.TransferData["IsGINTransaction"] = false;
                sampleTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.RemoveAllData();
                sampleTransfer.Navigate();
            }
            else if (e.CommandName == "Results")
            {
                PageDataTransfer sampleTransfer = new PageDataTransfer("/GINSamplingResults.aspx");
                sampleTransfer.TransferData["SampleId"] = new Guid((string)e.CommandArgument);
                sampleTransfer.TransferData["TransactionId"] = transferedData.GetTransferedData("TransactionId");
                sampleTransfer.TransferData["IsGINTransaction"] = false;
                sampleTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.RemoveAllData();
                sampleTransfer.Navigate();
            }
            else if (e.CommandName == "EditSample")
            {
                SampleDataEditor.IsNew = false;
                var sampleToEdit = from sample in ginProcess.GINProcessInformation.Samples
                                   where sample.Id == new Guid((string)e.CommandArgument)
                                   select sample;
                SampleDataEditor.DataSource = sampleToEdit.ElementAt(0);
                SampleDataEditor.DataBind();
                SampleDataEditorContainer.Attributes["class"] = "ShowPopupEditor";
            }
        }

        void SampleDataEditor_Cancel(object sender, EventArgs e)
        {
            SampleDataEditorContainer.Attributes["class"] = "HidePopupEditor";
        }

        void SampleDataEditor_Ok(object sender, EventArgs e)
        {
            if (SampleDataEditor.IsNew)
            {
                ginProcess.AddSample((SampleInfo)SampleDataEditor.DataSource);
                SampleGridViewer.DataBind();
            }
            else
            {
                var editedSample = from sample in ginProcess.GINProcessInformation.Samples
                                   where sample.Id == ((SampleInfo)SampleDataEditor.DataSource).Id
                                   select sample;
                if (editedSample.Count() > 0)
                {
                    editedSample.ElementAt(0).Copy((SampleInfo)SampleDataEditor.DataSource);
                    SampleGridViewer.DataBind();
                }
            }
            SampleDataEditorContainer.Attributes["class"] = "HidePopupEditor";
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
        }

        protected void btnAddSample_Click(object sender, EventArgs e)
        {
            SampleDataEditor.IsNew = true;
            SampleDataEditor.DataSource = ginProcess.GetBlankSample();
            SampleDataEditorContainer.Attributes["class"] = "ShowPopupEditor";
        }

    }
}
