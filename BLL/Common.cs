using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ECX.DataAccess
{
    public class Common
    {

        public static void DataRow2Object(System.Data.DataRow dr, object ob)
        {
            if (dr == null) return;
            if (dr.Table.Columns.Count < ob.GetType().GetProperties().Count())
                DataRow2ObjectByTable(dr, ob);
            else
                DataRow2ObjectByObject(dr, ob);
        }

        private static void DataRow2ObjectByTable(System.Data.DataRow dr, object ob)
        {
            if (dr == null) return;
            String obName = ob.GetType().Name;
            bool written = false;
            System.Reflection.PropertyInfo pInfo;
            foreach (System.Data.DataColumn dc in dr.Table.Columns)
            {
                try
                {
                    pInfo = ob.GetType().GetProperty(dc.ColumnName);
                    if (pInfo == null) continue;
                    if (!pInfo.CanWrite) continue;
                    System.Reflection.MethodInfo mInfo = pInfo.GetSetMethod(false);
                    if (mInfo == null) continue;
                    if (!dr.Table.Columns.Contains(pInfo.Name))
                    {
                        if (!written)
                        {
                            System.Diagnostics.Debug.WriteLine(obName);
                            written = true;
                        }
                        System.Diagnostics.Debug.WriteLine("Columns Not found: " + pInfo.Name);
                        continue;
                    }
                    if (dr.IsNull(pInfo.Name)) continue;
                    object v = dr[pInfo.Name];
                    if ((v == null) || (v == DBNull.Value)) continue;
                    pInfo.SetValue(ob, dr[pInfo.Name], null);
                    if (pInfo.Name == "IsDirty" || pInfo.Name == "IsDeleted")
                        pInfo.SetValue(ob, false, null);
                }
                catch (Exception ex)
                {
                    if (!written)
                    {
                        System.Diagnostics.Debug.WriteLine(obName);
                        written = true;
                    }
                    System.Diagnostics.Debug.WriteLine(dc.ColumnName + "  Exception: " + ex.Message);
                }
            }
        }

        private static void DataRow2ObjectByObject(System.Data.DataRow dr, object ob)
        {
            if (dr == null) return;
            String obName = ob.GetType().Name;
            bool written = false;
            foreach (System.Reflection.PropertyInfo pInfo in ob.GetType().GetProperties())
            {
                try
                {
                    if (!pInfo.CanWrite) continue;
                    System.Reflection.MethodInfo mInfo = pInfo.GetSetMethod(false);
                    if (mInfo == null) continue;
                    if (!dr.Table.Columns.Contains(pInfo.Name))
                    {
                        if (!written)
                        {
                            System.Diagnostics.Debug.WriteLine(obName);
                            written = true;
                        }
                        System.Diagnostics.Debug.WriteLine("Columns Not found: " + pInfo.Name);
                        continue;
                    }
                    if (dr.IsNull(pInfo.Name)) continue;
                    object v = dr[pInfo.Name];
                    if ((v == null) || (v == DBNull.Value)) continue;
                    pInfo.SetValue(ob, dr[pInfo.Name], null);
                    if (pInfo.Name == "IsDirty" || pInfo.Name == "IsDeleted")
                        pInfo.SetValue(ob, false, null);
                }
                catch (Exception ex)
                {
                    if (!written)
                    {
                        System.Diagnostics.Debug.WriteLine(obName);
                        written = true;
                    }
                    System.Diagnostics.Debug.WriteLine(pInfo.Name + "  Exception: " + ex.Message);
                }
            }
        }

        private const string SecurityManagerConnection = "Data Source=(local);Initial Catalog=ECXSecurityManager;Integrated Security=True";
        public static Guid GetUserID(string userName)
        {
            object ob = SQLHelper.ExecuteScalar(SecurityManagerConnection, "Login", userName);
            if (ob == null) return Guid.Empty;
            return new Guid(ob.ToString());
        }
        public static System.Data.DataRow GetUserInfo(Guid userid)
        {
            return SQLHelper.getDataRow(SecurityManagerConnection, "getUserInfo", userid);
        }
    }
}
