using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Text;
using System.IO;

namespace WarehouseApplication.DALManager
{
    public class Identifier : IDataIdentifier
    {
        private Guid id;
        private XmlDocument preview;

        public Identifier(Guid id, XmlDocument preview)
        {
            this.id = id;
            this.preview = preview;
        }

        #region IDataIdentifier Members

        public XmlDocument Preview
        {
            get { return preview; }
        }

        public object ID
        {
            get { return id; }
        }
        #endregion
    }

    [Serializable]
    public class XPathTemplateFilter : IDataFilter, IDataFilterConditionsStore
    {
        private string templateName;
        private List<DataFilterParameter> parameters;
        private List<DataFilterCondition> conditions;
        private IDataFilterFormatter formatter;

        public XPathTemplateFilter(SQLXmlDALManagerFactory.NamedXPathTemplate template)
        {
            this.templateName = template.Key;
            parameters = new List<DataFilterParameter>();
            conditions = new List<DataFilterCondition>();
            foreach (SQLXmlDALManagerFactory.XPathTemplateParameter parameter in template.Parameters)
            {
                this.parameters.Add(
                    new DataFilterParameter(parameter.Name, parameter.Caption, Type.GetType(parameter.Type), parameter.DefaultValue, parameter.ConditionType));
            }
            formatter = CreateFormatter(template.Key, template.FilterFormatter);
        }

        #region IDataFilter Members

        public String GetFilterExpression(string template)
        {
            return formatter.GetFilterExpression(template, this);
        }

        public string TemplateName
        {
            get { return templateName; }
        }

        public void SetCondition(DataFilterCondition condition)
        {
            conditions.Add(condition);
        }

        public List<DataFilterParameter> Parameters
        {
            get { return parameters; }
        }

        #endregion

        #region IDataFilterConditionsStore Members

        public List<DataFilterCondition> Conditions
        {
            get { return conditions; }
        }

        #endregion

        #region Helper Methods
        private IDataFilterFormatter CreateFormatter(string templateName, string typeInfo)
        {
            /* For the time being
            //System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            string[] typeInfoParts = typeInfo.Split(new char[] { ',' });
            if (typeInfoParts.Length < 5)
            {
                throw new InvalidOperationException(string.Format("Invalid {0} DataFilter Configuration Setting", templateName));
            }
            string assemblyName = string.Join(", ", typeInfoParts, 1, 4);
            IDataFilterFormatter formatterInstance;
            try
            {
                Assembly filterAssembly = Assembly.Load(new AssemblyName(assemblyName));
                formatterInstance = (IDataFilterFormatter)filterAssembly.CreateInstance(typeInfoParts[0]);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Invalid {0} DataFilter Configuration Setting", templateName));
            }
            return formatterInstance;
             */
            return new XPathConditionFormatter();
        }
        #endregion
    }

    [Serializable]
    public class XPathConditionFormatter : IDataFilterFormatter
    {
        #region IDataFilterFormatter Members

        public string GetFilterExpression(string template, IDataFilterConditionsStore store)
        {
            foreach (DataFilterCondition condition in store.Conditions)
            {
                if (condition.ConditionType == FilterConditionType.Comparison)
                {
                    template = template.Replace("__OP__", string.Format("{0}", condition.Operator));
                    template = template.Replace(string.Format("{0}", condition.Parameter.Name), condition.LeftOperand);
                }
            }
            return template;
        }

        #endregion
    }

    [Serializable]
    public class SQLTemplateFilter : IDataFilter, IDataFilterConditionsStore
    {
        private string templateName;
        private List<DataFilterParameter> parameters;
        private List<DataFilterCondition> conditions;
        private IDataFilterFormatter formatter;

        public SQLTemplateFilter(SQLXmlDALManagerFactory.NamedXPathTemplate template)
        {
            this.templateName = template.Key;
            parameters = new List<DataFilterParameter>();
            conditions = new List<DataFilterCondition>();
            foreach (SQLXmlDALManagerFactory.XPathTemplateParameter parameter in template.Parameters)
            {
                this.parameters.Add(
                    new DataFilterParameter(parameter.Name, parameter.Caption, Type.GetType(parameter.Type), parameter.DefaultValue, parameter.ConditionType));
            }
            formatter = CreateFormatter(template.Key, template.FilterFormatter);
        }

        #region IDataFilter Members

        public String GetFilterExpression(string template)
        {
            return formatter.GetFilterExpression(template, this);
        }

        public string TemplateName
        {
            get { return templateName; }
        }

        public void SetCondition(DataFilterCondition condition)
        {
            conditions.Add(condition);
        }

        public List<DataFilterParameter> Parameters
        {
            get { return parameters; }
        }

        #endregion

        #region IDataFilterConditionsStore Members

        public List<DataFilterCondition> Conditions
        {
            get { return conditions; }
        }

        #endregion

        #region Helper Methods
        private IDataFilterFormatter CreateFormatter(string templateName, string typeInfo)
        {
            string[] typeInfoParts = typeInfo.Split(new char[] { ',' });
            if (typeInfoParts.Length < 5)
            {
                throw new InvalidOperationException(string.Format("Invalid {0} DataFilter Configuration Setting", templateName));
            }
            string assemblyName = string.Join(", ", typeInfoParts, 1, 4);
            IDataFilterFormatter formatterInstance;
            try
            {
                Assembly filterAssembly = Assembly.Load(new AssemblyName(assemblyName));
                formatterInstance = (IDataFilterFormatter)filterAssembly.CreateInstance(typeInfoParts[0]);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Invalid {0} DataFilter Configuration Setting", templateName),ex);
            }
            return formatterInstance;
            /* For the time being
             */
            //return new SQLConditionFormatter();
        }
        #endregion
    }


    [Serializable]
    public class SQLConditionFormatter : IDataFilterFormatter
    {
        #region IDataFilterFormatter Members

        public string GetFilterExpression(string template, IDataFilterConditionsStore store)
        {
            List<SQLDataFilterParameter> parameters = new List<SQLDataFilterParameter>();
            foreach (DataFilterCondition condition in store.Conditions)
            {
                SQLDataFilterParameter[] conditionParameters = GetParameters(condition);
                for (int i = 0; i < conditionParameters.Length; i++)
                {
                    parameters.Add(conditionParameters[i]);
                }
            }
            store.Conditions.Clear();
            StringBuilder sb = new StringBuilder();
            XmlSerializer s = new XmlSerializer(typeof(List<SQLDataFilterParameter>));
            s.Serialize(new StringWriter(sb), parameters);
            return sb.ToString();
        }

        #endregion

        protected virtual SQLDataFilterParameter[] GetParameters(DataFilterCondition condition)
        {
            SQLDataFilterParameter[] parameters = null;
            if (condition.ConditionType == FilterConditionType.Range)
            {
                parameters = new SQLDataFilterParameter[2];
            }
            else
            {
                parameters = new SQLDataFilterParameter[1];
            }
            string singleParameterName = ((condition.ConditionType == FilterConditionType.Range) ? condition.Parameter.Name + "Before" : condition.Parameter.Name);
            parameters[0] = new SQLDataFilterParameter(
                    singleParameterName,
                    ConvertToDbType(condition.Parameter.Type),
                    1000,
                    ParameterDirection.Input,
                    53,
                    4,
                    condition.LeftOperand,
                    condition.Parameter.Type.FullName);
            if (condition.ConditionType == FilterConditionType.Range)
            {
                parameters[1] = new SQLDataFilterParameter(
                    condition.Parameter.Name + "After",
                    ConvertToDbType(condition.Parameter.Type),
                    1000,
                    ParameterDirection.Input,
                    53,
                    4,
                    condition.RightOperand,
                    condition.Parameter.Type.FullName);
            }
            return parameters;
        }

        private DbType ConvertToDbType(Type type)
        {
            switch (type.FullName)
            {
                case "System.Int32": return DbType.Int32;
                case "System.Guid": return DbType.Guid;
                case "System.Decimal": return DbType.Decimal;
                case "System.DateTime": return DbType.DateTime;
                case "System.String": return DbType.String;
                default: return DbType.String;
            }

        }
    }

}