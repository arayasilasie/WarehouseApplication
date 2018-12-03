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

namespace WarehouseApplication.BLL
{
    public class ClientBLL
    {
        private Guid _ClientUniqueIdentifier;
        private string _ClientName ;
        private string _ClientId ;
        #region Properties
        
        
        public Guid ClientUniqueIdentifier
        {
            get
            {
                return this._ClientUniqueIdentifier;
            }
            set
            {
                this._ClientUniqueIdentifier = value;
            }
        }
        public string ClientName
        {
            get
            {
                return this._ClientName;
            }
            set
            {
                this._ClientName = value;
            }
        }
        public string ClientId
        {
            get
            {
                return this._ClientId ;
            }
            set
            {
                this._ClientId = value;
            }
        }
        #endregion
        public ClientBLL()
        {
        }
        public ClientBLL(Guid UI , string CId ,string CName    )
        {
            this.ClientUniqueIdentifier = UI;
            this.ClientName = CName;
            this.ClientId = CId;
        }
        public static ClientBLL GetClinet(Guid Id)
        {
            try
            {
                ClientBLL objClient = new ClientBLL();
                Membership.Client objWebServiceClient;
                Membership.MemberShipLookUp objMembership = new WarehouseApplication.Membership.MemberShipLookUp();
                objWebServiceClient = objMembership.GetClient(Id);
                if (objWebServiceClient != null)
                {
                    objClient.ClientUniqueIdentifier = objWebServiceClient.ClientId;
                    objClient.ClientName = objWebServiceClient.Name;
                    objClient.ClientId = objWebServiceClient.IdNo;
                    return objClient;
                }
                else
                {
                    Membership.MembershipEntities me = new WarehouseApplication.Membership.MembershipEntities();
                    me = objMembership.GetEntityByGuid(Id);
                    if (me != null)
                    {
                        objClient.ClientUniqueIdentifier = Id;
                        objClient.ClientName = me.OrganizationName.ToString();
                        objClient.ClientId = me.StringIdNo;
                        return objClient;

                        
                    }
                    else
                    {
                        return null;
                    }
                    
                }
            }
            catch( Exception ex)
            {
                throw new Exception("Can't get Client from service please trya again.", ex);
            }
        }
        public static ClientBLL GetClinet(string IdNo)
        {
            //Todo Ask sisay
            Membership.MembershipEntities objentity;
            try
            {
                Membership.MemberShipLookUp objMembership = new WarehouseApplication.Membership.MemberShipLookUp();
                objentity = objMembership.GetEntityByIdNo(IdNo);
                if (objentity != null)
                {
                    string status = objentity.Status;
                    /// Peabean client problem - Nov 2 2010
                    //if (status == "Active")
                    //{
                        //ClientBLL obj = new ClientBLL(objentity.UniqueIdentifier, IdNo, objentity.OrganizationName);
                        //return obj;
                    //}
                    //else
                   // {
                   //     return null;
                   // }


                    if (status == "Terminated")
                    {

                        return null;
                    }
                    else
                    {
                        ClientBLL objC = new ClientBLL(objentity.UniqueIdentifier, IdNo, objentity.OrganizationName);
                        return objC;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Get membership Service", ex);
            }

          
            
        }
        public static List<ClientBLL> GetAllClient()
        {
            try
            {
                List<ClientBLL> lstClient = new List<ClientBLL>();
                Membership.MemberShipLookUp objMembership = new WarehouseApplication.Membership.MemberShipLookUp();
                Membership.Client[] listClient;
                listClient = objMembership.GetClients();
                foreach (Membership.Client c in listClient)
                {
                    ClientBLL objMyClient = new ClientBLL();
                    objMyClient.ClientId = c.IdNo;
                    objMyClient.ClientUniqueIdentifier = c.ClientId;
                    objMyClient.ClientName = c.Name;
                    lstClient.Add(objMyClient);
                }
                return lstClient;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not get Client information", ex);
            }
        }
        public static string GetClinetNameById(Guid Id)
        {
            try
            {
                ClientBLL objClient = new ClientBLL();
                Membership.Client objWebServiceClient;
                Membership.MemberShipLookUp objMembership = new WarehouseApplication.Membership.MemberShipLookUp();
                objWebServiceClient = objMembership.GetClient(Id);
                if (objWebServiceClient != null)
                {

                    return objWebServiceClient.Name + " - " + objWebServiceClient.IdNo + "";
                }
                else
                {
                    Membership.MembershipEntities me = new WarehouseApplication.Membership.MembershipEntities();
                    me = objMembership.GetEntityByGuid(Id);
                    if (me != null)
                    {
                        return me.OrganizationName + " - " + me.StringIdNo;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can't get Client from service please trya again.", ex);
            }
        }

        
    }
}
