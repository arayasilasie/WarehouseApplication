using System;
using System.Reflection;
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
using AjaxControlToolkit;
using WarehouseApplication.DALManager;

namespace WarehouseApplication.UserControls
{
    public partial class GINDataEditor : System.Web.UI.UserControl
    {
        private ILookupSource lookup;
        private GINGridViewerDriver driver;
        private string alternateRowClass = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
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
            get
            {
                return ViewState[string.Format("{0}_DataSource", ID)];
            }
            set
            {
                ViewState[string.Format("{0}_DataSource", ID)] = value;
            }
        }
        public string CssClass
        {
            get { return RenderPanel.CssClass; }
            set { RenderPanel.CssClass = value; }
        }

        public string CaptionClass
        {
            get { return CaptionPanel.CssClass; }
            set { CaptionPanel.CssClass = value; }
        }

        public string MessageClass
        {
            get { return MessagePanel.CssClass; }
            set { MessagePanel.CssClass = value; }
        }

        public string CommandClass
        {
            get { return CommandPanel.CssClass; }
            set { CommandPanel.CssClass = value; }
        }

        public string AlternateRowClass
        {
            get { return alternateRowClass; }
            set { alternateRowClass = value; }
        }

        public bool IsNew
        {
            get { return (bool)ViewState[string.Format("{0}IsNew", ID)]; }
            set { ViewState[string.Format("{0}IsNew", ID)] = value; }
        }

        public event EventHandler Ok;
        public event EventHandler Cancel;

        protected void OnOk(EventArgs e)
        {

            if (Ok != null)
            {
                Ok(this, e);
            }
        }

        protected void OnCancel(EventArgs e)
        {
            if (Cancel != null)
            {
                Cancel(this, e);
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataSource != null)
            {
                foreach (GINColumnDescriptor column in Driver.Columns)
                {
                    if (column.IsViewable)
                    {
                        column.AttachedRenderer.DataSource = DataSource;
                    }
                }
                RenderTable.DataBind();
            }
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            foreach (GINColumnDescriptor column in Driver.Columns)
            {
                if (column.IsViewable)
                {
                    column.AttachedRenderer.DataSource = DataSource;
                }
            }
        }

        public void Setup()
        {
            string validationGroup = string.Empty;
            if (!driver.SuppressValidationSummary)
            {
                validationGroup = (driver.ValidationGroup == null || driver.ValidationGroup == string.Empty) ? driver.Title : driver.ValidationGroup;
            }
            ValidationMessage.ValidationGroup = validationGroup;
            btnOk.ValidationGroup = validationGroup;
            List<TableRow> rowsToRemove = new List<TableRow>();
            foreach (TableRow tableRow in RenderTable.Rows)
            {
                if ((tableRow.ID != "CaptionRow") &&
                    (tableRow.ID != "CommandRow") &&
                    (tableRow.ID != "MessageRow"))
                {
                    rowsToRemove.Add(tableRow);
                }
            }
            foreach (TableRow tableRow in rowsToRemove)
            {
                RenderTable.Rows.Remove(tableRow);
            }
            ExtenderPanel.Controls.Clear();
            //foreach (GINColumnDescriptor column in driver.Columns)
            var nonCascadingColumns = from column in driver.Columns
                                      where column.IsViewable && (from child in driver.Columns select child).All(child => !child.IsCascadedLookup || (child.IsCascadedLookup && (child.ParentLookup != column.Name)))
                                      select column;
            foreach (GINColumnDescriptor column in nonCascadingColumns)
            {
                if (column.IsCDDExtender)
                {
                    //string parentColumnName = (from pcr in column.ExtendedColumns where pcr.Role=="Parent" select pcr.Name).Single();
                    //GINColumnDescriptor parentColumn = driver.Columns.Find(parent=>parent.Name == parentColumnName);
                    //string childColumnName = (from pcr in column.ExtendedColumns where pcr.Role=="Child" select pcr.Name).Single();
                    //GINColumnDescriptor childColumn = driver.Columns.Find(child=>child.Name == childColumnName);
                    //CascadingDropDown cddExtender = new CascadingDropDown();
                    //cddExtender.ID = string.Format("cddExtender_{0}", column.Name);
                    //cddExtender.Category = parentColumn.Lookup;
                    //cddExtender.Enabled = true;
                    //cddExtender.LoadingText = "Please wait...";
                    //Control childContainingControl = FindCell(string.Format("ddl{0}", childColumnName));
                    //cddExtender.ParentControlID = string.Format("ddl{0}", parentColumnName);
                    //cddExtender.TargetControlID = string.Format("ddl{0}", childColumnName);
                    //cddExtender.ServicePath = column.ServicePath;
                    //cddExtender.ServiceMethod = column.ServiceMethod;
                    //childContainingControl.Controls.Add(cddExtender);
                    GINColumnDescriptorReference parentReference = (from pcr in column.ExtendedColumns where pcr.Role == "Parent" select pcr).Single();
                    string parentColumnName = parentReference.Name;
                    GINColumnDescriptor parentColumn = driver.Columns.Find(parent => parent.Name == parentColumnName);
                    string childColumnName = (from pcr in column.ExtendedColumns where pcr.Role == "Child" select pcr.Name).Single();
                    GINColumnDescriptor childColumn = driver.Columns.Find(child => child.Name == childColumnName);
                    CascadingDropDown cddExtender = new CascadingDropDown();
                    cddExtender.ID = string.Format("cddExtender_{0}", childColumnName);
                    if ((parentColumn == null) && (parentReference.UseContextKey))
                    {
                        cddExtender.ContextKey = lookup.GetLookup(parentReference.ContextKeyLookup)[parentReference.ContextKeyName];
                    }
                    cddExtender.Category = (parentColumn == null)?"undefined":parentColumn.Lookup;
                    cddExtender.Enabled = true;
                    cddExtender.LoadingText = "Please wait...";
                    cddExtender.PromptText = string.Format("[select {0}]", childColumnName);
                    cddExtender.PromptValue = DALManager.NullFinder.GetNullValue(childColumn.DataType).ToString();
                    Control childContainingControl = FindCell(string.Format("ddl{0}", childColumnName));
                    if (parentColumnName != string.Empty)
                    {
                        cddExtender.ParentControlID = string.Format("ddl{0}", parentColumnName);
                    }
                    cddExtender.TargetControlID = string.Format("ddl{0}", childColumnName);
                    cddExtender.ServicePath = column.ServicePath;
                    cddExtender.ServiceMethod = column.ServiceMethod;
                    cddExtender.DataBinding += new EventHandler(cddExtender_DataBinding);
                    childContainingControl.Controls.Add(cddExtender);
                    continue;
                }
                if (column.IsTextChangeExtender)
                {
                    string publisherColumnName = (from pcr in column.ExtendedColumns where pcr.Role=="Publisher" select pcr.Name).Single();
                    TextBox txtPublisher = (TextBox)FindColumnControl(string.Format("txt{0}", publisherColumnName));
                    txtPublisher.AutoPostBack = true;
                    txtPublisher.TextChanged += new EventHandler(txtPublisher_TextChanged);
                    UpdatePanelControlTrigger trigger = new AsyncPostBackTrigger();
                    trigger.ControlID = txtPublisher.ID;
                    TextChangeUpdatePanel.Triggers.Add(trigger);
                    continue;
                }
                TableRow row = new TableRow();
                int index = RenderTable.Rows.Count - 1;
                RenderTable.Rows.AddAt(index, row);
                TableCell cell = new TableCell();
                if (index % 2 == 0)
                {
                    cell.CssClass = alternateRowClass;
                }
                cell.Width = new Unit(35, UnitType.Percentage);
                row.Cells.Add(cell);
                Literal caption = new Literal();
                caption.Text = column.Header;
                cell.Controls.Add(caption);
                cell = new TableCell();
                if (index % 2 == 0)
                {
                    cell.CssClass = alternateRowClass;
                }
                cell.Width = new Unit(65, UnitType.Percentage);
                row.Cells.Add(cell);
                if (column.IsCascadedLookup)
                {
                    GINColumnDescriptor parentColumn = driver.Columns.Find(parent => parent.Name == column.ParentLookup);
                    IGINColumnRenderer renderer = parentColumn.AttachedRenderer;
                    if (parentColumn.IsLookup)
                    {
                        renderer.Lookup = Lookup;
                    }
                    renderer.DataSource = DataSource;
                    WebControl parentControl = renderer.RenderInEditor();
                    parentControl.ID = string.Format("cddParent_{0}", parentColumn.Name);
                    parentControl.Width = new Unit(50, UnitType.Percentage);
                    renderer = column.AttachedRenderer;
                    renderer.DataSource = DataSource;
                    WebControl childControl = renderer.RenderInEditor();
                    childControl.ID = string.Format("cddChild_{0}", column.Name);
                    childControl.Width = new Unit(50, UnitType.Percentage);
                    Panel uPan = new Panel();
                    uPan.Controls.Add(parentControl);
                    uPan.Controls.Add(childControl);
                    CascadingDropDown cddExtender = new CascadingDropDown();
                    cddExtender.ID = string.Format("cddExtender_{0}", column.Name);
                    cddExtender.Category = parentColumn.Lookup;
                    cddExtender.LoadingText = "Please wait...";
                    cddExtender.ParentControlID = parentControl.ID;
                    cddExtender.TargetControlID = childControl.ID;
                    cddExtender.ServicePath = column.ServicePath;
                    cddExtender.ServiceMethod = column.ServiceMethod;
                    uPan.Controls.Add(cddExtender);
                    cell.Controls.Add(uPan);
                    row.Cells.Add(cell);
                }
                else
                {
                    IGINColumnRenderer renderer = column.AttachedRenderer;
                    if (column.IsLookup)
                    {
                        renderer.Lookup = Lookup;
                    }
                    renderer.DataSource = DataSource;
                    WebControl ctrl = renderer.RenderInEditor();
                    List<BaseValidator> validators = SetValidationGroup(ctrl);
                    cell.Controls.Add(ctrl);
                    cell = new TableCell();
                    foreach (BaseValidator validator in validators)
                    {
                        cell.Controls.Add(validator);
                        //ctrl.Controls.Remove(validator);
                    }
                    row.Cells.Add(cell);
                }
            }
        }

        void cddExtender_DataBinding(object sender, EventArgs e)
        {
            CascadingDropDown cddExtender = (CascadingDropDown)sender;
            string childColumnName = cddExtender.TargetControlID.Substring("ddl".Length);
            string fieldName = driver.Columns.Find(c => c.Name == childColumnName).Text;
            object dataValue = DataSource.GetType().GetProperty(fieldName).GetValue(DataSource, null);
            cddExtender.SelectedValue = dataValue.ToString();
        }

        void txtPublisher_TextChanged(object sender, EventArgs e)
        {
            var extender = from column in driver.Columns
                           where column.IsTextChangeExtender &&
                               (from publisher in column.ExtendedColumns where (publisher.Role == "Publisher") && (string.Format("txt{0}", publisher.Name) == ((TextBox)sender).ID) select publisher).Any()
                           select column;
            var textChangeSubscribers = extender.SelectMany(column => column.ExtendedColumns.FindAll(subscriber => subscriber.Role == "Subscriber"));
            SubscriberResponse subscriberResponse;
            //WarehouseApplication.TruckTypeResponse, WarehouseApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            string[] typeInfoParts = extender.Single().SubscriberResponse.Split(new char[] { ',' });
            if (typeInfoParts.Length < 5)
            {
                throw new InvalidOperationException(string.Format("Invalid Subscriber Response Configuration Setting"));
            }
            string assemblyName = string.Join(", ", typeInfoParts, 1, 4);
            try
            {
                Assembly responseAssembly = Assembly.Load(new AssemblyName(assemblyName));
                subscriberResponse = (SubscriberResponse)responseAssembly.CreateInstance(typeInfoParts[0]);
                subscriberResponse.Publisher = (TextBox)sender;
                subscriberResponse.SubscriberData = DataSource;
                subscriberResponse.Subscribers = (from subscriberRef in textChangeSubscribers
                                                  select FindColumnControl(string.Format("cddExtender_{0}", subscriberRef.Name))).ToArray();
                subscriberResponse.Respond();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Invalid Subscriber response configuration"), ex);
            }
            //DataBind();
        }

        protected void lblCaption_DataValidating(object sender, EventArgs e)
        {
            if (driver != null)
            {
                lblCaption.Text = driver.Title;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel(e);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            OnOk(e);
        }

        private List<BaseValidator> SetValidationGroup(Control ctrl)
        {
            List<BaseValidator> validators = new List<BaseValidator>();
            if (ctrl is BaseValidator)
            {
                ((BaseValidator)ctrl).ValidationGroup = (driver.ValidationGroup == null || driver.ValidationGroup == string.Empty) ? driver.Title : driver.ValidationGroup;;
                validators.Add((BaseValidator)ctrl);
            }
            else if (ctrl.Controls.Count > 0)
            {
                foreach (Control childControl in ctrl.Controls)
                {
                    List<BaseValidator> childValidators = SetValidationGroup(childControl);
                    validators.AddRange(childValidators);
                }
            }
            return validators;
        }

        private Control FindCell(string Id)
        {
            foreach (TableRow row in RenderTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        if (control.ID == Id)
                        {
                            return cell;
                        }
                    }
                }
            }
            return null;
        }

        private Control FindColumnControl(string Id)
        {
            foreach (TableRow row in RenderTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    Control control = cell.FindControl(Id);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return null;
        }
    }
}