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
    public class BagTypeBLL
    {
        #region Fields
        private Guid _id;
        private float _tare;
        private string _bagTypeName;
        private float _capacity;
        #endregion

        #region properties
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
          public float Tare
        {
            get { return _tare; }
            set { _tare = value; }
        }
        public string BagTypeName
        {
            get { return _bagTypeName; }
            set { _bagTypeName = value; }
        }
        public float Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        #endregion

        #region cache
        private static CacheManager<BagTypeBLL> bagTypeCache = new CacheManager<BagTypeBLL>(
            "BagType",
            (CacheManager<BagTypeBLL>.AllItemsEnumerator)delegate()
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                ECXLookUp.CBag[] activeBags = objEcxLookUp.GetActiveBags(Utility.GetWorkinglanguage());
                return (from bagType in activeBags
                        select new BagTypeBLL 
                        { 
                            Id = bagType.UniqueIdentifier, 
                            BagTypeName = bagType.Name, 
                            Tare = bagType.Tare,
                            Capacity=bagType.Capacity
                        }).ToList();
            },
            delegate(BagTypeBLL bagType)
            {
                return bagType.Id.ToString();
            },
            CacheManager<BagTypeBLL>.CacheDurability.Indefinite);
        private static CacheManager<CommodityGradeBagTypes> commodityGradeBagsCache = new CacheManager<CommodityGradeBagTypes>(
            "CommodityGradeBags",
            (CacheManager<CommodityGradeBagTypes>.ItemLocator)delegate(string commodityGradeId)
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                return new CommodityGradeBagTypes()
                    {
                        CommodityGradeId = new Guid(commodityGradeId),
                        CommodityGradeBags = (from bagType in objEcxLookUp.GetActiveCommodityGradeBags(Utility.GetWorkinglanguage(), new Guid(commodityGradeId))
                                              select bagTypeCache.GetItem(bagType.BagUniqueIdentifier.ToString())).ToList()
                    };
            },
            delegate(CommodityGradeBagTypes commodityGradeBags)
            {
                return commodityGradeBags.CommodityGradeId.ToString();
            },
            CacheManager<CommodityGradeBagTypes>.CacheDurability.Moderate);
        #endregion 

        public static BagTypeBLL GetBagType(Guid bagId)
        {
            return bagTypeCache.GetItem(bagId.ToString());
            //try
            //{
            //    ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            //    ECXLookUp.CBag objBag = objEcxLookUp.GetBag(Utility.GetWorkinglanguage(), bagId);
            //    if (objBag != null)
            //    {
            //        return new BagTypeBLL() { Id = objBag.UniqueIdentifier, BagTypeName = objBag.Name, Tare = objBag.Tare };
            //    }
            //    else
            //    {
            //        //To DO 
            //        return new BagTypeBLL() { Id = bagId, BagTypeName = "Remove HardCoded bag", Tare = 0.93F };
            //        //throw new InvalidTareException("Unable to Get Tare.", new Exception());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public static List<BagTypeBLL> GetAllBagTypes()
        {
            try
            {
                return bagTypeCache.GetAllItems();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Active Bags.", ex);
            }
        }
        public  void GetBagTypeById(Guid BagId)
        {
            BagTypeBLL bagType = bagTypeCache.GetItem(BagId.ToString());
            this.Id = bagType.Id;
            this.BagTypeName = bagType.BagTypeName;
            this.Tare = bagType.Tare;
            this.Capacity = bagType.Capacity;

            //try
            //{
            //    ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            //    ECXLookUp.CBag objBag = objEcxLookUp.GetBag(Utility.GetWorkinglanguage(), BagId);
            //    if (objBag != null)
            //    {
            //        this.Id = objBag.UniqueIdentifier;
            //        this.BagTypeName = objBag.Name;
            //        this.Tare = objBag.Tare;
            //    }
            //    else
            //    {
            //        throw new Exception("Unable to Get Bag Data.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public static List<BagTypeBLL> GetCommodityGradeBagTypes(Guid CommodityGradeId)
        {
            try
            {
                return commodityGradeBagsCache.GetItem(CommodityGradeId.ToString()).CommodityGradeBags;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Commodity Grade Bag.", ex);
            }
        }
        public List<BagTypeBLL> GetCommodityGradeBag(Guid CommodityGradeId)
        {
            try
            {
                return commodityGradeBagsCache.GetItem(CommodityGradeId.ToString()).CommodityGradeBags;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Commodity Grade Bag.", ex);
            }
        }

        private class CommodityGradeBagTypes
        {
            public CommodityGradeBagTypes() { }
            public Guid CommodityGradeId { get; set; }
            public List<BagTypeBLL> CommodityGradeBags { get; set; }
        }
    }
}
