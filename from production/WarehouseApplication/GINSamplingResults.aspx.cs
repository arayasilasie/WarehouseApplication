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
    public partial class GINSamplingResults : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            transferedData = new PageDataTransfer(Request.Path);
            ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

            SampleDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GINSamplingResults", "Sample");
            SamplingResultDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GINSamplingResults", "SamplingResult");
            SamplerGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("GINSamplingResults", "Sampler");

            SampleDataEditor.Lookup = ginProcess.LookupSource;
            SamplingResultDataEditor.Lookup = ginProcess.LookupSource;
            SamplerGridViewer.Lookup = ginProcess.LookupSource;

            SampleDataEditor.IsNew = false;

            SamplingResultDataEditor.Ok += new EventHandler(SamplingResultDataEditor_Ok);
            SamplingResultDataEditor.Cancel += new EventHandler(SamplingResultDataEditor_Cancel);

            SampleDataEditor.Setup();
            SamplingResultDataEditor.Setup();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SamplerGridViewer.DataSource = SampleInformation.Samplers;
            SamplerGridViewer.DataBind();
            var samplerGridCommands = from command in SamplerGridViewer.Driver.Columns
                                      where command.IsCommand
                                      select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in samplerGridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                SampleDataEditor.DataSource = SampleInformation;
                SampleDataEditor.DataBind();
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ShowResult")
            {
                SamplingResultDataEditor.IsNew = false;
                var samplingResultToEdit = from result in SampleInformation.SamplingResults
                                           where result.SamplerId == new Guid((string)e.CommandArgument)
                                           select result;
                SamplingResultInfo samplingResult = null;
                if (samplingResultToEdit.Count() == 0)
                {
                    samplingResult =
                        new SamplingResultInfo(
                            Guid.NewGuid(),
                            SampleInformation.Id,
                            new Guid((string)e.CommandArgument),
                            0,
                            0,
                            string.Empty,
                            (int)SamplingResultDataEditor.Lookup.GetLookup("SamplingResultStatus").Keys.ElementAt(0),
                            string.Empty);
                    SampleInformation.SamplingResults.Add(samplingResult);

                }
                else
                {
                    samplingResult = samplingResultToEdit.ElementAt(0);
                }
                SamplingResultDataEditor.DataSource = samplingResult;
                SamplingResultDataEditor.DataBind();
                SamplingResultDataEditorContainer.Attributes["class"] = "ShowPopupEditor";
            }
        }

        void SamplingResultDataEditor_Cancel(object sender, EventArgs e)
        {
            SamplingResultDataEditorContainer.Attributes["class"] = "HidePopupEditor";
        }

        void SamplingResultDataEditor_Ok(object sender, EventArgs e)
        {
            if (SamplingResultDataEditor.IsNew)
            {
            }
            else
            {
                var editedSamplingResult = from samplingResult in SampleInformation.SamplingResults
                                           where samplingResult.Id == ((SamplingResultInfo)SamplingResultDataEditor.DataSource).Id
                                           select samplingResult;
                if (editedSamplingResult.Count() > 0)
                {
                    editedSamplingResult.ElementAt(0).Copy((SamplingResultInfo)SamplingResultDataEditor.DataSource);
                    SamplerGridViewer.DataBind();
                }
            }
            SamplingResultDataEditorContainer.Attributes["class"] = "HidePopupEditor";
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
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

    }
}
