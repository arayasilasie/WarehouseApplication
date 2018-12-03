using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.BLL;
using Microsoft.ApplicationBlocks.Data;

namespace WarehouseApplication.DAL
{
    public class GradingFactorDAL
    {
        public static bool Insert(GradingFactorBLL obj, SqlTransaction tran)
        {
            
            int Affectedrow = 0;
            string strSql = "spInsertGradingFactor";
                             
            SqlParameter[] arPar = new SqlParameter[5];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@GradingTypeId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.GradingTypeId;

                arPar[2] = new SqlParameter("@GradingFactorName", SqlDbType.VarChar,250);
                arPar[2].Value = obj.GradingFactorName;


                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)obj.Status;

               

                arPar[4] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();               

                
                Affectedrow = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);

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
        public static bool Update(GradingFactorBLL obj , SqlTransaction tran)
        {
            
            int Affectedrow = 0;
            string strSql = "spUpdateGradingFactor";

            SqlParameter[] arPar = new SqlParameter[5];
            try
            {
                arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@GradingTypeId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.GradingTypeId;

                arPar[2] = new SqlParameter("@GradingFactorName", SqlDbType.VarChar, 250);
                arPar[2].Value = obj.GradingFactorName;


                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)obj.Status;



                arPar[4] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();

    
                Affectedrow = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);

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
        public static List<GradingFactorBLL> GetGradingFactors(Guid CommodityGradeId)
        {
            string strSql = "spGetGradingFactors";
            SqlConnection conn = null;
            List<GradingFactorBLL> list;
            try
            {

                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@CommodityId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = CommodityGradeId;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        list = new List<GradingFactorBLL>();
                        while (reader.Read())
                        {
                            GradingFactorBLL obj = new GradingFactorBLL();
                            if (reader["Id"] != DBNull.Value)
                            {
                                try
                                {
                                    obj.Id = new Guid(reader["Id"].ToString());
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Unable to get Grading Value", ex);
                                }
                            }
                            if (reader["GradingFactorName"] != DBNull.Value)
                            {
                                obj.GradingFactorName = reader["GradingFactorName"].ToString();
                            }
                            if (reader["ValueType"] != DBNull.Value)
                            {
                                obj.DataType = reader["ValueType"].ToString();
                            }
                            if (reader["PossibleValues"] != DBNull.Value)
                            {
                                obj.PossibleValues = reader["PossibleValues"].ToString();
                            }
                            if (reader["FailPoint"] != DBNull.Value)
                            {
                                try
                                {
                                    obj.FailPoint = reader["FailPoint"].ToString();
                                }
                                catch
                                {
                                }
                            }
                            if (reader["isMax"] != DBNull.Value)
                            {
                                try
                                {
                                    obj.IsMax = (FailPointComparsion)Int32.Parse(reader["isMax"].ToString());
                                }
                                catch
                                {
                                }
                            }
                            if (reader["isInTotalValue"] != DBNull.Value)
                            {
                                try
                                {
                                    obj.IsInTotalValue = bool.Parse(reader["isInTotalValue"].ToString());
                                }
                                catch
                                {
                                    obj.IsInTotalValue = false;
                                }
                            }
                            else
                            {
                                obj.IsInTotalValue = false;
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
                conn.Close();

            }
            return null;

        }
        public static int GetGradingFactorInTotalValueCount(Guid CommodityGradeId)
        {
            string strSql = "spGetGradingFactorsInTotalValueCount";
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = CommodityGradeId;
            SqlDataReader reader;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader["TotalCount"] != DBNull.Value)
                        {
                            try
                            {
                                return int.Parse(reader["TotalCount"].ToString());
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Invalid Count", ex);
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get Count", ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public static List<GradingFactorBLL> Search(String Name, Nullable<Guid> GradingfactorTypeId, Nullable<GradingFactorStatus> Status)
        {
            string strwhere = " where ";
            string strSql = "select tblGradingFactor.Id,tblGradingFactor.GradingTypeId,tblGradingFactor.GradingFactorName, ";
            strSql += " tblGradingFactor.Id,tblGradingFactor.GradingTypeId,tblGradingFactor.GradingFactorName, ";
            strSql += " tblGradingFactor.Status,tblGradingFactor.CreatedBy,tblGradingFactor.CreatedTimestamp, ";
            strSql += " tblGradingFactor.LastModifiedBy,tblGradingFactor.LastModifiedTimestamp , tblGradingFactorType.Type as GradingFactorTypeName  ";
            strSql += " from tblGradingFactor ";
            strSql += " inner join tblGradingFactorType on tblGradingFactorType.Id = GradingTypeId ";
            if (Name != "")
            {
                strwhere += "tblGradingFactor.GradingFactorName like '" + Name.Trim() + "' ";
            }
            if (GradingfactorTypeId != null)
            {
                if (strwhere != " where ")
                {
                    strwhere += " Or tblGradingFactor.GradingTypeId= '" + GradingfactorTypeId.ToString() + "' ";
                }
                else
                {
                    strwhere += "tblGradingFactor.GradingTypeId= '" + GradingfactorTypeId.ToString() + "' ";

                }
            }
            if (Status != null)
            {
                if (strwhere != " where ")
                {
                    strwhere += " or tblGradingFactor.Status= " + ((int)Status.Value).ToString() + " ";
                }
                else
                {
                    strwhere += " tblGradingFactor.Status= " +( (int) Status.Value).ToString() + " ";
                }
            }
            if (strwhere != " where ")
            {

                strSql += " " + strwhere;
            }
            else
            {
                throw new NULLSearchParameterException("No Search Parameter provided");
            }

            List<GradingFactorBLL> list;
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();

                reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    list = new List<GradingFactorBLL>();
                    while (reader.Read())
                    {
                        GradingFactorBLL obj = new GradingFactorBLL();
                        if (reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GradingTypeId"] != DBNull.Value)
                        {
                            obj.GradingTypeId = new Guid(reader["GradingTypeId"].ToString());
                        }
                        if (reader["GradingFactorName"] != DBNull.Value)
                        {
                            obj.GradingFactorName = reader["GradingFactorName"].ToString();
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = (GradingFactorStatus)(int.Parse(reader["Status"].ToString()));
                        }
                        if (reader["GradingFactorTypeName"] != DBNull.Value)
                        {
                            obj.GradingFactorTypeName = reader["GradingFactorTypeName"].ToString();
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
        public static GradingFactorBLL GetById(Guid Id)
        {
            string strSql = "spGetGradingFactorByid";
            GradingFactorBLL obj;
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection();
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new GradingFactorBLL();
                    if (reader.Read())
                    {


                        obj.Id = new Guid(reader["Id"].ToString());
                        if (reader["GradingTypeId"] != DBNull.Value)
                        {
                            obj.GradingTypeId = new Guid(reader["GradingTypeId"].ToString());
                        }
                        obj.GradingFactorName = reader["GradingFactorName"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = (GradingFactorStatus)(int.Parse(reader["Status"].ToString()));
                        }
                        if (reader["Rank"] != DBNull.Value)
                        {
                            obj.Rank = int.Parse(reader["Rank"].ToString());
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
                            obj.CreatedBy = new Guid(reader["LastModifiedBy"].ToString());
                        }
                        if (reader["LastModifiedTimestamp"] != DBNull.Value)
                        {
                            obj.LastModifiedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
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
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
        }
    }
    public class GradingFactorTypeDAL
    {
        public static List<GradingFactorTypeBLL> GetActive()
        {
            string strSql = "spActiveGradingFactorType";
            List<GradingFactorTypeBLL> list;
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();

            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql);
            if (reader.HasRows)
            {
                list = new List<GradingFactorTypeBLL>();
                while (reader.Read())
                {
                    GradingFactorTypeBLL obj = new GradingFactorTypeBLL();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["GradingFactorTypeName"] != DBNull.Value)
                    {
                        obj.GradingFactorTypeName = reader["GradingFactorTypeName"].ToString();
                    }
                    if (reader["ValueType"] != DBNull.Value)
                    {
                        obj.ValueType = reader["ValueType"].ToString();
                    }
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (int.Parse(reader["Status"].ToString()));
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
    }
}
