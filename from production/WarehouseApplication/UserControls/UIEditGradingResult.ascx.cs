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
using System.Collections.Generic;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditGradingResult : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            Nullable<Guid> GradingResultId = null;
            try
            {
                GradingResultId = new Guid(Request.QueryString.Get("id").ToString());
                this.hfGradingResultId.Value = GradingResultId.ToString();
            }
            catch 
            {
                this.lblMsg.Text = "Invalid Id!";
            }

           





            GradingResultBLL objGradingResult = new GradingResultBLL();
            if (GradingResultId != null)
            {
                objGradingResult = objGradingResult.GetGradingResultById((Guid)GradingResultId);
            }
            if (objGradingResult == null)
            {
                this.lblMsg.Text = "Record can not be found!";
            }
            // Get grading Code 
            //this.txtGradingCode.Text = 
            this.txtDateRecived.Text = objGradingResult.GradeRecivedTimeStamp.ToShortDateString();
            this.txtTimeRecived.Text = objGradingResult.GradeRecivedTimeStamp.ToShortTimeString();
            this.chkIsSupervisor.Checked = objGradingResult.IsSupervisor;
            this.txtRemark.Text = objGradingResult.Remark;
            this.cboStatus.SelectedIndex = (int)objGradingResult.Status;


            // //Load Control values.
            ////Loading Commodity.
            //ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            //ECXLookUp.CCommodityGrade[] objCC = objEcxLookUp.GetActiveCommodityGrades(new Guid("33673ac2-7888-42d5-9712-522941b3208c"), new Guid(ID.ToString()));
            //foreach (ECXLookUp.CCommodityGrade cc in objCC)
            //{
            //    this.cboCommodityGrade.Items.Add(new ListItem(cc.Name.ToString(),cc.UniqueIdentifier.ToString()));
            //    if (cc.UniqueIdentifier == objGradingResult.ID)
            //    {
            //        CommodityClassId = new Guid(cc.CommodityClassUniqueIdentifier);
            //        this.cboCommodityGrade.SelectedValue = objGradingResult.CommodityGradeId.ToString();
            //    }
            //}



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnUpdate")
            {
                cmd = new List<object>();
                cmd.Add(this.btnUpdate);
            }
            return cmd;
        }

        #endregion
    }
}