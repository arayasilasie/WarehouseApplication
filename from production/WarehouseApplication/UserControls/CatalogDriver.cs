using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using WarehouseApplication.BLL;
using WarehouseApplication.DALManager;
using WarehouseApplication.SECManager;
using AjaxControlToolkit;
using ECXControlsCollection;

namespace WarehouseApplication.UserControls
{
    /// <summary>
    /// Represents a renderer of a field to a grid viewer or an editor component
    /// </summary>
    /// 
    public interface IGINColumnRenderer
    {
        //Telling the grid viewer how to render a field
        DataControlField RenderInTable();
        //Telling the editor how to render a field
        WebControl RenderInEditor();
        //If the field is a lookup field the editor sets this property for the 
        //renderer to fetch text, value pairs to populate the associated dropdown
        ILookupSource Lookup { get; set; }
        //The dataSource the property of which the renderer is desired to bind with
        object DataSource { get; set; }
    }

    public interface IGINColumnRendererFactory
    {
        //represents a factory instantiating the appropriate renderer to attach to the field
        IGINColumnRenderer CreateRenderer(GINColumnDescriptor column);
    }

    //an implementation of the renderer factory
    public class GINColumnRendererDefaultFactory : IGINColumnRendererFactory
    {
        #region IGINColumnRendererFactory Members

        //queries the column descriptor and statically constructs the desired
        //renderer. A better implementation would construct the renderer dynamically,
        //via reflection to make the mechanizm highly extendable, as then you wouldn't
        //be constrained to use only existing renderers. 
        public IGINColumnRenderer CreateRenderer(GINColumnDescriptor column)
        {
            switch (column.Renderer)
            {
                case "LinkGINColumnRenderer":
                    return new LinkGINColumnRenderer(column);
                case "CheckBoxGINColumnRenderer":
                    return new CheckBoxGINColumnRenderer(column);
                case "DateTimeGINColumnRenderer":
                    return new DateTimeGINColumnRenderer(column);
                case "DateAndTimeGINColumnRenderer":
                    return new DateAndTimeGINColumnRenderer(column);
                default:
                    return new GINColumnRenderer(column);
            }

        }

        #endregion
    }

    //Represents the configurator of the mechanism. Implemented as a singleton it reads the 
    //associated configuration file and constructs the configuration heirarchy.
    //While the application runs it services view configuration requests from pages
    public class GINViewConfigurationReader
    {
        private static GINViewConfigurationReader singleton = null;
        public static GINGridViewerDriver GetViewConfiguration(string pageName, string viewName)
        {
            //if (singleton == null)
            //{
            singleton = new GINViewConfigurationReader();
            //}
            return singleton.FindViewConfiguration(pageName, viewName);
        }
        public static GINGridViewerDriver CopyViewConfiguration(string pageName, string viewName)
        {
            return new GINViewConfigurationReader().FindViewConfiguration(pageName, viewName);
        }
        private GINApplicationView applicationView = null;

        //when the singleton instance is loaded it reads the configuration heirarchy to memory
        private GINViewConfigurationReader()
        {
            //The configuration file shall be specified in web.config
            string configurationFile = HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ViewConfigurationFile"];
            Stream stream = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GINApplicationView));
                stream = File.OpenRead(configurationFile);
                applicationView = (GINApplicationView)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
            if (applicationView != null)
            {
                foreach (GINPage ginPage in applicationView.Pages)
                {
                    foreach (GINGridViewerDriver component in ginPage.ViewComponents)
                    {
                        component.RendererGenerator = new GINColumnRendererDefaultFactory();
                        foreach (GINColumnDescriptor column in component.Columns)
                        {
                            column.AttachedRenderer = component.RendererGenerator.CreateRenderer(column);
                        }
                    }
                }
            }
        }

        //helper method looking for the requested view
        private GINGridViewerDriver FindViewConfiguration(string pageName, string viewName)
        {
            var requestedPage = from page in applicationView.Pages
                                where (page.Name == pageName)
                                select page;
            if (requestedPage.Count() > 0)
            {
                GINPage ginPage = requestedPage.ElementAt(0);
                var requestedDriver = from driver in ginPage.ViewComponents
                                      where (driver.Name == viewName)
                                      select driver;
                if (requestedDriver.Count() > 0)
                {
                    GINGridViewerDriver driver = requestedDriver.ElementAt(0);
                    AppllySecurityRules(pageName, driver);
                    return driver;
                }
            }
            return null;
        }

        //helper method for applying the security rules of the application
        private void AppllySecurityRules(string pageName, GINGridViewerDriver driver)
        {
            XmlSerializer s = new XmlSerializer(typeof(SecurityResourceConfigurationInfo));
            SecurityResourceConfigurationInfo src = null;
            using (Stream stream = File.OpenRead(HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["SecurityConfigurationFile"]))
            {
                try
                {
                    src = (SecurityResourceConfigurationInfo)s.Deserialize(stream);
                }
                catch (Exception)
                {
                }
            }
            if (src == null)
            {
                return;
            }
            List<string> allRoleNames = new List<string>();
            foreach (SecurityRoleInfo role in src.SecurityRoles)
            {
                allRoleNames.Add(role.Name);
            }
            List<string> userRoleNames = UserBLL.HasRoles(
                UserBLL.GetCurrentUser(),
                allRoleNames.ToArray());
            foreach (SecurityRoleInfo role in
                (from role in src.SecurityRoles where (from userRole in userRoleNames where role.Name == userRole select userRole).Any() select role))
            {
                SecuredResourceContainerInfo resourceContainer = src.SecuredResourceContainers.Find(cont => cont.Name == pageName);
                if (resourceContainer == null)
                {
                    break;
                }
                foreach (GINColumnDescriptor column in driver.Columns)
                {
                    SecuredResourceInfo securedResource = resourceContainer.SecuredResources.Find(sr => (sr.Scope == driver.Name) && (sr.Name == column.Name));
                    if (securedResource != null)
                    {
                        var minLevel = securedResource.ConfigurationOptions.Select(option => option.Level).Min();
                        var restrictiveOptions = from option in securedResource.ConfigurationOptions
                                                 where option.Level == minLevel
                                                 select option;

                        List<ConfigurationOptionInfo> applicableOptions = new List<ConfigurationOptionInfo>(restrictiveOptions);
                        var grantedOptions = role.GrantedResourceContainers.Where(grc => grc.Name == resourceContainer.Name)
                            .SelectMany(grc => grc.GrantedResources.Where(gr => (gr.Scope == securedResource.Scope) && (gr.Name == securedResource.Name))
                                .Select(gr => gr.Option)
                                    .SelectMany(grantedOption => securedResource.ConfigurationOptions.Where(option => option.OptionId == grantedOption)
                                         .Select(option => option)));
                        foreach (ConfigurationOptionInfo option in grantedOptions)
                        {
                            if (applicableOptions.RemoveAll(ao => (ao.Property == option.Property) && (ao.Level < option.Level)) > 0)
                            {
                                applicableOptions.Add(option);
                            }
                        }
                        foreach (ConfigurationOptionInfo applicableOption in applicableOptions)
                        {
                            PropertyInfo optionProperty = column.GetType().GetProperty(applicableOption.Property);
                            optionProperty.SetValue(column, Convert.ChangeType(applicableOption.Value, optionProperty.PropertyType), null);
                        }
                    }
                }
            }
        }
    }

    //The root of the configuration heirarchy. An application would require a single root
    [XmlRoot("ApplicationView")]
    public class GINApplicationView
    {
        private List<GINPage> pages;
        public GINApplicationView()
        {
            pages = new List<GINPage>();
        }

        [XmlArray("Pages")]
        [XmlArrayItem("Page")]
        public List<GINPage> Pages
        {
            get { return pages; }
        }
    }

    //Corresponding to each configrued page of the application
    public class GINPage
    {
        private List<GINGridViewerDriver> viewComponents;
        private string name;

        public GINPage()
        {
            this.viewComponents = new List<GINGridViewerDriver>();
        }

        public GINPage(string name)
            : this()
        {
            this.name = name;
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlArray("ViewComponents")]
        [XmlArrayItem("ViewComponent")]
        public List<GINGridViewerDriver> ViewComponents
        {
            get { return viewComponents; }
        }

    }

    //a view represents a set of column descriptors specifying the fields
    //to be rendered in the view 
    public class GINGridViewerDriver
    {
        private string name;
        private string title;
        private string key;
        private string validationGroup;
        private bool suppressValidationSummary=false;
        private List<GINColumnDescriptor> columns = new List<GINColumnDescriptor>();
        private IGINColumnRendererFactory rendererGenerator = null;

        public GINGridViewerDriver()
        {
        }

        public GINGridViewerDriver(string name, string title, string key, IGINColumnRendererFactory rendererGenerator)
        {
            this.name = name;
            this.title = title;
            this.key = key;
            this.rendererGenerator = rendererGenerator;
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlAttribute]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [XmlAttribute]
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        [XmlAttribute]
        public string ValidationGroup
        {
            get { return validationGroup; }
            set { validationGroup = value; }
        }

        [XmlAttribute]
        public bool SuppressValidationSummary
        {
            get { return suppressValidationSummary; }
            set { suppressValidationSummary = value; }
        }

        [XmlArray("Columns")]
        [XmlArrayItem("Column")]
        public List<GINColumnDescriptor> Columns
        {
            get { return columns; }
        }

        [XmlIgnore]
        public IGINColumnRendererFactory RendererGenerator
        {
            get { return rendererGenerator; }
            set { rendererGenerator = value; }
        }

        public IGINColumnRenderer GetRenderer(GINColumnDescriptor column)
        {
            return this.rendererGenerator.CreateRenderer(column);
        }

    }

    public class GINColumnDescriptorReference
    {
        public GINColumnDescriptorReference()
        {
            Role = string.Empty;
        }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Role { get; set; }
        [XmlAttribute]
        public bool UseContextKey { get; set; }
        [XmlAttribute]
        public string ContextKeyLookup { get; set; }
        [XmlAttribute]
        public string ContextKeyName { get; set; }


    }
    //a description of a field with input rules and attached renderer
    public class GINColumnDescriptor
    {
        private string name;
        private int row;
        private string text;
        private string lookup;
        private bool isCommand = false;
        private string key = string.Empty;
        private bool isLookup = false;
        private bool isCDDExtender = false;
        private bool isCDDExtended = false;
        private bool isTextChangeExtender = false;
        private string cssCls = string.Empty;
        private bool isListable = true;
        private bool isViewable = true;
        private bool isEditable = true;
        private bool isStatic = false;
        private string renderer = string.Empty;
        private string header;
        private string dataType = "System.String";
        private bool isLongText = false;
        private bool isCascadedLookup = false;
        private string parentLookup = string.Empty;
        private string servicePath = string.Empty;
        private string serviceMethod = string.Empty;
        private string subscriberResponse = string.Empty;
        private string format = string.Empty;

        private IGINColumnRenderer attachedRenderer = null;
        private List<ValidationRule> rules = new List<ValidationRule>();
        private List<GINColumnDescriptorReference> extendedColumns = new List<GINColumnDescriptorReference>();

        public GINColumnDescriptor(string name, string text, string header)
        {
            this.name = name;
            this.text = text;
            this.header = header;
        }

        public GINColumnDescriptor() { }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlAttribute]
        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        [XmlAttribute]
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
            }
        }

        [XmlAttribute]
        public bool IsListable
        {
            get { return isListable; }
            set
            {
                isListable = value;
            }
        }

        [XmlAttribute]
        public bool IsViewable
        {
            get { return isViewable; }
            set
            {
                isViewable = value;
            }
        }

        [XmlAttribute]
        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                isEditable = value;
            }
        }

        [XmlAttribute("Class")]
        public string CssCls
        {
            get { return cssCls; }
            set
            {
                cssCls = value;
            }
        }

        [XmlAttribute]
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
            }
        }

        [XmlAttribute]
        public string Lookup
        {
            get { return lookup; }
            set { lookup = value; }
        }

        [XmlAttribute]
        public bool IsLookup
        {
            get { return isLookup; }
            set { isLookup = value; }
        }

        [XmlAttribute]
        public bool IsCDDExtender
        {
            get { return isCDDExtender; }
            set { isCDDExtender = value; }
        }

        [XmlAttribute]
        public bool IsTextChangeExtender
        {
            get { return isTextChangeExtender; }
            set { isTextChangeExtender = value; }
        }

        [XmlAttribute]
        public string SubscriberResponse
        {
            get { return subscriberResponse; }
            set { subscriberResponse = value; }
        }

        [XmlAttribute]
        public bool IsCDDExtended
        {
            get { return isCDDExtended; }
            set { isCDDExtended = value; }
        }

        [XmlAttribute]
        public bool IsStatic
        {
            get { return isStatic; }
            set { isStatic = value; }
        }

        [XmlAttribute]
        public bool IsCommand
        {
            get { return isCommand; }
            set { isCommand = value; }
        }

        [XmlAttribute]
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        [XmlAttribute]
        public bool IsLongText
        {
            get { return isLongText; }
            set
            {
                isLongText = value;
            }
        }

        [XmlAttribute]
        public string Format
        {
            get { return format; }
            set
            {
                format = value;
            }
        }

        [XmlArray("ValidationRules")]
        [XmlArrayItem("ValidationRule")]
        public List<ValidationRule> Rules
        {
            get { return rules; }
        }

        [XmlArray("ExtendedColumns")]
        [XmlArrayItem("ExtendedColumn")]
        public List<GINColumnDescriptorReference> ExtendedColumns
        {
            get { return extendedColumns; }
        }

        [XmlAttribute]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        [XmlAttribute]
        public string Renderer
        {
            get
            {
                return renderer;
            }
            set
            {
                renderer = value;
            }
        }

        [XmlAttribute]
        public bool IsCascadedLookup
        {
            get { return isCascadedLookup; }
            set { isCascadedLookup = value; }
        }

        [XmlAttribute]
        public string ParentLookup
        {
            get { return parentLookup; }
            set { parentLookup = value; }
        }

        [XmlAttribute]
        public string ServicePath
        {
            get { return servicePath; }
            set { servicePath = value; }
        }

        [XmlAttribute]
        public string ServiceMethod
        {
            get { return serviceMethod; }
            set { serviceMethod = value; }
        }

        [XmlIgnore]
        public IGINColumnRenderer AttachedRenderer
        {
            get { return attachedRenderer; }
            set { attachedRenderer = value; }
        }
    }

    //The generic renderer of a field. Depending on the descriptin of the field
    //it can render it as a static or dynamic text, a textbox or a dropdown list 
    public class GINColumnRenderer : IGINColumnRenderer, ITemplate
    {
        protected GINColumnDescriptor column;
        protected ILookupSource lookup;
        protected object dataSource;

        public GINColumnRenderer(GINColumnDescriptor column)
        {
            this.column = column;
        }

        public GINColumnRenderer()
            : this(null)
        {
        }

        public ILookupSource Lookup
        {
            get { return lookup; }
            set { lookup = value; }
        }

        public object DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        #region IGINColumnRenderer Members

        public virtual DataControlField RenderInTable()
        {
            if (column.IsLookup || column.IsCommand)
            {
                TemplateField field = new TemplateField();
                if (column.IsLookup)
                {
                    field.HeaderText = column.Header;
                }
                field.ItemTemplate = this;
                return field;
            }
            else
            {
                BoundField field = new BoundField();
                field.HeaderText = column.Header;
                field.DataField = column.Text;
                return field;
            }
        }

        #endregion

        #region Rendering a field to an editor
        public virtual WebControl RenderInEditor()
        {
            if (column.IsCDDExtender || column.IsTextChangeExtender)
            {
                return null;
            }
            if (!column.IsEditable || column.IsStatic)
            {
                Label lbl = new Label();
                lbl.Width = new Unit(100, UnitType.Percentage);
                if (column.IsStatic)
                {
                    lbl.Text = column.Text;
                }
                else
                {
                    lbl.DataBinding += new EventHandler(lbl_DataBinding);
                }
                return lbl;
            }
            if (column.IsLookup)
            {
                DropDownList ddl = new DropDownList();
                ddl.Width = new Unit(100, UnitType.Percentage);
                ddl.DataBinding += new EventHandler(ddl_DataBinding);
                ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                IDictionary<object, string> dictionary = lookup.GetLookup(column.Lookup);
                foreach (object key in dictionary.Keys)
                {
                    ListItem item = new ListItem(dictionary[key], key.ToString());
                    ddl.Items.Add(item);
                }
                var validationRule = from rule in column.Rules
                                     where (rule.ValidationType == "Required") && ((RequiredValidationRule)rule).IsRequired
                                     select rule;
                if (validationRule.Count() == 0)
                {
                    ddl.Items.Add(new ListItem(string.Empty, NullFinder.GetNullValue(column.DataType).ToString()));
                }
                ddl.ID = string.Format("ddl{0}", column.Name);
                return ddl;
            }
            if (column.IsCascadedLookup)// || column.IsCDDExtended)
            {
                DropDownList ddl = new DropDownList();
                ddl.ID = string.Format("ddl{0}", column.Name);
                ddl.Width = new Unit(100, UnitType.Percentage);
                ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                return ddl;
            }
            else
            {
                Panel ph = new Panel();
                TextBox txt = new TextBox();
                txt.Width = new Unit(100, UnitType.Percentage);
                txt.DataBinding += new EventHandler(txt_DataBinding);
                txt.TextChanged += new EventHandler(txt_TextChanged);
                txt.ID = string.Format("txt{0}", column.Name);
                if (column.IsLongText)
                {
                    txt.TextMode = TextBoxMode.MultiLine;
                }

                ph.Controls.Add(txt);
                //for each rule specified in the column descriptor
                //the renderer attaches the appropriate validator control
                foreach (ValidationRule rule in column.Rules)
                {
                    BaseValidator validationControl = null;
                    switch (rule.ValidationType)
                    {
                        case "Required":
                            if (((RequiredValidationRule)rule).IsRequired)
                            {
                                validationControl = new RequiredFieldValidator();
                            }
                            break;
                        case "Pattern":
                            RegularExpressionValidator validator = new RegularExpressionValidator();
                            validator.ValidationExpression = ((PatternValidationRule)rule).Pattern;
                            validationControl = validator;
                            break;
                        case "Range":
                            RangeValidator rangeValidator = new RangeValidator();
                            RangeValidationRule rangeRule = (RangeValidationRule)rule;
                            rangeValidator.MinimumValue = rangeRule.LeftBoundary;
                            rangeValidator.MaximumValue = rangeRule.RightBoundary;
                            validationControl = rangeValidator;
                            break;
                        case "Compare":
                            CompareValidator compareValidator = new CompareValidator();
                            CompareValidationRule compareRule = (CompareValidationRule)rule;
                            compareValidator.Operator = compareRule.CompareOperator;
                            compareValidator.Type = compareRule.RuleDataType;
                            compareValidator.ValueToCompare = compareRule.ValueToCompare;
                            validationControl = compareValidator;
                            break;
                    }
                    if (validationControl != null)
                    {
                        validationControl.ValidationGroup = "GINDataEditor";
                        validationControl.ControlToValidate = txt.ID;
                        validationControl.ErrorMessage = rule.ErrorMessage;
                        validationControl.Text = "*";
                        ph.Controls.Add(validationControl);
                    }
                }
                return ph;
            }
        }

        //when the value of the field is changed (by the user), and is valid
        //the handler updates the associated property of the datasource.
        protected void txt_TextChanged(object sender, EventArgs e)
        {
            PropertyInfo property = dataSource.GetType().GetProperty(column.Text);
            TextBox txt = (TextBox)sender;
            try
            {
                object oValue;
                if (txt.Text.Trim() == string.Empty)
                {
                    oValue = NullFinder.GetNullValue(column.DataType);
                }
                else
                {
                    oValue = NullFinder.Parse(txt.Text, property.PropertyType);
                }
                property.SetValue(dataSource, oValue, null);
            }
            catch (Exception)
            {
                txt_DataBinding(txt, null);
            }
        }

        //updating the datasource in accordance to the users selection from the 
        //dropdown list
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
            {
                PropertyInfo property = dataSource.GetType().GetProperty(column.Text);
                string selectedValue = ((DropDownList)sender).SelectedValue;
                object oValue = NullFinder.Parse(selectedValue, property.PropertyType);
                property.SetValue(dataSource, oValue, null);
            }
        }

        //binding a readonly field to the associated property of the datasource
        protected void lbl_DataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (column.IsStatic)
            {
                lbl.Text = column.Text;
            }
            else
            {
                object oValue = dataSource.GetType().GetProperty(column.Text).GetValue(dataSource, null);
                if (NullFinder.IsNull(oValue, column.DataType) || (oValue == null))
                {
                    lbl.Text = string.Empty;
                }
                else
                {
                    if (column.IsLookup)
                    {
                        lbl.Text = lookup.GetLookup(column.Lookup)[oValue];
                    }
                    else
                    {
                        if ((column.Format != null) && (column.Format != string.Empty))
                        {
                            lbl.Text = string.Format("{0:" + column.Format + "}", oValue);
                        }
                        else
                        {
                            lbl.Text = oValue.ToString();
                        }
                    }
                }
            }
        }

        //binding an editable field to the associated property of the datasource
        protected void txt_DataBinding(object sender, EventArgs e)
        {
            object oValue = dataSource.GetType().GetProperty(column.Text).GetValue(dataSource, null);
            TextBox txt = (TextBox)sender;
            if (NullFinder.IsNull(oValue, column.DataType) || (oValue == null))
            {
                txt.Text = string.Empty;
            }
            else
            {
                if ((column.Format != null) && (column.Format != string.Empty))
                {
                    txt.Text = string.Format("{0:" + column.Format + "}", oValue);
                }
                else
                {
                    txt.Text = oValue.ToString();
                }
            }
        }

        //mapping the associated property of the datasource to its text in the dropdown list
        protected void ddl_DataBinding(object sender, EventArgs e)
        {
            object dataValue = dataSource.GetType().GetProperty(column.Text).GetValue(dataSource, null);
            if (!NullFinder.IsNull(dataValue, column.DataType))
            {
                try
                {
                    ((DropDownList)sender).SelectedValue = dataValue.ToString();
                }
                catch
                {
                }
            }
        }

        #endregion

        #region Rendering a field to a grid view

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            PlaceHolder ph = new PlaceHolder();
            ph.DataBinding += new EventHandler(Bind);
            foreach (WebControl ctrl in GetTemplatedControls())
            {
                ph.Controls.Add(ctrl);
            }
            container.Controls.Add(ph);
        }

        #endregion

        //declared as virtual so that sub classes would be able to hookup
        //with the mechanism and make custom binding a field in a grid view.
        //The method works in tandem with the GetTemplatedControls method
        protected virtual void Bind(object sender, EventArgs e)
        {
            PlaceHolder ph = (PlaceHolder)sender;
            GridViewRow dgi = (GridViewRow)ph.NamingContainer;
            WebControl ctrl = (WebControl)ph.FindControl(Id);
            if (column.IsStatic)
            {
                SetValue(ctrl, column.Text);
            }
            else
            {
                if (column.IsLookup)
                {
                    object oValue = DataBinder.Eval(dgi.DataItem, column.Text);
                    object lookupValue = null;
                    try
                    {
                        lookupValue = lookup.GetLookup(column.Lookup)[oValue];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Item not found for the lookup for " + column.Name + ".", ex);
                    }
                    SetValue(ctrl,
                        ((oValue != null) ? lookupValue :
                        oValue));
                }
                else
                {
                    SetValue(ctrl, DataBinder.Eval(dgi.DataItem, column.Text));
                }
            }
        }

        //instantiating the appropriate list of controls corresponding to 
        //the field being rendererd
        protected virtual List<WebControl> GetTemplatedControls()
        {
            Label lbl = new Label();
            lbl.ID = Id;
            return new List<WebControl>(new WebControl[] { lbl });
        }

        protected virtual string Id
        {
            get { return string.Format("lbl{0}", column.Name); }
        }

        //to what property of the control (in a gridview template) should the value of the field be set?
        //The basic rendrer represents looked up values with labels and thus it set
        //the text property of the Label control. Subclasses need to override this
        //method of the field will be rendered in a different contol
        protected virtual void SetValue(WebControl control, object value)
        {
            ((Label)control).Text = value.ToString();
        }

        #endregion
    }

    //overides the basic renderer to render fields a link in a grid view
    public class LinkGINColumnRenderer : GINColumnRenderer
    {
        public LinkGINColumnRenderer(GINColumnDescriptor column)
            : base(column)
        {
        }
        public LinkGINColumnRenderer() { }

        //rendering the field as a link
        protected override List<WebControl> GetTemplatedControls()
        {
            LinkButton lnk = new LinkButton();
            lnk.ID = Id;
            lnk.CommandName = column.Name;
            lnk.Command += new CommandEventHandler(lnk_Command);
            return new List<WebControl>(new WebControl[] { lnk });
        }

        protected override string Id
        {
            get
            {
                return string.Format("lnk{0}", column.Name);
            }
        }

        //since the link renderer uses a link button, it has to override this
        //method to set it with field's value
        protected override void SetValue(WebControl control, object value)
        {
            ((LinkButton)control).Text = ((value != null) ? value.ToString() : string.Empty);
        }

        //The method is overriden so that the commandargument of the linkbutton
        //is set with the value of the Key field of the view
        protected override void Bind(object sender, EventArgs e)
        {
            base.Bind(sender, e);
            PlaceHolder ph = (PlaceHolder)sender;
            GridViewRow dgi = (GridViewRow)ph.NamingContainer;
            LinkButton lnk = (LinkButton)ph.FindControl(Id);
            lnk.CommandArgument = DataBinder.Eval(dgi.DataItem, column.Key).ToString();
        }

        //triggering the command event of the renderer, so that the event is
        //delegated to the containing page
        protected void lnk_Command(object sender, CommandEventArgs e)
        {
            OnCommand(e);
        }

        protected void OnCommand(CommandEventArgs e)
        {
            if (Command != null)
            {
                Command(this, e);
            }
        }

        public event CommandEventHandler Command;
    }

    //This renders a boolean field to a grid view as a check box
    public class CheckBoxGINColumnRenderer : GINColumnRenderer
    {
        public CheckBoxGINColumnRenderer(GINColumnDescriptor column) : base(column) { }

        public override DataControlField RenderInTable()
        {
            CheckBoxField field = new CheckBoxField();
            field.HeaderText = column.Header;
            field.DataField = column.Text;
            return field;
        }

        public override WebControl RenderInEditor()
        {
            CheckBox cb = new CheckBox();
            cb.ID = string.Format("cb{0}", column.Name);
            cb.Enabled = column.IsEditable;
            cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
            cb.DataBinding += new EventHandler(cb_DataBinding);
            return cb;
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            PropertyInfo property = dataSource.GetType().GetProperty(column.Text);
            property.SetValue(dataSource, ((CheckBox)sender).Checked, null);
        }

        void cb_DataBinding(object sender, EventArgs e)
        {
            bool value = (bool)dataSource.GetType().GetProperty(column.Name).GetValue(dataSource, null);
            ((CheckBox)sender).Checked = value;
        }
    }

    public abstract class SubscriberResponse
    {
        public Control Publisher { get; set; }
        public Control[] Subscribers { get; set; }
        public Object SubscriberData { get; set; }
        public abstract void Respond();
    }
    //Using the Ajax calendar extender for date time fields
    public class DateTimeGINColumnRenderer : GINColumnRenderer
    {
        public DateTimeGINColumnRenderer(GINColumnDescriptor column) : base(column) { }

        public override DataControlField RenderInTable()
        {
            BoundField field = new BoundField();
            field.HeaderText = column.Header;
            field.DataField = column.Text;
            field.DataFormatString = "{0:MM-dd-yyyy}";
            return field;
        }

        public override WebControl RenderInEditor()
        {
            if (!column.IsEditable)
                return base.RenderInEditor();
            Panel ph = new Panel();
            ph.ID = string.Format("ph{0}", column.Name);
            TextBox txt = new TextBox();
            txt.Width = new Unit(100, UnitType.Percentage);
            txt.TextChanged += new EventHandler(base.txt_TextChanged);
            txt.ID = string.Format("txt{0}", column.Name);
            txt.DataBinding += new EventHandler(dttxt_DataBinding);
            CalendarExtender calExtender = new CalendarExtender();
            calExtender.ID = string.Format("calExtender{0}", column.Name);
            calExtender.Format = "MM-dd-yyyy";
            calExtender.TargetControlID = txt.ID;
            ph.Controls.Add(txt);
            ph.Controls.Add(calExtender);
            return ph;
        }

        protected void dttxt_DataBinding(object sender, EventArgs e)
        {
            object oValue = dataSource.GetType().GetProperty(column.Text).GetValue(dataSource, null);
            TextBox txt = (TextBox)sender;
            if (NullFinder.IsNull(oValue, column.DataType))
            {
                txt.Text = string.Empty;
            }
            else
            {
                txt.Text = string.Format("{0:MM-dd-yyyy}", (DateTime)oValue);
            }
        }
    }

    public class DateAndTimeGINColumnRenderer : GINColumnRenderer
    {
        public DateAndTimeGINColumnRenderer(GINColumnDescriptor column) : base(column) { }

        public override DataControlField RenderInTable()
        {
            BoundField field = new BoundField();
            field.HeaderText = column.Header;
            field.DataField = column.Text;
            field.DataFormatString = "{0:MM-dd-yyyy hh:mm:ss}";
            return field;
        }

        public override WebControl RenderInEditor()
        {
            if (!column.IsEditable)
                return base.RenderInEditor();
            Panel ph = new Panel();
            ph.ID = string.Format("ph{0}", column.Name);
            Table renderTable = new Table();
            TableRow renderRow = new TableRow();
            renderTable.Rows.Add(renderRow);
            TableCell renderDateCell = new TableCell();
            renderDateCell.Width = new Unit(60, UnitType.Percentage);
            TableCell renderTimeCell = new TableCell();
            renderTimeCell.Width = new Unit(40, UnitType.Percentage);
            renderRow.Cells.Add(renderDateCell);
            renderRow.Cells.Add(renderTimeCell);
            TextBox txtDt = new TextBox();
            txtDt.Width = new Unit(100, UnitType.Percentage);
            txtDt.TextChanged += new EventHandler(txtDt_TextChanged);
            txtDt.ID = string.Format("txtDt{0}", column.Name);
            txtDt.DataBinding += new EventHandler(txtDt_DataBinding);
            CalendarExtender calExtender = new CalendarExtender();
            calExtender.ID = string.Format("calExtender{0}", column.Name);
            calExtender.Format = "MM-dd-yyyy";
            calExtender.TargetControlID = txtDt.ID;
            TextBox txtTm = new TextBox();
            txtTm.Width = new Unit(100, UnitType.Percentage);
            txtTm.TextChanged += new EventHandler(txtTm_TextChanged);
            txtTm.ID = string.Format("txtTm{0}", column.Name);
            txtTm.DataBinding += new EventHandler(txtTm_DataBinding);
            MaskedEditExtender meExtender = new MaskedEditExtender();
            meExtender.ID = string.Format("meExtender{0}", column.Name);
            meExtender.AcceptAMPM = true;
            meExtender.Mask = "99:99";
            meExtender.MaskType = MaskedEditType.Time;
            meExtender.TargetControlID = txtTm.ID;

            renderDateCell.Controls.Add(txtDt);
            renderDateCell.Controls.Add(calExtender);
            renderTimeCell.Controls.Add(txtTm);
            renderTimeCell.Controls.Add(meExtender);
            ph.Controls.Add(renderTable);
            return ph;
        }

        protected void txtDt_TextChanged(object sender, EventArgs e)
        {
            PropertyInfo property = dataSource.GetType().GetProperty(column.Text);
            TextBox txt = (TextBox)sender;
            try
            {
                object oValue;
                if (txt.Text.Trim() == string.Empty)
                {
                    oValue = NullFinder.GetNullValue(column.DataType);
                }
                else
                {
                    DateTime oldValue = (DateTime)property.GetValue(dataSource, null);
                    string newValue = string.Format("{0} {1}", txt.Text, oldValue.ToShortTimeString());
                    oValue = DateTime.Parse(newValue);
                }
                property.SetValue(dataSource, oValue, null);
            }
            catch (Exception)
            {
                txtDt_DataBinding(txt, null);
            }
        }

        protected void txtDt_DataBinding(object sender, EventArgs e)
        {
            object oValue = dataSource.GetType().GetProperty(column.Text).GetValue(dataSource, null);
            TextBox txt = (TextBox)sender;
            if (NullFinder.IsNull(oValue, column.DataType))
            {
                txt.Text = string.Empty;
            }
            else
            {
                txt.Text = string.Format("{0:MM-dd-yyyy}", (DateTime)oValue);
            }
        }
        protected void txtTm_TextChanged(object sender, EventArgs e)
        {
            PropertyInfo property = dataSource.GetType().GetProperty(column.Text);
            TextBox txt = (TextBox)sender;
            try
            {
                object oValue;
                if (txt.Text.Trim() == string.Empty)
                {
                    oValue = NullFinder.GetNullValue(column.DataType);
                }
                else
                {
                    DateTime oldValue = (DateTime)property.GetValue(dataSource, null);
                    string newValue = string.Format("{0} {1}", oldValue.ToShortDateString(), txt.Text);
                    oValue = DateTime.Parse(newValue);
                }
                property.SetValue(dataSource, oValue, null);
            }
            catch (Exception)
            {
                txtTm_DataBinding(txt, null);
            }
        }

        protected void txtTm_DataBinding(object sender, EventArgs e)
        {
            object oValue = dataSource.GetType().GetProperty(column.Text).GetValue(dataSource, null);
            TextBox txt = (TextBox)sender;
            if (NullFinder.IsNull(oValue, column.DataType))
            {
                txt.Text = string.Empty;
            }
            else
            {
                txt.Text = ((DateTime)oValue).ToShortTimeString();
            }
        }
    }


    public class CDDExtenderRenderer : GINColumnRenderer
    {
        public override WebControl RenderInEditor()
        {
            return null;
        }

        public override DataControlField RenderInTable()
        {
            return null;
        }

    }

    #region The validation rule semantics

    [XmlInclude(typeof(RequiredValidationRule)),
    XmlInclude(typeof(PatternValidationRule)),
    XmlInclude(typeof(RangeValidationRule)),
    XmlInclude(typeof(CompareValidationRule))]
    public abstract class ValidationRule
    {
        private string validationType;
        private string errorMessage;

        public ValidationRule() { }
        public ValidationRule(string validationType, string errorMessage)
        {
            this.validationType = validationType;
            this.errorMessage = errorMessage;
        }

        [XmlAttribute]
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        [XmlIgnore]
        public string ValidationType
        {
            get { return validationType; }
            set { validationType = value; }
        }
    }

    public class RequiredValidationRule : ValidationRule
    {
        private bool isRequired = false;

        public RequiredValidationRule() : base("Required", string.Empty) { }
        public RequiredValidationRule(bool isRequired, string errorMassage)
            : base("Required", errorMassage)
        {
            this.isRequired = isRequired;
        }

        [XmlAttribute]
        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }
    }

    public class PatternValidationRule : ValidationRule
    {
        private string pattern;
        public PatternValidationRule() : base("Pattern", string.Empty) { }
        public PatternValidationRule(string pattern, string errorMassage)
            : base("Pattern", errorMassage)
        {
            this.pattern = pattern;
        }

        [XmlAttribute]
        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }
    }

    public class RangeValidationRule : ValidationRule
    {
        private string leftBoundary;
        private string rightBoundary;

        public RangeValidationRule() : base("Range", string.Empty) { }
        public RangeValidationRule(string leftBoundary, string rightBoundary, string errorMassage)
            : base("Range", errorMassage)
        {
            this.leftBoundary = leftBoundary;
            this.rightBoundary = rightBoundary;
        }

        [XmlAttribute]
        public string LeftBoundary
        {
            get { return leftBoundary; }
            set { leftBoundary = value; }
        }

        [XmlAttribute]
        public string RightBoundary
        {
            get { return rightBoundary; }
            set { rightBoundary = value; }
        }
    }

    public class CompareValidationRule : ValidationRule
    {
        private ValidationCompareOperator compareOperator;
        private ValidationDataType ruleDataType;
        private string valueToCompare;

        public CompareValidationRule() : base("Compare", string.Empty) { }
        public CompareValidationRule(
            ValidationCompareOperator compareOperator,
            ValidationDataType ruleDataType,
            string valueToCompare, string errorMassage)
            : base("Compare", errorMassage)
        {
            this.compareOperator = compareOperator;
            this.ruleDataType = ruleDataType;
            this.valueToCompare = valueToCompare;
        }

        [XmlAttribute]
        public ValidationCompareOperator CompareOperator
        {
            get { return compareOperator; }
            set { compareOperator = value; }
        }

        [XmlAttribute]
        public ValidationDataType RuleDataType
        {
            get { return ruleDataType; }
            set { ruleDataType = value; }
        }

        [XmlAttribute]
        public string ValueToCompare
        {
            get { return valueToCompare; }
            set { valueToCompare = value; }
        }

    }

    #endregion
}