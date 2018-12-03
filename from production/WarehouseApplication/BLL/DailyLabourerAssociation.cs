using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using ECX.DataAccess;
using WarehouseApplication.BLL;

namespace GINBussiness
{
    public class DailyLabourersAssociation : WarehouseBaseModel
    {
        public int ID { get; set; }
        public string AssociationName { get; set; }
        public Guid WarehouseID { get; set; }
        public static List<DailyLabourersAssociation> DailyLabourersAssociations(Guid warehouseID)
        {
            List<DailyLabourersAssociation> DailyLabourersAssociationList;

            DailyLabourersAssociationList = new List<DailyLabourersAssociation>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetAllLabourersAssociation", warehouseID);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                DailyLabourersAssociation pnm = new DailyLabourersAssociation();
                Common.DataRow2Object(r, pnm);
                DailyLabourersAssociationList.Add(pnm);
            }
            return DailyLabourersAssociationList;

        }
    }
}