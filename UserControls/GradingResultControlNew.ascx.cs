using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WarehouseApplication.UserControls
{
    public partial class GradingResultControlNew : System.Web.UI.UserControl
    {
        public string Type
        {
            set
            {
                ViewState.Add("Type", value);
            }
            get
            {
                if (ViewState["Type"] == null)
                    return "";
                else
                    return ViewState["Type"].ToString();
            }
        }
        public string errorMessage;
        public string Value
        {
            get
            {
                //if (_type == "LookUp" || _type == "Yes/No")
                //{
                //    return drpGradeResult.SelectedValue;
                //}
                //else
                //{
                //    return txtGradeResult.Text;
                //}
                if(drpGradeResult.Visible)
                    return drpGradeResult.SelectedValue;
                else
                    return txtGradeResult.Text;
            }
        }

        public string Text
        {
            get
            {
                if (drpGradeResult.Visible)
                    return drpGradeResult.SelectedItem.Text;
                else
                    return txtGradeResult.Text;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        public void setType(string type, string possibleValues, string result)
        {
            this.Type = type;
            if (this.Type == "LookUp" || this.Type == "Yes/No")
            {
                rfvdrpGradeResult.Enabled = true;
                rfvtxtGradeResult.Enabled = false;
                rfvdrpGradeResult.Visible = true;
                rfvtxtGradeResult.Visible = false;

                drpGradeResult.Attributes.Add("onchange", "DrpChange('" + txtdrpGradeResult.ClientID + "','" + drpGradeResult.ClientID + "');");
                drpGradeResult.Items.Clear();
                drpGradeResult.Visible = true;
                txtGradeResult.Visible = false;
                chkGradeResult.Visible = false;
                if (this.Type == "Yes/No")
                {
                    drpGradeResult.Items.Add(new ListItem("",""));
                    drpGradeResult.Items.Add("Yes");
                    drpGradeResult.Items.Add("No");
                    if (result.Trim() != string.Empty)
                        drpGradeResult.SelectedValue = result;
                }
                else
                {
                    if (possibleValues != null && possibleValues.Trim() != string.Empty)
                    {
                        drpGradeResult.Items.Add(new ListItem("", ""));
                        string[] strArray = possibleValues.Split(';');
                        string[] tempStr;
                        foreach (string str in strArray)
                        {
                            tempStr = str.Substring(1, str.Length - 2).Trim().Split('|');
                            if (tempStr.Length > 1)
                                drpGradeResult.Items.Add(new ListItem(tempStr[0], tempStr[1]));
                            else
                                drpGradeResult.Items.Add(new ListItem(tempStr[0], tempStr[0]));
                        }
                        if (result.Trim() != string.Empty)
                            drpGradeResult.SelectedValue = txtdrpGradeResult.Text = result;
                    }
                }
            }
            else
            {
                rfvdrpGradeResult.Enabled = false;
                rfvtxtGradeResult.Enabled = true;
                rfvdrpGradeResult.Visible = false;
                rfvtxtGradeResult.Visible = true;
                if (this.Type == "Percent")
                {
                    rangeevtxtGradeResult.Enabled = true;
                    rangeevtxtGradeResult.ErrorMessage = "The input is isvalid. Value can only be between 0 and 100 and non decimal!";
                    rangeevtxtGradeResult.Type = ValidationDataType.Integer;
                    rangeevtxtGradeResult.MaximumValue = "100";
                    rangeevtxtGradeResult.MinimumValue = "0";
                }
                else if (this.Type == "Number")
                {
                    rangeevtxtGradeResult.Enabled = true;
                    rangeevtxtGradeResult.ErrorMessage = "The input is isvalid. Value can only be a non negative decimal!";
                    rangeevtxtGradeResult.Type = ValidationDataType.Double;
                    rangeevtxtGradeResult.MaximumValue = int.MaxValue.ToString();
                    rangeevtxtGradeResult.MinimumValue = "0";
                }
                else
                {
                    rangeevtxtGradeResult.Enabled = false;
                }

                txtGradeResult.Attributes.Add("onchange", "DrpChange('" + txtdrpGradeResult.ClientID + "','" + txtGradeResult.ClientID + "');");
                drpGradeResult.Visible = false;
                txtGradeResult.Visible = true;
                chkGradeResult.Visible = false;
                if (result.Trim() != string.Empty)
                    txtGradeResult.Text = txtdrpGradeResult.Text = result;
            }
        }

        public bool validate()
        {
            if(drpGradeResult.Visible)
            {
                if (drpGradeResult.SelectedIndex <= 0)
                {
                    errorMessage = "The input is isvalid. Please select one item from the list!";
                    return false;
                }
            }
            else
            {
                int tempi = -1; float tempf = 0;
                if (this.Type == "Percent" && (!int.TryParse(txtGradeResult.Text, out tempi) || tempi < 0))
                {
                    errorMessage = "The input is isvalid. Value can only be between 0 and 100 and non decimal!";
                    return false;
                }
                else if (this.Type == "Number" && (!float.TryParse(txtGradeResult.Text, out tempf) || tempf < 0))
                {
                    errorMessage = "The input is isvalid. Value can only be a non negative decimal!";
                    return false;
                }
            }
            return true;
        }
    }
}