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

namespace WarehouseApplication.DAL
{
    public class CommodityGradeDAL
    {
        public static Nullable<float> GetLotSize(Guid CommodityGradeId,Guid Lang)
        {
            Nullable<float> LotSize;
            
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                ECXLookUp.CCommodityGrade objCommodity = objEcxLookUp.GetCommodityGrade(Lang, CommodityGradeId);
                if (objCommodity != null)
                {

                    LotSize = objCommodity.LotSize;
                    return objCommodity.LotSize;
                }
                else
                {
                    return null;
                }
          
        }
    }
}
