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
    public class StackUnloadedDAL
    {
        public static bool InsertStackUnloaded( StackUnloadedBLL obj,SqlTransaction tran)
        {
            int affectedrow = 0;
            string strSql = "spInsertStackUnloaded";
            SqlParameter[] arPar = new SqlParameter[7];
            try
            {
                arPar[0] = new SqlParameter("@UnloadingId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.UnloadingId;

                arPar[1] = new SqlParameter("@StackId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.StackId;

                arPar[2] = new SqlParameter("@NumberOfBags", SqlDbType.Int);
                arPar[2].Value = obj.NumberOfbags;

                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int)StackUnloadedStatus.New;

                arPar[4] = new SqlParameter("@UnloadedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = obj.UserId;

                arPar[5] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[5].Value = obj.Remark;

                arPar[6] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[6].Value = UserBLL.GetCurrentUser();

                affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (affectedrow == 1)
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
                tran.Rollback();
                throw ex;
            }

        }
        public static List<StackUnloadedBLL> GetStackUnloadedByUnloadingId(Guid UnloadingId)
        {
            string strSql = "spGetStackInformationByUnloadingId";
            List<StackUnloadedBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@UnloadingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = UnloadingId;

            try
            {
                reader = SqlHelper.ExecuteReader(Connection.getConnection(), CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<StackUnloadedBLL>();
                    while (reader.Read())
                    {
                        int inserttolist = 0;
                        StackUnloadedBLL obj = new StackUnloadedBLL();
                        Nullable<Guid> Id = null;
                        if (DataValidationBLL.isGUID(reader["Id"].ToString(), out Id) == true)
                        {
                            obj.Id = (Guid)Id;
                        }
                        else
                        {
                            inserttolist = -1;
                        }
                        Nullable<Guid> StackId = null;
                        if (DataValidationBLL.isGUID(reader["StackId"].ToString(), out StackId) == true)
                        {

                            obj.StackId = (Guid)StackId;
                        }
                        else
                        {
                            inserttolist = -1;
                        }
                        //Inventory Controller.
                        Nullable<Guid> UnloadedBy = null;
                        if (DataValidationBLL.isGUID(reader["UnloadedBy"].ToString(), out UnloadedBy) == true)
                        {
                            obj.UserId = (Guid)UnloadedBy;
                        }
                        //Number of Bags
                        Nullable<Int32> noBags = null;
                        if (DataValidationBLL.isInteger(reader["NumberOfBags"].ToString(), out noBags) == true)
                        {
                            obj.NumberOfbags = (int)noBags;
                        }
                        Nullable<Int32> status = null;
                        if (DataValidationBLL.isInteger(reader["Status"].ToString(), out status) == true)
                        {
                            obj.Status = (StackUnloadedStatus)(int)status;
                        }
                 
                        obj.Remark = reader["Remark"].ToString();
                        obj.StackNo = reader["StackNumber"].ToString();
                        Nullable<Guid> ShedId = null;
                        if (DataValidationBLL.isGUID(reader["ShedId"].ToString(), out ShedId) == true)
                        {
                            obj.ShedId = (Guid)ShedId;
                        }
                        if (inserttolist != -1)
                        {
                            list.Add(obj);
                        }
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
           

        }
        public static bool UpdateStackUnloaded(StackUnloadedBLL obj, SqlTransaction tran)
        {
            int affectedrow = 0;
            string strSql = "spUpdateStack";
            SqlParameter[] arPar = new SqlParameter[5];
            try
            {
                arPar[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@NumberOfBags", SqlDbType.Int);
                arPar[1].Value = obj.NumberOfbags;

                arPar[2] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[2].Value = (int)obj.Status;

                arPar[3] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[3].Value = obj.Remark;

                arPar[4] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();

                affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (affectedrow == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(Exception ex )
            {
                throw ex;
                
            }
        }
        public static StackUnloadedBLL GetStackUnloadedById(Guid Id)
        {
            string strSql = "spGetStackUnloadedById";
            StackUnloadedBLL obj = null;
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
                    obj = new StackUnloadedBLL();
                    reader.Read();




                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }



                    if (reader["StackId"] != DBNull.Value)
                    {

                        obj.StackId = new Guid(reader["StackId"].ToString());
                    }

                    //Inventory Controller.
                    Nullable<Guid> UnloadedBy = null;
                    if (DataValidationBLL.isGUID(reader["UnloadedBy"].ToString(), out UnloadedBy) == true)
                    {
                        obj.UserId = (Guid)UnloadedBy;
                    }
                    //Number of Bags
                    Nullable<Int32> noBags = null;
                    if (DataValidationBLL.isInteger(reader["NumberOfBags"].ToString(), out noBags) == true)
                    {
                        obj.NumberOfbags = (int)noBags;
                    }
                    Nullable<Int32> status = null;
                    if (DataValidationBLL.isInteger(reader["Status"].ToString(), out status) == true)
                    {
                        obj.Status = (StackUnloadedStatus)(int)status;
                    }

                    obj.Remark = reader["Remark"].ToString();
                    obj.StackNo = reader["StackNumber"].ToString();
                    Nullable<Guid> ShedId = null;
                    if (DataValidationBLL.isGUID(reader["ShedId"].ToString(), out ShedId) == true)
                    {
                        obj.ShedId = (Guid)ShedId;
                    }



                }
                else
                {
                    obj = null;
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
            return obj;


        }
    
    }
}
