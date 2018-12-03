using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public enum GradingFactorGroupStatus { Active =1 , Cancelled}
    [Serializable]
    public class GradingFactorGroupBLL : GeneralBLL 
    {
        public Guid Id { get; set; }
        public string GradingFactorGroupName { get; set; }
        public GradingFactorGroupStatus Status { get; set; }
        public string Description { get; set; }

        public bool Save(List<GradingFactorGroupDetailBLL> list)
        {
            bool issaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            int At = -1;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                this.Id = Guid.NewGuid();
                this.CreatedBy = UserBLL.GetCurrentUser();
                issaved = GradingFactorGroupDAL.Save(this, tran);
                if (issaved == true)
                {
                    issaved = false;
                    GradingFactorGroupDetailBLL objGFD = new GradingFactorGroupDetailBLL();
                    issaved = objGFD.save(list, tran, this.Id);
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    At = objAt.saveAuditTrail(this, WFStepsName.GradingFactorGroupAd.ToString(), UserBLL.GetCurrentUser(), "Add Grading Factor Group ");
                    if (At == 1)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Rollback();
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return issaved;
        }
        public List<GradingFactorGroupBLL> GetActive()
        {
            return GradingFactorGroupDAL.GetActive();
        }
    }
    public enum GradingFactorGroupDetailStatus { Active = 1, Cancelled }
    [Serializable]
    public class GradingFactorGroupDetailBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Nullable<Guid> GradingFactorGroupId { get; set; }
        public Nullable<Guid> GradingFactorId { get; set; }
        public Nullable<Guid> GradingTypeId { get; set; }
        public Nullable<float> MaximumValue { get; set; }
        public Nullable<float> MinimumValue { get; set; }
        public string FailPoint { get; set;}
        public Nullable<int> isMax {get ; set ;}
        public Nullable<bool> isInTotalValue { get; set; }
        public string PossibleValues { get; set; }
        public GradingFactorGroupDetailStatus Status { get; set; }

        public GradingFactorGroupDetailBLL()
        {
            this.Id = Guid.Empty;
            this.GradingFactorGroupId = null;
            this.GradingFactorId = null;
            this.GradingTypeId = null;
            this.FailPoint = "";
            this.isMax = null;
            this.isInTotalValue = null;
            this.PossibleValues = "N/A";

        }

        public bool save(List<GradingFactorGroupDetailBLL> list, SqlTransaction tran, Guid GroupId)
        {
            bool isSaved = false;


            isSaved = GradingFactorGroupDetailDAL.save(list, tran, GroupId);
            if (isSaved == true)
            {
                isSaved = false;
                //Create audit Trail 
                int At = -1;
                AuditTrailBLL objAt = new AuditTrailBLL();
                string Str = "New Id - [";
                foreach(GradingFactorGroupDetailBLL o in list )
                {
                    Str += o.Id.ToString() + " , " ;
                }
                Str += " ] ";
                At = objAt.saveAuditTrailStringFormat("New Record-Grading Factor Group detail", Str, WFStepsName.GradingFactorGroupAd.ToString(), UserBLL.GetCurrentUser(), "Grading Factor Detail");
                if (At == 1)
                {
                    isSaved = true;
                }
                else
                {
                    isSaved = false;
                }

            }
            return isSaved;
        }


       
    }
    public enum CommodityGradingFactorStatus { Active = 1, Cancelled }
    [Serializable]
    public class CommodityGradingFactorBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid CommodityId { get; set; }
        public Guid GradingFactorGroupId { get; set; }
        public CommodityGradingFactorStatus Status { get; set; }
        public bool isForCommodity { get; set; }
        public string GroupName { get; set; }

        public bool Save()
        {
            bool isSaved = false;
            SqlTransaction tran= null;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = CommodityGradingFactorDAL.Save(this, tran);
                if (isSaved == true)
                {
                    isSaved = false;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    int at = -1;
                    at = objAt.saveAuditTrail(this, WFStepsName.CommmodityGradingFactor.ToString(), UserBLL.GetCurrentUser(), " Add Comm. Grading Factors ");
                    if (at == 1)
                    {
                        isSaved = true;
                    }
                   
                }
                if (isSaved == true)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                return isSaved;

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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public List<CommodityGradingFactorBLL> GetByCommodityId(Guid CommodityId)
        {
            return CommodityGradingFactorDAL.GetByCommodityId(CommodityId);
        }
        public bool Inactive(Guid Id )
        {
             bool isSaved = false;
            SqlTransaction tran= null;
            SqlConnection conn = null; 
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            try
            {
               
               
                isSaved = CommodityGradingFactorDAL.Inactive(Id ,tran ) ;
                if (isSaved == true)
                {
                    int At = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    At = objAt.saveAuditTrailStringFormat("", Id.ToString() + " - Staus set to Canceled", WFStepsName.CommmodityGradingFactor.ToString(), UserBLL.GetCurrentUser(), "Cancel Comm. Grading factor");
                    if (At == 1)
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
                    tran.Rollback();
                }
                if (isSaved == true)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                return isSaved;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                
            }
            
        }
    }
}
