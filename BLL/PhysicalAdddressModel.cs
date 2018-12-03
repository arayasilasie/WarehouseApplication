using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECX.DataAccess;
using GINBussiness;
using System.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication.BLL
{
    public class PhysicalAdddressModel:WarehouseBaseModel
    {

          #region Properties and Methods

        public Guid ID{get;set;}
        public Guid ShedID{get;set;}
        public string AddressName{get;set;}
        public string Row{get;set;}
        public string Columun {get;set;}
        public float Width {get;set;} 
        public float Length{get;set;}
        public float Height{get;set;}
        public float MaximumSize{get;set;}       
        public Guid CreatedBy{get;set;}
        public DateTime CreatedTimestamp{get;set;}   
        public Guid LastModifiedBy{get;set;}
        public DateTime LastModifiedTimestamp{get;set;}
        public int Status { get; set; }

        #endregion

        public PhysicalAdddressModel()
        {
        }
        public object InsertPhysicalAdddress()
        {
            return SQLHelper.SaveAndReturn(ConnectionString, "AddPhysicalAddress", this);
        }

        public object UpdatePhysicalAddress()
        {
            return SQLHelper.SaveAndReturn(ConnectionString, "UpdatePhysicalAddress",this);
        }

        public static void CancelPhysicalAddress(Guid ID, Guid LastModifiedBy, DateTime LastModifiedTimestamp)
        {
            SQLHelper.execNonQuery(ConnectionString, "CancelPhysicalAddress", ID,LastModifiedBy,LastModifiedTimestamp);
        }

        public static DataTable GetPhysicalAddresForEdit(Guid ShedID,string AddressName)
        {
            return SQLHelper.getDataTable(ConnectionString, "GetPhysicalAddresForEdit", ShedID,AddressName);
        }
    }
}