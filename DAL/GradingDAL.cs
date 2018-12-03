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
    public class GradingDAL
    {
        public static bool InsertGrading(GradingBLL obj , SqlTransaction trans)
        {
            string strSql = "spInsertGrading";
            //GradingDate
            SqlParameter[] arPar = new SqlParameter[8];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@SamplingResultId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.SamplingResultId;

            arPar[2] = new SqlParameter("@CommodityRecivingId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = obj.CommodityRecivingId;

            arPar[3] = new SqlParameter("@GradingCode", SqlDbType.NVarChar,50);
            arPar[3].Value = obj.GradingCode;

            arPar[4] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[4].Value = obj.Status;

            arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[5].Value = obj.CreatedBy;

            arPar[6] = new SqlParameter("@TransactionId", SqlDbType.NVarChar,50);
            arPar[6].Value = obj.TrackingNo;

            //GradingDate
            arPar[7] = new SqlParameter("@DateCoded", SqlDbType.DateTime);
            arPar[7].Value = obj.DateCoded;

            

            int AffectedRows = 0 ;
            AffectedRows = SqlHelper.ExecuteNonQuery(trans, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                throw new Exception("Adding Coding Record failed");
            }
        }
        public static bool UpdateSampleCodeReceived(Guid Id, bool IsCodeReceivedAtLab, DateTime CodeReceivedTimeStamp, String LabTechRemark, SqlTransaction tran)
        {
          
            string strSql = "spUpdateSampleCodeReceived";
            int AffectedRows = 0;
            SqlParameter[] arPar = new SqlParameter[4];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            arPar[1] = new SqlParameter("@IsCodeReceivedAtLab", SqlDbType.Bit);
            arPar[1].Value = IsCodeReceivedAtLab;

            arPar[2] = new SqlParameter("@CodeReceivedTimeStamp", SqlDbType.DateTime);
            arPar[2].Value = CodeReceivedTimeStamp;

            arPar[3] = new SqlParameter("@LabTechRemark", SqlDbType.Text);
            arPar[3].Value = LabTechRemark;

            AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            return AffectedRows == 1;
        }

        public static List<GradingBLL> GetGradingsPendingResult(Guid WarehouseId)
        {
            string strSql = "spGetGradingCodesPendingResult";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            List<GradingBLL> list ;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    list.Add(obj);
                }
                conn.Close();
                return list;
            }
            else
            { 
                conn.Close();
                return null;
            }
            

        }

        public static List<GradingBLL> GetGradingsPendingResultByGradingId(Guid WarehouseId, Guid GradingId)
        {
            string strSql = "spGetGradingCodesPendingResultbyGradingId";
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;

            arPar[1] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = GradingId;

            List<GradingBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    list.Add(obj);
                }
                conn.Close();
                return list;
            }
            else
            {
                conn.Close();
                return null;
            }


        }


        public static List<GradingBLL> GetGradingsPendingResultByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            string strSql = "spGetGradingCodesPendingResultByTrackingId";
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@TransactionId", SqlDbType.NVarChar,50);
            arPar[1].Value = TrackingNo;
            List<GradingBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    list.Add(obj);
                }
                conn.Close();
                return list;
            }
            else
            {
                conn.Close();
                return null;
            }


        }
        public static List<GradingBLL> GetGradingsPendingCodeReceivingByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            string strSql = "spGetGradingCodesPendingCodeReceivingByTrackingId";
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[1].Value = TrackingNo;
            List<GradingBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    list.Add(obj);
                }
                conn.Close();
                return list;
            }
            else
            {
                conn.Close();
                return null;
            }


        }

        public static GradingBLL GetGradingById(Guid Id)
        {
            string strSql = "spGetGradingbyId";
            GradingBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingBLL();
                    if (reader.Read())
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.SamplingResultId = new Guid(reader["SamplingResultId"].ToString());
                        obj.CommodityRecivingId = new Guid(reader["CommodityRecivingId"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        obj.DateCoded = Convert.ToDateTime(reader["DateCoded"].ToString());
                        obj.Status = (GradingStatus)Convert.ToInt32(reader["status"]);
                        obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                        obj.CreatedTimestamp = Convert.ToDateTime(reader["CreatedTimestamp"].ToString());
                        try
                        {
                            obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
                            obj.LastModifiedTimestamp = Convert.ToDateTime(reader["LastModifiedTimestamp"].ToString());
                        }
                        catch
                        {
                        }
                        obj.TrackingNo = reader["TransactionId"].ToString();
                       
                    }
                    conn.Close();
                    return obj;
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
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
        }

        public static List<GradingBLL> GetGradingBySamplingResultId(Guid SamplingResultId)
        {

            string strSql = "spGetGradingBySamplingResultId";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@SamplingResultId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = SamplingResultId;
            List<GradingBLL> list ;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    obj.DateCoded = DateTime.Parse(reader["DateCoded"].ToString());
                    obj.TrackingNo = reader["TransactionId"].ToString();

                    list.Add(obj);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public static List<GradingBLL> GetGradingsPendingDispute(Guid WarehouseId)
        {

            string strSql = "spGetGradingCodesWithClientRejection";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            List<GradingBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    obj.TrackingNo = reader["TransactionId"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public static String[] GetGradingCodeBylistTrackingNo(string TrackingNo)
        {
            string strSql = "Select GradingCode,TransactionId from tblGrading  where TransactionId in (" + TrackingNo + ") order by GradingCode desc";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["GradingCode"].ToString() + " * " + reader["TransactionId"].ToString());
                }
                return list.ToArray();
            }
            return null;

        }
        public static String[] GetGradingCodeBylistGRNEditTrackingNo(string TrackingNo)
        {
            string strSql = "Select GradingCode,TransactionId from tblGrading  where TransactionId in (" + TrackingNo + ") order by GradingCode desc";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["GradingCode"].ToString() + " * " + reader["TransactionId"].ToString());
                }
                return list.ToArray();
            }
            return null;

        }
        public static List<GradingBLL> Search(String TrackingNo, string GradingCode, string SamplingResultCode, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            string strWhere = "";
            string strSql = "select tblGrading.Id,tblGrading.SamplingResultId,tblGrading.CommodityRecivingId,tblGrading.GradingCode,tblGrading.DateCoded,tblGrading.Status,tblGrading.CreatedBy,tblGrading.CreatedTimestamp,tblGrading.LastModifiedBy,tblGrading.LastModifiedTimestamp,tblGrading.TransactionId ";
            strSql += " from tblGrading  ";
            strSql += " inner join tblSamplingResult on tblSamplingResult.Id = tblGrading.SamplingResultId where ";
            if (string.IsNullOrEmpty(TrackingNo) != true)
            {
                strWhere += " TransactionId='" + TrackingNo + "'"; 
            }
            if (string.IsNullOrEmpty(GradingCode) != true)
            {
                if (strWhere == "")
                {
                    strWhere += " GradingCode='" + GradingCode + "'";
                }
                else
                {
                    strWhere += " or GradingCode='" + GradingCode + "'";
                }
            }
            if (string.IsNullOrEmpty(SamplingResultCode) != true)
            {
                if (strWhere == "")
                {
                    strWhere += " SamplingResultCode='" + SamplingResultCode + "'";
                }
                else
                {
                    strWhere += " or SamplingResultCode='" + SamplingResultCode + "'";
                }
            }
            if (from != null && to != null)
            {
                if (strWhere == "")
                {
                    strWhere += " ( DateCoded >='" + from + "' and  DateCoded <='" + to + "') ";
                }
                else
                {
                    strWhere += " or ( DateCoded >='" + from + "' and  DateCoded <='" + to + "') ";
                }
            }
            else if (from != null && to == null)
            {
                if (strWhere == "")
                {
                    strWhere += " ( DateCoded >='" + from + "' ) ";
                }
                else
                {
                    strWhere += " or ( DateCoded >='" + from + "' )";
                }
            }
            else if (from == null && to != null)
            {
                if (strWhere == "")
                {
                    strWhere += " ( DateCoded <='" + to + "' ) ";
                }
                else
                {
                    strWhere += " or ( DateCoded <='" + to + "' )";
                }
            }
            if (strWhere == "")
            {
                throw new NULLSearchParameterException("Please Provide search criteria.");

            }
            else
            {
                strSql = strSql + strWhere;
            }
            return SearchHelper(strSql);
            
        }
        private static List<GradingBLL> SearchHelper(string strSql )
        {
            SqlDataReader reader;
            List<GradingBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                list = new List<GradingBLL>();
                while (reader.Read())
                {
                    GradingBLL obj = new GradingBLL();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["SamplingResultId"] != DBNull.Value)
                    {
                        obj.SamplingResultId = new Guid(reader["SamplingResultId"].ToString());
                    }
                    if (reader["CommodityRecivingId"] != DBNull.Value)
                    {
                        obj.CommodityRecivingId = new Guid(reader["CommodityRecivingId"].ToString());
                    }
                    if (reader["GradingCode"] != DBNull.Value)
                    {
                        obj.GradingCode = reader["GradingCode"].ToString();
                    }
                    if (reader["DateCoded"] != DBNull.Value)
                    {
                        obj.DateCoded = DateTime.Parse(reader["DateCoded"].ToString());
                    }
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (GradingStatus)(int.Parse(reader["Status"].ToString()));
                    }
                    if (reader["CreatedBy"] != DBNull.Value)
                    {
                        obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                    }
                    if (reader["CreatedTimestamp"] != DBNull.Value)
                    {
                        obj.CreatedTimestamp = DateTime.Parse(reader["CreatedTimestamp"].ToString());
                    }
                    if (reader["LastModifiedBy"] != DBNull.Value)
                    {
                        obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
                    }
                    if (reader["LastModifiedTimestamp"] != DBNull.Value)
                    {
                        obj.DateCoded = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                    }
                    obj.TrackingNo = reader["TransactionId"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            else
            {
                return null;
            }

        }
    }
}
