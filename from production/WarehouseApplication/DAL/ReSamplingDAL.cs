using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class ReSamplingDAL
    {
        public static bool Insert(ReSamplingBLL obj, SqlTransaction trans)
        {
            string strSql = "spInsertResamplingRequest";
            SqlParameter[] arPar = new SqlParameter[9];
            try
            {
                arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
                arPar[0].Value = obj.TrackingNo;

                arPar[1] = new SqlParameter("@SamplingId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.SamplingId;

                arPar[2] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.ReceivigRequestId;

                arPar[3] = new SqlParameter("@SamplingResultId", SqlDbType.UniqueIdentifier);
                arPar[3].Value = obj.SamplingResultId;

                arPar[4] = new SqlParameter("@DateTimeRequested", SqlDbType.DateTime);
                arPar[4].Value = obj.DateTimeRequested;

                arPar[5] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[5].Value = (int)obj.Status;

                arPar[6] = new SqlParameter("@Remark", SqlDbType.Int);
                arPar[6].Value = obj.Remark;

                arPar[7] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                arPar[7].Value = obj.CreatedBy;

                arPar[8] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[8].Value = obj.Id;

                int AffectedRows = 0;

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
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static List<ReSamplingBLL> GetMoistureFailedSamples(Guid WarehouseId)
        {
            string strSql = "spGetMoistureFailedPendingResampling";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            List<ReSamplingBLL> list;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                if (conn == null || conn.State != ConnectionState.Open)
                {
                    throw new Exception("Invalid database connection.");
                }
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<ReSamplingBLL>();
                    while (reader.Read())
                    {
                        ReSamplingBLL obj = new ReSamplingBLL();
                        if (reader["SamplingResultId"] != DBNull.Value)
                        {
                            obj.SamplingResultId = new Guid(reader["SamplingResultId"].ToString());
                        }
                        if (reader["SamplingId"] != DBNull.Value)
                        {
                            obj.SamplingId = new Guid(reader["SamplingId"].ToString());
                        }
                        if (reader["SampleCode"] != DBNull.Value)
                        {
                            try
                            {
                                obj.SampleCode = int.Parse(reader["SampleCode"].ToString());
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
        public static int GetCountBySamplingResult(Guid Id)
        {
            int Count = -1;
            string strSql = "spGetMoistureResamplingRequestCountBySamplinResultId";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            arPar[0] = new SqlParameter("@SamplingResultId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                Count = (int)SqlHelper.ExecuteScalar(conn, strSql, arPar);
                return Count;
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
        public static ReSamplingBLL GetSamplingRelatedDataBySamplingResultId(Guid SamplingResultId)
        {

            ReSamplingBLL objSample = new ReSamplingBLL();
            SqlDataReader reader;
            string strSql = "spGetSamplingInfoBySamplingResultId";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = new SqlConnection();
            arPar[0] = new SqlParameter("@SamplingResultId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = SamplingResultId;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader["SamplingId"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.SamplingId = new Guid(reader["SamplingId"].ToString());
                            }
                            catch
                            {
                                throw new Exception("Unable to Get Samlping Id.");
                            }
                        }
                        objSample.SamplingResultId = SamplingResultId;
                        if (reader["ReceivigRequestId"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                            }
                            catch
                            {
                                throw new Exception("Unable to Get Receivig Request Id.");
                            }
                        }
                    }
                    return objSample;
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
            return null;

        }
        public static List<ReSamplingBLL> Search(string TrackingNo, Nullable<int> previousSampleCode, Nullable<DateTime> from, Nullable<DateTime> to, Nullable<ReSamplingStatus> status)
        {
            string strSql = "Select tblMoistureResamplingRequest.Id,TransactionId,tblMoistureResamplingRequest.Status,tblSampling.SampleCode , DateTimeRequested ";
            strSql += " from tblMoistureResamplingRequest ";
            strSql += " inner join tblSamplingResult on tblSamplingResult.Id = tblMoistureResamplingRequest.SamplingResultId";
            strSql += " inner join tblSampling on tblSampling.Id = tblMoistureResamplingRequest.SamplingId";
            //Where clause
            string strWhere = "Where ";
            if (TrackingNo != "")
            {
                strWhere += " TransactionId='" + TrackingNo.Trim() + "' ";
            }
            if (previousSampleCode != null)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " SampleCode=" + previousSampleCode.ToString() + " ";
                }
                else
                {
                    strWhere += "Or SampleCode=" + previousSampleCode.ToString() + " ";
                }
            }

            if (from != null)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " DateTimeRequested >='" + from.ToString() + "' ";
                }
                else
                {
                    strWhere += "Or DateTimeRequested >=" + from.ToString() + "' ";
                }
            }

            if (to != null)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " DateTimeRequested <='" + to.ToString() + "' ";
                }
                else
                {
                    strWhere += "Or  DateTimeRequested <=" + to.ToString() + "' ";
                }
            }

            if (status != null)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " tblMoistureResamplingRequest.Status=" + ((int)status).ToString() + " ";
                }
                else
                {
                    strWhere += " Or  tblMoistureResamplingRequest.Status=" + ((int)status).ToString() + " ";
                }
            }

            if (strWhere == "Where ")
            {
                throw new NULLSearchParameterException("Please provide Search parameter");
            }
            else
            {
                strSql += " " + strWhere;
                try
                {
                    return SearchHelper(strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


        }
        private static List<ReSamplingBLL> SearchHelper(string strSql)
        {
            List<ReSamplingBLL> list;
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                list = new List<ReSamplingBLL>();
                while (reader.Read())
                {
                    
                    ReSamplingBLL obj = new ReSamplingBLL();
                    if (reader["Id"] != null)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["Status"] != DBNull.Value)
                    {
                        try
                        {
                            obj.Status = (ReSamplingStatus)int.Parse(reader["Status"].ToString());
                        }
                        catch
                        {
                        }

                    }
                    if (reader["SampleCode"] != DBNull.Value)
                    {
                        try
                        {
                            obj.SampleCode = int.Parse(reader["SampleCode"].ToString());
                        }
                        catch
                        {
                        }
                    }

                    if (reader["DateTimeRequested"] != DBNull.Value)
                    {
                        try
                        {
                            obj.DateTimeRequested = DateTime.Parse(reader["DateTimeRequested"].ToString());
                        }
                        catch { }
                    }
                    if (reader["TransactionId"] != DBNull.Value)
                    {
                        try
                        {
                            obj.TrackingNo = reader["TransactionId"].ToString();
                        }
                        catch { }
                    }
                    list.Add(obj);
                    

                }
                if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                return list;
            }
            else
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;
            }
        }
        public static ReSamplingBLL GetById(Guid Id)
        {
            ReSamplingBLL objSample = new ReSamplingBLL();
            SqlDataReader reader;
            string strSql = "spGetResamplingbyId";
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

                        if (reader["Status"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.Status = (ReSamplingStatus)(int.Parse(reader["Status"].ToString()));
                            }
                            catch
                            {
                                throw new Exception("Unable to Get Status.");
                            }
                        }
                        if (reader["SampleCode"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.SampleCode = int.Parse(reader["SampleCode"].ToString());
                            }
                            catch
                            {
                                throw new Exception("Unable to Get Sample Code.");
                            }
                        }
                        if (reader["DateTimeRequested"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.DateTimeRequested = DateTime.Parse(reader["DateTimeRequested"].ToString());
                            }
                            catch
                            {
                                throw new Exception("Unable to Date requested.");
                            }
                        }
                        objSample.Remark = reader["Remark"].ToString();
                        objSample.TrackingNo = reader["TransactionId"].ToString();
                    }
                    return objSample;
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
        public static ReSamplingBLL GetByTrackingNo(string TrackingNo)
        {
            ReSamplingBLL objSample = new ReSamplingBLL();
            SqlDataReader reader;
            string strSql = "spGetReSamplingByTrackingNo";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar,50);
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

                        if (reader["Id"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.Id = new Guid(reader["Id"].ToString());
                            }
                            catch
                            {
                                throw new Exception("Unable to Get Status.");
                            }
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.Status = (ReSamplingStatus)(int.Parse(reader["Status"].ToString()));
                            }
                            catch
                            {
                                throw new Exception("Unable to Get Status.");
                            }
                        }
                        if (reader["DateTimeRequested"] != DBNull.Value)
                        {
                            try
                            {
                                objSample.DateTimeRequested = DateTime.Parse(reader["DateTimeRequested"].ToString());
                            }
                            catch
                            {
                                throw new Exception("Unable to Date requested.");
                            }
                        }
                        objSample.Remark = reader["Remark"].ToString();
                        objSample.TrackingNo = reader["TransactionId"].ToString();
                    }
                    return objSample;
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
        public static bool Update(ReSamplingBLL obj, SqlTransaction trans)
        {
            string strSql = "spUpdateReSampling";
            SqlParameter[] arPar = new SqlParameter[4];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.NVarChar, 50);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@DateTimeRequested", SqlDbType.DateTime);
                arPar[1].Value = obj.DateTimeRequested;

                arPar[2] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[2].Value = (int)obj.Status;
                
                arPar[3] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[3].Value = obj.LastModifiedBy;

                int AffectedRows = 0;

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
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static String[] GetReGradingRequestbyTrackingNo(string TrackingNo)
        {
            string strSql = "select tblGrading.GradingCode as GradingCode ,tblGradeDispute.TransactionId as TrackingNo from tblGradeDispute ";
            strSql += " inner join tblGradingResult on tblGradingResult.Id = tblGradeDispute.GradingResultId ";
            strSql += " inner join tblGrading on tblGrading.Id = tblGradingResult.GradingId ";
            strSql += " inner join tblSamplingResult on tblSamplingResult.Id = tblGrading.SamplingResultId ";
            strSql += " where tblGradeDispute.TransactionId in (" + TrackingNo + ")";             
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["GradingCode"].ToString() + " * " + reader["TrackingNo"].ToString());
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return list.ToArray();
            }
            return null;

        }   

    }
}
