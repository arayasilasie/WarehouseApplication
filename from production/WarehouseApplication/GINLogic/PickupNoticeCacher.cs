using System;
using System.Collections.Generic;
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

namespace WarehouseApplication.GINLogic
{
    public class PickupNoticeCacher
    {
        public static List<PickupNoticeInformation> ImportPickuNotice()
        {
            //List<PickupNoticeInformation> importedPuns = new List<PickupNoticeInformation>();
            //ECXCD.WR wr = new WarehouseApplication.ECXCD.WR();
            //ECXCD.PUN pun = wr.GetPun();
            //foreach(ECXCD.PUN.PickUpNoticeRow punData in pun.PickUpNotice.Rows)
            //{
            //    PickupNoticeInformation punInfo = new PickupNoticeInformation()
            //    {
            //        ClientId = punData.ClientId,
            //        ExpirationDate = punData.ExpirationDate,
            //        PickupNoticeId = punData.PUNId,
            //        WarehouseId = punData.WarehouseId,
            //        id = pun.
            //    }
            //    importedPuns.Add(
            //}
            return null;
        }
    }
}
