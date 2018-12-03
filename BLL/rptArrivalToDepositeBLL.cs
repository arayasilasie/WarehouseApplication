using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public class rptArrivalToDepositeBLL
    {
        public string VoucherNo { get; set; }
        public string ClientName { get; set; }
        public string PlateNo { get; set; }
        public string TrailerPlateNo { get; set; }
        public int NoBags { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime unloadedDate { get; set; }
        public Guid ClientId;
        public Guid WarehouseId;

        public List<rptArrivalToDepositeBLL> GetReportData(Guid WarehouseId, DateTime from)
        {
            List<rptArrivalToDepositeBLL> list = null;
            list = rptArrivalToDepositeDAL.GetReportData(WarehouseId, from);
            if (list != null)
            {
                // string WarehouseName =  WarehouseBLL.GetWarehouseNameById(WarehouseId);
                //iterate through list to get clientname .
                for (int i = 0; i < list.Count; i++)
                {
                    Guid ClientId = list[i].ClientId;
                    string Cname = ClientBLL.GetClinetNameById(ClientId);
                    list[i].ClientName = Cname;
                }
            }
            return list;

        }

    }
}
