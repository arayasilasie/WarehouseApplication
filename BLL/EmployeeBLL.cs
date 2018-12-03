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
using WarehouseApplication.DAL;

namespace WarehouseApplication.BLL
{
    public class EmployeeAttendanceBLL
    {
    #region Feilds
        private Guid  _userId ;
        private DateTime _userInDateTime;
        private DateTime _userOutDateTime;
        private Guid  _roleId;
        private Guid _warehouseId;
        private bool _isActive;
        private Guid _createdBy;
        private DateTime _createdDate;
        private Guid _lastModifiedBy;
        private DateTime _lastModifiedDate;
        private string _employeeName;
    #endregion
    #region Properties
        public Guid UserId 
        { 
            get
            {
                return this._userId;
            }
            set
            {
                this._userId = value;
            }
        }
        public DateTime UserInDateTime
        {
            get
            {
                return this._userInDateTime;
            }
            set
            {
                this._userInDateTime = value;
            }
        }

        public DateTime UserOutDateTime
        {
            get
            {
                return this._userOutDateTime;
            }
            set
            {
                this._userOutDateTime = value;
            }
        }
        public Guid RoleId
        {
            get
            {
                return this._roleId;
            }
            set
            {
                this._roleId = value;
            }
        }
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                this._isActive = value;
            }
        }
        public Guid WarehouseId
        {
            get
            {
                return this._warehouseId;
            }
            set
            {
                this._warehouseId = value;
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
            set
            {
                this._createdDate = value; ;
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
        public string EmployeeName
        {
            get
            {
                return this._employeeName;
            }
            set
            {
                this._employeeName = value;
            }
        }

    #endregion

        public bool InsertEmployeeAttendance()
        {
            bool isSaved = false;
            try
            {
                isSaved = EmployeeAttendanceDAL.Insert(this);
            }
            catch(Exception ex )
            {
                throw ex;
            }
            return isSaved;
        }
        /// <summary>
        /// Updates the sta
        /// </summary>
        /// <returns></returns>
        public bool SetToInactive()
        {
            return true;
        }
        public bool CheckIn()
        {
            return true;
        }
        public bool CheckOut()
        {
            return true;
        }
        /// <summary>
        /// Returns List of Active Emplyees of the Specified 
        /// </summary>
        /// <param name="WarehouseId">Id of the current Warehouse</param>
        /// <param name="RoleId">Role Id</param>
        /// <returns>EmployeeAttendanceBL List of Active Employees</returns>
        public List<EmployeeAttendanceBLL> GetActiveEmployeesByRole(Guid WarehouseId, Guid RoleId)
        {
            List<EmployeeAttendanceBLL> list = new List<EmployeeAttendanceBLL>();
            list = EmployeeAttendanceDAL.GetAllEmployees(WarehouseId, RoleId);
            return list;
        }
        public List<EmployeeAttendanceBLL> GetAllEmployeesByRole(Guid WarehouseId, Guid RoleId)
        {
            List<EmployeeAttendanceBLL> list = new List<EmployeeAttendanceBLL>();
            list = EmployeeAttendanceDAL.GetAllEmployees(WarehouseId, RoleId);
            return list;
        }


        public EmployeeAttendanceBLL GetEmployee(Guid Id)
        {
            return EmployeeAttendanceDAL.GetEmployeeById(Id);
        }

        
    }
}
