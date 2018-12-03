using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public class SLABLL
    {
        public CommodityDepositeRequestBLL objArrival = new CommodityDepositeRequestBLL();
        public VoucherInformationBLL objVoucher = new VoucherInformationBLL();
        public DriverInformationBLL objDriver = new DriverInformationBLL();
        public SamplingBLL objSampling = new SamplingBLL();
        public SamplingResultBLL objSamplingResult = new SamplingResultBLL();
        public GradingBLL objGrading = new GradingBLL();
        public GradingResultBLL objGradingResult = new GradingResultBLL();
        public UnloadingBLL objUnloading = new UnloadingBLL();
        public ScalingBLL objScaling = new ScalingBLL();
        public GRNBLL objGRN = new GRNBLL();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WarehouseId">Warehouse Guid</param>
        /// <param name="DateDeposit">Date of deposit to start from</param>
        /// <param name="Flag">to identify if detail dewscription  is needed. 0- no need , 1-fully loaded </param>
        /// <returns>List of SLABLL </returns>
        public List<SLABLL> GetSLA(Guid WarehouseId, DateTime DateDeposit, int Flag)
        {
            List<SLABLL> list = null;
            list = SLADAL.GetSLAByDateDeposit(WarehouseId, DateDeposit);
            return list;
        }
    }
}
