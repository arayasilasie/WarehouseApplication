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
    public class WarehouseBLL
    {
        public Guid WarehouseId{get;set;}
        public string WarehouseName { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public Guid LocationId { get; set; }
        public Guid WarehouseType { get; set; }

        #region cache
        private static CacheManager<WarehouseBLL> warehouseCache = new CacheManager<WarehouseBLL>(
            "Warehouse",
            (CacheManager<WarehouseBLL>.AllItemsEnumerator)delegate()
            {
                Guid workingLanguage = Utility.GetWorkinglanguage();
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
               List<ECXLookUp.CWarehouseType> list = objEcxLookUp.GetActiveWarehouseTypes(workingLanguage).ToList<ECXLookUp.CWarehouseType>();

               List<ECXLookUp.CWarehouse> listWarehouse = objEcxLookUp.GetActiveWarehouses(workingLanguage, list[0].UniqueIdentifier).ToList<ECXLookUp.CWarehouse>();

               List<ECXLookUp.CLocation> listLocation = objEcxLookUp.GetActiveLocations(workingLanguage).ToList();

               return (from warehouse in
                           objEcxLookUp.GetActiveWarehouseTypes(workingLanguage)
                               .SelectMany(wht => (from wh in objEcxLookUp.GetActiveWarehouses(workingLanguage, wht.UniqueIdentifier)
                                                   select wh))
                       join locationCode in objEcxLookUp.GetActiveLocations(workingLanguage)
                       on warehouse.LocationGuid equals locationCode.UniqueIdentifier
                       where warehouse.Code != null && warehouse.Name.ToUpper() != "Unknown".ToUpper()
                       orderby warehouse.Name ascending
                       //&&
                       //(
                       // warehouse.Code.ToString() == "1"
                       //|| warehouse.Code.ToString() == "3"
                       //|| warehouse.Code.ToString() == "102"
                       //|| warehouse.Code.ToString() == "4"
                       //|| warehouse.Code.ToString() == "5"
                       //|| warehouse.Code.ToString() == "6"
                       //|| warehouse.Code.ToString() == "7"
                       //|| warehouse.Code.ToString() == "20"
                       //|| warehouse.Code.ToString() == "24")
                      
                        select new WarehouseBLL()
                        {
                            Code = warehouse.Code,
                            Location = locationCode.Code,
                            LocationId = warehouse.LocationGuid,
                            WarehouseId = warehouse.UniqueIdentifier,
                            WarehouseName = warehouse.Name,
                            WarehouseType = warehouse.WarehouseTypeUniqueIdentifier
                        }).ToList() ;
            },
            delegate(WarehouseBLL warehouse)
            {
                return warehouse.WarehouseId.ToString();
            },
            CacheManager<WarehouseBLL>.CacheDurability.Indefinite);
        #endregion 


        public static string GetWarehouseCode(Guid warehouseid)
        {
            return GetById(warehouseid).Code;
          
        }
        public static List<WarehouseBLL> GetAllActiveWarehouse()
        {
            return warehouseCache.GetAllItems();
        }
        public static WarehouseBLL GetById(Guid Id)
        {
            return warehouseCache.GetItem(Id.ToString());
        }
        public static string GetWarehouseNameById(Guid Id)
        {
            return GetById(Id).WarehouseName;
        }
        public static WarehouseBLL CurrentWarehouse
        {
            get
            {
                try
                {
                    Guid warehouseId = Guid.Empty;
                    if (HttpContext.Current.Session != null)
                    {
                        if (HttpContext.Current.Session["CurrentWarehouse"] != null)
                        {
                            warehouseId = (Guid)HttpContext.Current.Session["CurrentWarehouse"];
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("selectwarehouse.aspx");
                        }
                    }
                    else
                    {
                        warehouseId = new Guid(HttpContext.Current.Request["CurrentWarehouse"]);
                    }
                    return GetById(warehouseId);
                }
                catch (Exception ex)
                {
                    throw new Exception("Can't find the current warehouse", ex);
                  
                }
            }
        }



    

    }
}
