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
using System.Data.SqlClient;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;
using AjaxControlToolkit;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditUnloading : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                BindUnloading();
                BindStackInformation();
                this.cboUnloadedBy.Items.Clear();
                this.cboUnloadedBy.Items.Add(new ListItem("Please select Inveroty Controller", ""));
                List<UserBLL> list = UserRightBLL.GetUsersWithRight("InventoryController");
                if (list != null)
                {
                    if (list.Count > 0)
                    {

                        foreach (UserBLL u in list)
                        {
                            this.cboUnloadedBy.Items.Add(new ListItem(u.FullName, u.UserId.ToString()));
                        }
                    }
                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            this.lblMsg2.Text = "";
            this.lblmsg.Text = "";
            List<StackUnloadedBLL> listtoBeUpdated = null;
            List<StackUnloadedBLL> listOriginal = null;
            if(ViewState["vwStackUnloaded"] == null)
            {
                this.lblmsg.Text = "An error has occured please try again.If the error persists contact the administrators";
                return;
            }
            else
            {
                listtoBeUpdated = (List<StackUnloadedBLL>)ViewState["vwStackUnloaded"];
            }
            if (ViewState["vwStackUnloadedOriginal"] == null)
            {
                this.lblmsg.Text = "An error has occured please try again.If the error persists contact the administrators";
                return;
            }
            else
            {
                listOriginal = (List<StackUnloadedBLL>)ViewState["vwStackUnloadedOriginal"];
            }



            float remainder;
            int lotsizeInBags = 0;
            Guid CommGradeId = new Guid(ViewState["UnloadingCommGradeId"].ToString());
            lotsizeInBags = CommodityGradeBLL.GetCommodityGradeLotSizeInBagsById(CommGradeId);
            UnloadingBLL obj = new UnloadingBLL();
            
            try
            {
                obj.Id = new Guid(this.hfUnloadingId.Value.ToString());

            }
            catch
            {
                this.lblmsg.Text = "Unable to update this record please try agian";
                return;
            }
            
            Nullable<int> totbag = null;
            if(DataValidationBLL.isInteger(this.txtNumberOfBags.Text , out totbag)== true)
            {
                obj.TotalNumberOfBags = (int)totbag;
            }
            else
            {
                this.lblmsg.Text = "Please Enter Number of bags.";
                this.txtNumberOfBags.Focus();
                return;
            }
            //checking that the Number of Bags aree within ecx lot defn.
           
            remainder = ((int)totbag) % lotsizeInBags;
            int SamplerNoBags = -1;
            int.TryParse(ViewState["SamplingNoBags"].ToString(), out SamplerNoBags);
            
            if (SamplerNoBags != -1)
            {
                if (((int)totbag) != SamplerNoBags)
                {
                    //Check 
                    string str = " The number of bags counted by the sampler is " +
                         SamplerNoBags.ToString() + " which is different from what you entered.  Are you sure you want to save this record?";
                    lblModalMsg.Text = str;
                    //TODO: Remove Comment
                    //ModalPopupExtender1.Show();

                }
            }
            //TODO: Remove
            //ModalPopupExtender mdl = new ModalPopupExtender();
            //mdl = ModalPopupExtender1;
            remainder = ((int)totbag) % lotsizeInBags;

            if (((int)remainder) != 0)
            {
                lblModalMsg.Text = "Per ECX rule, the number of bags entered is not acceptable.  Do you still want to save?";
                //TODO: Remove Comment
                //ModalPopupExtender1.Show();

            }


            int totBagCount = 0;
            if (ViewState["totBagCount"] != null)
            {
                if ((int.TryParse(ViewState["totBagCount"].ToString(),out totBagCount)) != true)
                {
                    this.lblmsg.Text = "Unable to get the total sum of the bags in each stack unloaded.Please return to the search page and try again.";
                    return;
                }
            }
            if (totBagCount != (int)totbag)
            {
                this.lblmsg.Text = "The Total Number of bags doesn't match the sum of bags unloaded in each stack.";
                return;
            }

            Nullable<DateTime> depositeddate = null;
            if (DataValidationBLL.isDate(this.txtDateDeposited.Text, out depositeddate) == true)
            {
                obj.DateDeposited =(DateTime) depositeddate;
            }
            else
            {
                this.lblmsg.Text = "Please select date deposited.";
                this.txtDateDeposited.Focus();
                return;
            }
            Nullable<Int32> status = null;
            if (DataValidationBLL.isInteger(this.cboStatus.SelectedValue.ToString(), out status) == true)
            {
                obj.Status = (UnloadingStatus)(Int32) status;
            }
            else
            {
                this.lblmsg.Text = "Please select Status.";
                this.cboStatus.Focus();
                return;
            }
            try
            {

                if (ViewState["vwStackUnloaded"] == null)
                {
                    this.lblmsg.Text = "An error has occured please try agian.If the error persists contact the administrator";
                    return;
                }



                if (obj.Update(listtoBeUpdated, listOriginal) == true)
                {
                    this.lblmsg.Text = "Data updated Successfully.";
                }
                else
                {
                    this.lblmsg.Text = "Unable to update data please try again.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }
        protected void cboShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO :Remove the ff
           

            ////populate Stack from the list.
            this.cboStackNo.Items.Add(new ListItem("Please select Stack", ""));
            this.cboStackNo.AppendDataBoundItems = true;
            List<StackBLL> list = new List<StackBLL>();
            StackBLL obj = new StackBLL();
            list = obj.GetActiveStackbyShedId(new Guid(this.cboShed.SelectedValue.ToString()));
            if (list.Count < 1)
            {
                this.lblMsg2.Text = "Ther are no stacks in this shed.";
                this.btnSave.Enabled = false;
            }
            else
            {
                this.cboStackNo.DataSource = list;
                this.cboStackNo.DataTextField = "StackNumber";
                this.cboStackNo.DataValueField = "Id";
                this.cboStackNo.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.lblMsg2.Text = "";
            this.lblmsg.Text = "";
            int totalNumberofBags ;
            int totBagCount = 0;
          
            int bag = 0;
            if ((int.TryParse(this.txtNumberOfBags.Text, out totalNumberofBags)) == false)
            {
                this.lblmsg.Text = "Please enter Total Number of Bags";
                return;
            }
            if (ViewState["totBagCount"] != null)
            {
                if((int.TryParse(ViewState["totBagCount"].ToString(),out totBagCount)) != true )
                {
                    this.lblmsg.Text = "Invalid Total Number of Bags per Stack.";
                     return;
                }
            }
            else
            {
                totBagCount = 0;
            }
          
            if ((int.TryParse(this.txtStackNoBags.Text, out bag)) != true)
            {
                 this.lblMsg2.Text = "Please Enter Number of bags.";
                this.txtStackNoBags.Focus();
                return;
            }

            if (totalNumberofBags < totBagCount + bag)
            {
                this.lblMsg2.Text = "The sum of the number per stack exceeds the total number of bags.";
                this.txtStackNoBags.Focus();
                return;
            }
            
          
            StackUnloadedBLL obj = new StackUnloadedBLL();
            Nullable<Guid> unloadingId = null;
            if (DataValidationBLL.isGUID(this.hfUnloadingId.Value.ToString(), out unloadingId) == true)
            {
                obj.UnloadingId = (Guid)unloadingId;
            }
            else
            {
                this.lblMsg2.Text = "Please Select Stack";
                this.cboStackNo.Focus();
                return;
            }



           
            try
            {
                
                obj.StackId = new Guid(this.cboStackNo.SelectedValue.ToString());
                obj.StackNo = this.cboStackNo.SelectedItem.Text;
            }
            catch
            {
   
                this.lblMsg2.Text = "Please Select Stack";
                this.cboStackNo.Focus();
                return;
            }
            Nullable<Guid> user = null;
            if (DataValidationBLL.isGUID(this.cboUnloadedBy.SelectedValue.ToString(), out user) == true)
            {
                obj.UserId =(Guid) user;
            }
            else
            {
                 this.lblMsg2.Text = "Please Select Inventory controller";
                 this.cboUnloadedBy.Focus();
                 return;
            }
            obj.NumberOfbags = (int)bag;
            obj.Status =  StackUnloadedStatus.New;
            obj.Remark = this.txtRemark.Text;
            List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
            list = (List<StackUnloadedBLL>)ViewState["vwStackUnloaded"];
            list.Add(obj);
            ViewState["vwStackUnloaded"] = list;
            ViewState["totBagCount"] = totBagCount + bag;
            BindStackInformation();

        }
        protected void gvStackUnloaded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        private void BindUnloading()
        {
            try
            {
                Guid UnloadingId = new Guid(Request.QueryString["Id"].ToString());
                this.hfUnloadingId.Value = UnloadingId.ToString();
                UnloadingBLL obj = new UnloadingBLL();
                CommodityGradeBLL objCommodityGrade = new CommodityGradeBLL();
                obj.Id = UnloadingId;
                obj = obj.GetById();
              
                if (obj != null)
                {
                    this.lblGradingCode.Text = obj.GradingCode.ToString();
                    string CommodityGradeName = CommodityGradeBLL.GetCommodityGradeNameById(obj.CommodityGradeId);
                    this.lblCommodityGrade.Text = CommodityGradeName;
                    this.txtNumberOfBags.Text = obj.TotalNumberOfBags.ToString();
                    this.txtDateDeposited.Text = obj.DateDeposited.ToShortDateString();
                    this.cboStatus.SelectedValue = ((int)obj.Status).ToString();
                    ViewState["UnloadingCommGradeId"] = obj.CommodityGradeId;
                    ViewState["totBagCount"] = obj.TotalNumberOfBags.ToString();
                    // Get Grading Id
                    GradingResultBLL objGradingResult = new GradingResultBLL();
                    objGradingResult = objGradingResult.GetGradingResultById(obj.GradingResultId);
                    if (objGradingResult != null)
                    {
                        GradingBLL objGrading = new GradingBLL();
                        objGrading = objGrading.GetById(objGradingResult.GradingId);
                        if (objGrading != null)
                        {
                            SamplingResultBLL objSamplingResult = new SamplingResultBLL();
                            objSamplingResult = objSamplingResult.GetSamplingResultById(objGrading.SamplingResultId);
                            if (objSamplingResult != null)
                            {
                                ViewState["SamplingNoBags"] = objSamplingResult.NumberOfBags;
                            }
                            else
                            {
                                this.lblmsg.Text = "An error has occured please try again.If the error persists contact the administrator";
                                return;
                            }

                        }
                        else
                        {
                            this.lblmsg.Text = "An error has occured please try agin.If the error persists contact the administrator";
                            return;
                        }
                    }
           

                }

                //TODO :Remove the ff

               // this.cboStackNo.Items.Add(new ListItem("Stack No 1", "eaf7ebcc-f39a-4c7d-b855-1e5d5578dbcb"));

                //TODO Remove Comment

                Guid warehouseId = UserBLL.GetCurrentWarehouse();
                // Loading shedby warehouse.
                ShedBLL shed = new ShedBLL();
                List<ShedBLL> shedlist = new List<ShedBLL>();
                shedlist = shed.GetActiveShedByWarehouseId(warehouseId);
                cboShed.Items.Add(new ListItem("Please select Shed", ""));
                if (shedlist.Count > 0)
                {
                    foreach (ShedBLL o in shedlist)
                    {
                        cboShed.Items.Add(new ListItem(o.ShedNumber, o.Id.ToString()));
                    }
                }
                else
                {
                    this.btnAdd.Enabled = false;
                }
               
                GRNBLL objGRN = new GRNBLL();
                string strE = " UnLoadingId='" + UnloadingId.ToString()+ "' ";
                if (objGRN.IsEditableGRN(strE) == false)
                {
                    this.btnAdd.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.lblmsg.Text = "You can't edit this information as GRN has already been created using this Unloading Information.";
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }
           



        }
        private void BindStackInformation()
        {
            if( ViewState["vwStackUnloaded"] == null )
            {
                StackUnloadedBLL obj = new StackUnloadedBLL();
                try
                {
                    obj.UnloadingId =  new Guid(Request.QueryString["Id"].ToString());
                }
                catch
                {
                    this.lblMsg2.Text ="Unable to process the request";
                    return;
                }
                List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
                list = obj.GetStackInformationByUnloadingId();
                if (list != null)
                {
                    ViewState["vwStackUnloaded"] = list;
                    ViewState["vwStackUnloadedOriginal"] = list;
                    this.gvStackUnloaded.DataSource = list;
                    this.gvStackUnloaded.DataBind();
                }
                else
                {
                    this.lblMsg2.Text = "No Stack Information.";
                }
            }
            else
            {
                List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
                list =(List<StackUnloadedBLL>) ViewState["vwStackUnloaded"];
                int totBagCount = 0;
                foreach(StackUnloadedBLL su in list)
                {
                    if (su.Status == StackUnloadedStatus.New)
                    {
                        totBagCount = su.NumberOfbags;
                    }
                }
                if (ViewState["totBagCount"] == null)
                {
                    ViewState["totBagCount"] = totBagCount;
                }
                this.gvStackUnloaded.DataSource = list;
                this.gvStackUnloaded.DataBind();
            }

        }
        protected void gvStackUnloaded_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            this.lblMsg2.Text = "Updating Stack Information.";
            this.gvStackUnloaded.EditIndex = e.NewEditIndex;
            Label lblEditstat = (Label)this.gvStackUnloaded.Rows[this.gvStackUnloaded.EditIndex].FindControl("lblEditstatus");
            if (this.gvStackUnloaded.EditIndex != -1)
            {
                //DropDownList cboInv = (DropDownList)this.gvStackUnloaded.Rows[this.gvStackUnloaded.EditIndex].FindControl("cboStackUnloadedStatus");
                //cboInv.SelectedValue = lblEditstat.Text;
            }
           
            BindStackInformation();
            //List<UserBLL> list = UserRightBLL.GetUsersWithRight("InventoryController");
            //DropDownList cboInv = (DropDownList)this.gvStackUnloaded.Rows[this.gvStackUnloaded.EditIndex].FindControl("cboUnloadedBy");
            //cboInv.Items.Clear();
            //cboInv.Items.Add(new ListItem("Please Select Inventory Controller",""));
            //foreach(UserBLL u in list )
            //{
            //    cboInv.Items.Add( new ListItem(u.UserName , u.UserId.ToString()));
            //}
            //Label lblUI = (Label)this.gvStackUnloaded.Rows[this.gvStackUnloaded.EditIndex].FindControl("lblUserId");
            //cboInv.SelectedValue = lblUI.Text;
                

        }
        protected void gvStackUnloaded_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.lblMsg2.Text = "";
            this.gvStackUnloaded.EditIndex = -1;
            BindStackInformation();
        }
        protected void gvStackUnloaded_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.lblMsg2.Text = "";
            this.lblmsg.Text = "";
            StackUnloadedBLL obj = new StackUnloadedBLL();
            GridViewRow row = this.gvStackUnloaded.Rows[e.RowIndex];
            Label lblId = row.FindControl("lblId") as Label;
            TextBox remark = row.FindControl("txtStackRemark") as TextBox;
            Label lblEditstatus = row.FindControl("lblEditstatus") as Label;
            Label lblNoBag = row.FindControl("lblNoBag") as Label;
            int CurrBag = 0;
            if ((int.TryParse(lblNoBag.Text.ToString(),out CurrBag)) != true)
            {
                throw new Exception("Invalid No. Bags");
            }


            DropDownList cg = row.FindControl("cboStackUnloadedStatus") as DropDownList;
            if (cg.SelectedValue.ToString() == "3")
            {
                
                if (remark != null)
                {
                    if (remark.Text == "" || remark.Text == string.Empty)
                    {
                        this.lblMsg2.Text = "Please provide Remark.";
                        return;
                    }
                    else
                    {
                        obj.Remark = remark.Text;
                    }
                }
            }
            List<StackUnloadedBLL> list = new List<StackUnloadedBLL>();
            if (ViewState["vwStackUnloaded"] != null)
            {
                list = (List<StackUnloadedBLL>)ViewState["vwStackUnloaded"];
                
                ViewState["vwStackUnloaded"] = list; 
                int totBagCount = 0;
                if (ViewState["totBagCount"] != null)
                {
                    totBagCount = int.Parse(ViewState["totBagCount"].ToString());
                    if (lblEditstatus.Text == "New" && cg.SelectedValue.ToString().Trim() == "Cancelled")
                    {
                        totBagCount = totBagCount - CurrBag;
                    }
                    else if (lblEditstatus.Text == "Cancelled" && cg.SelectedValue.ToString().Trim() == "New")
                    {
                        int xtotalNumberofBags;
                        int xtotBagCount = 0;
                        int xbag = 0;
                        if ((int.TryParse(this.txtNumberOfBags.Text, out xtotalNumberofBags)) == false)
                        {
                            this.lblmsg.Text = "Please enter Total Number of Bags";
                            return;
                        }
                        if (ViewState["totBagCount"] != null)
                        {
                            if ((int.TryParse(ViewState["totBagCount"].ToString(), out xtotBagCount)) != true)
                            {
                                this.lblmsg.Text = "Invalid Total Number of Bags per Stack.";
                                return;
                            }
                        }
                        else
                        {
                            totBagCount = 0;
                        }

                        if((int.TryParse(lblNoBag.Text ,out  xbag ) != true ))
                        {
                            this.lblmsg.Text = "Invalid Stack Number of Bags.";
                            return;
                        }

                        if (xtotalNumberofBags > (totBagCount + CurrBag))
                        {
                            totBagCount = totBagCount + CurrBag;
                        }
                        else
                        {
                            this.lblMsg2.Text = "The Sum of the bags unloaded per stack exceeds the Total count";
                            return ;
                        }
                        foreach (StackUnloadedBLL i in list)
                        {
                            if (i.Id.ToString().ToLower() == lblId.Text.ToLower())
                            {
                                if (cg.SelectedValue.ToString().Trim() == "New")
                                {
                                    i.Status = StackUnloadedStatus.New;
                                }
                                else if (cg.SelectedValue.ToString().Trim() == "Cancelled")
                                {
                                    i.Status = StackUnloadedStatus.Cancelled;
                                }
                                i.Remark = remark.Text;
                                break;
                            }
                        }
                    }
                    List<StackUnloadedBLL> listOriginal = new List<StackUnloadedBLL>();
                    bool isSavedtoDb = false;
                    listOriginal = (List<StackUnloadedBLL>)ViewState["vwStackUnloadedOriginal"];
                    foreach (StackUnloadedBLL o in listOriginal)
                    {
                        if (o.Id == new Guid(lblId.Text))
                        {
                            isSavedtoDb = true;
                            
                        }
                        if (isSavedtoDb == true)
                            break;
                       
                    }
                    if (isSavedtoDb == false)
                    {
                        int x = 0;
                        foreach (StackUnloadedBLL i in list)
                        {
                            if (i.Id == new Guid(lblId.Text))
                            {
                                break;
                            }
                            x++;
                        }
                        list.RemoveAt(x);
                        ViewState["vwStackUnloaded"] = list;

                    }
                    else
                    {
                        int x = 0;
                        foreach (StackUnloadedBLL i in list)
                        {
                            if (i.Id == new Guid(lblId.Text))
                            {
                                break;
                            }
                            x++;
                        }
                        if (cg.SelectedValue.ToString() == "New")
                        {
                            list[x].Status = StackUnloadedStatus.New;
                        }
                        else if (cg.SelectedValue.ToString() == "Cancelled")
                        {
                            list[x].Status = StackUnloadedStatus.Cancelled;
                        } 
                        // call the update Method to change the status.
                       
                        ViewState["vwStackUnloaded"] = list;
                    }

                    ViewState["totBagCount"] = totBagCount;
                }
                 

                this.gvStackUnloaded.EditIndex = -1;
                BindStackInformation();
               
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
            else if(name == "btnAdd" )
            {
                cmd = new List<object>();
                cmd.Add(this.btnAdd);
            }
           
            return cmd;
        }

        #endregion
        protected void gvStackUnloaded_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StackUnloadedBLL obj = (StackUnloadedBLL)e.Row.DataItem;

                if (this.gvStackUnloaded.EditIndex == -1)
                {

                    Label lblInv = (Label)e.Row.FindControl("lblInvetoryCont");
                    Label lblStaclg = (Label)e.Row.FindControl("lblStackNo");
                    lblInv.Text = UserRightBLL.GetUserNameByUserId(obj.UserId);
                    lblStaclg.Text = obj.StackNo;
                }
                else
                {
                   
                    //Label lblEditstat = (Label)e.Row.FindControl("lblEditstatus");
                    //DropDownList cboInv = (DropDownList)e.Row.FindControl("cboStackUnloadedStatus");
                    //cboInv.SelectedValue = lblEditstat.Text;
                }
            }
        }
    }
}