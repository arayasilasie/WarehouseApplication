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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using WarehouseApplication.BLL;
using Microsoft.ApplicationBlocks.Data;

namespace WarehouseApplication.DAL
{
    public class EmployeeAttendanceDAL
    {
        private EmployeeAttendanceBLL _employeeAttendance;
        public EmployeeAttendanceBLL EmployeeAttendance 
        {
            get
            {
                return this._employeeAttendance;
            }
            set
            {
                this._employeeAttendance = value;
            }
        }
        public static bool Insert(EmployeeAttendanceBLL obj)
        {
            int AffectedRows = 0;
            string strSql = "spInseretEmployeeAttendance";
            try
            {
                SqlParameter[] arPar = new SqlParameter[5];

                arPar[0] = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
                arPar[0].Value = obj.UserId;

                arPar[1] = new SqlParameter("@RoleId", SqlDbType.UniqueIdentifier);
                arPar[1].Value = obj.RoleId;

                arPar[2] = new SqlParameter("@WarehouseId", SqlDbType.UniqueIdentifier);
                arPar[2].Value = obj.WarehouseId;

                arPar[3] = new SqlParameter("@UserInDateTime", SqlDbType.DateTime);
                arPar[3].Value = obj.UserInDateTime;

                arPar[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                arPar[4].Value = true;

                arPar[5] = new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier);
                arPar[5].Value = UserBLL.GetCurrentUser();

                SqlConnection conn = Connection.getConnection();
                AffectedRows = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, strSql, arPar);
                conn.Close();
                if (AffectedRows == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            



        }
        public static List<EmployeeAttendanceBLL> GetAllEmployees(Guid WarehoouseId , Guid RoleId)
        {
            List<EmployeeAttendanceBLL> list = new List<EmployeeAttendanceBLL>();
            EmployeeAttendanceBLL obj = new EmployeeAttendanceBLL();
            obj.UserId = new Guid("941b7fbf-6f34-47ff-990c-0a2e540eb0d4");
            obj.EmployeeName = "Employee 1 ";
            obj.IsActive = true;

            EmployeeAttendanceBLL obj2 = new EmployeeAttendanceBLL();
            obj2.UserId = new Guid("8941c759-2572-4bd0-a3a9-85def1fafb18");
            obj2.EmployeeName = "Employee 2 ";
            obj2.IsActive = true;

            EmployeeAttendanceBLL obj3 = new EmployeeAttendanceBLL();
            obj3.UserId = new Guid("9be3e763-9775-492e-9ed6-d257056a226d");
            obj3.EmployeeName = "Employee 3 ";
            obj3.IsActive = true;

            list.Add(obj);
            list.Add(obj2);
            list.Add(obj3);

            return list;

        }
        public static EmployeeAttendanceBLL GetEmployeeById(Guid EmployeeId)
        {
            // TODO : intgerate with SM
            EmployeeAttendanceBLL obj = new EmployeeAttendanceBLL();
            obj.EmployeeName = "Employee 1";
            return obj;
        }

    }
}
