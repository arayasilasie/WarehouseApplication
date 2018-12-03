using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using SamplingBussiness;
using System.Drawing;
using GradingBussiness;

namespace WarehouseApplication
{
    public partial class GetSampleTicketNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack ))
                populateGrid(0);
        }

        private SamplingModel SetSampling(GridView gv)
        {
            SamplingModel samplingModel = new SamplingModel();
            if (gv.Rows.Count <= 0) return null;
            GridViewRow rw = gv.SelectedRow;
            samplingModel.TrackingNo = ((Label)rw.FindControl("lblTrackingNo")).Text;
            samplingModel.ID = Guid.NewGuid();
            samplingModel.WarehouseID = UserBLL.GetCurrentWarehouse();
            samplingModel.ArrivalID = new Guid(((Label)rw.FindControl("lblArrivalId")).Text);
            Label preGradingStatus = ((Label)rw.FindControl("lblGradingStatusID"));
            int lastSerialNo = 0;
            //int.TryParse(SamplingModel.GetLastSerialNoForDate(DateTime.Today.ToShortDateString(), samplingModel.WarehouseID), out lastSerialNo);
            samplingModel.SerialNo = lastSerialNo + 1;
            samplingModel.SampleCode = UserBLL.GetCurrentWarehouseCode() + "-" + DateTime.Today.Year.ToString().Substring(2) +
                 DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");// +"-" + samplingModel.SerialNo;
            samplingModel.SampleCodeGeneratedTimeStamp = DateTime.Now;
            samplingModel.SamplingStatusID = (int)SamplingBussiness.SamplingStatus.SampleCodeGenerated;
            if (preGradingStatus!=null && preGradingStatus.Text.Trim().Length > 0 &&
                int.Parse(preGradingStatus.Text) == (int)GradingBussiness.GradingResultStatus.SegrigationRequested)
            {
                samplingModel.SamplerID = new Guid(((Label)rw.FindControl("lblPreSamplerId")).Text);
                samplingModel.SamplerName = ((Label)rw.FindControl("lblPreSamplerName")).Text;
                samplingModel.SamplingInspectorID = new Guid(((Label)rw.FindControl("lblPreSampleInspectorID")).Text);
            }
            else
            {
                List<string> sampler = SamplingModel.SelectSampler(samplingModel.WarehouseID);
                if (sampler == null || sampler.Count() <= 0)
                {
                    Messages.SetMessage("All available Samplers assigned. Please wait.!", WarehouseApplication.Messages.MessageType.Warning);
                    return null;
                }
                samplingModel.SamplerID = new Guid(sampler[0].ToString());
                samplingModel.SamplerName = sampler[1];

                List<string> samplingInspector = SamplingModel.SelectSampleInspector(samplingModel.WarehouseID, samplingModel.SamplerID);
                if (samplingInspector == null || samplingInspector.Count() <= 0)
                {
                    Messages.SetMessage("All available Sampling Inspectors assigned or the available Inspector may also be registed as Sampler. Please wait!", WarehouseApplication.Messages.MessageType.Warning);
                    return null;
                }
                samplingModel.SamplingInspectorID = new Guid(samplingInspector[0].ToString());
            }
            
            samplingModel.CreatedBy = BLL.UserBLL.CurrentUser.UserId;
            samplingModel.CreateTimestamp = DateTime.Now;
            samplingModel.NumberOfBags = null;
            samplingModel.BagTypeID = null;
            samplingModel.PlompStatusID = null;
            samplingModel.SupervisorID = null;//WarehouseBLL.GetById(samplingModel.WarehouseID).SuppervisorId

            Label templbl = (Label)rw.FindControl("lblGradeId");
            if (templbl.Text.Trim() != string.Empty && !templbl.Text.Trim().Contains(Guid.Empty.ToString()))
                samplingModel.PreviousGradingResultID =new Guid(templbl.Text);
            else
                samplingModel.PreviousGradingResultID = null;
            templbl = (Label)rw.FindControl("lblSampleId");
            if (templbl.Text.Trim() != string.Empty && !templbl.Text.Trim().Contains(Guid.Empty.ToString()))
                samplingModel.PreviousSamplingID = new Guid(templbl.Text);
            else
                samplingModel.PreviousSamplingID = null;
            samplingModel.HasChemicalOrPetrol = null;
            samplingModel.HasLiveInsect = null;
            samplingModel.HasMoldOrFungus = null;
            samplingModel.LastModifiedTimestamp = null;

            return samplingModel;
        }

        private void SaveSampling(GridView gv)
        {
          //  this.lblMessage.Visible = false;

            Messages.ClearMessage();

            SamplingModel sampleModel = SetSampling(gv);
            UpdatePanel4.Update();
            if (sampleModel == null) return;

            try
            {
                sampleModel.Save();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("Sample Code".ToUpper()))
                {
                    string msg = ex.Message.Split('|').First(s => s.ToUpper().Contains("Sample Code".ToUpper()));
                    Messages.SetMessage(msg, Messages.MessageType.Error);
                    return;
                }
                else
                    throw;
            }

            //Repopulate the grid
            int searchCase = gv.ID == "gvWaitingForSampling" ? 1 : gv.ID == "gvWaitForDriver" ? 2 : 3;
            populateGrid(searchCase,true);
             
            Session["ReportType"] = "SampleTicket";
            Session["SampleId"] = sampleModel.ID;
            ScriptManager.RegisterStartupScript(this,
                                                   this.GetType(),
                                                   "ShowReport",
                                                   "<script type=\"text/javascript\">" +
                                                   string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                   "</script>",
                                                   false);
        }

        protected void btnGetSample_Click(object sender, EventArgs e)
        {
            SaveSampling(this.gvWaitingForSampling);
        }

        protected void btnGetSampleForDriverMissing_Click(object sender, EventArgs e)
        {
            SaveSampling(this.gvWaitForDriver);
        }

        protected void btnGetSampleTicketForMF_Click(object sender, EventArgs e)
        {
            SaveSampling(this.gvWaitForReSamle);
        }    

        /// <summary>
        /// Get all the Arrivals which are Ready for sampling.
        /// Display them based on their order(CreatedTimeStamp)
        /// </summary>
        private void populateGrid(int _case, bool afterSave=false)
        {
            Messages.ClearMessage();
            List<ArrivalForSampling> ls = null;
            List<ArrivalForSampling> lsNA = null;
            List<ArrivalForSampling> lsDM = null;
            List<ArrivalForSampling> lsMF = null;
            ls = SamplingModel.GetArrivalsReadyForSampling(UserBLL.GetCurrentWarehouse(), _case, txtTrackingNo.Text.Trim(),
                              txtPreSampleCode.Text.Trim(), txtPreGradingCode.Text.Trim());
            lsNA = ls.Where(s => s.SamplingStatusID.Equals(0)).ToList();
            lsDM = ls.Where(s => s.SamplingStatusID == (int)SamplingBussiness.SamplingStatus.DriverNotFound).ToList();
            lsMF = ls.Where(s => s.SamplingStatusID != (int)SamplingBussiness.SamplingStatus.DriverNotFound && !s.SamplingStatusID.Equals(0)).ToList();
            if (_case == 0 || _case == 1)
            {
                this.gvWaitingForSampling.DataSource = lsNA;
                this.gvWaitingForSampling.DataBind();
                this.gvWaitingForSampling.SelectedIndex = 0;
            }
            if (_case == 0 || _case == 2)
            {
                this.gvWaitForDriver.DataSource = lsDM;
                this.gvWaitForDriver.DataBind();
                this.gvWaitForDriver.SelectedIndex = 0;
            }
            if (_case == 0 || _case == 3)
            {
                this.gvWaitForReSamle.DataSource = lsMF;
                this.gvWaitForReSamle.DataBind();
                this.gvWaitForReSamle.SelectedIndex = 0;
            }
            if (gvWaitForDriver.Rows.Count <= 0 && gvWaitForReSamle.Rows.Count <= 0  && gvWaitingForSampling.Rows.Count <= 0
                && (ls == null || ls.Count <= 0) && !afterSave)
            {
                Messages.SetMessage("No truck available for sampling. Please wait.", WarehouseApplication.Messages.MessageType.Warning);
            }
        }

        protected void btnReloadForResample_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lb = (ImageButton)sender;
            int searchCase = lb.ID == "lbtnReloadForNew" ? 1 : lb.ID == "lbtnReloadForDriverMissing" ? 2 : 3;
            populateGrid(searchCase);
            Messages.ClearMessage();
            UpdatePanel4.Update();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            int searchCase = gv.ID == "gvWaitingForSampling" ? 1 : gv.ID == "gvWaitForDriver" ? 2 : 3;
            gv.PageIndex = e.NewPageIndex;
            populateGrid(searchCase);
            Messages.ClearMessage();
            UpdatePanel4.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateGrid(0);
        }
    }
}