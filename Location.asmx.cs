using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for Location
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Location : System.Web.Services.WebService
    {

        [WebMethod]
        public void tets()
        {
        }


        [WebMethod]
        public CascadingDropDownNameValue[] GetRegions(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CRegion[] objRegion = objEcxLookUp.GetActiveRegions( WarehouseApplication.BLL.Utility.GetWorkinglanguage());
            foreach (ECXLookUp.CRegion region in objRegion)
            {
                l.Add(new CascadingDropDownNameValue(region.Name.ToString(), region.UniqueIdentifier.ToString()));
            }
           

            return l.ToArray();
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetZones(string knownCategoryValues, string category)
        {
            
            string RegionID = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("Region") || kv["Region"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Region.");
            }
            RegionID = kv["Region"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CZone[] objZone = objEcxLookUp.GetActiveZones(WarehouseApplication.BLL.Utility.GetWorkinglanguage(), new Guid(RegionID.ToString()));
            foreach (ECXLookUp.CZone zone in objZone )
            {
                l.Add(new CascadingDropDownNameValue(zone.Name.ToString(), zone.UniqueIdentifier.ToString()));
            }
            return l.ToArray();
        
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetWoredas(string knownCategoryValues, string category)
        {
            
            string ZoneID = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("Zone") || kv["Zone"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Zone.");
            }
            ZoneID = kv["Zone"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CWoreda[] objWoreda = objEcxLookUp.GetActiveWoredas(WarehouseApplication.BLL.Utility.GetWorkinglanguage(), new Guid(ZoneID.ToString()));
            foreach (ECXLookUp.CWoreda woreda in objWoreda)
            {
                l.Add(new CascadingDropDownNameValue(woreda.Name.ToString(), woreda.UniqueIdentifier.ToString()));
            }
            return l.ToArray();

        }
    }
}
