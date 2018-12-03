using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace WarehouseApplication.DAL
{
    public class DataAccessPoint
    {
        string connectionString = "";
        SqlConnection connection = null;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DataAccessPoint()
        {
            connectionString = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ToString();
        }
        /// <summary>
        /// Create a connection
        /// </summary>
        /// <returns>Returns a open connection to caller method</returns>
        public SqlConnection CreateConnection()
        {
            try
            {
                connection = new SqlConnection(this.connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        /// <summary>
        /// Close an opened connection
        /// The connection is declared at this page globally
        /// </summary>
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            else
                return;
        }
        /// <summary>
        /// Save any record to any table by accepting 2 parameters
        /// sqlCommandText (string) -> Stored Procedure Name
        /// ParamList (SqlParameter) -> Parameter list which a stored procedure accept
        /// </summary>
        /// <param name="sqlCommandText"></param>
        /// <param name="ParamList"></param>
        /// <returns>
        /// True if the record saved successflly or false if not
        /// </returns>
        public bool ExcuteProcedure(string sqlStoredProcedure, SqlParameter [] ParamList)
        {
            try
            {
                SqlConnection conn = CreateConnection();
                SqlCommand cmd = new SqlCommand(sqlStoredProcedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();//Clear any existing parameters from cmd instance
                cmd.Parameters.AddRange(ParamList);
                cmd.ExecuteNonQuery();
                return true;//Saved
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save record! " +ex.Message.ToString());
            }
            finally
            {
                CloseConnection();//Close Opened connection
            }
        }
        /// <summary>
        /// get filtered data based on sql command text and parameters passed to it and return
        /// the first row of the result (datareader)
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public SqlDataReader LoadByText(string cmdText, SqlParameter param)
        {
            try
            {
                SqlConnection conn = CreateConnection();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();//Clear any existing parameters from cmd instance
                cmd.Parameters.Add(param);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load data! " + ex.Message.ToString());
            }
            finally
            {
                CloseConnection();//Close Opened connection
            }
        }
        /// <summary>
        /// Create new parameter with supplied Parameter-Name, SqlDbType and Value intended with
        /// the parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns>
        /// Returns New parameter with name and value
        /// </returns>
        public static SqlParameter param(string paramName, SqlDbType dbType, object paramValue)
        {
            SqlParameter param = new SqlParameter(paramName, dbType);
            param.Value = paramValue;
            return param;
        }
        /// <summary>
        /// Create new parameter with supplied Parameter-Name, SqlDbType, Value intended with
        /// the parameter and the size if the parameter has string data type
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns>
        /// Returns New parameter with name, size, value
        /// </returns>
        public static SqlParameter param(string paramName, SqlDbType dbType, object paramValue, int size)
        {
            SqlParameter param = new SqlParameter(paramName, dbType, size);
            param.Value = paramValue;
            return param;
        }
    }
}
