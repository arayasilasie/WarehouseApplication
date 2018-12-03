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
using WarehouseApplication.DALManager;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    [Serializable]
    public class PUNFilterFormatter : SQLConditionFormatter
    {
        protected override SQLDataFilterParameter[] GetParameters(DataFilterCondition condition)
        {
            if (condition.Parameter.Name == "ClientId")
            {
                if (condition.LeftOperand == string.Empty)
                {
                    return new SQLDataFilterParameter[] { };
                }
                ClientBLL requestedClient = ClientBLL.GetClinet(condition.LeftOperand);
                DataFilterParameter dfp = new DataFilterParameter(
                    "ClientId", string.Empty, typeof(Guid), string.Empty, FilterConditionType.Comparison);
                DataFilterCondition clientCondition = new DataFilterCondition(
                    dfp, FilterConditionType.Comparison, "=", ((requestedClient == null)? Guid.Empty : requestedClient.ClientUniqueIdentifier).ToString());
                return base.GetParameters(clientCondition);
            }
            else
            {
                return base.GetParameters(condition);
            }
        }
    }
}
