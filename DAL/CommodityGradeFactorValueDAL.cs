using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class CommodityGradeFactorValueDAL
    {
        public static CommodityGradeFactorValueBLL GetActiveValueByGradeId(Guid Id)
        {
            string strSql = "spGetCommodityGradeGradingFactorValue";
            CommodityGradeFactorValueBLL obj;
            SqlDataReader reader;
            SqlConnection conn = null;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new CommodityGradeFactorValueBLL();
                    if (reader.Read())
                    {
                        try
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        catch (Exception  ex)
                        {
                            throw new Exception("Invalid Id ", ex);
                        }
                        try
                        {
                            obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Invalid Commodity Grade Id ", ex);
                        }
                        try
                        {
                            obj.MaxValue = float.Parse(reader["MaxValue"].ToString());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Invalid Maximum Value ", ex);
                        }
                        try
                        {
                            obj.MinValue = float.Parse(reader["MinValue"].ToString());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Invalid Minimum Value ", ex);
                        }
                        try
                        {
                            obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                            obj.CreatedTimestamp = Convert.ToDateTime(reader["CreatedTimestamp"].ToString());
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
                if (conn != null )
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
           
        }
    }
}
