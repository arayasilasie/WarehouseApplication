using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class InventoryTransfering : System.Web.UI.Page
    {
        static InventoryTransferModel InventoryTransfer = new InventoryTransferModel();
        static Guid CurrentWarehouse;
        static Guid LICID;
        static int ProductionYear;
        static Guid Symbol;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            CurrentWarehouse = new Guid(Session["CurrentWarehouse"].ToString());
            BindLIC();
            BindReason();

            RangeValidatorDate.MinimumValue = DateTime.Now.AddYears(-1).ToShortDateString();
            RangeValidatorDate.MaximumValue = DateTime.Now.ToShortDateString();

        }

        #region Transfer From

        public void BindLIC()
        {
            ddLIC.DataSource = InventoryTransferModel.GetLICsForInventoryTransfer(CurrentWarehouse);
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.DataBind();
        }

        public void BindShed()
        {   
            ddlShed.DataSource = InventoryTransferModel.GetShedsForInventoryTransfer(CurrentWarehouse, new Guid(ddLIC.SelectedValue));
            ddlShed.DataTextField = "ShedNumber";
            ddlShed.DataValueField = "ID";
            ddlShed.DataBind();       
        }

        public void BindStackNo()
        {
            ddlStackNo.Items.Clear();
            ddlStackNo.Items.Add(new ListItem("Select Stack No", ""));
            
            ddlStackNo.DataSource = InventoryTransferModel.GetStackNos(CurrentWarehouse, new Guid(ddlShed.SelectedValue), new Guid(ddLIC.SelectedValue));
            ddlStackNo.DataTextField = "StackNumber";
            ddlStackNo.DataValueField = "ID";
            ddlStackNo.DataBind();    
        }

        public void BindStackInfo()
        {
            DataRow dr = InventoryTransferModel.GetStackInfo(new Guid(ddlStackNo.SelectedValue));
            if (dr != null)
            {
                txtProductionYear.Text = dr["ProductionYear"].ToString();
                txtSymbol.Text = dr["Symbol"].ToString();
                txtSystemCount.Text = dr["CurrentBalance"].ToString();
                txtSystemWeight.Text = dr["CurrentWeight"].ToString();
                Symbol = new Guid(dr["CommodityGradeID"].ToString());
                LICID = new Guid(dr["LICID"].ToString());
                ProductionYear = int.Parse(dr["ProductionYear"].ToString());
                BindLICTo();
            }
        }

        public void BindReason()
        {
            ddlReason.DataSource = InventoryTransferModel.GetInventoryTransferReasons();
            ddlReason.DataTextField = "Description";
            ddlReason.DataValueField = "ID";
            ddlReason.DataBind();
        }

        protected void ddLIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls();
            ClearControls2();
            ClearDropdowns();
            if (ddLIC.SelectedValue != string.Empty)
            {
                BindShed();
            }
        }

        protected void ddlShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls();
            ClearControls2();
            if (ddlShed.SelectedValue != string.Empty)
            {
                BindStackNo();
            }
        }

        protected void ddlStackNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls();
            ClearControls2();
            if (ddlStackNo.SelectedValue != string.Empty)
            {
                BindStackInfo();
            }
        }
        public void ClearControls()
        {
            txtProductionYear.Text = "";
            txtSymbol.Text = "";
            txtSystemCount.Text = "";
            txtSystemWeight.Text = "";
            txtPhysicalCount.Text = "";
            txtPhysicalWeight.Text = "";
        }

        public void ClearDropdowns()
        {
            ddlShed.Items.Clear();
            ddlShed.Items.Add(new ListItem("Select Shed", ""));

            ddlStackNo.Items.Clear();
            ddlStackNo.Items.Add(new ListItem("Select Stack No", ""));

            ddLIC2.Items.Clear();
            ddLIC2.Items.Add(new ListItem("Select LIC", ""));

            ddlShed2.Items.Clear();
            ddlShed2.Items.Add(new ListItem("Select Shed", ""));

            ddlStackNo2.Items.Clear();
            ddlStackNo2.Items.Add(new ListItem("Select Stack No", ""));
        }

        #endregion

        #region Transfer To

        public void BindLICTo()
        {
            //ddLIC2.DataSource = StackModel.GetWarehouseLICs(new Guid(Session["CurrentWarehouse"].ToString()));
            ddLIC2.Items.Clear();
            ddLIC2.Items.Add(new ListItem("Select LIC", ""));

            ddLIC2.DataSource = InventoryTransferModel.GetLICsInventoryTransferTo(CurrentWarehouse,ProductionYear,Symbol, new Guid(ddLIC.SelectedValue));

            ddLIC2.DataTextField = "Name";
            ddLIC2.DataValueField = "ID";
            ddLIC2.DataBind();
        }       

        public void BindShedTo()
        {
            ddlShed2.Items.Clear();
            ddlShed2.Items.Add(new ListItem("Select Shed", ""));

            if (ddLIC2.SelectedValue != string.Empty)
            {
                ddlShed2.DataSource = InventoryTransferModel.GetShedsInventoryTransferTo(CurrentWarehouse, ProductionYear,Symbol, new Guid(ddLIC2.SelectedValue));
                ddlShed2.DataTextField = "ShedNumber";
                ddlShed2.DataValueField = "ID";
                ddlShed2.DataBind();
            }

            ddlStackNo2.Items.Clear();
            ddlStackNo2.Items.Add(new ListItem("Select Stack No", ""));
        }

        public void BindStackNo2()
        {
            ddlStackNo2.Items.Clear();
            ddlStackNo2.Items.Add(new ListItem("Select Stack No", ""));

            if (ddlShed2.SelectedValue != string.Empty)
            {
                ddlStackNo2.DataSource = InventoryTransferModel.GetStackNosTo(CurrentWarehouse, new Guid(ddlShed2.SelectedValue),ProductionYear,Symbol);
                ddlStackNo2.DataTextField = "StackNumber";
                ddlStackNo2.DataValueField = "ID";
                ddlStackNo2.DataBind();
            }            
        }

        public void BindStackInfo2()
        {
            DataRow dr = InventoryTransferModel.GetStackInfo(new Guid(ddlStackNo2.SelectedValue));
            txtProductionYear2.Text = dr["ProductionYear"].ToString();
            txtSymbol2.Text = dr["Symbol"].ToString();
            txtSystemCount2.Text = dr["CurrentBalance"].ToString();
            txtSystemWeight2.Text = dr["CurrentWeight"].ToString();
        }
          
        public void ClearControls2()
        {
            txtProductionYear2.Text = "";
            txtSymbol2.Text = "";
            txtSystemCount2.Text = "";
            txtSystemWeight2.Text = "";
            txtPhysicalCount2.Text = "";
            txtPhysicalWeight2.Text = "";
            txtTransferedDate.Text = "";
        }

        protected void ddLIC2_SelectedIndexChanged(object sender, EventArgs e)
        {
              ClearControls2();
            BindShedTo();
        }

        protected void ddlShed2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls2();
            BindStackNo2();
        }

        protected void ddlStackNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls2();
            if (ddlStackNo2.SelectedValue != string.Empty)
            {
                BindStackInfo2();
            }
        }
          
        #endregion

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            if (InitializeInventoryTransfer())
            {
                try
                {
                    InventoryTransfer.InsertInventoryTransfer();
                    Messages1.SetMessage("Record added successfully.", WarehouseApplication.Messages.MessageType.Success);

                    if (InventoryTransfer.ID != null)
                    {
                        Session["StackId"] = InventoryTransfer.ID;
                        ScriptManager.RegisterStartupScript(this,
                                            this.GetType(),
                                            "ShowReport",
                                            "<script type=\"text/javascript\">" +
                                            string.Format("javascript:window.open(\"ReportInventoryTransfer.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                                            "</script>",
                                            false);
                    }

                }
                catch (Exception ex)
                {
                    Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);
                }
            }
        }
        bool IsValidStack()
        {
            int balance;
            float weight;
            DateTime startDate;
            if (txtSystemCount.Text == "" || txtSystemCount2.Text == "" || txtSystemWeight.Text == "" || txtSystemWeight2.Text == "" || txtSystemWeight.Text == "" ||
                txtPhysicalCount.Text == "" || txtPhysicalCount2.Text == "" || txtPhysicalWeight.Text == "" || txtPhysicalWeight2.Text == "" || txtSystemWeight.Text == "" || txtTransferedDate.Text == "" ||
                ddLIC.SelectedValue == "" || ddLIC2.SelectedValue == "" || ddlShed.SelectedValue == "" || ddlShed2.SelectedValue == "" || ddlStackNo.SelectedValue == "" || ddlStackNo2.SelectedValue == "" || ddlReason.SelectedValue == "")
            {
                Messages1.SetMessage("Please enter all values.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(int.TryParse(txtSystemCount.Text, out  balance)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(int.TryParse(txtSystemCount2.Text, out  balance)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(float.TryParse(txtSystemWeight.Text, out weight)))
            {
                Messages1.SetMessage("Please enter valid weight.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(float.TryParse(txtSystemWeight2.Text, out weight)))
            {
                Messages1.SetMessage("Please enter valid weight.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }

            else if (!(int.TryParse(txtPhysicalCount.Text, out  balance)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(int.TryParse(txtPhysicalCount2.Text, out  balance)))
            {
                Messages1.SetMessage("Please enter valid number.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(float.TryParse(txtSystemWeight.Text, out weight)))
            {
                Messages1.SetMessage("Please enter valid weight.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else if (!(float.TryParse(txtSystemWeight2.Text, out weight)))
            {
                Messages1.SetMessage("Please enter valid weight.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }

            else if (!(DateTime.TryParse(txtTransferedDate.Text, out  startDate)))
            {
                Messages1.SetMessage("Please enter valid Date.", WarehouseApplication.Messages.MessageType.Success);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool  InitializeInventoryTransfer()
        {           
            InventoryTransfer.CreatedBy = UserBLL.CurrentUser.UserId;
            InventoryTransfer.CreatedTimestamp = DateTime.Now; //new Guid(Session["CurrentWarehouse"].ToString());
            InventoryTransfer.InventoryTransferReasonID = int.Parse(ddlReason.SelectedValue);
            InventoryTransfer.LICID = new Guid(ddLIC.SelectedValue);
            InventoryTransfer.LICIDTo = new Guid(ddLIC2.SelectedValue);
            InventoryTransfer.PhysicalCount = int.Parse(txtPhysicalCount.Text);
            InventoryTransfer.PhysicalCountTo = int.Parse(txtPhysicalCount2.Text);
            InventoryTransfer.PhysicalWeight = float.Parse(txtPhysicalWeight.Text);
            InventoryTransfer.PhysicalWeighTo = float.Parse(txtPhysicalWeight2.Text);
            InventoryTransfer.ShedID = new Guid(ddlShed.SelectedValue);
            InventoryTransfer.ShedIDTo = new Guid(ddlShed2.SelectedValue);
            InventoryTransfer.StackNo = new Guid(ddlStackNo.SelectedValue);
            InventoryTransfer.StackNoTo = new Guid(ddlStackNo2.SelectedValue);
            InventoryTransfer.Status = 1;
            InventoryTransfer.SystemCount = int.Parse(txtSystemCount.Text);
            InventoryTransfer.SystemCountTo = int.Parse(txtSystemCount2.Text);
            InventoryTransfer.SystemWeight = float.Parse(txtSystemWeight.Text);
            InventoryTransfer.SystemWeighTo = float.Parse(txtSystemWeight2.Text);
            InventoryTransfer.WarehouseID = CurrentWarehouse;
            InventoryTransfer.TransitionDate=DateTime.Parse(txtTransferedDate.Text);
            InventoryTransfer.ID = Guid.NewGuid();

            return true;           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindInventoryTransfer();
        }

        void BindInventoryTransfer()
        {
            grvInventoryTransfers.DataSource = InventoryTransferModel.GetInventoryTransferForEdit(new Guid(Session["CurrentWarehouse"].ToString()), DateTime.Parse(txtTrannsferDateSrch.Text));
            grvInventoryTransfers.DataBind();
        }
      
        protected void grvInventoryTransfer_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();

                HeaderGridRow.BackColor = Color.White;
                HeaderGridRow.ForeColor = ColorTranslator.FromHtml("#008000");
                
                //Add Transfer 
                HeaderCell.Text = "TRANSFER";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BorderWidth = 1;
                HeaderCell.BorderColor = ColorTranslator.FromHtml("#FFCC00");
                HeaderGridRow.Cells.Add(HeaderCell);

                //Add Transfer From
                HeaderCell = new TableCell();
                HeaderCell.Text = "TRANSFER FROM";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BorderWidth = 1;
                HeaderCell.BorderColor = ColorTranslator.FromHtml("#FFCC00");
                HeaderGridRow.Cells.Add(HeaderCell);

                //Add Transfer To
                HeaderCell = new TableCell();
                HeaderCell.Text = "TRANSFER TO";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BorderWidth = 1;
                HeaderCell.BorderColor = ColorTranslator.FromHtml("#FFCC00");
                HeaderGridRow.Cells.Add(HeaderCell);

                grvInventoryTransfers.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public string GetUrl(object ID)
        {
            Session["StackId"] = ID.ToString();
            string url = "~/ReportInventoryTransfer.aspx";
            return url;

        }

        protected void grvInventoryTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InventoryTransferModel.CancelInventoryTransfer(new Guid(grvInventoryTransfers.SelectedDataKey[0].ToString()));
                Messages1.SetMessage("Record cancelled successfully.", WarehouseApplication.Messages.MessageType.Success);                
               DateTime test;
                if(DateTime.TryParse(txtTrannsferDateSrch.Text,out test))
                    BindInventoryTransfer();
            }
            catch (Exception ex)
            {
                Messages1.SetMessage(ex.Message, WarehouseApplication.Messages.MessageType.Error);

            }
        }      
     
    }
}