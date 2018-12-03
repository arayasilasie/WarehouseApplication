using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using WarehouseApplication.BLL;


namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for CDDTrackRegistration
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CDDTrackRegistration : System.Web.Services.WebService
    {
        [WebMethod]
        public CascadingDropDownNameValue[] GetTruckType(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
                TruckTypeBLL objTT = new TruckTypeBLL();
                List<TruckTypeBLL> listTT = new List<TruckTypeBLL>();
                listTT = objTT.GetActiveTrucksTypes().OrderBy(tt=>tt.TruckTypeName).ToList();
                foreach (TruckTypeBLL o in listTT)
                {
                    l.Add(new CascadingDropDownNameValue(o.TruckTypeName, o.Id.ToString()));
                }
                
                return l.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetTruckTypeModels(string knownCategoryValues, string category)
        {
            try
            {
                string[] categoryValues = knownCategoryValues.Split(':', ';');
                Guid truckTypeId = new Guid(categoryValues[1]);
                List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
                TruckModelBLL objTm = new TruckModelBLL();
                List<TruckModelBLL> listTM = new List<TruckModelBLL>();
                listTM = objTm.GetActiveTrucksByTypeId(truckTypeId).OrderBy(tm=>tm.TruckModelName).ToList();
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
        public CascadingDropDownNameValue[] GetTruckTypeModelYears(string knownCategoryValues, string category)
        {

            string[] categoryValues = knownCategoryValues.Split(':', ';');
            Guid truckModelId = new Guid(categoryValues[1]);
            List<CascadingDropDownNameValue> l = new List<CascadingDropDownNameValue>();
            TruckModelYearBLL objTm = new TruckModelYearBLL();
            List<TruckModelYearBLL> listTM = new List<TruckModelYearBLL>();
            listTM = objTm.GetActiveTruckModelYearByModelId(truckModelId).OrderBy(tmy=>tmy.ModelYearName).ToList();
            foreach (TruckModelYearBLL o in listTM)
            {
                l.Add(new CascadingDropDownNameValue(o.ModelYearName, o.Id.ToString()));
            }
            return l.ToArray();
        }
    }
}
