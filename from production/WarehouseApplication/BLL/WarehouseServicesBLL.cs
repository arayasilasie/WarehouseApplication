using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WarehouseApplication.BLL
{
    public class WarehouseServicesBLL
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public List<WarehouseServicesBLL> GetActiveServices()
        {
            List<WarehouseServicesBLL> list = null;
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CService[] objServices = objEcxLookUp.GetActiveServices(Utility.GetWorkinglanguage());
            if (objServices != null)
            {
                if (objServices.Count() > 0)
                {
                    list = new List<WarehouseServicesBLL>();
                    foreach (ECXLookUp.CService s in objServices)
                    {
                        WarehouseServicesBLL obj = new WarehouseServicesBLL();
                        obj.Id = s.UniqueIdentifier;
                        obj.Name = s.Name;
                        obj.Status = s.Status;
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public List<WarehouseServicesBLL> GetServices()
        {
            List<WarehouseServicesBLL> list = null;
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CService[] objServices = objEcxLookUp.GetActiveServices(Utility.GetWorkinglanguage());
            if (objServices != null)
            {
                if (objServices.Count() > 0)
                {
                    list = new List<WarehouseServicesBLL>();
                    foreach (ECXLookUp.CService s in objServices)
                    {
                        WarehouseServicesBLL obj = new WarehouseServicesBLL();
                        obj.Id = s.UniqueIdentifier;
                        obj.Name = s.Name;
                        obj.Status = s.Status;
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public static string GetServiceNameById(Guid Id)
        {
            ECXLookUp.ECXLookup objEcxLookUp = new WarehouseApplication.ECXLookUp.ECXLookup();
            ECXLookUp.CService objServices = objEcxLookUp.GetService(Utility.GetWorkinglanguage(), Id);
            return objServices.Name;


        }
    }
}
