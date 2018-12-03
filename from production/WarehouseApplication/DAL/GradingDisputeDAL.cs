using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;


namespace WarehouseApplication.DAL
{
    public class GradingDisputeDAL
    {
        public static bool InsertGradingDisputeBLL(GradingDisputeBLL obj, SqlTransaction tran)
        {
            string strSql = "spInsertGradeDispute";
            SqlParameter[] arPar = new SqlParameter[10];

            arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[0].Value = obj.TrackingNo;

            arPar[1] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.GradingId;

            arPar[2] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = obj.GradingResultId;

            arPar[3] = new SqlParameter("@PrevioudCommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[3].Value = obj.PreviousCommodityGradeId;

            arPar[4] = new SqlParameter("@ExpectedCommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[4].Value = obj.ExpectedCommodityGradeId;

            arPar[5] = new SqlParameter("@DateTimeRequested", SqlDbType.DateTime);
            arPar[5].Value = Convert.ToDateTime(obj.DateTimeRecived);

            arPar[6] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[6].Value = obj.Remark;

            arPar[7] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[7].Value = obj.Status;

            arPar[8] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[8].Value = UserBLL.GetCurrentUser();

            arPar[9] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[9].Value = obj.Id;

            int AffectedRows = 0;
            AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                throw new Exception("Adding Grading Dispute Failed.");
            }

        }
        public static int GetCountGradingDisputeCountByGradingId(Guid Id)
        {

            int count = 0;
            string strSql = "spGetGradingDisputeCountByGradingId";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                count = (int)SqlHelper.ExecuteScalar(conn, strSql, arPar);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
        public static GradingDisputeBLL GetById(Guid Id)
        {
            GradingDisputeBLL obj = new GradingDisputeBLL();
            string strSql = "spGetGradingDisputeById";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
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

                    reader.Read();
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.TrackingNo = reader["TransactionId"].ToString();
                    obj.GradingId = new Guid(reader["GradingId"].ToString());
                    obj.GradingResultId = new Guid(reader["GradingResultId"].ToString());
                    obj.PreviousCommodityGradeId = new Guid(reader["PrevioudCommodityGradeId"].ToString());
                    if (reader["ExpectedCommodityGradeId"] != DBNull.Value)
                    {
                        obj.ExpectedCommodityGradeId = new Guid(reader["ExpectedCommodityGradeId"].ToString());
                    }
                    if (reader["DateTimeRequested"] != DBNull.Value)
                    {
                        obj.DateTimeRecived = Convert.ToDateTime(reader["DateTimeRequested"].ToString());
                    }
                    obj.Remark = reader["Remark"].ToString();
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = Convert.ToInt32(reader["Status"].ToString());
                    }


                    if (reader["CreatedBy"] != DBNull.Value)
                    {
                        try
                        {
                            obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                        }
                        catch
                        {
                        }
                    }
                    if (reader["CreatedTimestamp"] != DBNull.Value)
                    {
                        try
                        {
                            obj.CreatedTimestamp = DateTime.Parse(reader["CreatedTimestamp"].ToString());
                        }
                        catch
                        {
                        }
                    }
                    if (reader["LastModifiedBy"] != DBNull.Value)
                    {
                        try
                        {
                            obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
                        }
                        catch
                        {
                        }
                    }
                    if (reader["LastModifiedTimestamp"] != DBNull.Value)
                    {
                        try
                        {
                            obj.LastModifiedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                        }
                        catch
                        {
                        }
                    }
                    return obj;

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
                    {
                        conn.Close();
                    }
                }
            }


        }
        public static GradingDisputeBLL GetByTranNo(string TranNo)
        {
            GradingDisputeBLL obj = new GradingDisputeBLL();
            string strSql = "spGetGradingDisputeByTrackinNo";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@TrackinNo", SqlDbType.NVarChar, 50);
            arPar[0].Value = TranNo;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (reader.HasRows)
            {

                reader.Read();
                obj.Id = new Guid(reader["Id"].ToString());
                obj.TrackingNo = reader["TransactionId"].ToString();
                obj.GradingId = new Guid(reader["GradingId"].ToString());
                obj.GradingResultId = new Guid(reader["GradingResultId"].ToString());
                obj.PreviousCommodityGradeId = new Guid(reader["PrevioudCommodityGradeId"].ToString());
                if (reader["ExpectedCommodityGradeId"] != DBNull.Value)
                {
                    obj.ExpectedCommodityGradeId = new Guid(reader["ExpectedCommodityGradeId"].ToString());
                }
                if (reader["DateTimeRequested"] != DBNull.Value)
                {
                    obj.DateTimeRecived = Convert.ToDateTime(reader["DateTimeRequested"].ToString());
                }
                obj.Remark = reader["Remark"].ToString();
                if (reader["Status"] != DBNull.Value)
                {
                    obj.Status = Convert.ToInt32(reader["Status"].ToString());
                }


                if (reader["CreatedBy"] != DBNull.Value)
                {
                    try
                    {
                        obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                    }
                    catch
                    {
                    }
                }
                if (reader["CreatedTimestamp"] != DBNull.Value)
                {
                    try
                    {
                        obj.CreatedTimestamp = DateTime.Parse(reader["CreatedTimestamp"].ToString());
                    }
                    catch
                    {
                    }
                }
                if (reader["LastModifiedBy"] != DBNull.Value)
                {
                    try
                    {
                        obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
                    }
                    catch
                    {
                    }
                }
                if (reader["LastModifiedTimestamp"] != DBNull.Value)
                {
                    try
                    {
                        obj.LastModifiedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
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
                conn.Close();
                return null;
            }

        }
        public static bool UpdateGradingDisputeBLL(GradingDisputeBLL obj, SqlTransaction tran)
        {
            string strSql = "spUpdateGradeDispute";
            SqlParameter[] arPar = new SqlParameter[6];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@ExpectedCommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.ExpectedCommodityGradeId;

            arPar[2] = new SqlParameter("@DateTimeRequested", SqlDbType.DateTime);
            arPar[2].Value = obj.DateTimeRecived;

            arPar[3] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[3].Value = obj.Remark;

            arPar[4] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[4].Value = obj.Status;

            arPar[5] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[5].Value = UserBLL.GetCurrentUser();
            int AffectedRows = 0;
            AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                throw new Exception("Updating Grading Dispute Failed.");
            }

        }
        public static List<SamplingResultBLL> GetGradingDisputePendingCoding(Guid WarehouseId, string SamplingCode)
        {
            string strSql = "spGetGradingDisputePendingCoding";
            SqlDataReader reader;
            List<SamplingResultBLL> list;
            //GradingDate
            SqlParameter[] arPar = new SqlParameter[2];
            SqlConnection conn = Connection.getConnection();
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;

            //GradingDate
            arPar[1] = new SqlParameter("@SamplingCode", SqlDbType.NVarChar, 50);
            arPar[1].Value = SamplingCode;

            try
            {
                reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<SamplingResultBLL>();
                    while (reader.Read())
                    {
                        SamplingResultBLL objSampling = new SamplingResultBLL();
                        objSampling.Id = new Guid(reader["SamplingResultId"].ToString());
                        objSampling.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                        objSampling.SamplingCode = long.Parse(reader["SampleCode"].ToString());
                        objSampling.TrackingNo = reader["TransactionId"].ToString();
                        objSampling.SamplingResultCode = reader["SamplingResultCode"].ToString();
                        //GradingDate
                        if (reader["SampleReturnedDateTime"] != DBNull.Value)
                        {
                            objSampling.ResultReceivedDateTime = DateTime.Parse(reader["SampleReturnedDateTime"].ToString());
                        }
                        else if(reader["SamplingResultCreatedDate"] != DBNull.Value )
                        {
                            objSampling.ResultReceivedDateTime = DateTime.Parse(reader["SamplingResultCreatedDate"].ToString());
                        }
                        else
                        {
                            objSampling.ResultReceivedDateTime = DateTime.Now;
                        }
                        list.Add(objSampling);
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }
        public static SamplingResultBLL GetGradingDisputePendingCodingByTrackingNo(string TrackingNo)
        {
            string strSql = "spGetGradingDisputePendingCodingByTrackingNo";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            SqlConnection conn = Connection.getConnection();
            arPar[0] = new SqlParameter("@Trackingno", SqlDbType.NVarChar, 50);
            arPar[0].Value = TrackingNo;
            arPar[1] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentWarehouse();
            try
            {
                reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {

                    reader.Read();
                    SamplingResultBLL objSampling = new SamplingResultBLL();
                    if (reader["SamplingResultId"] != DBNull.Value)
                    {
                        objSampling.Id = new Guid(reader["SamplingResultId"].ToString());
                    }
                    objSampling.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                    objSampling.SamplingCode = long.Parse(reader["SampleCode"].ToString());
                    objSampling.TrackingNo = reader["TransactionId"].ToString();
                    objSampling.SamplingResultCode = reader["SamplingResultCode"].ToString();
                    return objSampling;
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
        public static string GetTrackingNumberBySamplingResultId(Guid SamplingId)
        {
            GradingDisputeBLL obj = new GradingDisputeBLL();
            string strSql = "spGetGradingDisputeTrackingNo";
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@SamplingResultId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = SamplingId;
            arPar[1] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentWarehouse();
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (reader.HasRows)
            {
                reader.Read();

                return reader["TransactionId"].ToString();
            }
            else
            {
                return "";
            }

        }
    }
}
