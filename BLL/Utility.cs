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
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace WarehouseApplication.BLL
{
    public class Utility
    {
        //TODO : read from Config File.
        public static Guid GetWorkinglanguage()
        {
                return new Guid(ConfigurationSettings.AppSettings["WorkingLanguage"].ToString());
            
           
        }
        public static float WeightTolerance()
        {
            return float.Parse(ConfigurationSettings.AppSettings["WeightTolerance"].ToString());
           
        }
        
        public static Guid GetGraderTypeId()
        {
            Guid GraderRoleid= Guid.Empty;
            try
            {
                GraderRoleid = new Guid(ConfigurationSettings.AppSettings["Cupper"].ToString());
            }
            catch
            {
                throw new Exception("Missing Configuration setting.");
            }
            return GraderRoleid;
        }
        public static string GetApplicationName()
        {
            try
            {
                return ConfigurationSettings.AppSettings["WarehouseApplication"].ToString();
            }
            catch
            {
                throw new Exception("Unable to Get Configuration Value");
            }

        }

        public static long LogException(Exception error)
        {
            if (error == null)
            {
                throw new Exception();
            }
            string connectionString = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand("spLogError", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(new SqlParameter[]{
                    new SqlParameter("@DateTimeStamp", DateTime.Now),
                    new SqlParameter("@SourcePage", HttpContext.Current.Request.Path),
                    new SqlParameter("@ExceptionData",error.ToString()),
                    new SqlParameter("@ReturnedResult", SqlDbType.BigInt)});
                command.Parameters["@ReturnedResult"].Direction = ParameterDirection.ReturnValue;
                conn.Open();
                command.ExecuteNonQuery();
                return Convert.ToInt64(command.Parameters["@ReturnedResult"].Value);
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
        public static int GetNoGradersByCommodity(Guid CommodityId )
        {
            string str = "MinGraders-";
            if (CommodityId == Guid.Empty)
            {
                return -1;
            }
            else
            {
                str = str + CommodityId.ToString();
            }

            try
            {
                return int.Parse(ConfigurationSettings.AppSettings[str].ToString());
            }
            catch
            {
                return -1;
            }

        }
        public static Nullable<Guid> GetCommodityId(string strCommodityName)
        {
            strCommodityName +=  "Id";
            Nullable<Guid> Id =  null;
            Id=  new Guid (ConfigurationSettings.AppSettings[strCommodityName].ToString());
            return Id;
        }
    }
}
