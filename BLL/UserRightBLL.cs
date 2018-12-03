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

namespace WarehouseApplication.BLL
{
    public class UserRightBLL
    {
        private static CacheManager<UserRightBLL> userRightCache = new CacheManager<UserRightBLL>(
            "UsersWithRight",
            (CacheManager<UserRightBLL>.ItemLocator)delegate(string id)
            {
                
                //Utility.LogException(new Exception("Current Loc" + WarehouseBLL.CurrentWarehouse.Location.ToString()));
                //string strerr = "";
                //foreach (ECXSecurity.OUser o in oUsers)
                //{
                //    strerr += "User - Id=" + o.UserGuid + " Name=" + o.UserName + Environment.NewLine;
                //}
                //Utility.LogException(new Exception("O User -- " + strerr ));
                ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
                ECXSecurity.OUser[] oUsers = access.UsersWithRight(id, WarehouseBLL.CurrentWarehouse.Location);
                return new UserRightBLL()
                {
                    Code = id,
                    Users = (from oUser in oUsers select UserBLL.GetUser(oUser.UserGuid)).ToList()
                };
            },
            delegate(UserRightBLL userRight)
            {
                return userRight.Code;
            },
            CacheManager<UserRightBLL>.CacheDurability.Short);

        public string Code { get; set; }
        public List<UserBLL> Users{get;set;}

        public static List<UserBLL> GetUsersWithRight(string code)
        {
            //return userRightCache.GetItem(code).Users.OrderBy(user => user.FullName).ToList();
            ECXSecurity.ECXSecurityAccess access = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
            ECXSecurity.OUser[] oUsers = access.UsersWithRight(code, WarehouseBLL.CurrentWarehouse.Location);
            //return new UserRightBLL()
            //{
            // Code = code,
            return (from oUser in oUsers select UserBLL.GetUser(oUser.UserGuid)).ToList();
            //};
        }

        public static string GetUserNameByUserId(Guid userId)
        {
            UserBLL o = null;
            o = UserBLL.GetUser(userId);
            if (o != null)
            {
                return o.FullName;
            }
            else
            {
                return "";
            }

        }
    }
}
