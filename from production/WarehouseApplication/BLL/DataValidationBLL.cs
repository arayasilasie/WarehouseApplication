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
    /// <summary>
    /// Utility class that is used to check data Validation
    /// </summary>
    public class DataValidationBLL
    {
        public static bool isDataValidForDataType(string input, string DataType)
        {
            switch (DataType)
            {
                case "int":
                    int xint;
                    try
                    {
                        xint = Convert.ToInt32(input);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case "bit":
                    if (input.ToUpper() == "YES")
                    {
                        input = "true";
                    }
                    else if (input.ToUpper() == "NO")
                    {
                        input = "false";
                    }

                    bool xbool;
                    try
                    {
                        xbool = Convert.ToBoolean(input);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                case "Float":
                    float xFloat;
                    try
                    {
                        xFloat = float.Parse(input);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                default:
                    return true;


            }
        }
        public static bool isExists(string input, string checklist)
        {
            if ((checklist == "N/A") || (string.IsNullOrEmpty(checklist) == true))
            {
                return true;
            }
            else
            {
                string[] strArr = checklist.Split(';');
                
                foreach (string str in strArr)
                {
                    string xstr = "";
                    if (str.Length - 2 > 0)
                    {
                        xstr = str.Substring(1, str.Length - 2);
                    }
                                      
                    if (input.ToUpper() == xstr.ToUpper())
                    {
                        return true;
                    }
                }
                
                return false;
            }

        }
        public static bool isGUID(string input, out Nullable<Guid> Id)
        {

            try
            {
                Id = new Guid(input);
                return true;

            }
            catch (ArgumentNullException)
            {
                Id = null;

                return false;
            }
            catch (FormatException)
            {
                Id = null;

                return false;
            }
            catch (OverflowException)
            {
                Id = null;

                return false;
            }
        }
        public static bool isInteger(string input, out Nullable<int> myInt)
        {
            try
            {
                myInt = Convert.ToInt32(input);
                return true;
            }
            catch (FormatException)
            {
                myInt = null;

                return false;
            }
            catch (OverflowException)
            {

                myInt = null;
                return false;
            }
        }
        public static bool isDate(string input, out Nullable<DateTime> myDate)
        {
            try
            {
                myDate = Convert.ToDateTime(input);
                return true;
            }
            catch (FormatException)
            {

                myDate = null;
                return false;
            }
        }

    }
}
