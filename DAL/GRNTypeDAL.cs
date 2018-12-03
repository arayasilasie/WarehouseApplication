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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.DAL
{
    public class GRNTypeDAL
    {
        public static List<GRNTypeBLL> GetActiveGRNTypes()
        {
            List<GRNTypeBLL> list;
            try
            {
                ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
                ECXLookUp.CGRNType[] obj = objEcxLookUp.GetActiveGRNTypes(Utility.GetWorkinglanguage());

                if (obj != null)
                {
                    if (obj.Count() > 0)
                    {
                        list = new List<GRNTypeBLL>();
                        foreach (ECXLookUp.CGRNType i in obj)
                        {
                            GRNTypeBLL o = new GRNTypeBLL();
                            o.Id = i.UniqueIdentifier;
                            o.Name = i.Name;
                            list.Add(o);
                        }
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Get GRN Types.",ex);
            }
            
        }
    }
}
