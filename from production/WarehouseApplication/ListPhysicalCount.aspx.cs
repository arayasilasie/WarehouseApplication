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

namespace WarehouseApplication
{
    public partial class ListPhysicalCount : System.Web.UI.Page
    {
        private ILookupSource inventoryServiceLookup = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            inventoryServiceLookup = InventoryServices.StaticLookupSource;
            SearchConditionSelector1.LookupSource = inventoryServiceLookup;
            if (!IsPostBack)
            {
                SearchConditionSelector1.DataFilter = InventoryServices.GetPhysicalCountCatalogFilter("CompleteFilterSet");
            }
            SetCatalogData();
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            PageDataTransfer physicalCountTransfer = new PageDataTransfer(Request.ApplicationPath + "/TakePhysicalCount.aspx");
            physicalCountTransfer.RemoveAllData();
            physicalCountTransfer.TransferData["PhysicalCountId"] = new Guid((string)gvPhysicalCount.DataKeys[gvPhysicalCount.SelectedIndex].Value);
            physicalCountTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
            physicalCountTransfer.Navigate();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvPhysicalCount.Visible = true;
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
                                string.Empty, inventoryServiceLookup.GetLookup("CurrentWarehouse")["Id"]));
                    }
                    else
                    {
                        filter.SetCondition(SearchConditionSelector1[parameter.Name]);
                    }
                }
                ids = InventoryServices.SearchPhysicalCount(filter);
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
            string PhysicalCountSet = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <Catalog>" + buffer + "</Catalog>";
            xdsPhysicalCountSource.Data = PhysicalCountSet;
            xdsPhysicalCountSource.DataBind();
            gvPhysicalCount.DataBind();
        }

        protected void gvPhysicalCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PhysicalCountInfo pc = new PhysicalCountInfo()
            {
                Id = Guid.NewGuid(),
                WarehouseId = new Guid(SystemLookup.LookupSource.GetLookup("CurrentWarehouse")["Id"]),
                IsBeginingCount = false,
                PhysicalCountDate = DateTime.Now
            };
            InventoryServices.GetInventoryService().CreatePhysicalCount(pc);
            PageDataTransfer physicalCountTransfer = new PageDataTransfer(Request.ApplicationPath + "/TakePhysicalCount.aspx");
            physicalCountTransfer.RemoveAllData();
            physicalCountTransfer.TransferData["PhysicalCountId"] = pc.Id;
            physicalCountTransfer.TransferData["ReturnPage"] = HttpContext.Current.Request.Path;
            physicalCountTransfer.Navigate();
        }
    }
}
