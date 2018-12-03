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
    public enum CGTotalValueStatus { New = 1, Cancelled }
    [Serializable]
    public class CommodityGradeTotalValueBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid CommodityGradeId { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        public CGTotalValueStatus Status { get; set; }

        public CommodityGradeTotalValueBLL GetByCommodityGradeId(Guid CGId )
        {
            return CommodityGradeTotalValueDAL.GetByCommodityGradeId(CGId);
        }
        /// <summary>
        /// if a total value exists for a Commodity grade it will update else it will create a new one
        /// </summary>
        /// <returns></returns>
        public bool Update(CommodityGradeTotalValueBLL oldObject)
        {
            SqlTransaction tran = null;
            SqlConnection conn = Connection.getConnection();
            bool isSaved = false;
            
            try
            {
                tran = conn.BeginTransaction();
                if (CommodityGradeTotalValueDAL.Update(this, tran) == true)
                {
                    // take audit trail
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    if (oldObject != null)
                    {
                        at = objAt.saveAuditTrail(oldObject, this, WFStepsName.CommodityGradeTotalValue.ToString(), UserBLL.GetCurrentUser(), "Update CGTV");
                    }
                    else
                    {
                        at = objAt.saveAuditTrail(this, WFStepsName.CommodityGradeTotalValue.ToString(), UserBLL.GetCurrentUser(), "Add CGTV");
                    }
                    if (at == 1)
                    {
                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                else
                {
                    return isSaved;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return false;
         }
      
    }
}
