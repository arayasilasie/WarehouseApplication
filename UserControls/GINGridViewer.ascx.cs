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
using WarehouseApplication.DALManager;

namespace WarehouseApplication.UserControls
{
    public partial class GINGridViewer : System.Web.UI.UserControl
    {
        private ILookupSource lookup;
        private GINGridViewerDriver driver;
        private object dataSource;
        GridView gv = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (gv == null)
            {
                CreateGridView();
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            GridView.DataSource = dataSource;
            GridView.DataBind();
        }
        public ILookupSource Lookup
        {
            get { return lookup; }
            set { lookup = value; }
        }

        public GINGridViewerDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }

        public object DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        public GridView GridView
        {
            get
            {
                if (gv == null)
                {
                    CreateGridView();
                }
                return gv;
            }
        }

        private void CreateGridView()
        {
            gv = new GridView();
            //gv.Caption = driver.Title;
            gv.AutoGenerateColumns = false;
            //gv.DataKeyNames = new string[] { driver.Key };
            gv.PageSize = 10;
            gv.Width = new Unit(100, UnitType.Percentage);
            gv.ShowHeader = true;
            gv.CssClass = "Grid";
            gv.HeaderStyle.CssClass = "GridHeader";
            gv.RowStyle.CssClass = "GridRow";
            gv.AlternatingRowStyle.CssClass = "GridAlternate";
            gv.PagerStyle.CssClass = "GridPager";
            foreach (GINColumnDescriptor ginColumn in driver.Columns)
            {
                if (!ginColumn.IsListable) continue;
                //IGINColumnRenderer rendrer = driver.GetRenderer(ginColumn);
                IGINColumnRenderer rendrer = ginColumn.AttachedRenderer;
                if (ginColumn.IsLookup)
                {
                    rendrer.Lookup = Lookup;
                }
                DataControlField field = rendrer.RenderInTable();
                field.ItemStyle.CssClass = ginColumn.CssCls;
                GridView.Columns.Add(field);
            }
            this.Controls.Add(GridView);
        }
    }
}
