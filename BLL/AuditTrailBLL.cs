using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;


namespace WarehouseApplication.BLL
{
    //TODO - Create a list ;
    public class AuditTrailComparer
    {
        public static string Compare(object objOld, object objNew)
        {

            // check that they are of the same type and the type is the same as as theType.
            // if not throw an excpetion.
            Type theType = objOld.GetType();
            string obj1TypeName, obj2TypeName = "";
            string strOldValue = "";
            string strNewValue = "";
            obj1TypeName = objOld.GetType().Name;
            obj2TypeName = objNew.GetType().Name;
            if (obj1TypeName != obj2TypeName)
            {
                throw new Exception("Can not compare two Different types");
            }
            else
            {
                if (obj1TypeName != theType.Name)
                {
                    throw new Exception("The object Type and the type provided are differnet");
                }
            }
            try
            {
                foreach (PropertyInfo p in theType.GetProperties())
                {
                    string t = p.Name;
                    object valObjectOld = null;
                    object valObjectNew = null;
                    bool isOk = false;
                    valObjectOld = p.GetValue(objOld, null);
                    valObjectNew = p.GetValue(objNew, null);
                    // check if type is Nullbale 
                    Type dType = p.PropertyType;
                    if (isNullable(dType) == true)
                    {
                        dType = GetUnderlyingType(dType);
                    }
                    if (valObjectOld == null && valObjectNew != null)// Value Modified
                    {

                        string Valold = "";
                        string VallNew = "";
                        if (valObjectOld != null)
                        {
                            Valold = valObjectOld.ToString();
                        }

                        if (valObjectNew != null)
                        {
                            VallNew = valObjectNew.ToString();
                        }
                        strOldValue += "(" + p.Name.ToString() + "-" + Valold + "),";
                        strNewValue += "(" + p.Name.ToString() + "-" + VallNew + "),";
                    }
                    else if (valObjectOld == null && valObjectNew == null)
                    {
                    }
                    else
                    {
                        isOk = ValueComparer(valObjectOld, valObjectNew, dType);
                        if (isOk != true)// Value Modified
                        {
                            string Valold = "";
                            string VallNew = "";
                            if (valObjectOld != null)
                            {
                                Valold = valObjectOld.ToString();
                            }

                            if (valObjectNew != null)
                            {
                                VallNew = valObjectNew.ToString();
                            }


                            strOldValue += "(" + p.Name.ToString() + "-" + Valold + "),";
                            strNewValue += "(" + p.Name.ToString() + "-" + VallNew + "),";

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (strOldValue != "" && strNewValue != "")
            {
                return strOldValue + "*" + strNewValue;
            }
            else
            {
                return "";
            }
        }
        public static string Compare(object objNew)
        {

            // check that they are of the same type and the type is the same as as theType.
            // if not throw an excpetion.
            Type theType = objNew.GetType();
            string strNewValue = "";
            foreach (PropertyInfo p in theType.GetProperties())
            {
                string t = p.Name;

                object valObjectNew = null;


                valObjectNew = p.GetValue(objNew, null);
                // check if type is Nullbale 
                Type dType = p.PropertyType;
                if (valObjectNew != null)
                {
                    strNewValue += "(" + p.Name.ToString() + "-" + valObjectNew.ToString() + "),";
                }

            }
            return strNewValue;

        }

        private static bool ValueComparer(object obj1, object obj2, Type DataType)
        {
            bool isValid = false;
            string DataTypeName = "";
            DataTypeName = DataType.Name.ToString();
            if (obj1 == null && obj2 != null)
            {
                return false;
            }
            else if (obj2 == null && obj1 != null)
            {
                return false;
            }



            switch (DataTypeName)
            {
                case "Int32":
                    Int32 val1, val2;
                    val1 = (Int32)obj1;
                    val2 = (Int32)obj2;
                    if (val1 == val2)
                    {
                        isValid = true;
                    }
                    break;
                case "String":
                    string strval1, strval2;
                    strval1 = (string)obj1;
                    strval2 = (string)obj2;
                    if (string.IsNullOrEmpty(strval1) == true)
                    {
                        strval1 = "";
                    }
                    if (string.IsNullOrEmpty(strval2) == true)
                    {
                        strval2 = "";
                    }

                    if (strval1.ToUpper() == strval2.ToUpper())
                    {
                        isValid = true;
                    }
                    break;
                case "Guid":
                    //TODO : Check ID 
                    //Guid guidval1 = new Guid(obj1.ToString());
                    //Guid guidval2 = new Guid(obj2.ToString());
                    //if (guidval1.CompareTo(guidval2) == 0)
                    //{
                    //    isValid = true;
                    //}
                    //break;
                    return false;
                case "DateTime":
                    DateTime dtVal1 = (DateTime)obj1;
                    DateTime dtVal2 = (DateTime)obj2;
                    if (dtVal1.CompareTo(dtVal2) == 0)
                    {
                        isValid = true;
                    }
                    break;
                case "float":
                    float fltVal1 = float.Parse(obj1.ToString());
                    float fltVal2 = float.Parse(obj2.ToString());
                    if (fltVal1.CompareTo(fltVal2) == 0)
                    {
                        isValid = true;
                    }
                    break;
                case "Boolean":
                    Boolean bltVal1 = Boolean.Parse(obj1.ToString());
                    Boolean bltVal2 = Boolean.Parse(obj2.ToString());
                    if (bltVal1.CompareTo(bltVal2) == 0)
                    {
                        isValid = true;
                    }
                    break;
                default:
                    return false;
            }
            return isValid;
        }
        private static bool isNullable(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));

        }
        private static Type GetUnderlyingType(Type NullbaleType)
        {
            Type theType;
            theType = Nullable.GetUnderlyingType(NullbaleType);
            return theType;
        }

    }
    public class AuditTrailBLL
    {
        public string FieldName;
        public string ModuleCode;
        public Guid UserGuid;
        public string oldValue;
        public string newValue;

        // To save audit trail
        // 1- saved 
        // 0 - no changes made
        //-1 fail
        /// <summary>
        /// Automatically logs audit Trail identifiying changes made.Requires public properties.
        /// </summary>
        /// <param name="objOld">The Original Object Before Update </param>
        /// <param name="objNew">The Object with Modidfied Values </param>
        /// <param name="ModuleName">The name of the application module</param>
        /// <param name="UserId">User Id of the persom who make the modification</param>
        /// <param name="BussinessProcess">The Name of Bussiness proccess e.g Update Driver Information</param>
        /// <returns>1-if successfull , 0 - when no changes are made , -1 when fail</returns>
        public int saveAuditTrail(object objOld, object objNew, string ModuleName, Guid UserId, string BussinessProcess)
        {

            string strVal = AuditTrailComparer.Compare(objOld, objNew);

            if (strVal == "")
            {
                return 0;
            }
            else // save.
            {
                bool isSaved = false;
                string strOld = "";
                string strNew = "";
                string[] str = new string[2];
                str = strVal.Split('*');
                if (str.Count() == 2)
                {
                    strOld = BussinessProcess + str[0];
                    strNew = BussinessProcess + str[1];
                }
                ECXSecurity.ECXSecurityAccess objAT = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
                objAT.CookieContainer = new System.Net.CookieContainer();
                if (strOld == "" || strNew == "")
                {
                    return 0;
                }


                isSaved = objAT.AddAuditTrail(ModuleName, UserId, strOld, strNew);
                objAT.AuditTrailCommit();



                if (isSaved == true)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }



        }
        public int saveAuditTrail(object objNew, string ModuleName, Guid UserId, string BussinessProcess)
        {

            string strVal = AuditTrailComparer.Compare(objNew);

            if (strVal == "")
            {
                return 0;
            }
            else // save.
            {
                bool isSaved = false;
                ECXSecurity.ECXSecurityAccess objAT = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
                objAT.CookieContainer = new System.Net.CookieContainer();



                isSaved = objAT.AddAuditTrail(ModuleName, UserId, strVal, "New Record Added");
                objAT.AuditTrailCommit();



                if (isSaved == true)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }



        }
        public int saveAuditTrailStringFormat(string strOld, string strNew, string ModuleName, Guid UserId, string BussinessProcess)
        {




            bool isSaved = false;
            ECXSecurity.ECXSecurityAccess objAT = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
            objAT.CookieContainer = new System.Net.CookieContainer();



            isSaved = objAT.AddAuditTrail(ModuleName, UserId, strOld, strNew);
            objAT.AuditTrailCommit();



            if (isSaved == true)
            {
                return 1;
            }
            else
            {
                return -1;
            }





        }
        public void RoleBack()
        {
            ECXSecurity.ECXSecurityAccess objAT = new WarehouseApplication.ECXSecurity.ECXSecurityAccess();
            objAT.AuditTrailRollback();
        }



    }
}
