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
using WarehouseApplication.GINLogic;
using AjaxControlToolkit;

namespace WarehouseApplication.UserControls
{
    public partial class SearchConditionSelector : System.Web.UI.UserControl
    {
        private ILookupSource lookupSource;
        protected void Page_Load(object sender, EventArgs e)
        {
            SetSearchUp();
        }

        public ILookupSource LookupSource
        {
            get { return lookupSource; }
            set { lookupSource = value; }
        }

        public IDataFilter DataFilter
        {
            get
            {
                return (IDataFilter)ViewState[string.Format("{0}_DataFilter", ID)];
            }
            set
            {
                ViewState[string.Format("{0}_DataFilter", ID)] = value;
                foreach (DataFilterParameter parameter in value.Parameters)
                {
                    if (parameter.Caption == null) continue;
                    string initialValue = string.Empty;
                    //if ((parameter.ConditionType == FilterConditionType.Lookup) && (LookupSource != null))
                    //{
                    //    //IDictionary<object, string> dictionary = lookupSource.GetLookup(parameter.Name);
                    //    initialValue = lookupSource.GetLookup(parameter.Name).Keys.ElementAt(0).ToString();
                    //}
                    ViewState[GetParameterId(parameter.Name)] =
                        new DataFilterCondition(
                            parameter,
                            (((parameter.ConditionType & FilterConditionType.Range) == FilterConditionType.Range) ? FilterConditionType.Range : FilterConditionType.Comparison),
                            string.Empty, initialValue);
                }
            }
        }

        public DataFilterCondition this[string parameter]
        {
            get
            {
                return (DataFilterCondition)ViewState[GetParameterId(parameter)];
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SetSearchUp();
        }

        private string GetParameterId(string parameterName)
        {
            return string.Format("{0}_{1}_Condition", ID, parameterName);
        }

        private void SetSearchUp()
        {
            if (DataFilter != null)
            {
                SearchConditionContainer.Controls.Clear();
                Table table = new Table();
                SearchConditionContainer.Controls.Add(table);
                foreach (DataFilterParameter parameter in DataFilter.Parameters)
                {
                    if (parameter.Caption == null) continue;
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Width = Unit.Percentage(35);
                    cell.Controls.Add(new LiteralControl(parameter.Caption + " : "));
                    row.Cells.Add(cell);
                    if (((parameter.ConditionType & FilterConditionType.Comparison) == FilterConditionType.Comparison) &&
                        ((parameter.ConditionType & FilterConditionType.Range) == FilterConditionType.Range))
                    {

                        RadioButtonList rbl = new RadioButtonList();
                        rbl.ID = string.Format("{0}_CriteriaOption", parameter.Name);
                        rbl.Items.Add(new ListItem("Single"));
                        rbl.Items.Add(new ListItem("Range"));
                        rbl.SelectedIndex = ((this[parameter.Name].ConditionType == FilterConditionType.Comparison) ? 0 : 1);
                        rbl.AutoPostBack = true;
                        rbl.SelectedIndexChanged += new EventHandler(CriteriaOption_SelectedIndexChanged);
                        cell = new TableCell();
                        cell.Controls.Add(rbl);
                        row.Cells.Add(cell);
                    }
                    if (parameter.ConditionType == FilterConditionType.Lookup)
                    {
                        TableCell lookupCell = new TableCell();
                        lookupCell.ColumnSpan = 2;
                        DropDownList lookup = new DropDownList();
                        lookup.Width = new Unit(100, UnitType.Percentage);
                        lookup.ID = string.Format("{0}_Lookup", parameter.Name);
                        lookup.SelectedIndexChanged += new EventHandler(Lookup_SelectedIndexChanged);
                        if (lookupSource != null)
                        {
                            IDictionary<string, object> lookupData = lookupSource.GetInverseLookup(parameter.Name);
                            foreach (string key in lookupData.Keys)
                            {
                                lookup.Items.Add(new ListItem(key, lookupData[key].ToString()));
                            }
                            lookup.SelectedValue = this[parameter.Name].LeftOperand;
                            Lookup_SelectedIndexChanged(lookup, null);
                        }
                        lookupCell.Controls.Add(lookup);
                        row.Cells.Add(lookupCell);
                    }
                    else
                    {
                        TableCell left = new TableCell();
                        TextBox leftValue = new TextBox();
                        leftValue.Width = new Unit(100, UnitType.Percentage);
                        leftValue.Text = this[parameter.Name].LeftOperand;
                        leftValue.ID = string.Format("{0}_LeftValue", parameter.Name);
                        leftValue.TextChanged += new EventHandler(LeftValue_TextChanged);
                        left.Controls.Add(leftValue);
                        if (this[parameter.Name].Parameter.Type.FullName == "System.DateTime")
                        {
                            CalendarExtender ceLeft = new CalendarExtender();
                            ceLeft.ID = string.Format("{0}_LeftCalExtender", parameter.Name);
                            ceLeft.TargetControlID = leftValue.ID;
                            left.Controls.Add(ceLeft);
                        }
                        row.Cells.Add(left);
                        if (this[parameter.Name].ConditionType == FilterConditionType.Range)
                        {
                            TableCell right = new TableCell();
                            TextBox rightValue = new TextBox();
                            rightValue.Width = new Unit(100, UnitType.Percentage);
                            rightValue.Text = this[parameter.Name].RightOperand;
                            rightValue.ID = string.Format("{0}_RightValue", parameter.Name);
                            rightValue.TextChanged += new EventHandler(RightValue_TextChanged);
                            right.Controls.Add(rightValue);
                            row.Cells.Add(right);
                            if (this[parameter.Name].Parameter.Type.FullName == "System.DateTime")
                            {
                                CalendarExtender ceRight = new CalendarExtender();
                                ceRight.ID = string.Format("{0}_RightCalExtender", parameter.Name);
                                ceRight.TargetControlID = rightValue.ID;
                                right.Controls.Add(ceRight);
                            }
                        }
                    }
                    table.Rows.Add(row);
                }
            }
        }

        void Lookup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);
            string ddlID = ddl.ID;
            string parameterName = ddlID.Substring(0, ddlID.Length - "_Lookup".Length);
            this[parameterName].LeftOperand = ddl.SelectedValue;
        }

        void LeftValue_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = ((TextBox)sender);
            string txtID = txt.ID;
            string paramaterName = txtID.Substring(0, txtID.Length - "_LeftValue".Length);
            this[paramaterName].LeftOperand = txt.Text;
        }

        void RightValue_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = ((TextBox)sender);
            string textID = txt.ID;
            string paramaterName = textID.Substring(0, textID.Length - "_RightValue".Length);
            this[paramaterName].RightOperand = txt.Text;
        }

        void CriteriaOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rbl = ((RadioButtonList)sender);
            string optionGroup = rbl.ID;
            string parameterName = optionGroup.Substring(0, optionGroup.Length - "_CriteriaOption".Length);
            this[parameterName].ConditionType = ((rbl.SelectedIndex) == 0) ? FilterConditionType.Comparison : FilterConditionType.Range;
        }


    }
}