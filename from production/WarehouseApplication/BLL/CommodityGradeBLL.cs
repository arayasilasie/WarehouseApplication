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
using WarehouseApplication.DAL;
using System.Collections;
using System.Collections.Generic;

namespace WarehouseApplication.BLL
{
    public class commodityType
    {
        public string Name;
        public Guid Id;
        public static List<commodityType> GetAllCoffeeTypes()
        {
            List<commodityType> list = null;
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CCommodityType[] objCommodity = objEcxLookUp.GetActiveCommodityTypes(Utility.GetWorkinglanguage());

            if (objCommodity != null && objCommodity.Length > 0)
            {
                list = new List<commodityType>();
                foreach (ECXLookUp.CCommodityType i in objCommodity)
                {
                    commodityType o = new commodityType();
                    o.Id = i.UniqueIdentifier;
                    o.Name = i.Name;
                    list.Add(o);
                }
            }
            return list;
        }


    }
    public class CommodityGradeBLL
    {
       
        public Guid CommodityId{get;set;}
        public Guid CommodityClassId { get; set; }
        public Guid CommodityGradeId { get; set; }
        public string Commodity { get; set; }
        public string ClassName { get; set; }
        public string GradeName { get; set; }
        public string Symbol { get; set; }
        public string UnitOfMeasure{get;set;}
        public Nullable<float> LotSize { get; set; }
        public int  LotSizeInBags { get; set; }
        #region cache
        private static CacheManager<CommodityGradeBLL> commodityCache = new CacheManager<CommodityGradeBLL>(
            "Commodity",
            delegate()
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                return (from com in objEcxLookUp.GetAllCommodities(Utility.GetWorkinglanguage())
                     select new CommodityGradeBLL{CommodityId=com.UniqueIdentifier, Commodity=com.Name, UnitOfMeasure=com.UnitOfMeasure}).ToList();
            },
            delegate(CommodityGradeBLL commodity)
            {
                return commodity.CommodityId.ToString();
            },
            CacheManager<CommodityGradeBLL>.CacheDurability.Indefinite);

        private static CacheManager<CommodityGradeBLL> commodityClassCache = new CacheManager<CommodityGradeBLL>(
            "CommodityClass",
            delegate()
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                return (from com in commodityCache.GetAllItems()
                 from comClass in objEcxLookUp.GetActiveCommodityClasses(Utility.GetWorkinglanguage(), com.CommodityId)
                 select new CommodityGradeBLL { CommodityId = com.CommodityId, Commodity=com.Commodity, CommodityClassId = comClass.UniqueIdentifier, ClassName = comClass.Name }).ToList();
            },
            delegate(CommodityGradeBLL commodityClass)
            {
                return commodityClass.CommodityClassId.ToString();
            },
            CacheManager<CommodityGradeBLL>.CacheDurability.Indefinite);
        private static CacheManager<List<CommodityGradeBLL>> commodityGradeCache =
            new CacheManager<List<CommodityGradeBLL>>(
                "CommodityGrade",
                (CacheManager<List<CommodityGradeBLL>>.ItemLocator)delegate(string id)
                {
                    ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                    CommodityGradeBLL comClass = commodityClassCache.GetItem(id);
                    List<CommodityGradeBLL> comGrades = (from comGrade in objEcxLookUp.GetActiveCommodityGrades(Utility.GetWorkinglanguage(), new Guid(id))
                            select new CommodityGradeBLL 
                            { 
                                CommodityId = comClass.CommodityId, 
                                Commodity = comClass.Commodity,
                                CommodityClassId = comClass.CommodityClassId,
                                ClassName = comClass.ClassName,
                                CommodityGradeId = comGrade.UniqueIdentifier,
                                GradeName = comGrade.Name,
                                Symbol = comGrade.Symbol,
                                LotSize=comGrade.LotSize,
                                LotSizeInBags = comGrade.LotsizeInBag
                                //UnitOfMeasure=comGrade.LotsizeInBag
                                
                            }).ToList();
                    return comGrades;
                },
                delegate(List<CommodityGradeBLL> cgs)
                {
                    if (cgs.Count == 0)
                        return Guid.Empty.ToString();
                    return cgs[0].CommodityClassId.ToString();
                },
                CacheManager<List<CommodityGradeBLL>>.CacheDurability.Moderate);
#endregion 

        public static CommodityGradeBLL GetCommodityById(Guid Id)
        {
            try
            {
                return commodityCache.GetItem(Id.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static CommodityGradeBLL GetCommodityClassById(Guid Id)
        {
            try
            {
                return commodityClassCache.GetItem(Id.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetCommodityGradeNameById(Guid Id)
        {
            try
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                ECXLookUp.CCommodityGrade objCommodity = objEcxLookUp.GetCommodityGrade(Utility.GetWorkinglanguage(), Id);
                if (objCommodity != null)
                {
                    CommodityGradeBLL obj = new CommodityGradeBLL();
                    obj.GradeName = objCommodity.Name;
                    obj.CommodityClassId = objCommodity.CommodityClassUniqueIdentifier;
                    return obj.GradeName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static CommodityGradeBLL GetCommodityGrade(Guid Id)
        {
            try
            {
               CommodityGradeBLL comGrade = commodityGradeCache.GetAllItems().SelectMany(gs=>(from g in gs where g.CommodityGradeId == Id select g)).SingleOrDefault();
               if (comGrade == null)
               {
                   ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                   ECXLookUp.CCommodityGrade oGrade = objEcxLookUp.GetCommodityGrade(Utility.GetWorkinglanguage(), Id);
                   comGrade = (from grade in commodityGradeCache.GetItem(oGrade.CommodityClassUniqueIdentifier.ToString())
                    where grade.CommodityGradeId == Id select grade).SingleOrDefault();
                   
               }
               return comGrade;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Nullable<float> GetCommodityGradeLotSizeById(Guid CommodityGradeId)
        {
            return GetCommodityGrade(CommodityGradeId).LotSize;
        }
        public static int GetCommodityGradeLotSizeInBagsById(Guid CommodityGradeId)
        {
           // return GetCommodityGrade(CommodityGradeId).LotSizeInBags;
            return 30;
        }

        public static List<CommodityGradeBLL> GetAllCommodityDetail()
        {
            List<CommodityGradeBLL> list = new List<CommodityGradeBLL>();
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();

            // Get All Commodities
            ECXLookUp.CCommodity[] objCommodity = objEcxLookUp.GetAllCommodities(Utility.GetWorkinglanguage());
            foreach (ECXLookUp.CCommodity i in objCommodity)
            {
                ECXLookUp.CCommodityClass[] objCommodityClass = objEcxLookUp.GetAllCommodityClasses(Utility.GetWorkinglanguage(), i.UniqueIdentifier);
                foreach (ECXLookUp.CCommodityClass o in objCommodityClass)
                {

                    ECXLookUp.CCommodityGrade[] objGrade = objEcxLookUp.GetAllCommodityGrades(Utility.GetWorkinglanguage(), o.UniqueIdentifier);
                    foreach (ECXLookUp.CCommodityGrade c in objGrade)
                    {
                        CommodityGradeBLL objCG = new CommodityGradeBLL();
                        objCG.CommodityId = i.UniqueIdentifier;
                        objCG.Commodity = i.Name;
                        objCG.CommodityClassId = o.UniqueIdentifier;
                        objCG.ClassName = o.Name;
                        objCG.CommodityGradeId = c.UniqueIdentifier;
                        objCG.GradeName = c.Name;
                        objCG.Symbol = c.Symbol;
                        list.Add(objCG);
                    }
                }
            }
            string cgs = string.Empty;
            list.ForEach(cg => cgs += string.Format("id - {0}, name - {1}{2}", cg.CommodityGradeId, cg.GradeName, Environment.NewLine));
            Utility.LogException(new Exception(cgs));
            return list;
        }

        public static List<CommodityGradeBLL> GetAllCommodity()
        {
            return commodityCache.GetAllItems();
        }
        public static List<CommodityGradeBLL> GetCommodityClassByCommodityId(Guid CommodityId )
        {
            List<CommodityGradeBLL> comClass = commodityClassCache.GetAllItems();
             return (from cs in comClass
                        where cs.CommodityId == CommodityId
                        select cs).ToList();   
        }
        public static List<CommodityGradeBLL> GetCommodityGradeByClassId(Guid CommClassId)
        {
            List<CommodityGradeBLL> comClass = commodityGradeCache.GetItem(CommClassId.ToString());
            return comClass;
        }

    }
}
