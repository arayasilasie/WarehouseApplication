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
    public class TransactionTypeProvider
    {
        public static Guid GetTransactionTypeId(string TranType)
        {
            string strGUID = "";
            try
            {
                strGUID = ConfigurationSettings.AppSettings[TranType];
                return new Guid(strGUID);
            }
            catch
            {
                throw new InvalidTransactionType("Can not find Transaction type");
            }
            
        }
        public static Guid GetTransactionTypeId(Guid CommodityId)
        {
            //NoClient
            //if (CommodityId == Guid.Empty)
            //{
            //    throw new InvalidTransactionType("Can not find Transaction type");
            //}
            string strGUID = CommodityId.ToString();
            try
            {
                return new Guid (ConfigurationSettings.AppSettings[strGUID]);
                
            }
            catch
            {
                throw new InvalidTransactionType("Can not find Transaction type");
            }

        }
    }
}
