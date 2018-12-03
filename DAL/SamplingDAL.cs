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
    public class SamplingDAL
    {
        private SamplingBLL _samplingBLL;
        public SamplingDAL()
        {

        }
        public SamplingDAL(SamplingBLL source)
        {
            this._samplingBLL = source;
        }
        public static Nullable<int> GetSerial(Guid WarehouseId)
        {
            int SerialNo = 0;
            try
            {


                string strSql = "spGetSerialNo";
                SqlParameter[] arPar = new SqlParameter[2];

                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;

                arPar[1] = new SqlParameter("@GeneratedDate", SqlDbType.DateTime);
                arPar[1].Value = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                SqlConnection conn = null;
                try
                {
                    conn = Connection.getConnection();
                    SerialNo = (int)SqlHelper.ExecuteScalar(conn, strSql, arPar);
                }
                catch (InvalidCastException)
                {

                    return 1;
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
                return SerialNo + 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Nullable<Guid> InsertSample(SamplingBLL objSample, SqlTransaction tran)
        {
            try
            {

                Nullable<Guid> Id = null;

                string strSql = "spInsertSampling";
                SqlParameter[] arPar = new SqlParameter[7];

                arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = objSample.ReceivigRequestId;

                arPar[1] = new SqlParameter("@SamplingStatusId", SqlDbType.Int);
                arPar[1].Value = 1;

                arPar[2] = new SqlParameter("@SerialNo", SqlDbType.Int);
                arPar[2].Value = objSample.SerialNo;

                arPar[3] = new SqlParameter("@GeneratedDate", SqlDbType.DateTime);
                arPar[3].Value = DateTime.Today;

                arPar[4] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[4].Value = objSample.WarehouseId;

                arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[5].Value = UserBLL.GetCurrentUser();


                arPar[6] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
                arPar[6].Value = objSample.TrackingNo;

                //   AffectedRows = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                Id = new Guid(SqlHelper.ExecuteScalar(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar).ToString());

                return Id;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public static SamplingBLL GetSampleById(Guid Id)
        {
            SamplingBLL objSample = new SamplingBLL();
            SqlDataReader reader;
            string strSql = "spGetSampleById";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@samplingId", SqlDbType.UniqueIdentifier);
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
                        SamplerBLL Sampler = new SamplerBLL();
                        objSample.SerialNo = Convert.ToInt32(reader["SerialNo"]);
                        objSample.GeneratedTimeStamp = Convert.ToDateTime(reader["GeneratedDate"]);
                        objSample.SampleCode = reader["SampleCode"].ToString();
                        objSample.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                        objSample.TrackingNo = reader["TrackingNo"].ToString();
                        if (reader["GeneratedDate"] != DBNull.Value)
                        {
                            objSample.GeneratedTimeStamp = (DateTime)reader["GeneratedDate"];
                        }
                        Sampler.SamplerId = new Guid(reader["UserId"].ToString());

                        objSample._sampler = Sampler;


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
        public static List<SamplingBLL> GetSamplesPendingResult(Guid WarehouseId, string TranNo)
        {
            string strSql;
            int path = 0;
            if (TranNo == "")
            {
                strSql = "spGetSamplesWaitingforResult";
                path = 1;
            }
            else
            {
                strSql = "spGetSamplesWaitingforResultByTransactionNo";
                path = 2;
            }
            SqlDataReader reader;
            List<SamplingBLL> list;
            SqlParameter[] arPar = null;
            if (path == 1)
            {
                arPar = new SqlParameter[1];

                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;
            }
            else if (path == 2)
            {
                arPar = new SqlParameter[2];

                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;

                arPar[1] = new SqlParameter("@TransNo", SqlDbType.NVarChar, 50);
                arPar[1].Value = TranNo;
            }
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<SamplingBLL>();
                    while (reader.Read())
                    {
                        SamplingBLL objSampling = new SamplingBLL();
                        objSampling.Id = new Guid(reader["Id"].ToString());
                        objSampling.SampleCode = reader["SampleCode"].ToString();
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
        public static List<SamplingBLL> GetSamplesPendingResultByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            string strSql;
            int path = 2;

            strSql = "spGetSamplesWaitingforResultByTransactionNo";

            SqlDataReader reader;
            List<SamplingBLL> list;
            SqlParameter[] arPar = null;
            if (path == 1)
            {
                arPar = new SqlParameter[1];

                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;
            }
            else if (path == 2)
            {
                arPar = new SqlParameter[2];

                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;

                arPar[1] = new SqlParameter("@TransNo", SqlDbType.NVarChar, 50);
                arPar[1].Value = TrackingNo;
            }
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<SamplingBLL>();
                    while (reader.Read())
                    {
                        SamplingBLL objSampling = new SamplingBLL();
                        objSampling.Id = new Guid(reader["Id"].ToString());
                        objSampling.SampleCode = reader["SampleCode"].ToString();
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
        public static Nullable<Guid> GetRandomSamplingIdWithin2Hours(Guid WarehouseId)
        {

            Nullable<Guid> Id = null;
            string strSql = "spGetDepositeRequestWaitingSamplingWithin2Hours";
            SqlConnection conn = Connection.getConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            try
            {
                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;
                string strUId;
                strUId = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, strSql, arPar).ToString();
                if (string.IsNullOrEmpty(strUId) == true)
                {
                    Id = null;
                }
                else
                {
                    Id = new Guid(strUId);
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
            return Id;
        }
        public static Nullable<Guid> GetRandomSamplingId(Guid WarehouseId)
        {

            Nullable<Guid> Id = null;
            string strSql = "spGetDepositeRequestWaitingSampling";
            SqlConnection conn = Connection.getConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            List<SamplingBLL> list = null;
            SqlDataReader reader;
            try
            {
                arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = WarehouseId;
                String strId = "";

                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<SamplingBLL>();
                    while (reader.Read())
                    {
                        SamplingBLL objSampling = new SamplingBLL();
                        if (reader["ReceivigRequestId"] != DBNull.Value)
                        {
                            objSampling.Id = new Guid(reader["ReceivigRequestId"].ToString());
                        }
                        if (reader["TrackingNo"] != DBNull.Value)
                        {
                            objSampling.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        list.Add(objSampling);

                    }
                    if (list.Count < 1)
                    {
                        return null;
                    }
                    else
                    {
                        foreach (SamplingBLL o in list)
                        {
                            try
                            {
                                string strm = WFTransaction.GetMessage(o.TrackingNo);
                                if (strm.ToUpper() == "GetSampleTicket".ToUpper())
                                {
                                    WFTransaction.UnlockTask(o.TrackingNo);
                                    return o.Id;
                                }
                                else
                                {
                                    WFTransaction.UnlockTask(o.TrackingNo);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                }
                else
                {
                    return null;
                }


            }
            catch (NullReferenceException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null || conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return Id;
        }
        public static List<SamplingBLL> GetSamplesPendingCoding(Guid WarehouseId)
        {
            string strSql = "spGetSamplesPendingCoding";
            SqlDataReader reader;
            List<SamplingBLL> list;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<SamplingBLL>();
                while (reader.Read())
                {
                    SamplingBLL objSampling = new SamplingBLL();
                    objSampling.Id = new Guid(reader["Id"].ToString());
                    objSampling.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                    objSampling.SampleCode = reader["SampleCode"].ToString();
                    list.Add(objSampling);
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return list;
            }
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;

        }
        public static string GetTransactionNumber(Guid Id)
        {
            string strGuid = string.Empty;
            string strSql = "spGetTransactionNumber";
            SqlConnection conn = Connection.getConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = Id;
                strGuid = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, strSql, arPar).ToString();
            }
            catch
            {
                strGuid = string.Empty;
            }
            finally
            {
                if (conn != null || conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return strGuid;

        }
        public static SamplingBLL GetApprovedSampleByReceivigRequestId(Guid Id)
        {
            SamplingBLL objSample = new SamplingBLL();
            SqlDataReader reader;
            string strSql = "spGetApprovedSamplingDetail";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = null;
            arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SamplerBLL Sampler = new SamplerBLL();
                        objSample.Id = new Guid(reader["SamplingId"].ToString());
                        objSample.SerialNo = Convert.ToInt32(reader["SerialNo"]);
                        objSample.GeneratedTimeStamp = Convert.ToDateTime(reader["GeneratedDate"]);
                        objSample.SampleCode = reader["SampleCode"].ToString();
                        Sampler.SamplerId = new Guid(reader["UserId"].ToString());
                        objSample._sampler = Sampler;
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
        public static SamplingBLL GetApprovedSampleByGradingId(Guid Id)
        {
            SamplingBLL objSample = new SamplingBLL();
            SqlDataReader reader;
            string strSql = "spGetApprovedSamplingDetailByGradingId";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
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
                        SamplerBLL Sampler = new SamplerBLL();
                        objSample.Id = new Guid(reader["SamplingId"].ToString());
                        objSample.SerialNo = Convert.ToInt32(reader["SerialNo"]);
                        objSample.GeneratedTimeStamp = Convert.ToDateTime(reader["GeneratedDate"]);
                        objSample.SampleCode = reader["SampleCode"].ToString();
                        Sampler.SamplerId = new Guid(reader["UserId"].ToString());
                        objSample._sampler = Sampler;
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
        public static Nullable<Guid> GetRandomReSamplingId(Guid WarehouseId, out Guid MoistureId, out string TransactionId)
        {

            Nullable<Guid> Id = null;
            string strSql = "spGetCommodityDepositeRequestPendingReSampling";
            SqlConnection conn = Connection.getConnection();
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader["Id"] != DBNull.Value)
                    {
                        try
                        {
                            Id = new Guid(reader["Id"].ToString());
                        }
                        catch
                        {
                            throw new Exception("Can not Get Resampling Id.");
                        }
                    }
                    else
                    {
                        throw new Exception("Can not Get Resampling Id.");
                    }
                    if (reader["MoistureResamplingRequestId"] != DBNull.Value)
                    {
                        try
                        {
                            MoistureId = new Guid(reader["MoistureResamplingRequestId"].ToString());
                        }
                        catch
                        {
                            throw new Exception("Can not Get Resampling Id.");
                        }
                    }
                    else
                    {
                        throw new Exception("Can not Get Resampling Id.");
                    }
                    if (reader["TransactionId"] != DBNull.Value)
                    {
                        try
                        {
                            TransactionId = reader["TransactionId"].ToString();
                        }
                        catch
                        {
                            throw new Exception("Can not Get Resampling Id.");
                        }
                    }
                    else
                    {
                        throw new Exception("Can not Get Resampling Id.");
                    }
                    reader.Close();
                    reader.Dispose();
                    return Id;
                }
                else
                {
                    throw new Exception("No Record pending Resampling.");
                }

            }
            catch
            {
                MoistureId = Guid.Empty;
                TransactionId = String.Empty;
                return null;
            }
            finally
            {
                if (conn != null || conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        public static String[] GetSamplingCodeBylistTrackingNo(string TrackingNo)
        {
            string strSql = "Select SampleCode,TrackingNo from tblSampling where TrackingNo in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["SampleCode"].ToString() + " * " + reader["TrackingNo"].ToString());
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
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;

        }
        public static String[] GetSamplingCodeForMixedBylistTrackingNo(string TrackingNo)
        {
            string strSql = "select SamplingResultCode as SampleCode ,TrackingNo from tblSamplingResult where TrackingNo in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["SampleCode"].ToString() + " * " + reader["TrackingNo"].ToString());
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
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;

        }
        public static List<SamplingBLL> Search(string TrackingNo, string SampleCode, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            string strSql = "Select Id,SampleCode,TrackingNo,GeneratedDate from tblSampling  ";
            string strWhere = " Where ";
            if (TrackingNo != "")
            {
                strWhere += "TrackingNo='" + TrackingNo + "' ";
            }
            if (SampleCode != "")
            {
                if (strWhere != " Where ")
                {
                    strWhere += " or SampleCode='" + SampleCode + "' ";
                }
                else
                {
                    strWhere += " SampleCode='" + SampleCode + "' ";
                }
            }
            if (from != null && to != null)
            {
                if (strWhere != " Where ")
                {
                    strWhere += " and ( GeneratedDate >='" + from.ToString() + "' and GeneratedDate<='" + to.ToString() + "')";
                }
                else
                {
                    strWhere += " ( GeneratedDate >='" + from.ToString() + "' and GeneratedDate<='" + to.ToString() + "')"; ;
                }
            }
            else if (from != null && to == null)
            {
                if (strWhere != " Where ")
                {
                    strWhere += " and ( GeneratedDate >='" + from.ToString() + "' and GeneratedDate<=" + to.ToString() + "')";
                }
                else
                {
                    strWhere += " GeneratedDate >='" + from.ToString() + "' and GeneratedDate<='" + to.ToString() + "'";
                }
            }
            strSql = strSql + " " + strWhere;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.Text, strSql);
            List<SamplingBLL> list = null;
            if (reader.HasRows)
            {
                list = new List<SamplingBLL>();
                while (reader.Read())
                {
                    SamplingBLL obj = new SamplingBLL();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["SampleCode"] != DBNull.Value)
                    {
                        obj.SampleCode = reader["SampleCode"].ToString();
                    }
                    if (reader["TrackingNo"] != DBNull.Value)
                    {
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                    }
                    if (reader["GeneratedDate"] != DBNull.Value)
                    {
                        obj.GeneratedTimeStamp = DateTime.Parse(reader["GeneratedDate"].ToString());
                    }

                    list.Add(obj);
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return list;
            }
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;
        }
        public static bool UpdateDateSampled(Guid Id, DateTime DateSampled)
        {
            bool isSaved = false;
            int Affectedrow = 0;
            string strSql = "spUpdateSampledDate";
            SqlTransaction tran = null;
            SqlConnection conn = Connection.getConnection();
            tran = conn.BeginTransaction();

            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            arPar[1] = new SqlParameter("@dateSampled", SqlDbType.DateTime);
            arPar[1].Value = DateSampled;

            arPar[2] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[2].Value = UserBLL.GetCurrentUser();

            try
            {
                Affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (Affectedrow == 1)
                {
                    tran.Commit();
                    return true;
                }
                else
                {
                    tran.Rollback();
                    return false;
                }
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }
            return isSaved;
        }
    }
}
