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
using System.Collections.Generic;

namespace WarehouseApplication
{
    public class PageDataTransfer
    {
        private string targetPage;
        private Dictionary<string, object> transferData = new Dictionary<string, object>();

        public PageDataTransfer(string targetPage)
        {
            this.targetPage = targetPage;
        }

        public Dictionary<string, object> TransferData
        {
            get { return transferData; }
        }

        public void Navigate(bool endResponse)
        {
            PersistToSession();
            HttpContext.Current.Response.Redirect(targetPage, endResponse);
        }

        public void Navigate()
        {
            Navigate(true);
        }

        public bool IsDataTransfered(string key)
        {
            string sessionValueName = string.Format("{0}-{1}", targetPage, key);
            return (HttpContext.Current.Session[sessionValueName] != null);
        }
        public object GetTransferedData(string key)
        {
            string sessionValueName = string.Format("{0}-{1}", targetPage, key);
            return HttpContext.Current.Session[sessionValueName];
        }

        public void RemoveAllData()
        {
            List<string> keysToRemove = new List<string>();
            foreach (string key in HttpContext.Current.Session.Keys)
            {
                string targetKeys = string.Format("{0}-", targetPage);
                if ((key.Length > targetKeys.Length) && (key.Substring(0, targetPage.Length) == targetPage))
                    keysToRemove.Add(key);
            }
            foreach (string key in keysToRemove)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public void RemoveData(string key)
        {
            HttpContext.Current.Session.Remove(string.Format("{0}-{1}", targetPage, key));
        }

        public void PersistToSession()
        {
            foreach (string key in transferData.Keys)
            {
                string sessionValueName = string.Format("{0}-{1}", targetPage, key);
                HttpContext.Current.Session[sessionValueName] = transferData[key];
            }
        }

        public void Return()
        {
            PageDataTransfer transfer = new PageDataTransfer((string)GetTransferedData("ReturnPage"));
            RemoveAllData();
            transfer.Navigate();            

        }

        public void Return(string url)
        {
            PageDataTransfer transfer = new PageDataTransfer(url);
            RemoveAllData();
            transfer.Navigate();            
        }

        public void Return(string url, Dictionary<string, object> parameter)
        {
            PageDataTransfer transfer = new PageDataTransfer(url);
            foreach (string key in parameter.Keys)
            {
                transfer.TransferData[key] = parameter[key];
            }
            RemoveAllData();
            transfer.Navigate();
        }

        public void Store(string key, object data)
        {
            HttpContext.Current.Session[key] = data;
        }
    }
}
