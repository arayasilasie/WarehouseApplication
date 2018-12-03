using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using WarehouseApplication.GINLogic;
using WarehouseApplication.DALManager;

namespace WarehouseApplication.Reports
{
    public class GINReportDataCollection : CollectionBase, IComponent
    {
        private ISite _site;

        public GINReportDataCollection(GINReportInfo ginReportInfo, ILookupSource lookupSource)
        {
            _site = null;
            
            List.Add(
                new GINReportData(
                    ginReportInfo.GINNo,
                    ginReportInfo.DateIssued,
                    lookupSource.GetLookup("Warehouse")[ginReportInfo.WarehouseId],
                    lookupSource.GetLookup("Client")[ginReportInfo.ClientId],
                    lookupSource.GetLookup("ClientId")[ginReportInfo.ClientId],
                    lookupSource.GetLookup("PUNAgent")[ginReportInfo.PickupNoticeAgentId],
                    ginReportInfo.PUNAIdNo,
                    lookupSource.GetLookup("NIDType")[ginReportInfo.PUNANIDType],
                    lookupSource.GetLookup("CommodityGrade")[ginReportInfo.CommodityGradeId],
                    ginReportInfo.Quantity,
                    ginReportInfo.Weight,
                    ginReportInfo.ScaleTicketNo,
                    lookupSource.GetLookup("BagType")[ginReportInfo.BagType],
                    ginReportInfo.Bags,
                    false,
                    string.Empty,
                    string.Empty,
                    ginReportInfo.DriverName,
                    ginReportInfo.LicenseNo,
                    ginReportInfo.IssuedBy,
                    ginReportInfo.PlateNo,
                    lookupSource.GetLookup("GINStatus")[ginReportInfo.Status]
                    ));
        }

        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            OnDisposed(EventArgs.Empty);
        }

        #endregion

        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }

    public class TrackingReportDataCollection : CollectionBase, IComponent
    {
        private ISite _site;

        public TrackingReportDataCollection(TrackingReportData trackingReportData)
        {
            _site = null;

            List.Add(trackingReportData);
        }

        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            OnDisposed(EventArgs.Empty); ;
        }

        #endregion

        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }

    public class PUNReportDataCollection : CollectionBase, IComponent
    {
        private ISite _site;

        public PUNReportDataCollection(PUNReportData punReportData)
        {
            _site = null;

            List.Add(punReportData);
        }

        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            OnDisposed(EventArgs.Empty); ;
        }

        #endregion

        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }

    public class WRReportDataCollection : CollectionBase, IComponent
    {
        private ISite _site;

        public WRReportDataCollection(List<WRReportData> wrReportData)
        {
            _site = null;

            wrReportData.ForEach(wr=>List.Add(wr));
        }

        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            OnDisposed(EventArgs.Empty); ;
        }

        #endregion

        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }

    public class GINTrackingReportDataCollection : CollectionBase, IComponent
    {
        private ISite _site;

        public GINTrackingReportDataCollection() 
        {
            _site = null;
        }
        public GINTrackingReportDataCollection(GINTrackingReportData ginTrackingReportData)
        {
            _site = null;

            List.Add(ginTrackingReportData);
        }

        public void Add(GINTrackingReportData ginTrackingReportData)
        {
            List.Add(ginTrackingReportData);
        }

        public void AddList(List<GINTrackingReportData> ginTrackingReports)
        {
            ginTrackingReports.ForEach(trd => List.Add(trd));
        }
        #region IComponent Members

        public event EventHandler Disposed;

        public ISite Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            OnDisposed(EventArgs.Empty); ;
        }

        #endregion

        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }
}
