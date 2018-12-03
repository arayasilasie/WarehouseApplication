using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.SECManager;

namespace WarehouseApplication
{
    public partial class ListGIN : ECXWarehousePage, ISecurityConfiguration //System.Web.UI.Page
    {
        private ILookupSource ginLookup = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ginLookup = GINProcessBLL.StaticLookupSource;
            SearchConditionSelector1.LookupSource = ginLookup;
            if (!IsPostBack)
            {
                SearchConditionSelector1.DataFilter = GINProcessBLL.GetGINCatalogFilter("CompleteFilterSet");
            }
            SetCatalogData();
        }

        private void SetCatalogData()
        {
            List<IDataIdentifier> ids = null;
            try
            {
                IDataFilter filter = SearchConditionSelector1.DataFilter;
                foreach (DataFilterParameter parameter in filter.Parameters)
                {
                    if (parameter.Name == "WarehouseId")
                    {
                        filter.SetCondition(
                            new DataFilterCondition(
                                parameter, FilterConditionType.Comparison,
                                string.Empty, ginLookup.GetLookup("CurrentWarehouse")["Id"]));
                    }
                    else
                    {
                        filter.SetCondition(SearchConditionSelector1[parameter.Name]);
                    }
                }
                ids = GINProcessBLL.SearchGIN(filter);
            }
            catch
            {
                ids = new List<IDataIdentifier>();
            }

            string buffer = string.Empty;
            foreach (IDataIdentifier identifier in ids)
            {
                buffer += identifier.Preview.DocumentElement.InnerXml;
            }
            string ginProcessSet = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <Catalog>" + buffer + "</Catalog>";
            xdsGINSource.Data = ginProcessSet;
            gvGIN.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            panCommands.Visible = false;
            SetCatalogData();
            gvGIN.SelectedIndex = -1;
        }

        protected string GetStatusName(object status)
        {
            return ginLookup.GetLookup("GINStatus")[int.Parse(status.ToString())];
        }

        protected string GetCommodityGradeName(object commodityGrade)
        {
            return ginLookup.GetLookup("CommodityGrade")[new Guid(commodityGrade.ToString())];
        }

        protected string GetClientId(object clientId)
        {
            return ginLookup.GetLookup("ClientId")[new Guid(clientId.ToString())];
        }

        protected string GetClientName(object clientId)
        {
            return ginLookup.GetLookup("Client")[new Guid(clientId.ToString())];
        }

        protected void gvGIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            panCommands.Visible = true;
            if (SelectedStatus == GINStatusType.ReadyToLoad)
            {
                btnDriverInfo.Visible = true;
                btnLoadingInfo.Visible=false;
                btnScalingInfo.Visible=false;
                btnPrintGIN.Visible = false;
            }else if (SelectedStatus == GINStatusType.Loaded)
            {
                btnDriverInfo.Visible = true;
                btnLoadingInfo.Visible = true;
                btnScalingInfo.Visible = false;
                btnPrintGIN.Visible = false;
            }
            //else if (SelectedStatus == GINStatusType.Scaled)
            //{
            //    btnDriverInfo.Visible = true;
            //    btnLoadingInfo.Visible = true;
            //    btnScalingInfo.Visible = true;
            //    btnPrintGIN.Visible = false;
            //}
            else if ((SelectedStatus == GINStatusType.GINGenerated) ||
                (SelectedStatus == GINStatusType.GINSigned) ||
                (SelectedStatus == GINStatusType.GINApproved) ||
                (SelectedStatus == GINStatusType.Scaled))
            {
                btnDriverInfo.Visible = true;
                btnLoadingInfo.Visible = true;
                btnScalingInfo.Visible = true;
                btnPrintGIN.Visible = true;
            }
        }

        protected void btnDriverInfo_Click(object sender, EventArgs e)
        {
            PageDataTransfer driverDateTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/EditTruckInformation.aspx");
            driverDateTransfer.TransferData["TransactionId"] = SelectedTransactionId;
            driverDateTransfer.TransferData["IsGINTransaction"] = true;
            driverDateTransfer.TransferData["NoWorkflow"] = true;
            driverDateTransfer.TransferData["ReturnPage"] = Request.Path;
            driverDateTransfer.Navigate();
        }

        protected void btnLoadingInfo_Click(object sender, EventArgs e)
        {
            PageDataTransfer loadDataTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/EditTruckLoading.aspx");
            loadDataTransfer.TransferData["TransactionId"] = SelectedTransactionId;
            loadDataTransfer.TransferData["IsGINTransaction"] = true;
            loadDataTransfer.TransferData["NoWorkflow"] = true;
            loadDataTransfer.TransferData["ReturnPage"] = Request.Path;
            loadDataTransfer.Navigate();
        }

        protected void btnScalingInfo_Click(object sender, EventArgs e)
        {
            PageDataTransfer driverDateTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/EditTruckScaling.aspx");
            driverDateTransfer.TransferData["TransactionId"] = SelectedTransactionId;
            driverDateTransfer.TransferData["IsGINTransaction"] = true;
            driverDateTransfer.TransferData["NoWorkflow"] = true;
            driverDateTransfer.TransferData["ReturnPage"] = Request.Path;
            driverDateTransfer.Navigate();
        }

        protected void btnPrintGIN_Click(object sender, EventArgs e)
        {
            PageDataTransfer reportTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ReportViewerForm.aspx");
            reportTransfer.TransferData["TransactionId"] = SelectedTransactionId;
            reportTransfer.TransferData["IsGINTransaction"] = true;
            reportTransfer.TransferData["RequestedReport"] = "rptGINReport";
            reportTransfer.TransferData["ReturnPage"] = Request.Path;
            reportTransfer.PersistToSession();
            ScriptManager.RegisterStartupScript(this,
                this.GetType(),
                "ShowReport",
                "<script type=\"text/javascript\">" +
                    string.Format("javascript:window.open(\"ReportViewerForm.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                    //string.Format("location.href = '{0}';", Request.Path) +
                "</script>",
                false);
        }

        private string SelectedTransactionId
        {
            get
            {
                XElement catalog = XElement.Parse(xdsGINSource.Data);
                var ginTransactionId = (from cg in catalog.Descendants("GIN") where cg.Attribute("GINId").Value == (string)gvGIN.DataKeys[gvGIN.SelectedIndex].Value
                                   select cg.Attribute("TransactionId").Value).First();
                return ginTransactionId;
            }
        }

        private GINStatusType SelectedStatus
        {
            get
            {
                XElement catalog = XElement.Parse(xdsGINSource.Data);
                var selectedStatus = (from cg in catalog.Descendants("GIN") where cg.Attribute("GINId").Value == (string)gvGIN.DataKeys[gvGIN.SelectedIndex].Value
                                   select Int32.Parse(cg.Attribute("Status").Value)).First();
                return (GINStatusType)selectedStatus;
            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> securedResources = new List<object>();
            if (name == "btnDriverInfo")
                securedResources.Add(btnDriverInfo);
            else if (name == "btnLoadingInfo")
                securedResources.Add(btnLoadingInfo);
            else if (name == "btnScalingInfo")
                securedResources.Add(btnScalingInfo);

            return securedResources;
        }

        #endregion
    }
}
