using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class rptSampleTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SamplingBLL Sampling;
            Guid id = new Guid(Request.QueryString["Id"].ToString());
            SamplingBLL obj = new SamplingBLL();

            Sampling = obj.GetSampleDetail(id);

            this.lblSamplingCode.Text = Sampling.SampleCode.ToString();
        }
    }
}
