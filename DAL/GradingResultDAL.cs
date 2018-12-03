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
    public class GradingResultDAL
    {
        public static Nullable<Guid> InsertGradingResult(GradingResultBLL obj, SqlTransaction tran)
        {

            string strSql = "spInsertGradingResult";
            SqlParameter[] arPar = new SqlParameter[10];
            try
            {
                arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.GradingId;

                arPar[1] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.CommodityGradeId;

                arPar[2] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[2].Value = (int)obj.Status;

                arPar[3] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[3].Value = obj.Remark;

                arPar[4] = new SqlParameter("@GradeRecivedTimestamp", SqlDbType.DateTime);
                arPar[4].Value = obj.GradeRecivedTimeStamp;

                arPar[5] = new SqlParameter("@isSupervisor", SqlDbType.Bit);
                arPar[5].Value = obj.IsSupervisor;

                arPar[6] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[6].Value = obj.CreatedBy;

                arPar[7] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[7].Value = obj.WarehouseId;

                arPar[8] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                arPar[8].Value = obj.ID;

                arPar[9] = new SqlParameter("@ProductionYear", SqlDbType.Int);
                arPar[9].Value = obj.ProductionYear;




                Nullable<Guid> id = null;
                id = new Guid(SqlHelper.ExecuteScalar(tran, strSql, arPar).ToString());
                if (id != null)
                {

                    return id;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Can not insert Grading Result", ex);

            }

        }

        public static bool UpdateGradingResult(GradingResultBLL obj, SqlTransaction tran)
        {
            int AffectedRows = 0;
            string strSql = "spUpdateGradingResult";

            SqlParameter[] arPar = new SqlParameter[8];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.ID;

                arPar[1] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.CommodityGradeId;

                arPar[2] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[2].Value = obj.Status;

                arPar[3] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[3].Value = obj.Remark;

                arPar[4] = new SqlParameter("@GradeRecivedTimestamp", SqlDbType.DateTime);
                arPar[4].Value = obj.GradeRecivedTimeStamp;

                arPar[5] = new SqlParameter("@isSupervisor", SqlDbType.Bit);
                arPar[5].Value = obj.IsSupervisor;

                arPar[6] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[6].Value = obj.LastModifiedBy;

                arPar[7] = new SqlParameter("@ProductionYear", SqlDbType.Int);
                arPar[7].Value = obj.ProductionYear;


                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);

                return AffectedRows == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool InactivateGradingResultDetails(Guid GradingResultId, SqlTransaction tran)
        {
            int AffectedRows = 0;
            string strSql = "spInactivateGradingResultDetailByGradingResultId";
            SqlConnection conn = Connection.getConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            try
            {
                arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = GradingResultId;
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int GetNumberofSupervisorResults(Guid Id)
        {
            int count = 0;
            string strSql = "spGetNumberOfSupervisorGradingResults";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = Connection.getConnection();
                count = (int)SqlHelper.ExecuteScalar(Connection.getConnection(), strSql, arPar);
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
            return count;
        }
        public static List<GradingResultBLL> GetGradingResultSearch(String TrackingNo, string GradingCode)
        {
            string strSql = "spSearchGradingResult";
            SqlConnection conn = new SqlConnection();
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[3];
            arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[0].Value = TrackingNo;

            arPar[1] = new SqlParameter("@GradingCode", SqlDbType.NVarChar, 50);
            arPar[1].Value = GradingCode;

            arPar[2] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = UserBLL.GetCurrentWarehouse();

            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.TrackingNo = reader["TransactionId"].ToString();
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);
                        list.Add(obj);
                    }
                    return list;
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
            }

        }
        public static GradingResultBLL GetGradingResultByGradingId(Guid Id)
        {
            string strSql = "spGetGradingResultByGradingId";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["status"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.TrackingNo = reader["TransactionId"].ToString();
                        obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);
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
        public static GradingResultBLL GetClientRejectedGradingResultByGradingId(Guid Id)
        {
            string strSql = "spGetGradingClientRejectedResultByGradingId";
            GradingResultBLL obj;
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
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["Remark"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["Status"]);

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
        public static List<GradingResultBLL> GetGradingResultPendinngResult(Guid WarehouseId)
        {
            string strSql = "spGetGradingResultsPendingClientAcceptance";
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.NVarChar, 50);
            arPar[0].Value = WarehouseId;
            SqlConnection conn = Connection.getConnection();
            try
            {
                reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        list.Add(obj);
                    }
                    return list;
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

        }
        public static GradingResultBLL GetGradingResultById(Guid Id)
        {
            string strSql = "spGetGradingResultById";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingId = new Guid(reader["GradingId"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["Remark"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
                        if (reader["ClientAcceptanceTimeStamp"] != null)
                        {
                            try
                            {
                                obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                            }
                            catch
                            {

                            }
                        }
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);



                        obj.TrackingNo = reader["TransactionId"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        try
                        {
                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        catch
                        {

                        }
                        obj.GradingCode = reader["GradingCode"].ToString();
                        if (reader["ProductionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = Convert.ToInt32(reader["ProductionYear"].ToString());
                        }
                        else
                        {
                            obj.ProductionYear = -1;
                        }

                    }
                    reader.Close();
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

        }
        public static GradingResultBLL GetGradingResultByTrackingNo(string TrackingNo)
        {
            string strSql = "spGetGradingResultByTrackingNo";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
            arPar[0].Value = TrackingNo;
            arPar[1] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentWarehouse();
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingId = new Guid(reader["GradingId"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["status"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
                        if (reader["ClientAcceptanceTimeStamp"] != null)
                        {
                            try
                            {
                                obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                            }
                            catch
                            {
                            }
                        }
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);




                        obj.TrackingNo = reader["TransactionId"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        try
                        {
                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        catch
                        {

                        }
                        obj.GradingCode = reader["GradingCode"].ToString();

                    }
                    reader.Close();
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


        }
        public static GradingResultBLL GetGradingResultById(Guid Id, SqlTransaction tran)
        {
            string strSql = "spGetGradingResultById";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                reader = SqlHelper.ExecuteReader(tran, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingId = new Guid(reader["GradingId"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["status"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
                        if (reader["ClientAcceptanceTimeStamp"] != DBNull.Value)
                        {

                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());

                        }

                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);


                        obj.TrackingNo = reader["TransactionId"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        if (reader["ClientAcceptanceTimeStamp"] != DBNull.Value)
                        {
                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        obj.GradingCode = reader["GradingCode"].ToString();

                    }
                    reader.Close();
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



        }
        public static List<GradingResultBLL> GetAccpetedResultsPendingUnloading(Guid WarehouseId)
        {
            string strSql = "spGetClientAcceptedResultsPendingUnloading";
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            SqlConnection conn = new SqlConnection();
            arPar[0].Value = WarehouseId;
            try
            {
                reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();

                        list.Add(obj);
                    }
                    reader.Close();
                    reader.Dispose();
                    return list;
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

        }
        public static List<GradingResultBLL> GetAccpetedResultsPendingUnloadingByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            string strSql = "spGetClientAcceptedResultsPendingUnloadingByTrackingNo";
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
            arPar[1].Value = TrackingNo;
            SqlConnection conn = Connection.getConnection();
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();

                        list.Add(obj);
                    }
                    reader.Close();
                    reader.Dispose();
                    return list;
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

        }
        public static List<GradingResultBLL> GetAccpetedResultsPendingScaling(Guid WarehouseId)
        {
            string strSql = "spGetGradingResultsPendingScaling";
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            SqlConnection conn = Connection.getConnection();
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositeRequest"].ToString());
                        list.Add(obj);
                    }
                    return list;
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

        }
        public static List<GradingResultBLL> GetAccpetedResultsPendingScalingByGradingCode(Guid WarehouseId, string GradingCode)
        {
            string strSql = "spspGetGradingResultsPendingScalingByGradingCode";
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@GradingCode", SqlDbType.NVarChar, 50);
            arPar[1].Value = GradingCode;
            SqlConnection conn = Connection.getConnection();
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositeRequest"].ToString());
                        list.Add(obj);
                    }
                    return list;
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

        }
        public static List<GradingResultBLL> GetAccpetedResultsPendingScalingByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            string strSql = "spGetGradingResultsPendingScalingByTrackingNo";
            List<GradingResultBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
            arPar[1].Value = TrackingNo;
            SqlConnection conn = Connection.getConnection();
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<GradingResultBLL>();
                    while (reader.Read())
                    {
                        GradingResultBLL obj = new GradingResultBLL();
                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositeRequest"].ToString());
                        list.Add(obj);
                    }
                    return list;
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

        }
        public static GradingResultBLL GetApprovedGradingResultByCommdityDepositerequest(Guid Id)
        {
            string strSql = "GetApprovedGradingResultByCommdityDepositerequest";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        obj.GradingId = new Guid(reader["GradingId"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["status"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.TrackingNo = reader["TransactionId"].ToString();
                        try
                        {
                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        catch
                        {
                        }
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
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
        public static GradingResultBLL GetApprovedGradingResultByGradingId(Guid Id)
        {
            string strSql = "spGetApprovedGradingResultByGradingId";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingCode = reader["GradingCode"].ToString();
                        obj.GradingId = new Guid(reader["GradingId"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["status"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.TrackingNo = reader["TransactionId"].ToString();
                        if (reader["ProductionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = int.Parse(reader["ProductionYear"].ToString());
                        }
                        try
                        {
                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        catch
                        {
                        }
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
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
        public static bool ClientAcceptanceGradingResult(GradingResultBLL obj, SqlTransaction tran)
        {
            int AffectedRows = 0;
            string strSql = "spGradingResultClientAccptance";
            SqlParameter[] arPar = new SqlParameter[7];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.ID;
                arPar[1] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[1].Value = obj.Status;
                arPar[2] = new SqlParameter("@ClientAcceptanceTimeStamp", SqlDbType.DateTime);
                arPar[2].Value = obj.ClientAcceptanceTimeStamp;
                arPar[3] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[3].Value = UserBLL.GetCurrentUser();
                arPar[4] = new SqlParameter("@PreWeightQueueNo", SqlDbType.Int);
                arPar[4].Value = obj.PreWeightQueueNo;
                arPar[5] = new SqlParameter("@QueueNo", SqlDbType.Int);
                arPar[5].Value = obj.QueueNo;
                arPar[6] = new SqlParameter("@QueueDate", SqlDbType.DateTime);
                arPar[6].Value = obj.QueueDate;


                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
                return AffectedRows == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void GetQueueNumber(string QueueCode, Guid warehouseId, DateTime QueueDateTime, out int? QueueNo, out int? PreWeightQueueNo)
        {
            QueueNo = null;
            PreWeightQueueNo = null;
            string strSql = "spGetGRNQueueNumber";
            SqlParameter[] arPar = new SqlParameter[3];
            SqlDataReader reader;
            try
            {
                arPar[0] = new SqlParameter("@QueueCode", SqlDbType.NVarChar, 4);
                arPar[0].Value = QueueCode;
                arPar[1] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = warehouseId;
                arPar[2] = new SqlParameter("@QueueDateTime", SqlDbType.DateTime);
                arPar[2].Value = QueueDateTime;
                SqlConnection conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        QueueNo = Convert.ToInt32(reader["QueueNo"].ToString());
                        PreWeightQueueNo = Convert.ToInt32(reader["PreWeightQueueNo"].ToString());
                    }
                    else
                    {
                        throw new Exception("Unable to ge queue information.");
                    }
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static String[] GetGradingResultCodeBylistTrackingNo(string TrackingNo)
        {
            string strSql = "Select PreWeightQueueNo,TransactionId from tblGradingResult ";
            strSql += " inner join tblGrading on tblGradingResult.GradingId=tblGrading.Id ";
            strSql += " where TransactionId in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["PreWeightQueueNo"].ToString() + " * " + reader["TransactionId"].ToString());
                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list.ToArray();
            }
            return null;

        }
        public static GradingResultBLL GetGradingResultByTrackingNoForGradeDispute(string TrackingNo)
        {
            string strSql = "spGetGradingResultByTrackingNoForGradeDispute";
            GradingResultBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
            arPar[0].Value = TrackingNo;
            arPar[1] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentWarehouse();
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingResultBLL();
                    if (reader.Read())
                    {


                        obj.ID = new Guid(reader["Id"].ToString());
                        obj.GradingId = new Guid(reader["GradingId"].ToString());
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        obj.Status = (GradingResultStatus)Convert.ToInt32(reader["status"]);
                        obj.Remark = reader["status"].ToString();
                        obj.GradeRecivedTimeStamp = Convert.ToDateTime(reader["GradeRecivedTimestamp"].ToString());
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.CommodityDepositRequestId = new Guid(reader["CommodityDepositRequest"].ToString());
                        if (reader["ClientAcceptanceTimeStamp"] != null)
                        {
                            try
                            {
                                obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                            }
                            catch
                            {
                            }
                        }
                        obj.GradingResult = (GradingResultStatus)Convert.ToInt32(reader["GradingResult"]);




                        obj.TrackingNo = reader["TransactionId"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        try
                        {
                            obj.ClientAcceptanceTimeStamp = Convert.ToDateTime(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        catch
                        {

                        }
                        obj.GradingCode = reader["GradingCode"].ToString();

                    }
                    reader.Close();
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


        }
    }
}
