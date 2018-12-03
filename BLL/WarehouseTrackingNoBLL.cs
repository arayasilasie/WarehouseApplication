using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WarehouseApplication.DAL;
using System.Data.SqlClient;
using System.Data;

namespace WarehouseApplication.BLL
{
    public class WarehouseTrackingNoBLL
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public string TrackingNo { get; set; }

        public static bool Save(string TrackingNo , SqlTransaction tran)
        {
            bool isSaved = false;
            Guid Id = Guid.NewGuid();
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            isSaved = WarehouseTrackingNoDAL.Insert(Id, TrackingNo,WarehouseId, tran);
            return isSaved;
        }
        public static bool IsForWarehouse(Guid warehouseId , string TrackingNo)
        {
            bool isTrue = false;
            List<string> list = WarehouseTrackingNoDAL.GetWarehouseTracking(TrackingNo, warehouseId);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return isTrue;
        }
        public static List<WarehouseTrackingNoBLL> getWarehouseForTrackingNos(string trackingNos)
        {
            List<WarehouseTrackingNoBLL> list = null;
            list = WarehouseTrackingNoDAL.GetWarehouseForTrackingNos(trackingNos);
            return list;
        }


    }
}
