using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using ECX.DataAccess;
using WarehouseApplication.BLL;
using GradingBussiness;

namespace GINBussiness
{
    public class WarehouseOperator : WarehouseBaseModel
    {
        public Guid ID { get; set; }
        public Guid OperatorId { get; set; }
        public Guid ShedID { get; set; }
        public string ShedName { get; set; }
        public string ShedNo { get; set; }
        public Guid WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public int ProductionYear { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string StackNumber { get; set; }
        public Guid CreatedBy { get; set; } 
	    public DateTime CreatedDate { get; set; }  
	    public Guid LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; } 
        public static List<WarehouseOperator> WarehouseOperators(Guid warehouseID)
        {
            List<WarehouseOperator> operatorList;

            operatorList = new List<WarehouseOperator>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GettblWarehouseOperator", warehouseID);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                WarehouseOperator pnm = new WarehouseOperator();
                Common.DataRow2Object(r, pnm);
                operatorList.Add(pnm);
            }
            return operatorList;

        }
        public static List<GradingModel> Shed(Guid CwareHouseId)
        {
            List<GradingModel> gradingList = new List<GradingModel>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[GetAllShed]", CwareHouseId);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                GradingModel pnm = new GradingModel();
                Common.DataRow2Object(r, pnm);
                gradingList.Add(pnm);
            }
            return gradingList;
        }
        public static List<WarehouseOperator> WarehouseOperatorsList(Guid warehouseID, int Type)
        {
            List<WarehouseOperator> operatorList;

            operatorList = new List<WarehouseOperator>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[GetAllWarehouseOperators]", warehouseID,Type);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                WarehouseOperator pnm = new WarehouseOperator();
                Common.DataRow2Object(r, pnm);
                operatorList.Add(pnm);
            }
            return operatorList;

        }
        public static DataTable GetWarehouseOperators()
        {           
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[GetAllWarehouseOperators]");
            return WarehouseOperator;

        }
        public static List<WarehouseOperator> LICs(GINModel currentGIN)
        {
            List<WarehouseOperator> licList;
            licList = new List<WarehouseOperator>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetLICs", currentGIN.WarehouseID, currentGIN.CommodityGradeID, currentGIN.PickupNoticesList[0].ProductionYear);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                WarehouseOperator pnm = new WarehouseOperator();
                Common.DataRow2Object(r, pnm);
                licList.Add(pnm);
            }
            return licList;
        }
        public static List<WarehouseOperator> LICAll(Guid curentWareHouse)
        {
            List<WarehouseOperator> licList;
            licList = new List<WarehouseOperator>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "GetAllLICs", curentWareHouse);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                WarehouseOperator pnm = new WarehouseOperator();
                Common.DataRow2Object(r, pnm);
                licList.Add(pnm);
            }
            return licList;
        }
        public static void FillWareHouseOperatorType(DropDownList ddl)
        {
            ListItem li = new ListItem("Sampler", ((int)WareHouseOperatorTypeEnum.Sampler).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Grader", ((int)WareHouseOperatorTypeEnum.Grader).ToString());
            ddl.Items.Add(li);

            li = new ListItem("LIC", ((int)WareHouseOperatorTypeEnum.LIC).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Loader", ((int)WareHouseOperatorTypeEnum.Loader).ToString());
            ddl.Items.Add(li);

            li = new ListItem("Weigher ", ((int)WareHouseOperatorTypeEnum.Weigher).ToString());
            ddl.Items.Add(li);
            
        }
        public void Save()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "[SaveWareHouseOperators]", this);
        }
        public void DisableWareHouseOperator()
        {
            ECX.DataAccess.SQLHelper.Save(ConnectionString, "[UpdateWareHouseOperators]", this);
        }
       
        public static DataTable GetWarehouseOperatorType()
        {           
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "getOperatorTypes");
            return WarehouseOperator;

        }
        public static DataTable GetWarehouseOperatorTypeNo(Guid OperatorId)
        {
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[GetWarehouseOperatorTypeNo]", OperatorId);
            return WarehouseOperator;

        }

        public static DataTable ShedByLic(Guid CwareHouseId, Guid LIC)
        {
            return ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[GetShedByLIC]", CwareHouseId, LIC);
        }     
        public static List<WarehouseOperator> GetLICByShed(Guid warehouseID, Guid ShedID)
        {
            List<WarehouseOperator> licList;
            licList = new List<WarehouseOperator>();
            DataTable WarehouseOperator = ECX.DataAccess.SQLHelper.getDataTable(ConnectionString, "[GetStackByShed]", warehouseID, ShedID);
            foreach (DataRow r in WarehouseOperator.Rows)
            {
                WarehouseOperator pnm = new WarehouseOperator();
                Common.DataRow2Object(r, pnm);
                licList.Add(pnm);
            }
            return licList;
        }
    }

}

