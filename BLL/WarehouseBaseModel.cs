using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace GINBussiness
{
    [Serializable]
    public class WarehouseBaseModel
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public String TrackingNo { get; set; }
        public virtual bool IsValid() { throw new NotImplementedException(); }
        public string ErrorMessage { get;  set; }
        //public string ErrorMessage { get; protected set; }

        protected static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;

            }
        }

        public string CDConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["dbCentralDepositoryConnectionString"].ConnectionString;

            }
        }

    }
}





