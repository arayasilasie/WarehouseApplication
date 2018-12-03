using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public class CommodityGradeFactorValueBLL : GeneralBLL
    {
        #region fields
        private Guid _Id;
        private Guid _CommodityGradeId;
        private Nullable<float> _MaxValue = null;
        private Nullable<float> _MinValue = null;
        private int _Status;
        #endregion
        #region Properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public Guid CommodityGradeId
        {
            get { return _CommodityGradeId; }
            set { _CommodityGradeId = value; }
        }
        public Nullable<float> MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }
        public Nullable<float> MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        #endregion

        public CommodityGradeFactorValueBLL GetActiveValueByCommodoityGradeId(Guid CommodityGradeId)
        {
            CommodityGradeFactorValueBLL obj = null;
            obj = CommodityGradeFactorValueDAL.GetActiveValueByGradeId(CommodityGradeId);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                return null;
            }
           

       
            
        }

    }
}
