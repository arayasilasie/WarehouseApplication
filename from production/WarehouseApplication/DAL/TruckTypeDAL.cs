using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class TruckTypeDAL 
    {
        public static List<TruckTypeBLL> GetAll()
        {
            string strSql = "spGetAllTruckType";
           
            SqlDataReader reader;
            SqlConnection conn = null;
            List<TruckTypeBLL> list;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql);
                if (reader.HasRows)
                {
                    list = new List<TruckTypeBLL>();
                    while (reader.Read())
                    {
                        TruckTypeBLL obj = new TruckTypeBLL();
                        if (reader["Id"] == DBNull.Value)
                        {
                            throw new InvalidIdException("Id Can't be null");
                        }
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.TruckTypeName = reader["TruckTypeName"].ToString();
                        obj.Status = (TruckStatus)(int.Parse(reader["Status"].ToString()));
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;
        }
        
    }
    public class TruckModelDAL
    {
        public static List<TruckModelBLL> GetAll()
        {
            string strSql = "spGetAllTruckModel";
            SqlDataReader reader;
            SqlConnection conn = null;
            List<TruckModelBLL> list;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql);
                if (reader.HasRows)
                {
                    list = new List<TruckModelBLL>();
                    while (reader.Read())
                    {
                        TruckModelBLL obj = new TruckModelBLL();
                        if (reader["Id"] == DBNull.Value)
                        {
                            throw new InvalidIdException("Id Can't be null");
                        }
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.TruckTypeId = new Guid(reader["TruckTypeId"].ToString());
                        obj.TruckModelName = reader["TruckModelName"].ToString();
                        obj.Status = (TruckStatus)int.Parse(reader["Status"].ToString());
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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }
    }
    public class TruckModelYearDAL
    {
        public static List<TruckModelYearBLL> GetAll()
        {
            string strSql = "spGetAllTruckModelYear";
            SqlDataReader reader;
            SqlConnection conn = null;
            List<TruckModelYearBLL> list;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql);
                if (reader.HasRows)
                {
                    list = new List<TruckModelYearBLL>();
                    while (reader.Read())
                    {
                        TruckModelYearBLL obj = new TruckModelYearBLL();
                        if (reader["Id"] == DBNull.Value)
                        {
                            throw new InvalidIdException("Id Can't be null");
                        }
                        obj.Id = new Guid(reader["Id"].ToString());
                        obj.TruckModelId = new Guid(reader["TruckModelId"].ToString());
                        obj.ModelYearName = reader["ModelYearName"].ToString();
                        obj.Status = (TruckStatus)reader["Status"];
                        if (reader["ModelWeight"] != DBNull.Value)
                        {
                            obj.ModelWeight = float.Parse((reader["ModelWeight"]).ToString());
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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }
    }
    public class TruckRegisterDAL
    {
        public static TruckRegisterBLL GetByTruckNumber(string TruckNumber, bool isTrailer)
        {
            TruckRegisterBLL obj = null;
            string strSql = "spGetTruckRegisterByTruckNumber";
            SqlConnection conn = null;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            try
            {


                arPar[0] = new SqlParameter("@TruckNumber", SqlDbType.NVarChar, 50);
                arPar[0].Value = TruckNumber;
                arPar[1] = new SqlParameter("@IsTrailer", SqlDbType.Bit);
                arPar[1].Value = isTrailer;
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    reader.Read();
                    obj = new TruckRegisterBLL();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    obj.TruckModelYearId = new Guid(reader["TruckModelYearId"].ToString());
                    obj.TruckNumber = reader["TruckNumber"].ToString();
                    if (reader["IsTrailer"] != DBNull.Value)
                    {
                        obj.IsTrailer = bool.Parse(reader["IsTrailer"].ToString());
                    }
                    else
                    {
                        obj.IsTrailer = false;
                    }
                    obj.Status = (TruckStatus)int.Parse(reader["Status"].ToString());
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
                        obj.CreatedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                    }


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
        public static bool Save(TruckRegisterBLL obj, SqlTransaction tran)
        {
            string strSql = "spInsertTruckRegister";
            SqlParameter[] arPar = new SqlParameter[6];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@TruckModelYearId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.TruckModelYearId;

                arPar[2] = new SqlParameter("@TruckNumber", SqlDbType.NVarChar,50);
                arPar[2].Value = obj.TruckNumber;

                arPar[3] = new SqlParameter("@IsTrailer", SqlDbType.Bit);
                arPar[3].Value = obj.IsTrailer;

                arPar[4] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[4].Value = (int)obj.Status;

                arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[5].Value = obj.CreatedBy;

                int Affeccted = -1;

                Affeccted = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);
             
                if (Affeccted == 1)
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

                throw new Exception("Can not insert Grading Result", ex);

            }
        }
    }
    public class TruckWeightDAL
    {
        public static bool Insert(TruckWeight obj , SqlTransaction tran)
        {
            string strSql = "spInsertTruckWeight";
            SqlParameter[] arPar = new SqlParameter[8];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@TruckPlateNo", SqlDbType.NVarChar,50);
                if (string.IsNullOrEmpty(obj.TruckPlateNo) == true)
                {
                    arPar[1].Value = DBNull.Value;
                }
                else
                {
                    arPar[1].Value = obj.TruckPlateNo;
                }

                arPar[2] = new SqlParameter("@TrailerPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(obj.TrailerPlateNo) == true)
                {
                    arPar[2].Value = DBNull.Value;
                }
                else
                {
                    arPar[2].Value = obj.TrailerPlateNo;
                }


                arPar[3] = new SqlParameter("@hasTrailer", SqlDbType.Int);
                arPar[3].Value = (int)obj.hasTrailer;

                arPar[4] = new SqlParameter("@Weight", SqlDbType.Float);
                arPar[4].Value = obj.Weight;

                arPar[5] = new SqlParameter("@DateWeighed", SqlDbType.DateTime);
                arPar[5].Value = obj.DateWeighed;

                arPar[6] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[6].Value = (int)obj.Status;

                arPar[7] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[7].Value = obj.CreatedBy;

                int Affeccted = -1;

                Affeccted = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, strSql, arPar);

                if (Affeccted == 1)
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

                throw new Exception("Unable to Add Truck Weight", ex);

            }
           
        }

        public static bool InsertNew(TruckWeight obj)
        {
            DataAccessPoint dal = new DataAccessPoint();
            string strSql = "spInsertTruckWeight";
            SqlParameter[] arPar = new SqlParameter[8];
            try
            {
                arPar[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.Id;

                arPar[1] = new SqlParameter("@TruckPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(obj.TruckPlateNo) == true)
                {
                    arPar[1].Value = DBNull.Value;
                }
                else
                {
                    arPar[1].Value = obj.TruckPlateNo;
                }

                arPar[2] = new SqlParameter("@TrailerPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(obj.TrailerPlateNo) == true)
                {
                    arPar[2].Value = DBNull.Value;
                }
                else
                {
                    arPar[2].Value = obj.TrailerPlateNo;
                }


                arPar[3] = new SqlParameter("@hasTrailer", SqlDbType.Int);
                arPar[3].Value = (int)obj.hasTrailer;

                arPar[4] = new SqlParameter("@Weight", SqlDbType.Float);
                arPar[4].Value = obj.Weight;

                arPar[5] = new SqlParameter("@DateWeighed", SqlDbType.DateTime);
                arPar[5].Value = obj.DateWeighed;

                arPar[6] = new SqlParameter("@Status", SqlDbType.Int);
                arPar[6].Value = (int)obj.Status;

                arPar[7] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[7].Value = obj.CreatedBy;

                int Affeccted = -1;

                if (dal.ExcuteProcedure(strSql, arPar))
                    Affeccted = 1;
                if (Affeccted == 1)
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

                throw new Exception("Unable to Add Truck Weight", ex);

            }

        }

        public static bool Update(TruckWeight obj, SqlTransaction tran)
        {
            return true;
        }
        public static List<TruckWeight> GetAllActiveTruckWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            List<TruckWeight> list = null;
            string strSql = "spSearchTruckWeight";
            SqlConnection conn = null;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            try
            {
                arPar[0] = new SqlParameter("@TruckPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TruckPlateNo) == true)
                {
                    arPar[0].Value = DBNull.Value;
                }
                else
                {
                    arPar[0].Value = TruckPlateNo;
                }

                arPar[1] = new SqlParameter("@TrailerPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TrailerPlateNo) == true)
                {
                    arPar[0].Value = DBNull.Value;
                }
                else
                {
                    arPar[0].Value = TrailerPlateNo;
                }

                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TruckWeight>();
                    while (reader.Read())
                    {
                        TruckWeight obj = new TruckWeight();
                        if (reader["Id"] != DBNull.Value)
                        {
                            obj.Id = new Guid(reader["Id"].ToString());
                        }
                        obj.TruckPlateNo = reader["TruckPlateNo"].ToString();
                        obj.TrailerPlateNo = reader["TrailerPlateNo"].ToString();
                        if (reader["hasTrailer"] != DBNull.Value)
                        {
                            obj.hasTrailer = (TruckHasTrailerType)int.Parse(reader["hasTrailer"].ToString());
                        }
                        if (reader["Weight"] != DBNull.Value)
                        {
                            obj.Weight = float.Parse(reader["Weight"].ToString());
                        }
                        if (reader["DateWeighed"] != DBNull.Value)
                        {
                            obj.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                        }

                        if (reader["Status"] != DBNull.Value)
                        {
                            obj.Status = (TruckWeightStatus)int.Parse(reader["Status"].ToString());
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
                            obj.CreatedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                        }
                        list.Add(obj);
                    }
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


            return list;
        }
        public static TruckWeight GetLatestActiveTruckWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            TruckWeight obj = null;
            string strSql = "spGetLatestTruckWeight";
            List<TruckWeight> list = null;
            SqlConnection conn = null;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            try
            {
                arPar[0] = new SqlParameter("@TruckPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TruckPlateNo) == true)
                {
                    arPar[0].Value = DBNull.Value;
                }
                else
                {
                    arPar[0].Value = TruckPlateNo;
                }

                arPar[1] = new SqlParameter("@TrailerPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TrailerPlateNo) == true)
                {
                    arPar[1].Value = DBNull.Value;
                }
                else
                {
                    arPar[1].Value = TrailerPlateNo;
                }

                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TruckWeight>();
                    reader.Read();
                    obj = new TruckWeight();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    obj.TruckPlateNo = reader["TruckPlateNo"].ToString();
                    obj.TrailerPlateNo = reader["TrailerPlateNo"].ToString();
                    if (reader["hasTrailer"] != DBNull.Value)
                    {
                        obj.hasTrailer = (TruckHasTrailerType)int.Parse(reader["hasTrailer"].ToString());
                    }
                    if (reader["Weight"] != DBNull.Value)
                    {
                        obj.Weight = float.Parse(reader["Weight"].ToString());
                    }
                    if (reader["DateWeighed"] != DBNull.Value)
                    {
                        obj.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                    }

                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (TruckWeightStatus)int.Parse(reader["Status"].ToString());
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
                        obj.CreatedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                    }


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
        public static TruckWeight GetLatestActiveTrailerWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            TruckWeight obj = null;
            string strSql = "spGetLatestTrailerWeight";
            List<TruckWeight> list = null;
            SqlConnection conn = null;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            try
            {
                arPar[0] = new SqlParameter("@TruckPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TruckPlateNo) == true)
                {
                    arPar[0].Value = DBNull.Value;
                }
                else
                {
                    arPar[0].Value = TruckPlateNo;
                }

                arPar[1] = new SqlParameter("@TrailerPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TrailerPlateNo) == true)
                {
                    arPar[1].Value = DBNull.Value;
                }
                else
                {
                    arPar[1].Value = TrailerPlateNo;
                }

                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TruckWeight>();
                    reader.Read();
                    obj = new TruckWeight();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    obj.TruckPlateNo = reader["TruckPlateNo"].ToString();
                    obj.TrailerPlateNo = reader["TrailerPlateNo"].ToString();
                    if (reader["hasTrailer"] != DBNull.Value)
                    {
                        obj.hasTrailer = (TruckHasTrailerType)int.Parse(reader["hasTrailer"].ToString());
                    }
                    if (reader["Weight"] != DBNull.Value)
                    {
                        obj.Weight = float.Parse(reader["Weight"].ToString());
                    }
                    if (reader["DateWeighed"] != DBNull.Value)
                    {
                        obj.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                    }

                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (TruckWeightStatus)int.Parse(reader["Status"].ToString());
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
                        obj.CreatedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                    }


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
        public static TruckWeight GetLatestActivetTruckTrailerWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            TruckWeight obj = null;
            string strSql = "spGetLatestTruckTrailerWeight";
            List<TruckWeight> list = null;
            SqlConnection conn = null;
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            try
            {
                arPar[0] = new SqlParameter("@TruckPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TruckPlateNo) == true)
                {
                    arPar[0].Value = DBNull.Value;
                }
                else
                {
                    arPar[0].Value = TruckPlateNo;
                }

                arPar[1] = new SqlParameter("@TrailerPlateNo", SqlDbType.NVarChar, 50);
                if (string.IsNullOrEmpty(TrailerPlateNo) == true)
                {
                    arPar[1].Value = DBNull.Value;
                }
                else
                {
                    arPar[1].Value = TrailerPlateNo;
                }

                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<TruckWeight>();
                    reader.Read();
                    obj = new TruckWeight();
                    if (reader["Id"] != DBNull.Value)
                    {
                        obj.Id = new Guid(reader["Id"].ToString());
                    }
                    obj.TruckPlateNo = reader["TruckPlateNo"].ToString();
                    obj.TrailerPlateNo = reader["TrailerPlateNo"].ToString();
                    if (reader["hasTrailer"] != DBNull.Value)
                    {
                        obj.hasTrailer = (TruckHasTrailerType)int.Parse(reader["hasTrailer"].ToString());
                    }
                    if (reader["Weight"] != DBNull.Value)
                    {
                        obj.Weight = float.Parse(reader["Weight"].ToString());
                    }
                    if (reader["DateWeighed"] != DBNull.Value)
                    {
                        obj.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                    }

                    if (reader["Status"] != DBNull.Value)
                    {
                        obj.Status = (TruckWeightStatus)int.Parse(reader["Status"].ToString());
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
                        obj.CreatedTimestamp = DateTime.Parse(reader["LastModifiedTimestamp"].ToString());
                    }


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
