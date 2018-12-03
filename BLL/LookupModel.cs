using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace WarehouseApplication.BLL
{
    public class SimpleLookup
    {

        private static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;
            }
        }
        
        public string Name { get; set; }
        public bool Refresh { get; set; }
        public Guid ID { get; set; }
        public int EnumID { get; set; }
        public LookupTypeEnum LookupType
        {
            get
            {
                return (LookupTypeEnum)EnumID ;
            }
            set
            {
                EnumID = (int)value;
            }
        }

        List<LookupValue> lst;
        public  Dictionary<Guid, LookupValue> GetDictionary()
        {
            GetList();
            return lst.ToDictionary(l => l.ID);
        }
        public  List<LookupValue> GetList()
        {
            if (lst != null && lst.Count > 0 && !Refresh)
                return lst;
            lst = new List<LookupValue>();
            DataTable dt = new DataTable();
            ECX.DataAccess.SQLHelper.PopulateTable(SimpleLookup.ConnectionString, dt, "LookupGetAll", EnumID);
            foreach (DataRow dr in dt.Rows)
            {
                LookupValue lv = new LookupValue(this);
                ECX.DataAccess.Common.DataRow2Object(dr, lv);
                lst.Add(lv);
            }
            Refresh = false;
            return lst;
        }
        public List<LookupValue> GetList(Predicate<LookupValue> criteria )
        {
            GetList();
            return lst.FindAll(criteria);
        }
        static Dictionary<LookupTypeEnum, SimpleLookup> luLst;
        public static Dictionary<LookupTypeEnum, SimpleLookup> GetData()
        {
            if (luLst != null && luLst.Count > 0)
                return luLst;
            luLst = new Dictionary<LookupTypeEnum, SimpleLookup>();
            DataTable dt = new DataTable();
            ECX.DataAccess.SQLHelper.PopulateTable(SimpleLookup.ConnectionString, dt, "LookupGet");
            foreach (DataRow dr in dt.Rows)
            {
                SimpleLookup lv = new SimpleLookup();
                ECX.DataAccess.Common.DataRow2Object(dr, lv);
                luLst.Add(lv.LookupType, lv);
            }
            return luLst;
        }

        public static SimpleLookup Lookup(LookupTypeEnum type)
        {
            GetData();
            return luLst[type];
        }

    }
    public class LookupValue 
    {
        public Guid ID { get; set; }

        public string Symbol { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid RegionID { get; set; }
        public Guid ZoneID { get; set; }
        public Guid WoredaID { get; set; }
        public int CommodityGroupID { get; set; }
        public Guid CommodityID { get; set; }
        public int MaxLimit { get; set; }
        public Guid StandardUnitOfMeasureID { get; set; }
        public SimpleLookup theParent { get; private set; }
        public LookupValue(SimpleLookup parent)
        {
            theParent = parent;
        }
    }
   
    public enum LookupTypeEnum
    {
        Commodity = 1,
        CommodityGroupBagTypes = 2,
        CommoditySymbols = 3,
        MeasureTypes = 4,
        UnitsOfMeasureConversion = 5,
        UnitsOfMeasures = 6,
        Warehouses = 7,
        Locations = 8,
        Regions = 9,
        Zones = 10,
        Woredas = 11
        //WashingStations=12
    }
}