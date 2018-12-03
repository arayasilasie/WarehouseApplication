using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SamplingBussiness;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class AddSamplingResultNew : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SamplingModel sm = new SamplingModel();
                if (Session["SamplingsId_Search"] != null && Session["SamplingsId_Search"].ToString().Trim().Length > 0)
                {
                    sm = SamplingModel.GetSampleById(new Guid(Session["SamplingsId_Search"].ToString()));
                    Session["SamplingsId_Search"] = null;
                    Session.Remove("SamplingsId_Search");
                }
                else if (Request.QueryString["sampleCode"] != null)
                {
                    sm = SamplingModel.GetSampleBySampleCode(Request.QueryString["sampleCode"]);
                    Session["SamplingsId_Search"] = null;
                    Session.Remove("SamplingsId_Search");
                }

                if (sm != null)
                {
                    txtResultReceivedDate.Text = DateTime.Now.ToShortDateString();
                    txtResultReceivedTime.Text = DateTime.Now.ToShortTimeString();
                    if (sm != null && sm.ID.ToString() != Guid.Empty.ToString())
                    {
                        ViewState.Add("SelectedSMID", sm.ID);
                        txtSampleCode.Text = sm.SampleCode;
                        txtSamplerName.Text = sm.SamplerName;
                        txtArrivalNoOfBags.Text = sm.ArrivalNumberOfBags.ToString();
                        txtSampleCodeGeneratedDateStamp.Text = sm.SampleCodeGeneratedTimeStamp.ToShortDateString();
                        txtSampleCodeGeneratedTimeStamp.Text = sm.SampleCodeGeneratedTimeStamp.ToShortTimeString();
                        lblCodeGeneratedDate.Text = sm.SampleCodeGeneratedTimeStamp.ToShortDateString() + " " + sm.SampleCodeGeneratedTimeStamp.ToShortTimeString();
                        if ((Request.QueryString["CommandName"] != null &&
                              Request.QueryString["CommandName"].ToUpper() == "Update".ToUpper())
                            || (sm.IsGraded || sm.IsReSampled))
                        {
                            txtNumberOfBags.Text = sm.NumberOfBags.ToString();
                            chkHasChemicalOrPetrol.Checked = sm.HasChemicalOrPetrol != null ? sm.HasChemicalOrPetrol.Value : false;
                            chkHasLiveInsect.Checked = sm.HasLiveInsect != null ? sm.HasLiveInsect.Value : false;
                            chkHasMoldOrFungus.Checked = sm.HasMoldOrFungus != null ? sm.HasMoldOrFungus.Value : false;
                            chkIsPlompOk.Checked = (sm.PlompStatusID == (int)PlompStatus.PlompOK) ? true : false;
                            rbSampleStatus.SelectedValue = sm.SamplingStatusID.ToString();
                            txtSamplerComments.Text = sm.SamplerComments;
                            if (sm.ResultReceivedDateTime != null)
                            {
                                txtResultReceivedDate.Text = sm.ResultReceivedDateTime.Value.ToShortDateString();
                                txtResultReceivedTime.Text = sm.ResultReceivedDateTime.Value.ToShortTimeString();
                            }
                            drpBagType.SelectedValue = sm.BagTypeID.ToString();
                            if (!chkIsPlompOk.Checked && ((int)SamplingBussiness.SamplingStatus.DriverNotFound) != sm.SamplingStatusID)
                            {
                                reqRemark.Enabled = true;
                                reqRemark.Validate();
                            }
                        //}
                            if (sm.IsGraded || sm.IsReSampled)
                            {
                                rbSampleStatus.Enabled = txtNumberOfBags.Enabled = txtSamplerComments.Enabled = drpBagType.Enabled = false;
                                chkHasChemicalOrPetrol.Enabled = chkHasLiveInsect.Enabled = chkHasMoldOrFungus.Enabled =
                                    chkIsPlompOk.Enabled = false;
                                txtResultReceivedDate.Enabled = txtResultReceivedTime.Enabled = false;
                                btnSaveSampleResult.Enabled = false;
                                btnClear.Enabled = false;
                                btnClear.ToolTip = btnSaveSampleResult.ToolTip = "ReSampled or Graded sample can't be edited!";
                            }
                        }
                        else
                            rdSampleStatus_SelectedIndexChanged(null, null);

                    }
                }
                PopulateCombo(sm.CommodityID, sm.VoucherCommodityTypeID);
                PopulateSampleType();
            }
            RangeValidator2.MinimumValue = DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture);
            RangeValidator2.MaximumValue = DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture);
            unBlockUI();
        }

        private void PopulateSampleType()
        {
            drpSampleType.Items.Clear();
            drpSampleType.Items.Add(new ListItem("- - Please Select Bag type - -", ""));
            drpSampleType.Items.Add(new ListItem("Normal", ""));
            drpSampleType.Items.Add(new ListItem("General Requirement Fail", ""));
            drpSampleType.Items.Add(new ListItem("Moisture Fail", ""));
            drpSampleType.Items.Add(new ListItem("Segregation Requested", ""));
            drpSampleType.DataBind();
            //drpSampleType.DataSource
        }

        void PopulateCombo(Guid? commodityID, Guid? voucherCommodityTypeID)
        {
            List<SamplingModel> sampleModels = SamplingModel.GetSamples(UserBLL.GetCurrentWarehouse(), "", "", (int)SamplingBussiness.SamplingStatus.SampleCodeGenerated);
            //cboSampleCode.DataSource = sampleModels;
            //cboSampleCode.DataTextField = "SampleCode";
            //cboSampleCode.DataValueField = "ID";
            //cboSampleCode.DataBind();
            //cboSampleCode_SelectedIndexChanged(null, null);
            drpBagType.Items.Clear();
            drpBagType.Items.Add(new ListItem("- - Please Select Bag type - -", ""));
            drpBagType.DataSource = SamplingModel.GetBagTypes(commodityID, voucherCommodityTypeID);
            drpBagType.DataTextField = "Description";
            drpBagType.DataValueField = "Guid";
            drpBagType.DataBind();
        }

        protected void btnSaveSampleResult_Click(object sender, EventArgs e)
        {
            Messages.ClearMessage();

            //int no = 0;
            //if (((int)SamplingBussiness.SamplingStatus.DriverNotFound) != int.Parse(rbSampleStatus.SelectedValue) &&
            //    txtArrivalNoOfBags.Text.Trim() != string.Empty &&
            //    int.TryParse(txtArrivalNoOfBags.Text, out no) &&
            //    no < int.Parse(txtNumberOfBags.Text))
            //{
            //    Messages.SetMessage("Bag count greater than the bag count " + no + " registered on Voucher!", WarehouseApplication.Messages.MessageType.Error);
            //    return;
            //}
            if (ViewState["SelectedSMID"] == null)
            {
                Messages.SetMessage("The sample ticket dosen't exits. Please try reloading again!", WarehouseApplication.Messages.MessageType.Error);
                return;
            }
            //SamplingModel sm = (SamplingModel)ViewState["SelectedSMID"];
            SamplingModel sm = SamplingModel.GetSampleById(new Guid(ViewState["SelectedSMID"].ToString()));
            if (sm == null)
            {
                Messages.SetMessage("The sample ticket dosen't exits. Please try reloading again!", WarehouseApplication.Messages.MessageType.Error);
                return;
            }
            sm.ResultReceivedDateTime = DateTime.Parse(txtResultReceivedDate.Text + " " + txtResultReceivedTime.Text);
            sm.SamplingStatusID = int.Parse(rbSampleStatus.SelectedValue);
            if (((int)SamplingBussiness.SamplingStatus.DriverNotFound) != int.Parse(rbSampleStatus.SelectedValue))
            {
                if (!chkIsPlompOk.Checked && txtSamplerComments.Text.Trim().Length <= 0)
                {
                    Messages.SetMessage("Please enter the remark since the plomp ok is not checked!", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                int noOfbag = 0;
                if (!int.TryParse(txtNumberOfBags.Text, out noOfbag))
                {
                    Messages.SetMessage("Please enter a valid number of bags!", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                sm.NumberOfBags = noOfbag;
                if (drpBagType.SelectedIndex <= 0)
                {
                    Messages.SetMessage("Please select Bag Type!", WarehouseApplication.Messages.MessageType.Error);
                    return;
                }
                sm.BagTypeID = new Guid(drpBagType.SelectedValue);
                sm.SamplerComments = txtSamplerComments.Text;
                sm.PlompStatusID = (int)(chkIsPlompOk.Checked ? PlompStatus.PlompOK : PlompStatus.PlompNotOk);
                // sm.SupervisorApprovalRemark = ""; txtSuppervisorApprovalRemark.Text;
                //sm.SupervisorApprovalDateTime = DateTime.Parse(txtSupperVisorApprovalDate.Text);
                sm.HasLiveInsect = chkHasLiveInsect.Checked;
                sm.HasMoldOrFungus = chkHasMoldOrFungus.Checked;
                sm.HasChemicalOrPetrol = chkHasChemicalOrPetrol.Checked;
                sm.SampleType = drpSampleType.SelectedItem.ToString();
                reqRemark.Enabled = !chkIsPlompOk.Checked;

            }
            sm.LastModifiedBy = BLL.UserBLL.CurrentUser.UserId;
            sm.LastModifiedTimestamp = DateTime.Now;

            try
            {
                sm.Save();


                int no = 0;
                string msg = "";
                if (((int)SamplingBussiness.SamplingStatus.DriverNotFound) != int.Parse(rbSampleStatus.SelectedValue) &&
                    txtArrivalNoOfBags.Text.Trim() != string.Empty &&
                    int.TryParse(txtArrivalNoOfBags.Text, out no) &&
                    no < int.Parse(txtNumberOfBags.Text))
                {
                    msg = "Bag count greater than the bag count " + no + " registered on Voucher!";
                }
                //PopulateCombo(sm.CommodityID,sm.VoucherCommodityTypeID);
                //Clear(true);

                if (msg == string.Empty)
                    Messages.SetMessage("Record saved successfully! ", WarehouseApplication.Messages.MessageType.Success);
                else
                    Messages.SetMessage("Record saved successfully! " + msg, WarehouseApplication.Messages.MessageType.Warning);
                btnSaveSampleResult.Visible = false;
            }
            catch (Exception ex)
            {

                Messages.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
            } 
        }

        void unBlockUI()
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(),
                           "unBlockUI", "<script type='text/javascript'>$.unblockUI();</script>", false);
        }

        protected void rdSampleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            //clear and disable the rest unwanted controls
            if (((int)SamplingBussiness.SamplingStatus.DriverNotFound) == int.Parse(rbSampleStatus.SelectedValue))
            {
                txtNumberOfBags.Enabled = false;
                drpBagType.Enabled = false;
                reqValidaterNoOfBags.Enabled = false;
                reqValidatorBagType.Enabled = false;
                rangValidaterNoOfBags.Enabled = false;
                reqRemark.Enabled = false;
                txtSamplerComments.Enabled = false;
                //txtSupperVisorApprovalDate.Enabled = false;
                //txtSuppervisorApprovalRemark.Enabled = false;
                chkHasChemicalOrPetrol.Enabled = false;
                chkHasLiveInsect.Enabled = false;
                chkHasMoldOrFungus.Enabled = false;
                chkIsPlompOk.Enabled = false;
                Clear(true);
            }
            else
            {
                txtNumberOfBags.Enabled = true;
                drpBagType.Enabled = true;
                reqRemark.Enabled = !chkIsPlompOk.Checked;
                reqValidatorBagType.Enabled = true;
                reqValidaterNoOfBags.Enabled = true;
                rangValidaterNoOfBags.Enabled = true;
                txtSamplerComments.Enabled = true;
                //txtSupperVisorApprovalDate.Enabled = true;
                //txtSuppervisorApprovalRemark.Enabled = true;
                chkHasChemicalOrPetrol.Enabled = true;
                chkHasLiveInsect.Enabled = true;
                chkHasMoldOrFungus.Enabled = true;
                chkIsPlompOk.Enabled = true;
            }
        }

        void Clear(bool isCheckChange)
        {
            txtNumberOfBags.Text = "";
            drpBagType.SelectedIndex = -1;
            txtSamplerComments.Text = "";
            //txtSuppervisorApprovalRemark.Text = "";
            if (!isCheckChange)
            {
                rbSampleStatus.SelectedValue = "2";
                rdSampleStatus_SelectedIndexChanged(null, null);
                chkIsPlompOk.Checked = true;
            }
            reqValidaterNoOfBags.ValidationGroup = "save";
            rangValidaterNoOfBags.ValidationGroup = "save";
            reqValidatorBagType.ValidationGroup = "save";
            chkHasChemicalOrPetrol.Checked = false;
            chkHasLiveInsect.Checked = false;
            chkHasMoldOrFungus.Checked = false;

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear(false);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CommandName"] != null &&
                              Request.QueryString["CommandName"].ToUpper() == "Update".ToUpper())
            {
                Response.Redirect(@"~\SearchPage.aspx");
            }
            else
            {
                Response.Redirect(@"~\ListInboxNew.aspx");
            }
        }
    }
}