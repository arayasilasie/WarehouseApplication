using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public enum GRNServiceStatus { Active = 1, Cancelled }
    [Serializable]
    public class GRNServiceBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid GRNId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
        public GRNServiceStatus Status { get; set; }
        public string ServiceName { get; set;}

        public bool  Save (Guid GRNId ,List<GRNServiceBLL> list , SqlTransaction tran)
        {
            bool isSaved = false;
            int at = -1;
            try
            {
                isSaved = GRNServiceDAL.Insert(GRNId , list, tran);

                if (isSaved == true)
                {
                    isSaved = false;
                   
                    string strAt = "GRN service [";
                    foreach (GRNServiceBLL o in list)
                    {
                        strAt += "(Id=" + o.Id.ToString() + ", ";
                        strAt += "ServiceId=" + o.ServiceId.ToString() + ", ";
                        strAt += "Quantity=" + o.Quantity.ToString() + ", ";
                        strAt += "Status=" + o.Status.ToString() + " );";
                    }
                    strAt += "]";
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrailStringFormat(strAt,"New data Added", WFStepsName.GRNServiceAdd.ToString(), UserBLL.GetCurrentUser(), " Add GRN service ");
                    if (at != 1)
                    {
                       
                        isSaved = false;
                    }
                    else
                    {
                        
                        isSaved = true;
                    }
                }

            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally
            {
              
            }
                
            return isSaved;
        }
        public bool Save()
        {
            bool isSaved = false;
            int at = -1;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = GRNServiceDAL.Insert(this, tran);

                if (isSaved == true)
                {
                    isSaved = false;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrail(this, WFStepsName.GRNServiceAdd.ToString(), UserBLL.GetCurrentUser(), "Add GRN Service");
                    if (at == 1)
                    {
                        tran.Commit();
                        isSaved = true;
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }

            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                if( tran != null)
                    tran.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return isSaved;
        }
        public bool Cancel(Guid Id)
        {
            bool isSaved = false;
            SqlTransaction tran = null;
            SqlConnection conn = null;
            int at = -1;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                isSaved = GRNServiceDAL.Cancel(Id, tran);
                if (isSaved == true)
                {
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrailStringFormat("[Id=" + Id.ToString() + ",Status=Active]", "[Id=" + Id.ToString() + ",Status=Cancelled]", WFStepsName.GRNServiceCancel.ToString(), UserBLL.GetCurrentUser(), "Cancel GRN service");
                    if (at == 1)
                    {
                        tran.Commit();
                        isSaved = true;
                    }
                    else
                    {
                        tran.Rollback();
                        isSaved = false;
                    }
                }
                else
                {
                    tran.Rollback();
                    return false;
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
                    tran.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return isSaved;
        }
        public List<GRNServiceBLL> GetByGRNId(Guid Id)
        {
            List<GRNServiceBLL> list = null;
            List<WarehouseServicesBLL> listWS = null;
            try
            {
                list = GRNServiceDAL.GetByGRNId(Id);
                WarehouseServicesBLL objservice = new WarehouseServicesBLL();
                listWS = objservice.GetServices();
                if (list != null)
                {
                    listWS = objservice.GetActiveServices();
                    if (listWS != null)
                    {
                        var x = from grnser in list
                                join wservice in listWS on grnser.ServiceId equals wservice.Id
                                select new { grnser.Id, grnser.GRNId, grnser.Quantity, grnser.Status, grnser.ServiceId, ServiceName=wservice.Name };
                        list= new List<GRNServiceBLL>();
                        foreach( var i in x)
                        {
                            GRNServiceBLL o = new GRNServiceBLL();
                            o.Id = i.Id;
                            o.GRNId = i.GRNId;
                            o.ServiceId = i.ServiceId ;
                            o.ServiceName = i.ServiceName;
                            o.Status = i.Status;
                            o.Quantity = i.Quantity;
                            list.Add(o);
                           
                        }
                        return list;
                    }
                    else
                    {
                        throw new Exception("unable to get look up information.");
                    }
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
            
        }
        public SqlDataReader GetActiveByGRNId(Guid Id)
        {
            return GRNServiceDAL.GetActiveByGRNId(Id);
        }

    }
}
