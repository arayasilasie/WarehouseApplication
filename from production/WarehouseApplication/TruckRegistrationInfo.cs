using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;
using WarehouseApplication.BLL;
using AjaxControlToolkit;

namespace WarehouseApplication
{
    [Serializable] 
    public class TruckRegistrationInfo
    {
        private GINTruckInfo ginTruck;

        public GINTruckInfo GINTruck {
            get
            {
                ginTruck.MainTruckId = Truck.TruckId;
                ginTruck.PlateNo = Truck.PlateNo;
                ginTruck.TrailerId = Trailer.TruckId;
                ginTruck.TrailerNo = Trailer.PlateNo;
                return ginTruck;
            }
            set
            {
                ginTruck = value;
                if (ginTruck.MainTruckId != Guid.Empty)
                {
                    TruckRegisterBLL truckRegister = null;
                    TruckModelYearBLL truckModelYear = null;
                    TruckModelBLL truckModel = null;
                    truckRegister = new TruckRegisterBLL().GetTruckInfoByTruckNumber(ginTruck.PlateNo, false);
                    if (truckRegister != null)
                    {
                        truckModelYear = new TruckModelYearBLL().GetbyId(truckRegister.TruckModelYearId);
                        if (truckModelYear != null)
                        {
                            truckModel = new TruckModelBLL().GetbyId(truckModelYear.TruckModelId);
                        }
                    }
                    Truck = null;
                    if (truckRegister != null)
                    {
                        Truck = new TruckInfo()
                        {
                            PlateNo = truckRegister.TruckNumber,
                            TruckId = truckRegister.Id,
                            TruckModelYearId = truckRegister.TruckModelYearId,
                            TruckModelId = truckModelYear.TruckModelId,
                            TruckTypeId = truckModel.TruckTypeId,
                            IsNew = (ginTruck.PlateNo.Trim() == string.Empty) ||
                                (truckRegister == null)
                        };
                    }
                    else
                    {
                        Truck = new TruckInfo()
                        {
                            PlateNo = ginTruck.PlateNo,
                            TruckId = ginTruck.MainTruckId,
                            IsNew = true
                        };
                    }
                }
                if (ginTruck.TrailerId != Guid.Empty)
                {
                    TruckRegisterBLL truckRegister = null;
                    TruckModelYearBLL truckModelYear = null;
                    TruckModelBLL truckModel = null;
                    truckRegister = new TruckRegisterBLL().GetTruckInfoByTruckNumber(ginTruck.PlateNo, true);
                    if (truckRegister != null)
                    {
                        truckModelYear = new TruckModelYearBLL().GetbyId(truckRegister.TruckModelYearId);
                        if (truckModelYear != null)
                        {
                            truckModel = new TruckModelBLL().GetbyId(truckModelYear.TruckModelId);
                        }
                    }
                    Trailer = null;
                    if (truckRegister != null)
                    {
                        Trailer = new TruckInfo()
                            {
                                PlateNo = truckRegister.TruckNumber,
                                TruckId = truckRegister.Id,
                                TruckModelYearId = truckRegister.TruckModelYearId,
                                TruckModelId = truckModelYear.TruckModelId,
                                TruckTypeId = truckModel.TruckTypeId,
                                IsNew = (ginTruck.TrailerNo.Trim() == string.Empty) ||
                                    (truckRegister == null)
                            };
                    }
                    else
                    {
                        Trailer = new TruckInfo()
                        {
                            PlateNo = ginTruck.TrailerNo,
                            TruckId = ginTruck.TrailerId,
                            IsNew = true
                        };
                    }
                }
            }
        }
        public string DriverName
        {
            get
            {
                return GINTruck.DriverName;
            }
            set
            {
                GINTruck.DriverName = value;
            }
        }
        public string LicenseNo
        {
            get
            {
                return GINTruck.LicenseNo;
            }
            set
            {
                GINTruck.LicenseNo = value;
            }
        }
        public string IssuedBy
        {
            get
            {
                return GINTruck.IssuedBy;
            }
            set
            {
                GINTruck.IssuedBy = value;
            }
        }
        public Guid MainTruckId
        {
            get
            {
                return GINTruck.MainTruckId;
            }
            set
            {
                GINTruck.MainTruckId = value;
            }
        }
        public Guid TrailerId
        {
            get
            {
                return GINTruck.TrailerId;
            }
            set
            {
                GINTruck.TrailerId = value;
            }
        }
        public string PlateNo
        {
            get
            {
                return GINTruck.PlateNo;
            }
            set
            {
                GINTruck.PlateNo = value;
            }
        }
        public string TrailerNo
        {
            get
            {
                return GINTruck.TrailerNo;
            }
            set
            {
                GINTruck.TrailerNo = value;
            }
        }

        public TruckInfo Truck { get; set; }
        public TruckInfo Trailer { get; set; }
        public TruckHasTrailerType TruckHasTrailer
        {
            get
            {
                TruckHasTrailerType hasTrailer = TruckHasTrailerType.TruckTrailer;
                if (PlateNo == string.Empty)
                {
                    hasTrailer = TruckHasTrailerType.TrailerOnly;
                }
                if (TrailerNo == string.Empty)
                {
                    hasTrailer = TruckHasTrailerType.TruckOnly;
                }
                return hasTrailer;
            }
        }

        public bool TruckWeightRegistered
        {
            get
            {
                TruckWeight truckWeight = TruckWeight.GetLatestActiveWeight(PlateNo, TrailerNo);
                return truckWeight!= null;
            }
        }

        public TruckWeight LatestActiveWeight
        {
            get
            {
                return TruckWeight.GetLatestActiveWeight(PlateNo, TrailerNo);
                //return new TruckWeight()
                //{
                //    hasTrailer = TruckHasTrailerType.TruckTrailer,
                //    Id = new Guid("cb5349d0-71ef-4648-8796-a28f0feacf9d"),
                //    Status = TruckWeightStatus.Active,
                //    TrackingNo = string.Empty,
                //    TruckPlateNo = "12345",
                //    TrailerPlateNo = "00012",
                //    Weight = 20000,
                //    DateWeighed = DateTime.Parse("5/18/2010 11:39:58 AM")
                //};
            }
        }
        public TruckWeight TruckWeight
        {
            get
            {
                TruckWeight truckWeight = LatestActiveWeight;
                if (truckWeight == null)
                {
                    truckWeight = new TruckWeight()
                    {
                        hasTrailer = TruckHasTrailer,
                        Id = Guid.NewGuid(),
                        Status = TruckWeightStatus.Active,
                        TrackingNo = string.Empty,
                        TruckPlateNo = PlateNo,
                        TrailerPlateNo = TrailerNo,
                        DateWeighed = ginTruck.Weight.DateWeighed
                    };
                }
                truckWeight.Weight = Convert.ToSingle(ginTruck.Weight.TruckWeight);
                return truckWeight;
            }
        }

        public bool IsSuspecious
        {
            get
            {
                TruckWeight truckWeight = LatestActiveWeight;
                if (truckWeight != null)
                {
                    return Math.Abs(truckWeight.Weight - Convert.ToSingle(GINTruck.Weight.TruckWeight)) >= 1;
                }
                return false;

            }
        }
    }

    [Serializable]
    public class TruckInfo
    {
        public Guid TruckId { get; set; }
        public String PlateNo { get; set; }
        public Guid TruckTypeId { get; set; }
        public Guid TruckModelId { get; set; }
        public Guid TruckModelYearId { get; set; }
        public bool IsNew { get; set; }
    }

    public abstract class TruckRegistrationResponse : SubscriberResponse
    {
        protected void FindTruckRegistration(string truckNo, bool isTrailer)
        {
            ((TruckInfo)SubscriberData).TruckTypeId = Guid.Empty;
            ((TruckInfo)SubscriberData).TruckModelId = Guid.Empty;
            ((TruckInfo)SubscriberData).TruckModelYearId = Guid.Empty;
            ((TruckInfo)SubscriberData).IsNew = false;

            TruckRegisterBLL obj = null;
            if (truckNo.Trim() != string.Empty)
            {
                obj = new TruckRegisterBLL().GetTruckInfoByTruckNumber(truckNo, isTrailer);
            }
            if (obj != null)
            {
                TruckModelYearBLL objTMY = new TruckModelYearBLL();
                objTMY = objTMY.GetbyId(obj.TruckModelYearId);
                if (objTMY != null)
                {
                    ((TruckInfo)SubscriberData).TruckModelYearId = objTMY.Id;
                    TruckModelBLL objTM = new TruckModelBLL();
                    objTM = objTM.GetbyId(objTMY.TruckModelId);
                    if (objTM != null)
                    {
                        ((TruckInfo)SubscriberData).TruckModelId = objTM.Id;
                        TruckTypeBLL objTT = new TruckTypeBLL();
                        objTT = objTT.GetbyId(objTM.TruckTypeId);
                        if (objTT != null)
                        {
                            ((TruckInfo)SubscriberData).TruckTypeId = objTT.Id;
                        }
                    }
                }
            }
            else
            {
                ((TruckInfo)SubscriberData).IsNew = true;
            }
            foreach (Control subscriber in Subscribers)
            {
                if (subscriber == null) continue;
                switch (subscriber.ID)
                {
                    case "cddExtender_TruckTypeId":
                        ((CascadingDropDown)subscriber).SelectedValue = (((TruckInfo)SubscriberData).TruckTypeId.ToString());
                        break;
                    case "cddExtender_TruckModelId":
                        ((CascadingDropDown)subscriber).SelectedValue = (((TruckInfo)SubscriberData).TruckModelId.ToString());
                        break;
                    case "cddExtender_TruckModelYearId":
                        ((CascadingDropDown)subscriber).SelectedValue = (((TruckInfo)SubscriberData).TruckModelYearId.ToString());
                        break;
                }
            }

        }
    }

    public class TruckResponse : TruckRegistrationResponse
    {
        public override void Respond()
        {
            FindTruckRegistration(((TextBox)Publisher).Text.Trim(), false);
        }
    }

    public class TrailerResponse : TruckRegistrationResponse
    {
        public override void Respond()
        {
            FindTruckRegistration(((TextBox)Publisher).Text.Trim(), true);
        }
    }

}
