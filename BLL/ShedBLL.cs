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
    public class ShedBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public string ShedNumber { get; set; }
        public Guid WarehouseId { get; set; }
        public int NoStack { get; set;  }

        #region cache
        private static CacheManager<ShedBLL> shedCache = new CacheManager<ShedBLL>(
            "Shed",
            (CacheManager<ShedBLL>.AllItemsEnumerator)delegate()
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                return (from warehouse in WarehouseBLL.GetAllActiveWarehouse()
                        from shed in objEcxLookUp.GetActiveWarehouseSheds(Utility.GetWorkinglanguage(), warehouse.WarehouseId)
                        select new ShedBLL 
                        { 
                            Id = shed.UniqueIdentifier, 
                            ShedNumber= shed.ShedNumber, 
                           // NoStack = shed.NoOfStack,
                           NoStack = 10,
                            WarehouseId = warehouse.WarehouseId }).ToList();
            },
            delegate(ShedBLL shed)
            {
                return shed.Id.ToString();
            },
            CacheManager<ShedBLL>.CacheDurability.Indefinite);
        #endregion
        
        public List<ShedBLL> GetActiveShedByWarehouseId(Guid warehouseid)
        {
            //List<ShedBLL> list = new List<ShedBLL>();
            //ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            //ECXLookUp.CWarehouseShed[] objShed = objEcxLookUp.GetActiveWarehouseSheds(Utility.GetWorkinglanguage(), warehouseid);
            //foreach (ECXLookUp.CWarehouseShed shed in objShed)
            //{
            //    ShedBLL obj = new ShedBLL();
            //    obj.Id = shed.UniqueIdentifier;
            //    obj.ShedNumber = shed.ShedNumber;
            //    obj.WarehouseId = warehouseid;
            //    list.Add(obj);
            //}

            //ShedBLL o = new ShedBLL();
            //o.Id = new Guid("46995e4e-43c3-40cd-82be-c8d6b1c2aef8");
            //o.ShedNumber = "Shed-1";
            //list.Add(o);

            return (from shed in shedCache.GetAllItems() where shed.WarehouseId == warehouseid select shed).ToList();
        }
        public static List<ShedBLL> GetAllShed()
        {
            //List<ShedBLL> list = new List<ShedBLL>();
            ////ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ////ECXLookUp.CWarehouseShed[] objShed = objEcxLookUp.GetActiveWarehouseSheds(Utility.GetWorkinglanguage(), warehouseid);
            ////foreach (ECXLookUp.CWarehouseShed shed in objShed)
            ////{
            ////    ShedBLL obj = new ShedBLL();
            ////    obj.Id = shed.UniqueIdentifier;
            ////    obj.ShedNumber = shed.ShedNumber;
            ////    obj.WarehouseId = shed.WarehouseUniqueIdentifier;
            ////    list.Add(obj);
            ////}

            //ShedBLL o = new ShedBLL();
            //o.Id = new Guid("46995e4e-43c3-40cd-82be-c8d6b1c2aef8");
            //o.ShedNumber = "RW101-1";
            //o.WarehouseId = new Guid("fa0a52e8-9308-4d5e-b323-88ca5ba232ed");
            //list.Add(o);

            return shedCache.GetAllItems();
        }
        public ShedBLL GetActiveShedById(Guid ShedId)
        {
            //List<ShedBLL> list = new List<ShedBLL>();
            //ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            //ECXLookUp.CWarehouseShed[] objShed = objEcxLookUp.GetActiveWarehouseSheds(Utility.GetWorkinglanguage(), warehouseid);
            //foreach (ECXLookUp.CWarehouseShed shed in objShed)
            //{
            //    ShedBLL obj = new ShedBLL();
            //    obj.Id = shed.UniqueIdentifier;
            //    obj.ShedNumber = shed.ShedNumber;
            //    obj.WarehouseId = warehouseid;
            //    list.Add(obj);
            //}

            //ShedBLL o = new ShedBLL();
            //o.Id = new Guid("46995e4e-43c3-40cd-82be-c8d6b1c2aef8");
            //o.ShedNumber = "Shed-1";
            //list.Add(o);

            return (from shed in shedCache.GetAllItems() where shed.Id == ShedId select shed).SingleOrDefault();
        }

    }
}
