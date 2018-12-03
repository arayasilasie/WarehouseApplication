using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using WarehouseApplication.SECManager;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Utility.LogException(new Exception(string.Format("Session_Start at {0} for {1}", DateTime.Now,
            //    User.Identity.Name.Remove(0, HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1))));
            UserBLL.UserSessionStarted(User.Identity.Name.Remove(0, HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1));
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //Utility.LogException(new Exception(string.Format("Application_AuthenticateRequest at {0}", DateTime.Now)));
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //Utility.LogException(new Exception(string.Format("Application_PostAuthenticateRequest at {0}", DateTime.Now)));
            //UserBLL.UserSessionStarted(User.Identity.Name.Remove(0, HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1));
        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            string formName = Request.AppRelativeCurrentExecutionFilePath.Substring(1);
            if ((formName.ToUpper() == "/SelectWarehouse.aspx".ToUpper()) ||
                (formName.ToUpper() == "/AccessDenied.aspx".ToUpper()) ||
                (formName.ToUpper() == "/ErrorPage.aspx".ToUpper()) ||
                new FileInfo(formName.Substring(1)).Extension.ToUpper() != ".aspx".ToUpper())
            {
                return;
            }

            if (Session["CurrentWarehouse"] == null)
            {
                Response.Redirect("SelectWarehouse.aspx", true);
            }

            XmlSerializer s = new XmlSerializer(typeof(SecurityResourceConfigurationInfo));
            Stream stream = null;
            SecurityResourceConfigurationInfo src = null;
            try
            {
                stream = File.OpenRead(HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["SecurityConfigurationFile"]);
                src = (SecurityResourceConfigurationInfo)s.Deserialize(stream);
            }
            catch (Exception)
            {
            }
            finally
            {
                stream.Close();
            }
            if (src == null) return;
            string[] allRoleNames = new string[src.SecurityRoles.Count];
            int i = 0;
            foreach (SecurityRoleInfo role in src.SecurityRoles)
            {
                allRoleNames[i++] = role.Name;
            }
            List<string> userRoleNames = null;
            if (formName == "/SelectWarehouse.aspx")
            {
                userRoleNames = UserBLL.HasRoles(
                   UserBLL.GetCurrentUser(),
                   allRoleNames);
            }
            else
            {
                userRoleNames = UserBLL.HasRoles(
                   UserBLL.GetCurrentUser(),
                   allRoleNames);
            }


            var forms = from role in src.SecurityRoles
                        where userRoleNames.Any(urn => urn == role.Name) && role.GrantedResourceContainers.Any(gr => gr.Name == formName)
                        select role;
            if (forms.Count() == 0)
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();

            if (lastError != null)
            {
                if (HttpContext.Current.Session == null)
                {
                    Utility.LogException(lastError);
                    Response.Redirect("portal.ecx.com.et?CMD=logoff", true);
                }
                else
                {
                    Session["ErrorId"] = Utility.LogException(lastError);
                }
            }
        }




    }
}