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
using System.Data.SqlClient;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public class GRNSentBLL
    {
        public Guid warehouseId { get; set; }
        public string warehousename { get; set; }
        public DateTime dataSent { get; set; }
        public int count { get; set; }


        public List<GRNSentBLL> getCount(DateTime dataSent , out int totalCount )
        {
            totalCount = 0;
            List<GRNSentBLL> list = null;
            List<WarehouseBLL> listWH = null;
            listWH = WarehouseBLL.GetAllActiveWarehouse();
            // intailize for all WH.
            list = new List<GRNSentBLL>();
            foreach (WarehouseBLL w in listWH)
            {
                GRNSentBLL oSentGRN = new GRNSentBLL();
                oSentGRN.warehouseId = w.WarehouseId;
                oSentGRN.warehousename = w.WarehouseName;
                oSentGRN.count = 0;
                oSentGRN.dataSent = dataSent;
                list.Add(oSentGRN);
            }

            List<GRNSentBLL> listTemp = null;
            
            listTemp = GRNSentDAL.getCountApprovedGRNSentbyDate(dataSent);
            if (listTemp != null)
            {
                foreach (GRNSentBLL w in list)
                {
                    foreach (GRNSentBLL s in listTemp)
                    {
                        if (s.warehouseId == w.warehouseId)
                        {
                            totalCount += s.count;
                            w.count = s.count;
                        }
                    }

                }
            }
            
            return list;
        }
    }
}
