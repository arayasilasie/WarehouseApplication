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
using System.Collections;
using System.Collections.Generic;

namespace WarehouseApplication.BLL
{
    public class NIDTypeBLL
    {
        #region properties
        public int Id
        {
            get;
            set;
        }
        public String Name
        {
            get;
            set;
        }
        #endregion

        #region cache
        private static CacheManager<NIDTypeBLL> nidTypeCache = new CacheManager<NIDTypeBLL>(
            "NIDType",
            (CacheManager<NIDTypeBLL>.AllItemsEnumerator)delegate()
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                return (from nidType in objEcxLookUp.GetActiveNIDTypes(BLL.Utility.GetWorkinglanguage())
                        select new NIDTypeBLL
                        {
                            Id = nidType.Id,
                            Name = nidType.Name
                        }).ToList();
            },
            delegate(NIDTypeBLL nidType)
            {
                return nidType.Id.ToString();
            },
            CacheManager<NIDTypeBLL>.CacheDurability.Indefinite);
        #endregion

        public static NIDTypeBLL GetNIDType(Guid nidTypeId)
        {
            return nidTypeCache.GetItem(nidTypeId.ToString());
        }
        public static List<NIDTypeBLL> GetAllNIDTypes()
        {
            try
            {
                return nidTypeCache.GetAllItems();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get NID Types.", ex);
            }
        }
    }
}
