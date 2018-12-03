using System;
using System.Collections;
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
    public partial class GINSamplers : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            transferedData = new PageDataTransfer(Request.Path);
            ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

            SampleDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GINSamplers", "Sample");
            SamplerDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GINSamplers", "Sampler");
            SamplerGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("GINSamplers", "Sampler");

            SampleDataEditor.Lookup = ginProcess.LookupSource;
            SamplerDataEditor.Lookup = ginProcess.LookupSource;
            SamplerGridViewer.Lookup = ginProcess.LookupSource;

            SampleDataEditor.IsNew = false;

            SamplerDataEditor.Ok += new EventHandler(SamplerDataEditor_Ok);
            SamplerDataEditor.Cancel += new EventHandler(SamplerDataEditor_Cancel);

            SampleDataEditor.Setup();
            SamplerDataEditor.Setup();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SamplerGridViewer.DataSource = SampleInformation.Samplers;
            SamplerGridViewer.DataBind();
            var sampleGridCommands = from command in SamplerGridViewer.Driver.Columns
                                     where command.IsCommand
                                     select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in sampleGridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                SampleDataEditor.DataSource = SampleInformation;
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditSampler")
            {
                SamplerDataEditor.IsNew = false;
                var samplerToEdit = from sampler in SampleInformation.Samplers
                                    where sampler.Id == new Guid((string)e.CommandArgument)
                                    select sampler;
                SamplerDataEditor.DataSource = samplerToEdit.ElementAt(0);
                SamplerDataEditor.DataBind();
                SamplerDataEditorContainer.Attributes["class"] = "ShowPopupEditor";
            }
        }

        private SampleInfo SampleInformation
        {
            get
            {
                var transferedSample = from sample in ginProcess.GINProcessInformation.Samples
                                       where sample.Id == (Guid)transferedData.GetTransferedData("SampleId")
                                       select sample;
                return transferedSample.ElementAt(0);
            }
        }

        void SamplerDataEditor_Cancel(object sender, EventArgs e)
        {
            SamplerDataEditorContainer.Attributes["class"] = "HidePopupEditor";
        }

        void SamplerDataEditor_Ok(object sender, EventArgs e)
        {
            if (SamplerDataEditor.IsNew)
            {
            }
            else
            {
                var editedSampler = from sampler in SampleInformation.Samplers
                                    where sampler.Id == ((WorkerInformation)SamplerDataEditor.DataSource).Id
                                    select sampler;
                if (editedSampler.Count() > 0)
                {
                    editedSampler.ElementAt(0).Copy((WorkerInformation)SamplerDataEditor.DataSource);
                    SamplerGridViewer.DataBind();
                }
            }
            SamplerDataEditorContainer.Attributes["class"] = "HidePopupEditor";
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
        }

        protected void btnAddSampler_Click(object sender, EventArgs e)
        {

        }
    }
}
