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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;


namespace WarehouseApplication.DAL
{
    public class GradingResultDetailDAL
    {
        public static bool InsertGradingResultDetail(GradingResultDetailBLL obj, SqlTransaction trans)
        {
            if (obj != null)
            {
                int AffectedRows = 0;
                string strSql = "spInsertGradingResultDetail";
                SqlConnection conn = Connection.getConnection();
                SqlParameter[] arPar = new SqlParameter[6];

                arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.GradingResultId;

                arPar[1] = new SqlParameter("@GradingFactorId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.GradingFactorId;

                arPar[2] = new SqlParameter("@ReceivedValue", SqlDbType.NVarChar, 50);
                arPar[2].Value = obj.RecivedValue;

                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)obj.Status;

                arPar[4] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                arPar[4].Value = UserBLL.GetCurrentUser();

                arPar[5] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[5].Value = obj.Id;

                AffectedRows = SqlHelper.ExecuteNonQuery(trans, strSql, arPar);
                if (AffectedRows == 1)
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
        public static List<GradingResultDetailBLL> GetGradingResultDetailByGradingResultId(Guid Id)
        {
            string strSql = "spGetGradingResultDetailByGradingResultId";
            SqlConnection conn = new SqlConnection();
            List<GradingResultDetailBLL> list;
            try
            {

                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = Id;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        list = new List<GradingResultDetailBLL>();
                        while (reader.Read())
                        {
                            GradingResultDetailBLL obj = new GradingResultDetailBLL();
                            obj.Id = new Guid(reader["Id"].ToString());
                            obj.GradingFactorId = new Guid(reader["GradingFactorId"].ToString());
                            obj.RecivedValue = reader["ReceivedValue"].ToString();
                            obj.Status = (GradingResultDetailStatus)reader["Status"];
                            obj.GradingFactorName = reader["GradingFactorName"].ToString();
                            obj.DataType = reader["ValueType"].ToString();
                            obj.PossibleValues = reader["PossibleValues"].ToString();
                            if (reader["isInTotalValue"] != DBNull.Value)
                            {
                                obj.isInTotalValue = bool.Parse(reader["isInTotalValue"].ToString());
                            }
                            else
                            {
                                obj.isInTotalValue = false;
                            }

                            list.Add(obj);
                        }
                        return list;
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
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return null;


        }
        public static GradingResultDetailBLL GetGradingResultDetailById(Guid Id)
        {
            string strSql = "spGetGradingResultDetailById";
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {


                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    GradingResultDetailBLL obj = new GradingResultDetailBLL();
                    if (reader.Read())
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.GradingFactorId = new Guid(reader["GradingFactorId"].ToString());
                        obj.RecivedValue = reader["ReceivedValue"].ToString();
                        obj.Status = (GradingResultDetailStatus)reader["Status"];
                        obj.GradingFactorName = reader["GradingFactorName"].ToString();
                        obj.DataType = reader["ValueType"].ToString();
                        obj.PossibleValues = reader["PossibleValues"].ToString();
                        if (reader["isInTotalValue"] != DBNull.Value)
                        {
                            obj.isInTotalValue = bool.Parse(reader["isInTotalValue"].ToString());
                        }
                        else
                        {
                            obj.isInTotalValue = false;
                        }

                        try
                        {
                            obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
                            obj.LastModifiedTimestamp = Convert.ToDateTime(reader["LastModifiedTimestamp"].ToString());
                        }
                        catch
                        {
                        }


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
        public static bool UpdateGradingResultDetailEach(Guid Id, string value, SqlTransaction tran)
        {
            string strSql = "spUpdateGradingResultDetailValue";
            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            arPar[1] = new SqlParameter("@value", SqlDbType.NVarChar, 50);
            arPar[1].Value = value;

            arPar[2] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[2].Value = UserBLL.GetCurrentUser();

            int AffectedRows = 0;
            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                throw e;
            }
            return AffectedRows == 1;


        }
        public static List<GradingResultDetailBLL> GetGradingResultWithTotalValue(Guid Id)
        {
            string strSql = "spGetTotalValue";
            SqlConnection conn = new SqlConnection();
            List<GradingResultDetailBLL> list;
            try
            {

                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = Id;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        list = new List<GradingResultDetailBLL>();
                        while (reader.Read())
                        {
                            GradingResultDetailBLL obj = new GradingResultDetailBLL();
                            obj.Id = new Guid(reader["Id"].ToString());
                            obj.GradingFactorId = new Guid(reader["GradingFactorId"].ToString());
                            obj.RecivedValue = reader["ReceivedValue"].ToString();
                            obj.Status = (GradingResultDetailStatus)reader["Status"];
                            obj.GradingFactorName = reader["GradingFactorName"].ToString();
                            obj.DataType = reader["ValueType"].ToString();
                            obj.PossibleValues = reader["PossibleValues"].ToString();

                            list.Add(obj);
                        }
                        return list;
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
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return null;
        }
        public static SqlDataReader GetGradingResultDetailByGradingIdDataReader(Guid GradingId, ref  SqlConnection conn)
        {
            string strSql = "spGetApprovedGradingValueByGradingId";
            try
            {

                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = GradingId;
                SqlDataReader reader;
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {

                        return reader;
                    }
                    else
                    {
                        return null;
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
    }
}
