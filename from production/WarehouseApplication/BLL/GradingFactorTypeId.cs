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

namespace WarehouseApplication.BLL
{
    public enum GradingFactorTypeStatus { Active=1, Cancelled}
    public class GradingFactorType
    {
        private Guid _id;
        private string _gradingFactorTypeName;
        private string _gradingFactorTypeValueType;
        private GradingFactorTypeStatus _gradingFactorTypeStatus;

        #region Properties
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public string GradingFactorTypeName
        {
            get
            {
                return this._gradingFactorTypeName;
            }
            set
            {
                this._gradingFactorTypeName = value;
            }
        }
        public string GradingFactorTypeValueType
        {
            get
            {
                return this._gradingFactorTypeValueType;
            }
            set
            {
                this._gradingFactorTypeValueType = value;
            }
        }
        public GradingFactorTypeStatus GradingFactorTypeStatus
        {
            get
            {
                return this._gradingFactorTypeStatus;
            }
            set
            {
                this._gradingFactorTypeStatus = value;
            }
        }
        #endregion

    }
}
