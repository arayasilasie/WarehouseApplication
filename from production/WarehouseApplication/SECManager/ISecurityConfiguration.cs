using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace WarehouseApplication.SECManager
{
    public interface ISecurityConfiguration
    {
        List<Object> GetSecuredResource(string scope, string name);
    }
}
