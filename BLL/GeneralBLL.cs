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
    public enum ObjectLoading { Partial, Full }
    [Serializable]
    public class GeneralBLL
    {
        private string _TrackingNo;
        private Guid _createdBy;
        private DateTime _createdTimestamp;
        private Guid _lastModifiedBy;
        private DateTime _lastModifiedTimestamp;
        public string TrackingNo
        {
            get
            {
                return this._TrackingNo;
            }
            set
            {
                this._TrackingNo = value;
            }
        }
        public Guid CreatedBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
            }
        }
        public DateTime CreatedTimestamp
        {
            get
            {
                return this._createdTimestamp;
            }
            set
            {
                this._createdTimestamp = value;
            }
        }
        public Guid LastModifiedBy
        {
            get
            {
                return this._lastModifiedBy;
            }
            set
            {
                this._lastModifiedBy = value;
            }
        }
        public DateTime LastModifiedTimestamp
        {
            get
            {
                return this._lastModifiedTimestamp;
            }
            set
            {
                this._lastModifiedTimestamp = value;
            }
        }
        public ECXWF.CMessage Message;
        
    }
}
