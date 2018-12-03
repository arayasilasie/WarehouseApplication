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
    public class SLADAL
    {
        public static List<SLABLL> GetSLAByDateDeposit(Guid WarehouseId, DateTime DateDeposit)
        {
            List<SLABLL> list = null;
            string strSql = "spSLARPT";
            SqlDataReader reader;
            SqlParameter[] arPar = new SqlParameter[2];
            arPar[0] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
            arPar[0].Value = WarehouseId;

            arPar[1] = new SqlParameter("@DepositDate", SqlDbType.DateTime);
            arPar[1].Value = DateDeposit;

            SqlConnection conn = null;
            try
            {
                conn = Connection.getConnection();
                reader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strSql, arPar);
                if (reader.HasRows)
                {
                    list = new List<SLABLL>();
                    while (reader.Read())
                    {
                        SLABLL obj = new SLABLL();
                        if (reader["VoucherNo"] != DBNull.Value)
                        {
                            obj.objVoucher.VoucherNo = reader["VoucherNo"].ToString();
                        }
                        if (reader["ClientId"] != DBNull.Value)
                        {
                            obj.objGRN.ClientId = new Guid(reader["ClientId"].ToString());
                        }
                        if (reader["PlateNumber"] != DBNull.Value)
                        {
                            obj.objDriver.PlateNumber = reader["PlateNumber"].ToString();
                        }
                        if (reader["TrailerPlateNumber"] != DBNull.Value)
                        {
                            obj.objDriver.TrailerPlateNumber = reader["TrailerPlateNumber"].ToString();
                        }
                        if (reader["TotalNumberOfBags"] != DBNull.Value)
                        {
                            obj.objUnloading.TotalNumberOfBags = int.Parse(reader["TotalNumberOfBags"].ToString());
                        }
                        if (reader["ArrivalDate"] != DBNull.Value)
                        {
                            obj.objArrival.DateTimeRecived = DateTime.Parse(reader["ArrivalDate"].ToString());
                        }
                        if (reader["ArrivalDateSystem"] != DBNull.Value)
                        {
                            obj.objArrival.CreatedTimestamp = DateTime.Parse(reader["ArrivalDateSystem"].ToString());
                        }
                        if (reader["SampledDate"] != DBNull.Value)
                        {
                            obj.objSampling.GeneratedTimeStamp = DateTime.Parse(reader["SampledDate"].ToString());
                        }
                        if (reader["SampledDateSystem"] != DBNull.Value)
                        {
                            obj.objSampling.CreatedTimestamp = DateTime.Parse(reader["SampledDateSystem"].ToString());
                        }
                        if (reader["SamplingResultRecivedDate"] != DBNull.Value)
                        {
                            obj.objSamplingResult.ResultReceivedDateTime = DateTime.Parse(reader["SamplingResultRecivedDate"].ToString());
                        }
                        if (reader["SamplingResultRecivedDate"] != DBNull.Value)
                        {
                            obj.objSamplingResult.CreatedTimeStamp = DateTime.Parse(reader["SamplingResultRecivedDate"].ToString());
                        }
                        if (reader["SamplingResultStatus"] != DBNull.Value)
                        {
                            obj.objSamplingResult.Status = (SamplingResultStatus)int.Parse(reader["TotalNumberOfBags"].ToString());
                        }
                        if (reader["CodingDate"] != DBNull.Value)
                        {
                            obj.objGrading.DateCoded = DateTime.Parse(reader["CodingDate"].ToString());
                        }
                        if (reader["CodingDateSystem"] != DBNull.Value)
                        {
                            obj.objGrading.CreatedTimestamp = DateTime.Parse(reader["CodingDateSystem"].ToString());
                        }
                        if (reader["GradeRecivedTimestamp"] != DBNull.Value)
                        {
                            obj.objGradingResult.GradeRecivedTimeStamp = DateTime.Parse(reader["GradeRecivedTimestamp"].ToString());
                        }
                        if (reader["GradeRecivedTimestampSystem"] != DBNull.Value)
                        {
                            obj.objGradingResult.CreatedTimestamp = DateTime.Parse(reader["GradeRecivedTimestampSystem"].ToString());
                        }
                        if (reader["ClientAcceptanceTimeStamp"] != DBNull.Value)
                        {
                            obj.objGradingResult.ClientAcceptanceTimeStamp = DateTime.Parse(reader["ClientAcceptanceTimeStamp"].ToString());
                        }
                        else
                        {
                            obj.objGradingResult.ClientAcceptanceTimeStamp = DateTime.Parse("1/1/0001");
                        }
                        if (reader["GradingResultStatus"] != DBNull.Value)
                        {
                            obj.objGradingResult.Status = (GradingResultStatus)int.Parse(reader["GradingResultStatus"].ToString());
                        }
                        if (reader["DateDeposited"] != DBNull.Value)
                        {
                            obj.objUnloading.DateDeposited = DateTime.Parse(reader["DateDeposited"].ToString());
                        }
                        if (reader["DateDepositedSystem"] != DBNull.Value)
                        {
                            obj.objUnloading.CreatedTimestamp = DateTime.Parse(reader["DateDepositedSystem"].ToString());
                        }
                        if (reader["DateWeighed"] != DBNull.Value)
                        {
                            obj.objScaling.DateWeighed = DateTime.Parse(reader["DateWeighed"].ToString());
                        }
                        if (reader["DateWeighedSystem"] != DBNull.Value)
                        {
                            obj.objScaling.CreatedTimestamp = DateTime.Parse(reader["DateWeighedSystem"].ToString());
                        }

                        if (reader["GRN_Number"] != DBNull.Value)
                        {

                            if (reader["GRNCreatedDate"] != DBNull.Value)
                            {
                                obj.objGRN.GRNCreatedDate = DateTime.Parse(reader["GRNCreatedDate"].ToString());
                            }
                            if (reader["GRNCreatedDateSystem"] != DBNull.Value)
                            {
                                obj.objGRN.CreatedTimestamp = DateTime.Parse(reader["GRNCreatedDateSystem"].ToString());
                            }
                            if (reader["ClientAccepted"] != DBNull.Value)
                            {
                                obj.objGRN.ClientAccepted = Boolean.Parse(reader["ClientAccepted"].ToString());
                            }
                            if (reader["ClientAcceptedTimeStamp"] != DBNull.Value)
                            {
                                obj.objGRN.ClientAcceptedTimeStamp = DateTime.Parse(reader["ClientAcceptedTimeStamp"].ToString());
                            }
                            if (reader["ManagerApprovedDateTime"] != DBNull.Value)
                            {
                                obj.objGRN.ManagerApprovedDateTime = DateTime.Parse(reader["ManagerApprovedDateTime"].ToString());
                            }
                            if (reader["ManagerApprovedDateTimeSystem"] != DBNull.Value)
                            {
                                obj.objGRN.ApprovedTimeStamp = DateTime.Parse(reader["ManagerApprovedDateTimeSystem"].ToString());
                            }
                            if (reader["GRNStatus"] != DBNull.Value)
                            {
                                obj.objGRN.Status = int.Parse(reader["GRNStatus"].ToString());
                            }
                            if (reader["OriginalQuantity"] != DBNull.Value)
                            {
                                obj.objGRN.OriginalQuantity = float.Parse(reader["OriginalQuantity"].ToString());
                            }
                            if (reader["GRN_Number"] != DBNull.Value)
                            {
                                obj.objGRN.GRN_Number = reader["GRN_Number"].ToString();
                            }
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

    }
}

