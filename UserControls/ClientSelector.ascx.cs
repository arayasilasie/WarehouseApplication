using System;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    public partial class ClientSelector : System.Web.UI.UserControl
    {
        private string clientId = null;
        //Property
        public string ClientIdSelected
        {
            get
            {
                return clientId;
            }
            set
            {
                if(value != null)
                {
                    ClientGUID.Value  = value;
                }
                else
                {
                    ClientGUID.Value  = null;
                }
            }
        }


        public string ClientName { set; get; }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void txtClientId_TextChanged(object sender, EventArgs e)
        {
            ClientBLL objClient = null;
            if (this.txtClientId.Text != "")
            {
                objClient = new ClientBLL();
                objClient = ClientBLL.GetClinet(this.txtClientId.Text);
                if (objClient != null)
                {
                    if (objClient.ClientUniqueIdentifier == Guid.Empty)
                    {
                        this.lblMessage.ForeColor = System.Drawing.Color.Tomato;
                        this.lblMessage.Text = "Temporary Client Id";
                        this.ClientGUID.Value = Guid.Empty.ToString();
                        this.ClientName = "Temporary Client Id";
                    }
                    else
                    {

                        if (objClient.ClientName != "" && objClient.ClientUniqueIdentifier != null)
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            this.lblMessage.Text = "[ " + objClient.ClientName + " ]" /*+ "-" + this.txtClientId.Text*/;
                            this.ClientGUID.Value = objClient.ClientUniqueIdentifier.ToString();
                            this.ClientName = objClient.ClientName;
                        }
                        else
                        {
                            this.lblMessage.ForeColor = System.Drawing.Color.Tomato;
                            this.lblMessage.Text = "Please Provide Client Id and try agin.";
                            this.ClientName = "";
                        }
                    }
                }
                else
                {
                    this.lblMessage.ForeColor = System.Drawing.Color.Tomato;
                    this.lblMessage.Text = "No Client Found!";
                }
            }
            else if (string.IsNullOrEmpty(this.txtClientId.Text))
            {
                this.lblMessage.Text = "";
                this.ClientGUID.Value = Guid.Empty.ToString();
                this.ClientName = null;
                objClient=null;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (this.txtClientId.Text != "")
            //{
            //    ClientBLL objClient = new ClientBLL();
            //    objClient = ClientBLL.GetClinet(this.txtClientId.Text);
            //    if (objClient != null)
            //    {
            //        string Name = "";
            //        this.lblMessage.ForeColor = System.Drawing.Color.Black;
            //        if (objClient.ClientName != "" && objClient.ClientUniqueIdentifier != null)
            //        {
            //            this.lblMessage.Text = objClient.ClientName + "-" + this.txtClientId.Text;
            //            this.ClientGUID.Value = objClient.ClientUniqueIdentifier.ToString();
            //        }
            //        else
            //        {
            //            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            //            this.lblMessage.Text = "Please Provide Client Id and try agin.";
            //        }

            //    }
            //    else
            //    {
            //        this.lblMessage.ForeColor = System.Drawing.Color.Red;
            //        this.lblMessage.Text = "No Client Found";
            //    }
            //}
            //else
            //{
            //    this.lblMessage.Text = "Please Provide Client Id.";
            //    return;
            //}
        }
    }
}