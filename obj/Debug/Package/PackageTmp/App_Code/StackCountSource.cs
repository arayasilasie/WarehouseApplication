using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public class StackCountSource
    {
        //private static List<StackPhysicalCountInfo> stackCounts = new List<StackPhysicalCountInfo>(new StackPhysicalCountInfo[]{
        //            new StackPhysicalCountInfo(){Balance=0,CummulatedShortage=0,ExpectedBalance=0,Id=Guid.NewGuid(),ShedId=Guid.Empty,StackId=Guid.Empty}});
        public static List<StackPhysicalCountInfo> StackCounts 
        { 
            get{
                //return stackCounts;
                return InventoryServiceWrapper.GetPhysicalCountInformation(true).Stacks;
            }
        }

        public static List<StackPhysicalCountInfo> GetStackCounts()
        {
            return GetStackCounts(int.MaxValue, 0, string.Empty);
        }
        public static List<StackPhysicalCountInfo> GetStackCounts(int maximumRows, int startRowIndex)
        {
            return GetStackCounts(maximumRows, startRowIndex, string.Empty);
        }
        public static List<StackPhysicalCountInfo> GetStackCounts(string SortExpression)
        {
            return GetStackCounts(int.MaxValue, 0, SortExpression);
        }

        public static List<StackPhysicalCountInfo> GetStackCounts(int maximumRows, int startRowIndex, string sortExpression)
        {
            int returnedRows = (startRowIndex + maximumRows <= TotalNumberOfStacks()) ? maximumRows : TotalNumberOfStacks() - startRowIndex;
            return StackCounts.GetRange(startRowIndex, returnedRows);
        }

        public static int TotalNumberOfStacks()
        {
            return StackCounts.Count;
        }
    }
}
