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
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Collections.Generic;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication
{
    public class ECXWarehousePage : Page
    {
        protected override void OnPreRenderComplete(EventArgs e)
        {
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
            if (Page is ISecurityConfiguration)
            {
                var pageFileQuery = from container in src.SecuredResourceContainers
                                    where (container.Name.ToUpper() == Request.Path.ToUpper())
                                    select container;
                if (pageFileQuery.Count() > 0)
                {
                    ApplySecurityRules(src, pageFileQuery.ElementAt(0), (ISecurityConfiguration)Page);
                }
            }
            List<UserControl> userControls = new List<UserControl>();
            AccumulateUserControls(this, userControls);
            foreach (UserControl userControl in userControls)
            {
                if (userControl is ISecurityConfiguration)
                {
                    var userControlFileQuery = from container in src.SecuredResourceContainers
                                               where (container.Name.ToUpper() == userControl.AppRelativeVirtualPath.Substring(1).ToUpper())
                                               select container;

                    if (userControlFileQuery.Count() > 0)
                    {
                        ApplySecurityRules(src, userControlFileQuery.ElementAt(0), (ISecurityConfiguration)userControl);
                    }
                }
            }
            base.OnPreRenderComplete(e);
        }

        protected virtual void ApplySecurityRules(SecurityResourceConfigurationInfo src, SecuredResourceContainerInfo resourceContainer, ISecurityConfiguration securityConfiguration)
        {
            foreach (SecuredResourceInfo resource in resourceContainer.SecuredResources)
            {
                List<Object> securedResources = securityConfiguration.GetSecuredResource(resource.Scope, resource.Name);
                if (securedResources == null)
                {
                    return;
                }
                if (securedResources.Count > 0)
                {
                    var minLevel = resource.ConfigurationOptions.Select(option => option.Level).Min();
                    var restrictiveOptions = from option in resource.ConfigurationOptions
                                             where option.Level == minLevel
                                             select option;

                    List<ConfigurationOptionInfo> applicableOptions = new List<ConfigurationOptionInfo>(restrictiveOptions);

                    string[] allRoleNames = (from roles in src.SecurityRoles select roles.Name).ToArray();
                    //    new string[src.SecurityRoles.Count];
                    //foreach (SecurityRoleInfo role in src.SecurityRoles)
                    //{
                    //    allRoleNames[allRoleNames.Length-1] = role.Name;
                    //}
                    List<string> userRoleNames = UserBLL.HasRoles(
                        UserBLL.GetCurrentUser(),
                        allRoleNames);
                    var anyRoles = from role in src.SecurityRoles where (from userRole in userRoleNames where role.Name == userRole select userRole).Any() select role;
                    foreach (SecurityRoleInfo role in
                        (from role in src.SecurityRoles where (from userRole in userRoleNames where role.Name == userRole select userRole).Any() select role))
                    {
                        var grantedOptions = role.GrantedResourceContainers.Where(grc => grc.Name == resourceContainer.Name)
                            .SelectMany(grc => grc.GrantedResources.Where(gr => (gr.Scope == resource.Scope) && (gr.Name == resource.Name)))
                                .Select(gr => gr.Option)
                                    .SelectMany(grantedOption => resource.ConfigurationOptions.Where(option => option.OptionId == grantedOption)
                                         .Select(option => option));
                        foreach (ConfigurationOptionInfo option in grantedOptions)
                        {
                            if (applicableOptions.RemoveAll(ao => (ao.Property == option.Property) && (ao.Level < option.Level)) > 0)
                            {
                                applicableOptions.Add(option);
                            }
                        }
                    }
                    foreach (ConfigurationOptionInfo applicableOption in applicableOptions)
                    {
                        foreach (object securedResource in securedResources)
                        {
                            PropertyInfo optionProperty = securedResource.GetType().GetProperty(applicableOption.Property);
                            optionProperty.SetValue(securedResource, Convert.ChangeType(applicableOption.Value, optionProperty.PropertyType), null);
                        }
                    }
                }
            }
        }

        private void AccumulateUserControls(Control control, List<UserControl> userControls)
        {
            if(control is UserControl)
            {
                userControls.Add((UserControl)control);
            }
            foreach (Control childControl in control.Controls)
            {
                AccumulateUserControls(childControl, userControls);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            Form.SubmitDisabledControls = false;

            //Response.CacheControl = "no-cache";
            //Response.AddHeader("Pragma", "no-cache");
            //Response.Expires = -1;
        }
    }
}
