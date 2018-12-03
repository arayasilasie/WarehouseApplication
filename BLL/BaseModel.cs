using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WarehouseApplication.BLL
{
    public class BaseModel
    {
        public Guid ID { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public String TrackingNumber { get; set; }
        public Guid ClientID { get; set; }
        public String ClientName { get; set; }
        public Guid WarehouseID { get; set; }
        public virtual bool IsValid() { throw new NotImplementedException(); }
        public string ErrorMessage { get; protected set; }
        public bool IsNew { get;  set; }
        public Guid UserID { get; set; }

        protected static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;

            }
        }
    }
}