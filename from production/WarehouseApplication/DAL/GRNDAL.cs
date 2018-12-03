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
    public class GRNDAL
    {
        public static List<GRNBLL> GetGRNPendingCreation(Guid warehouseId)
        {
            string strSql = "spGetDepositeRequestPendingGRNCreation";
            SqlParameter[] arPar = new SqlParameter[1];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = warehouseId;
            List<GRNBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GRNBLL>();
                while (reader.Read())
                {
                    int inList = 0;
                    GRNBLL obj = new GRNBLL();

                    obj.GradingId = new Guid(reader["GradingId"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    try
                    {
                        obj.CommodityRecivingId = new Guid(reader["CommodityDepositRequest"].ToString());
                    }
                    catch
                    {
                        inList = -1;// do not inlclude in the list.
                    }
                    if (inList == 0)
                    {
                        list.Add(obj);
                    }

                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list;
            }
            else
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return null;
            }

        }
        public static List<GRNBLL> GetGRNPendingCreationByTrackingNoByTrackingNo(Guid warehouseId, string TrackingNo)
        {
            //string strSql = "spGetDepositeRequestPendingGRNCreation";
            string strSql = "spGetDepositeRequestPendingGRNCreationByTrackingNo";
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = warehouseId;
            arPar[1] = new SqlParameter("@TrackingNo", SqlDbType.NVarChar, 50);
            arPar[1].Value = TrackingNo;
            List<GRNBLL> list;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<GRNBLL>();
                while (reader.Read())
                {
                    int inList = 0;
                    GRNBLL obj = new GRNBLL();

                    obj.GradingId = new Guid(reader["GradingId"].ToString());
                    obj.GradingCode = reader["GradingCode"].ToString();
                    try
                    {
                        obj.CommodityRecivingId = new Guid(reader["CommodityDepositRequest"].ToString());
                    }
                    catch
                    {
                        inList = -1;// do not inlclude in the list.
                    }
                    if (inList == 0)
                    {
                        list.Add(obj);
                    }

                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list;
            }
            else
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return null;
            }

        }
        public static Nullable<Guid> InsertNewGRN(GRNBLL obj, string warehouseCode, SqlTransaction tran)
        {

            Nullable<Guid> Id = null;
            string strSql = "spInsertGRN";
            SqlParameter[] arPar = new SqlParameter[22];

            arPar[0] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.CommodityGradeId;

            arPar[1] = new SqlParameter("@CommodityRecivingId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.CommodityRecivingId;

            arPar[2] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = obj.WarehouseId;

            arPar[3] = new SqlParameter("@BagTypeId", SqlDbType.UniqueIdentifier);
            arPar[3].Value = obj.BagTypeId;

            arPar[4] = new SqlParameter("@VoucherId", SqlDbType.UniqueIdentifier);
            arPar[4].Value = obj.VoucherId;

            arPar[5] = new SqlParameter("@UnLoadingId", SqlDbType.UniqueIdentifier);
            arPar[5].Value = obj.UnLoadingId;

            arPar[6] = new SqlParameter("@ScalingId", SqlDbType.UniqueIdentifier);
            arPar[6].Value = obj.ScalingId;

            arPar[7] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[7].Value = obj.GradingId;

            arPar[8] = new SqlParameter("@SamplingTicketId", SqlDbType.UniqueIdentifier);
            arPar[8].Value = obj.SamplingTicketId;

            arPar[9] = new SqlParameter("@GRNTypeId", SqlDbType.UniqueIdentifier);
            arPar[9].Value = obj.GRNTypeId;

            arPar[10] = new SqlParameter("@DateDeposited", SqlDbType.DateTime);
            arPar[10].Value = obj.DateDeposited;

            arPar[11] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[11].Value = (int)obj.Status;

            arPar[12] = new SqlParameter("@TotalNumberOfBags", SqlDbType.Int);
            arPar[12].Value = (int)obj.TotalNumberOfBags;

            arPar[13] = new SqlParameter("@GrossWeight", SqlDbType.Float);
            arPar[13].Value = obj.GrossWeight;

            arPar[14] = new SqlParameter("@NetWeight", SqlDbType.Float);
            arPar[14].Value = obj.NetWeight;

            arPar[15] = new SqlParameter("@OriginalQuantity", SqlDbType.Float);
            arPar[15].Value = (Math.Round(obj.OriginalQuantity * 10000))/ 10000;

            arPar[16] = new SqlParameter("@CurrentQuantity", SqlDbType.Float);
            arPar[16].Value = (Math.Round(obj.CurrentQuantity * 10000)) / 10000;

            arPar[17] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[17].Value = UserBLL.GetCurrentUser();

            arPar[18] = new SqlParameter("@warehouseCode", SqlDbType.Float);
            arPar[18].Value = warehouseCode;

            arPar[19] = new SqlParameter("@GradingCode", SqlDbType.NVarChar, 50);
            arPar[19].Value = obj.GradingCode;

            arPar[20] = new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier);
            arPar[20].Value = obj.ClientId;

            arPar[21] = new SqlParameter("@GRNCreatedDate", SqlDbType.DateTime);
            arPar[21].Value = obj.GRNCreatedDate;

            
            try
            {
                Id = new Guid(SqlHelper.ExecuteScalar(tran, strSql, arPar).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Id;







        }
        public static int GetCountByGradingId(Guid GradingId)
        {

            int count = -1;
            string strSql = "spGetCountbyGradingId";
            SqlParameter[] arPar = new SqlParameter[1];

            arPar[0] = new SqlParameter("@GradingId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = GradingId;
            SqlConnection conn = Connection.getConnection();
            try
            {
                count = (int)SqlHelper.ExecuteScalar(conn, strSql, arPar);
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
            return count;
        }
        public static List<GRNBLL> Search(string GRN, string TrackingNo, Nullable<Guid> ClientId, Nullable<Guid> CommodityId, Nullable<Guid> CommodityClassId, Nullable<Guid> CommodityGradeId, Nullable<GRNStatus> Status, Nullable<DateTime> From, Nullable<DateTime> To)
        {
            #region strsqlConstr
            string strSql = "Select * from vGRNSearch ";
            string strWhere = "";
            string warhouse = " where ( WarehouseId='" + UserBLL.GetCurrentWarehouse().ToString() + "') And  (";
            if (!(string.IsNullOrEmpty(UserBLL.GetCurrentWarehouse().ToString())))
            {
                strWhere += warhouse;
            }
            else
            {
                throw new Exception("invalid Warehouse.");
            }
            


            if (GRN != "")
            {
                if (strWhere != warhouse)
                {
                    strWhere += " or GRN_Number='" + GRN + "'";
                }
                else
                {
                    strWhere += " GRN_Number='" + GRN + "'";
                }
            }

            if (TrackingNo != "")
            {
                if (strWhere != warhouse )
                {
                    strWhere += " or  TrackingNo ='" + TrackingNo + "' ";
                }
                else
                {
                    strWhere += " TrackingNo ='" + TrackingNo + "' ";
                }
            }

            if (ClientId != null && ClientId != Guid.Empty)
            {
                if (strWhere != warhouse)
                {
                    strWhere += " or ClientId='" + ClientId + "'";
                }
                else
                {
                    strWhere += " ClientId='" + ClientId + "' ";
                }

            }

            if (CommodityId != null && CommodityId != Guid.Empty)
            {
                if (strWhere != warhouse )
                {
                    strWhere += " or CommodityGradeId=" + CommodityId;
                }
                else
                {
                    strWhere += " CommodityGradeId=" + CommodityId + " ";
                }
            }

            if (CommodityClassId != null && CommodityClassId != Guid.Empty)
            {
                if (strWhere != warhouse )
                {
                    strWhere += " or CommodityClassId=" + CommodityClassId;
                }
                else
                {
                    strWhere += " CommodityClassId=" + CommodityClassId + " ";
                }
            }

            if (CommodityGradeId != null && CommodityGradeId != Guid.Empty)
            {
                if (strWhere != warhouse )
                {
                    strWhere += " or CommodityGradeId=" + CommodityGradeId;
                }
                else
                {
                    strWhere += " CommodityGradeId=" + CommodityGradeId + " ";
                }
            }
            if (Status != null)
            {
                if (strWhere != warhouse )
                {
                    strWhere += " or Status=" + ((int)Status).ToString();
                }
                else
                {
                    strWhere += " Status=" + ((int)Status).ToString() + " ";
                }
            }
            if (From != null && To != null)
            {
                if (strWhere != warhouse)
                {
                    strWhere += " or ( DateDeposited >='" + From + "' and DateDeposited <='" + To + "' )";
                }
                else
                {
                    strWhere += "  ( DateDeposited >='" + From + "' and DateDeposited <='" + To + "' )";
                }
            }
            else if (From != null && To == null)
            {
                if (strWhere != warhouse)
                {
                    strWhere += " or DateDeposited >='" + From + "'";
                }
                else
                {
                    strWhere += " DateDeposited >= '" + From + "' ";
                }
            }
            else if (To != null && From == null )
            {
                if (strWhere != warhouse )
                {
                    strWhere += " or DateDeposited <='" + To + "' ";
                }
                else
                {
                    strWhere += " DateDeposited <='" + To + "' ";
                }
            }
            strSql = strSql + strWhere + " ) ";
            #endregion
            List<GRNBLL> list = new List<GRNBLL>();
            list = SearchHelper(strSql);
            return list;


        }
        private static List<GRNBLL> SearchHelper(string strSql)
        {
            List<GRNBLL> list;
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                list = new List<GRNBLL>();
                while (reader.Read())
                {

                    GRNBLL obj = new GRNBLL();
                    if (reader["Id"] != null)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["GRN_Number"] != DBNull.Value)
                    {
                        obj.GRN_Number = reader["GRN_Number"].ToString();
                    }
                    if (reader["CommodityId"] != DBNull.Value)
                    {
                        obj.CommodityId = new Guid(reader["CommodityId"].ToString());
                    }
                    if (reader["CommodityClassId"] != DBNull.Value)
                    {
                        obj.CommodityClassId = new Guid(reader["CommodityClassId"].ToString());
                    }
                    if (reader["CommodityGradeId"] != DBNull.Value)
                    {
                        obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                    }
                    if (reader["ClientId"] != DBNull.Value)
                    {
                        obj.ClientId = new Guid(reader["ClientId"].ToString());
                    }
                    if (reader["OriginalQuantity"] != DBNull.Value)
                    {
                        obj.OriginalQuantity = float.Parse(reader["OriginalQuantity"].ToString());
                    }
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = int.Parse(reader["Status"].ToString());
                    }
                    if (reader["DateDeposited"] != DBNull.Value)
                    {
                        obj.DateDeposited = Convert.ToDateTime( reader["DateDeposited"].ToString());
                    }

                    list.Add(obj);

                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list;
            }
            else
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return null;
            }

        }
        public static GRNBLL GetGRNbyGRN_Number(Guid GRN)
        {

            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetGRNDetailbyId";
                SqlParameter[] arPar = new SqlParameter[2];
                arPar[0] = new SqlParameter("@GRN_Number", SqlDbType.UniqueIdentifier);
                arPar[0].Value = GRN;
                arPar[1] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = UserBLL.GetCurrentWarehouse();
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        GRNBLL obj = new GRNBLL();
                        reader.Read();
                        if (reader["Id"] != null)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRN_Number"].ToString() != null)
                        {
                            obj.GRN_Number = reader["GRN_Number"].ToString();
                        }
                        else
                        {
                            throw new InvalidIdException("GRN Numbers do not match.");
                        }
                        if (reader["GRNTypeId"] != DBNull.Value)
                        {
                            obj.GRNTypeId = new Guid(reader["GRNTypeId"].ToString());
                        }

                        if (reader["DateDeposited"] != DBNull.Value)
                        {
                            obj.DateDeposited = Convert.ToDateTime(reader["DateDeposited"].ToString());
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = Convert.ToInt32(reader["Status"].ToString());
                        }

                        if (reader["TotalNumberOfBags"] != DBNull.Value)
                        {
                            obj.TotalNumberOfBags = Convert.ToInt32(reader["TotalNumberOfBags"].ToString());
                        }
                        if (reader["GrossWeight"] != DBNull.Value)
                        {
                            obj.GrossWeight = float.Parse(reader["GrossWeight"].ToString());
                        }
                        if (reader["NetWeight"] != DBNull.Value)
                        {
                            obj.NetWeight = float.Parse(reader["NetWeight"].ToString());
                        }
                        if (reader["OriginalQuantity"] != DBNull.Value)
                        {
                            obj.OriginalQuantity = float.Parse(reader["OriginalQuantity"].ToString());
                        }
                        if (reader["CurrentQuantity"] != DBNull.Value)
                        {
                            obj.CurrentQuantity = float.Parse(reader["CurrentQuantity"].ToString());
                        }
                        if (reader["Remark"] != DBNull.Value)
                        {
                            obj.Remark = reader["Remark"].ToString();
                        }
                        if (reader["CommodityGradeId"] != DBNull.Value)
                        {
                            obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        if (reader["TransactionId"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TransactionId"].ToString();
                        }
                        if (reader["ProductionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = Convert.ToInt32(reader["ProductionYear"].ToString());
                        }

                        if (reader["BagTypeId"] != DBNull.Value)
                        {
                            obj.BagTypeId = new Guid(reader["BagTypeId"].ToString());
                        }

                        if (reader["GradingCode"] != DBNull.Value)
                        {
                            obj.GradingCode = reader["GradingCode"].ToString();
                        }

                        if (reader["WarehouseId"] != DBNull.Value)
                        {
                            obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                        }

                        if (reader["ClientId"] != DBNull.Value)
                        {
                            obj.ClientId = new Guid(reader["ClientId"].ToString());
                        }

                        if (reader["CommodityRecivingId"] != DBNull.Value)
                        {
                            obj.CommodityRecivingId = new Guid(reader["CommodityRecivingId"].ToString());
                        }

                        if (reader["VoucherId"] != DBNull.Value)
                        {
                            obj.VoucherId = new Guid(reader["VoucherId"].ToString());
                        }

                        if (reader["UnLoadingId"] != DBNull.Value)
                        {
                            obj.UnLoadingId = new Guid(reader["UnLoadingId"].ToString());
                        }

                        if (reader["ScalingId"] != DBNull.Value)
                        {
                            obj.ScalingId = new Guid(reader["ScalingId"].ToString());
                        }

                        if (reader["GradingId"] != DBNull.Value)
                        {
                            obj.GradingId = new Guid(reader["GradingId"].ToString());
                        }

                        if (reader["SamplingTicketId"] != DBNull.Value)
                        {
                            obj.SamplingTicketId = new Guid(reader["SamplingTicketId"].ToString());
                        }
                        if (reader["ClientAccepted"] != DBNull.Value)
                        {
                            obj.ClientAccepted = bool.Parse(reader["ClientAccepted"].ToString());
                        }

                        if (reader["ClientAcceptedTimeStamp"] != DBNull.Value)
                        {
                            obj.ClientAcceptedTimeStamp = DateTime.Parse(reader["ClientAcceptedTimeStamp"].ToString());
                        }
                        if (reader["ApprovedTimeStamp"] != DBNull.Value)
                        {
                            obj.ApprovedTimeStamp = DateTime.Parse(reader["ApprovedTimeStamp"].ToString());
                        }
                        if (reader["ApprovedBy"] != DBNull.Value)
                        {
                            obj.ApprovedBy = new Guid(reader["ApprovedBy"].ToString());
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

                        if (reader["ClientId"] != DBNull.Value)
                        {
                            obj.ClientId = new Guid(reader["ClientId"].ToString());
                        }
                        if (reader["GRNCreatedDate"] != DBNull.Value)
                        {
                            obj.GRNCreatedDate = DateTime.Parse(reader["GRNCreatedDate"].ToString());
                        }
                        else
                        {
                            obj.GRNCreatedDate = obj.CreatedTimestamp;
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
                    conn.Close();

            }
            return null;
        }
        public static bool ClientAcceptance(string GRN_number, DateTime AccptedTimeStamp, bool CAStatus, int status, SqlTransaction tran)
        {


            string strSql = "spInsertClientAcceptance";
            SqlParameter[] arPar = new SqlParameter[5];

            arPar[0] = new SqlParameter("@GRN_Number", SqlDbType.NVarChar, 50);
            arPar[0].Value = GRN_number;

            arPar[1] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentUser();

            arPar[2] = new SqlParameter("@AccptedTimeStamp", SqlDbType.DateTime);
            arPar[2].Value = AccptedTimeStamp;

            arPar[3] = new SqlParameter("@ClientAcceptanceStatus", SqlDbType.Bit);
            arPar[3].Value = CAStatus;

            arPar[4] = new SqlParameter("@status", SqlDbType.Int);
            arPar[4].Value = status;

            int affectedRow = 0;

            affectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (affectedRow == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateGRN(string GRN_number, GRNStatus status, SqlTransaction tran)
        {


            string strSql = "spUpdateGRN";
            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@GRN_Number", SqlDbType.NVarChar, 50);
            arPar[0].Value = GRN_number;

            arPar[1] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentUser();

            arPar[2] = new SqlParameter("@status", SqlDbType.Int);
            arPar[2].Value = status;

            int affectedRow = 0;

            affectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (affectedRow == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SetGRNStatus(string GRN_number, GRNStatus status, SqlTransaction tran, DateTime ManagerApprovedSDateTime)
        {


            string strSql = "spUpdateApproveGRN";
            SqlParameter[] arPar = new SqlParameter[4];

            arPar[0] = new SqlParameter("@GRN_Number", SqlDbType.NVarChar, 50);
            arPar[0].Value = GRN_number;

            arPar[1] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentUser();

            arPar[2] = new SqlParameter("@status", SqlDbType.Int);
            arPar[2].Value = status;

            arPar[3] = new SqlParameter("@ApprovedBy", SqlDbType.UniqueIdentifier);
            arPar[3].Value = UserBLL.GetCurrentUser();

            int affectedRow = 0;

            affectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (affectedRow == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateGRNStatus(string GRN_number, GRNStatus status, SqlTransaction tran)
        {


            string strSql = "spUpdateApproveGRN";
            SqlParameter[] arPar = new SqlParameter[4];

            arPar[0] = new SqlParameter("@GRN_Number", SqlDbType.NVarChar, 50);
            arPar[0].Value = GRN_number;

            arPar[1] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[1].Value = UserBLL.GetCurrentUser();

            arPar[2] = new SqlParameter("@status", SqlDbType.Int);
            arPar[2].Value = status;

            arPar[3] = new SqlParameter("@ApprovedBy", SqlDbType.UniqueIdentifier);
            arPar[3].Value = UserBLL.GetCurrentUser();

            int affectedRow = 0;

            affectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (affectedRow == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static GRNBLL GetGRNbyTrackingNo(string TrackingNo)
        {

            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetGRNDetailbyTrackingNo";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
                arPar[0].Value = TrackingNo;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        GRNBLL obj = new GRNBLL();
                        reader.Read();
                        if (reader["Id"] != null)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRN_Number"].ToString() != null)
                        {
                            obj.GRN_Number = reader["GRN_Number"].ToString();
                        }
                        else
                        {
                            throw new InvalidIdException("GRN Numbers do not match.");
                        }
                        if (reader["GRNTypeId"] != DBNull.Value)
                        {
                            obj.GRNTypeId = new Guid(reader["GRNTypeId"].ToString());
                        }

                        if (reader["DateDeposited"] != DBNull.Value)
                        {
                            obj.DateDeposited = Convert.ToDateTime(reader["DateDeposited"].ToString());
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = Convert.ToInt32(reader["Status"].ToString());
                        }

                        if (reader["TotalNumberOfBags"] != DBNull.Value)
                        {
                            obj.TotalNumberOfBags = Convert.ToInt32(reader["TotalNumberOfBags"].ToString());
                        }
                        if (reader["GrossWeight"] != DBNull.Value)
                        {
                            obj.GrossWeight = float.Parse(reader["GrossWeight"].ToString());
                        }
                        if (reader["NetWeight"] != DBNull.Value)
                        {
                            obj.NetWeight = float.Parse(reader["NetWeight"].ToString());
                        }
                        if (reader["OriginalQuantity"] != DBNull.Value)
                        {
                            obj.OriginalQuantity = float.Parse(reader["OriginalQuantity"].ToString());
                        }
                        if (reader["CurrentQuantity"] != DBNull.Value)
                        {
                            obj.CurrentQuantity = float.Parse(reader["CurrentQuantity"].ToString());
                        }
                        if (reader["Remark"] != DBNull.Value)
                        {
                            obj.Remark = reader["Remark"].ToString();
                        }
                        if (reader["CommodityGradeId"] != DBNull.Value)
                        {
                            obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        if (reader["TransactionId"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TransactionId"].ToString();
                        }
                        if (reader["ProductionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = Convert.ToInt32(reader["ProductionYear"].ToString());
                        }

                        if (reader["BagTypeId"] != DBNull.Value)
                        {
                            obj.BagTypeId = new Guid(reader["BagTypeId"].ToString());
                        }

                        if (reader["GradingCode"] != DBNull.Value)
                        {
                            obj.GradingCode = reader["GradingCode"].ToString();
                        }

                        if (reader["WarehouseId"] != DBNull.Value)
                        {
                            obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                        }

                        if (reader["ClientId"] != DBNull.Value)
                        {
                            obj.ClientId = new Guid(reader["ClientId"].ToString());
                        }

                        if (reader["CommodityRecivingId"] != DBNull.Value)
                        {
                            obj.CommodityRecivingId = new Guid(reader["CommodityRecivingId"].ToString());
                        }

                        if (reader["VoucherId"] != DBNull.Value)
                        {
                            obj.VoucherId = new Guid(reader["VoucherId"].ToString());
                        }

                        if (reader["UnLoadingId"] != DBNull.Value)
                        {
                            obj.UnLoadingId = new Guid(reader["UnLoadingId"].ToString());
                        }

                        if (reader["ScalingId"] != DBNull.Value)
                        {
                            obj.ScalingId = new Guid(reader["ScalingId"].ToString());
                        }

                        if (reader["GradingId"] != DBNull.Value)
                        {
                            obj.GradingId = new Guid(reader["GradingId"].ToString());
                        }

                        if (reader["SamplingTicketId"] != DBNull.Value)
                        {
                            obj.SamplingTicketId = new Guid(reader["SamplingTicketId"].ToString());
                        }
                        if (reader["ClientAccepted"] != DBNull.Value)
                        {
                            obj.ClientAccepted = bool.Parse(reader["ClientAccepted"].ToString());
                        }

                        if (reader["ClientAcceptedTimeStamp"] != DBNull.Value)
                        {
                            obj.ClientAcceptedTimeStamp = DateTime.Parse(reader["ClientAcceptedTimeStamp"].ToString());
                        }
                        if (reader["ApprovedTimeStamp"] != DBNull.Value)
                        {
                            obj.ApprovedTimeStamp = DateTime.Parse(reader["ApprovedTimeStamp"].ToString());
                        }

                        if (reader["ApprovedBy"] != DBNull.Value)
                        {
                            obj.ApprovedBy = new Guid(reader["ApprovedBy"].ToString());
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
                    conn.Close();

            }
            return null;
        }
        public static GRNBLL GetGRNbyGRNEditTrackingNo(string TrackingNo)
        {

            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetGRNDetailbyEditTrackingNo";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
                arPar[0].Value = TrackingNo;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        GRNBLL obj = new GRNBLL();
                        reader.Read();
                        if (reader["Id"] != null)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRN_Number"].ToString() != null)
                        {
                            obj.GRN_Number = reader["GRN_Number"].ToString();
                        }
                        else
                        {
                            throw new InvalidIdException("GRN Numbers do not match.");
                        }
                        if (reader["GRNTypeId"] != DBNull.Value)
                        {
                            obj.GRNTypeId = new Guid(reader["GRNTypeId"].ToString());
                        }

                        if (reader["DateDeposited"] != DBNull.Value)
                        {
                            obj.DateDeposited = Convert.ToDateTime(reader["DateDeposited"].ToString());
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = Convert.ToInt32(reader["Status"].ToString());
                        }

                        if (reader["TotalNumberOfBags"] != DBNull.Value)
                        {
                            obj.TotalNumberOfBags = Convert.ToInt32(reader["TotalNumberOfBags"].ToString());
                        }
                        if (reader["GrossWeight"] != DBNull.Value)
                        {
                            obj.GrossWeight = float.Parse(reader["GrossWeight"].ToString());
                        }
                        if (reader["NetWeight"] != DBNull.Value)
                        {
                            obj.NetWeight = float.Parse(reader["NetWeight"].ToString());
                        }
                        if (reader["OriginalQuantity"] != DBNull.Value)
                        {
                            obj.OriginalQuantity = float.Parse(reader["OriginalQuantity"].ToString());
                        }
                        if (reader["CurrentQuantity"] != DBNull.Value)
                        {
                            obj.CurrentQuantity = float.Parse(reader["CurrentQuantity"].ToString());
                        }
                        if (reader["Remark"] != DBNull.Value)
                        {
                            obj.Remark = reader["Remark"].ToString();
                        }
                        if (reader["CommodityGradeId"] != DBNull.Value)
                        {
                            obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        if (reader["TransactionId"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TransactionId"].ToString();
                        }
                        if (reader["ProductionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = Convert.ToInt32(reader["ProductionYear"].ToString());
                        }

                        if (reader["BagTypeId"] != DBNull.Value)
                        {
                            obj.BagTypeId = new Guid(reader["BagTypeId"].ToString());
                        }

                        if (reader["GradingCode"] != DBNull.Value)
                        {
                            obj.GradingCode = reader["GradingCode"].ToString();
                        }

                        if (reader["WarehouseId"] != DBNull.Value)
                        {
                            obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                        }

                        if (reader["ClientId"] != DBNull.Value)
                        {
                            obj.ClientId = new Guid(reader["ClientId"].ToString());
                        }

                        if (reader["CommodityRecivingId"] != DBNull.Value)
                        {
                            obj.CommodityRecivingId = new Guid(reader["CommodityRecivingId"].ToString());
                        }

                        if (reader["VoucherId"] != DBNull.Value)
                        {
                            obj.VoucherId = new Guid(reader["VoucherId"].ToString());
                        }

                        if (reader["UnLoadingId"] != DBNull.Value)
                        {
                            obj.UnLoadingId = new Guid(reader["UnLoadingId"].ToString());
                        }

                        if (reader["ScalingId"] != DBNull.Value)
                        {
                            obj.ScalingId = new Guid(reader["ScalingId"].ToString());
                        }

                        if (reader["GradingId"] != DBNull.Value)
                        {
                            obj.GradingId = new Guid(reader["GradingId"].ToString());
                        }

                        if (reader["SamplingTicketId"] != DBNull.Value)
                        {
                            obj.SamplingTicketId = new Guid(reader["SamplingTicketId"].ToString());
                        }
                        if (reader["ClientAccepted"] != DBNull.Value)
                        {
                            obj.ClientAccepted = bool.Parse(reader["ClientAccepted"].ToString());
                        }

                        if (reader["ClientAcceptedTimeStamp"] != DBNull.Value)
                        {
                            obj.ClientAcceptedTimeStamp = DateTime.Parse(reader["ClientAcceptedTimeStamp"].ToString());
                        }
                        if (reader["ApprovedTimeStamp"] != DBNull.Value)
                        {
                            obj.ApprovedTimeStamp = DateTime.Parse(reader["ApprovedTimeStamp"].ToString());
                        }

                        if (reader["ApprovedBy"] != DBNull.Value)
                        {
                            obj.ApprovedBy = new Guid(reader["ApprovedBy"].ToString());
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
                    conn.Close();
            }
            return null;
        }
        public static GRNBLL GetGRNbyGRNApprovedGRNCancelByTrackingNo(string TrackingNo)
        {

            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spGetGRNDetailbyCancelApprovedTrackingNo";
                SqlParameter[] arPar = new SqlParameter[1];
                arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
                arPar[0].Value = TrackingNo;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        GRNBLL obj = new GRNBLL();
                        reader.Read();
                        if (reader["Id"] != null)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        if (reader["GRN_Number"].ToString() != null)
                        {
                            obj.GRN_Number = reader["GRN_Number"].ToString();
                        }
                        else
                        {
                            throw new InvalidIdException("GRN Numbers do not match.");
                        }
                        if (reader["GRNTypeId"] != DBNull.Value)
                        {
                            obj.GRNTypeId = new Guid(reader["GRNTypeId"].ToString());
                        }

                        if (reader["DateDeposited"] != DBNull.Value)
                        {
                            obj.DateDeposited = Convert.ToDateTime(reader["DateDeposited"].ToString());
                        }
                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = Convert.ToInt32(reader["Status"].ToString());
                        }

                        if (reader["TotalNumberOfBags"] != DBNull.Value)
                        {
                            obj.TotalNumberOfBags = Convert.ToInt32(reader["TotalNumberOfBags"].ToString());
                        }
                        if (reader["GrossWeight"] != DBNull.Value)
                        {
                            obj.GrossWeight = float.Parse(reader["GrossWeight"].ToString());
                        }
                        if (reader["NetWeight"] != DBNull.Value)
                        {
                            obj.NetWeight = float.Parse(reader["NetWeight"].ToString());
                        }
                        if (reader["OriginalQuantity"] != DBNull.Value)
                        {
                            obj.OriginalQuantity = float.Parse(reader["OriginalQuantity"].ToString());
                        }
                        if (reader["CurrentQuantity"] != DBNull.Value)
                        {
                            obj.CurrentQuantity = float.Parse(reader["CurrentQuantity"].ToString());
                        }
                        if (reader["Remark"] != DBNull.Value)
                        {
                            obj.Remark = reader["Remark"].ToString();
                        }
                        if (reader["CommodityGradeId"] != DBNull.Value)
                        {
                            obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        if (reader["TransactionId"] != DBNull.Value)
                        {
                            obj.TrackingNo = reader["TransactionId"].ToString();
                        }
                        if (reader["ProductionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = Convert.ToInt32(reader["ProductionYear"].ToString());
                        }

                        if (reader["BagTypeId"] != DBNull.Value)
                        {
                            obj.BagTypeId = new Guid(reader["BagTypeId"].ToString());
                        }

                        if (reader["GradingCode"] != DBNull.Value)
                        {
                            obj.GradingCode = reader["GradingCode"].ToString();
                        }

                        if (reader["WarehouseId"] != DBNull.Value)
                        {
                            obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                        }

                        if (reader["ClientId"] != DBNull.Value)
                        {
                            obj.ClientId = new Guid(reader["ClientId"].ToString());
                        }

                        if (reader["CommodityRecivingId"] != DBNull.Value)
                        {
                            obj.CommodityRecivingId = new Guid(reader["CommodityRecivingId"].ToString());
                        }

                        if (reader["VoucherId"] != DBNull.Value)
                        {
                            obj.VoucherId = new Guid(reader["VoucherId"].ToString());
                        }

                        if (reader["UnLoadingId"] != DBNull.Value)
                        {
                            obj.UnLoadingId = new Guid(reader["UnLoadingId"].ToString());
                        }

                        if (reader["ScalingId"] != DBNull.Value)
                        {
                            obj.ScalingId = new Guid(reader["ScalingId"].ToString());
                        }

                        if (reader["GradingId"] != DBNull.Value)
                        {
                            obj.GradingId = new Guid(reader["GradingId"].ToString());
                        }

                        if (reader["SamplingTicketId"] != DBNull.Value)
                        {
                            obj.SamplingTicketId = new Guid(reader["SamplingTicketId"].ToString());
                        }
                        if (reader["ClientAccepted"] != DBNull.Value)
                        {
                            obj.ClientAccepted = bool.Parse(reader["ClientAccepted"].ToString());
                        }

                        if (reader["ClientAcceptedTimeStamp"] != DBNull.Value)
                        {
                            obj.ClientAcceptedTimeStamp = DateTime.Parse(reader["ClientAcceptedTimeStamp"].ToString());
                        }
                        if (reader["ApprovedTimeStamp"] != DBNull.Value)
                        {
                            obj.ApprovedTimeStamp = DateTime.Parse(reader["ApprovedTimeStamp"].ToString());
                        }

                        if (reader["ApprovedBy"] != DBNull.Value)
                        {
                            obj.ApprovedBy = new Guid(reader["ApprovedBy"].ToString());
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
                    conn.Close();
            }
            return null;
        }
        public static int HasGRN(string tableName, Guid Id)
        {
            int count = -1;
            SqlConnection conn = new SqlConnection();
            try
            {

                string strSql = "spHasGRN";
                SqlParameter[] arPar = new SqlParameter[2];
                arPar[0] = new SqlParameter("@tablename", SqlDbType.NVarChar, 50);
                arPar[0].Value = tableName;
                arPar[1] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[1].Value = Id;
                SqlDataReader reader;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        GRNBLL obj = new GRNBLL();
                        reader.Read();
                        if (reader["TotalCount"] != null)
                        {
                            count = Convert.ToInt32(reader["TotalCount"].ToString());
                        }
                        else
                        {
                            count = 0;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    count = 0;
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

            return count;
        }
        public static bool ReCreate(GRNBLL obj, SqlTransaction tran)
        {

            string strSql = "spReCreateGRN";
            SqlParameter[] arPar = new SqlParameter[14];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.ClientId;

            arPar[2] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = obj.CommodityGradeId;

            arPar[3] = new SqlParameter("@BagTypeId", SqlDbType.UniqueIdentifier);
            arPar[3].Value = obj.BagTypeId;

            arPar[4] = new SqlParameter("@GRNTypeId", SqlDbType.UniqueIdentifier);
            arPar[4].Value = obj.GRNTypeId;

            arPar[5] = new SqlParameter("@DateDeposited", SqlDbType.DateTime);
            arPar[5].Value = obj.DateDeposited;

            arPar[6] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[6].Value = (int)obj.Status;

            arPar[7] = new SqlParameter("@TotalNumberOfBags", SqlDbType.Int);
            arPar[7].Value = obj.TotalNumberOfBags;

            arPar[8] = new SqlParameter("@GrossWeight", SqlDbType.Float);
            arPar[8].Value = obj.GrossWeight;

            arPar[9] = new SqlParameter("@NetWeight", SqlDbType.Float);
            arPar[9].Value = obj.NetWeight;

            arPar[10] = new SqlParameter("@OriginalQuantity", SqlDbType.Float);
            arPar[10].Value = obj.OriginalQuantity;

            arPar[11] = new SqlParameter("@CurrentQuantity", SqlDbType.Float);
            arPar[11].Value = obj.CurrentQuantity;

            arPar[12] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[12].Value = obj.Remark;

            arPar[13] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[13].Value = UserBLL.GetCurrentUser();


            int affectedRow = 0;

            affectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (affectedRow == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static String[] GetParentCodeBylistTrackingNo(string TrackingNo)
        {
            string strSql = "Select CommodityDepositRequest, TrackingNo,tblCommodityDepositRequest.TransactionId as MainTracking from vPendingGRNCreation ";
            strSql += " inner join tblCommodityDepositRequest on vPendingGRNCreation.CommodityDepositRequest = tblCommodityDepositRequest.Id ";
            strSql += " where GradingCode not in ( select GradingCode from tblGRN where Status<>3) and  TrackingNo in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["MainTracking"].ToString() + " * " + reader["TrackingNo"].ToString());
                }
                reader.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list.ToArray();
            }
            return null;
        }
        public static String[] GetGRNNoBylistTrackingNoForGRN(string TrackingNo)
        {
            string strSql = "select GRN_Number,TrackingNo from VGRN ";
            strSql += " where  TrackingNo in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["GRN_Number"].ToString() + " * " + reader["TrackingNo"].ToString());
                }
                reader.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list.ToArray();
            }
            return null;
        }
        public static String[] GetGRNNoBylistEditTrackingNoForGRN(string TrackingNo)
        {
            string strSql = " select GRN_Number,tblRequestApprovedGRNEdit.TrackingNo from VGRN ";
            strSql += " inner join tblRequestApprovedGRNEdit on tblRequestApprovedGRNEdit.GRNId= VGRN.iD ";
            strSql += " where  tblRequestApprovedGRNEdit.TrackingNo in (" + TrackingNo + ")";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            SqlConnection conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(reader["GRN_Number"].ToString() + " * " + reader["TrackingNo"].ToString());
                }
                reader.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return list.ToArray();
            }
            return null;
        }
        public static bool IsEditableGRN(string whereClause)
        {


            string strSql = "select Count(Id) from  tblGRN where ( Status<>7  ) and ";
            if (whereClause != "")
            {
                int count = 1;
                SqlConnection conn = Connection.getConnection();
                try
                {

                    strSql += " " + whereClause;
                    count = (int)SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql);
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
                if (count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        public static bool IsEditableGRNByTrackingNo(string TrackingNo)
        {


            string strSql = "select Count(Id) from  vGRN where Status <>7 and TransactionId='" + TrackingNo + "'";
            int count = 1;
            SqlConnection conn = Connection.getConnection();
            try
            {
                count = (int)SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpDateGRNNumber(Guid Id, String oldGRNNumber, string newGRNNumber, SqlTransaction tran)
        {
            string strSql = "spUpdateGRNNumber";
            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            arPar[1] = new SqlParameter("@GRN_Number", SqlDbType.NVarChar, 50);
            arPar[1].Value = oldGRNNumber;

            arPar[2] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[2].Value = UserBLL.GetCurrentUser();

            int affectedRow = 0;

            affectedRow = SqlHelper.ExecuteNonQuery(tran, strSql, arPar);
            if (affectedRow == 1)
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
