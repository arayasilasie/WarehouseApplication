using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseApplication.BLL;
using System.Data;

namespace WarehouseApplication.BLL
{
    public class ModelWRNO:  GINBussiness.WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public int WareHouseReceiptNo { get; set; }
        public Guid WarehouseID { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        
        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "[ImportedWareHouseReceiptSave]", this);
        }
        public DataTable CheckWRNo(int WRNO)
        {
            DataTable dt = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[CheckWarehouseReceiptNo]", WareHouseReceiptNo);
            return dt;
        }
       
    }
}