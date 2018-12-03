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
    public class GradingByDAL 
    {
        public static bool InsertGraders(GradingByBLL obj , SqlTransaction tran )
        {
            string strSql = "spInsertGrader";

            SqlParameter[] arPar = new SqlParameter[6];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.GradingId;

            arPar[2] = new SqlParameter("@UserId", SqlDbType.NVarChar, 50);
            arPar[2].Value = obj.UserId;

            arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[3].Value = obj.Status;

            arPar[4] = new SqlParameter("@isSupervisor", SqlDbType.Bit);
            arPar[4].Value = obj.IsSupervisor;

            arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[5].Value = obj.CreatedBy;

            int AffectedRows = 0;
            AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                throw new Exception("Adding Grader Record failed");
            }
        }
        public static List<GradingByBLL> GetSupervisorByGradingId(Guid Id)
        {
             List<GradingByBLL> list;
             string strSql = "spGetSupervisorGraderByGradingId";
             SqlParameter[] arPar = new SqlParameter[1];
             SqlDataReader reader;
             arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
             arPar[0].Value = Id;
             SqlConnection conn = Connection.getConnection();
             if (conn == null || conn.State != ConnectionState.Open)
             {
                 throw new Exception("Invalid database connection.");
             }
             try
             {
                 reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                 if (reader.HasRows)
                 {
                     list = new List<GradingByBLL>();
                     while (reader.Read())
                     {
                         GradingByBLL obj = new GradingByBLL();
                         obj.Id = new Guid(reader["Id"].ToString());
                         obj.GradingId = new Guid(reader["GradingId"].ToString());
                         obj.UserId = new Guid(reader["UserId"].ToString());
                         obj.Status = int.Parse(reader["Status"].ToString());
                         obj.IsSupervisor = bool.Parse(reader["isSupervisor"].ToString());
                         if (reader["CreatedBy"] != null)
                         {
                             try
                             {
                                 obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                             }
                             catch
                             {
                             }
                         }
                         if (reader["CreatedTimestamp"] != null)
                         {
                             try
                             {
                                 obj.CreatedTimestamp = DateTime.Parse(reader["CreatedTimestamp"].ToString());
                             }
                             catch
                             {
                             }
                         }
                         if (reader["LastModifiedBy"] != null)
                         {
                             try
                             {
                                 obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
                             }
                             catch
                             {
                             }
                         }
                         if (reader["LastModifiedTimestamp"] != null)
                         {
                             try
                             {
                                 obj.LastModifiedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                             }
                             catch
                             {
                             }
                         }


                         list.Add(obj);
                     }
                     return list;
                 }
                 else
                 {
                     return null;
                 }
             }
             catch (Exception e)
             {
                 throw e;
             }
             finally
             {
                 if (conn != null)
                 {
                     if (conn.State == ConnectionState.Open)
                         conn.Close();
                 }
             }

        }
        public static SqlDataReader GetGradersByGradingIdDataReader(Guid Id, SqlConnection conn)
        {

            string strSql = "spGetGradersByGradingId";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                return reader;
            }
            catch (Exception e)
            {
                throw e;
            }
            
            
           
        }
        
    }
}
