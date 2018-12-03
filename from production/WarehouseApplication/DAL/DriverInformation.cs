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
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;
using System.Collections;
using System.Collections.Generic;


namespace WarehouseApplication.DAL
{
    public class DriverInformation
    {
        public Guid InsertDriverInformation(DriverInformationBLL ObjDriverInfo ,SqlTransaction tran  )
        {

            string strSql = "spInsertDriverInformation";
            SqlParameter[] arPar = new SqlParameter[9];

            arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier );
            arPar[0].Value = ObjDriverInfo.ReceivigRequestId ;

            arPar[1] = new SqlParameter("@DriverName", SqlDbType.NVarChar,50);
            arPar[1].Value = ObjDriverInfo.DriverName ;

            arPar[2] = new SqlParameter("@LicenseNumber", SqlDbType.NVarChar,50);
            arPar[2].Value = ObjDriverInfo.LicenseNumber ;

            arPar[3] = new SqlParameter("@LicenseIssuedPlace", SqlDbType.NVarChar,50);
            arPar[3].Value = ObjDriverInfo.LicenseIssuedPlace ;

            arPar[4] = new SqlParameter("@PlateNumber", SqlDbType.NVarChar,50);
            arPar[4].Value = ObjDriverInfo.PlateNumber ;

            arPar[5] = new SqlParameter("TrailerPlateNumber", SqlDbType.NVarChar,50);
            arPar[5].Value = ObjDriverInfo.TrailerPlateNumber ;

            arPar[6] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier );
            arPar[6].Value = ObjDriverInfo.CreatedBy ;


            arPar[7] = new SqlParameter("@Status", SqlDbType.Int );
            arPar[7].Value = 1 ;

            arPar[8] = new SqlParameter("@DriverInformationId", SqlDbType.UniqueIdentifier);
            arPar[8].Direction = ParameterDirection.Output;


            
            int AffectedRows;
            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
                Guid DriverInformationId = Guid.Empty;
                DriverInformationId = new Guid(arPar[8].Value.ToString());
                return DriverInformationId;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to Insert Data", e);

            }
            

            
        }
        public bool UpdateDriverInformation(DriverInformationBLL ObjDriverInfo, SqlTransaction  conn)
        {
            string strSql = "spUpdateDriverInformation";
            SqlParameter[] arPar = new SqlParameter[9];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = ObjDriverInfo.Id ;

            arPar[1] = new SqlParameter("@DriverName", SqlDbType.NVarChar , 50 );
            arPar[1].Value = ObjDriverInfo.DriverName;

            arPar[2] = new SqlParameter("@LicenseNumber", SqlDbType.NVarChar, 50);
            arPar[2].Value = ObjDriverInfo.LicenseNumber;

            arPar[3] = new SqlParameter("@LicenseIssuedPlace", SqlDbType.NVarChar, 50);
            arPar[3].Value = ObjDriverInfo.LicenseIssuedPlace;

            arPar[4] = new SqlParameter("@PlateNumber", SqlDbType.NVarChar, 50);
            if (ObjDriverInfo.PlateNumber == null)
            {
                //arPar[4].Value = DBNull ;
   
            }
            else
            {
                arPar[4].Value = ObjDriverInfo.PlateNumber;
            }

            arPar[5] = new SqlParameter("@TrailerPlateNumber", SqlDbType.NVarChar, 50);
            if (ObjDriverInfo.TrailerPlateNumber == null)
            {
                //arPar[5].Value = DBNull;
            }
            else
            {
                arPar[5].Value = ObjDriverInfo.TrailerPlateNumber;
            }

            arPar[6] = new SqlParameter("@LastModifiedBy", SqlDbType.UniqueIdentifier);
            arPar[6].Value = ObjDriverInfo.LastModifiedBy;

            arPar[7] = new SqlParameter("@Remark", SqlDbType.Text);

            arPar[8] = new SqlParameter("@Status", SqlDbType.Int);
            arPar[8].Value = ObjDriverInfo.Status;

            if (ObjDriverInfo.Remark == null)
            {
                arPar[7].Value = String.Empty; ;
            }
            else
            {
                arPar[7].Value = ObjDriverInfo.Remark;
            }
            int AffectedRows = 0;
            try
            {
                AffectedRows = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                ErrorLogger.Log(e);
               // trans.Rollback();
                AffectedRows = 0;
            }
            return AffectedRows == 1;

        }
        public DataSet  GetDriverInformationByReceivigRequestId(Guid Id )
        {
            string strSql = "spGetDriverInformationByReceivigRequestId";

            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier );
            arPar[0].Value = Id;

            DataSet dsDriverInformation = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsDriverInformation = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                ErrorLogger.Log(e);
            }
            finally
            {
                conn.Close();
            }
            return dsDriverInformation;
        }
        public static List<DriverInformationBLL> GetActiveDriverInformationByReceivigRequestId(Guid ReceivigRequestId)
        {
            List<DriverInformationBLL> list = null;
            string strSql = "spGetDriverInformationByReceivigRequestId";
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = ReceivigRequestId;
            DataSet dsDriverInformation = null;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                dsDriverInformation = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
                if (dsDriverInformation.Tables[0].Rows.Count > 0)
                {
                    list = new List<DriverInformationBLL>();
                    for (int i = 0; i < dsDriverInformation.Tables[0].Rows.Count; i++)
                    {
                        DriverInformationBLL o = new DriverInformationBLL();
                        o.Id = new Guid(dsDriverInformation.Tables[0].Rows[i]["Id"].ToString());
                        o.ReceivigRequestId = new Guid(dsDriverInformation.Tables[0].Rows[i]["ReceivingRequestId"].ToString());
                        o.PlateNumber = dsDriverInformation.Tables[0].Rows[i]["PlateNumber"].ToString();
                        o.TrailerPlateNumber = dsDriverInformation.Tables[0].Rows[i]["TrailerPlateNumber"].ToString();
                        o.Status = Convert.ToInt32( dsDriverInformation.Tables[0].Rows[i]["Status"].ToString());
                        o.DriverName = dsDriverInformation.Tables[0].Rows[i]["DriverName"].ToString();
                        o.LicenseNumber = dsDriverInformation.Tables[0].Rows[i]["LicenseNumber"].ToString();
                        o.LicenseIssuedPlace = dsDriverInformation.Tables[0].Rows[i]["LicenseIssuedPlace"].ToString();
                        
                        if (o.Status == 2 || o.Status == 1)
                        {
                            list.Add(o);
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
                conn.Close();
            }
          
        }
        public static int GetUniqueCount(Guid ReceivingRequestId, string LicenseNumber, string LicenseIssuedPlace)
        {
            string strSql = "spGetDuplicateDriveInformation";
            SqlParameter[] arPar = new SqlParameter[3];

            arPar[0] = new SqlParameter("@ReceivingRequestId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = ReceivingRequestId;

            arPar[1] = new SqlParameter("@LicenseNumber", SqlDbType.NVarChar,50);
            arPar[1].Value = LicenseNumber;

            arPar[2] = new SqlParameter("@LicenseIssuedPlace", SqlDbType.NVarChar,50);
            arPar[2].Value = LicenseIssuedPlace;
            SqlDataReader reader;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    int count = 0;
                    reader.Read();
                    if(reader["TotalCount"] != null)
                    {
                        try
                        {
                            count = int.Parse((reader["TotalCount"].ToString()));
                            return count;
                        }
                        catch 
                        {
                            return 0;
                        }

                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch( Exception ex)
            {
                throw ex;
            }
            finally
            {
                if( conn != null )
                {
                    if( conn.State == ConnectionState.Open )
                        conn.Close();
                }
            }
            return 0;

        }
        public static DataSet GetDriverInformationById(Guid Id)
        {
            string strSql = "spGetDriverInformationById";

            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            DataSet dsDriverInformation = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsDriverInformation = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                throw new Exception("unable to Insert Data", e);
            }
            finally
            {
                conn.Close();
            }
            return dsDriverInformation;
        }
        public static DataSet GetDriverInformationByCommodityDepositRequestId(Guid Id)
        {
            string strSql = "spGetDriverInformationByCommodityDepositRequestId";

            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@ReceivigRequestId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;

            DataSet dsDriverInformation = null;
            SqlConnection conn = Connection.getConnection();
            try
            {
                dsDriverInformation = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, strSql, arPar);
            }
            catch (Exception e)
            {
                throw new Exception("unable to Insert Data", e);
            }
            finally
            {
                conn.Close();
            }
            return dsDriverInformation;
        }
    }
}

