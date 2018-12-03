using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using AjaxControlToolkit;

namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for StackCDDService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class StackCDDService : System.Web.Services.WebService
    {
        [WebMethod]
        public CascadingDropDownNameValue[] GetShedStacks(string knownCategoryValues, string category)
        {
            string[] categoryValues = knownCategoryValues.Split(':', ';');
            string[] idPair = categoryValues[1].Split('_');
            Guid shedID = new Guid(idPair[0]);
            Guid commodityGradeId = new Guid(idPair[1]);
            int productionYear = int.Parse(idPair[2]);
            List<CascadingDropDownNameValue> stacks = new List<CascadingDropDownNameValue>();
            foreach (BLL.StackBLL stack in new BLL.StackBLL().GetActiveStackbyShedId(shedID))
            {
                if ((stack.CommodityGradeid != commodityGradeId) || (stack.ProductionYear != productionYear)) continue;
                //if (stack.CommodityGradeid != commodityGradeId) continue;
                stacks.Add(new CascadingDropDownNameValue(stack.StackNumber, stack.Id.ToString()));
            }
            return stacks.ToArray();
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetSheds(string knownCategoryValues, string category, string contextKey)
        {
            return (from shed in new BLL.ShedBLL().GetActiveShedByWarehouseId(new Guid(contextKey))
             select new CascadingDropDownNameValue(shed.ShedNumber, shed.Id.ToString())).ToArray();
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetAllShedStacks(string knownCategoryValues, string category)
        {
            string[] categoryValues = knownCategoryValues.Split(':', ';');
            Guid shedId = new Guid(categoryValues[1]);
            return (from stack in new BLL.StackBLL().GetActiveStackbyShedId(shedId)
                    select new CascadingDropDownNameValue(stack.StackNumber, stack.Id.ToString())).ToArray();
        }
    }
}
