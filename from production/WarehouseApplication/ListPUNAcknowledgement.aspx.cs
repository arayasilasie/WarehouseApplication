using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Xml;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;

namespace WarehouseApplication
{
    public partial class ListPUNAcknowledgement : System.Web.UI.Page
    {
        private ILookupSource ginLookup = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ginLookup = GINProcessBLL.StaticLookupSource;
            SearchConditionSelector1.LookupSource = ginLookup;
            if (!IsPostBack)
            {
                SearchConditionSelector1.DataFilter = GINProcessBLL.GetCatalogFilter("CompleteFilterSet");
            }
            SetCatalogData();
        }

        protected string GetStatusName(object status)
        {
            return ginLookup.GetLookup("Status")[int.Parse(status.ToString())];
        }

        protected string GetCommodityGradeName(object commodityGrade)
        {
            return ginLookup.GetLookup("CommodityGrade")[new Guid(commodityGrade.ToString())];
        }

        protected bool Navigable(object status, string purpose, object oBalanceWeight)
        {
            GINProcessStatusType ginpStatus = (GINProcessStatusType)(int.Parse((string)status));
            if (purpose == "Verify")
            {
                return (ginpStatus == GINProcessStatusType.New);
            }
            else if (purpose == "Load")
            {
                return ((ginpStatus == GINProcessStatusType.Ok_to_Load) && (decimal.Parse((string)oBalanceWeight) > 0M));
            }
            return false;
        }

        protected void btnOpen_Command(object sender, CommandEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xdsGINProcessSource.Data);
            XmlNode statusNode = document.DocumentElement.SelectSingleNode(string.Format("/Catalog/GINProcess[@Id=\"{0}\"]/@Status", e.CommandArgument));
            int status = int.Parse(((XmlAttribute)statusNode).Value);
            if (e.CommandName == "VerifyAgent")
            {
                PageDataTransfer confirmationTransfer = new PageDataTransfer(Request.ApplicationPath + "/VerifyGINAvailability.aspx");
                confirmationTransfer.RemoveAllData();
                GINProcessWrapper.RemoveGINProcessInformation();
                confirmationTransfer.TransferData.Add("GINProcessId", new Guid(e.CommandArgument.ToString()));
                confirmationTransfer.TransferData.Add("ReturnPage", HttpContext.Current.Request.Path);
                confirmationTransfer.Navigate();
            }
            else if (e.CommandName == "RegisterTruck")
            {
                PageDataTransfer confirmationTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/TruckRegistration.aspx");
                confirmationTransfer.RemoveAllData();
                GINProcessWrapper.RemoveGINProcessInformation();
                confirmationTransfer.TransferData["GINProcessId"] = new Guid(e.CommandArgument.ToString());
                confirmationTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
                confirmationTransfer.Navigate();
            }

        }

        private void SetCatalogData()
        {
            List<IDataIdentifier> ids = null;
            try
            {
                IDataFilter filter = SearchConditionSelector1.DataFilter;
                foreach (DataFilterParameter parameter in filter.Parameters)
                {
                    filter.SetCondition(SearchConditionSelector1[parameter.Name]);
                }
                ids = GINProcessBLL.SearchGINProcess(filter);
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
            xdsGINProcessSource.Data = ginProcessSet;
            gvPickupNotice.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetCatalogData();
        }
    }
}
