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
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    /// <summary>
    /// Summary description for Commodity
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Commodity : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] GetCommodities(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            List <CommodityGradeBLL> list = CommodityGradeBLL.GetAllCommodity();
            foreach (CommodityGradeBLL commodity in list)
            {
                l.Add(new CascadingDropDownNameValue(commodity.Commodity.ToString(), commodity.CommodityId.ToString()));
            }
            return l.ToArray();
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetCommoditiesContext(string knownCategoryValues, string category, string contextKey)
        {
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            List<CommodityGradeBLL> list = CommodityGradeBLL.GetAllCommodity();
            foreach (CommodityGradeBLL commodity in list)
            {
                if (commodity.CommodityId.ToString().Trim().ToUpper() == contextKey.Trim().ToUpper())
                {
                    l.Add(new CascadingDropDownNameValue(commodity.Commodity.ToString(), commodity.CommodityId.ToString()));
                }
            }
            return l.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetCommodityClass(string knownCategoryValues, string category)
        {

            string ID = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("Commodity") || kv["Commodity"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Commodity.");
            }
            ID = kv["Commodity"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            List<CommodityGradeBLL> list = CommodityGradeBLL.GetCommodityClassByCommodityId(new Guid(ID));

            foreach (CommodityGradeBLL  commodityClass in list)
            {
                l.Add(new CascadingDropDownNameValue(commodityClass.ClassName .ToString(), commodityClass.CommodityClassId.ToString()));
            }
            return l.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetCommodityGrades(string knownCategoryValues, string category)
        {

            string ID = "";
            StringDictionary kv;
            kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            if (!kv.ContainsKey("CommodityClass") || kv["CommodityClass"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Zone.");
            }
            ID = kv["CommodityClass"];
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            List<CommodityGradeBLL> list = CommodityGradeBLL.GetCommodityGradeByClassId(new Guid(ID));


            foreach (CommodityGradeBLL cc in list)
            {
                l.Add(new CascadingDropDownNameValue(cc.GradeName.ToString(), cc.CommodityGradeId.ToString()));
            }
            return l.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetActiveTruckTypes(string knownCategoryValues, string category)
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
            catch (Exception ex)
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
            if (!kv.ContainsKey("TruckModel") || kv["TruckModel"].ToString() == "")
            {
                throw new ArgumentException("Couldn't find selected Truck Type.");
            }
            ModelId = kv["TruckModel"];
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
