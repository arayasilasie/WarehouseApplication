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
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public class GRNListBLL
    {
        #region Fields
        private Guid _id;
        private string _GRN;
        private Guid _commodityId;
        private Guid _CommodityClassId;
        private Guid _commodityGradeId;
        private Guid _clientId;
        private GRNStatus _status;
        private string _clinetName;
        private string _commodity;
        private string _commodityClass;
        private string _commodityGrade;
        private float _originalQuantity;
        private DateTime _dateDeposited;


        #endregion
        #region Properites

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string GRN
        {
            get { return _GRN; }
            set { _GRN = value; }
        }
        public Guid CommodityId
        {
            get { return _commodityId; }
            set { _commodityId = value; }
        }
        public Guid CommodityClassId
        {
            get { return _CommodityClassId; }
            set { _CommodityClassId = value; }
        }
        public Guid CommodityGradeId
        {
            get { return _commodityGradeId; }
            set { _commodityGradeId = value; }
        }
        public Guid ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
        public GRNStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string ClinetName
        {
            get { return _clinetName; }
            set { _clinetName = value; }
        }
        public string Commodity
        {
            get { return _commodity; }
            set { _commodity = value; }
        }
        public string CommodityClass
        {
            get { return _commodityClass; }
            set { _commodityClass = value; }
        }
        public string CommodityGrade
        {
            get { return _commodityGrade; }
            set { _commodityGrade = value; }
        }
        public float OriginalQuantity
        {
            get { return _originalQuantity; }
            set { _originalQuantity = value; }
        }
        public DateTime DateDeposited
        {
            get { return _dateDeposited; }
            set { _dateDeposited = value; }
        }

        #endregion

        public List<GRNListBLL> Search(string GRN, string TrackingNo, Nullable<Guid> ClientId, Nullable<Guid> CommodityId, Nullable<Guid> CommoidtyClassId, Nullable<Guid> CommodityGradeId, Nullable<GRNStatus> Status, Nullable<DateTime> From, Nullable<DateTime> To)
        {
            List<GRNListBLL> lstGRNlist = null;
            List<GRNBLL> GRNlist = new List<GRNBLL>();
            // check search parameters are there.
            if ((string.IsNullOrEmpty(GRN) == true) && (string.IsNullOrEmpty(TrackingNo) == true) && (ClientId == null) && (CommodityId == null) && (CommoidtyClassId == null) && (CommodityGradeId == null) && (Status == null) && (From == null) && (To == null))
            {
                throw (new NULLSearchParameterException("No Search parameter"));
            }

            GRNlist = GRNDAL.Search(GRN, TrackingNo, ClientId, CommodityId, CommodityClassId, CommodityGradeId, Status, From, To);

            if (GRNlist != null)
            {
                if (GRNlist.Count > 0)
                {
                    lstGRNlist = new List<GRNListBLL>();
                    GRNListBLL oblGRNList = new GRNListBLL();
                    foreach (GRNBLL o in GRNlist)
                    {
                        GRNListBLL obj = new GRNListBLL();
                        obj.Id = o.Id;
                        obj.GRN = o.GRN_Number;
                        obj.CommodityId = o.CommodityId;
                        obj.CommodityClassId = o.CommodityClassId;
                        obj.CommodityGradeId = o.CommodityGradeId;
                        obj.Status = (GRNStatus)o.Status;
                        obj.ClinetName = ClientBLL.GetClinetNameById(o.ClientId);
                        obj.OriginalQuantity = o.OriginalQuantity;
                        obj.DateDeposited = Convert.ToDateTime(o.DateDeposited.ToShortDateString());
                        lstGRNlist.Add(obj);


                    }
                }
                else
                {
                    return null;
                }
            }
            return lstGRNlist;

        }

        private List<GRNListBLL> MergeWithClient(List<GRNBLL> list)
        {
            //Get Client list.
            List<GRNListBLL> grnList = new List<GRNListBLL>();
            List<ClientBLL> ClientList = new List<ClientBLL>();
            ClientList = ClientBLL.GetAllClient();
            if (ClientList == null)
            {
                throw new ClientInformationException("Can not get Clinet Information");
            }
            else
            {
                var q = from GRN in list
                        join client in ClientList on GRN.ClientId equals client.ClientUniqueIdentifier
                        select new { GRN.Id, GRN.GRN_Number, GRN.CommodityId, GRN.CommodityClassId, GRN.CommodityGradeId, GRN.Status, GRN.OriginalQuantity, GRN.DateDeposited, client.ClientName };
                foreach (var i in q)
                {
                    GRNListBLL obj = new GRNListBLL();
                    obj.Id = i.Id;
                    obj.GRN = i.GRN_Number;
                    obj.CommodityId = i.CommodityId;
                    obj.CommodityClassId = i.CommodityClassId;
                    obj.CommodityGradeId = i.CommodityGradeId;
                    obj.Status = (GRNStatus)i.Status;
                    obj.ClinetName = i.ClientName;
                    obj.OriginalQuantity = i.OriginalQuantity;
                    obj.DateDeposited = Convert.ToDateTime(i.DateDeposited.ToShortDateString());
                    obj.CommodityGrade = CommodityGradeBLL.GetCommodityGradeNameById(i.CommodityGradeId);
                    grnList.Add(obj);
                }
                return grnList;

            }
        }

        //private List<GRNListBLL> MergeWithCommodityGrade(List<GRNListBLL> list)
        //{
        //    List<GRNListBLL> grnList = new List<GRNListBLL>();
        //    List<CommodityGradeBLL> commList = new List<CommodityGradeBLL>();
        //    // Get commodity List - 
        //    //TODO CAching should be done.
        //    commList = CommodityGradeBLL.GetAllCommodityDetail();
        //    if (commList == null || commList.Count <= 0)
        //    {
        //        throw new CommodityDetailException("Unable To get Commodity information.");

        //    }
        //    else
        //    {
        //        var q = from GRN in list 
        //                join comm in commList on GRN.CommodityGradeId equals comm.CommodityGradeId
        //                select new { GRN.Id, GRN.GRN, GRN.CommodityId, GRN.CommodityClassId, GRN.CommodityGradeId, GRN.Status, GRN.OriginalQuantity, GRN.DateDeposited,GRN.ClinetName, comm.GradeName };
        //        foreach (var i in q)
        //        {
        //            GRNListBLL obj = new GRNListBLL();
        //            obj.Id = i.Id;
        //            obj.GRN = i.GRN;
        //            obj.CommodityId = i.CommodityId;
        //            obj.CommodityClassId = i.CommodityClassId;
        //            obj.CommodityGradeId = i.CommodityGradeId;
        //            obj.Status = (GRNStatus)i.Status;
        //            obj.ClinetName = i.ClinetName;
        //            obj.OriginalQuantity = i.OriginalQuantity;
        //            obj.DateDeposited = Convert.ToDateTime(i.DateDeposited.ToShortDateString());
        //            obj.CommodityGrade = i.GradeName;
        //            grnList.Add(obj);
        //        }
        //        return grnList;
        //    }

        //}





    }
}
