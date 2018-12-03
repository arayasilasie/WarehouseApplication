using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Timers;
using System.Data;

namespace WarehouseApplication.DALManager
{
    [Flags]
    public enum FilterConditionType
    {
        Comparison = 1,
        Range = 2,
        Lookup = 4
    }

    [Serializable]
    public class DataFilterCondition
    {
        private DataFilterParameter parameter;
        private FilterConditionType conditionType;
        private string conditionOperator;
        private string leftOperand;
        private string rightOperand;

        public DataFilterCondition() { }
        public DataFilterCondition(
            DataFilterParameter parameter,
            FilterConditionType conditionType,
            string conditionOperator,
            string leftOperand,
            string rightOperand)
        {
            this.parameter = parameter;
            this.conditionType = conditionType;
            this.conditionOperator = conditionOperator;
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public DataFilterCondition(
            DataFilterParameter parameter,
            FilterConditionType conditionType,
            string conditionOperator,
            string operand)
            : this(parameter, conditionType, conditionOperator, operand, null) { }

        public DataFilterParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public FilterConditionType ConditionType
        {
            get { return conditionType; }
            set { conditionType = value; }
        }

        public string Operator
        {
            get { return conditionOperator; }
            set { conditionOperator = value; }
        }

        public string LeftOperand
        {
            get { return leftOperand; }
            set { leftOperand = value; }
        }

        public string RightOperand
        {
            get { return rightOperand; }
            set { rightOperand = value; }
        }
    }

    [Serializable]
    public class DataFilterParameter
    {
        private string name;
        private string caption;
        private Type type;
        private string defaultValue;
        private FilterConditionType conditionType;

        public DataFilterParameter() { }
        public DataFilterParameter(string name, string caption, Type type, string defaultValue, FilterConditionType conditionType)
        {
            this.name = name;
            this.caption = caption;
            this.type = type;
            this.defaultValue = defaultValue;
            this.conditionType = conditionType;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
        public FilterConditionType ConditionType
        {
            get { return conditionType; }
            set { conditionType = value; }
        }
    }
    public class LockExpiryEventArgs : EventArgs
    {
        private Guid updateLockId;

        public LockExpiryEventArgs(Guid updateLockId)
        {
            this.updateLockId = updateLockId;
        }

        public Guid UpdateLockId
        {
            get { return updateLockId; }
        }
    }

    public delegate void LockExpiryEvent(object sender, LockExpiryEventArgs e);

    public class InvalidUpdateRequest : Exception
    {
        public InvalidUpdateRequest() : base() { }
        public InvalidUpdateRequest(string message) : base(message) { }
    }

    public interface IDataIdentifier
    {
        XmlDocument Preview { get; }
        object ID { get; }
    }

    public interface IDataFilter
    {
        String TemplateName { get; }
        String GetFilterExpression(string template);
        void SetCondition(DataFilterCondition condition);
        List<DataFilterParameter> Parameters { get; }
    }

    public interface ILookupSource
    {
        IDictionary<object, string> GetLookup(string lookupName);
        IDictionary<string, object> GetInverseLookup(string lookupName);
    }

    public interface IDataFilterConditionsStore
    {
        List<DataFilterCondition> Conditions { get; }
    }

    public interface IDataFilterFormatter
    {
        string GetFilterExpression(string temaplate, IDataFilterConditionsStore store);
    }

    [Serializable]
    public class DataUpdateLock
    {
        private Guid id;
        IDataIdentifier data;
        private DateTime expiresAt;
        private Timer expiryTimer;

        public DataUpdateLock(Guid id, IDataIdentifier data, DateTime expiresAt)
        {
            this.id = id;
            this.data = data;
            this.expiresAt = expiresAt;
            expiryTimer = new Timer();
            expiryTimer.Interval = (expiresAt - DateTime.Now).Milliseconds;
            expiryTimer.Enabled = true;
            expiryTimer.Elapsed += new ElapsedEventHandler(expiryTimer_Elapsed);
        }

        private void expiryTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnLockExpired(this, new LockExpiryEventArgs(id));
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public IDataIdentifier Data
        {
            get { return data; }
            set { data = value; }
        }

        public DateTime ExpiresAt
        {
            get { return expiresAt; }
            set { expiresAt = value; }
        }

        public event LockExpiryEvent OnLockExpired;
    }

    public interface IDALManager
    {
        List<IDataIdentifier> GetCatalog();
        List<IDataIdentifier> GetCatalog(IDataFilter filter);
        XmlDocument OpenData(IDataIdentifier id);
        XmlDocument OpenData(IDataIdentifier id, IDataFilter filter);
        DataUpdateLock GetUpdateLock(IDataIdentifier id);
        void UpdateData(XmlDocument updatedData, DataUpdateLock dataUpdateLock);
        IDataFilter GetCatalogFilter();
        IDataFilter GetCatalogFilter(string templateName);
        IDataFilter GetDataFilter();
        IDataFilter GetDataFilter(string templateName);
    }

    public interface ISource
    {
        XmlDocument OpenData(IDataIdentifier id);
        XmlDocument OpenData(IDataIdentifier id, IDataFilter filter);
        void UpdateData(XmlDocument updatedData, IDataIdentifier id);

    }

    public interface ICatalogSource : ISource
    {
        List<IDataIdentifier> GetCatalog();
        List<IDataIdentifier> GetCatalog(IDataFilter filter);
        IDataFilter GetFilter();
        IDataFilter GetFilter(string templateName);
    }

    public interface IPersistenceOperator
    {
        XmlReader Open(Dictionary<string, object> parameterSource);
        int Persist(object dataSource);
    }
}
