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

namespace WarehouseApplication.UserControls
{
    public partial class CalendarToggler : System.Web.UI.UserControl
    {
        public DateTime SelectedDate
        {
            get { return calExpanded.SelectedDate; }
            set
            {
                calExpanded.SelectedDate = value;
                lblShorthand.Text = value.ToShortDateString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calExpanded_SelectionChanged(null, null);
                calExpanded.SelectedDate = DateTime.Today;
                lblShorthand.Text = DateTime.Today.ToShortDateString();
            }
        }
        protected void calExpanded_SelectionChanged(object sender, EventArgs e)
        {
            panShortsand.Visible = true;
            calExpanded.Visible = false;
            lblShorthand.Text = calExpanded.SelectedDate.ToShortDateString();
        }
        protected void btnSet_Click(object sender, EventArgs e)
        {
            calExpanded.Visible = true;
            panShortsand.Visible = false;
        }
    }
}