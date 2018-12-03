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
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class Voucher
    {



        public Guid InsertVoucherInformation(VoucherInformationBLL obj , SqlTransaction tran)
        {
            string strSql = "spInsertVoucherInformation";
            SqlConnection conn = Connection.getConnection();
            SqlParameter[] arPar = new SqlParameter[12];

            arPar[0] = new SqlParameter("@DepositRequestId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.DepositRequestId;

            arPar[1] = new SqlParameter("@VoucherNo", SqlDbType.NVarChar, 50);
            arPar[1].Value = obj.VoucherNo;

            arPar[2] = new SqlParameter("@CoffeeTypeId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = obj.CoffeeTypeId;

            arPar[3] = new SqlParameter("@SpecificArea", SqlDbType.NVarChar);
            arPar[3].Value = obj.SpecificArea;

            arPar[4] = new SqlParameter("@NumberOfBags", SqlDbType.Int);
            arPar[4].Value = obj.NumberofBags;

            arPar[5] = new SqlParameter("@NumberOfPlomps", SqlDbType.Int);
            arPar[5].Value = obj.NumberOfPlomps;

            arPar[6] = new SqlParameter("@NumberOfPlompsTrailer", SqlDbType.Int);
            arPar[6].Value = obj.NumberOfPlompsTrailer;

            arPar[7] = new SqlParameter("@CertificateNo", SqlDbType.NVarChar, 50);
            arPar[7].Value = obj.CertificateNo;

            arPar[8] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
            arPar[8].Value = obj.CreatedBy;

            arPar[9] = new SqlParameter("@CreatedDate", SqlDbType.DateTime );
            arPar[9].Value = DateTime.Now;

            arPar[10] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[10].Value = obj.Status;

            arPar[11] = new SqlParameter("@VoucherId", SqlDbType.UniqueIdentifier);
            arPar[11].Direction = ParameterDirection.Output;

            int AffectedRows;
            Guid VoucherId = Guid.Empty;
            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
                if( AffectedRows == -1)
                {
                    
                    VoucherId = new Guid(arPar[11].Value.ToString());
                    
                }
            }
            catch ( Exception ex)
            {
                throw new Exception("Unable to insert the database.", ex);
            }
           return VoucherId;
            
        }
        public bool UpdateVoucherInformation(VoucherInformationBLL obj, SqlTransaction tran)
        {
            string strSql = "spUpdateVoucherInformation";
         
            SqlParameter[] arPar = new SqlParameter[10];

            // for Selecting the for Update 
            arPar[0] = new SqlParameter("@VoucherId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@VoucherNo", SqlDbType.NVarChar, 50);
            arPar[1].Value = obj.VoucherNo;


            arPar[2] = new SqlParameter("@CoffeeTypeId", SqlDbType.UniqueIdentifier);
            arPar[2].Value = obj.CoffeeTypeId;

            arPar[3] = new SqlParameter("@SpecificArea", SqlDbType.NVarChar);
            arPar[3].Value = obj.SpecificArea;

            arPar[4] = new SqlParameter("@NumberOfBags", SqlDbType.Int);
            arPar[4].Value = obj.NumberofBags;

            arPar[5] = new SqlParameter("@NumberOfPlomps", SqlDbType.Int);
            arPar[5].Value = obj.NumberOfPlomps;

            arPar[6] = new SqlParameter("@NumberOfPlompsTrailer", SqlDbType.Int);
            arPar[6].Value = obj.NumberOfPlompsTrailer;

            arPar[7] = new SqlParameter("@CertificateNo", SqlDbType.NVarChar);
            arPar[7].Value = obj.CertificateNo;

            arPar[8] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[8].Value = obj.LastModifiedBy;

            arPar[9] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[9].Value = obj.Status;

            int AffectedRows;
            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
            }
            catch
            {
                throw new Exception("Can not update database.");
            }
            return AffectedRows == 1;

        }


        public DataSet getVoucherInformation(Guid CommodityDepositRequestId)
        {
            string strSql = "spGetVoucherInformationByDepositRequestId";
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier );
            arPar[0].Value = CommodityDepositRequestId;

            DataSet dsResult = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsResult = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);

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
          
            return dsResult;
        }
        public static VoucherInformationBLL getVoucherInformationById(Guid Id)
        {
            VoucherInformationBLL objVoucherInformation = null;
            string strSql = "spGetVoucherInformationById";
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            DataSet dsResult = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsResult = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
                if (dsResult.Tables[0].Rows.Count == 1)
                {
                    objVoucherInformation = new VoucherInformationBLL();
                    if (dsResult.Tables[0].Rows[0]["VoucherId"] != null)
                    {
                        objVoucherInformation.Id = new Guid(dsResult.Tables[0].Rows[0]["VoucherId"].ToString());
                    }
                    if (dsResult.Tables[0].Rows[0]["DepositRequestId"] != null)
                    {
                        objVoucherInformation.DepositRequestId = new Guid(dsResult.Tables[0].Rows[0]["DepositRequestId"].ToString());
                    }
                    objVoucherInformation.VoucherNo = dsResult.Tables[0].Rows[0]["VoucherNo"].ToString();
                    if (dsResult.Tables[0].Rows[0]["CoffeeTypeId"] != null)
                    {
                        objVoucherInformation.CoffeeTypeId = new Guid(dsResult.Tables[0].Rows[0]["CoffeeTypeId"].ToString());
                    }
                    objVoucherInformation.SpecificArea = dsResult.Tables[0].Rows[0]["SpecificArea"].ToString();
                    if (dsResult.Tables[0].Rows[0]["NumberOfBags"] != null)
                    {
                        objVoucherInformation.NumberofBags = int.Parse(dsResult.Tables[0].Rows[0]["NumberOfBags"].ToString());
                    }
                    if (dsResult.Tables[0].Rows[0]["NumberOfPlomps"] != null)
                    {
                        objVoucherInformation.NumberOfPlomps = int.Parse(dsResult.Tables[0].Rows[0]["NumberOfPlomps"].ToString());
                    }
                    if (dsResult.Tables[0].Rows[0]["NumberOfPlompsTrailer"] != null)
                    {
                        objVoucherInformation.NumberOfPlompsTrailer = int.Parse(dsResult.Tables[0].Rows[0]["NumberOfPlompsTrailer"].ToString());
                    }
                    objVoucherInformation.CertificateNo = dsResult.Tables[0].Rows[0]["CertificateNo"].ToString();
                }

            }
            catch (Exception e)
            {
                throw e;
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
            
            return objVoucherInformation;
        }
        public static VoucherInformationBLL getVoucherInformationByDepositRequestId(Guid Id)
        {
            VoucherInformationBLL objVoucherInformation = null;
            string strSql = "spGetVoucherInformationByDepositRequestId";
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            DataSet dsResult = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsResult = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
                if (dsResult.Tables[0].Rows.Count == 1)
                {
                    objVoucherInformation = new VoucherInformationBLL();
                    if (dsResult.Tables[0].Rows[0]["VoucherId"] != null)
                    {
                        objVoucherInformation.Id = new Guid(dsResult.Tables[0].Rows[0]["VoucherId"].ToString());
                    }
                    if (dsResult.Tables[0].Rows[0]["DepositRequestId"] != null)
                    {
                        objVoucherInformation.DepositRequestId = new Guid(dsResult.Tables[0].Rows[0]["DepositRequestId"].ToString());
                    }
                    objVoucherInformation.VoucherNo = dsResult.Tables[0].Rows[0]["VoucherNo"].ToString();
                    if (dsResult.Tables[0].Rows[0]["CoffeeTypeId"] != null)
                    {
                        objVoucherInformation.CoffeeTypeId = new Guid(dsResult.Tables[0].Rows[0]["CoffeeTypeId"].ToString());
                    }
                    objVoucherInformation.SpecificArea = dsResult.Tables[0].Rows[0]["SpecificArea"].ToString();
                    if (dsResult.Tables[0].Rows[0]["NumberOfBags"] != null)
                    {
                        objVoucherInformation.NumberofBags = int.Parse(dsResult.Tables[0].Rows[0]["NumberOfBags"].ToString());
                    }
                    if (dsResult.Tables[0].Rows[0]["NumberOfPlomps"] != null)
                    {
                        objVoucherInformation.NumberOfPlomps = int.Parse(dsResult.Tables[0].Rows[0]["NumberOfPlomps"].ToString());
                    }
                    if (dsResult.Tables[0].Rows[0]["NumberOfPlompsTrailer"] != null)
                    {
                        objVoucherInformation.NumberOfPlompsTrailer = int.Parse(dsResult.Tables[0].Rows[0]["NumberOfPlompsTrailer"].ToString());
                    }
                    objVoucherInformation.CertificateNo = dsResult.Tables[0].Rows[0]["CertificateNo"].ToString();
                }

            }
            catch (Exception e)
            {
                throw e;
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
         

            return objVoucherInformation;
        }
        
    }
}
