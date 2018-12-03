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
    public class SamplingResultDAL
    {
        

        public static bool InsertSamplingResult(SamplingResultBLL obj, SqlTransaction tran)
        {
           
            int AffectedRows = 0;
            string strSql = "spInsertSamplingResult";
            SqlParameter[] arPar = new SqlParameter[15];

            arPar[0] = new SqlParameter("@SamplingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.SamplingId;

            arPar[1] = new SqlParameter("@EmployeeId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.EmployeeId;

            arPar[2] = new SqlParameter("@NumberOfBags", SqlDbType.Int);
            arPar[2].Value = obj.NumberOfBags;

            arPar[3] = new SqlParameter("@NumberOfSeparations", SqlDbType.Int);
            arPar[3].Value = obj.NumberOfSeparations;


            arPar[4] = new SqlParameter("@SamplerComments", SqlDbType.Text);
            arPar[4].Value = obj.SamplerComments; ;

            arPar[5] = new SqlParameter("@isSupervisor", SqlDbType.Bit);
            arPar[5].Value = obj.IsSupervisor;

            arPar[6] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[6].Value = (int)obj.Status;

            arPar[7] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[7].Value = obj.Remark;

            arPar[8] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[8].Value = obj.CreatedBy;

            arPar[9] = new SqlParameter("@SamplingCode", SqlDbType.Int);
            arPar[9].Value = obj.SamplingCode;

            arPar[10] = new SqlParameter("@SamplingResultCode", SqlDbType.NVarChar,50);
            arPar[10].Value = obj.SamplingResultCode;

            arPar[11] = new SqlParameter("@isPlompOk", SqlDbType.Bit);
            arPar[11].Value = obj.IsPlompOk;

            arPar[12] = new SqlParameter("@TrackingNo", SqlDbType.NChar, 50);
            arPar[12].Value = obj.TrackingNo;

            arPar[13] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[13].Value = obj.Id;

            arPar[14] = new SqlParameter("@ResultReceivedTimeStamp", SqlDbType.DateTime);
            arPar[14].Value = obj.ResultReceivedDateTime;


            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception ex)
            {
              throw  new Exception("Can not update the database.", ex);
            }
            if (AffectedRows == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateSamplingResult(SamplingResultBLL obj, SqlTransaction tran)
        {
            int AffectedRows = 0;
            string strSql = "spUpdateSamplingResult";
            SqlParameter[] arPar = new SqlParameter[9];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@EmployeeId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.EmployeeId;

            arPar[2] = new SqlParameter("@NumberOfBags", SqlDbType.Int);
            arPar[2].Value = obj.NumberOfBags;

            arPar[3] = new SqlParameter("@NumberOfSeparations", SqlDbType.Int);
            arPar[3].Value = obj.NumberOfSeparations; ;

            arPar[4] = new SqlParameter("@SamplerComments", SqlDbType.Text);
            arPar[4].Value = obj.SamplerComments;

            arPar[5] = new SqlParameter("@isSupervisor", SqlDbType.Bit);
            arPar[5].Value = obj.IsSupervisor;

            arPar[6] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[6].Value = (int)obj.Status;

            arPar[7] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[7].Value = obj.Remark;

            arPar[8] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[8].Value = UserBLL.GetCurrentUser();

            AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetNumberofSupervisorResults(Guid Id)
        {
            int count = 0;
            string strSql = "spGetNumberOfSupervisorSamplingResults";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@SamplingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = null;
  
            conn = Connection.getConnection();
            count =(int) SqlHelper.ExecuteScalar(conn, strSql, arPar);
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
            return count;
        }
        public static List<SamplingResultBLL> SearchSamplingResult(string TrackingNo,string SampleCode)
        {
            SqlDataReader reader ;
            SqlConnection conn = null;
            List<SamplingResultBLL> list;
            string strWhere = "";
            string strSql = "select  SamplingResultId,SamplingId,TrackingNo, SamplingCode,EmployeeId,NumberOfBags,";
            strSql = strSql + " NumberOfSeparations,SamplerComments,isSupervisor,Status,Remark, ";
            strSql = strSql + " CreatedBy,CreatedTimestamp,LastModifiedBy,LastModifiedTimestamp,SamplingResultCode,IsPlompOk , ResultReceivedTimeStamp";
            strSql = strSql + " from vSamplingResult where ";
            try
            {
                if (TrackingNo != "" || SampleCode != null)
                {
                    if (TrackingNo != "")
                    {
                        strWhere = " TrackingNo='" + TrackingNo + "' ";
                    }
                    if (SampleCode != null)
                    {
                        if (strWhere == "")
                        {
                            strWhere = strWhere + " SamplingCode like '" + @SampleCode + "'";
                        }
                        else
                        {
                            strWhere = strWhere + " or  SamplingCode like '" + SampleCode + "'";
                        }
                    }
                    strSql = strSql + strWhere + " order By isSupervisor desc ";
                    conn = Connection.getConnection();
                    reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
                    if (reader.HasRows)
                    {

                        list = new List<SamplingResultBLL>();
                        while (reader.Read())
                        {
                            SamplingResultBLL obj = new SamplingResultBLL();
                            obj.Id = new Guid(reader["SamplingResultId"].ToString());
                            obj.SamplingId = new Guid(reader["SamplingId"].ToString());
                            obj.EmployeeId = new Guid(reader["EmployeeId"].ToString());
                            obj.NumberOfBags = Convert.ToInt32(reader["NumberOfBags"]);
                            obj.NumberOfSeparations = Convert.ToInt32(reader["NumberOfSeparations"]);
                            obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                            obj.Status = (SamplingResultStatus)reader["Status"];
                            obj.SamplerComments = reader["SamplerComments"].ToString();
                            obj.Remark = reader["remark"].ToString();
                            obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                            obj.CreatedTimeStamp = Convert.ToDateTime(reader["CreatedTimestamp"]);
                            obj.TrackingNo = reader["TrackingNo"].ToString();

                            obj.SamplingResultCode = reader["SamplingResultCode"].ToString();
                            if (reader["IsPlompOk"] != null)
                            {
                                obj.IsPlompOk = bool.Parse(reader["IsPlompOk"].ToString());
                            }
                            else
                            {
                                obj.IsPlompOk = false;
                            }
                            if (reader["LastModifiedBy"] != DBNull.Value)
                            {

                                obj.LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());

                            }
                            if (reader["LastModifiedTimestamp"] != DBNull.Value)
                            {
                                obj.LastModifiedTimestamp = Convert.ToDateTime(reader["LastModifiedTimestamp"]);

                            }
                            if (reader["ResultReceivedTimeStamp"] != DBNull.Value)
                            {
                                obj.ResultReceivedDateTime = Convert.ToDateTime(reader["ResultReceivedTimeStamp"]);

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
        public static SamplingResultBLL GetSamplingResultById(Guid Id)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                
                string strSql = "spGetSamplingResultById";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = Id;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        SamplingResultBLL obj = new SamplingResultBLL();
                        reader.Read();
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.SamplingId = new Guid(reader["SamplingId"].ToString());
                        obj.EmployeeId = new Guid(reader["EmployeeId"].ToString());
                        obj.NumberOfBags = Convert.ToInt32(reader["NumberOfBags"].ToString());
                        obj.NumberOfSeparations = Convert.ToInt32(reader["NumberOfSeparations"].ToString());
                        obj.SamplerComments = reader["SamplerComments"].ToString();
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.Status = (SamplingResultStatus)reader["Status"];
                        obj.Remark = reader["Status"].ToString();
                        if (reader["ResultReceivedTimeStamp"] != DBNull.Value)
                        {
                            obj.ResultReceivedDateTime = DateTime.Parse(reader["ResultReceivedTimeStamp"].ToString());
                        }
                        if (reader["SampleCode"] != null)
                        {
                            obj.SamplingResultCode = reader["SampleCode"].ToString();
                        }
                        if (reader["TrackingNo"] != null)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["IsPlompOk"] != null)
                        {
                            obj.IsPlompOk = bool.Parse(reader["IsPlompOk"].ToString());
                        }
                        else
                        {
                            obj.IsPlompOk = false;
                        }
                        return obj;
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
            finally{
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
        public static SamplingResultBLL GetSamplingResultBySamplingResultCode(string Code)
        {
            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetSamplingResultByCode";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@Code", SqlDbType.NVarChar,50);
                arPar[0].Value = Code;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        SamplingResultBLL obj = new SamplingResultBLL();
                        reader.Read();
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.SamplingId = new Guid(reader["SamplingId"].ToString());
                        obj.EmployeeId = new Guid(reader["EmployeeId"].ToString());
                        obj.NumberOfBags = Convert.ToInt32(reader["NumberOfBags"].ToString());
                        obj.NumberOfSeparations = Convert.ToInt32(reader["NumberOfSeparations"].ToString());
                        obj.SamplerComments = reader["SamplerComments"].ToString();
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.Status = (SamplingResultStatus)reader["Status"];
                        obj.Remark = reader["Status"].ToString();
                        if (reader["ResultReceivedTimeStamp"] != DBNull.Value)
                        {
                            obj.ResultReceivedDateTime = DateTime.Parse(reader["ResultReceivedTimeStamp"].ToString());
                        }
                        if (reader["SampleCode"] != null)
                        {
                            obj.SamplingResultCode = reader["SampleCode"].ToString();
                        }
                        if (reader["TrackingNo"] != null)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["IsPlompOk"] != null)
                        {
                            obj.IsPlompOk = bool.Parse(reader["IsPlompOk"].ToString());
                        }
                        else
                        {
                            obj.IsPlompOk = false;
                        }
                        return obj;
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
        public static SamplingResultBLL GetActiveSamplingResultBySamplingId(Guid SamplingId)
        {
            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetActiveSamplingResultBySamplingId";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@SamplingId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = SamplingId;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        SamplingResultBLL obj = new SamplingResultBLL();
                        reader.Read();
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.SamplingId = new Guid(reader["SamplingId"].ToString());
                        obj.EmployeeId = new Guid(reader["EmployeeId"].ToString());
                        obj.NumberOfBags = Convert.ToInt32(reader["NumberOfBags"].ToString());
                        obj.NumberOfSeparations = Convert.ToInt32(reader["NumberOfSeparations"].ToString());
                        obj.SamplerComments = reader["SamplerComments"].ToString();
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.Status = (SamplingResultStatus)reader["Status"];
                        obj.Remark = reader["Status"].ToString();
                        if (reader["SampleCode"] != null)
                        {
                            obj.SamplingCode = long.Parse(reader["SampleCode"].ToString());
                        }

                        if (reader["IsPlompOk"] != null)
                        {
                            obj.IsPlompOk = bool.Parse(reader["IsPlompOk"].ToString());
                        }
                        else
                        {
                            obj.IsPlompOk = false;
                        }
                        if (reader["TrackingNo"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["ResultReceivedTimeStamp"] != DBNull.Value )
                        {
                            obj.ResultReceivedDateTime = DateTime.Parse( reader["ResultReceivedTimeStamp"].ToString());
                        }
                        return obj;
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
        //GradingDate
        public static List<SamplingResultBLL> GetSamplesResultsPendingCoding(Guid WarehouseId, string SamplingCode)
        {
            string strSql = "spGetSamplesResultsPendingCoding";
            SqlDataReader reader;
            List<SamplingResultBLL> list;
            SqlParameter[] arPar = new SqlParameter[2];
            SqlConnection conn = Connection.getConnection();
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;

            arPar[1] = new SqlParameter("@SamplingCode", SqlDbType.NVarChar , 50);
            arPar[1].Value = SamplingCode;

            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<SamplingResultBLL>();
                while (reader.Read())
                {
                    SamplingResultBLL objSamplingResult = new SamplingResultBLL();
                    objSamplingResult.Id = new Guid(reader["Id"].ToString());
                    objSamplingResult.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                    objSamplingResult.SamplingResultCode = reader["SampleCode"].ToString();
                    objSamplingResult.TrackingNo = reader["TrackingNo"].ToString();
                    //GradingDate
                    if (reader["SampleReturnedDateTime"] != DBNull.Value)
                    {
                        objSamplingResult.ResultReceivedDateTime = DateTime.Parse(reader["SampleReturnedDateTime"].ToString());
                    }
                    else if (reader["SamplingResultCreatedDate"] != DBNull.Value)
                    {
                        objSamplingResult.ResultReceivedDateTime = DateTime.Parse(reader["SamplingResultCreatedDate"].ToString());
                    }
                    else
                    {
                        objSamplingResult.ResultReceivedDateTime = DateTime.Now;
                    }
                    list.Add(objSamplingResult);
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
        public static SamplingResultBLL GetSamplesResultsPendingCodingByTrackingNo(Guid WarehouseId, string TrackingNo)
        {
            string strSql = "spGetSamplesResultsPendingCodingByTrackingNo";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            SqlConnection conn = Connection.getConnection();
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
            arPar[1].Value = TrackingNo;
            SamplingResultBLL objSamplingResult = null;
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                reader.Read();
                objSamplingResult  = new SamplingResultBLL();
                objSamplingResult.Id = new Guid(reader["Id"].ToString());
                objSamplingResult.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                objSamplingResult.SamplingResultCode = reader["SampleCode"].ToString();
                objSamplingResult.TrackingNo = TrackingNo;
                
            }
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return objSamplingResult; 
           
              
        }
        public static String[] GetSamplingCodeBylistTrackingNo(string TrackingNo)
        {
            string strSql = "Select SamplingResultCode,TrackingNo from tblSamplingResult where TrackingNo in (" + TrackingNo + ") order by SamplingResultCode desc ";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["SamplingResultCode"].ToString() + " * " + reader["TrackingNo"].ToString());
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
        public static SamplingResultBLL GetSamplingResultByTrackingNo(string TrackingNo)
        {
            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetSamplingResultListBySamplingTrackingNo";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar,50);
                arPar[0].Value = TrackingNo;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        SamplingResultBLL obj = new SamplingResultBLL();
                        reader.Read();
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.SamplingId = new Guid(reader["SamplingId"].ToString());
                        obj.EmployeeId = new Guid(reader["EmployeeId"].ToString());
                        obj.NumberOfBags = Convert.ToInt32(reader["NumberOfBags"].ToString());
                        obj.NumberOfSeparations = Convert.ToInt32(reader["NumberOfSeparations"].ToString());
                        obj.SamplerComments = reader["SamplerComments"].ToString();
                        obj.IsSupervisor = Convert.ToBoolean(reader["isSupervisor"]);
                        obj.Status = (SamplingResultStatus)reader["Status"];
                        obj.Remark = reader["Remark"].ToString();
                        if (reader["SampleCode"] != DBNull.Value)
                        {
                            obj.SamplingCode = long.Parse( reader["SampleCode"].ToString());
                        }
                        if (reader["SamplingResultCode"] != DBNull.Value)
                        {
                            obj.SamplingResultCode = reader["SamplingResultCode"].ToString();
                        }
                        if (reader["TrackingNo"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["IsPlompOk"] != DBNull.Value)
                        {
                            obj.IsPlompOk = bool.Parse(reader["IsPlompOk"].ToString());
                        }
                        else
                        {
                            obj.IsPlompOk = false;
                        }
                        if (reader["ResultReceivedTimeStamp"] != DBNull.Value)
                        {
                            obj.ResultReceivedDateTime = DateTime.Parse( reader["ResultReceivedTimeStamp"].ToString());
                        }
                        return obj;
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
        public static bool UpdateManagerApproval(SamplingResultBLL obj, SqlTransaction tran)
        {
            int AffectedRows = 0;
            string strSql = "spUpdateManagerApprovalSamplingResult";
            SqlParameter[] arPar = new SqlParameter[4];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@ManagerApprovalRemark", SqlDbType.Text);
            arPar[1].Value = obj.ManagerApprovalRemark;

            arPar[2] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[2].Value = (int)obj.Status;

            arPar[3] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[3].Value = UserBLL.GetCurrentUser();

            AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            if (AffectedRows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        }


    
    }

