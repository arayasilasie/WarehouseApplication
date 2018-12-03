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

namespace WarehouseApplication.DALManager
{

    public class NullFinder
    {
        public static object GetNullValue(string dataType)
        {
            switch (dataType)
            {
                case "System.Int32":
                    return Int32.MinValue;
                case "System.Int16":
                    return Int16.MinValue;
                case "System.Int64":
                    return Int64.MinValue;
                case "System.Byte":
                    return Byte.MinValue;
                case "System.UInt":
                    return uint.MinValue;
                case "System.UInt16":
                    return UInt16.MinValue;
                case "System.UInt32":
                    return UInt32.MinValue;
                case "System.UInt64":
                    return UInt64.MinValue;
                case "System.String":
                    return String.Empty;
                case "System.DateTime":
                    return DateTime.MinValue;
                case "System.Decimal":
                    return Decimal.MinValue;
                case "System.Float":
                    return float.MinValue;
                case "System.Boolean":
                    return false;
                case "System.Guid":
                    return Guid.Empty;
                case "System.Char":
                    return char.MinValue;
                case "System.Double":
                    return double.MinValue;
            }
            return string.Empty;
        }

        public static bool IsNull(object value, string dataType)
        {
            switch (dataType)
            {
                case "System.Int32":
                    return (int)value == int.MinValue;
                case "System.String":
                    return value == null;
                case "System.Int16":
                    return (Int16)value == Int16.MinValue;
                case "System.Int64":
                    return (Int64)value == Int64.MinValue;
                case "System.Byte":
                    return (Byte)value == Byte.MinValue;
                case "System.UInt":
                    return (uint)value == uint.MinValue;
                case "System.UInt16":
                    return (UInt16)value == UInt16.MinValue;
                case "System.UInt32":
                    return (UInt32)value == UInt32.MinValue;
                case "System.UInt64":
                    return (UInt64)value == UInt64.MinValue;
                case "System.DateTime":
                    return (DateTime)value == DateTime.MinValue;
                case "System.Decimal":
                    return (Decimal)value == Decimal.MinValue;
                case "System.Float":
                    return (float)value == float.MinValue;
                case "System.Boolean":
                    return false;
                case "System.Guid":
                    return (Guid)value == Guid.Empty;
                case "System.Char":
                    return (char)value == char.MinValue;
                case "System.Double":
                    return (double)value == double.MinValue;
            }
            return false;
        }

        public static object Parse(string valueToParse, Type targetType)
        {
            if (targetType.FullName == "System.Guid")
            {
                if (valueToParse == string.Empty)
                {
                    return Guid.Empty;
                }
                return new Guid(valueToParse);
            }
            return Convert.ChangeType(valueToParse, targetType);
        }

        public static object Parse(string valueToParse, string typeName)
        {
            if (typeName == "System.Guid")
                return new Guid(valueToParse);
            else
                return Convert.ChangeType(valueToParse, Type.GetType(typeName));
        }
    }
}
