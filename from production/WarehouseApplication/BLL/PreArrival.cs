using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;

namespace WarehouseApplication.BLL
{
    public class PreArrival
    {
        public Guid ArrivalId { get; set; }

        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string VoucherNumber { get; set; }
        public string TruckPlateNumber { get; set; }
        public string TrailerPlateNumber { get; set; }
        public string CodeType { get; set; }
        public bool HasVoucher { get; set; }
        public Guid WarehouseID { get; set; }
        public int WorkflowTypeID { get; set; }
        public Guid UserID { get; set; }
        public string CompleteCodeToReturn { get; set; }
        public bool IsTruckInCompound { get; set; }

        private string ConnectionString = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ToString();

        public string Save()
        {
            this.ArrivalId = Guid.NewGuid();


            System.Data.DataRow dr = ECX.DataAccess.SQLHelper.SaveAndReturnRow(ConnectionString, "[ArrivalSavePre]", this);
            return dr[0].ToString();
        }

        public static List<PreArrival> GetArrivalList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            ECX.DataAccess.SQLHelper.PopulateTable("", dt, "");
            List<PreArrival> lst = new List<PreArrival>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                PreArrival a = new PreArrival();
                ECX.DataAccess.Common.DataRow2Object(dr, a);
                lst.Add(a);
            }
            return lst;
        }

        /// <summary>
        /// a function that populates the pre-arrival information from tblArrivals table
        /// </summary>
        /// <param name="ArrivalId"></param>
        /// <returns></returns>
        //public DataTable PopulatePreArrival(Guid arrivalId)
        //{
        //    try
        //    {
        //        System.Data.DataTable dt = ECX.DataAccess.SQLHelper.getTable(ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ToString(), "Get_PreArrivalInfo", arrivalId);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

        public DataTable PopulatePreArrival(Guid arrivalId)
        {
            DataTable dt;
            try
            {
                dt = ECX.DataAccess.SQLHelper.getDataTable(ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ToString(), "Get_PreArrivalInfo", arrivalId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }

    }
}
