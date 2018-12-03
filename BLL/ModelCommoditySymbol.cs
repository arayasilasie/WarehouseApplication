using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using GINBussiness;
using ECX.DataAccess;

namespace CommoditySymbolBussiness
{
    public class ModelCommoditySymbol : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid? GradingFactorGroupID { get; set; }
        public int? Classification { get; set; }
        public int? MinimumTotalValue { get; set; }
        public int? MaximumTotalValue { get; set; }
        public string Grade { get; set; }
        public Guid? ParentID { get; set; }
        public bool InActive { get; set; }
        public string CommodityGroupI { get; set; }

        public List<woreda> woredaIdList;
        public void addWoreda(woreda woreda)
        {
            if (woredaIdList == null)
                woredaIdList = new List<woreda>();
            woredaIdList.Add(woreda);

        }
        public string woredaList
        {
            get
            {
                string woredainfoXML;

                if (woredaIdList == null || woredaIdList.Count == 0)
                {
                    woredainfoXML = "<CommoditySymbol></CommoditySymbol>";
                }
                else
                {

                    IEnumerable<string> woredaNods = woredaIdList.Select(s => s.ToXML);

                    woredainfoXML = "<CommoditySymbol>" + woredaNods.Aggregate((str, next) => str + next) + "</CommoditySymbol>";
                }
                return woredainfoXML;
            }
        }

        public static DataTable GetCommoditySymbol(Guid CommodityId, string Symbol, string ClassSymbol, string Grade, bool forGrade, bool ShowInActive,
                                                     Guid? woredaID)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommoditySymbol", CommodityId, Symbol, ClassSymbol, Grade, forGrade, ShowInActive, woredaID);
            return dt;
        }

        public static DataTable GetWoredaForClass(Guid commodityClass)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "[GetClassWoreda]", commodityClass);
            return dt;
        }

        public static DataTable GetFactorGroupForCommodity(Guid CommodityId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetFactorGroupForCommodity", CommodityId);
            return dt;
        }

        public static DataTable GetCommodityClassification(Guid CommodityId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityClassification", CommodityId);
            return dt;
        }

        public static DataTable GetWoreda(Guid CommodityId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetWoreda", CommodityId);
            return dt;
        }

        public static DataTable GetCommodityClassByCommodity(Guid CommodityId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetCommodityClassByCommodity", CommodityId);
            return dt;
        }
        
        public static DataTable GetAllCommodityTypeForClassMap()
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllCommodityTypeForClassMap");
            return dt;
        }

        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveCommoditySymbol", this);
        }
    }

    [Serializable]
    public class woreda
    {
        public Guid GradingClassID { get; set; }
        public string WoredaID { get; set; }
        public string WoredaName { get; set; }

        public string ToXML
        {
            get
            {
                return "<Woreda> " +
                        "<woredaId>" + WoredaID.ToString() + "</woredaId>" +
                        "</Woreda>";
            }
        }
    }
}