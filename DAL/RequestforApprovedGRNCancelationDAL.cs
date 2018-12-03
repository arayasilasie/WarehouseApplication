using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class RequestforApprovedGRNCancelationDAL
    {
        public static bool save(SqlTransaction tran, RequestforApprovedGRNCancelationBLL obj)
        {

            try
            {
                int AffectedRows = 0;

                string strSql = "spInsertRequestApprovedGRNCancelation";
                SqlParameter[] arPar = new SqlParameter[8];

                arPar[0] = new SqlParameter("@GRNId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.GRNId;

                arPar[1] = new SqlParameter("@RequestedBy", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.RequestedBy;

                arPar[2] = new SqlParameter("@DateRequested", SqlDbType.DateTime);
                arPar[2].Value = obj.DateRequested;

                arPar[3] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[3].Value = obj.Remark;

                arPar[4] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[4].Value = (int)obj.Status;

                arPar[5] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
                arPar[5].Value = obj.TrackingNo;

                arPar[6] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[6].Value = UserBLL.GetCurrentUser();

                arPar[7] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[7].Value = obj.Id;

                AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (AffectedRows == 1)
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
                throw new Exception("Unable to Insert Data ", ex);

            }

        }
        public static bool Update(SqlTransaction tran, RequestforApprovedGRNCancelationBLL obj)
        {
            try
            {
                int AffectedRows = 0;

                string strSql = "spUpdateRequestApprovedGRNCancelation";
                SqlParameter[] arPar = new SqlParameter[5];

                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;


                arPar[1] = new SqlParameter("@DateRequested", SqlDbType.DateTime);
                arPar[1].Value = obj.DateRequested;

                arPar[2] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[2].Value = obj.Remark;

                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)obj.Status;

                arPar[4] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();

                AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (AffectedRows == 1)
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
                throw new Exception("Unable to Insert Sampling Data ", ex);

            }

        }
        public static List<RequestforApprovedGRNCancelationBLL> Search(string GRNNo, string TrackingNo, Nullable<RequestforEditGRNStatus> status, Nullable<DateTime> from, Nullable<DateTime> to)
        {

            List<RequestforApprovedGRNCancelationBLL> list = null;
            string strWhere = " where ";
            string strSql = "select tblRequestApprovedGRNCancelation.Id,tblRequestApprovedGRNCancelation.GRNId, ";
            strSql += " tblRequestApprovedGRNCancelation.RequestedBy,tblRequestApprovedGRNCancelation.DateRequested,  tblGRN.GRN_Number, ";
            strSql += " tblRequestApprovedGRNCancelation.Remark,tblRequestApprovedGRNCancelation.Status,tblRequestApprovedGRNCancelation.TrackingNo ";
            strSql += " from tblRequestApprovedGRNCancelation ";
            strSql += " inner join tblGRN on tblRequestApprovedGRNCancelation.GRNId = tblGRN.Id ";
            if (GRNNo != null && GRNNo != "")
            {
                strWhere += " tblGRN.GRN_Number='" + GRNNo.ToString() + "' ";
            }
            if (TrackingNo != null && TrackingNo != "")
            {
                if (strWhere == " where ")
                {
                    strWhere += " tblRequestApprovedGRNCancelation.TrackingNo='" + TrackingNo.ToString() + "' ";
                }
                else
                {
                    strWhere += " or tblRequestApprovedGRNCancelation.TrackingNo='" + TrackingNo.ToString() + "' ";
                }
            }
            if (status != null)
            {
                if (strWhere == " where ")
                {
                    strWhere += " tblRequestApprovedGRNCancelation.Status=" + ((int)status).ToString() + " ";
                }
                else
                {
                    strWhere += " or tblRequestApprovedGRNCancelation.Status=" + ((int)status).ToString() + " ";
                }
            }
            if (from != null && to != null)
            {
                if (strWhere == " where ")
                {
                    strWhere += " ( tblRequestApprovedGRNCancelation.DateRequested =>'" + from.ToString() + "' ";
                    strWhere += " and  tblRequestApprovedGRNCancelation.DateRequested <='" + to.ToString() + "' ) ";
                }
                else
                {
                    strWhere += " or ( tblRequestApprovedGRNCancelation.DateRequested =>'" + from.ToString() + "' ";
                    strWhere += " and  tblRequestApprovedGRNCancelation.DateRequested <='" + to.ToString() + "' ) ";

                }
            }
            else if (from != null && to == null)
            {
                if (strWhere == " where ")
                {
                    strWhere += " tblRequestApprovedGRNCancelation.DateRequested =>'" + from.ToString() + "' ";
                }
                else
                {
                    strWhere += " or tblRequestApprovedGRNCancelation.DateRequested =>'" + from.ToString() + "' ";
                }
            }
            else if (from == null && to != null)
            {
                if (strWhere == " where ")
                {
                    strWhere += " tblRequestApprovedGRNCancelation.DateRequested <='" + to.ToString() + "' ";
                }
                else
                {
                    strWhere += " or tblRequestApprovedGRNCancelation.DateRequested <='" + to.ToString() + "' ";
                }
            }
            strSql = strSql + " " + strWhere;
            list = SearchHelper(strSql);
            return list;
        }
        private static List<RequestforApprovedGRNCancelationBLL> SearchHelper(string strSql)
        {
            List<RequestforApprovedGRNCancelationBLL> list;
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                list = new List<RequestforApprovedGRNCancelationBLL>();
                while (reader.Read())
                {

                    RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["GRNId"] != DBNull.Value)
                    {
                        obj.GRNId = new Guid(reader["GRNId"].ToString());
                    }
                    if (reader["RequestedBy"] != DBNull.Value)
                    {
                        obj.RequestedBy = new Guid(reader["RequestedBy"].ToString());
                    }
                    if (reader["DateRequested"] != DBNull.Value)
                    {
                        obj.DateRequested = DateTime.Parse((reader["DateRequested"].ToString()));
                    }
                    obj.Remark = reader["Remark"].ToString();
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (RequestforApprovedGRNCancelationStatus)(int.Parse(reader["Status"].ToString()));
                    }
                    obj.TrackingNo = reader["Remark"].ToString();


                    if (reader["TrackingNo"] != DBNull.Value)
                    {
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                    }
                    if (reader["GRN_Number"] != DBNull.Value)
                    {
                        obj.GRN_Number = reader["GRN_Number"].ToString();
                    }
                    
                    list.Add(obj);

                }
                if (conn.State == ConnectionState.Open)
                        conn.Close();
                return list;
            }
            else
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return null;
            }
        }
        public static RequestforApprovedGRNCancelationBLL GetById(Guid Id)
        {
            string strSql = "spGetRequestApprovedGRNCancelationById";
            SqlDataReader reader = null;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
                        if (reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRNId"] != DBNull.Value)
                        {
                            obj.GRNId = new Guid(reader["GRNId"].ToString());
                        }
                        if (reader["RequestedBy"] != DBNull.Value)
                        {
                            obj.RequestedBy = new Guid(reader["RequestedBy"].ToString());
                        }
                        if (reader["DateRequested"] != DBNull.Value)
                        {
                            obj.DateRequested = DateTime.Parse((reader["DateRequested"].ToString()));
                        }
                        obj.Remark = reader["Remark"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = (RequestforApprovedGRNCancelationStatus)(int.Parse(reader["Status"].ToString()));
                        }
                        obj.TrackingNo = reader["Remark"].ToString();


                        if (reader["TrackingNo"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["GRN_Number"] != DBNull.Value)
                        {
                            obj.GRN_Number = reader["GRN_Number"].ToString();
                        }

                        return obj;
                    }
                    return null;
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }
        public static RequestforApprovedGRNCancelationBLL GetByTrackingNo(string TrackingNo)
        {
            string strSql = "spGetRequestApprovedGRNCancelationByTrackingNo";
            SqlDataReader reader = null;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@TrackingNo", SqlDbType.VarChar, 50);
            arPar[0].Value = TrackingNo;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
                        if (reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRNId"] != DBNull.Value)
                        {
                            obj.GRNId = new Guid(reader["GRNId"].ToString());
                        }
                        if (reader["RequestedBy"] != DBNull.Value)
                        {
                            obj.RequestedBy = new Guid(reader["RequestedBy"].ToString());
                        }
                        if (reader["DateRequested"] != DBNull.Value)
                        {
                            obj.DateRequested = DateTime.Parse((reader["DateRequested"].ToString()));
                        }
                        obj.Remark = reader["Remark"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = (RequestforApprovedGRNCancelationStatus)(int.Parse(reader["Status"].ToString()));
                        }
                        obj.TrackingNo = reader["Remark"].ToString();


                        if (reader["TrackingNo"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["GRN_Number"] != DBNull.Value)
                        {
                            obj.GRN_Number = reader["GRN_Number"].ToString();
                        }

                        return obj;
                    }
                    return null;
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }
    }
}
