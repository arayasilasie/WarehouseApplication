using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections;

namespace WarehouseApplication.BLL
{
    public class GradingByCollection : CollectionBase , IList
    {
        private ISite _site;  //required for the IComponent implementation
        private List<GradingByBLL> list = new List<GradingByBLL>();
        public GradingByCollection()
        {
        }
        public GradingByCollection(List<GradingByBLL> Graders)
        {
            foreach (GradingByBLL obj in Graders)
            {
              
                this.list.Add(obj);
            }
            this.list = Graders;
            _site = null;
        }

        #region Implementation of IComponent

        public event System.EventHandler Disposed;

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

        /// <summary>
        /// Required for IComponent implementation
        /// </summary>
        public void Dispose()
        {
            OnDisposed(EventArgs.Empty);
        }

        /// <summary>
        /// Required for IComponent implementation
        /// </summary>
        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }
}
