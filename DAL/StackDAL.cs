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
   
    public class StackDAL : GeneralBLL
    {
        private Guid _id;
        private Guid _shedId;
        private int _stackNumber;
        private StackStatus _status;
        private DateTime _dateStarted;
        #region properties
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Guid ShedId
        {
            get { return _shedId; }
            set { _shedId = value; }
        }
        public int StackNumber
        {
            get { return _stackNumber; }
            set { _stackNumber = value; }
        }
        public StackStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public DateTime DateStarted
        {
            get { return _dateStarted; }
            set { _dateStarted = value; }
        }
        #endregion
   
        public static List<StackBLL> GetActiveStackByShed(Guid Id )
        {
            string strSql = "spGetActiveStackByShedId";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[1];
            List<StackBLL> list = null;
            arPar[0] = new SqlParameter("@ShedId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = Id;
            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<StackBLL>();
                    while (reader.Read())
                    {
                        StackBLL obj = new StackBLL();
                        if(reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        else 
                        {
                            throw new Exception("Invalid Id");
                        }

                        if(reader["ShedId"] != DBNull.Value )
                        {
                        obj.ShedId = new Guid(reader["ShedId"].ToString());
                        }
                        else
                        {
                            throw new Exception("Invalid Shed");
                        }
                                     
                        if(reader["CommodityGradeId"] != DBNull.Value )
                        {
                        obj.CommodityGradeid = new Guid(reader["CommodityGradeId"].ToString());
                        }
                        else
                        {
                            throw new Exception("Invalid Commodity Grade");
                        }                      
                        obj.StackNumber = reader["StackNumber"].ToString();
                        if(reader["Status"] != DBNull.Value )
                        {
                            obj.Status = (StackStatus)Convert.ToInt32(reader["Status"].ToString());
                        }
                        else
                        {
                            throw new Exception("Invalid Status");
                        }
             
                        if(reader["DateStarted"] != DBNull.Value )
                        {
                            obj.DateStarted = Convert.ToDateTime(reader["DateStarted"].ToString());
                        }
                        else
                        {
                            throw new Exception("Invalid Date Statrted");
                        }
                        if (reader["PhysicalAddress"] != DBNull.Value)
                        {
                            obj.PhysicalAddress = int.Parse(reader["PhysicalAddress"].ToString());
                        }
                        if (reader["productionYear"] != DBNull.Value)
                        {
                            obj.ProductionYear = int.Parse(reader["productionYear"].ToString());
                        }

                       

                        list.Add(obj);


                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if( conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }
        public static bool InsertStack( StackBLL obj , SqlTransaction tran)
        {
            int Affectedrow = 0;
            string strSql = "spInsertStack";
            
            SqlParameter[] arPar = new SqlParameter[11];
            try
            {
                arPar[0] = new SqlParameter("@ShedId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.ShedId;

                arPar[1] = new SqlParameter("@CommodityGradeId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.CommodityGradeid;

                arPar[2] = new SqlParameter("@StackNumber", SqlDbType.NVarChar,50);
                arPar[2].Value = obj.StackNumber;

                arPar[3] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[3].Value =(int)obj.Status;

                arPar[4] = new SqlParameter("@DateStarted", SqlDbType.DateTime);
                arPar[4].Value = obj.DateStarted;

                arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[5].Value = UserBLL.GetCurrentUser();

                arPar[6] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[6].Value =obj.Id;

                arPar[7] = new SqlParameter("@PhysicalAddress", SqlDbType.Int);
                arPar[7].Value = obj.PhysicalAddress;

                arPar[8] = new SqlParameter("@BeginingBalance", SqlDbType.Int);
                arPar[8].Value = obj.BeginingNoBags;

                arPar[9] = new SqlParameter("@warehouseId", SqlDbType.UniqueIdentifier);
                arPar[9].Value = obj.WarehouseId;

                //productionyearstack
                arPar[10] = new SqlParameter("@productionYear", SqlDbType.Int);
                arPar[10].Value = obj.ProductionYear;

               
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
        public static List<StackBLL> Search(Nullable<Guid> ShedId, Nullable<Guid> CommodityGradeId, String StackNumber)
        {
            string strSql = SearchHelper(ShedId, CommodityGradeId, StackNumber);
            List<StackBLL> list;
            SqlDataReader reader;
            SqlConnection conn = Connection.getConnection();
            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Invalid database connection.");
            }
            reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            if (reader.HasRows)
            {
                list = new List<StackBLL>();
                while (reader.Read())
                {
                   
                    StackBLL obj = new StackBLL();
                    if (reader["Id"] != null)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    if (reader["ShedId"] != DBNull.Value)
                    {
                        obj.ShedId = new Guid( reader["ShedId"].ToString());
                    }
                    if (reader["CommodityGradeId"] != DBNull.Value)
                    {
                        obj.CommodityGradeid = new Guid(reader["CommodityGradeId"].ToString());
                    }
                    if (reader["StackNumber"] != DBNull.Value)
                    {
                        obj.StackNumber = reader["StackNumber"].ToString();
                    }
                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status =(StackStatus) int.Parse(reader["Status"].ToString());
                    }
                    if (reader["DateStarted"] != DBNull.Value)
                    {
                        obj.DateStarted = DateTime.Parse(reader["DateStarted"].ToString());
                    }
                    if (reader["PhysicalAddress"] != DBNull.Value)
                    {
                        obj.PhysicalAddress = int.Parse(reader["PhysicalAddress"].ToString());
                    }
                    list.Add(obj);

                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return list;
            }
            else
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return null;
            }

        }
        private static string SearchHelper(Nullable<Guid> ShedId, Nullable<Guid> CommodityGradeId, String StackNumber)
        {
            string strSql = "select Id,ShedId,CommodityGradeId,StackNumber,Status,DateStarted,PhysicalAddress from tblStack ";
            string strWhere = " where ";
            if (ShedId != null)
            {
                strWhere += "ShedId='" + ShedId.ToString() + "' ";
            }
            if (CommodityGradeId != null)
            {
                if (strWhere == " where ")
                {
                    strWhere += " CommodityGradeId='" + CommodityGradeId.ToString() + "' ";
                }
                else
                {
                    strWhere += " and CommodityGradeId='" + CommodityGradeId.ToString() + "' ";
                }
            }
            if (string.IsNullOrEmpty(StackNumber) != true)
            {
                if (strWhere == " where ")
                {
                    strWhere += " StackNumber='" + StackNumber.ToString() + "' ";
                }
                else
                {
                    strWhere += " and StackNumber='" + StackNumber.ToString() + "' ";
                }
            }
            if (strWhere == " where ")
            {
                return strSql;
            }
            else
            {
                return strSql + strWhere;
            }

        }
    }
}
