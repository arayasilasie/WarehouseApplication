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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;
using WarehouseApplication.GINLogic;

namespace WarehouseApplication.BLL
{
    public enum StackUnloadedStatus { New = 1, Approved, Cancelled }
    [Serializable]
    public class StackUnloadedBLL : GeneralBLL
    {
        #region Fields
        private Guid _id;
        private Guid _unloadingId;
        private Guid _stackId;
        private Guid _userId;
        private int _numberOfbags;
        private StackUnloadedStatus _status;
        private string _remark;
        private string _stackNo;
        private string _inventoryControllerName;
        private Guid _shedId;
        private string _shedName;
        #endregion
        #region Const
        public StackUnloadedBLL()
        {

        }
        public StackUnloadedBLL(StackUnloadedBLL source)
        {
            this.Id = source.Id;
            this.UnloadingId = source.UnloadingId;
            this.StackId = source.StackId;
            this.UserId = source.UserId;
            this.NumberOfbags = source.NumberOfbags;
            this.Status = source.Status;
            this.Remark = source.Remark;
            this.CreatedBy = source.CreatedBy;
            this.CreatedTimestamp = source.CreatedTimestamp;
            this.LastModifiedBy = source.LastModifiedBy;
            this.LastModifiedTimestamp = source.LastModifiedTimestamp;

        }
        #endregion
        #region properties
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Guid UnloadingId
        {
            get { return _unloadingId; }
            set { _unloadingId = value; }
        }
        public Guid StackId
        {
            get { return _stackId; }
            set { _stackId = value; }
        }
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public int NumberOfbags
        {
            get { return _numberOfbags; }
            set { _numberOfbags = value; }
        }
        public StackUnloadedStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public string StackNo
        {
            get { return _stackNo; }
            set { _stackNo = value; }
        }
        public string InventoryControllerName
        {
            get { return _inventoryControllerName; }
            set { _inventoryControllerName = value; }
        }
        public Guid ShedId
        {
            get { return _shedId; }
            set { _shedId = value; }
        }
        public string ShedName
        {
            get { return _shedName; }
            set { _shedName = value; }
        }
        #endregion
        #region Public methods 
        public  bool Add(SqlTransaction tran, List<StackUnloadedBLL> list, Guid Id)
        {
            if (list != null && list.Count > 0)
            {
                string strAt = "Stack Unloaded ";
                foreach (StackUnloadedBLL o in list)
                {
                    o.UnloadingId = Id;
                    if (StackUnloadedDAL.InsertStackUnloaded(o, tran) == false)
                    {
                        

                        return false;
                    }
                    else
                    {
                        UpdateInventory(o.StackId, o.NumberOfbags, 0, tran);
                        strAt += "[ ";
                        strAt += "(Id-" + o.Id.ToString() + ") , ";
                        strAt += "(UnloadingId-" + o.UnloadingId.ToString() + ") , ";
                        strAt += "(StackId-" + o.StackId.ToString() + "), ";
                        strAt += "(NumberOfBags-" + o.NumberOfbags.ToString() + "), ";
                        strAt += "(Status-" + o.Status.ToString() + "), ";
                        strAt += "(UnloadedBy-" + o.UserId.ToString() + "), ";
                        strAt += "(Remark-" + o.Remark.ToString() + "), ";
                        strAt += "(CreatedBy-" + o.CreatedBy.ToString() + ") ";
                        strAt += " ]";


                    }
                }
                int at = -1;
                AuditTrailBLL objAt = new AuditTrailBLL();
                at = objAt.saveAuditTrailStringFormat("New Data Added", strAt, WFStepsName.AddUnloadingInfo.ToString(), UserBLL.GetCurrentUser(), "Add statck Unloaded");
                if (at == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            


        }
        public bool Add( )
        {
            SqlTransaction tran ;
            SqlConnection conn = new SqlConnection();
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            int at = -1;
            AuditTrailBLL objAt = new AuditTrailBLL();
            try
            {
                conn = Connection.getConnection();
                
                
               
                if (StackUnloadedDAL.InsertStackUnloaded(this, tran) == true)
                {

                    
                   
                    at = objAt.saveAuditTrail(this, WFStepsName.AddUnloadingInfo.ToString(), UserBLL.GetCurrentUser(), "Add statck Unloaded");
                    if (at == 1)
                    {
                        tran.Commit();
                        tran.Dispose();
                        conn.Close();
                        return true;
                    }
                }
                else
                {
                    tran.Rollback();
                    tran.Dispose();
                    return false;
                }
            }
            catch
            {
                tran.Rollback();
                tran.Dispose();
                if (at == 1)
                {
                    objAt.RoleBack();
                }
                return false;
            }
                return false;
        }
        public bool Add(SqlTransaction tran)
        {
           
            int at = -1;
            AuditTrailBLL objAt = new AuditTrailBLL();
            try
            {
                if (StackUnloadedDAL.InsertStackUnloaded(this, tran) == true)
                {

                    at = objAt.saveAuditTrail(this, WFStepsName.AddUnloadingInfo.ToString(), UserBLL.GetCurrentUser(), "Add statck Unloaded");
                    if (at == 1)
                    {
                       
                        return true;
                    }
                }
                else
                {
                    
                    return false;
                }
            }
            catch ( Exception ex)
            {

                throw ex;
            }
            return false;
        }
        public List<StackUnloadedBLL> GetStackInformationByUnloadingId()
        {
            try
            {
                return StackUnloadedDAL.GetStackUnloadedByUnloadingId(this.UnloadingId);
            }
            catch( Exception ex)
            {
                throw new Exception("Unable to get Stack information", ex);
                
            }
        }
        public bool Update(SqlTransaction tran)
        {
            bool isSaved = false;
            try
            {
               
                // old object 
                StackUnloadedBLL oldobj = new StackUnloadedBLL();
                oldobj = oldobj.GetById(this.Id);
                AuditTrailBLL objAt = new AuditTrailBLL();
                int at = -1;
                
                if (oldobj.Status == this.Status)
                {
                    return true;
                }
                if (oldobj.Status == StackUnloadedStatus.New && this.Status == StackUnloadedStatus.Cancelled)
                {
                    UpdateInventory(this.StackId, (-1 * this.NumberOfbags), 0, tran);
                }
                else if (oldobj.Status == StackUnloadedStatus.New && this.Status == StackUnloadedStatus.Cancelled)
                {
                    UpdateInventory(this.StackId, this.NumberOfbags, 0, tran);
                }

                isSaved = StackUnloadedDAL.UpdateStackUnloaded(this, tran);

                if (isSaved == true)
                {
                    at = objAt.saveAuditTrail(oldobj, this, WFStepsName.EditUnloading.ToString(), UserBLL.GetCurrentUser(), "Edit Unloading");
                    if (at == 1)
                    {
                        isSaved = true;
                    }
                    else
                    {
                        isSaved = false;
                    }
                }
                else
                {                 
                    isSaved =  false;
                }
            }
            catch( Exception ex)
            {
                throw ex;
            }
            return isSaved;
        }
        private void UpdateInventory(Guid StackId, int noBags, float Weight, SqlTransaction tran)
        {
            if (noBags > 0)
            {
                InventoryServices.GetInventoryService().UnloadToStack(StackId, noBags, Weight, tran);
            }
            else
            {
                InventoryServices.GetInventoryService().LoadFromStack(StackId, noBags, Weight, tran);
            }

        }
        public StackUnloadedBLL GetById(Guid Id)
        {
            try
            {
                return StackUnloadedDAL.GetStackUnloadedById(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Stack information", ex);

            }
        }
        
        #endregion

    }
}
