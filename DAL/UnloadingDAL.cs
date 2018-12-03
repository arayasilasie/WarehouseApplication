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
    public class UnloadingDAL
    {
        public static Nullable<Guid> InsertUnloadingInformation(UnloadingBLL obj, SqlTransaction tran)
        {
            Nullable<Guid> Id = null;
            string strSql = "spInsertUnloading";
          
            SqlParameter[] arPar = new SqlParameter[8];
            try
            {
                arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.GradingResultId;

                arPar[1] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.ReceivigRequestId;

                arPar[2] = new SqlParameter("@TotalNumberOfBags", SqlDbType.Int);
                arPar[2].Value = obj.TotalNumberOfBags;

                arPar[3] = new SqlParameter("@DateDeposited", SqlDbType.DateTime);
                arPar[3].Value = obj.DateDeposited;

                arPar[4] = new SqlParameter("@Remark", SqlDbType.Text);
                arPar[4].Value = obj.Remark;

                arPar[5] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[5].Value = (int)UnloadingStatus.New;

                arPar[6] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[6].Value =UserBLL.GetCurrentUser();

                arPar[7] = new SqlParameter("@BagTypeId", SqlDbType.UniqueIdentifier);
                arPar[7].Value = obj.BagTypeId;

                SqlConnection conn = new SqlConnection();
                conn = Connection.getConnection();

                Id = new Guid(SqlHelper.ExecuteScalar(tran, strSql, arPar).ToString());
                return Id;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Unable to insert Unloading Information.", ex);
                
            }

            
        }

        public static int GetNumberofUnloadingByGradeingResultId(Guid Id)
        {
            int count = 1;
            string strSql = "spGetNumberofUnloadingbyGradingResultId";
            SqlConnection conn = null; 
            try
            {
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@GradingResultId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = Id;
                conn = Connection.getConnection();
                count = (int)SqlHelper.ExecuteScalar(conn, strSql, arPar);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get unloading count", ex);
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
           
        }

        public static List<UnloadingBLL> SearchUnloadingInformation( string Code,string TrackingNo )
        {
            string strSql = "spGetUnloadingInformationBySearchParameter";
            List<UnloadingBLL> list;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@GradingCode", SqlDbType.NVarChar,50);
            arPar[0].Value = Code;

            arPar[1] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[1].Value = TrackingNo;

            arPar[2] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = UserBLL.GetCurrentWarehouse();
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<UnloadingBLL>();
                    while (reader.Read())
                    {
                        int inserttolist = 0;
                        UnloadingBLL obj = new UnloadingBLL();
                        try
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        catch
                        {
                            inserttolist = -1;
                        }
                        try
                        {

                            obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        catch
                        {
                            inserttolist = -1;
                        }
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        try
                        {
                            obj.Status = (UnloadingStatus)int.Parse(reader["Status"].ToString());
                        }
                        catch { }

                        try
                        {
                            obj.TotalNumberOfBags = int.Parse(reader["TotalNumberOfBags"].ToString());
                        }
                        catch { }
                        try
                        {
                            obj.DateDeposited = DateTime.Parse(reader["DateDeposited"].ToString());
                        }
                        catch { }
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

        }

        public static UnloadingBLL GetUnloadingInformationById(Guid Id)
        {
            string strSql = "spGetUnloadingInformationById";
            UnloadingBLL obj;
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
                    obj = new UnloadingBLL();
                    while (reader.Read())
                    {

                        obj = new UnloadingBLL();
                        Nullable<Guid> UnloadingId = null;
                        if (DataValidationBLL.isGUID(reader["Id"].ToString(), out UnloadingId) == true)
                        {
                            obj.Id = (Guid)UnloadingId;
                        }
                        else
                        {
                            throw new Exception("Invalid Id Exception");
                        }
                        Nullable<Guid> CommodityGradeId = null;
                        if (DataValidationBLL.isGUID(reader["CommodityGradeId"].ToString(), out CommodityGradeId) == true)
                        {
                            obj.CommodityGradeId = (Guid)CommodityGradeId;
                        }
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        Nullable<int> status = null;
                        if (DataValidationBLL.isInteger(reader["Status"].ToString(), out status) == true)
                        {
                            obj.Status = (UnloadingStatus)(int)status;
                        }

                        Nullable<int> TotBags = null;
                        if (DataValidationBLL.isInteger(reader["TotalNumberOfBags"].ToString(), out TotBags) == true)
                        {
                            obj.TotalNumberOfBags = (int)TotBags;
                        }
                        Nullable<DateTime> dateDep = null;
                        if (DataValidationBLL.isDate(reader["DateDeposited"].ToString(), out dateDep) == true)
                        {
                            obj.DateDeposited = (DateTime)dateDep;
                        }
                        if (reader["GradingResultId"] != DBNull.Value)
                        {
                            obj.GradingResultId = new Guid(reader["GradingResultId"].ToString());
                        }
                        if (reader["GradingResultId"] != DBNull.Value)
                        {
                            obj.GradingResultId = new Guid(reader["GradingResultId"].ToString());
                        }
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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            


        }

        public static bool UpdateUnloading(UnloadingBLL obj, SqlTransaction tran)
        {
            int Affectedrow = 0;
            string strSql = "spUpdateUnloading";
            SqlParameter[] arPar = new SqlParameter[5];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@TotalNumberofBags", SqlDbType.Int);
                arPar[1].Value = obj.TotalNumberOfBags;

                arPar[2] = new SqlParameter("@DateDeposited", SqlDbType.DateTime);
                arPar[2].Value = obj.DateDeposited;

                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value = (int) obj.Status;

                arPar[4] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
                arPar[4].Value = UserBLL.GetCurrentUser();

                Affectedrow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
                if (Affectedrow == 1 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch ( Exception ex)
            {
                throw new Exception("Unable to Update Unloading Information.", ex);
                
            }
        }


        public static UnloadingBLL GetApprovedUnloadingInformationByCommodityDepositeId(Guid Id)
        {
            string strSql = "spGetApprovedUnloadingInformationByCommodityDepositeId";
            UnloadingBLL obj;
            SqlDataReader reader = null;
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
                    obj = new UnloadingBLL();
                    while (reader.Read())
                    {

                        obj = new UnloadingBLL();
                        Nullable<Guid> UnloadingId = null;
                        if (DataValidationBLL.isGUID(reader["Id"].ToString(), out UnloadingId) == true)
                        {
                            obj.Id = (Guid)UnloadingId;
                        }
                        else
                        {
                            throw new Exception("Invalid Id Exception");
                        }
                        Nullable<Guid> CommodityGradeId = null;
                        if (DataValidationBLL.isGUID(reader["CommodityGradeId"].ToString(), out CommodityGradeId) == true)
                        {
                            obj.CommodityGradeId = (Guid)CommodityGradeId;
                        }
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        Nullable<int> status = null;
                        if (DataValidationBLL.isInteger(reader["Status"].ToString(), out status) == true)
                        {
                            obj.Status = (UnloadingStatus)(int)status;
                        }

                        Nullable<int> TotBags = null;
                        if (DataValidationBLL.isInteger(reader["TotalNumberOfBags"].ToString(), out TotBags) == true)
                        {
                            obj.TotalNumberOfBags = (int)TotBags;
                        }
                        Nullable<DateTime> dateDep = null;
                        if (DataValidationBLL.isDate(reader["DateDeposited"].ToString(), out dateDep) == true)
                        {
                            obj.DateDeposited = (DateTime)dateDep;
                        }
                        Nullable<Guid> BagType = null;
                        if (DataValidationBLL.isGUID(reader["BagTypeId"].ToString(), out BagType) == true)
                        {
                            obj.BagTypeId = (Guid)BagType;
                        }
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
                if (reader != null)
                {
                    reader.Dispose();
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            


        }
        public static UnloadingBLL GetApprovedUnloadingInformationByGradingId(Guid Id)
        {
            string strSql = "spGetApprovedUnloadingInformationByGradingId";
            UnloadingBLL obj;
            SqlDataReader reader = null;
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
                    obj = new UnloadingBLL();
                    while (reader.Read())
                    {

                        obj = new UnloadingBLL();
                        Nullable<Guid> UnloadingId = null;
                        if (DataValidationBLL.isGUID(reader["Id"].ToString(), out UnloadingId) == true)
                        {
                            obj.Id = (Guid)UnloadingId;
                        }
                        else
                        {
                            throw new Exception("Invalid Id Exception");
                        }
                        Nullable<Guid> CommodityGradeId = null;
                        if (DataValidationBLL.isGUID(reader["CommodityGradeId"].ToString(), out CommodityGradeId) == true)
                        {
                            obj.CommodityGradeId = (Guid)CommodityGradeId;
                        }
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        Nullable<int> status = null;
                        if (DataValidationBLL.isInteger(reader["Status"].ToString(), out status) == true)
                        {
                            obj.Status = (UnloadingStatus)(int)status;
                        }

                        Nullable<int> TotBags = null;
                        if (DataValidationBLL.isInteger(reader["TotalNumberOfBags"].ToString(), out TotBags) == true)
                        {
                            obj.TotalNumberOfBags = (int)TotBags;
                        }
                        Nullable<DateTime> dateDep = null;
                        if (DataValidationBLL.isDate(reader["DateDeposited"].ToString(), out dateDep) == true)
                        {
                            obj.DateDeposited = (DateTime)dateDep;
                        }
                        Nullable<Guid> BagType = null;
                        if (DataValidationBLL.isGUID(reader["BagTypeId"].ToString(), out BagType) == true)
                        {
                            obj.BagTypeId = (Guid)BagType;
                        }
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
            

        }
        public static UnloadingBLL GetApprovedUnloadingInformationByGradingResultId(Guid Id)
        {
            string strSql = "spGetApprovedUnloadingInformationByGradingResultId";
            UnloadingBLL obj;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection() ;
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    obj = new UnloadingBLL();
                    while (reader.Read())
                    {

                        obj = new UnloadingBLL();
                        Nullable<Guid> UnloadingId = null;
                        if (DataValidationBLL.isGUID(reader["Id"].ToString(), out UnloadingId) == true)
                        {
                            obj.Id = (Guid)UnloadingId;
                        }
                        else
                        {
                            throw new Exception("Invalid Id Exception");
                        }
                        Nullable<Guid> CommodityGradeId = null;
                        if (DataValidationBLL.isGUID(reader["CommodityGradeId"].ToString(), out CommodityGradeId) == true)
                        {
                            obj.CommodityGradeId = (Guid)CommodityGradeId;
                        }
                        obj.TrackingNo = reader["TrackingNo"].ToString();
                        obj.GradingCode = reader["GradingCode"].ToString();
                        Nullable<int> status = null;
                        if (DataValidationBLL.isInteger(reader["Status"].ToString(), out status) == true)
                        {
                            obj.Status = (UnloadingStatus)(int)status;
                        }

                        Nullable<int> TotBags = null;
                        if (DataValidationBLL.isInteger(reader["TotalNumberOfBags"].ToString(), out TotBags) == true)
                        {
                            obj.TotalNumberOfBags = (int)TotBags;
                        }
                        Nullable<DateTime> dateDep = null;
                        if (DataValidationBLL.isDate(reader["DateDeposited"].ToString(), out dateDep) == true)
                        {
                            obj.DateDeposited = (DateTime)dateDep;
                        }
                        Nullable<Guid> BagType = null;
                        if (DataValidationBLL.isGUID(reader["BagTypeId"].ToString(), out BagType) == true)
                        {
                            obj.BagTypeId = (Guid)BagType;
                        }
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
                if (conn != null)
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
