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

namespace WarehouseApplication.BLL
{
    public class ErrorLogger : GeneralBLL
    {
        public enum ErrorSeverityLevel { Low = 1, Medium, High, critical };
        public static void Log( Exception e )
        {
           
        }
        public static void Log(Exception e, int severity, string ApplicationName,string methodName)
        {

        }
    }
}
