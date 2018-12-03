using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using WarehouseApplication.DAL;

namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for ClientTest
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class ClientTest : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetClientName(int Id)
        {
            Client objClient = new Client();
            return objClient.getClientName(Id);
        }
    }
}
