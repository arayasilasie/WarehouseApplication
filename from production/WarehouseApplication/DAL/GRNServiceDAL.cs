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
    public class GRNServiceDAL
    {
        public static bool Insert(Guid GRNId , List<GRNServiceBLL> list ,SqlTransaction tran )
        {
            bool IsSaved = false; ;
            string strSql = "spInsertGRNService";
            foreach (GRNServiceBLL obj in list)
            {
                SqlParameter[] arPar = new SqlParameter[6];

                arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@GRNId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = GRNId;

                arPar[2] = new SqlParameter("@ServiceId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.ServiceId;

                arPar[3] = new SqlParameter("@Quantity", SqlDbType.Float);
                arPar[3].Value = obj.Quantity;

                arPar[4] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[4].Value = (int)obj.Status;

                arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[5].Value = UserBLL.GetCurrentUser();



                int Affectedrow = 0;
                try
                {
                    Affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                    if (Affectedrow == 1)
                    {
                        IsSaved = true; ;
                    }
                    else
                    {

                        IsSaved = false;
                        return IsSaved;
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Unable to add GRN Service.", ex);

                }
                
            }
            return IsSaved;
        }
        public static bool Insert(GRNServiceBLL obj , SqlTransaction tran)
        {
                bool IsSaved = false; ;
                string strSql = "spInsertGRNService";

                SqlParameter[] arPar = new SqlParameter[6];

                arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@GRNId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.GRNId;

                arPar[2] = new SqlParameter("@ServiceId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.ServiceId;

                arPar[3] = new SqlParameter("@Quantity", SqlDbType.Float);
                arPar[3].Value = obj.Quantity;

                arPar[4] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[4].Value = (int)obj.Status;

                arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[5].Value = UserBLL.GetCurrentUser();



                int Affectedrow = 0;
                try
                {
                    Affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                    if (Affectedrow == 1)
                    {
                        IsSaved = true; ;
                    }
                    else
                    {

                        IsSaved = false;
                        return IsSaved;
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Unable to add GRN Service.", ex);

                }

            
            return IsSaved;
        }
        public static bool Cancel(Guid Id, SqlTransaction tran)
        {
            bool IsSaved = false;
            string strSql = "spCancelGRNService";
            SqlParameter[] arPar = new SqlParameter[2];

            arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            arPar[1] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentUser();
            int Affectedrow = 0;
                try
                {
                    Affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                    if (Affectedrow == 1)
                    {
                        IsSaved = true; 
                    }
                    else
                    {

                        IsSaved = false;
                        return IsSaved;
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Unable to add GRN Service.", ex);

                }
                return IsSaved;

        }
        public static List<GRNServiceBLL> GetByGRNId(Guid GRNId)
        {
            string strSql = "spGetGRNServicesByGRNId";
            List<GRNServiceBLL> list = null;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@GRNId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = GRNId;
                SqlDataReader reader;
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    list = new List<GRNServiceBLL>();
                    while (reader.Read())
                    {
                        GRNServiceBLL obj = new GRNServiceBLL();
                        if (reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRNId"] != DBNull.Value)
                        {
                            obj.GRNId = new Guid(reader["GRNId"].ToString());
                        }
                        if (reader["ServiceId"] != DBNull.Value)
                        {
                            obj.ServiceId = new Guid(reader["ServiceId"].ToString());
                        }
                        if (reader["Quantity"] != DBNull.Value)
                        {
                            obj.Quantity = int.Parse(reader["Quantity"].ToString());
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = (GRNServiceStatus) int.Parse(reader["Status"].ToString());
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return list;

        }
        public static SqlDataReader GetActiveByGRNId(Guid GRNId)
        {
            string strSql = "spGetActiveGRNServicesByGRNId";
            
            SqlConnection conn =  Connection.getConnection();
            try
            {

                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@GRNId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = GRNId;
                SqlDataReader reader;
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    return reader;
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
