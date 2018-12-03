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
    public class TrucksMissingOnSamplingDAL
    {
        public static bool Insert(TrucksMissingOnSamplingBLL obj , SqlTransaction tran)
        {
            
            string strSql = "spInsertTrucksMissingOnSampling";
            SqlParameter[] arPar = new SqlParameter[8];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@TrucksForSamplingId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.TrucksForSamplingId;

            arPar[2] = new SqlParameter("@DateTimeReported", SqlDbType.DateTime);
            arPar[2].Value = obj.DateTimeReported;

            arPar[3] = new SqlParameter("Reason", SqlDbType.Text);
            arPar[3].Value = obj.Remark;

            arPar[4] = new SqlParameter("Status", SqlDbType.Int);
            arPar[4].Value = (int) obj.Status;

            arPar[5] = new SqlParameter("TrackingNo", SqlDbType.NVarChar,50);
            arPar[5].Value =obj.TrackingNo;

            arPar[6] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[6].Value = obj.CreatedBy;

            arPar[7] = new SqlParameter("WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[7].Value = obj.WarehouseId;

            int AffectedRow = 0;
            AffectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (AffectedRow == 1)
            {
                return true;

            }
            else
            {
                throw new Exception("Unable to Insert all the Data.");
            }


 
           
        }
        public static List<TrucksMissingOnSamplingBLL> GetAbsentTrucks(Guid warehouseid)
        {
            List<TrucksMissingOnSamplingBLL> list = null;
            string strSql = "spGetTrucksMissingOnSampling";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@warehouseid", SqlDbType.UniqueIdentifier);
            arPar[0].Value = warehouseid;
            SqlConnection conn = null;
            conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<TrucksMissingOnSamplingBLL>();
                while (reader.Read())
             {
                TrucksMissingOnSamplingBLL obj = new TrucksMissingOnSamplingBLL();
                if (reader["Id"] != DBNull.Value)
                {
                    obj.Id = new Guid(reader["Id"].ToString());
                }
                if (reader["TrucksForSamplingId"] != DBNull.Value)
                {
                    obj.TrucksForSamplingId = new Guid(reader["TrucksForSamplingId"].ToString());
                }
                if (reader["DateTimeReported"] != DBNull.Value)
                {
                    obj.DateTimeReported = DateTime.Parse(reader["DateTimeReported"].ToString());
                }
                if (reader["Reason"] != DBNull.Value)
                {
                    obj.Remark = reader["Reason"].ToString();
                }
                if (reader["Status"] != DBNull.Value)
                {
                    obj.Status = (TrucksMissingOnSamplingStatus)reader["Status"];
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
                    obj.LastModifiedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                }
                if (reader["TrackingNo"] != DBNull.Value)
                {
                    obj.TrackingNo = reader["TrackingNo"].ToString();
                }
                if (reader["WarehouseId"] != DBNull.Value)
                {
                    obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                }
                if (reader["ReRequestedTime"] != DBNull.Value)
                {
                    obj.ReRequestedTime = DateTime.Parse(reader["ReRequestedTime"].ToString());
                }
                if (reader["isReRequested"] != DBNull.Value)
                {
                    obj.IsRequested = bool.Parse(reader["isReRequested"].ToString());
                }
                if (reader["PlateNumber"] != DBNull.Value)
                {
                    obj.PlateNo = reader["PlateNumber"].ToString();
                }
                if (reader["TrailerPlateNumber"] != DBNull.Value)
                {
                    obj.TrailerPlateNo = reader["TrailerPlateNumber"].ToString();
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
        else
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;
        }

     
        }
        public static bool SetStatus(Guid Id, TrucksMissingOnSamplingStatus status, SqlTransaction tran)
        {
          
            string strSql = "spReAllowTruckConfirmation";
          
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            arPar[1] = new SqlParameter("@Status", SqlDbType.UniqueIdentifier);
            arPar[1].Value = (int)status;

            int AffectedRow = 0;
            AffectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (AffectedRow == 1)
            {
                return true;

            }
            else
            {
                throw new Exception("Unable to Insert all the Data.");
            }


            
        }
        public static TrucksMissingOnSamplingBLL GetById(Guid Id)
        {
            string strSql = "spGetTrucksMissingOnSamplingById";
            SqlDataReader reader;
            SqlConnection conn = null;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows == true)
                {
                    TrucksMissingOnSamplingBLL obj = new TrucksMissingOnSamplingBLL();
                    reader.Read();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["TrucksForSamplingId"] != DBNull.Value)
                    {
                        obj.TrucksForSamplingId = new Guid(reader["TrucksForSamplingId"].ToString());
                    }
                    if (reader["DateTimeReported"] != DBNull.Value)
                    {
                        obj.DateTimeReported = DateTime.Parse(reader["DateTimeReported"].ToString());
                    }
                    if (reader["Reason"] != DBNull.Value)
                    {
                        obj.Remark = reader["Reason"].ToString();
                    }
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (TrucksMissingOnSamplingStatus)reader["Status"];
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
                        obj.LastModifiedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                    }
                    if (reader["TrackingNo"] != DBNull.Value)
                    {
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                    }
                    if (reader["WarehouseId"] != DBNull.Value)
                    {
                        obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                    }
                    if (reader["ReRequestedTime"] != DBNull.Value)
                    {
                        obj.ReRequestedTime = DateTime.Parse(reader["ReRequestedTime"].ToString());
                    }
                    if (reader["isReRequested"] != DBNull.Value)
                    {
                        obj.IsRequested = bool.Parse(reader["isReRequested"].ToString());
                    }
                    if (reader["PlateNumber"] != DBNull.Value)
                    {
                        obj.PlateNo = reader["PlateNumber"].ToString();
                    }
                    if (reader["TrailerPlateNumber"] != DBNull.Value)
                    {
                        obj.TrailerPlateNo = reader["TrailerPlateNumber"].ToString();
                    }
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
