using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GINBussiness;
using System.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class GINPSA : BasePage
    {
        protected GINModel CurrentGINModel
        {
            get
            {
                if (!SessionKeyExists(SessionKey.GINModel)) return null;
                return Session["GINMODEL"] as GINModel;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            if (!SessionKeyExists(SessionKey.GINModel))
            {
                Response.Redirect("ErrorPage.aspx");
                return;
            }
            RangeValidator1.MaximumValue = DateTime.Now.ToShortDateString();
            gvPUNInformation.DataSource = CurrentGINModel.PickupNoticesList;
            gvPUNInformation.DataBind();
            FillLIC(drpInventoryCoordinatorLoad, WareHouseOperatorTypeEnum.LIC);//Inventory Coordinator                       
            if (CurrentGINModel.GINStatusID != Convert.ToInt32(GINStatusTypeEnum.New))
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
            bool editMode = (bool)Session["EditMode"];
            if (editMode)
                RePopulateGINForm(CurrentGINModel);
            else
            {
                drpInventoryCoordinatorLoad.Items.Insert(0, new ListItem("Select", string.Empty));
                Session.Remove("remainingBalance");
            }
            Session["EditMode"] = false;
        }
        private void RePopulateGINForm(GINModel gm)
        {
            // txtDNSubmitedTime.Text = gm.TruckRequestTime.ToString();
            drpInventoryCoordinatorLoad.SelectedValue = Convert.ToString(gm.LeadInventoryControllerID);
            PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
            btnPrint.Visible = true;
        }
        public Guid GINID
        {
            get { return CurrentGINModel.ID; }
        }
        private void FillLIC(DropDownList ddl, WareHouseOperatorTypeEnum type)
        {
            ddl.DataSource = null;
            ddl.DataSource = WarehouseOperator.LICs(CurrentGINModel);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        private void SateGIN()
        {
            GINModel ginModel = CurrentGINModel;

            ginModel.WarehouseID = UserBLL.GetCurrentWarehouse();
            ginModel.LeadInventoryControllerID = new Guid(drpInventoryCoordinatorLoad.SelectedValue);
            ginModel.LICShedID = new Guid(drpInventoryCoordinatorLoad.SelectedValue);
            ginModel.IsPSA = true;
            ginModel.DateIssued = DateTime.Now; //DateTime.Parse(Convert.ToDateTime(txtDateIssued.Text).ToShortDateString() + " " + Convert.ToDateTime(txtDateIssuedTime.Text).ToShortTimeString());
            ginModel.LeadInventoryController = drpInventoryCoordinatorLoad.SelectedItem.Text;
            ginModel.CreatedBy = BLL.UserBLL.CurrentUser.UserId;
            ginModel.NetWeight = ginModel.PickupNoticesList.Sum(s => s.RemainingWeight);// -Convert.ToDouble(Session["remainingBalance"]);
            ginModel.GINStatusID = Convert.ToInt32(GINStatusTypeEnum.New);
            btnPrint.Visible = true;
        }
        private void SaveGINPSA()
        {
            GINModel ginPSAModel = CurrentGINModel;

            ginPSAModel.IsPSA = true;
            ginPSAModel.SavePSA();

            Messages.SetMessage("PSA with PSA Number " + ginPSAModel.GINNumber + " is saved successfully ", WarehouseApplication.Messages.MessageType.Success);
        }

        private void DoNew()
        {
            // txtDNSubmitedTime.Text = string.Empty;
            drpInventoryCoordinatorLoad.SelectedIndex = 0;
        }
        private void DoNewStack()
        {
            // drpStackNo.SelectedIndex = 0;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["ReportType"] = "PSA";
            ScriptManager.RegisterStartupScript(this,
                                                          this.GetType(),
                                                          "ShowReport",
                                                          "<script type=\"text/javascript\">" +
                                                          string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                          "</script>",
                                                          false);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (drpInventoryCoordinatorLoad.SelectedItem.Text == "[Select LIC]")
            {
                Messages.SetMessage("LIC not selected!!!", WarehouseApplication.Messages.MessageType.Error);
                return;
            }
            //Messages.ClearMessage();
            Session.Remove("remainingBalance");
            //StackTransactionModel sm = GetStack();

            GINModel gm = CurrentGINModel;

            string errorMessage = string.Empty;
            try
            {
                SateGIN();
                // if (gm.IsValidForPSA())
                //   {
                //  gm.AddStack(sm);
                SaveGINPSA();
                //  }
                //   else
                //   {
                //       errorMessage = gm.ErrorMessage;
                //        Messages.SetMessage(gm.ErrorMessage, WarehouseApplication.Messages.MessageType.Error);
                //    }

            }
            catch (Exception ex)
            {
                Messages.SetMessage(gm.ErrorMessage, WarehouseApplication.Messages.MessageType.Error);
            }

            UpdatePanel2.Update();
        }
        //   private StackTransactionModel GetStack()
        //   {
        //  StackTransactionModel sm = new StackTransactionModel(CurrentGINModel);
        // if (drpStackNo.SelectedIndex > 0)
        //     sm.StackID = new Guid(drpStackNo.SelectedValue);
        //  return sm;
        //  }
        private double TextToDouble(string text)
        {
            double w;
            double.TryParse(text, out w);
            return w;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPickupNotice.aspx");
        }
        protected void drpInventoryCoordinatorLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStack(new Guid(drpInventoryCoordinatorLoad.SelectedValue));
            //    drpStackNo.Items.Insert(0, new ListItem("Select", string.Empty));
        }
        private void PopulateStack(Guid LIC)
        {
            WarehouseOperator ob = new WarehouseOperator();
            CurrentGINModel.LICShedID = LIC;
            List<StackTransactionModel> list = null;
            if (CurrentGINModel.PickupNoticesList[0].Commodity != new Guid("71604275-df23-4449-9dae-36501b14cc3b"))
            {
                list = StackTransactionModel.GetStackInShed(CurrentGINModel);
                if (list != null)
                {
                    //drpStackNo.DataSource = list;
                    //drpStackNo.DataTextField = "StackNumber";
                    //drpStackNo.DataValueField = "ID";
                    //drpStackNo.DataBind();
                    DataRow dr = StackTransactionModel.GetShedCurrentBalance(CurrentGINModel);
                    if (dr != null)
                    {
                        txtShedNo.Text = dr["ShedNo"].ToString();
                        txtCommoditySymbol.Text = dr["CommoditySymbol"].ToString();
                        txtCurrentBalance.Text = dr["CurrentBalance"].ToString();
                        txtCurrentWeight.Text = dr["CurrentWeight"].ToString();
                    }
                    else
                    {
                        txtShedNo.Text = string.Empty;
                        txtCommoditySymbol.Text = string.Empty; ;
                        txtCurrentBalance.Text = string.Empty; ;
                        txtCurrentWeight.Text = string.Empty;
                    }
                }
            }
            else
            {
                list = StackTransactionModel.GetCurCoffBalance(CurrentGINModel);
                if (list != null)
                {
                    DataRow dr = StackTransactionModel.GetShedCurrCoffBalance(CurrentGINModel);
                    if (dr != null)
                    {
                        txtShedNo.Text = dr["ShedNo"] == null ? "" : dr["ShedNo"].ToString();
                        txtCommoditySymbol.Text = dr["CommoditySymbol"].ToString();
                        txtCurrentBalance.Text = dr["CurrentBalance"].ToString();
                        txtCurrentWeight.Text = dr["CurrentWeight"].ToString();
                    }
                    else
                    {
                        txtShedNo.Text = string.Empty;
                        txtCommoditySymbol.Text = string.Empty; ;
                        txtCurrentBalance.Text = string.Empty; ;
                        txtCurrentWeight.Text = string.Empty; 
                    }
                }
            }

        }

        decimal total = 0;
        protected void gvPUNInformation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RemainingWeight"));  
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Totals:";
                e.Row.Cells[3].Text = total.ToString();    
            }
        }
    }
}
