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
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddStack : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                LoadControls();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            Nullable<Guid> CommodityGradeId = null;
            Nullable<Guid> ShedId = null;
            StackStatus Status;
            Nullable<DateTime> DateStarted = null;
            string StackNumer;
            try
            {
                CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
            }
            catch{
                this.lblmsg.Text = "Please select Commodity Grade";
                return ;
            }

            try
            {
                ShedId = new Guid(this.cboShed.SelectedValue.ToString());
            }
            catch
            {
                this.lblmsg.Text = "Please select shed";
                return;
            }
            StackNumer = this.txtStackNumber.Text;
            try
            {
                Status = (StackStatus)int.Parse(this.cboStatus.SelectedValue.ToString());
            }
            catch
            {
                this.lblmsg.Text = "Please select Status";
                return;
            }
            try
            {
                DateStarted = DateTime.Parse(this.txtDateStarted.Text);
            }
            catch
            {
                this.lblmsg.Text = "Please select Date Started.";
                return;
            }
            CommodityGradeBLL objCG = new CommodityGradeBLL();
            objCG = CommodityGradeBLL.GetCommodityGrade((Guid)CommodityGradeId);
            if(objCG == null)
            {

                this.lblmsg.Text = "Unable to get commodity Grade.Please try again";
                return;
            }
            if(string.IsNullOrEmpty(objCG.Symbol) == true)
            {
                this.lblmsg.Text = "Unable to get commodity Grade Symbol.Please try again";
                return;
            }

            int NoBags,PhysicalAddress;
            StackBLL objStack = new StackBLL();
            objStack.CommodityGradeid =(Guid) CommodityGradeId;
            objStack.CommodityGradeid = (Guid)CommodityGradeId;
            objStack.ShedId = (Guid)ShedId;
            if (int.TryParse(this.txtBeginingNoBags.Text, out NoBags) == false)
            {
                this.lblmsg.Text = "Please correct Begining No. Bags. ";
                return;
            }
            if (int.TryParse(this.cboStackNumber.SelectedValue.ToString(), out PhysicalAddress) == false)
            {
                this.lblmsg.Text = "Please Select Physical Stack Number.  ";
                return;
            }
            string stackName = this.cboStackNumber.SelectedValue + "-" + objCG.Symbol + "-" + this.txtDateStarted.Text;

            //productionyearstack
            int productionYear = int.Parse(this.cboProductionYear.SelectedValue.ToString());

            objStack.BeginingNoBags = NoBags;
            objStack.PhysicalAddress = PhysicalAddress;
            objStack.StackNumber = stackName;         
            objStack.DateStarted = (DateTime)DateStarted;
            objStack.Status = Status;
            objStack.WarehouseId = new Guid(this.cboWarehouse.SelectedValue.ToString());
            objStack.ProductionYear = productionYear;
            if (objStack.ValidateForSave() == true)
            {
                isSaved = objStack.Add();
                if (isSaved == true)
                {
                    this.lblmsg.Text = "Data updated Successfully";
                    this.txtStackNumber.Text = stackName;
                   //Clear();
                    this.btnSave.Enabled = false;

                }
                else
                {
                    this.lblmsg.Text = "Unable to add this record.";
                }
            }
        }
        private void LoadControls()
        {
            // Loading Warehoouse.

            List<WarehouseBLL> listWarehouse = new List<WarehouseBLL>();
            try
            {
                listWarehouse = WarehouseBLL.GetAllActiveWarehouse();
            }
            catch( Exception ex)
            {
                this.lblmsg.Text = ex.Message;
                return;
            }
            //this.cboWarehouse.Items.Add( new ListItem("Please Select warehouse",""));
            if (listWarehouse.Count > 0)
            {
                this.cboWarehouse.AppendDataBoundItems = true;
                foreach (WarehouseBLL ow in listWarehouse)
                {
                    if (ow.WarehouseId == UserBLL.GetCurrentWarehouse())
                    {
                        this.cboWarehouse.Items.Add(new ListItem(ow.WarehouseName.ToString(), ow.WarehouseId.ToString()));
                        this.cboWarehouse.SelectedValue = ow.WarehouseId.ToString();
                    }
                }
                this.cboWarehouse.AppendDataBoundItems = false;
                WarehouseChanged();

            }
            //productionyearStack
            int currYear;
            currYear = int.Parse(ConfigurationSettings.AppSettings["CurrentEthiopianYear"]);
            this.cboProductionYear.Items.Clear();
            this.cboProductionYear.Items.Add(new ListItem("Please Select Production Year.", ""));
            this.cboProductionYear.AppendDataBoundItems = true;
            for (int i = currYear - 2; i <= currYear; i++)
            {
                this.cboProductionYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
          

        }

        protected void cboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboShed.Items.Clear();
            // load Stack based on the selected index.
            if (this.cboWarehouse.SelectedValue == "")
            {
                return;
            }
            else
            {
                Guid WarehouseId = new Guid();
                ShedBLL objShed = new ShedBLL();
               
                try
                {
                    WarehouseId = new Guid(this.cboWarehouse.SelectedValue.ToString());
                }
                catch
                {
                    this.lblmsg.Text = "Please select warehouse and try again";
                    return;
                }
               
                List<ShedBLL> list = new List<ShedBLL>();
                list = objShed.GetActiveShedByWarehouseId(WarehouseId);
                this.cboShed.Items.Add( new ListItem("Please Select Shed",""));
                if(list.Count > 0 ) 
                {
                    foreach (ShedBLL oshed in list)
                    {
                        this.cboShed.Items.Add(new ListItem(oshed.ShedNumber, oshed.Id.ToString()));
                    }
                }
            }

        }

        

        protected void cboShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cboShed.SelectedValue.ToString()) == false)
            {
                Guid Id = new Guid(this.cboShed.SelectedValue.ToString());
                ShedBLL objShed = new ShedBLL();
                objShed = objShed.GetActiveShedById(Id);
                objShed.NoStack = 150;
                this.cboStackNumber.Items.Clear();
                this.cboStackNumber.Items.Add(new ListItem("Please Select physical Stack No", ""));
                if (objShed != null)
                {
                    for (int i = 1; i < objShed.NoStack; i++)
                    {
                        this.cboStackNumber.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                }
            }
            else
            {
                this.lblmsg.Text = "Please select Shed";
                return;
            }
        }

        protected void cboCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboCommodityGrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            this.cboWarehouse.SelectedIndex = -1;
            this.cboShed.SelectedIndex = -1;
            this.cboShed.Items.Clear();
            this.cboCommodity.SelectedIndex = -1;
            this.cboCommodityClass.SelectedIndex = -1;
            this.cboCommodityGrade.SelectedIndex = -1;
            this.txtDateStarted.Text = "";
            this.txtStackNumber.Text = "";
            this.cboStatus.SelectedIndex = -1;
        }
        private void WarehouseChanged()
        {
            this.cboShed.Items.Clear();
            // load Stack based on the selected index.
            if (this.cboWarehouse.SelectedValue == "")
            {
                return;
            }
            else
            {
                Guid WarehouseId = new Guid();
                ShedBLL objShed = new ShedBLL();

                try
                {
                    WarehouseId = new Guid(this.cboWarehouse.SelectedValue.ToString());
                }
                catch
                {
                    this.lblmsg.Text = "Please select warehouse and try again";
                    return;
                }

                List<ShedBLL> list = new List<ShedBLL>();
                list = objShed.GetActiveShedByWarehouseId(WarehouseId);
                this.cboShed.Items.Add(new ListItem("Please Select Shed", ""));
                if (list.Count > 0)
                {
                    foreach (ShedBLL oshed in list)
                    {
                        this.cboShed.Items.Add(new ListItem(oshed.ShedNumber, oshed.Id.ToString()));
                    }
                }
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnSave")
            {
                cmd = new List<object>();
                cmd.Add(this.btnSave);
            }
            return cmd;
        }

        #endregion
    }
}