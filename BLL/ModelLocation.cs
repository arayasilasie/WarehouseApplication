using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using GINBussiness;
using ECX.DataAccess;

namespace WarehouseApplication.BLL
{
    public class ModelLocation : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid RegionID { get; set; }
        public Guid? ZoneID { get; set; }
        public Guid? WoredaID { get; set; }

        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "SaveLocation", this);
        }

        public static void DeleteLocation(Guid ID)
        {
            SQLHelper.ExecuteSP(ConnectionString, "RemoveLocation", ID);
        }

        public static DataTable GetWoredaByName(string Name)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllLocationName", Name, true);
            return dt;
        }

        public static DataTable GetZoneByName(string Name)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllLocationName", Name, false);
            return dt;
        }

        public static DataTable GetRegion()
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllLocationPerType", null, null);
            return dt;
        }

        public static DataTable GetZone(Guid regionId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllLocationPerType", regionId, null);
            return dt;
        }

        public static DataTable GetWoreda(Guid zoneId, Guid regionId)
        {
            DataTable dt = SQLHelper.getDataTable(ConnectionString, "GetAllLocationPerType", regionId, zoneId);
            return dt;
        }

    }
}