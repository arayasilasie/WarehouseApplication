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
    public class WoredaBLL
    {
        private Guid _WoredaId;


        private Guid _ZoneId;
        private string _WoredaName;
        public Guid WoredaId
        {
            get { return _WoredaId; }
            set { _WoredaId = value; }
        }
        public Guid ZoneId
        {
            get { return _ZoneId; }
            set { _ZoneId = value; }
        }
        public string WoredaName
        {
            get { return _WoredaName; }
            set { _WoredaName = value; }
        }
        public WoredaBLL GetWoredabyId(Guid WoredaId)
        {
            try
            {
                WoredaBLL obj = new WoredaBLL();
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                ECXLookUp.CWoreda objWoreda = objEcxLookUp.GetWoreda(Utility.GetWorkinglanguage(), WoredaId);
                if (objWoreda != null)
                {
                    obj.WoredaId = objWoreda.UniqueIdentifier;
                    obj.WoredaName = objWoreda.Name;
                    obj.ZoneId = objWoreda.ZoneUniqueIdentifier;
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception("can not get woreda Name.");
            }
        }
    }
}
