using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.IO;


namespace WarehouseApplication.BLL
{
    [Serializable]
    public class TransactionDetail
    {
        private string _DisplayName;

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }
        private string _TrackNo;

        public string TrackNo
        {
            get { return _TrackNo; }
            set { _TrackNo = value; }
        }
        public TransactionDetail(string displayname, string trackNo)
        {
            this.DisplayName = displayname;
            this.TrackNo = trackNo;
        }
    }
    public enum WFFlowNames { WRC, WRG, WRCRS }
    public enum WFStepName { WarehouseManagerAppr, EditGRN, AddSamplingResult, AddGRN, GRNAcceptance, ClientAcceptance, GenerateGradingCode, AddGradingResult, GradingResultCA, PreWeighTruck, AddUnloadingInfo, PostWeighTruck, AddScalingInfo, UpdateGRNNo }
    public class InboxItems
    {
        private string _Name;
        private List<TransactionCode> _TransactionCodeList;
        private int _Count;
        public int Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }
        public List<TransactionCode> TransactionCodeList
        {
            get { return _TransactionCodeList; }
            set { _TransactionCodeList = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public InboxItems()
        {
        }
        public InboxItems(string name, List<TransactionCode> list)
        {
            this.Name = name;
            this.TransactionCodeList = list;
        }

        public List<InboxItems> GetGrantedInboxItems(List<InboxItems> objInboxItem)
        {
            List<InboxItems> myInboxItem = new List<InboxItems>();
            //Get all Inbox Item Rights.
            List<string> Rights = new List<string>();
            foreach (InboxItems inbox in objInboxItem)
            {
                foreach (TransactionCode t in inbox.TransactionCodeList)
                {
                    Rights.Add(t.Right);
                }
            }
            string[] RightArr = Rights.ToArray<String>();
            //Todo Add Warehouse Location.
            List<string> hasRight = UserBLL.HasRoles(UserBLL.GetCurrentUser(), RightArr);
            foreach (InboxItems inbox in objInboxItem)
            {
                List<TransactionCode> grantedTC = new List<TransactionCode>();
                foreach (TransactionCode t in inbox.TransactionCodeList)
                {
                    var has = from x in hasRight
                              where x.ToUpper() == t.Right.ToUpper()
                              select x;
                    if (has.Count<string>() > 0)
                    {
                        grantedTC.Add(t);
                    }
                }
                inbox.TransactionCodeList = grantedTC;
                if (grantedTC.Count > 0)
                {
                    myInboxItem.Add(inbox);
                }

            }


            return myInboxItem;
        }
        public InboxItems GetGrantedInboxItem(InboxItems objInboxItem)
        {
            List<InboxItems> myInboxItem = new List<InboxItems>();
            //Get all Inbox Item Rights.
            List<string> Rights = new List<string>();

            foreach (TransactionCode t in objInboxItem.TransactionCodeList)
            {
                Rights.Add(t.Right);
            }

            string[] RightArr = Rights.ToArray<String>();
            //Todo Add Warehouse Location.
            List<string> hasRight = UserBLL.HasRoles(UserBLL.GetCurrentUser(), RightArr);
            InboxItems inbox = new InboxItems();
            List<TransactionCode> grantedTC = new List<TransactionCode>();
            foreach (TransactionCode t in objInboxItem.TransactionCodeList)
            {
                var has = from x in hasRight
                          where x.ToUpper() == t.Right.ToUpper()
                          select x;
                if (has.Count<string>() > 0)
                {
                    grantedTC.Add(t);
                }
            }
            inbox.TransactionCodeList = grantedTC;
            return inbox;
        }
        public List<TransactionDetail> GetTransactions()
        {
            List<TransactionDetail> list = new List<TransactionDetail>();
            try
            {
                string[] TranArr;
                foreach (TransactionCode tc in this.TransactionCodeList)
                {
                    List<TransactionDetail> Templist = new List<TransactionDetail>();
                    TranArr = WFTransaction.GetOpentransaction(tc.FlowName, tc.StepName, tc.Step);
                    if (TranArr != null)
                    {
                        if (TranArr.Count() > 0)
                        {
                            Templist = getDislayName(TranArr, tc.FlowName, tc.StepName);
                        }

                        foreach (TransactionDetail detail in Templist)
                        {
                            list.Add(detail);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        private static List<TransactionDetail> getDislayName(string[] TranArr, string FlowName, string StepName)
        {
            string str = "";
            List<TransactionDetail> list;
            for (int i = 0; i < TranArr.Count(); i++)
            {

                if (i < TranArr.Count() - 1)
                {
                    str += "'" + TranArr[i].ToString() + "' ,";
                }
                else
                {
                    str += "'" + TranArr[i].ToString() + "'";
                }

            }
            list = new List<TransactionDetail>();
            if (StepName == WFStepName.AddSamplingResult.ToString())
            {

                SamplingBLL objSampling = new SamplingBLL();
                if (FlowName.Trim().ToUpper() == "WRCM".ToUpper())
                {
                    TranArr = objSampling.GetMixedSamplingCodeBylistTrackingNo(str);
                }
                else
                {
                    TranArr = objSampling.GetSamplingCodeBylistTrackingNo(str);
                }
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.GenerateGradingCode.ToString())
            {
                SamplingResultBLL objSampling = new SamplingResultBLL();
                TranArr = objSampling.GetSamplingResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    if (TranArr != null)
                    {
                        for (int x = 0; x < TranArr.Count(); x++)
                        {
                            string[] temp = TranArr[x].Split('*');
                            list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                        }
                    }
                }
                // regrading 
                GradingDisputeBLL objGradeDispute = new GradingDisputeBLL();
                TranArr = objGradeDispute.GetReGradingRequestbyTrackingNo(str);
                if (TranArr != null)
                {
                    if (TranArr != null)
                    {
                        for (int x = 0; x < TranArr.Count(); x++)
                        {
                            string[] temp = TranArr[x].Split('*');
                            list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                        }
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.AddGradingResult.ToString())
            {
                GradingBLL objSampling = new GradingBLL();
                TranArr = objSampling.GetGradingResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.GradingResultCA.ToString())
            {
                GradingBLL objSampling = new GradingBLL();
                TranArr = objSampling.GetGradingResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }


                return list;
            }
            else if (StepName == WFStepName.PreWeighTruck.ToString() || StepName == WFStepName.AddUnloadingInfo.ToString())
            {
                GradingResultBLL obj = new GradingResultBLL();
                TranArr = obj.GetGradingResultResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.PostWeighTruck.ToString() || StepName == WFStepName.AddScalingInfo.ToString())
            {
                //TODO _ check hwewe
                //post Weight que No.
                GradingResultBLL obj = new GradingResultBLL();
                TranArr = obj.GetGradingResultResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.AddGRN.ToString())
            {
                GRNBLL obj = new GRNBLL();
                TranArr = obj.GetGradingResultResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.GRNAcceptance.ToString() && FlowName == "WHEditAppGRN")
            {
                GRNBLL obj = new GRNBLL();
                TranArr = obj.GetGRNNoBylistEditTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.GRNAcceptance.ToString() && FlowName != "WHEditAppGRN")
            {
                GRNBLL obj = new GRNBLL();
                TranArr = obj.GetGRNNoBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            //Update GRN
            else if (StepName == WFStepName.UpdateGRNNo.ToString())
            {
                GRNBLL obj = new GRNBLL();
                TranArr = obj.GetGRNNoBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.WarehouseManagerAppr.ToString() && FlowName == "WHEditAppGRN")
            {
                GRNBLL obj = new GRNBLL();
                TranArr = obj.GetGRNNoBylistEditTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepName.EditGRN.ToString() || StepName == WFStepName.WarehouseManagerAppr.ToString())
            {
                GRNBLL obj = new GRNBLL();
                TranArr = obj.GetGRNNoBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else if (StepName == WFStepsName.EditGradingResult.ToString())
            {
                string strTr;
                GradingDisputeBLL objGrDisp = new GradingDisputeBLL();
                TranArr = objGrDisp.GetReGradingRequestbyTrackingNo(str);

                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }

                return list;
            }
            else if (StepName.ToUpper().Trim() == "CodeSampRec".ToUpper().Trim())
            {
                GradingBLL objSampling = new GradingBLL();
                TranArr = objSampling.GetGradingResultCodeBylistTrackingNo(str);
                if (TranArr != null)
                {
                    for (int x = 0; x < TranArr.Count(); x++)
                    {
                        string[] temp = TranArr[x].Split('*');
                        list.Add(new TransactionDetail(temp[0].Trim(), temp[1].Trim()));
                    }
                }
                return list;
            }
            else
            {
                for (int x = 0; x < TranArr.Count(); x++)
                {
                    list.Add(new TransactionDetail(TranArr[x].Trim(), TranArr[x].Trim()));
                }
                return list;
            }


        }



    }
    public class TransactionCode
    {
        private string _FlowName;
        private string _Step;
        private string _StepName;
        private string _Right;

        public string FlowName
        {
            get { return _FlowName; }
            set { _FlowName = value; }
        }
        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }
        public string Step
        {
            get { return _Step; }
            set { _Step = value; }
        }
        public string Right
        {
            get { return _Right; }
            set { _Right = value; }
        }
        public TransactionCode()
        {
        }
        public TransactionCode(string flowName, string stepName, string step, string right)
        {
            this.FlowName = flowName;
            this.StepName = stepName;
            this.Step = step;
            this.Right = right;
        }

    }
    public class XMLHelper
    {
        public string xmlPath;
        public XMLHelper(string path)
        {
            this.xmlPath = path;
        }

        public List<InboxItems> ReadInBoxXML()
        {
            List<InboxItems> MyInBox = new List<InboxItems>();
            HttpApplicationState AppStates = null;

            AppStates = HttpContext.Current.Application.Contents;
            string strAppCon = xmlPath.Remove(xmlPath.IndexOf('.'));
            //if (xmlPath.ToUpper() == "InboxGRNQueue.xml".ToUpper())
            //{
            if (AppStates != null && AppStates.Contents[strAppCon] != null)
            {
                return (List<InboxItems>)AppStates.Contents[strAppCon];
            }
            else
            {
                MyInBox = this.GetFormXML();
                AppStates.Contents.Add(strAppCon, MyInBox);
                return (List<InboxItems>)AppStates.Contents[strAppCon];
            }




            //}
            //else if (xmlPath.ToUpper() == "InboxGDQueue.xml".ToUpper())
            //{
            //    if (AppStates.Contents["InboxGDQueue"] != null)
            //    {
            //        return (List<InboxItems>)AppStates.Contents["InboxGDQueue"];
            //    }
            //    else
            //    {
            //        MyInBox = this.GetFormXML();
            //        AppStates.Contents.Add("InboxGDQueue", MyInBox);
            //        return (List<InboxItems>)AppStates.Contents["InboxGDQueue"];
            //    }
            //}
            //else if (xmlPath.ToUpper() == "InboxGINQueue.xml".ToUpper())
            //{
            //    if (AppStates.Contents["InboxGINQueue"] != null)
            //    {
            //        return (List<InboxItems>)AppStates.Contents["InboxGINQueue"];
            //    }
            //    else
            //    {
            //        MyInBox = this.GetFormXML();
            //        AppStates.Contents.Add("InboxGINQueue", MyInBox);
            //        return (List<InboxItems>)AppStates.Contents["InboxGINQueue"];
            //    }
            //}
            //else if (xmlPath.ToUpper() == "InboxRCRSQueue.xml".ToUpper())
            //{
            //    if (AppStates.Contents["InboxRCRSQueue"] != null)
            //    {
            //        return (List<InboxItems>)AppStates.Contents["InboxRCRSQueue"];
            //    }
            //    else
            //    {
            //        MyInBox = this.GetFormXML();
            //        AppStates.Contents.Add("InboxRCRSQueue", MyInBox);
            //        return (List<InboxItems>)AppStates.Contents["InboxRCRSQueue"];
            //    }
            //}
            //else if (xmlPath.ToUpper() == "InboxApprovedGRNEditRequest.xml".ToUpper())
            //{
            //    if (AppStates.Contents["InboxApprovedGRNEditRequest"] != null)
            //    {
            //        return (List<InboxItems>)AppStates.Contents["InboxRCRSQueue"];
            //    }
            //    else
            //    {
            //        MyInBox = this.GetFormXML();
            //        AppStates.Contents.Add("InboxApprovedGRNEditRequest", MyInBox);
            //        return (List<InboxItems>)AppStates.Contents["InboxApprovedGRNEditRequest"];
            //    }
            //}
            //else if (xmlPath.ToUpper() == "InboxCancelApprovedGRNQueue.xml".ToUpper())
            //{
            //    if (AppStates.Contents["InboxCancelApprovedGRNQueue"] != null)
            //    {
            //        return (List<InboxItems>)AppStates.Contents["InboxCancelApprovedGRNQueue"];
            //    }
            //    else
            //    {
            //        MyInBox = this.GetFormXML();
            //        AppStates.Contents.Add("InboxCancelApprovedGRNQueue", MyInBox);
            //        return (List<InboxItems>)AppStates.Contents["InboxCancelApprovedGRNQueue"];
            //    }
            //}
            //else if (xmlPath.ToUpper() == "InboxGINEditQueue.xml".ToUpper())
            //{
            //    if (AppStates.Contents["InboxGINEditQueue"] != null)
            //    {
            //        return (List<InboxItems>)AppStates.Contents["InboxGINEditQueue"];
            //    }
            //    else
            //    {
            //        MyInBox = this.GetFormXML();
            //        AppStates.Contents.Add("InboxGINEditQueue", MyInBox);
            //        return (List<InboxItems>)AppStates.Contents["InboxGINEditQueue"];
            //    }
            //}
            //return null;


        }
        private List<InboxItems> GetFormXML()
        {
            List<InboxItems> MyInBox = new List<InboxItems>();
            string url = HttpContext.Current.Request.PhysicalApplicationPath + this.xmlPath;
            FileStream fs = null;
            fs = File.OpenRead(url);
            XmlReader reader = XmlReader.Create(fs);
            if (reader == null)
            {
                throw new Exception("Invalid XML Path");

            }
            try
            {
                using (reader)
                {
                    InboxItems obj = new InboxItems();
                    List<TransactionCode> lstTran = new List<TransactionCode>();
                    lstTran = new List<TransactionCode>();
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name == "InboxItem")
                                {
                                    obj.Name = reader.GetAttribute("Name");
                                }
                                if (reader.Name == "TransactionCode")
                                {
                                    TransactionCode oBjTr = new TransactionCode();
                                    oBjTr.FlowName = reader.GetAttribute("FlowName");
                                    oBjTr.StepName = reader.GetAttribute("StepName");
                                    oBjTr.Step = reader.GetAttribute("Step");
                                    oBjTr.Right = reader.GetAttribute("Right");
                                    lstTran.Add(oBjTr);
                                }
                                obj.TransactionCodeList = lstTran;


                                break;
                            case XmlNodeType.EndElement:
                                if (reader.Name == "InboxItem")
                                {
                                    MyInBox.Add(obj);
                                    obj = new InboxItems();
                                    lstTran = new List<TransactionCode>();
                                }
                                else if (reader.Name == "TransactionCode")
                                {

                                }
                                break;
                            case XmlNodeType.Text:
                                break;
                            case XmlNodeType.CDATA:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Inbox Item Exception.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();

                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }



            return MyInBox;
        }
        public InboxRowGrid GetGrantedInBoxItems()
        {
            List<InboxItems> MyInBoxGranted = new List<InboxItems>();

            InboxItems objGrantedInboxItem = new InboxItems();
            List<InboxItems> l = this.GetFormXML();
            MyInBoxGranted = objGrantedInboxItem.GetGrantedInboxItems(l);
            InboxRowGrid dictInboxRowGrid = new InboxRowGrid();
            foreach (InboxItems i in MyInBoxGranted)
            {
                if (!(dictInboxRowGrid.InboxRow.ContainsKey(i.Name)))
                {
                    dictInboxRowGrid.InboxRow.Add(i.Name, new List<string>());
                }
                foreach (TransactionCode tranCode in i.TransactionCodeList)
                {
                    dictInboxRowGrid.InboxRow[i.Name].Add(tranCode.StepName);
                }

            }




            return dictInboxRowGrid;


        }
        public InboxItems SearchByInboxItemName(string InboxItemName)
        {
            List<TransactionCode> tranCodelst = new List<TransactionCode>();
            InboxItems searchItem = new InboxItems();
            searchItem.Name = InboxItemName;
            string url = HttpContext.Current.Request.PhysicalApplicationPath + this.xmlPath;
            StreamReader sr = new StreamReader(url);
            XDocument doc = XDocument.Load(sr);
            var records = from InboxItem in doc.Root.Elements("InboxItem")
                          where (string)InboxItem.Attribute("Name") == InboxItemName
                          select InboxItem;
            foreach (var i in records)
            {
                var tranCode = from tran in i.Elements("TransactionCode")
                               select tran;
                foreach (var x in tranCode)
                {
                    TransactionCode objTrC = new TransactionCode();
                    objTrC.FlowName = x.Attribute("FlowName").Value;
                    objTrC.Step = x.Attribute("Step").Value;
                    objTrC.StepName = x.Attribute("StepName").Value;
                    objTrC.Right = x.Attribute("Right").Value;
                    tranCodelst.Add(objTrC);
                }
                searchItem.TransactionCodeList = tranCodelst;
            }
            sr.Close();
            InboxItems objGrantedItems = new InboxItems();
            searchItem = objGrantedItems.GetGrantedInboxItem(searchItem);
            return searchItem;
        }

    }
    public class TransactionDetailItems
    {
        public string DisplayField;
        public string KeyField;
        public string TransactionNumber;
        public TransactionDetailItems(string display, string key, string tranNo)
        {
            this.DisplayField = display;
            this.KeyField = key;
            this.TransactionNumber = tranNo;
        }
    }

}
