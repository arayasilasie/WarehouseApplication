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
    public class TrucksForSamplingDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WarehouseId"></param>
        /// <returns>Commodity Deposite Request</returns>
        /// 
        public static List<TrucksForSamplingBLL> GetRandomTrucksForSamplingId(Guid WarehouseId, int NumberOfTrucks)
        {

            List<TrucksForSamplingBLL> list = null;
            string strSql = "spGetTrucksWaitingSampling";
            SqlConnection conn = null;
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader = null;
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@top", SqlDbType.Int);
            arPar[1].Value = NumberOfTrucks;

            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TrucksForSamplingBLL>();
                    int counter = 1;
                    while (reader.Read())
                    {
                        TrucksForSamplingBLL obj = new TrucksForSamplingBLL();
                        obj.WarehouseId = WarehouseId;
                        obj.ReceivigRequestId = new Guid(reader["Id"].ToString());
                        if (reader["DriverInformationId"] != DBNull.Value)
                        {
                            obj.DriverInformationId = new Guid(reader["DriverInformationId"].ToString());
                        }
                        else
                        {
                            obj.DriverInformationId = Guid.Empty;
                        }

                        obj.DateGenerated = DateTime.Now;
                        obj.SamplerInspectorId = UserBLL.GetCurrentUser();
                        obj.Status = TrucksForSamplingStatus.New;
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.CreatedBy = UserBLL.GetCurrentUser();
                        obj.PlateNo = reader["PlateNumber"].ToString();
                        obj.TrailerPlateNo = reader["TrailerPlateNumber"].ToString();
                        if (obj.TrackingNo != "")
                        {
                            try
                            {
                                string strM = "";
                                strM = WFTransaction.GetMessage(obj.TrackingNo);
                                if (strM.ToUpper() == "GetTrucksReadyForSam".ToUpper())
                                {
                                    if (counter <= NumberOfTrucks)
                                    {
                                        list.Add(obj);
                                        counter++;
                                    }
                                }
                                WFTransaction.UnlockTask(obj.TrackingNo);
                            }
                            catch
                            {
                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return list;

        }
        public static List<TrucksForSamplingBLL> GetRandomSamplingIdWithin2Hours(Guid WarehouseId, int NumberOfTrucks)
        {
            List<TrucksForSamplingBLL> list = null;
            string strSql = "spGetTrucksWaitingSamplingWithin2Hours";
            SqlConnection conn = null;
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader = null;
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@top", SqlDbType.Int);
            arPar[1].Value = NumberOfTrucks;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TrucksForSamplingBLL>();
                    int counter = 1;
                    while (reader.Read())
                    {
                        TrucksForSamplingBLL obj = new TrucksForSamplingBLL();
                        obj.WarehouseId = WarehouseId;
                        obj.ReceivigRequestId = new Guid(reader["Id"].ToString());
                        if (reader["DriverInformationId"] != DBNull.Value)
                        {
                            obj.DriverInformationId = new Guid(reader["DriverInformationId"].ToString());
                        }
                        else
                        {
                            obj.DriverInformationId = Guid.Empty;
                        }

                        obj.DateGenerated = DateTime.Now;
                        obj.SamplerInspectorId = UserBLL.GetCurrentUser();
                        obj.Status = TrucksForSamplingStatus.New;
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.CreatedBy = UserBLL.GetCurrentUser();
                        obj.PlateNo = reader["PlateNumber"].ToString();
                        obj.TrailerPlateNo = reader["TrailerPlateNumber"].ToString();
                        //GetTrucksReadyForSam
                        //check for each that they are at the step
                        if (obj.TrackingNo != "")
                        {
                            try
                            {
                                string strM = "";
                                strM = WFTransaction.GetMessage(obj.TrackingNo);
                                if (strM.ToUpper() == "GetTrucksReadyForSam".ToUpper())
                                {
                                    if (counter <= NumberOfTrucks)
                                    {
                                        list.Add(obj);
                                        counter++;
                                    }
                                }
                                WFTransaction.UnlockTask(obj.TrackingNo);
                            }
                            catch
                            {
                            }

                        }



                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return list;

        }
        public static bool InsertTruksForSampling(TrucksForSamplingBLL obj, SqlTransaction tran)
        {
            string strSql = "spInsertTrucksForSampling";

            try
            {

                SqlParameter[] arPar = new SqlParameter[9];
                arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.ReceivigRequestId;

                arPar[1] = new SqlParameter("@DriverInformationId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.DriverInformationId;

                arPar[2] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.WarehouseId;

                arPar[3] = new SqlParameter("@TrackingNo", SqlDbType.NChar, 50);
                arPar[3].Value = obj.TrackingNo;

                arPar[4] = new SqlParameter("@DateGenerated", SqlDbType.DateTime);
                arPar[4].Value = obj.DateGenerated;

                arPar[5] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[5].Value = (int)obj.Status;

                arPar[6] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[6].Value = obj.Remark;

                arPar[7] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[7].Value = UserBLL.GetCurrentUser();

                arPar[8] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[8].Value = obj.Id;

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
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static bool ApproveForSampling(Guid TrucksForSamplingId)
        {
            return true;
        }
        public static List<TrucksForSamplingBLL> GetTrucksPendingConfirmation(Guid WarehouseId)
        {

            List<TrucksForSamplingBLL> list = null;
            string strSql = "spGetTrucksPendingConfirmation";
            SqlConnection conn = null;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader = null;
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TrucksForSamplingBLL>();
                    while (reader.Read())
                    {
                        TrucksForSamplingBLL obj = new TrucksForSamplingBLL();
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.WarehouseId = WarehouseId;
                        obj.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                        obj.DriverInformationId = new Guid(reader["DriverInformationId"].ToString());
                        obj.DateGenerated = DateTime.Parse(reader["DateGenerated"].ToString());
                        obj.SamplerInspectorId = new Guid(reader["SamplingInspector"].ToString());
                        obj.Status = (TrucksForSamplingStatus)int.Parse(reader["Status"].ToString());
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.CreatedBy = UserBLL.GetCurrentUser();
                        obj.PlateNo = reader["PlateNumber"].ToString();
                        obj.TrailerPlateNo = reader["TrailerPlateNumber"].ToString();
                        obj.TrackingNo = reader["TrackingNo"].ToString();

                        list.Add(obj);

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return list;

        }
        public static bool UpdateConfirmation(TrucksForSamplingBLL obj, SqlTransaction tran)
        {
            string strSql = "spUpdateTrucksForSamplingConfirmation";
            try
            {

                SqlParameter[] arPar = new SqlParameter[4];
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[1].Value = (int)obj.Status;

                arPar[2] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[2].Value = obj.Remark;

                arPar[3] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[3].Value = UserBLL.GetCurrentUser();



                int AffectedRow = 0;
                AffectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (AffectedRow == 1)
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
    }
}
