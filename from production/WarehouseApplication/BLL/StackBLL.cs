using System;
using System.Data;
using System.Data.SqlClient;
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
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public enum StackStatus { New = 1, Approved, Closed }
    public class StackBLL : GeneralBLL
    {
        #region fields
        private Guid _id;
        private Guid _shedId;
        private string _stackNumber;
        private StackStatus _status;
        private DateTime _dateStarted;
        private Guid _CommodityGradeid;
        private string _CommodityGradeName;
        private string _WarehouseName;
        private string _ShedName;
        private int _PhysicalAddress;
        private int _BeginingNoBags;
        private Guid _WarehouseId;
        private int _ProductionYear;
        #endregion
        #region properties
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Guid ShedId
        {
            get { return _shedId; }
            set { _shedId = value; }
        }
        public string StackNumber
        {
            get { return _stackNumber; }
            set { _stackNumber = value; }
        }
        public StackStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public DateTime DateStarted
        {
            get { return _dateStarted; }
            set { _dateStarted = value; }
        }
        public Guid CommodityGradeid
        {
            get { return _CommodityGradeid; }
            set { _CommodityGradeid = value; }
        }
        public string ShedName
        {
            get { return _ShedName; }
            set { _ShedName = value; }
        }
        public string WarehouseName
        {
            get { return _WarehouseName; }
            set { _WarehouseName = value; }
        }
        public string CommodityGradeName
        {
            get { return _CommodityGradeName; }
            set { _CommodityGradeName = value; }
        }
        public int PhysicalAddress
        {
            get { return _PhysicalAddress; }
            set { _PhysicalAddress = value; }
        }
        public int BeginingNoBags
        {
            get { return _BeginingNoBags; }
            set { _BeginingNoBags = value; }
        }
        public Guid WarehouseId
        {
            get { return _WarehouseId; }
            set { _WarehouseId = value; }
        }
        public int ProductionYear
        {
            get { return _ProductionYear; }
            set { _ProductionYear = value; }
        }

        #endregion


        public bool Add()
        {

            bool issaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            try
            {
                this.Id = Guid.NewGuid();
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                issaved = StackDAL.InsertStack(this, tran);
                if (issaved == true)
                {
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(this, WFStepsName.Stack.ToString(), UserBLL.GetCurrentUser(), "Add Stack");
                    if (at == 1)
                    {
                        tran.Commit();
                        issaved = true;
                    }
                    else
                    {
                        tran.Rollback();
                        issaved = false;
                    }
                }
                else
                {
                    tran.Rollback();
                    issaved = false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                issaved = false;
                throw ex;
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return issaved;
        }
        public bool ValidateForSave()
        {
            if (this.ShedId == null)
            {
                return false;
            }
            if (this.CommodityGradeid == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.StackNumber) == true)
            {
                return false;
            }
            if (this.Status == null)
            {
                return false;
            }
            if (this.DateStarted == null)
            {
                return false;
            }
            return true;
        }

        public List<StackBLL> Search(Nullable<Guid> ShedId, Nullable<Guid> CommodityGradeId, String StackNumber)
        {
            List<StackBLL> list = new List<StackBLL>();
            list = StackDAL.Search(ShedId, CommodityGradeId, StackNumber);
            try
            {
                if (list != null)
                {
                    list = MergeWithCommodityGrade(list);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        public List<StackBLL> GetActiveStackbyShedId(Guid shedId)
        {


            List<StackBLL> list = new List<StackBLL>();
            StackBLL obj = new StackBLL();
            try
            {
                List<StackBLL> dalList = StackDAL.GetActiveStackByShed(shedId);
                if (dalList != null)
                {
                    list.AddRange(dalList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Stack", ex);
            }
            return list;
        }
        public List<StackBLL> GetActiveStackbyShedId(Guid shedId, Guid CommodityGrade,
             int productionYear)
        {


            List<StackBLL> list = new List<StackBLL>();
            StackBLL obj = new StackBLL();
            try
            {
                List<StackBLL> dalList = StackDAL.GetActiveStackByShed(shedId);
                if (dalList != null)
                {
                    list = (from s in dalList where s.CommodityGradeid == CommodityGrade && s.ProductionYear == productionYear select s).ToList();
                    //list.AddRange(dalList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Stack", ex);
            }
            return list;
        }

        private List<StackBLL> MergeWithCommodityGrade(List<StackBLL> list)
        {

            List<StackBLL> mergedListComplete = new List<StackBLL>();
            List<CommodityGradeBLL> CommodityGradeList = new List<CommodityGradeBLL>();
            CommodityGradeList = CommodityGradeBLL.GetAllCommodityDetail();
            List<ShedBLL> listShed = new List<ShedBLL>();
            listShed = ShedBLL.GetAllShed();
            List<WarehouseBLL> listWarehouse = new List<WarehouseBLL>();
            listWarehouse = WarehouseBLL.GetAllActiveWarehouse();
            if (CommodityGradeList == null)
            {
                throw new Exception("Can not get Commodity Grade list.");

            }
            if (listShed == null)
            {
                throw new Exception("Can not get Shed list.");

            }
            if (listWarehouse == null)
            {
                throw new Exception("Can not get Warehouse list.");

            }

            var q = from stack in list
                    join CommGrade in CommodityGradeList on stack.CommodityGradeid equals CommGrade.CommodityGradeId
                    join Shed in listShed on stack.ShedId equals Shed.Id
                    join warehouse in listWarehouse on Shed.WarehouseId equals warehouse.WarehouseId
                    select new { stack.Id, stack.ShedId, stack.StackNumber, stack.Status, stack.DateStarted, stack.CommodityGradeid, Shed.ShedNumber, warehouse.WarehouseName, CommGrade.GradeName };

            foreach (var i in q)
            {
                StackBLL obj = new StackBLL();
                obj.Id = i.Id;
                obj.ShedId = i.ShedId;
                obj.ShedName = i.ShedNumber;
                obj.CommodityGradeName = i.GradeName;
                obj.StackNumber = i.StackNumber;
                obj.Status = i.Status;
                obj.DateStarted = DateTime.Parse(i.DateStarted.ToShortDateString());
                obj.WarehouseName = i.WarehouseName;
                mergedListComplete.Add(obj);
            }

            return mergedListComplete;

        }
    }
}
