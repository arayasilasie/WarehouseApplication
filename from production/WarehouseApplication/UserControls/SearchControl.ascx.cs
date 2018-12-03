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
    public partial class SearchControl : System.Web.UI.UserControl
    {
        public string TrackingNo
        {
            get
            {
                return this.txtTrackingNo.Text;
            }

            set
            {
                this.txtTrackingNo.Text = value;
            }
        }

        public string Code
        {
            get
            {
                return this.txtCode.Text;
            }

            set
            {
                this.txtCode.Text = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}