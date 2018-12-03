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
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Xsl;
using WarehouseApplication.BLL;
namespace WarehouseApplication
{
    public partial class ListPickupNotice : System.Web.UI.Page
    {
        private ILookupSource punLookup = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            punLookup = PickupNoticeBLL.StaticLookupSource;
            SearchConditionSelector1.LookupSource = punLookup;
            if (!IsPostBack)
            {
                SearchConditionSelector1.DataFilter = PickupNoticeBLL.GetCatalogFilter("CompleteFilterSet");
            }
            SetCatalogData();
        }

        protected void gvPickupNotice_SelectedIndexChanged(object sender, EventArgs e)
        {
            String pickupNoticeID = (string)gvPickupNotice.DataKeys[gvPickupNotice.SelectedIndex].Value;
            xdsAgentSource.XPath = string.Format("/Catalog/PickupNotice[@Id=\"{0}\"]/PickupNoticeAgents/PickupNoticeAgent", pickupNoticeID);
            xdsWarehouseReceiptSource.XPath = string.Format("/Catalog/PickupNotice[@Id=\"{0}\"]/WarehouseReceipts/WarehouseReceipt", pickupNoticeID);
            XmlDocument listDocument = xdsPickupNoticeSource.GetXmlDocument();
            XmlElement punElement = (XmlElement)listDocument.DocumentElement.SelectSingleNode(string.Format("/Catalog/PickupNotice[@Id=\"{0}\"]", pickupNoticeID));
            btnOpen.Enabled = (punElement.Attributes["Status"].Value ==
                PickupNoticeBLL.StaticLookupSource.GetInverseLookup("Status")["Open"].ToString());
            btnPrint.Enabled = (punElement.Attributes["TransactionId"]!= null) &&
                (punElement.Attributes["TransactionId"].Value != string.Empty);
            btnPrintPUN.Enabled = true;
            gvAgents.Visible = true;
            lblAgents.Visible = true;
            gvWarehouseReceipts.Visible = true;
            lblWarehouseReceipts.Visible = true;
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            PageDataTransfer punAcknowledgementTransfer = new PageDataTransfer(Request.ApplicationPath + "/PickupNoticeAcknowledged.aspx");
            punAcknowledgementTransfer.RemoveAllData();
            punAcknowledgementTransfer.TransferData["PickupNoticeId"] = new Guid((string)gvPickupNotice.DataKeys[gvPickupNotice.SelectedIndex].Value);
            punAcknowledgementTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
            punAcknowledgementTransfer.Navigate();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
                XmlDocument listDocument = xdsPickupNoticeSource.GetXmlDocument();
                XmlElement punElement = (XmlElement)listDocument.DocumentElement.SelectSingleNode(string.Format("/Catalog/PickupNotice[@Id=\"{0}\"]",
                    gvPickupNotice.DataKeys[gvPickupNotice.SelectedIndex].Value));
                PageDataTransfer reportTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ReportViewerForm.aspx");
                reportTransfer.TransferData["TransactionId"] = punElement.Attributes["TransactionId"].Value;
                reportTransfer.TransferData["IsGINTransaction"] = false;
                reportTransfer.TransferData["RequestedReport"] = "rptPUNTrackingReport";
                reportTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                reportTransfer.PersistToSession();
                ScriptManager.RegisterStartupScript(this,
                    this.GetType(),
                    "ShowReport",
                    "<script type=\"text/javascript\">" +
                        string.Format("javascript:window.open(\"ReportViewerForm.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                    //string.Format("location.href = '{0}';", transferedData.GetTransferedData("ReturnPage")) +
                    "</script>",
                    false);

        }

        protected void btnPrintPUN_Click(object sender, EventArgs e)
        {
            XmlDocument listDocument = xdsPickupNoticeSource.GetXmlDocument();
            try
            {
                XmlElement punElement = (XmlElement)listDocument.DocumentElement.SelectSingleNode(string.Format("/Catalog/PickupNotice[@Id=\"{0}\"]",
                    gvPickupNotice.DataKeys[gvPickupNotice.SelectedIndex].Value));
                PageDataTransfer reportTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ReportViewerForm.aspx");
                Guid punId = new Guid((string)gvPickupNotice.DataKeys[gvPickupNotice.SelectedIndex].Value);
                PickupNoticeBLL pun = new PickupNoticeBLL(punId);
                reportTransfer.TransferData["PUNReportData"] = pun.GetPUNReport(punId);
                reportTransfer.TransferData["RequestedReport"] = "rptPUNReport";
                reportTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                reportTransfer.PersistToSession();
                ScriptManager.RegisterStartupScript(this,
                    this.GetType(),
                    "ShowReport",
                    "<script type=\"text/javascript\">" +
                        string.Format("javascript:window.open(\"ReportViewerForm.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                    //string.Format("location.href = '{0}';", transferedData.GetTransferedData("ReturnPage")) +
                    "</script>",
                    false);
            }
            catch (Exception ex)
            {
                Utility.LogException(new Exception(string.Format("XML Doc:{0}", listDocument.DocumentElement.OuterXml)));
                throw (ex);
            }
        }

        protected string GetStatusName(object status)
        {
            return punLookup.GetLookup("Status")[int.Parse(status.ToString())];
        }

        protected string GetCommodityGradeName(object commodityGrade)
        {
            return punLookup.GetLookup("CommodityGrade")[new Guid(commodityGrade.ToString())];
        }

        protected string GetNIDType(object nidType)
        {
            return punLookup.GetLookup("NIDType")[int.Parse(nidType.ToString())];
        }

        protected string GetClientName(object clientId)
        {
            return punLookup.GetLookup("Client")[new Guid((string)clientId)];
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvPickupNotice.Visible = true;
            SetCatalogData();
            gvAgents.Visible = false;
            lblAgents.Visible = false;
            gvWarehouseReceipts.Visible = false;
            lblWarehouseReceipts.Visible = false;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            PickupNoticeBLL.CachePickupNotices();
            SetCatalogData();
            gvAgents.Visible = false;
            lblAgents.Visible = false;
            gvWarehouseReceipts.Visible = false;
            lblWarehouseReceipts.Visible = false;
        }

        private void SetCatalogData()
        {
            List<IDataIdentifier> ids = null;
            try
            {
                IDataFilter filter = SearchConditionSelector1.DataFilter;
                foreach(DataFilterParameter parameter in filter.Parameters)
                {
                    if (parameter.Name == "WarehouseId")
                    {
                        filter.SetCondition(
                            new DataFilterCondition(
                                parameter, FilterConditionType.Comparison, 
                                string.Empty, punLookup.GetLookup("CurrentWarehouse")["Id"]));
                    }
                    else
                    {
                        filter.SetCondition(SearchConditionSelector1[parameter.Name]);
                    }
                }
                ids = PickupNoticeBLL.SearchPickupNotice(filter);
            }
            catch(Exception ex)
            {
                Utility.LogException(ex);
                ids = new List<IDataIdentifier>();
            }
            string buffer = string.Empty;
            foreach (IDataIdentifier identifier in ids)
            {
                buffer += identifier.Preview.DocumentElement.InnerXml;
            }
            string pickupNoticeSet = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <Catalog>" + buffer + "</Catalog>";
            xdsPickupNoticeSource.Data = pickupNoticeSet;
            xdsPickupNoticeSource.DataBind();
            //xdsPickupNoticeSource.TransformFile = Request.PhysicalApplicationPath + "/PickupNoticeTransformer.xslt";
            xdsAgentSource.Data = pickupNoticeSet;
            //xdsAgentSource.TransformFile = Request.PhysicalApplicationPath + "/PickupNoticeTransformer.xslt";
            xdsWarehouseReceiptSource.Data = pickupNoticeSet;
            //xdsWarehouseReceiptSource.TransformFile = Request.PhysicalApplicationPath + "/PickupNoticeTransformer.xslt";
            gvPickupNotice.DataBind();
        }
    }
}
