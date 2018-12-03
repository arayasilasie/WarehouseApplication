using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Web.Caching;

using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public class InboxContent
    {
        private String _TaskName;
        private int _Count;
        public String TaskName
        {
            get { return _TaskName; }
            set { _TaskName = value; }
        }
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
        public DateTime InboxGeneratedTime { get; set; }



        public static List<InboxContent> GetListForInbox()
        {
            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            string cacheName = "InboxContent" + WarehouseId.ToString();
            List<InboxContent> lst = null;
            if (HttpContext.Current.Cache[cacheName] != null)
            {
                return (List<InboxContent>)HttpContext.Current.Cache[cacheName];
            }
            else
            {
                lst = InboxCountDAL.GetInboxItemsByWarehouseId(WarehouseId);
                if (lst != null)
                {
                    HttpContext.Current.Cache.Insert(cacheName, lst, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
                }
            }
            return lst;
        }
    }

    public class InBoxList
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class InboxRowGrid
    {
        private Dictionary<string, List<string>> _InboxRow = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> InboxRow
        {
            get { return _InboxRow; }
            set { _InboxRow = value; }
        }


        public List<InBoxList> GetInbox(List<InboxContent> listTaskNameWithCount, InboxRowGrid dictInboxRowGrid)
        {
            List<InBoxList> listIn = new List<InBoxList>();
            List<InboxRowGrid> list = new List<InboxRowGrid>();


            if (listTaskNameWithCount != null)
            {

                foreach (string s in dictInboxRowGrid.InboxRow.Keys)
                {
                    InBoxList oInbox = new InBoxList();
                    oInbox.Name = s;
                    oInbox.Count = 0;
                    foreach (string ss in dictInboxRowGrid.InboxRow[s])
                    {
                        Boolean isFound = false;
                        foreach (InboxContent sss in listTaskNameWithCount)
                        {

                            if (sss.TaskName.ToUpper() == ss.ToUpper())
                            {
                                oInbox.Count += sss.Count;
                                isFound = true;
                                break;
                            }

                        }
                        if (isFound == true)
                        {
                            break;
                        }
                    }
                    listIn.Add(oInbox);
                }




            }

            return listIn;
        }
    }
}
