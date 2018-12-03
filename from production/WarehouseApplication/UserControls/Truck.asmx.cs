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
    /// Summary description for Truck
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Truck : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //Truck type
        [WebMethod]
        public CascadingDropDownNameValue[] GetActiveTruckType(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            TruckTypeBLL objTruckType = new TruckTypeBLL();
            List<TruckTypeBLL> list = new List<TruckTypeBLL>();
            list = objTruckType.GetActiveTrucksTypes();
            foreach (TruckTypeBLL o in list)
            {
                l.Add(new CascadingDropDownNameValue(o.TruckTypeName.ToString(), o.Id.ToString()));
            }
           

            return l.ToArray();
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetAllTruckType(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            TruckTypeBLL objTruckType = new TruckTypeBLL();
            List<TruckTypeBLL> list = new List<TruckTypeBLL>();
            list = objTruckType.GetAllTrucksTypes();
            foreach (TruckTypeBLL o in list)
            {
                l.Add(new CascadingDropDownNameValue(o.TruckTypeName.ToString(), o.Id.ToString()));
            }


            return l.ToArray();
        }

        //Trauck Model
        [WebMethod]
        public CascadingDropDownNameValue[] GetActiveTruckModels(string knownCategoryValues, string category)
        {

            try
            {
                string TruckTypeID = "";
                StringDictionary kv;
                kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                if (!kv.ContainsKey("TruckType") || kv["TruckType"].ToString() == "")
                {
                    throw new ArgumentException("Couldn't find selected Truck Type.");
                }
                TruckTypeID = kv["TruckType"];
                List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
                TruckModelBLL objTm = new TruckModelBLL();
                List<TruckModelBLL> listTM = new List<TruckModelBLL>();
                listTM = objTm.GetActiveTrucksByTypeId(new Guid(TruckTypeID));
                foreach (TruckModelBLL o in listTM)
                {
                    l.Add(new CascadingDropDownNameValue(o.TruckModelName, o.Id.ToString()));
                }
                return l.ToArray();
            }
            catch( Exception ex)
            {
                throw ex;
            }


        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetAllTruckModels(string knownCategoryValues, string category)
        {

            string TruckTypeID = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("TruckType") || kv["TruckType"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Truck Type.");
            }
            TruckTypeID = kv["TruckType"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            TruckModelBLL objTm = new TruckModelBLL();
            List<TruckModelBLL> listTM = new List<TruckModelBLL>();
            listTM = objTm.GetAllTrucksByTypeId(new Guid(TruckTypeID));
            foreach (TruckModelBLL o in listTM)
            {
                l.Add(new CascadingDropDownNameValue(o.TruckModelName, o.Id.ToString()));
            }
            return l.ToArray();

        }


        //Truck Model Year
        [WebMethod]
        public CascadingDropDownNameValue[] GetActiveTruckModelYear(string knownCategoryValues, string category)
        {

            string ModelId = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("ModelId") || kv["ModelId"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Truck Type.");
            }
            ModelId = kv["ModelId"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            TruckModelYearBLL objTm = new TruckModelYearBLL();
            List<TruckModelYearBLL> listTM = new List<TruckModelYearBLL>();
            listTM = objTm.GetActiveTruckModelYearByModelId(new Guid(ModelId));
            foreach (TruckModelYearBLL o in listTM)
            {
                l.Add(new CascadingDropDownNameValue(o.ModelYearName, o.Id.ToString()));
            }
            return l.ToArray();

        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetAllTruckModelYear(string knownCategoryValues, string category)
        {

            string ModelId = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("ModelId") || kv["ModelId"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Truck Type.");
            }
            ModelId = kv["ModelId"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            TruckModelYearBLL objTm = new TruckModelYearBLL();
            List<TruckModelYearBLL> listTM = new List<TruckModelYearBLL>();
            listTM = objTm.GetAllTrucksByModelId(new Guid(ModelId));
            foreach (TruckModelYearBLL o in listTM)
            {
                l.Add(new CascadingDropDownNameValue(o.ModelYearName, o.Id.ToString()));
            }
            return l.ToArray();

        }

    }
}
