using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for GRNWRSync
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GRNWRSync : System.Web.Services.WebService
    {

        [WebMethod]
        public bool CancelGRN(Guid GRNId, string TrackingNo, RequestforApprovedGRNCancelationStatus status)
        {

            bool isSaved = false;
            GRNBLL objGRN = new GRNBLL();
            objGRN = objGRN.GetbyGRN_Number(GRNId);
            GRNStatus GRNstatus = (GRNStatus)objGRN.Status;
            if (status == RequestforApprovedGRNCancelationStatus.Cancelled)
            {
                GRNstatus = GRNStatus.Cancelled;
            }
            if (objGRN != null)
            {
                if (GRNstatus == GRNStatus.Cancelled)
                {
                    isSaved = objGRN.Update(objGRN.GRN_Number, GRNstatus, objGRN, TrackingNo, DateTime.Now);
                }
                if (isSaved == true)
                {
                    ECXWF.CMessage mess = WFTransaction.Request(TrackingNo);
                    WFTransaction.WorkFlowManager(TrackingNo, mess);
                    isSaved = true;
                }
            }
            else
            {
                throw new Exception("Unable to find GRN");
            }
            return isSaved;

        }

        [WebMethod]
        public bool CancelGRNCancellationRequest(string TrackingNo)
        {
            bool isSaved = false;
            Utility.LogException(new Exception(TrackingNo));
            RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
            isSaved = obj.CancelGRNCancellationRequest(TrackingNo);
            return isSaved;
        }


        
    }
}
