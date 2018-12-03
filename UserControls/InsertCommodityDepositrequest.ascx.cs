using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
//using AjaxControlToolkit;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;
using System.Collections.Generic;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{

    public partial class AddCommodityDepositrequest : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            
            
           

            if (IsPostBack != true)
            {
                 Page.DataBind();
                try
                {
                    this.cboCommodity.Items.Add(new ListItem("Please Select Commodity", ""));
                    this.cboCommodity.AppendDataBoundItems = true;
                    ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                    ECXLookUp.CCommodity[] objCommodity = objEcxLookUp.GetActiveCommodities (Utility.GetWorkinglanguage());
                    this.cboCommodity.DataSource = objCommodity;
                    this.cboCommodity.DataTextField = "Name";
                    this.cboCommodity.DataValueField = "UniqueIdentifier";
                    this.cboCommodity.DataBind();
                }
                catch( Exception ex )
                {
                    throw ex;
                }

                try
                {
                   // get the type of the current warehoouse.
                
              
                    this.cboWarehouse.Items.Add(new ListItem("Please Select Warehouse", ""));
                    this.cboWarehouse.AppendDataBoundItems = true;
                    List<WarehouseBLL> listWarehouse = WarehouseBLL.GetAllActiveWarehouse();
                    foreach (WarehouseBLL owarehouse in listWarehouse)
                    {
                        this.cboWarehouse.Items.Add(new ListItem(owarehouse.WarehouseName, owarehouse.WarehouseId.ToString()));
                    }
                    this.cboWarehouse.SelectedValue = UserBLL.GetCurrentWarehouse().ToString();
                }
                catch( Exception ex )
                {
                    throw ex;
                }

                int currYear ;
                currYear = int.Parse(ConfigurationSettings.AppSettings["CurrentEthiopianYear"]);
                this.cboProductionYear.Items.Add(new ListItem("Please Select Production Year.",""));
                this.cboProductionYear.AppendDataBoundItems = true ;
                for(int i = currYear- 2 ; i <= currYear ; i++)
                {
                    this.cboProductionYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
                }
            }
            else
            {
               
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            this.lblMessage.Text = "";

            if (this.btnSave.Text == "Save")
            {
                string TransactionNo = "";
                Guid TransactionTypeId = Guid.Empty;
                Guid CommodityGuid = Guid.Empty;

                Guid ClientId, CommodityId, WarehouseId, CreatedBY;
                Guid RepId, WoredaId;
                DateTime DateTimeRecived;
                float Weight;
                int Year, NumberOfBags;
                string Remark, TransactionId;
                int Status; // Status - 1= new 
                Nullable<Guid> Id;
                //NoClient
                ClientId = new Guid(this.ClientSelector1.ClientGUID.Value.ToString());
                try
                {
                    ClientId = new Guid(this.ClientSelector1.ClientGUID.Value.ToString());
                }
                catch
                {

                    this.lblMessage.Text = "Please Select Client";
                    return;
                }

                //NoClient
                //Check if ClientId is Empty.
                if (ClientId != Guid.Empty)
                {
                    if (!(string.IsNullOrEmpty(this.cboCommodity.SelectedValue)))
                    {
                        CommodityGuid = new Guid(this.cboCommodity.SelectedValue);
                        try
                        {
                            TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId(CommodityGuid);
                        }
                        catch (InvalidTransactionType ex)
                        {
                            this.lblMessage.Text = ex.msg;
                            return;
                        }
                    }
                    else
                    {
                        TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId("RegularGrainTypeId");
                    }
                }
                else
                {
                    TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId(Guid.Empty);
                }
               



 

                Status = 1;// Approved Status
                
                
               

                try
                {
                    CommodityId = new Guid(this.cboCommodity.SelectedValue.ToString());
                }
                catch 
                {
                    this.lblMessage.Text = "Please Select Commodity";
                    return;
                }
                

                try
                {
                    WarehouseId = new Guid(this.cboWarehouse.SelectedValue.ToString());// Should be Non selectable.
                }
                catch 
                {
                    this.lblMessage.Text = "Please Select Warehouse";
                    return;
                }
                
                try
                {
                    WoredaId = new Guid(this.cboWoreda.SelectedValue.ToString());
                }
                catch 
                {
                    WoredaId = Guid.Empty;
                }
                
                try
                {
                    Year = Convert.ToInt32(this.cboProductionYear.SelectedValue.ToString());
                }
                catch 
                {
                    Year = 0;
                }
                Remark = this.txtRemark.Text;


                float.TryParse(this.txtWeight.Text,out Weight);
               
                 int.TryParse(this.txtNumberOfBags.Text,out NumberOfBags);
               
               
                try
                {
                    DateTimeRecived = Convert.ToDateTime(this.txtArrivalDate.Text + " " + this.txtTimeArrival.Text );
                }
                catch 
                {
                    this.lblMessage.Text = "Please enter the correct date time format.";
                    return;
                }

                // get from Workflow
                TransactionId = TransactionNo;//WFTransaction.GetTransaction();
                CreatedBY = UserBLL.GetCurrentUser();
                //TO DO Remove this.
                RepId = new Guid();
                CommodityDepositeRequestBLL objSave = new CommodityDepositeRequestBLL();
                objSave.ClientId = ClientId;
                objSave.CommodityId = CommodityId;
                objSave.WarehouseId = WarehouseId;
                objSave.RepresentativeId = RepId;
                objSave.WoredaId = WoredaId;
                objSave.ProductionYear = Year;
                objSave.NumberofBags = NumberOfBags;
                objSave.DateTimeRecived = DateTimeRecived;
                objSave.Weight = Weight;
                objSave.Remark = Remark;
                objSave.Status = Status;
                objSave.CreatedBy = CreatedBY;

                Id = objSave.AddCommodityDepositRequest(TransactionTypeId);

                if (Id != null)
                {

                    Session["CommodityRequestId"] = Id.ToString();
                    this.btnSave.CausesValidation = false;
                    this.lblMessage.Text = "Data Added Sucessfully.";
                    this.btnSave.Text = "Next";
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("<script>");
                    //sb.Append("window.open('ReportTrackingNumber.aspx");
                    //sb.Append("', '', '');");
                    //sb.Append("location.href='ListInbox.aspx';");
                    //sb.Append("</scri");
                    //sb.Append("pt>");
                    ////Page.ClientScript.RegisterStartupScript(GetType(), "js", sb.ToString());
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "win", sb.ToString(), false);
                    //return;
                    ////Response.Redirect("~/PageSwicther.aspx?TranNo=" + TransactionNo);

                    ScriptManager.RegisterStartupScript(this,
                    this.GetType(),
                    "ShowReport",
                    "<script type=\"text/javascript\">" +
                        string.Format("javascript:window.open(\"ReportTrackingNumber.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                    "</script>",
                    false);


                }
                else
                {
                    this.lblMessage.Text = "Error Saving Request!";
                }
            }
            else
            {
                
                string trackingNo = Session["TransactionNo"].ToString();
                Session["TransactionNo"] = null;
                Response.Redirect("ListInbox.aspx");
            }
         

            
        }

        protected override void OnInit(EventArgs e)
        {
           
            base.OnInit(e);
            
            this.btnSave.UseSubmitBehavior = false;

            btnSave.OnClientClick = "javascript:";

            if (btnSave.CausesValidation)
            {
                btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
                btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }

           
        }

        #region ISecurityConfiguration Members

        public System.Collections.Generic.List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            string Id = string.Empty;
            if (name == "btnSave")
            {
                cmd.Add(this.btnSave);
                return cmd;
            }
            else
            {
                return null;
            }
        }

        #endregion

        protected void chkIsSourceDetermined_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }

       
    }
}