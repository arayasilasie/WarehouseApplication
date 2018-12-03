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
    public class InvalidLotSizeException : Exception
    {
        public InvalidLotSizeException(string msg)
        {

        }
    }
    public class GRNAddException : Exception
    {
        public GRNAddException(string msg)
        {

        }
    }
    public class IndeterminateGRNCountException : Exception
    {
        public IndeterminateGRNCountException(string msg)
        {
        }
    }
    public class MultipleGRNForSingleGradingCodeException : Exception
    {

    }
    public class ClientInformationException : Exception
    {
        public string msg;
        public ClientInformationException(string str)
        {
            this.msg = str;
        }
    }
    public class CommodityDetailException : Exception
    {
        public string msg;
        public CommodityDetailException(string str)
        {
            this.msg = str;
        }
    }
    public class InvalidIdException : Exception
    {
        public string msg;
        public InvalidIdException(string str)
        {
            this.msg = str;
        }
    }
    public class InvalidTransactionType : Exception
    {
        public string msg;
        public InvalidTransactionType(string str)
        {
            this.msg = str;
        }
    }
    public class CodeGenerationException : Exception
    {
        public string msg;
        public CodeGenerationException(string str)
        {
            this.msg = str;
        }
    }
    public class InvalidTareException : Exception
    {
         public string msg;
        public InvalidTareException(string str)
        {
            this.msg = str;
        }

    }
    public class InvalidTransactionNumber : Exception
    {
        public string msg;
        public InvalidTransactionNumber(string str)
        {
            this.msg = str;
        }

    }
    public class DuplicateDriverInformationException : Exception
    {
        public string msg;
        public DuplicateDriverInformationException(string str)
        {
            this.msg = str;
        }

    }
    public class NULLSearchParameterException : Exception
    {
        public string msg;
        public NULLSearchParameterException(string str)
        {
            this.msg = str;
        }

    }
    public class GRNNotOnUpdateStatus : Exception
    {
        public string msg;
        public GRNNotOnUpdateStatus(string str)
        {
            this.msg = "GRN Is Created using this record.Data can not be Modified";
        }
    }
   

}
