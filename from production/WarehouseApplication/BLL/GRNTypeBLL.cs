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
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum GRNTypeStatus { Active = 1, Cancelled}
    public class GRNTypeBLL : GeneralBLL
    {
        #region Feilds
        private Guid _id;
        private string _grnType;
        private GRNTypeStatus _status;
        private string _description;
        private string _name;
        #endregion
        #region Properties
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string GRNType
        {
            get { return _grnType; }
            set { _grnType = value; }
        }

        public GRNTypeStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        public GRNTypeBLL()
        {

        }

        public List<GRNTypeBLL> GetActiveGRNType()
        {
            try
            {

                return GRNTypeDAL.GetActiveGRNTypes();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
