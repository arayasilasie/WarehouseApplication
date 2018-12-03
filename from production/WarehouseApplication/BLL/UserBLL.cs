using System;
using System.Collections.Generic;
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
using System.Xml.Serialization;
using System.DirectoryServices;
using System.IO;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.BLL
{
    public class UserBLL
    {

        private Guid _CurrentWarehouse = new Guid("fa0a52e8-9308-4d5e-b323-88ca5ba232ed");
        private Guid _createdBy = new Guid("c1235d80-5d4a-44b0-8ec3-39da36a2c7ce");
        private Guid _lastModifiedBy = new Guid("c1235d80-5d4a-44b0-8ec3-39da36a2c7ce");
        private string _fullName;
        private Guid _userId;
        private string _userName;

        #region Cache
        private static CacheManager<UserBLL> userCache = new CacheManager<UserBLL>(
            "User",
            (CacheManager<UserBLL>.ItemLocator)delegate(string id)
            {
                //To do - Remove this
                //ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
                //Guid userId = Guid.Empty;
                //try
                //{
                //    userId = new Guid(id);
                //}
                //catch
                //{
                //    throw new Exception("User not found");
                //}
                //string fullName = access.GetUserName(userId);
                //string userName = fullName.Replace(' ', '.');
                //return new UserBLL()
                //{
                //    UserId = userId,
                //    FullName = fullName,
                //    UserName = userName
                //};
                //END of Remove

                string query = string.Empty;
                try
                {
                    string guidQuery = string.Empty;
                    try
                    {
                        byte[] byteArrayId = new Guid(id).ToByteArray();
                        for (int i = 0; i < byteArrayId.Length; i++)
                        {
                            guidQuery += string.Format("\\{0:X2}", byteArrayId[i]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Exception e = new Exception("Id Provided" + id.ToString(), ex);
                        Utility.LogException(e);
                    }
                    query = string.Format("(objectGUID={0})", guidQuery);
                }
                catch (Exception ex)
                {
                    throw new Exception("Not valid UserID", ex);
                    //query = string.Format("(sAMAccountName={0})",id);
                }
                DirectoryEntry de = FindDEAccount(query);
                if (de == null)
                {
                    throw new Exception(string.Format("User not found GUID:{0}", id));
                }
                return new UserBLL()
                {
                    FullName = (string)de.Properties["CN"][0],
                    UserName = (string)de.Properties["sAMAccountName"][0],
                    UserId = new Guid(id)
                };
            },
            delegate(UserBLL user)
            {
                return user.UserId.ToString();
            },
            CacheManager<UserBLL>.CacheDurability.Indefinite);

        private static CacheManager<UserRoles> userRolesCache = new CacheManager<UserRoles>(
            "UserRoles",
            (CacheManager<UserRoles>.ItemLocator)delegate(string id)
            {
                XmlSerializer s = new XmlSerializer(typeof(SecurityResourceConfigurationInfo));
                SecurityResourceConfigurationInfo src = null;
                using (Stream stream = File.OpenRead(HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["SecurityConfigurationFile"]))
                {
                    try
                    {
                        src = (SecurityResourceConfigurationInfo)s.Deserialize(stream);
                    }
                    catch
                    {
                        throw new Exception("Unable to read the security configuration file");
                    }
                }
                string[] allRoleNames = (from role in src.SecurityRoles select role.Name).ToArray();
                ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
                List<string> userRoles = new List<string>();
                float[] rights = access.HasRights(new Guid(id), allRoleNames, WarehouseBLL.CurrentWarehouse.Location);
                for (int i = 0; i < allRoleNames.Length; i++)
                {
                    if ((rights[i] == 1) || (rights[i] == 3))
                    {
                        userRoles.Add(allRoleNames[i]);
                    }
                }
                return new UserRoles()
                {
                    User = userCache.GetItem(id),
                    Roles = userRoles
                };
            },
            delegate(UserRoles userRoles)
            {
                return userRoles.User.UserId.ToString();
            },
            CacheManager<UserRoles>.CacheDurability.Long);


        #endregion

        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public Guid CreatedBy
        {
            get
            {
                return this._createdBy;
            }
        }
        public Guid LastModifiedBy
        {
            get
            {
                return this._lastModifiedBy;
            }
        }

        public static void UserSessionStarted(string userName)
        {
            //To do Remove
            //userCache.GetItem("1fb16b56-9d80-4d61-8956-b2fbe3a7b09e");
            //End Remove
            //StreamWriter w = File.AppendText(HttpContext.Current.Request.PhysicalApplicationPath + @"\Test.Log");
            //w.WriteLine(userName + " Session Started");
            //w.Flush();
            //w.Close();

            userCache.GetItem(GetUser(userName).ToString());
        }

        public static void UserSessionEnd(string userName)
        {
            //To do Remove
            //userCache.Remove("1fb16b56-9d80-4d61-8956-b2fbe3a7b09e");
            //End Remove
            //StreamWriter w = File.AppendText(HttpContext.Current.Request.PhysicalApplicationPath + @"\Test.Log");
            //w.WriteLine(userName + " Session Ended");
            //w.Flush();
            //w.Close();
            //userCache.Remove(GetUser(userName).ToString());
        }

        public static Guid GetCurrentUser()
        {
            return CurrentUser.UserId;
            ////TODO Remove 
            //return new Guid("1fb16b56-9d80-4d61-8956-b2fbe3a7b09e");
            //if (HttpContext.Current.Session["LoggedUser"] == null)
            //{
            //    Guid? UserGuid = UserBLL.GetUser(HttpContext.Current.User.Identity.Name.Remove(0, HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1));
            //    if (UserGuid != null)
            //    {
            //        HttpContext.Current.Session["LoggedUser"] = UserGuid;
            //        return new Guid(HttpContext.Current.Session["LoggedUser"].ToString());
            //    }
            //    else
            //    {
            //        FormsAuthentication.SignOut();
            //        HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["lbLoggoffISAURL"]);
            //        throw (new Exception("Unable to find user."));
            //    }
            //}
            //else
            //{
            //    return new Guid(HttpContext.Current.Session["LoggedUser"].ToString());
            //}
        }

        public static UserBLL CurrentUser
        {
            get
            {
                UserBLL currentUser =
                    (from user in userCache.GetAllItems()
                     where user.UserName.ToUpper() == CurrentUserName.ToUpper()
                     select user).SingleOrDefault();
                if (currentUser == null)
                {

                    //StreamWriter w = File.AppendText(HttpContext.Current.Request.PhysicalApplicationPath + @"\Test.Log");
                    //w.WriteLine(userCache.GetAllItems().Count.ToString() + " users in cache");
                    //userCache.GetAllItems().ForEach(u => w.WriteLine(u.UserName + " In cache"));
                    //w.WriteLine(CurrentUserName + " 11 Not found");
                    //w.Flush();
                    //w.Close();

                    FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["lbLoggoffISAURL"]);
                    throw (new Exception("Unable to find user."));
                }
                return currentUser;

                //Guid loggedUser = (Guid)HttpContext.Current.Session["LoggedUser"];
                //string loggedUserName = (String)HttpContext.Current.Session["LoggedUserName"];
                //return new UserBLL() { FullName = loggedUserName, UserId = loggedUser };
            }
        }

        private static DirectoryEntry FindDEAccount(string query)
        {
            try
            {
                DirectoryEntry de = new DirectoryEntry(ConfigurationManager.AppSettings["DirPath"]);
                de.Username = ConfigurationManager.AppSettings["ACDUser"];
                de.Password = ConfigurationManager.AppSettings["ACDPass"];

                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.SearchRoot = de;

                deSearch.Filter = string.Format("(&(objectClass=user)({0}))", query);
                deSearch.PropertiesToLoad.Add("CN");
                return deSearch.FindOne().GetDirectoryEntry();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static UserBLL GetUser(Guid userId)
        {
            return userCache.GetItem(userId.ToString());
        }

        public static Guid? GetUser(string userName)
        {
            try
            {
                DirectoryEntry de = FindDEAccount(string.Format("(sAMAccountName={0})", userName));
                if (de != null)
                {
                    return de.Guid;
                }
                else
                {
                    return null;
                }
                //DirectoryEntry de = new DirectoryEntry(ConfigurationManager.AppSettings["DirPath"]);
                //de.Username = ConfigurationManager.AppSettings["ACDUser"];
                //de.Password = ConfigurationManager.AppSettings["ACDPass"];

                //DirectorySearcher deSearch = new DirectorySearcher();
                //deSearch.SearchRoot = de;

                //deSearch.Filter = "(&(objectClass=user))";
                //deSearch.SearchScope = SearchScope.Subtree;
                ////deSearch.Sort = new SortOption();
                ////deSearch.Sort.Direction = System.DirectoryServices.SortDirection.Ascending;

                //SearchResultCollection results = deSearch.FindAll();

                //for (int i = 0; i < results.Count; i++)
                //{
                //    if (results[i].GetDirectoryEntry().Name.Remove(0, 3).Replace(" ", ".").ToUpper() == UserName.ToUpper())
                //    {
                //        return results[i].GetDirectoryEntry().Guid;
                //    }
                //}
                //return null;
            }
            catch
            {
                return null;
            }
        }
        public static Guid GetCurrentWarehouse()
        {
            //return new Guid("fa0a52e8-9308-4d5e-b323-88ca5ba232ed");
            return WarehouseBLL.CurrentWarehouse.WarehouseId;
        }
        public static string GetCurrentWarehouseCode()
        {
            //return "101";
            return WarehouseBLL.CurrentWarehouse.Code;
        }
        public static string GetName(Guid Id)
        {
            return userCache.GetItem(Id.ToString()).UserName;
            //ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
            //return access.GetUserName(Id);
        }

        public static List<string> HasRoles(Guid userId, string[] roles)
        {
            return userRolesCache.GetItem(userId.ToString()).Roles;
            //List<string> userRoles  = new List<string>();
            //ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
            //float[] rights = access.HasRights(userId, roles, WarehouseBLL.CurrentWarehouse.Location);
            //for (int i = 0; i < roles.Length; i++)
            //{
            //    if ((rights[i] == 1) || (rights[i] == 3))
            //    {
            //        userRoles.Add(roles[i]);
            //    }
            //}
            //return userRoles;
        }

        public static void Expire(Guid userId)
        {
            //return userRolesCache.GetItem(userId.ToString()).Roles;

            //userRolesCache.Remove(userId.ToString());
            //List<string> userRoles = new List<string>();
            //ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
            //float[] rights = access.HasRights(userId, roles, WarehouseBLL.CurrentWarehouse.Location);
            //for (int i = 0; i < roles.Length; i++)
            //{
            //    if ((rights[i] == 1) || (rights[i] == 3))
            //    {
            //        userRoles.Add(roles[i]);
            //    }
            //}

            //return userRoles;
        }


        public static string CurrentUserName
        {
            get
            {
                //To Do Remove
                //return userCache.GetItem("1fb16b56-9d80-4d61-8956-b2fbe3a7b09e").UserName;
                //End Remove
                return HttpContext.Current.User.Identity.Name.Remove(0, HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1);
                
                    
            }
        }

        public class UserRoles
        {
            public UserRoles() { }
            public UserBLL User { get; set; }
            public List<string> Roles { get; set; }
        }

    }
}
