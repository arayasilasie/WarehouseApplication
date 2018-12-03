using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;


namespace WarehouseApplication
{
    public class BasePage : Page
    {
        private string connectString = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ToString();
        public string ConnectionString
        { //set{connectString=null;} 
            get { return connectString; }
        }
        public bool SessionKeyExists(string keyName)
        {
            foreach (string key in Session.Keys)
            {
                if (key == keyName)
                    return true;
            }
            return false;
        }

        public void BindCombo(DropDownList ddl, LookupTypeEnum luType, string defaultText)
        {
            BindCombo(ddl, luType, defaultText, "Description", "ID");
        }

        public void BindCombo(DropDownList ddl, LookupTypeEnum luType, Predicate<LookupValue> criteria, string defaultText)
        {
            ddl.Items.Clear();

            ddl.Items.Add(new ListItem(defaultText, string.Empty));
            ddl.AppendDataBoundItems = true;

            List<LookupValue> ds = SimpleLookup.Lookup(luType).GetList(criteria);
            ds.Sort(CompareByString);
            ddl.DataSource = ds;
            ddl.DataTextField = "Description";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }

        public static int CompareByString(LookupValue x, LookupValue y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return x.Description.CompareTo(y.Description);
            }
        }

        public void BindCombo(DropDownList ddl, LookupTypeEnum luType, string defaultText, string dataTextField, string dataValueField)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(defaultText, string.Empty));
            ddl.AppendDataBoundItems = true;
            List<LookupValue> ds = SimpleLookup.Lookup(luType).GetList();
            ds.Sort(CompareByString);
            ddl.DataSource = ds;
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataBind();
        }

        public int CurrentEthiopianYear
        {
            get
            {
                if (DateTime.Today.Month < 10)
                    return DateTime.Today.Year - 8;
                return DateTime.Today.Year - 7;
            }
        }

        public DataTable GetCommodityTypesByCommodityId(Guid CommodityId, string CommandType)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "Get_CommodityTypeByCommodityId", CommodityId, CommandType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }

    }
}