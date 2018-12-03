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
using System.Data.SqlClient;
using WarehouseApplication.DAL;


namespace WarehouseApplication.BLL
{
    public enum WFStepsName
    {
        AddArrival, AddDriverInformation, AddVoucherInfo, GetSampleTicket, AddSamplingResult, AddSampleCoding,
        AddGradingResult, GradingResultCA, AddUnloadingInfo, GenerateGradingCode,
        AddScalingInfo, AddGRN, GRNAcceptance, EditGRN, EditGradeDispute, PreWeighTruck, PostWeighTruck,
        ApproveReSampling, OpenGRNForEdit, EditWHR, ClientAcceptance, WarehouseManagerAppr, CancelGRN, EditDriverInfo, EditAppGRN, NewVoucherInfo, EditVoucherInfo,
        GradingFactorUpdate, GradingFactorAdd, GradingFactorGroupAd, GradingFactorGroupEd, CommmodityGradingFactor, GRNServiceAdd, GRNServiceCancel, TrucksMissingForSamp,
        RequestforEditGRN, Stack, GetTrucksReadyForSam, ConfirmTrucksForsamp, ArrivalUpdate, EditGradingResultDet, EditGradingResult, EditResampling, EditSamplingResult,
        EditScaling, EditUnloading, AddReSamplingRequest, RegisterTruckWeight, CodeSampRec, WHAppGRNEdit, UpdateGRNNo, CommodityGradeTotalValue

    }
    public class WFTransaction
    {

        public static string GetTransaction(Guid TransactionTypeId)
        {
            string TransactionNo;

            bool isSaved = false;


            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            eng.OpenTransaction(TransactionTypeId, UserBLL.GetCurrentUser(),
                new string[] { "" }, WarehouseBLL.GetWarehouseCode(UserBLL.GetCurrentWarehouse()), out TransactionNo);

             return TransactionNo;
          


        }
        public static string GetTransaction(Guid TransactionTypeId, SqlTransaction tran)
        {
            string TransactionNo;

            bool isSaved = false;
             
                
                ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                eng.OpenTransaction(TransactionTypeId, UserBLL.GetCurrentUser(),
                    new string[] { "" }, WarehouseBLL.GetWarehouseCode(UserBLL.GetCurrentWarehouse()), out TransactionNo);
                isSaved = WarehouseTrackingNoBLL.Save(TransactionNo, tran);
                if (isSaved == false)
                {                  
                    WFTransaction.Remove(TransactionNo);
                    
                    throw new Exception("Unable to get Transaction.WFTransaction.");
                }
                else
                {
                     
                     return TransactionNo;
                }
               
            
            
        }
        public static string GetTransaction(String TransactionTypeName, SqlTransaction tran)
        {
            Guid TransactionTypeId = new Guid();
            TransactionTypeId = TransactionTypeProvider.GetTransactionTypeId(TransactionTypeName);
            return GetTransaction(TransactionTypeId, tran);


        }
        public static void WorkFlowManager(string TransactionNo)
        {
            if (String.IsNullOrEmpty(TransactionNo) == true)
            {
                throw new InvalidTransactionNumber("Invalid Tracking Number");
            }
            try
            {
                if (HttpContext.Current.Session["msg"] != null)
                {

                    ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                    ECXWF.CMessage mess = (ECXWF.CMessage)HttpContext.Current.Session["msg"];
                    mess.IsCompleted = true;
                    eng.Response(TransactionNo.Trim(), mess);
                }
                else
                {
                    throw new Exception("Session Expired");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Work Flow Exception.", ex);
            }


        }
        public static void WorkFlowManager(string TransactionNo, ECXWF.CMessage mess)
        {
            if (String.IsNullOrEmpty(TransactionNo) == true)
            {
                throw new InvalidTransactionNumber("Invalid Tracking Number");
            }
            try
            {
                if (mess != null)
                {

                    ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                    mess.IsCompleted = true;
                    eng.Response(TransactionNo.Trim(), mess);
                    HttpContext.Current.Session["msg"] = null; ;
                }
                else
                {
                    throw new Exception("Session Expired");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Work Flow Exception.", ex);
            }


        }
        public static void LoadVariables(string msg, string TrackingNo)
        {
            TrackingNo = TrackingNo.Trim();
            if (msg.Trim() == WFStepsName.AddDriverInformation.ToString())
            {
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailByTrackingNo(TrackingNo);
                if (objCDR != null)
                {
                    HttpContext.Current.Session["CommodityRequestId"] = objCDR.Id;
                }
                else
                {
                    throw new Exception("Can't load page variables,please try again.");
                }
            }
            else if (msg.Trim() == WFStepsName.AddVoucherInfo.ToString())
            {
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailByTrackingNo(TrackingNo);
                if (objCDR != null)
                {
                    HttpContext.Current.Session["CommodityRequestId"] = objCDR.Id;
                }
                else
                {
                    throw new Exception("Can't load page variables,please try again.");
                }
            }
            else if (msg.Trim() == WFStepsName.AddDriverInformation.ToString())
            {
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailByTrackingNo(TrackingNo);
                if (objCDR != null)
                {
                    HttpContext.Current.Session["CommodityRequestId"] = objCDR.Id;
                }
                else
                {
                    throw new Exception("Can't load page variables,please try again.");
                }
            }
            else if (msg.Trim() == WFStepsName.AddVoucherInfo.ToString())
            {
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailByTrackingNo(TrackingNo);
                if (objCDR != null)
                {
                    HttpContext.Current.Session["CommodityRequestId"] = objCDR.Id;
                }
                else
                {
                    throw new Exception("Can't load page variables,please try again.");
                }
            }
            else if (msg.Trim() == WFStepsName.GetSampleTicket.ToString())
            {

            }
            else if (msg.Trim() == WFStepsName.AddSamplingResult.ToString())
            {
                List<SamplingBLL> list = new List<SamplingBLL>();
                SamplingBLL obj = new SamplingBLL();
                list = obj.GetSamplesPenndingResultByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        obj = list[0];
                        HttpContext.Current.Session["SamplingReasultAddId"] = obj.Id;
                    }

                }
            }
            else if (msg.Trim() == WFStepsName.AddSampleCoding.ToString())
            {


                //SamplingResultBLL obj = new SamplingResultBLL();
                //obj = obj.GetSamplesResultsPendingCodingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                //if (obj != null)
                //{
                //    if (string.IsNullOrEmpty(obj.TrackingNo) != true)
                //    {

                //        HttpContext.Current.Session["GenerateCodeSampleId"] = obj.Id;
                //    }
                //    else
                //    {
                //        throw new Exception("Unable to get Record");
                //    }

                //}
                SamplingResultBLL objGrading = new SamplingResultBLL();

                objGrading = objGrading.GetSamplesResultsPendingCodingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (objGrading != null)
                {
                    if (string.IsNullOrEmpty(objGrading.TrackingNo) != true)
                    {

                        HttpContext.Current.Session["GradingCodeId"] = objGrading.SamplingResultCode;
                    }

                }
            }
            else if (msg.Trim() == WFStepsName.GenerateGradingCode.ToString())
            {
                SamplingResultBLL objGrading = new SamplingResultBLL();

                objGrading = objGrading.GetSamplesResultsPendingCodingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (objGrading != null)
                {
                    if (string.IsNullOrEmpty(objGrading.TrackingNo) != true)
                    {

                        HttpContext.Current.Session["GradingCodeId"] = objGrading.SamplingResultCode;
                        HttpContext.Current.Session["GradingCodeTrackingNo"] = TrackingNo.ToString();
                    }

                }
            }
            else if (msg.Trim() == WFStepsName.CodeSampRec.ToString())
            {
                GradingBLL objGrading = new GradingBLL();
                List<GradingBLL> list = new List<GradingBLL>();
                list = objGrading.GetGradingsPendingCodeReceivingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        objGrading = list[0];
                        HttpContext.Current.Session["CodeSampleRecivedGradingId"] = objGrading.Id;
                    }

                }
            }
            else if (msg.Trim() == WFStepsName.AddGradingResult.ToString())
            {
                GradingBLL objGrading = new GradingBLL();
                List<GradingBLL> list = new List<GradingBLL>();
                list = objGrading.GetGradingsPendingResultByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        objGrading = list[0];
                        HttpContext.Current.Session["GradingRecivedGradingId"] = objGrading.Id;
                    }

                }
            }
            else if (msg.Trim() == WFStepsName.GradingResultCA.ToString())
            {
                GradingResultBLL objGrading = new GradingResultBLL();
                objGrading = objGrading.GetGradingResultByTrackingNo(TrackingNo);
                if (objGrading != null)
                {
                    if (objGrading.ID != null)
                    {
                        HttpContext.Current.Session["GRID"] = objGrading.ID;
                    }
                }
            }

            else if (msg.Trim() == WFStepsName.PreWeighTruck.ToString())
            {
                GradingResultBLL objGrading = new GradingResultBLL();
                List<GradingResultBLL> list = new List<GradingResultBLL>();
                list = objGrading.GetAcceptedresultsPendingUnloadingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        objGrading = list[0];
                        HttpContext.Current.Session["AddUnLoadingId"] = objGrading.ID;
                        HttpContext.Current.Session["AddUnLoadingIdGradingCode"] = objGrading.GradingCode;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.AddUnloadingInfo.ToString())
            {
                GradingResultBLL objGrading = new GradingResultBLL();
                List<GradingResultBLL> list = new List<GradingResultBLL>();
                list = objGrading.GetAcceptedresultsPendingUnloadingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        objGrading = list[0];
                        HttpContext.Current.Session["AddUnLoadingId"] = objGrading.ID;
                        HttpContext.Current.Session["AddUnLoadingIdGradingCode"] = objGrading.GradingCode;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.PostWeighTruck.ToString())
            {
                GradingResultBLL objGrading = new GradingResultBLL();
                List<GradingResultBLL> list = new List<GradingResultBLL>();
                list = objGrading.GetAcceptedresultsPendingScalingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        objGrading = list[0];
                        HttpContext.Current.Session["AddScalingGradingCode"] = objGrading.GradingCode;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.AddScalingInfo.ToString())
            {
                GradingResultBLL objGrading = new GradingResultBLL();
                List<GradingResultBLL> list = new List<GradingResultBLL>();
                list = objGrading.GetAcceptedresultsPendingScalingByTrackingNo(UserBLL.GetCurrentWarehouse(), TrackingNo);
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        objGrading = list[0];
                        HttpContext.Current.Session["AddScalingGradingCode"] = objGrading.GradingCode;
                    }
                }
            }
            // ADD GRN
            else if (msg.Trim() == WFStepsName.AddGRN.ToString())
            {
                GradingResultBLL objGrading = new GradingResultBLL();
                objGrading = objGrading.GetGradingResultByTrackingNo(TrackingNo);
                if (objGrading != null)
                {
                    if (objGrading.ID != null)
                    {
                        HttpContext.Current.Session["GRNID"] = TrackingNo; // objGrading.ID;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.GRNAcceptance.ToString())
            {
                GRNBLL objGRN = new GRNBLL();
                objGRN = objGRN.GetbyByTrackingNo(TrackingNo);
                if (objGRN != null)
                {
                    if (objGRN.Id != null)
                    {
                        HttpContext.Current.Session["GRNID"] = objGRN.Id;
                    }
                    else
                    {
                        throw new Exception("Invalid GRN Id");
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.ClientAcceptance.ToString())
            {
                RequestforEditGRNBLL objGRN = new RequestforEditGRNBLL();
                objGRN = objGRN.GetByTrackingNo(TrackingNo);
                if (objGRN != null)
                {
                    if (objGRN.Id != null)
                    {
                        HttpContext.Current.Session["GRNID"] = objGRN.GRNId;
                    }
                    else
                    {
                        throw new Exception("Invalid GRN Id");
                    }
                }
            }
            //Update GRN
            else if (msg.Trim() == WFStepsName.UpdateGRNNo.ToString())
            {
                GRNBLL objGRN = new GRNBLL();
                objGRN = objGRN.GetbyByTrackingNo(TrackingNo);
                if (objGRN != null)
                {
                    if (objGRN.GRN_Number != null)
                    {
                        HttpContext.Current.Session["GRNIDUpdateGRNNo"] = objGRN.Id;
                        HttpContext.Current.Session["TrackingNoUpdateGRNNo"] = TrackingNo;

                    }
                }
            }
            else if (msg.Trim() == WFStepsName.EditGRN.ToString())
            {
                GRNBLL objGRN = new GRNBLL();
                objGRN = objGRN.GetbyByTrackingNo(TrackingNo);
                if (objGRN != null)
                {
                    if (objGRN.GRN_Number != null)
                    {
                        HttpContext.Current.Session["GRNID"] = objGRN.Id;
                        HttpContext.Current.Session["GRNTrackingNo"] = TrackingNo;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.WarehouseManagerAppr.ToString())
            {
                RequestforEditGRNBLL objGRN = new RequestforEditGRNBLL();
                objGRN = objGRN.GetByTrackingNo(TrackingNo);
                if (objGRN != null)
                {
                    if (objGRN.Id != null)
                    {
                        HttpContext.Current.Session["GRNID"] = objGRN.GRNId;
                        HttpContext.Current.Session["GRNTrackingNo"] = TrackingNo;
                    }
                    else
                    {
                        throw new Exception("Invalid GRN Id");
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.EditGradeDispute.ToString())
            {
                GradingDisputeBLL objGradeDispute = new GradingDisputeBLL();
                objGradeDispute = objGradeDispute.GetByTransactionNo(TrackingNo);
                if (objGradeDispute != null)
                {
                    if (objGradeDispute.Id != null)
                    {
                        HttpContext.Current.Session["EditGradeDisputeId"] = objGradeDispute.Id;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.ApproveReSampling.ToString())
            {
                ReSamplingBLL objReSampling = new ReSamplingBLL();
                objReSampling = objReSampling.GetByTrackingNo(TrackingNo);
                if (objReSampling != null)
                {
                    if (objReSampling.Id != null)
                    {
                        HttpContext.Current.Session["ResamplingEdit"] = objReSampling.Id;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.WHAppGRNEdit.ToString())
            {
                RequestforEditGRNBLL obj = new RequestforEditGRNBLL();
                obj = obj.GetByTrackingNo(TrackingNo);
                if (obj != null)
                {
                    if (obj.Id != null)
                    {
                        HttpContext.Current.Session["GRNEditRequestId"] = obj.Id;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.OpenGRNForEdit.ToString())
            {
                RequestforEditGRNBLL obj = new RequestforEditGRNBLL();
                obj = obj.GetByTrackingNo(TrackingNo);
                if (obj != null)
                {
                    if (obj.Id != null)
                    {
                        HttpContext.Current.Session["OpenGRNEditId"] = obj.Id;
                    }
                }
            }
            else if (msg.Trim() == WFStepsName.EditWHR.ToString().Trim())
            {
                RequestforEditGRNBLL obj = new RequestforEditGRNBLL();
                obj = obj.GetByTrackingNo(TrackingNo);
                if (obj != null)
                {
                    if (obj.GRNId != null)
                    {
                        HttpContext.Current.Session["ReCreateGRNID"] = obj.GRNId;
                        HttpContext.Current.Session["ReCreateGRNTrackingNo"] = TrackingNo;
                    }
                }

            }
            else if (msg.Trim() == "ApproveCancelationRequest".ToString().Trim() || (msg.Trim() == "CancelGRN"))
            {
                RequestforApprovedGRNCancelationBLL obj = new RequestforApprovedGRNCancelationBLL();
                obj = obj.GetByTrackingNo(TrackingNo);
                if (obj != null)
                {
                    if (obj.GRNId != null)
                    {
                        HttpContext.Current.Session["CancelGRNTrackingNo"] = TrackingNo;
                        HttpContext.Current.Session["GRNID"] = obj.GRNId;
                    }
                }

            }
            else if (msg.Trim() == WFStepsName.EditGradingResult.ToString().Trim())
            {
                GradingResultBLL objGradingResult = new GradingResultBLL();
                HttpContext.Current.Session["GRID"] = null;
                objGradingResult = objGradingResult.GetGradingResultByTrackingNo(TrackingNo.Trim());
                if (objGradingResult != null)
                {
                    HttpContext.Current.Session["GRID"] = objGradingResult.ID.ToString();
                }
                else
                {
                    objGradingResult = new GradingResultBLL();
                    objGradingResult = objGradingResult.GetGradingResultByTrackingNoForGradeDispute(TrackingNo.Trim());
                    if (objGradingResult != null)
                    {
                        HttpContext.Current.Session["GRID"] = objGradingResult.ID.ToString();
                    }
                }

            }
            else if (msg.Trim() == "UpdateClientNo")
            {
                 //NoClient
                CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                objCDR = objCDR.GetCommodityDepositeDetailByTrackingNo(TrackingNo);
                if (objCDR != null)
                {
                    HttpContext.Current.Session["CommodityRequestId"] = objCDR.Id;
                }
                else
                {
                    throw new Exception("Can't load page variables,please try again.");
                }
            }

        }
        public static int GetOpentransactionCount(string transactionTypeCode, string taskName, string step)
        {
            int count = 0;
            string[] transaction;
            List<string> lsttransaction;

            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            //try
            //{

                //transaction = eng.GetTransactionsByTaskName(transactionTypeCode, taskName, step);
            transaction = null;
                lsttransaction = new List<string>();
               // lsttransaction = transaction.ToList<string>();
                
                string str = "";
                if (lsttransaction != null)
                {
                    if (lsttransaction.Count > 0)
                    {
                        foreach (string s in lsttransaction)
                        {
                            if (s != null)
                            {
                                str += "'" + s + "' , ";
                            }
                        }
                        int x = str.LastIndexOf(',');
                        str = str.Remove(x);
                        lsttransaction = WarehouseTrackingNoDAL.GetWarehouseTracking(str, UserBLL.GetCurrentWarehouse());

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

          //  }
          //  catch (Exception ex)
          //  {
          //     throw new Exception("Unable to Get Workflow Data", ex);
          //  }
            if (lsttransaction != null)
            {
                count = lsttransaction.Count;
            }
            else
            {
                return 0;
            }
            //Check the transaction Numbers are in the current WH from DB.

            //Get the count of the new Transaction array after current WH from DB

            return count;
        }
        public static string[] GetOpentransaction(string transactionTypeCode, string taskName, string step)
        {
            string[] transaction;
            List<string> lstTran = null;
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            transaction = eng.GetTransactionsByTaskName(transactionTypeCode, taskName, step);
            if (transaction.Count() > 0)
            {
                lstTran = new List<string>();
                lstTran = transaction.ToList<string>();
                if (lstTran != null)
                {
                    if (lstTran.Count > 0)
                    {
                        string str = "";
                        foreach (string s in lstTran)
                        {
                            str += "'" + s + "' , ";
                        }
                        int x = str.LastIndexOf(',');
                        str = str.Remove(x);
                        lstTran = WarehouseTrackingNoDAL.GetWarehouseTracking(str, UserBLL.GetCurrentWarehouse());
                        if (lstTran != null)
                        {
                            if (lstTran.Count > 0)
                            {
                                return lstTran.ToArray<string>();
                            }
                        }
                    }
                }


            }
            else
            {
                return null;
            }

            //Check the transaction Numbers are in the current WH from DB.

            //Get the count of the new Transaction array after current WH from DB
            return null;
        }
        public static string[] GetTransaction(string transactionTypeCode, string taskName, string step)
        {
            string[] strTrackingNo;
            try
            {
                ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                strTrackingNo = eng.GetTransactionsByTaskName(transactionTypeCode, taskName, step);
                return strTrackingNo;
            }
            catch
            {
                throw new Exception("Unable to get transactions.");
            }

        }
        public static void Close(string TransactionNo)
        {
            TransactionNo = TransactionNo.Trim();
            if (String.IsNullOrEmpty(TransactionNo) == true)
            {
                throw new InvalidTransactionNumber("Invalid Tracking Number");
            }
            try
            {
                ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                eng.CloseTransaction(TransactionNo);

            }
            catch (Exception ex)
            {

                throw new Exception("Workflow Engine Exception: Unable to close task.", ex);
            }
        }
        public static void Remove(string TransactionNo)
        {
            TransactionNo = TransactionNo.Trim();
            if (String.IsNullOrEmpty(TransactionNo) == true)
            {
                throw new InvalidTransactionNumber("Invalid Tracking Number");
            }
            try
            {
                ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
                eng.RemoveTransaction(TransactionNo);

            }
            catch (Exception ex)
            {

                throw new Exception("Workflow Engine Exception: Unable to Remove task.", ex);
            }
        }
        public static string GetMessage(string transactionNo)
        {
            transactionNo = transactionNo.Trim();
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            ECXWF.CMessage[] mess = null;
            try
            {

                mess = eng.Request(transactionNo, UserBLL.GetCurrentUser(), new string[] { WarehouseBLL.CurrentWarehouse.Location });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (mess == null)
                return "";
            if (mess.Length > 1)
            {
                throw new Exception("Inappropriate Message exception");
            }
            else if (mess.Length == 1)
            {
                return mess[0].Name.ToString();
            }
            return "";
        }
        public static ECXWF.CMessage Request(string transactionNo)
        {
            transactionNo = transactionNo.Trim();
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            ECXWF.CMessage[] mess = null;
            try
            {

                mess = eng.Request(transactionNo, UserBLL.GetCurrentUser(), new string[] { WarehouseBLL.CurrentWarehouse.Location });
                if (mess != null)
                {
                    if (mess.Length > 0)
                    {

                        return mess[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public static void UnlockTask(string transactionNo)
        {
            transactionNo = transactionNo.Trim();
            ECXWF.ECXEngine eng = new WarehouseApplication.ECXWF.ECXEngine();
            eng.UnlockTask(transactionNo);
        }
        public List<String> GetWarehouseTrackingNo(string TrackingNo)
        {
            List<String> list = null;

            return list;
        }
       


    }
}
