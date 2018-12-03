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
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.DAL;



namespace WarehouseApplication.BLL
{
    public class GradingByBLL : GeneralBLL
    {
        #region Fields
        private Guid _Id;
        private Guid _GradingId;
        private Guid _UserId;
        private int _Status;
        private bool isSupervisor;
        private string _GraderName;



        #endregion
        #region Properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Guid GradingId
        {
            get { return _GradingId; }
            set { _GradingId = value; }
        }
        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public bool IsSupervisor
        {
            get { return isSupervisor; }
            set { isSupervisor = value; }
        }
        public string GraderName
        {
            get { return _GraderName; }
            set { _GraderName = value; }
        }
        #endregion

        //oublic Functions
        public bool Add(Guid Id, List<GradingByBLL> list, SqlTransaction tran)
        {
            if (list.Count > 0)
            {
                try
                {
                    string strAt = "";
                    foreach (GradingByBLL i in list)
                    {
                        bool isSaved = false;
                        i.GradingId = Id;
                        isSaved = GradingByDAL.InsertGraders(i, tran);
                        if (isSaved == false)
                        {
                            return false;
                        }
                        else
                        {
                            strAt += "[(Id-" + i.Id.ToString() + "),(GradingId-" + i.GradingId.ToString() + "),(UserId-" +
                                i.UserId.ToString() + "),(Status-" + i.Status.ToString() + "),(isSupervisor-" + isSupervisor.ToString() + "),(CreatedBy"
                                + UserBLL.GetCurrentUser().ToString() + ")] ; ";

                        }

                    }
                    int at = -1;
                    AuditTrailBLL objAt = new AuditTrailBLL();
                    at = objAt.saveAuditTrailStringFormat("New Data added", strAt, WFStepsName.GenerateGradingCode.ToString(), UserBLL.GetCurrentUser(), "Add Grader");
                    if (at == 1)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    new Exception("Unable to Add Graders.");
                }

            }
            return false;

        }
        public List<GradingByBLL> GetByGradingId(Guid GradingId)
        {
            List<GradingByBLL> listComplete = null; ;
            List<GradingByBLL> list = new List<GradingByBLL>();
            list = GradingByDAL.GetSupervisorByGradingId(GradingId);

            EmployeeAttendanceBLL objEmp = new EmployeeAttendanceBLL();
            List<UserBLL> empList = new List<UserBLL>();
            empList = UserRightBLL.GetUsersWithRight("Grader");

            if (empList != null && list != null)
            {
                if (empList.Count > 0 && list.Count > 0)
                {
                    var q = from Graders in list
                            join UserDetail in empList on Graders.UserId equals UserDetail.UserId
                            select new { Graders.IsSupervisor, UserDetail.FullName };
                    listComplete = new List<GradingByBLL>();
                    foreach (var i in q)
                    {

                        GradingByBLL o = new GradingByBLL();
                        o.isSupervisor = i.IsSupervisor;
                        o.GraderName = i.FullName;
                        listComplete.Add(o);
                    }
                }

            }



            return listComplete;
        }
        public SqlDataReader GetGradersByGradingIdDataReader(Guid GradingId, SqlConnection conn)
        {
            SqlDataReader reader = null;

            reader = GradingByDAL.GetGradersByGradingIdDataReader(GradingId, conn);
            return reader;
        }
        public string GetSupGraderNameByGradingId(Guid GradingId)
        {

            List<GradingByBLL> list = null;
            list = GradingByDAL.GetSupervisorByGradingId(GradingId);
            if (list != null)
            {
                if (list.Count() == 1)
                {
                    GradingByBLL o = list[0];
                    return UserRightBLL.GetUserNameByUserId(o.UserId);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static bool IsNumberofGraderAcceptable(int count, Guid CommodityId)
        {
            int MinNoGraders = Utility.GetNoGradersByCommodity(CommodityId);
            if (MinNoGraders == -1)
            {
                return true;
            }
            else
            {
                if (MinNoGraders > count)
                    return false;
                else
                    return true;
            }

        }


    }
}
