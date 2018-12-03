using System;
using System.Data;
using System.Data.SqlClient;
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

namespace WarehouseApplication.BLL
{
    public enum VoucherInformationStatus { New = 1, Active, Cancelled };
    public class VoucherInformationBLL : GeneralBLL
    {
        private Guid _Id;
        private Guid _DepositRequestId;
        private string _VoucherNo;
        private Guid _CoffeeTypeId;
        private string _SpecificArea;
        private int _NumberofBags;
        private int _NumberOfPlomps;
        private int _NumberOfPlompsTrailer;
        private string _CertificateNo;
        private int _Status;

        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Guid DepositRequestId
        {
            get { return _DepositRequestId; }
            set { _DepositRequestId = value; }
        }
        public string VoucherNo
        {
            get { return _VoucherNo; }
            set { _VoucherNo = value; }
        }
        public Guid CoffeeTypeId
        {
            get { return _CoffeeTypeId; }
            set { _CoffeeTypeId = value; }
        }
        public string SpecificArea
        {
            get { return _SpecificArea; }
            set { _SpecificArea = value; }
        }
        public int NumberofBags
        {
            get { return _NumberofBags; }
            set { _NumberofBags = value; }
        }
        public int NumberOfPlomps
        {
            get { return _NumberOfPlomps; }
            set { _NumberOfPlomps = value; }
        }
        public int NumberOfPlompsTrailer
        {
            get { return _NumberOfPlompsTrailer; }
            set { _NumberOfPlompsTrailer = value; }
        }
        public string CertificateNo
        {
            get { return _CertificateNo; }
            set { _CertificateNo = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //oct-18-2011
        //update empty voucher entry error
        public bool Save(Guid DepositRequestId, string VoucherNo, Guid CoffeeTypeId,
         string SpecificArea, int NumberofBags, int NumberOfPlomps, int NumberOfPlompsTrailer, string CertificateNo, Guid CreatedBy,
         int Status, string TrackingNo)
        {
            SqlTransaction tran;
            bool isSaved = false;

            //get Tracking No.
            CommodityDepositeRequestBLL oC = new CommodityDepositeRequestBLL();
            oC = oC.GetCommodityDepositeDetailById(DepositRequestId);
            TrackingNo = oC.TrackingNo;

            VoucherInformationBLL obj = new VoucherInformationBLL();
            obj.DepositRequestId = DepositRequestId;
            obj.VoucherNo = VoucherNo;
            obj.CoffeeTypeId = CoffeeTypeId;
            obj.SpecificArea = SpecificArea;
            obj.NumberofBags = NumberofBags;
            obj.NumberOfPlomps = NumberOfPlomps;
            obj.NumberOfPlompsTrailer = NumberOfPlompsTrailer;
            obj.CertificateNo = CertificateNo;
            obj.CreatedBy = CreatedBy;
            obj.Status = Status;
            Voucher objSave = new Voucher();
            SqlConnection conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            AuditTrailBLL objAt = new AuditTrailBLL();
            int AtSaved = -1;
            try
            {
                Guid VoucherId = Guid.Empty;
                VoucherId = objSave.InsertVoucherInformation(obj, tran);
                if (VoucherId == Guid.Empty)
                {
                    isSaved = false;
                }
                else
                {
                    obj.Id = VoucherId;
                    if (objAt.saveAuditTrail(obj, WFStepsName.NewVoucherInfo.ToString(), UserBLL.GetCurrentUser(), "Insert Voucher") == 1)
                    {
                        AtSaved = 1;
                        string msgFromWF = "";
                        msgFromWF = WFTransaction.GetMessage(TrackingNo);
                        if (msgFromWF == "AddVoucherInfo")
                        {
                            WFTransaction.WorkFlowManager(TrackingNo);
                            WFTransaction.UnlockTask(TrackingNo);
                            HttpContext.Current.Session["msg"] = null;
                        }
                        tran.Commit();
                        isSaved = true;
                    }
                    else
                    {
                        tran.Rollback();
                        isSaved = false;
                    }
                }

            }
            catch (Exception ex)
            {
                tran.Rollback();
                if (AtSaved == 1)
                {
                    objAt.RoleBack();
                }
                throw ex;
            }
            finally
            {
                conn.Close();
                tran.Dispose();
            }
            return isSaved;






        }

        public bool Update(Guid Id,Guid RecReqId,  string VoucherNo, Guid CoffeeTypeId,
             string SpecificArea, int NumberofBags, int NumberOfPlomps, int NumberOfPlompsTrailer, string CertificateNo, Guid LastModifiedBy,
             int Status)
        {

            if (isGRNEditable(RecReqId ) == false)
            {
                throw new GRNNotOnUpdateStatus("This Information can't up updated because the GRN is not on Edit status");
                
            }



            bool isSaved = false;
            SqlTransaction tran;
            SqlConnection conn;
            VoucherInformationBLL objEdit = new VoucherInformationBLL();
            objEdit = objEdit.GetVoucherInformationById(Id);
            VoucherInformationBLL obj = new VoucherInformationBLL();
            obj.Id = Id;
            obj.VoucherNo = VoucherNo;
            obj.CoffeeTypeId = CoffeeTypeId;
            obj.SpecificArea = SpecificArea;
            obj.NumberofBags = NumberofBags;
            obj.NumberOfPlomps = NumberOfPlomps;
            obj.NumberOfPlompsTrailer = NumberOfPlompsTrailer;
            obj.CertificateNo = CertificateNo;
            obj.LastModifiedBy = LastModifiedBy;
            obj.Status = Status;
            obj.DepositRequestId = RecReqId;
            conn = Connection.getConnection();
            tran = conn.BeginTransaction();
            Voucher objUpdate = new Voucher();
            int atStatus = -1;
            try
            {
                isSaved = objUpdate.UpdateVoucherInformation(obj, tran);
                if (isSaved == true)
                {
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    string AppMode = WFStepsName.EditVoucherInfo.ToString();
                    atStatus = objAt.saveAuditTrail(objEdit, obj, AppMode, UserBLL.GetCurrentUser(), "UpdateVoucher");
                    if (atStatus == 1)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }





                }
                else
                {
                    tran.Rollback();
                    tran.Dispose();
                    conn.Close();

                }

            }
            catch( Exception ex)
            {
                tran.Rollback();
                throw ex;
               
            }
            finally
            {
                tran.Dispose();
                conn.Close();

            }
            return isSaved;

        }

        public VoucherInformationBLL GetVoucherInformationById(Guid Id)
        {

            return Voucher.getVoucherInformationById(Id);
        }
        public VoucherInformationBLL GetVoucherInformationByCommodityDepositRequestId(Guid Id)
        {

            return Voucher.getVoucherInformationByDepositRequestId(Id);
        }
        private static bool isGRNEditable(Guid CommodityDepositeId)
        {
            GRNBLL objGRN = new GRNBLL();
            return objGRN.IsEditableGRN("CommodityRecivingId='" + CommodityDepositeId.ToString() + "'");

        }
       


    }
}
