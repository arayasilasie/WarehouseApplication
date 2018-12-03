using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WarehouseApplication.BLL;
namespace Shed
{
    public partial class AssignShed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindShedNulls();
        }
        private void BindShedNulls()
        {
            WarehouseApplication.DataSetShedTableAdapters.NullShedsTableAdapter objShed = new WarehouseApplication.DataSetShedTableAdapters.NullShedsTableAdapter();
            DataTable dt = new DataTable();
            dt = objShed.GetData(UserBLL.GetCurrentWarehouse());
            gvShow.DataSource = null;
            gvShow.DataBind();
            if (dt.Rows.Count > 0)
            {
                gvShow.DataSource = dt;
                gvShow.DataBind();
            }
        }       
        protected void gvShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            Messages.ClearMessage();
            WarehouseApplication.DataSetShedTableAdapters.tblPickupNoticesTableAdapter objUpdate = new WarehouseApplication.DataSetShedTableAdapters.tblPickupNoticesTableAdapter();
            Session["PUNID"] = gvShow.SelectedDataKey["ID"].ToString();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            WarehouseApplication.DataSetShedTableAdapters.tblPickupNoticesTableAdapter objUpdate = new WarehouseApplication.DataSetShedTableAdapters.tblPickupNoticesTableAdapter();
            objUpdate.UpdateAssignShed(new Guid(Session["PUNID"].ToString()), txtShed.Text);            
            BindShedNulls();
            Messages.SetMessage("Successfully saved…", WarehouseApplication.Messages.MessageType.Success);
            btnUpdate.Enabled = false;
        }    
        protected void gvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShow.PageIndex = e.NewPageIndex;
            BindShedNulls();
        }

        protected void gvShow_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gvShow.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                gvShow.DataSource = dataView;
                gvShow.DataBind();
            }

        }
        //The following delcarations are for handling the sorting and paging 
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

    }
}
