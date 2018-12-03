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
    public class ScalingDAL
    {

        public static bool InsertScalingInformation(ScalingBLL obj, SqlTransaction tran)
        {
            string strSql = "spInsertScaling";
            SqlParameter[] arPar = new SqlParameter[18];
            try
            {
                arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.ReceivigRequestId;

                arPar[1] = new SqlParameter("@DriverInformationId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.DriverInformationId;

                arPar[2] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.GradingResultId;

                arPar[3] = new SqlParameter("@ScaleTicketNumber", SqlDbType.NChar , 50);
                arPar[3].Value = obj.ScaleTicketNumber;

                arPar[4] = new SqlParameter("@DateWeighed", SqlDbType.DateTime);
                arPar[4].Value = obj.DateWeighed;

                arPar[5] = new SqlParameter("@GrossWeightWithTruck", SqlDbType.Float);
                arPar[5].Value = obj.GrossWeightWithTruck;

                arPar[6] = new SqlParameter("@TruckWeight", SqlDbType.Float);
                arPar[6].Value = obj.TruckWeight;

                arPar[7] = new SqlParameter("@GrossWeight", SqlDbType.Float);
                arPar[7].Value = obj.GrossWeight;

                arPar[8] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[8].Value = (int)obj.Status;

                arPar[9] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[9].Value = obj.Remark;

                arPar[10] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[10].Value = UserBLL.GetCurrentUser();

                arPar[11] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[11].Value = obj.WarehouseId;

                arPar[12] = new SqlParameter("@PreWeightQueueNo", SqlDbType.Int);
                arPar[12].Value = obj.PreWeightQueueNo;

                arPar[13] = new SqlParameter("@QueueNo", SqlDbType.Int);
                arPar[13].Value = obj.QueueNo ;

                arPar[14] = new SqlParameter("@QueueDate", SqlDbType.DateTime);
                arPar[14].Value = obj.QueueDate;

                arPar[15] = new SqlParameter("@QueueDate", SqlDbType.DateTime);
                arPar[15].Value = obj.WeigherId;


                arPar[16] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[16].Value = obj.Id;

                arPar[17] = new SqlParameter("@TruckWeightId", SqlDbType.UniqueIdentifier);
                arPar[17].Value = obj.TruckWeightId;
                

                SqlConnection conn = new SqlConnection();
                conn = Connection.getConnection();
                int AffectedRow = 0;
                AffectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if(AffectedRow == 1)
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
                throw new Exception("Unable to insert Scaling Information.");
                
            }

        }
        public static int GetNumberofActiveScalingByGradingResultId(Guid DriverInformationId)
        {
            int count = 1;
            string strSql = "spGetNumberofActiveScalingByGradingId";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = DriverInformationId;
            SqlConnection conn = null;
            conn = Connection.getConnection();
            count = (int)SqlHelper.ExecuteScalar(conn, strSql, arPar);
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return count;
        }
        public static Nullable<float> GetApprovedWeightInformationByCommodityDepositeId(Guid Id)
        {
            Nullable<float> totalWeight = null;
            string strSql = "spGetApprovedScalingInformationByGradingId";
           
            SqlDataReader reader;
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



                        try
                        {
                            if (reader["TotalGrossWeight"] != null)
                            {
                                totalWeight = float.Parse(reader["TotalGrossWeight"].ToString());
                                return totalWeight;
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
        public static Nullable<float> GetApprovedWeightInformationByGradingId(Guid Id)
        {
            Nullable<float> totalWeight = null;
            string strSql = "spGetApprovedScalingInformationByGradingId";

            SqlDataReader reader;
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
                        if (reader["TotalGrossWeight"] != DBNull.Value)
                        {
                            totalWeight = float.Parse(reader["TotalGrossWeight"].ToString());
                        }
                        return totalWeight;
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
        public static Nullable<Guid> GetApprovedScalingIdByGradingId(Guid Id)
        {
            
            string strSql = "spGetApprovedScalingIdByGradingId";

            SqlDataReader reader;
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
                        Nullable<Guid> ScalingId = null;
                        if (reader["Id"] != DBNull.Value)
                        {
                            ScalingId = new Guid(reader["Id"].ToString());
                        }
                        return ScalingId;
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
        public static void GetQueueNumber(string QueueCode, Guid warehouseId, DateTime QueueDateTime, out int? QueueNo, out int? PreWeightQueueNo)
        {
            QueueNo = null;
            PreWeightQueueNo = null;
            string strSql = "spGetScalingQueueNumber";
            SqlParameter[] arPar = new SqlParameter[3];
            SqlDataReader reader;
            SqlConnection conn = null;
            try
            {
                arPar[0] = new SqlParameter("@QueueCode", SqlDbType.NVarChar, 4);
                arPar[0].Value = QueueCode;
                arPar[1] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = warehouseId;
                arPar[2] = new SqlParameter("@QueueDateTime", SqlDbType.DateTime);
                arPar[2].Value = QueueDateTime;
                conn = Connection.getConnection();
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
        public static List<ScalingBLL> Search(string ScaleTicketNumber, Nullable<DateTime> stratDateWeighed, Nullable<DateTime> endDateWeighed, string TrackingNo, string GradingCode)
        {
            List<ScalingBLL> list = new List<ScalingBLL>();
            string strWhere = "Where ";
            string strSql = " select tblScaling.Id,tblScaling.ReceivigRequestId, ";
            strSql += " tblScaling.DriverInformationId, tblScaling.GradingResultId, ";
            strSql += " tblScaling.ScaleTicketNumber,tblScaling.DateWeighed,";
            strSql += " tblScaling.GrossWeightWithTruck,tblScaling.TruckWeight, ";
            strSql += " tblScaling.GrossWeight,tblScaling.Status,tblScaling.Remark, ";
            strSql += " tblScaling.CreatedBy,tblScaling.CreatedTimestamp, ";
            strSql += " tblScaling.LastModifiedBy,tblScaling.LastModifiedTimestamp, ";
            strSql += " tblScaling.PreWeightQueueNo,tblScaling.QueueNo, ";
            strSql += " tblScaling.WarehouseId,tblScaling.QueueDate, ";
            strSql += " TransactionId as TrackingNo , GradingCode,WeigherId ";
            strSql += " from tblScaling ";
            strSql += " inner join tblGradingResult on tblGradingResult.Id = tblScaling.GradingResultId ";
            strSql += " inner join tblGrading on tblGrading.id = tblGradingResult.GradingId ";
            if (string.IsNullOrEmpty(ScaleTicketNumber) == false)
            {
                strWhere += " tblScaling.ScaleTicketNumber='" + ScaleTicketNumber + "'";
            }
            if (stratDateWeighed != null)
            {
                if(strWhere == "Where ")
                {
                    strWhere += " tblScaling.DateWeighed >= '" + stratDateWeighed.ToString() + "'";
                }
                else
                {
                    strWhere += " or  tblScaling.DateWeighed >= '" + stratDateWeighed.ToString() + "'";
                }

            }
            if (endDateWeighed != null)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " tblScaling.DateWeighed >= '" + endDateWeighed.ToString() + "'";
                }
                else
                {
                    strWhere += " or  tblScaling.DateWeighed >= '" + endDateWeighed.ToString() + "'";
                }

            }
            if (string.IsNullOrEmpty(TrackingNo) == false)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " TransactionId='" + TrackingNo.ToString() + "'";
                }
                else
                {
                    strWhere += " or  TransactionId= '" + TrackingNo.ToString() + "'";
                }

            }
            if (string.IsNullOrEmpty(GradingCode) == false)
            {
                if (strWhere == "Where ")
                {
                    strWhere += " GradingCode='" + GradingCode.ToString() + "'";
                }
                else
                {
                    strWhere += " or  GradingCode= '" + GradingCode.ToString() + "'";
                }

            }
            if( strWhere == "Where ")
            {
                throw new Exception("Invalid Search cretria.");
            }
            else
            {
                strSql = strSql + strWhere;
                list = Searchhelper(strSql);
            }
            return list;
        }
        private static List<ScalingBLL> Searchhelper(string strSql)
        {
            List<ScalingBLL> list = new List<ScalingBLL>();
            SqlDataReader reader ;
            SqlConnection conn = Connection.getConnection();
            try
            {
                reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);

                if (reader.HasRows)
                {
                   
                    while (reader.Read())
                    {
                        ScalingBLL obj = new ScalingBLL();
                        if (reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if(reader["GradingCode"] != DBNull.Value )
                        {
                            obj.GradingCode = reader["GradingCode"].ToString();
                        }
                        if (reader["GradingCode"] != DBNull.Value)
                        {
                            obj.GradingCode = reader["GradingCode"].ToString();
                        }
                        if (reader["ScaleTicketNumber"] != DBNull.Value)
                        {
                            obj.ScaleTicketNumber = reader["ScaleTicketNumber"].ToString();
                        }
                       
                        if (reader["DateWeighed"] != DBNull.Value)
                        {
                            try
                            {
                                obj.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                            }
                            catch
                            {
                            }
                        }
                        if (reader["GrossWeight"] != DBNull.Value)
                        {
                            try
                            {
                                obj.GrossWeight = float.Parse(reader["GrossWeight"].ToString());
                            }
                            catch
                            {
                            }
                        }
                        if (reader["TrackingNo"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TrackingNo"].ToString();
                        }
                        if (reader["WeigherId"] != null)
                        {
                            obj.WeigherId = new Guid(reader["WeigherId"].ToString());
                        }
                       
                        list.Add(obj);
                    }
                    reader.Dispose();
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
            return list;
        }
        public static ScalingBLL GetById(Guid Id)
        {

            string strSql = "spGetScalingInformationById";
            SqlConnection conn = new SqlConnection();
            try
            {

                
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
                        ScalingBLL obj = new ScalingBLL();
                        reader.Read();
                        if (reader["Id"] != null)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["ReceivigRequestId"] != null)
                        {
                            obj.ReceivigRequestId =  new Guid(reader["ReceivigRequestId"].ToString());
                        }
                        if (reader["ReceivigRequestId"] != DBNull.Value )
                        {
                            
                                obj.ReceivigRequestId = new Guid(reader["ReceivigRequestId"].ToString());
                            
                        }
                        if (reader["DriverInformationId"] != DBNull.Value )
                        {
                         
                                obj.DriverInformationId = new Guid(reader["DriverInformationId"].ToString());
                                
                        }
                        if (reader["DriverInformationId"] != DBNull.Value)
                        {
                            
                                obj.DriverInformationId = new Guid(reader["DriverInformationId"].ToString());
                            
                        }
                        if (reader["DriverInformationId"] != DBNull.Value )
                        {
                            
                                obj.DriverInformationId = new Guid(reader["DriverInformationId"].ToString());
       
                        }
                        if (reader["GradingResultId"] != DBNull.Value )
                        {
                            
                                obj.GradingResultId = new Guid(reader["GradingResultId"].ToString());
                            
                        }
                        if (reader["ScaleTicketNumber"] != DBNull.Value )
                        {
                            obj.ScaleTicketNumber = reader["ScaleTicketNumber"].ToString();                        
                        }
                        if (reader["DateWeighed"] != DBNull.Value )
                        {
                           
                            
                                obj.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                           
                        }
                        if (reader["GrossWeightWithTruck"] != DBNull.Value )
                        {
                            
                                obj.GrossWeightWithTruck = float.Parse(reader["GrossWeightWithTruck"].ToString());
                           
                        }
                        if (reader["TruckWeight"] != DBNull.Value)
                        {
                            
                                obj.TruckWeight = float.Parse(reader["TruckWeight"].ToString());
                           
                        }
                        if (reader["GrossWeight"] != DBNull.Value)
                        {
                            
                                obj.GrossWeight = float.Parse(reader["GrossWeight"].ToString());
                            
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            
                                obj.Status = ((ScalingStatus) int.Parse(reader["Status"].ToString()));
                           
                        }
                        if (reader["Remark"] != DBNull.Value)
                        {
                            obj.Remark = reader["Remark"].ToString();
                        }
                        if (reader["PreWeightQueueNo"] != DBNull.Value)
                        {
                            
                                obj.PreWeightQueueNo = int.Parse(reader["PreWeightQueueNo"].ToString());
                            
                        }
                        if (reader["QueueDate"] != DBNull.Value)
                        {
              
                                obj.QueueDate = DateTime.Parse(reader["QueueDate"].ToString());
                           
                        }
                        if (reader["WeigherId"] != DBNull.Value)
                        {
                            obj.WeigherId = new Guid(reader["WeigherId"].ToString());                     
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            return null;
           
        }
        public static bool Update(ScalingBLL obj, SqlTransaction tran)
        {
            int Affectedrow = 0;
            string strSql = "spUpdateScaling";
          
         
            SqlParameter[] arPar = new SqlParameter[10];
          
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@ScaleTicketNumber", SqlDbType.NVarChar, 50);
                arPar[1].Value = obj.ScaleTicketNumber;

                arPar[2] = new SqlParameter("@DateWeighed", SqlDbType.DateTime);
                arPar[2].Value = obj.DateWeighed;

                arPar[3] = new SqlParameter("@GrossWeightWithTruck", SqlDbType.Float);
                arPar[3].Value = obj.GrossWeightWithTruck;

                arPar[4] = new SqlParameter("@TruckWeight", SqlDbType.Float);
                arPar[4].Value = obj.TruckWeight;


                arPar[5] = new SqlParameter("@GrossWeight", SqlDbType.Float);
                arPar[5].Value = obj.GrossWeight;

                arPar[6] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[6].Value = (int)obj.Status;

                arPar[7] = new SqlParameter("@Remark", SqlDbType.Int);
                arPar[7].Value = obj.Remark;

                arPar[8] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[8].Value = UserBLL.GetCurrentUser();

                arPar[9] = new SqlParameter("@WeigherId", SqlDbType.UniqueIdentifier);
                arPar[9].Value = obj.WeigherId;
                try
                {
                    Affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                    if (Affectedrow == 1)
                    {

                        
                       
                        return true;
                    }
                    else
                    {
                
                        return false;
                    }
                }
                catch
                {
                    
                    throw new Exception("Unable to Update Unloading Information.");
                    
                }
                
        }
        public static String[] GetScalingCodeBylistTrackingNo(string TrackingNo)
        {
            string strSql = "select tblScaling.PreWeightQueueNo,TransactionId from tblScaling ";
            strSql += " inner join tblGradingResult on tblScaling.GradingResultId=tblGradingResult.Id ";
            strSql += " where TransactionId in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["PreWeightQueueNo"].ToString() + " * " + reader["TransactionId"].ToString());
                }
                return list.ToArray();
            }
            return null;

        }
    }
}
