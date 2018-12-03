using System;
using System.Web;
using WarehouseApplication.BLL;
using System.Web.UI;

namespace WarehouseApplication
{
    public partial class PreArrival : System.Web.UI.Page
    {
        //protected override void OnInit(EventArgs e)
        //{

        //    base.OnInit(e);

        //    this.btnSave.UseSubmitBehavior = false;
        //    btnSave.OnClientClick = "javascript:";

        //    if (btnSave.CausesValidation)
        //    {
        //        btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
        //        btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                btnNext.Enabled = false;
                btnNext.BackColor = System.Drawing.Color.LightGray;
                this.btnSave.UseSubmitBehavior = false;
                btnSave.OnClientClick = "javascript:";

                if (btnSave.CausesValidation)
                {
                    btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
                    btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
                }
            }
            Messages.ClearMessage();
        }

        /// <summary>
        /// Validates if passed string is valid guid or not
        /// </summary>
        /// <param name="guidToBeChecked"></param>
        /// <returns>true, if valid guid else false.</returns>
        private bool IsGuidValid(string guidToBeChecked)
        {
            try
            {
                new Guid(guidToBeChecked.ToString());
                return true;
            }
            catch 
            {
                return false;
            }

        }

        /// <summary>
        /// Validates all inputs in the Pre-Arrival form including Nulls and wrong inputs.
        /// </summary>
        private void ValidateInputs()
        {
            try
            {
                if (!IsGuidValid(this.ClientSelector1.ClientGUID.Value.ToString()))
                    throw new Exception("Invalid client id!");
                if (string.IsNullOrEmpty(txtVoucherNo.Text) && string.IsNullOrEmpty(txtTruckPlateNo.Text) && string.IsNullOrEmpty(txtTrailerPlateNo.Text))
                    throw new Exception("Either Voucher No., Truck Plate No. or Trailer Plate No. should be supplied!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Guid ClientId;
            try
            {
                
                ClientId = new Guid(this.ClientSelector1.ClientGUID.Value.ToString());
                if (ClientId == Guid.Empty || ClientId == null)
                {
                    Messages.SetMessage("Please enter Client ID.", WarehouseApplication.Messages.MessageType.Warning);
                    return;
                }

            }
            catch
            {
                Messages.SetMessage("Please enter Client ID.", WarehouseApplication.Messages.MessageType.Warning);                
                return;
            }

            try
            {
                ValidateInputs();
                BLL.PreArrival objPreArrival = new BLL.PreArrival();
                objPreArrival.ClientId = ClientId;
                objPreArrival.ClientName = this.ClientSelector1.lblMessage.Text.Replace(']', ' ').Replace('[', ' ').Trim();
                objPreArrival.HasVoucher = chkHasVoucher.Checked;
                objPreArrival.VoucherNumber = txtVoucherNo.Text.Trim();
                objPreArrival.TruckPlateNumber = txtTruckPlateNo.Text.Trim();
                objPreArrival.TrailerPlateNumber = txtTrailerPlateNo.Text.Trim();
                objPreArrival.CodeType = "GRN";

                objPreArrival.WorkflowTypeID = 1;
                objPreArrival.WarehouseID = new Guid(HttpContext.Current.Session["CurrentWarehouse"].ToString());
                objPreArrival.UserID = UserBLL.GetCurrentUser();
                objPreArrival.CompleteCodeToReturn = "";
                objPreArrival.IsTruckInCompound = chkIsTruckInCompound.Checked;

                objPreArrival.Save();
                //lblMessage.ForeColor = System.Drawing.Color.Green;
                //lblMessage.Text = "Pre-Arrival registered successfully!";
                Messages.SetMessage("Pre-arrival record saved successfully.", WarehouseApplication.Messages.MessageType.Success);
                Clear();
                btnNext.Enabled = true;
                btnNext.BackColor = System.Drawing.ColorTranslator.FromHtml("#88AB2D");

                Nullable<Guid> Id;
                Id = objPreArrival.ArrivalId;
                if (Id != null)
                {
                    Session["CommodityRequestId"] = Id;
                    Session["ReportType"] = "PreArrival";
                    ScriptManager.RegisterStartupScript(this,
                                        this.GetType(),
                                        "ShowReport",
                                        "<script type=\"text/javascript\">" +
                                        string.Format("javascript:window.open(\"ReportViewer.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                                        "</script>",
                                        false);
                }
            }
            catch (Exception ex)
            {
                //lblMessage.ForeColor = System.Drawing.Color.Tomato;
                //lblMessage.Text = "Failed to proceed, " + ex.Message.ToString();
                Messages.SetMessage("Unexpected error.  Please try again." , WarehouseApplication.Messages.MessageType.Error);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            this.ClientSelector1.ClientGUID.Value = string.Empty;
            this.ClientSelector1.lblMessage.Text = "";
            this.ClientSelector1.txtClientId.Text = "";
            chkHasVoucher.Checked = true;
            txtVoucherNo.Text = "";
            txtTrailerPlateNo.Text = "";
            txtTruckPlateNo.Text = "";
            chkIsTruckInCompound.Checked = true;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInboxNew.aspx");
        }


    }
}
