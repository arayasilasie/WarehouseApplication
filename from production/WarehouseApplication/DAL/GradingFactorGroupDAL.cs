using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.BLL;
using Microsoft.ApplicationBlocks.Data;




namespace WarehouseApplication.DAL
{
   
    public class GradingFactorGroupDAL
    {
        public static bool Save(GradingFactorGroupBLL obj, SqlTransaction tran)
        {
            int Affectedrow = -1;
          
            string strsql = "spInsertGradingFactorGroup";
            SqlParameter[] arPar = new SqlParameter[5];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@GradingFactorGroupName", SqlDbType.NVarChar,250);
                arPar[1].Value = obj.GradingFactorGroupName;

                arPar[2] = new SqlParameter("@Description", SqlDbType.NVarChar);
                arPar[2].Value = obj.Description;


                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)obj.Status;



                arPar[4] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();


                Affectedrow = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strsql, arPar);

                if (Affectedrow == 1)
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
        public static List<GradingFactorGroupBLL> GetActive()
        {
            string strSql = "spGetActiveGradingFactorGroup";
            SqlConnection conn = new SqlConnection();
            List<GradingFactorGroupBLL> list;
            try
            {
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        list = new List<GradingFactorGroupBLL>();
                        while (reader.Read())
                        {
                            GradingFactorGroupBLL obj = new GradingFactorGroupBLL();
                            if (reader["Id"] != DBNull.Value )
                            {
                                obj.Id = new Guid( reader["Id"].ToString());
                            }
                            if (reader["GradingFactorGroupName"] != DBNull.Value)
                            {
                                obj.GradingFactorGroupName = reader["GradingFactorGroupName"].ToString();
                            }
                            if (reader["Description"] != DBNull.Value)
                            {
                                obj.Description = reader["Description"].ToString();
                            }
                            if (reader["Status"] != DBNull.Value)
                            {
                                obj.Status = (GradingFactorGroupStatus) int.Parse( reader["Status"].ToString());
                            }
                            if (reader["CreatedBy"] != DBNull.Value)
                            {
                                obj.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                            }
                            if (reader["CreatedTimestamp"] != DBNull.Value)
                            {
                                obj.CreatedTimestamp = DateTime.Parse( reader["CreatedTimestamp"].ToString());
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
                if( conn.State == ConnectionState.Open )
                    conn.Close();

            }
            return null;
        }
        
    }
    public class GradingFactorGroupDetailDAL
    {
        public static bool save(List<GradingFactorGroupDetailBLL> list, SqlTransaction tran, Guid GradingFactorGroupId )
        {
            
            string strSql = " ";
            foreach(GradingFactorGroupDetailBLL o in list )
            {
                string strTemp = "exec spInsertGradingfactorGroupDetail ";
                strTemp += "'" + o.Id.ToString() + "', ";
                
                if (GradingFactorGroupId == null)
                {
                    throw new Exception("Invalid GradingFactorGroupId ");
                }
                else
                {
                    strTemp += "'" + GradingFactorGroupId + "', ";
                }
                if (o.GradingFactorId == null)
                {
                    throw new Exception("Invalid Grading Factor Id ");
                }
                else
                {
                    strTemp += "'" + o.GradingFactorId.ToString() + "', ";
                }
                if (o.GradingTypeId != null)
                {
                    strTemp += "'" + o.GradingTypeId.ToString() + "', ";
                }
                else
                {
                    strTemp += "" + DBNull.Value.ToString() + ", ";
                }
                if (o.MaximumValue != null)
                {
                    strTemp += " " + o.MaximumValue.ToString() + ", ";
                }
                else
                {
                    strTemp += " null " + ", ";
                }
                if (o.MinimumValue != null)
                {
                    strTemp += " " + o.MinimumValue.ToString() + ", ";
                }
                else
                {
                    strTemp += " null " + ", ";
                }
                strTemp += " '" + o.FailPoint.ToString() + "', ";
                if (o.isMax != null)
                {
                    strTemp += " " + o.isMax.ToString() + ", ";
                }
                else
                {
                    strTemp += "" + DBNull.Value.ToString() + ", ";
                }
                if (o.isInTotalValue != null)
                {
                    strTemp += " " + o.isInTotalValue.ToString() + ", ";
                }
                else
                {
                    strTemp += "" + DBNull.Value.ToString() + ", ";
                }
                strTemp += " '" + o.PossibleValues.ToString() + "', ";
                strTemp += " " + o.Status.ToString() + ", ";
                strTemp += " '" + o.CreatedBy.ToString() + "' ";
                strTemp += " ; ";
                strSql += strTemp;
            }
            int Affectedrow = -1;
            Affectedrow = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql);
            if (Affectedrow > -1)
            {
                return true;
            }
            else
            {
                return false;
            }        
        }
    }
    public class CommodityGradingFactorDAL
    {
        public static bool Save(CommodityGradingFactorBLL obj, SqlTransaction tran)
        {
            int Affectedrow = -1;
            string strsql = "spInsertCommodityGradingFactor";
            SqlParameter[] arPar = new SqlParameter[6];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@CommodityId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.CommodityId;

                arPar[2] = new SqlParameter("@GradingFactorGroupId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.GradingFactorGroupId;


                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)obj.Status;



                arPar[4] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();


                arPar[5] = new SqlParameter("@isForCommodity", SqlDbType.Bit);
                arPar[5].Value = obj.isForCommodity;

                


                Affectedrow = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strsql, arPar);

                if (Affectedrow == 1)
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
        public static List<CommodityGradingFactorBLL> GetByCommodityId(Guid CommodityId)
        {
            string strSql = "spGetCommodityGradingFactorGroupByCommodityId";
            SqlConnection conn = new SqlConnection();
            List<CommodityGradingFactorBLL> list;
            try
            {
                
                conn = Connection.getConnection();
                SqlParameter[] arPar = new SqlParameter[1];
                SqlDataReader reader;
                arPar[0] = new SqlParameter("@CommodityId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = CommodityId;
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        list = new List<CommodityGradingFactorBLL>();
                        while (reader.Read())
                        {
                            
                            CommodityGradingFactorBLL obj = new CommodityGradingFactorBLL();
                            if (reader["Id"] != DBNull.Value)
                            {
                                obj.Id = new Guid(reader["Id"].ToString());
                            }
                            if (reader["CommodityId"] != DBNull.Value)
                            {
                                obj.CommodityId = new Guid( reader["CommodityId"].ToString());
                            }

                            if (reader["GradingFactorGroupId"] != DBNull.Value)
                            {
                                obj.GradingFactorGroupId = new Guid(reader["GradingFactorGroupId"].ToString());
                            }
                            if (reader["Status"] != DBNull.Value)
                            {
                                obj.Status = (CommodityGradingFactorStatus)int.Parse(reader["Status"].ToString());
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
                            if (reader["IsForCommodity"] != DBNull.Value)
                            {
                                obj.isForCommodity = bool.Parse(reader["IsForCommodity"].ToString());
                            }
                            obj.GroupName = reader["GradingFactorGroupName"].ToString();



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
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return null;
        }
        public static bool Inactive(Guid Id, SqlTransaction tran)
        {
            string strSql = "spInactiveCommodityGradingFactorGroup";
            int Affectedrow = 0;
           
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            arPar[1] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentUser();
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
            catch( Exception ex)
            {
               
                throw new Exception("Unable to Update Unloading Information.", ex);

            }

        }

    }

}
