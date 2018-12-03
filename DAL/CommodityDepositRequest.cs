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
    public class CommodityDepositRequest
    {

        /// <summary>
        /// InsertCommodityDepositeRequest 
        /// </summary>
        /// <param name="objCommDepoReq"></param>
        /// <returns>affected No</returns>
        public Guid  InsertCommodityDepositeRequest( CommodityDepositeRequestBLL objCommDepoReq , SqlTransaction tran )
        {
            string strSql = "spInsertCommodityDepositeRequest";
            
            SqlParameter[] arPar = new SqlParameter[14];

            arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[0].Value = objCommDepoReq.TransactionId ;

            arPar[1] = new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = objCommDepoReq.ClientId ;

            arPar[2] = new SqlParameter("@CommodityId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = objCommDepoReq.CommodityId; ;

            arPar[3] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[3].Value = objCommDepoReq.WarehouseId ;

            arPar[4] = new SqlParameter("@RepresentativeId", SqlDbType.UniqueIdentifier);
            arPar[4].Value = objCommDepoReq.RepresentativeId ;

            arPar[5] = new SqlParameter("@WoredaId", SqlDbType.UniqueIdentifier);
            if (objCommDepoReq.WoredaId != Guid.Empty)
            {
                arPar[5].Value = objCommDepoReq.WoredaId;
            }
            else
            {
                arPar[5].Value = null;
            }

            arPar[6] = new SqlParameter("@productionYear", SqlDbType.Int);
            if (objCommDepoReq.ProductionYear != 0)
            {
                arPar[6].Value = objCommDepoReq.ProductionYear;
            }
            else
            {
                arPar[6].Value = null; 
            }

            arPar[7] = new SqlParameter("@Status", SqlDbType.Int  );
            arPar[7].Value = objCommDepoReq.Status ;

            arPar[8] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[8].Value = objCommDepoReq.CreatedBy ;

         

            arPar[9] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[9].Value = objCommDepoReq.Remark ;

            arPar[10] = new SqlParameter("@Weight", SqlDbType.Decimal);
            arPar[10].Value = objCommDepoReq.Weight ;

            arPar[11] = new SqlParameter("@NumberofBags", SqlDbType.Int);
            arPar[11].Value = objCommDepoReq.NumberofBags ;

            arPar[12] = new SqlParameter("@DateTimeRecived", SqlDbType.DateTime);
            arPar[12].Value = objCommDepoReq.DateTimeRecived ;
            int AffectedRows;

            arPar[13] = new SqlParameter("@CommodityDepositRequestId", SqlDbType.UniqueIdentifier);
            arPar[13].Direction = ParameterDirection.Output;
            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            }
            catch( Exception ex)
            {
                throw new Exception("Can not update the database.", ex);
            }
            Guid CommodityDepositRequestId = new Guid();
            CommodityDepositRequestId = (Guid)arPar[13].Value;
            return CommodityDepositRequestId;
        }

        public static  int UpdateCommodityDepositeRequest(CommodityDepositeRequestBLL objCommDepoReq , SqlTransaction tran )
        {
            
            int AffectedRows;
            string strSql = "spEditCommodityDepositeRequest";
            SqlParameter[] arPar = new SqlParameter[12];

            arPar[0] = new SqlParameter("@CommodityDepositRequestId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = objCommDepoReq.Id ;

            arPar[1] = new SqlParameter("@Commodity", SqlDbType.UniqueIdentifier);
            arPar[1].Value = objCommDepoReq.CommodityId ;

            arPar[2] = new SqlParameter("@RepresentativeId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = objCommDepoReq.RepresentativeId ;

            arPar[3] = new SqlParameter("@ProductionYear", SqlDbType.Int);
            arPar[3].Value = objCommDepoReq.ProductionYear ;

            arPar[4] = new SqlParameter("@Woreda", SqlDbType.UniqueIdentifier);
            arPar[4].Value = objCommDepoReq.WoredaId ;

            arPar[5] = new SqlParameter("@Weight", SqlDbType.Float);
            arPar[5].Value = objCommDepoReq.Weight ;

            arPar[6] = new SqlParameter("@NumberofBags", SqlDbType.Int);
            arPar[6].Value = objCommDepoReq.NumberofBags ;

            arPar[7] = new SqlParameter("@DateTimeRecived", SqlDbType.DateTime);
            arPar[7].Value = objCommDepoReq.DateTimeRecived ;

            arPar[8] = new SqlParameter("@Remark", SqlDbType.Text);
            arPar[8].Value = objCommDepoReq.Remark ;

            arPar[9] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[9].Value = objCommDepoReq.Status ;

            arPar[10] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[10].Value = objCommDepoReq.LastModifiedBy  ;

            arPar[11] = new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier);
            arPar[11].Value = objCommDepoReq.ClientId;

            AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);  
            return AffectedRows;

        }




        public static List<CommodityDepositeRequestBLL> SearchCommodityDeposite(string TrackingNo, string VoucherNo, Nullable<Guid> Clientid,  Nullable<Guid> CommodityId, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            List<CommodityDepositeRequestBLL> list = new List<CommodityDepositeRequestBLL>();
            string strSql = SearchHelper(TrackingNo, VoucherNo, Clientid, CommodityId, from, to);
            DataSet  dsCommodityDepositeRequest = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsCommodityDepositeRequest = SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql);
                if (dsCommodityDepositeRequest.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; dsCommodityDepositeRequest.Tables[0].Rows.Count > i; i++ )
                    {
                        DataRow row;
                        row = dsCommodityDepositeRequest.Tables[0].Rows[i];
                        CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
                        if (row["Id"] != null)
                        {
                            obj.Id = new Guid(row["Id"].ToString());
                        }
                        obj.TrackingNo = row["TransactionId"].ToString();
                        if (row["ClientId"] != null)
                        {
                            obj.ClientId = new Guid(row["ClientId"].ToString());
                        }
                        if (row["VoucherNo"] != null)
                        {
                            obj.VoucherNo = row["VoucherNo"].ToString();
                        }
                        if (row["CommodityId"] != null)
                        {
                            obj.CommodityId = new Guid(row["CommodityId"].ToString());
                        }
                        if (row["DateTimeRecived"] != null)
                        {
                            obj.DateTimeRecived = Convert.ToDateTime(row["DateTimeRecived"].ToString());
                        }

                        list.Add(obj);
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
                conn.Close();
            }
           
        }

        public DataSet getCommodityDepositRequestById(Guid Id)
        {
            string strSql = "spGetCommodityDepositeRequestById";
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            DataSet dsCommodityDepositeRequest = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsCommodityDepositeRequest = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
                int x = dsCommodityDepositeRequest.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ( conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dsCommodityDepositeRequest;
        }
        public DataSet getCommodityDepositRequestByTrackingNo(string TrackingNo)
        {
            string strSql = "spGetCommodityDepositeRequestByTrackingNo";
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@TransactionId", SqlDbType.NVarChar, 50);
            arPar[0].Value = TrackingNo;

            DataSet dsCommodityDepositeRequest = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsCommodityDepositeRequest = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ( conn.State == ConnectionState.Open )
                conn.Close();
            }
            return dsCommodityDepositeRequest;
        }
        private static string SearchHelper(string TrackingNo, string VoucherNo, Nullable<Guid> Clientid, Nullable<Guid> CommodityId, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            string strSql = "select Id,TransactionId,ClientId,VoucherNo,CommodityId,DateTimeRecived from vCommodityDeposite where (WarehouseId='"+ UserBLL.GetCurrentWarehouse().ToString()+"') and" ;
            string strWhere = "";
            if (TrackingNo != "")
            {
                if (strWhere == "")
                {
                    strWhere += "  TransactionId='" + TrackingNo + "' ";
                }
            }
            if (VoucherNo != "")
            {
                if (strWhere == "")
                {
                    strWhere += "  VoucherNo='" + VoucherNo + "' ";
                }
                else
                {
                    strWhere += " or VoucherNo='" + VoucherNo + "' ";
                }
            }

            if (Clientid != null)
            {
                if (strWhere == "")
                {
                    strWhere += "  ClientId='" + Clientid + "' ";
                }
                else
                {
                    strWhere += " or ClientId='" + Clientid + "' ";
                }
            }

            if (CommodityId != null)
            {
                if (strWhere == "")
                {
                    strWhere += "  CommodityId='" + CommodityId + "' ";
                }
                else
                {
                    strWhere += " or CommodityId='" + CommodityId + "' ";
                }
            }
            if (from != null)
            {
                if (strWhere == "")
                {
                    strWhere += "  DateTimeRecived > '" + from + "' ";
                }
                else
                {
                    strWhere += " or DateTimeRecived > '" + from + "' ";
                }
            }
            if (to != null)
            {
                if (strWhere == "")
                {
                    strWhere += "  DateTimeRecived < '" + to + "' ";
                }
                else
                {
                    strWhere += " or DateTimeRecived< '" + to + "' ";
                }
            }
            strWhere = " (" + strWhere + ") and WarehouseId='" + UserBLL.GetCurrentWarehouse().ToString() + "' ";
            return strSql + strWhere;

        }
    }
}
