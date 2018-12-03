using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using WarehouseApplication.DAL;
using System.Data;
using System.Data.SqlClient;


namespace WarehouseApplication.BLL
{
    public enum  TruckStatus { Active = 1, Cancelled };
    public class TruckTypeBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public string TruckTypeName { get; set; }
        public TruckStatus Status { get; set; }

        public bool Add()
        {
            return true;
        }
        public bool Update()
        {
            return true;
        }
        public List<TruckTypeBLL> GetActiveTrucksTypes()
        {
            List<TruckTypeBLL> list = null;
            List<TruckTypeBLL> listActive = null;
            try
            {
                
                list = TruckTypeDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Status == TruckStatus.Active
                                  select c).ToList<TruckTypeBLL>();
                    return listActive;
                }
            }
            catch ( Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<TruckTypeBLL> GetAllTrucksTypes()
        {
            List<TruckTypeBLL> list = null;
            try
            {
                
                list = TruckTypeDAL.GetAll();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
        public TruckTypeBLL GetbyId(Guid Id)
        {
            List<TruckTypeBLL> list = null;
            try
            {
                TruckTypeBLL listActive = null;
                list = TruckTypeDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Id == Id
                                  select c).Single<TruckTypeBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
    public class TruckModelBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid TruckTypeId { get; set; }
        public String TruckModelName { get; set; }
        public TruckStatus Status { get; set; }
        public List<TruckModelBLL> GetActiveTrucks()
        {
            List<TruckModelBLL> list = null;
            try
            {
                List<TruckModelBLL> listActive = null;
                list = TruckModelDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Status == TruckStatus.Active
                                  select c).ToList<TruckModelBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<TruckModelBLL> GetActiveTrucksByTypeId(Guid Id)
        {
            List<TruckModelBLL> list = null;
            try
            {
                List<TruckModelBLL> listActive = null;
                list = TruckModelDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Status == TruckStatus.Active && c.TruckTypeId == Id
                                  select c).ToList<TruckModelBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<TruckModelBLL> GetAllTrucksByTypeId(Guid Id)
        {
            List<TruckModelBLL> list = null;
            try
            {
                List<TruckModelBLL> listActive = null;
                list = TruckModelDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.TruckTypeId == Id
                                  select c).ToList<TruckModelBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<TruckModelBLL> GetAllTrucks()
        {
            List<TruckModelBLL> list = null;
            try
            {

                list = TruckModelDAL.GetAll();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public TruckModelBLL GetbyId(Guid Id)
        {
            List<TruckModelBLL> list = null;
            try
            {
                TruckModelBLL listActive = null;
                list = TruckModelDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Id == Id
                                  select c).Single<TruckModelBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
    public class TruckModelYearBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid TruckModelId { get; set; }
        public String ModelYearName { get; set; }
        public TruckStatus Status { get; set; }
        public float ModelWeight { get; set; }
        public List<TruckModelYearBLL> GetActiveTruckModelYearByModelId(Guid Id)
        {
            List<TruckModelYearBLL> list = null;
            try
            {
                List<TruckModelYearBLL> listActive = null;
                list = TruckModelYearDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Status == TruckStatus.Active
                                  select c).ToList<TruckModelYearBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<TruckModelYearBLL> GetActiveTrucksByModelId(Guid Id)
        {
            List<TruckModelYearBLL> list = null;
            try
            {
                List<TruckModelYearBLL> listActive = null;
                list = TruckModelYearDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.Status == TruckStatus.Active && c.TruckModelId == Id
                                  select c).ToList<TruckModelYearBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<TruckModelYearBLL> GetAllTrucksByModelId(Guid Id)
        {
            List<TruckModelYearBLL> list = null;
            try
            {
                List<TruckModelYearBLL> listActive = null;
                list = TruckModelYearDAL.GetAll();
                if (list != null)
                {
                    listActive = (from c in list
                                  where c.TruckModelId == Id
                                  select c).ToList<TruckModelYearBLL>();
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public TruckModelYearBLL GetbyId(Guid Id)
        {
            List<TruckModelYearBLL> list = null;
            try
            {
                TruckModelYearBLL listActive = null;
                list = TruckModelYearDAL.GetAll();
                if (list != null)
                {
                    try
                    {
                        listActive = (from c in list
                                      where c.Id == Id
                                      select c).Single<TruckModelYearBLL>();
                    }
                    catch
                    {
                        return null;
                    }
                    return listActive;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


    }

    public enum TruckWeightStatus { Active = 1, Cancelled };
    public enum TruckHasTrailerType { TruckOnly = 1, TruckTrailer,TrailerOnly };
    public class TruckWeight : GeneralBLL
    {
        public Guid Id { get ; set ; }
        public String TruckPlateNo { get; set; }
        public String TrailerPlateNo { get; set; }
        public DateTime DateWeighed { get; set; }
        public TruckHasTrailerType hasTrailer { get; set; }
        public float Weight { get; set; }
        public TruckWeightStatus Status { get; set; }


        public  bool Save(SqlTransaction tran)
        {

            bool isSaved = false;
            this.CreatedBy = UserBLL.GetCurrentUser();
            if ((string.IsNullOrEmpty(this.TruckPlateNo) != true) && (string.IsNullOrEmpty(this.TrailerPlateNo) == true))
            {
                this.hasTrailer = TruckHasTrailerType.TruckOnly;
            }
            else if ((string.IsNullOrEmpty(this.TruckPlateNo) == true) && (string.IsNullOrEmpty(this.TrailerPlateNo) != true))
            {
                this.hasTrailer = TruckHasTrailerType.TrailerOnly;
            }
            else if ((string.IsNullOrEmpty(this.TruckPlateNo) != true) && (string.IsNullOrEmpty(this.TrailerPlateNo) != true))
            {
                this.hasTrailer = TruckHasTrailerType.TruckTrailer;
            }
            else if ((string.IsNullOrEmpty(this.TruckPlateNo) == true) && (string.IsNullOrEmpty(this.TrailerPlateNo) == true))
            {
                throw new Exception("Truck Plate No. And Trailer Plate No. Are Both Empty");
            }
            this.Status = TruckWeightStatus.Active;
            isSaved = TruckWeightDAL.Insert(this, tran);
            AuditTrailBLL objAt = new AuditTrailBLL();
            int at = -1;
            if (isSaved == true)
            {
                at = objAt.saveAuditTrail(this, WFStepsName.RegisterTruckWeight.ToString(), UserBLL.GetCurrentUser(), "Scaling Information");
                if (at == 1)
                {
                    isSaved = true;
                }
                else
                {
                    isSaved = false;
                }
            }
            return isSaved;
        }

        public bool SaveNew()
        {
            bool isSaved = false;
            this.CreatedBy = UserBLL.GetCurrentUser();
            if ((string.IsNullOrEmpty(this.TruckPlateNo) != true) && (string.IsNullOrEmpty(this.TrailerPlateNo) == true))
            {
                this.hasTrailer = TruckHasTrailerType.TruckOnly;
            }
            else if ((string.IsNullOrEmpty(this.TruckPlateNo) == true) && (string.IsNullOrEmpty(this.TrailerPlateNo) != true))
            {
                this.hasTrailer = TruckHasTrailerType.TrailerOnly;
            }
            else if ((string.IsNullOrEmpty(this.TruckPlateNo) != true) && (string.IsNullOrEmpty(this.TrailerPlateNo) != true))
            {
                this.hasTrailer = TruckHasTrailerType.TruckTrailer;
            }
            else if ((string.IsNullOrEmpty(this.TruckPlateNo) == true) && (string.IsNullOrEmpty(this.TrailerPlateNo) == true))
            {
                throw new Exception("Truck Plate No. And Trailer Plate No. Are Both Empty");
            }
            this.Status = TruckWeightStatus.Active;

            //THE FOLLOWING METHOD ADDED BY SINISHAW
            isSaved = TruckWeightDAL.InsertNew(this);

            AuditTrailBLL objAt = new AuditTrailBLL();
            int at = -1;
            if (isSaved == true)
            {
                at = objAt.saveAuditTrail(this, WFStepsName.RegisterTruckWeight.ToString(), UserBLL.GetCurrentUser(), "Scaling Information");
                if (at == 1)
                    isSaved = true;
                else
                    isSaved = false;
            }
            return isSaved;
        }

        public  bool Update(SqlTransaction tran)
        {
            bool isSaved = false;
            isSaved = TruckWeightDAL.Update(this, tran);
            return isSaved;
        }
        public static List<TruckWeight> GetAllActiveTruckWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            List<TruckWeight> list = null;
            list = TruckWeightDAL.GetAllActiveTruckWeight(TruckPlateNo, TrailerPlateNo);
            return list;
        }
        public void  GetLatestActiveTruckWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            TruckWeight obj = null;
            obj = TruckWeightDAL.GetLatestActiveTruckWeight(TruckPlateNo, TrailerPlateNo);
            if (obj != null)
            {
                this.Id = obj.Id;
                this.TruckPlateNo = obj.TruckPlateNo;
                this.TrailerPlateNo = obj.TrailerPlateNo;
                this.hasTrailer = obj.hasTrailer;
                this.DateWeighed = obj.DateWeighed;
                this.Weight = obj.Weight;
                this.Status = obj.Status;
            }
        }
        public static TruckWeight GetLatestActiveWeight(string TruckPlateNo, string TrailerPlateNo)
        {
            TruckWeight obj = null;
            //Truck only
            if ((string.IsNullOrEmpty(TruckPlateNo) != true) && (string.IsNullOrEmpty(TrailerPlateNo) == true))
            {
                obj = TruckWeightDAL.GetLatestActiveTruckWeight(TruckPlateNo, TrailerPlateNo);
            }
            //Trailer only
            else if ((string.IsNullOrEmpty(TruckPlateNo) == true) && (string.IsNullOrEmpty(TrailerPlateNo) != true))
            {
                obj = TruckWeightDAL.GetLatestActiveTrailerWeight(TruckPlateNo, TrailerPlateNo);
            }
            else if ((string.IsNullOrEmpty(TruckPlateNo) != true) && (string.IsNullOrEmpty(TrailerPlateNo) != true))
            {
                obj = TruckWeightDAL.GetLatestActivetTruckTrailerWeight(TruckPlateNo, TrailerPlateNo);

            }
            else
            {
                throw new Exception("Truck type is not determinded");
            }
            return obj;
        }

    }
    public class TruckRegisterBLL : GeneralBLL
    {
        public Guid Id { get; set; }
        public Guid TruckModelYearId { get; set; }
        public string TruckNumber { get; set; }
        public bool IsTrailer { get; set; }
        public TruckStatus Status { get; set; }

        public TruckRegisterBLL GetTruckInfoByTruckNumber(string TruckNo , bool isTrailer )
        {
            return TruckRegisterDAL.GetByTruckNumber(TruckNo, isTrailer);
        }

        public bool Add()
        {
            SqlTransaction tran = null;
            SqlConnection conn = null;
            bool isSaved = false;
            try
            {
                conn = Connection.getConnection();
                tran = conn.BeginTransaction();
                this.Id = Guid.NewGuid();
                this.CreatedBy = UserBLL.GetCurrentUser();
                isSaved = TruckRegisterDAL.Save(this, tran);
                tran.Commit();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                    tran.Dispose();
                }
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

            return isSaved;
        }
         
        public bool Add(SqlTransaction tran)
        {
          
            bool isSaved = false;
            try
            {
               
                this.Id = Guid.NewGuid();
                this.CreatedBy = UserBLL.GetCurrentUser();
                isSaved = TruckRegisterDAL.Save(this, tran);
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            

            return isSaved;
        }

    }
}
