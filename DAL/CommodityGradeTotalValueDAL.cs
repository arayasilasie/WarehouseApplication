using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using WarehouseApplication.BLL;
using Microsoft.ApplicationBlocks.Data;

namespace WarehouseApplication.DAL
{
    public class CommodityGradeTotalValueDAL
    {
        public static CommodityGradeTotalValueBLL GetByCommodityGradeId(Guid CGId)
        {
            string strSql = "GetCommodityGradeFactorValueByCGid";
            CommodityGradeTotalValueBLL obj = null;
            SqlDataReader reader;
            SqlConnection conn = null;
            SqlParameter[] arPar = new SqlParameter[1];
            arPar[0] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = CGId;
            conn = Connection.getConnection();
            reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
            if (reader.HasRows)
            {
                obj = new CommodityGradeTotalValueBLL();
                if (reader.Read())
                {
                    obj.Id = new Guid(reader["Id"].ToString());
                    obj.CommodityGradeId = new Guid(reader["CommodityGradeId"].ToString());
                    obj.MaxValue = float.Parse(reader["MaxValue"].ToString());
                    obj.MinValue = float.Parse(reader["MinValue"].ToString());
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (CGTotalValueStatus)int.Parse(reader["Status"].ToString());
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
                    return obj;

                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        public static bool Update(CommodityGradeTotalValueBLL obj, SqlTransaction tran)
        {
            string strSql = "spUpdateCommodityGradeTotalValue";

            SqlParameter[] arPar = new SqlParameter[6];

            arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            arPar[0].Value = obj.Id;

            arPar[1] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
            arPar[1].Value = obj.CommodityGradeId;

            arPar[2] = new SqlParameter("@MaxValue", SqlDbType.Float);
            arPar[2].Value = obj.MaxValue;

            arPar[3] = new SqlParameter("@MinValue", SqlDbType.Float);
            arPar[3].Value = obj.MinValue;

            arPar[4] = new SqlParameter("@Status", SqlDbType.TinyInt);
            arPar[4].Value = (int)obj.Status;

            arPar[5] = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            arPar[5].Value = UserBLL.GetCurrentUser();

            int AffectedRows = -1;
            AffectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);

            return AffectedRows == 1;




        }
    }
}
