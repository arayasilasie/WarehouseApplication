using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WarehouseApplication.DALManager
{
    public class GuidIdentifier : IDataIdentifier
    {
        public GuidIdentifier() { }
        public GuidIdentifier(Guid id, XmlDocument preview)
        {
            this.preview = preview;
            this.id = id;
        }
        private XmlDocument preview;
        private Guid id;

        #region IDataIdentifier Members

        public object ID
        {
            get { return id; }
            set { id = (Guid)value; }
        }

        public XmlDocument Preview
        {
            get { return preview; }
            set { preview = value; }
        }

        #endregion
    }
}
