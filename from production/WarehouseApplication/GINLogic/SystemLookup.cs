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
using WarehouseApplication.DALManager;
using System.Collections.Generic;
using WarehouseApplication.BLL;

namespace WarehouseApplication.GINLogic
{
    public class SystemLookup
    {
        public static ILookupSource LookupSource
        {
            get { return new SystemLookupSource(); }
        }

        public static void ExpireWarehouseData()
        {
        }

        private class SystemLookupSource : ILookupSource
        {
            private Dictionary<object, ClientBLL> clientLookup = new Dictionary<object, ClientBLL>();
            private Dictionary<object, WarehouseBLL> warehouseLookup = new Dictionary<object, WarehouseBLL>();
            //private CachedDictionary<ECXLookUp.CNIDType> nidTypeLookup = new CachedDictionary<ECXLookUp.CNIDType>(
            //                delegate(ECXLookUp.CNIDType nidType)
            //                {
            //                    return nidType.Name;
            //                },
            //                delegate(object key)
            //                {
            //                    return null;
            //                },
            //                delegate()
            //                {
            //                    ECXLookUp.ECXLookup ecxLookup = new WarehouseApplication.ECXLookUp.ECXLookup();
            //                    ECXLookUp.CNIDType[] nidTypes = ecxLookup.GetActiveNIDTypes(BLL.Utility.GetWorkinglanguage());
            //                    List<KeyValuePair<object, WarehouseApplication.ECXLookUp.CNIDType>> kvps = new List<KeyValuePair<object, WarehouseApplication.ECXLookUp.CNIDType>>();
            //                    foreach (ECXLookUp.CNIDType nidType in nidTypes)
            //                    {
            //                        kvps.Add(new KeyValuePair<object, WarehouseApplication.ECXLookUp.CNIDType>(nidType.Id, nidType));
            //                    }
            //                    return kvps;
            //                });
            //private Dictionary<object, string> shedLookup = new Dictionary<object,string>();
            //private Dictionary<object, string> stackLookup = new Dictionary<object,string>();
            //private CachedDictionary<BagTypeBLL> bagTypeLookup = new CachedDictionary<BagTypeBLL>(
            //    delegate(BagTypeBLL bagType)
            //    {
            //        return bagType.BagTypeName;
            //    },
            //    delegate(object key)
            //    {
            //        return BagTypeBLL.GetBagType((Guid)key);
            //    },
            //    delegate()
            //    {
            //        List<KeyValuePair<object, BagTypeBLL>> kvps = new List<KeyValuePair<object, BagTypeBLL>>();
            //        foreach (BagTypeBLL bagType in BagTypeBLL.GetAllBagTypes())
            //        {
            //            kvps.Add(new KeyValuePair<object, BagTypeBLL>(bagType.Id, bagType));
            //        }
            //        return kvps;
            //    });
            
            //private WarehouseBLL currentWareHouse = new WarehouseBLL() {
            //    Code = "102",
            //    WarehouseId = new Guid("fa0a52e8-9308-4d5e-b323-88ca5ba232ed")
            //};
            #region ILookupSource Members

            public IDictionary<object, string> GetLookup(string lookupName)
            {
                try
                {
                    Dictionary<object, string> lookup = new Dictionary<object, string>();
                    switch (lookupName)
                    {
                        case "CommodityGrade":
                            return new CachedDictionary<CommodityGradeBLL>(
                                delegate(CommodityGradeBLL commodityGrade)
                                {
                                    return commodityGrade.GradeName;
                                },
                                delegate(object key)
                                {
                                    return CommodityGradeBLL.GetCommodityGrade((Guid)key);
                                },
                                delegate()
                                {
                                    List<KeyValuePair<object, CommodityGradeBLL>> kvps = new List<KeyValuePair<object, CommodityGradeBLL>>();
                                    foreach (CommodityGradeBLL commodityGrade in CommodityGradeBLL.GetAllCommodityDetail())
                                    {
                                        kvps.Add(new KeyValuePair<object, CommodityGradeBLL>(commodityGrade.CommodityGradeId, commodityGrade));
                                    }
                                    return kvps;
                                });
                        case "Client":
                            return new CachedDictionary<ClientBLL>(
                                clientLookup,
                                delegate(ClientBLL client)
                                {
                                    return client.ClientName;
                                },
                                delegate(object key)
                                {
                                    return ClientBLL.GetClinet((Guid)key);
                                });
                        case "ClientId":
                            return new CachedDictionary<ClientBLL>(
                                clientLookup,
                                delegate(ClientBLL client)
                                {
                                    return client.ClientId;
                                },
                                delegate(object key)
                                {
                                    return ClientBLL.GetClinet((Guid)key);
                                });
                        case "Warehouse":
                            return new CachedDictionary<WarehouseBLL>(
                                warehouseLookup,
                                delegate(WarehouseBLL warehouse)
                                {
                                    return warehouse.WarehouseName;
                                },
                                delegate(object key)
                                {
                                    return WarehouseBLL.GetById((Guid)key);
                                });
                        case "WarehouseManager":
                            return new CachedDictionary<string>(
                                delegate(string warehouseManager)
                                {
                                    return warehouseManager;
                                },
                                delegate(object key)
                                {
                                    //return UserRightBLL.GetUsersWithRight("WHPWM").Find(u => u.UserId == (Guid)key).FullName;
                                    return UserRightBLL.GetUserNameByUserId((Guid)key);
                                });
                        case "NIDType":
                            //ICollection<object> keys = nidTypeLookup.Keys;
                            //return nidTypeLookup;
                            List<NIDTypeBLL> nidTypes = NIDTypeBLL.GetAllNIDTypes();
                            nidTypes.ForEach(nidType => lookup.Add(nidType.Id, nidType.Name));
                            break;
                        case "VerifyingClerk":
                            //return GetWorkersLookup("VerifyingClerk");
                            UserRightBLL.GetUsersWithRight("VerifyingClerk").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "InventoryController":
                            //return GetWorkersLookup("InventoryController");
                            UserRightBLL.GetUsersWithRight("InventoryController").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "Loader":
                            //return GetWorkersLookup("Loader");
                            UserRightBLL.GetUsersWithRight("Loader").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "Weigher":
                            //return GetWorkersLookup("Weigher");
                            UserRightBLL.GetUsersWithRight("Weigher").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "Sampler":
                            //return GetWorkersLookup("Sampler");
                            UserRightBLL.GetUsersWithRight("Sampler").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "Grader":
                            //return GetWorkersLookup("Grader");
                            UserRightBLL.GetUsersWithRight("Grader").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "Gatekeeper":
                            //return GetWorkersLookup("Gatekeeper");
                            UserRightBLL.GetUsersWithRight("Gatekeeper").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "Inspector":
                            //return GetWorkersLookup("Gatekeeper");
                            UserRightBLL.GetUsersWithRight("WHPCT").ForEach(user => lookup.Add(user.UserId, user.FullName));
                            break;
                        case "BagType":
                            foreach (BagTypeBLL bagType in BagTypeBLL.GetAllBagTypes())
                            {
                                lookup.Add(bagType.Id, bagType.BagTypeName);
                            }
                            break;
                        case "BagWeight":
                            foreach (BagTypeBLL bagType in BagTypeBLL.GetAllBagTypes())
                            {
                                lookup.Add(bagType.Id, bagType.Tare.ToString());
                            }
                            break;
                        case "CurrentWarehouse":
                            lookup.Add("Id", WarehouseBLL.CurrentWarehouse.WarehouseId.ToString());
                            lookup.Add("WarehouseCode", WarehouseBLL.CurrentWarehouse.Code);
                            //lookup.Add("WarehouseManagerId", currentWareHouse.WarehouseId.ToString());
                            //lookup.Add("WarehouseManagerName", "");
                            break;
                        case "CurrentUser":
                            lookup.Add("Id", UserBLL.GetCurrentUser().ToString());
                            lookup.Add("Name", UserBLL.GetName(UserBLL.GetCurrentUser()));
                            break;
                        case "WorkerStatus":
                            lookup.Add(0, "Status 1");
                            lookup.Add(1, "Status 2");
                            break;
                        case "EmployeeRole":
                            lookup.Add(WorkType.GINGrading, "0e604921-ee27-4406-97ce-846b47b1cd75");
                            lookup.Add(WorkType.GINLoading, "0e604921-ee27-4406-97ce-846c47b1cd75");
                            lookup.Add(WorkType.GINSampling, "0e604921-ee27-4406-97ce-84db47b1cd75");
                            lookup.Add(WorkType.GINScaling, "0e604921-ee27-4406-97ce-846e47b1cd75");
                            break;
                        case "Shed":
                            foreach (ShedBLL shed in new ShedBLL().GetActiveShedByWarehouseId(WarehouseBLL.CurrentWarehouse.WarehouseId))
                            {
                                lookup.Add(shed.Id, shed.ShedNumber);
                            }
                            break;
                        case "Stack":
                            foreach (Guid shedId in GetLookup("Shed").Keys)
                            {
                                foreach (StackBLL stack in new StackBLL().GetActiveStackbyShedId(shedId))
                                {
                                    lookup.Add(stack.Id, stack.StackNumber);
                                }
                            }
                            break;
                        case "TruckType":
                            lookup.Add(Guid.Empty, "Not Registred");
                            TruckTypeBLL objTT = new TruckTypeBLL();
                            List<TruckTypeBLL> list = objTT.GetActiveTrucksTypes();
                            list.Sort(TruckTypeComp);
                            list.ForEach(tt => lookup.Add(tt.Id, tt.TruckTypeName));
                            break;
                    }
                    lookup.OrderBy(lkup => lkup.Value);
                    return lookup;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Item not found for the {0} lookup.", lookupName), ex);
                }

            }

            private static int TruckTypeComp(TruckTypeBLL ltt, TruckTypeBLL rtt)
            {
                if (ltt == null)
                {
                    if (rtt == null)
                        return 0;
                    else
                        return 1;
                }
                else
                {
                    if (rtt == null)
                        return -1;
                    else
                        return ltt.TruckTypeName.CompareTo(rtt.TruckTypeName);
                }
            }
            public IDictionary<string, object> GetInverseLookup(string lookupName)
            {
                IDictionary<string, object> lookup = new Dictionary<string, object>();
                return lookup;
            }

            #endregion

            private IDictionary<object, string> GetWorkersLookup(string role)
            {
                CachedDictionary<string> workers = new CachedDictionary<string>(
                    delegate(string user)
                    {
                        return user;
                    },
                    delegate(object key)
                    {
                        //return UserRightBLL.GetUsersWithRight(role).Find(u => u.UserId == (Guid)key).FullName;
                        return UserRightBLL.GetUserNameByUserId((Guid)key);
                    },
                    delegate()
                    {
                        return new List<KeyValuePair<object, string>>(
                            from user in UserRightBLL.GetUsersWithRight(role)
                            select new KeyValuePair<object, string>(user.UserId, user.FullName));
                    });
                ICollection<object> keys = workers.Keys;
                return workers;
            }
        }
    }
}
