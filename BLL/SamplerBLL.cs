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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum SamplerStatus { Active=1,Reassigned , Cancelled}
    public class SamplerBLL
    {
        #region Fileds

        private Guid _id;
        private Guid _samplingTicketId;
        private Guid _samplerId;
        private SamplerStatus _status;
        private Guid _createdBy;
        private DateTime _createdDate;
        private Guid _lastModifiedBy;
        private DateTime _lastModifiedDate;
        private string _samplerName;
        #endregion
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
        public Guid SampleingTicketId
        {
            get
            {
                return this._samplingTicketId;
            }
            set
            {
                this._samplingTicketId = value;
            }
        }
        public Guid SamplerId
        {
            get
            {
                return this._samplerId;
            }
            set
            {
                this._samplerId = value;
            }
        }
        public SamplerStatus Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        public String SamplerName
        {
            get
            {
                return this._samplerName;
            }
            set
            {
                this._samplerName = value;
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
        public DateTime CreatedDate
        {
            get
            {
                return this._createdDate;
            }
            set
            {
                this._createdDate = value;
            }

        }
        #endregion
        #region Constructors
        public SamplerBLL()
        {
        }
        public SamplerBLL(SamplerBLL source )
        {
            this._samplingTicketId = source._samplingTicketId;
            this._samplerId = source._samplerId;
            this._status = source._status;
            this._createdBy = source._createdBy;
            this._createdDate = source._createdDate;
            this._lastModifiedBy = source._lastModifiedBy;
            this._lastModifiedDate = source._lastModifiedDate;

        }
        #endregion
        #region Public Methods
        public List<SamplerBLL> GetSamplerBySamplingId(Guid SamplingId)
        {
            List<SamplerBLL> list= null;
            List<SamplerBLL> Temp;
            SamplerBLL obj = new SamplerBLL();

            Temp = SamplerDAL.GetSamplerBySamplingId(SamplingId);
            if (Temp != null)
            {
                if (Temp.Count > 0)
                {
                    list = new List<SamplerBLL>();
                    foreach (SamplerBLL i in Temp)
                    {
                        SamplerBLL objSampler = new SamplerBLL();
                        objSampler.SamplerId = i.SamplerId;
                        objSampler.SamplerName = UserBLL.GetName(i.SamplerId);
                        list.Add(objSampler);
                    }
                }
            }
            return list;
        }
        public SamplerBLL GetActiveSamplingSupBySamplingId(Guid SamplingId)
        {
            return SamplerDAL.GetActiveSamplerSupBySamplingId(SamplingId);
        }
        #endregion

    }
}
