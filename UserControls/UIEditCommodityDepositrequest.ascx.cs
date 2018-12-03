using System;
using System.Collections;
using System.Collections.Generic;
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
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.DAL;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditCommodityDepositrequest : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();

            //this.rvFT.MaximumValue = DateTime.Now.ToString("d");
            //this.rvFT.MinimumValue = DateTime.Now.AddDays(-100).ToString("d");
            if (IsPostBack != true)
            {
                Guid Id = new Guid(this.Request.QueryString.Get("id").ToString());
                hfCommodityDepositeId.Value = Id.ToString();
                LoadData(Id);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            Guid ClientId = Guid.Empty;
            CommodityDepositeRequestBLL objOld = (CommodityDepositeRequestBLL)ViewState["CommDepOld"];
            if (string.IsNullOrEmpty(this.ClientSelector1.ClientGUID.Value.ToString()))
            {
                ClientId = new Guid(this.hfClientId.Value.ToString());
            }
            else
            {
                ClientId = new Guid( this.ClientSelector1.ClientGUID.Value.ToString());
            }
            CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
            Guid CommDepId = new Guid(hfCommodityDepositeId.Value.ToString());
            Guid CommdityId = new Guid(cboCommodity.SelectedValue.ToString());
            Guid WoredaId = new Guid(cboWoreda.SelectedValue.ToString());
            Guid RepId = Guid.NewGuid();
            int Prodyear = int.Parse(cboProductionYear.SelectedValue.ToString());
            float weight = 0;
            if (txtWeight.Text != "")
            {
                weight = float.Parse(txtWeight.Text);
            }
            int NoBags = 0;
            if (txtNumberOfBags.Text != "")
            {
                 NoBags = int.Parse(this.txtNumberOfBags.Text);
            }
            DateTime dtRecivedDate = Convert.ToDateTime(dtDateTimeRecived.Text + " " + this.txtTimeArrival.Text);
            string remark = this.txtRemark.Text;
            int Status = int.Parse(this.cboStatus.SelectedValue.ToString());

            int prodYear = int.Parse(cboProductionYear.SelectedValue.ToString());
            try
            {
                isSaved = obj.EditCommodityDepositRequest(ClientId, CommDepId, CommdityId, RepId, Prodyear,
                    WoredaId, weight, NoBags, dtRecivedDate, remark, Status, UserBLL.GetCurrentUser(), objOld);
            }
            catch (GRNNotOnUpdateStatus ex)
            {
                this.lblmsg.Text = ex.msg;
                return;
            }
            if (isSaved == true)
            {
                this.lblmsg.Text = "Data Updated Sucessfully";
                LoadData(CommDepId);
            }
            else
            {
                this.lblmsg.Text = "Unable to Updated data.";

            }

            
        }

        protected void cboWoreda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadData(Guid Id )
        {
            //get The Id from the qs
         
            //Get the Commodity  deposite Request form.
            CommodityDepositeRequestBLL objEdit = new CommodityDepositeRequestBLL();
            CommodityDepositRequest objCommDepReq = new CommodityDepositRequest();
            DataSet dsResult = new DataSet();
            objEdit.Id = Id;
            dsResult = objCommDepReq.getCommodityDepositRequestById(Id);
            Guid ClientGuid = new Guid(dsResult.Tables[0].Rows[0]["ClientId"].ToString());
            objEdit.ClientId = ClientGuid;
            this.hfClientId.Value = ClientGuid.ToString();
            this.txtNumberOfBags.Text = dsResult.Tables[0].Rows[0]["NumberofBags"].ToString();
            objEdit.NumberofBags = int.Parse((dsResult.Tables[0].Rows[0]["NumberofBags"].ToString()));
            float weight = float.Parse(dsResult.Tables[0].Rows[0]["Weight"].ToString());
            if (weight == 0)
            {
                this.txtWeight.Text = "";
            }
            else
            {
                this.txtWeight.Text = dsResult.Tables[0].Rows[0]["Weight"].ToString();
            }
            objEdit.Weight = float.Parse(dsResult.Tables[0].Rows[0]["Weight"].ToString());
            this.txtRemark.Text = dsResult.Tables[0].Rows[0]["Remark"].ToString();
            objEdit.Remark = this.txtRemark.Text;
            this.dtDateTimeRecived.Text = Convert.ToDateTime(dsResult.Tables[0].Rows[0]["DateTimeRecived"]).ToShortDateString();
            this.txtTimeArrival.Text = Convert.ToDateTime(dsResult.Tables[0].Rows[0]["DateTimeRecived"]).ToLongTimeString();
            objEdit.DateTimeRecived = Convert.ToDateTime((dsResult.Tables[0].Rows[0]["DateTimeRecived"]).ToString()); 
            this.cboReprsentative.SelectedValue = dsResult.Tables[0].Rows[0]["RepresentativeId"].ToString();
            objEdit.RepresentativeId = Guid.Empty;
            if (dsResult.Tables[0].Rows[0]["WoredaId"] != DBNull.Value)
            {
                this.cboWoreda.SelectedValue = dsResult.Tables[0].Rows[0]["WoredaId"].ToString();
                objEdit.WoredaId = new Guid(dsResult.Tables[0].Rows[0]["WoredaId"].ToString());
            }
            this.cboStatus.SelectedValue = dsResult.Tables[0].Rows[0]["Status"].ToString();
            objEdit.Status = int.Parse(dsResult.Tables[0].Rows[0]["Status"].ToString());
            //if (Convert.ToInt32((this.cboStatus.SelectedValue)) == 0 )
            //{
            //    //this.btnEdit.Enabled = false;
            //}
            // Display the Client Id from this 
            // Id = dsResult.Tables[0].Rows[0]["ClientId"].ToString();

            ECXLookUp.ECXLookup objLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            //Populate Drop down from server cboCommodity
            this.cboCommodity.AppendDataBoundItems = true;
            ECXLookUp.CCommodity[] objCommodity = objLookUp.GetActiveCommodities(WarehouseApplicationConfiguration.GetWorkingLanguage());
            this.cboCommodity.DataSource = objCommodity;
            this.cboCommodity.DataTextField = "Name";
            this.cboCommodity.DataValueField = "UniqueIdentifier";
            this.cboCommodity.DataBind();
            this.cboCommodity.SelectedValue = dsResult.Tables[0].Rows[0]["CommodityId"].ToString();
            objEdit.CommodityId = new Guid(dsResult.Tables[0].Rows[0]["CommodityId"].ToString());
            //Populate Region
            //ECXLookUp.CRegion[] objRegion = objLookUp.GetActiveRegions(WarehouseApplicationConfiguration.GetWorkingLanguage());
            //this.cboRegion.DataSource = objRegion;
            //this.cboRegion.DataTextField = "Name";
            //this.cboRegion.DataValueField = "UniqueIdentifier";
            //this.cboRegion.DataBind();


            string SelectedWoreda = "";
            SelectedWoreda = dsResult.Tables[0].Rows[0]["WoredaId"].ToString();


            if (SelectedWoreda != "")
            {
                ECXLookUp.CWoreda objWoreda = objLookUp.GetWoreda(WarehouseApplicationConfiguration.GetWorkingLanguage(), new Guid(SelectedWoreda));
                Guid SelectedZone = new Guid(objWoreda.ZoneUniqueIdentifier.ToString());
                ECXLookUp.CZone objZoneSelected = objLookUp.GetZone(WarehouseApplicationConfiguration.GetWorkingLanguage(), SelectedZone);
                Guid SelectedRegion = new Guid(objZoneSelected.RegionUniqueIdentifier.ToString());
                this.cboRegion_CascadingDropDown.SelectedValue = SelectedRegion.ToString();
                this.cboZone_CascadingDropDown.SelectedValue = SelectedZone.ToString();
                this.cboWoreda_CascadingDropDown.SelectedValue = SelectedWoreda;
            }
           


            
           


            
           

            // Bind Clinet
            ClientBLL objClient = new ClientBLL();
            objClient = ClientBLL.GetClinet(ClientGuid);

            if (objClient != null)
            {
                this.txtClient.Text = objClient.ClientName + " - " + objClient.ClientId;
                this.txtClient.Enabled = false;
            }


            //Populate the Production Year 
            // set because we will not have Production year no latter than 2007
            string strSelectedYear = dsResult.Tables[0].Rows[0]["ProductionYear"].ToString();
            int currYear = int.Parse(ConfigurationSettings.AppSettings["CurrentEthiopianYear"]);
            this.cboProductionYear.Items.Add(new ListItem("Please Select Production Year.", ""));
            this.cboProductionYear.AppendDataBoundItems = true;
            if (strSelectedYear != "")
            {
                if (int.Parse(strSelectedYear) < currYear - 2)
                {
                    this.cboProductionYear.Items.Add(new ListItem(strSelectedYear, strSelectedYear));
                }
            }
            for (int i = currYear - 2; i <= currYear; i++)
            {

                this.cboProductionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.cboProductionYear.SelectedValue = dsResult.Tables[0].Rows[0]["ProductionYear"].ToString();
            if (dsResult.Tables[0].Rows[0]["ProductionYear"] != DBNull.Value )
            {
                objEdit.ProductionYear = int.Parse(dsResult.Tables[0].Rows[0]["ProductionYear"].ToString());
            }
            ViewState["CommDepOld"] = objEdit;
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            return new List<object>(new object[]{btnSave});
        }

        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
    }
}