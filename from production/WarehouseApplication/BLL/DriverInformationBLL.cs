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
using WarehouseApplication.DAL;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;


namespace WarehouseApplication.BLL
{
    public class DriverInformationBLL
    {
       private Guid _id;
       private Guid _receivigRequestId;
       private string _driverName;
       private string _licenseNumber;
       private string _licenseIssuedPlace;
       private string _plateNumber;
       private string _trailerPlateNumber;
       private int _status;
       private string _remark;
       private Guid  _createdBy;
       private DateTime _createdDate;
       private Guid _lastModifiedBy;
       private DateTime _lastModifiedDate;

        #region Propperties
       public Guid Id
       {
           get
           {
               return _id;
           }
           set
           {
               this._id = value;
           }
       }
       public Guid ReceivigRequestId
       {
           get
           {
               return _receivigRequestId;
           }
           set
           {
               this._receivigRequestId = value;
           }
       }
       public string  DriverName
       {
           get
           {
               return _driverName;
           }
           set
           {
               this._driverName = value;
           }
       }
       public string LicenseNumber
       {
           get
           {
               return _licenseNumber;
           }
           set
           {
               this._licenseNumber = value;
           }
       }
       public string  LicenseIssuedPlace
       {
           get
           {
               return _licenseIssuedPlace;
           }
           set
           {
               this._licenseIssuedPlace = value;
           }
       }
       public string PlateNumber
       {
           get
           {
               return _plateNumber;
           }
           set
           {
               this._plateNumber = value;
           }
       }
       public string TrailerPlateNumber
       {
           get
           {
               return _trailerPlateNumber;
           }
           set
           {
               this._trailerPlateNumber = value;
           }
       }
       public int Status
       {
           get
           {
               return this._status;
           }
           set
           {
               this._status = value;
           }
       }
       public string Remark
       {
           get
           {
               return this._remark;
           }
           set
           {
               this._remark = value;
           }
       }
       public Guid CreatedBy 
       { 
           get
           {
               return this._createdBy;
           }
           set
           {
               this._createdBy = value;
           }
       }
       public DateTime CreatedDate
       {
           get
           {
               return this._createdDate;
           }
           
       }
       public Guid LastModifiedBy
       {
           get
           {
               return this._lastModifiedBy;
           }
           set
           {
               this._lastModifiedBy = value;
           }
       }
       public DateTime LastModifiedDate
       {
           get
           {
               return this._lastModifiedDate;
           }
           
       }
        #endregion
        #region Constructors
       public DriverInformationBLL()
       {
       }
       public DriverInformationBLL(Guid receivigRequestId, string driverName, string licenseNumber, string licenseIssuedPlace, string plateNumber,
           string trailerPlateNumber, int status, string remark, Guid createdby)
       {
           this.ReceivigRequestId = receivigRequestId;
           this.DriverName = driverName;
           this.LicenseNumber = licenseNumber;
           this.LicenseIssuedPlace = licenseIssuedPlace;
           this.PlateNumber = plateNumber;
           this.TrailerPlateNumber = trailerPlateNumber;
           this.Status = status;
           this.Remark = remark;
           this.CreatedBy = createdby;
       }
       public DriverInformationBLL(Guid receivigRequestId, string driverName, string licenseNumber, string licenseIssuedPlace, string plateNumber,
   string trailerPlateNumber, int status, string remark, Guid createdby, Guid lastModifiedBy )
       {
           this.ReceivigRequestId = receivigRequestId;
           this.DriverName = driverName;
           this.LicenseNumber = licenseNumber;
           this.LicenseIssuedPlace = licenseIssuedPlace;
           this.PlateNumber = plateNumber;
           this.TrailerPlateNumber = trailerPlateNumber;
           this.Status = status;
           this.Remark = remark;
           this.CreatedBy = createdby;
           this.LastModifiedBy = lastModifiedBy;

       }
        #endregion
        /// <summary>
        /// Validates whether the Data fields are in proper format
        /// </summary>
        /// <param name="objDriverInfoBll"></param>
        /// <returns>True when all check are okay</returns>
       public bool isValidForSave (DriverInformationBLL objDriverInfoBll )
       {
           if (objDriverInfoBll.ReceivigRequestId == null)
           {
               
               return false;
           }
           //if (objDriverInfoBll.DriverName == "" || objDriverInfoBll.DriverName.Length > 50 )
           //{
               
           //    return false;
           //}
           //if (objDriverInfoBll.LicenseNumber == "" || objDriverInfoBll.LicenseNumber.Length > 50)
           //{
               
           //    return false;
           //}
           //if (objDriverInfoBll.LicenseIssuedPlace == "" || objDriverInfoBll.LicenseIssuedPlace.Length > 50)
           //{
               
           //    return false;
           //}
           //if (objDriverInfoBll.PlateNumber == "" || objDriverInfoBll.PlateNumber.Length > 50)
           //{
               
           //    return false;
           //}


           //if (objDriverInfoBll.CreatedBy  == null)
           //{
           //    throw new Exception("Invalid Created By");
               
           //}
           return true;
       }
        /// <summary>
        /// Checks if the Object can be Updated, in that all required Data are in a correct format 
        /// </summary>
        /// <param name="objDriverInfoBll"></param>
        /// <returns>True if all checks are passed otherwise false</returns>
       public bool isValidForEdit(DriverInformationBLL objDriverInfoBll)
       {
           bool isValid = false;
           isValid = this.isValidForSave(objDriverInfoBll);
           if (isValid == true)
           {
               if (objDriverInfoBll.LastModifiedBy == null)
               {

                   return false;
               }
               else
               {
                   isValid = true;
               }
           }
           return isValid;
       }

       public bool SaveDriverInformation()
       {
           bool isValid,isDuplicate = false;
           bool isSaved = false ;
           AuditTrailBLL objAt = new AuditTrailBLL();
           DriverInformationBLL objDriverInfoBLL = new DriverInformationBLL(this.ReceivigRequestId, this.DriverName,
               this.LicenseNumber, this.LicenseIssuedPlace, this.PlateNumber, this.TrailerPlateNumber, this.Status, this.Remark, this.CreatedBy);
            //TODO : Check status is not cancelled
           //isDuplicate = this.isDuplicate(this.ReceivigRequestId, this.LicenseIssuedPlace, this.LicenseIssuedPlace);
           //if (isDuplicate == true)
           //{
               //throw new DuplicateDriverInformationException("This driver information has already been added");
           //}
           isValid = this.isValidForSave(objDriverInfoBLL);
           if (isValid == true)
           {
               SqlTransaction tran;
               SqlConnection conn = Connection.getConnection();
               tran = conn.BeginTransaction();
               try
               {
                   
                   
                   DriverInformation objDriverInfoDAL = new DriverInformation();
                   Guid DriverInformationId = Guid.Empty;

                   DriverInformationId = objDriverInfoDAL.InsertDriverInformation(objDriverInfoBLL, tran);
                   if (DriverInformationId == Guid.Empty)
                   {
                       tran.Rollback();
                       isSaved = false;
                   }
                   else
                   {
                       objDriverInfoBLL.Id = DriverInformationId ;
                     
                       if (objAt.saveAuditTrail(objDriverInfoBLL, WFStepsName.AddDriverInformation.ToString(),UserBLL.GetCurrentUser(), "Add New Driver Information") == -1)
                       {
                           tran.Rollback();
                           isSaved = false;
                       }
                       else
                       {
                           tran.Commit();
                           isSaved = true;
                       }
                   }
                   conn.Close();
               }
               catch(Exception e )
               {
                   tran.Rollback();
                   objAt.RoleBack();
                   throw e;
               }
               finally
               {
                   tran.Dispose();
                   conn.Close();
               }
           }
           else
           {
               return false ;
           }
           return isSaved;
       }

       public bool EditDriverInformation( DriverInformationBLL objEdit)
       {
           //check if it has a GRN not in Edit mood.

           if (isGRNEditable(this.ReceivigRequestId) == false)
           {
               throw new GRNNotOnUpdateStatus(" This Information can't up updated because the GRN is not on Edit status");
              
           }


           bool isSaved = false;
           SqlTransaction tran;
           SqlConnection Conn = Connection.getConnection();
           tran = Conn.BeginTransaction();
           int AtStatus = -1;
           try
           {

               
               DriverInformation objDriverInfoDAL = new DriverInformation();
               isSaved = objDriverInfoDAL.UpdateDriverInformation(this, tran);
               if (isSaved == true)
               {
                   string AppMode = WFStepsName.EditDriverInfo.ToString();
                   AuditTrailBLL objAt = new AuditTrailBLL();
                   AtStatus = objAt.saveAuditTrail(this, objEdit, AppMode, UserBLL.GetCurrentUser(), "DriverInformationUpdate");
                   if (AtStatus == 1 || AtStatus == 0)
                   {
                       tran.Commit();
                   }
                   else
                   {
                       tran.Rollback();
                   }

               }
           }
           catch(Exception ex )
           {
               tran.Rollback();
               ErrorLogger.Log(ex);
               return false;
           }
           if (AtStatus == 1 || AtStatus == 0)
           {
               return true;
           }
           else
           {
               return false;
           }

       }

       public List<DriverInformationBLL> GetActiveDriverInformationByReceivigRequestId(Guid ReceivigRequestId)
       {
           List<DriverInformationBLL> list = new List<DriverInformationBLL>();
           try
           {
               list = DriverInformation.GetActiveDriverInformationByReceivigRequestId(ReceivigRequestId);
               return list;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public bool isDuplicate(Guid ReceivingRequestId, string LicenseNumber, string LicenseIssuedPlace)
       {
           int count = 0;
           count = DriverInformation.GetUniqueCount(ReceivingRequestId, LicenseNumber, LicenseIssuedPlace);
           if (count != 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       public DriverInformationBLL GetByReceivingRequestId(Guid Id)
       {
           DriverInformationBLL obj = new DriverInformationBLL();
           DataSet dsDriver = DriverInformation.GetDriverInformationByCommodityDepositRequestId(Id);
           if (dsDriver != null)
           {
               if (dsDriver.Tables[0].Rows.Count == 1)
               {
                   obj.Id = new Guid(dsDriver.Tables[0].Rows[0]["Id"].ToString());
                   obj.ReceivigRequestId = new Guid(dsDriver.Tables[0].Rows[0]["ReceivingRequestId"].ToString());
                   obj.DriverName = dsDriver.Tables[0].Rows[0]["DriverName"].ToString();
                   obj.LicenseNumber = dsDriver.Tables[0].Rows[0]["LicenseNumber"].ToString();
                   obj.LicenseIssuedPlace = dsDriver.Tables[0].Rows[0]["LicenseIssuedPlace"].ToString();
                   obj.PlateNumber = dsDriver.Tables[0].Rows[0]["PlateNumber"].ToString();
                   obj.TrailerPlateNumber = dsDriver.Tables[0].Rows[0]["TrailerPlateNumber"].ToString();
                   obj.Status = Convert.ToInt32(dsDriver.Tables[0].Rows[0]["Status"].ToString());
                   obj.Remark = dsDriver.Tables[0].Rows[0]["Remark"].ToString();
                   return obj;
               }
           }
           return null;
       }
       public DriverInformationBLL GetById(Guid Id)
       {
           DriverInformationBLL obj = new DriverInformationBLL();
           DataSet dsDriver = DriverInformation.GetDriverInformationById(Id);
           if (dsDriver != null)
           {
               if (dsDriver.Tables[0].Rows.Count == 1)
               {
                   obj.Id = new Guid(dsDriver.Tables[0].Rows[0]["Id"].ToString());
                   obj.ReceivigRequestId = new Guid(dsDriver.Tables[0].Rows[0]["ReceivingRequestId"].ToString());
                   obj.DriverName = dsDriver.Tables[0].Rows[0]["DriverName"].ToString();
                   obj.LicenseNumber = dsDriver.Tables[0].Rows[0]["LicenseNumber"].ToString();
                   obj.LicenseIssuedPlace = dsDriver.Tables[0].Rows[0]["LicenseIssuedPlace"].ToString();
                   obj.PlateNumber = dsDriver.Tables[0].Rows[0]["PlateNumber"].ToString();
                   obj.TrailerPlateNumber = dsDriver.Tables[0].Rows[0]["TrailerPlateNumber"].ToString();
                   obj.Status = Convert.ToInt32(dsDriver.Tables[0].Rows[0]["Status"].ToString());
                   obj.Remark = dsDriver.Tables[0].Rows[0]["Remark"].ToString();
                   return obj;
               }
           }
           return null;
       }
       private static bool isGRNEditable(Guid CommodityDepositeId)
       {
           GRNBLL objGRN = new GRNBLL();
           return objGRN.IsEditableGRN("CommodityRecivingId='" + CommodityDepositeId.ToString() + "'");

       }


    }
}
