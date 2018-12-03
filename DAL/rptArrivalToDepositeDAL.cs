using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class rptArrivalToDepositeDAL
    {
        public static List<rptArrivalToDepositeBLL> GetReportData(Guid WarehouseId, DateTime from)
        {
            string strSql = "spArrivalToDepositeRPT";
            List<rptArrivalToDepositeBLL> list = null;
            SqlParameter[] arPar = new SqlParameter[2];
            SqlDataReader reader;
            arPar[0] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;
            arPar[1] = new SqlParameter("@ArrivalDate", SqlDbType.DateTime);
            arPar[1].Value = from;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                list = new List<rptArrivalToDepositeBLL>();
                while (reader.Read())
                {
                    rptArrivalToDepositeBLL obj = new rptArrivalToDepositeBLL();
                    obj.VoucherNo = reader["VoucherNo"].ToString();
                    if (reader["VoucherNo"] != DBNull.Value)
                    {
                        obj.ClientId = new Guid(reader["ClientId"].ToString());
                    }
                    obj.PlateNo = reader["PlateNumber"].ToString();
                    obj.TrailerPlateNo = reader["TrailerPlateNumber"].ToString();
                    if (reader["TotalNumberOfBags"] != DBNull.Value)
                    {
                        obj.NoBags = int.Parse(reader["TotalNumberOfBags"].ToString());
                    }
                    if (reader["ArrivalDate"] != DBNull.Value)
                    {
                        obj.ArrivalDate = DateTime.Parse(reader["ArrivalDate"].ToString());
                    }
                    if (reader["DateDeposited"] != DBNull.Value)
                    {
                        obj.unloadedDate = DateTime.Parse(reader["DateDeposited"].ToString());
                    }
                    if (reader["WarehouseId"] != DBNull.Value)
                    {
                        obj.WarehouseId = new Guid(reader["WarehouseId"].ToString());
                    }
                    list.Add(obj);
                }
                conn.Close();
                return list;
            }
            else
            {
                conn.Close();
                return null;
            }
            return list;
        }
    }
}
