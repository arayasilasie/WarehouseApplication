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
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class SamplerDAL
    {
        
        public static bool InsertSampler(SamplerBLL obj, SqlTransaction tran )
        {
            try
            {
                string strSql = "spInsertSampler";
                int AffectedRows = 0;
                SqlParameter[] arPar = new SqlParameter[5];

                arPar[0] = new SqlParameter("@SamplingTicketId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.SampleingTicketId;

                arPar[1] = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.SamplerId;

                arPar[2] = new SqlParameter("@Status", SqlDbType.UniqueIdentifier);
                arPar[2].Value = (int)obj.Status;

                arPar[3] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[3].Value = UserBLL.GetCurrentUser();
                arPar[4] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[4].Value = obj.Id;
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (AffectedRows == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }

          
            
        }

        public static List<SamplerBLL> GetSamplerBySamplingId(Guid SamplingId)
        {
            List<SamplerBLL> list;
            string strSql = "spGetSamplersBySamplingTicketId";
            SqlParameter[] arPar = new SqlParameter[4];
            arPar[0] = new SqlParameter("@SamplingTicketId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = SamplingId;
            SqlDataReader reader;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<SamplerBLL>();
                    while (reader.Read())
                    {
                        SamplerBLL objsampler = new SamplerBLL();
                        objsampler.SamplerId = new Guid(reader["UserId"].ToString());

                        list.Add(objsampler);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;



        }
        public static SamplerBLL GetActiveSamplerSupBySamplingId(Guid SamplingId)
        {
  
            string strSql = "spGetActiveSamplerBySamplingTicketId";
            SqlParameter[] arPar = new SqlParameter[4];
            arPar[0] = new SqlParameter("@SamplingTicketId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = SamplingId;
            SqlDataReader reader;
            SqlConnection conn = null;
            SamplerBLL objsampler = null;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                  if( reader.Read())
                  {
                        objsampler = new SamplerBLL();
                        if (reader["UserId"] != DBNull.Value)
                        {
                            objsampler.SamplerId = new Guid(reader["UserId"].ToString());
                        }
                        else
                        {
                            objsampler.SamplerId = Guid.Empty;
                        }

                  }   
                }
            if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return objsampler ;
            }
            
           



        }
    }
