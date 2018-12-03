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
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace WarehouseApplication.DALManager
{
    public class PersistenceTransactionFactory
    {
        private static PersistenceTransactionFactory singleton;
        public static PersistenceTransaction CreatePersistenceTransaction()
        {
            if (singleton == null)
            {
                singleton = new PersistenceTransactionFactory();
            }
            return singleton.transaction;
        }

        private PersistenceTransaction transaction;

        private PersistenceTransactionFactory()
        {
            string dataAccessConfigurationFile = HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DataAccessConfigurationFile"];
            Stream stream = File.OpenRead(dataAccessConfigurationFile);
            XmlSerializer s = new XmlSerializer(typeof(PersistenceTransaction));
            transaction = (PersistenceTransaction)s.Deserialize(stream);
            if ((transaction.ConnectionString == null) || (transaction.ConnectionString == string.Empty))
            {
                transaction.ConnectionString = ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;
            }
        }
    }

    [XmlRoot]
    public class PersistenceTransaction
    {
        private string connectionString;
        private List<PersistenceOperator> persistenceOperators = new List<PersistenceOperator>();
        public PersistenceTransaction() { }

        [XmlAttribute]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        [XmlArray("PersistenceOperators")]
        [XmlArrayItem("PersistenceOperator")]
        public List<PersistenceOperator> PeristenceOperators
        {
            get { return persistenceOperators; }
        }

        public SqlTransaction Persist(Queue<object> persistableObjects, Dictionary<string, object> contextualParameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                Persist(persistableObjects, contextualParameters, transaction);
                return transaction;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                }
                throw (ex);
            }
        }

        public SqlTransaction Persist(Queue<object> persistableObjects, Dictionary<string, object> contextualParameters, SqlTransaction transaction)
        {
            try
            {
                while (persistableObjects.Count > 0)
                {
                    object persistableObject = persistableObjects.Dequeue();
                    var pos = from po in persistenceOperators
                              where po.SourceObjectName == persistableObject.GetType().FullName
                              select po;
                    PersistenceOperator targetPO = pos.ElementAt(0);
                    targetPO.AttachedTransaction = transaction;
                    if (targetPO.Persist(persistableObject, contextualParameters) == 0)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception)
                        {
                        }
                        throw new Exception(string.Format("A {0} couldnot be persisted", targetPO.SourceObjectName));
                    }
                }
                return transaction;
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw (ex);
            }
        }

        public SqlTransaction Persist(Queue<object[]> persistableObjects, Dictionary<string, object> contextualParameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                while (persistableObjects.Count > 0)
                {
                    object[] persistableObject = persistableObjects.Dequeue();
                    var pos = from po in persistenceOperators
                              where po.Name == (string)persistableObject[1]
                              select po;
                    PersistenceOperator targetPO = pos.ElementAt(0);
                    targetPO.AttachedTransaction = transaction;
                    if (targetPO.Persist(persistableObject[0], contextualParameters) == 0)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception)
                        {
                        }
                        throw new Exception(string.Format("A {0} couldnot be persisted", targetPO.SourceObjectName));
                    }
                }
                return transaction;
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw (ex);
            }
            finally
            {
                //transaction.Dispose();
                //connection.Close();
            }
        }

        public XmlDocument Open(string operatorName, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            XmlDocument document = new XmlDocument();
            SqlTransaction transaction = connection.BeginTransaction();
            XmlReader reader = null;
            try
            {
                var pos = from po in persistenceOperators
                          where po.Name == operatorName
                          select po;
                PersistenceOperator targetPO = pos.ElementAt(0);
                targetPO.AttachedTransaction = transaction;
                reader = targetPO.Open(parameters);
                document.Load(reader);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw (ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return document;
        }
    }

    public class PersistenceOperator
    {
        private string sourceObjectName;
        private string name;
        private SqlTransaction attachedTransaction;
        private List<PersistenceOperatorParameter> poParameters = new List<PersistenceOperatorParameter>();
        public PersistenceOperator() { }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlAttribute]
        public string SourceObjectName
        {
            get { return sourceObjectName; }
            set { sourceObjectName = value; }
        }

        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public List<PersistenceOperatorParameter> Parameters
        {
            get { return poParameters; }
        }

        [XmlIgnore]
        public SqlTransaction AttachedTransaction
        {
            get { return attachedTransaction; }
            set { attachedTransaction = value; }
        }

        #region PersistenceOperator Members

        public XmlReader Open(Dictionary<string, object> parameterSource)
        {
            SqlCommand command = new SqlCommand(name);
            command.CommandType = CommandType.StoredProcedure;
            foreach (PersistenceOperatorParameter poParameter in poParameters)
            {
                if (parameterSource.ContainsKey(poParameter.SourceName))
                {
                    command.Parameters.Add(ReadParameter(poParameter, parameterSource[poParameter.SourceName]));
                }
            }
            command.Transaction = attachedTransaction;
            command.Connection = attachedTransaction.Connection;
            return command.ExecuteXmlReader();
        }

        public int Persist(object dataSource, Dictionary<string, object> contextualParameters)
        {
            SqlCommand command = new SqlCommand(name);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter returnParameter = null;
            foreach (PersistenceOperatorParameter poParameter in poParameters)
            {
                object parameterValue = null;
                if (contextualParameters.ContainsKey(poParameter.SourceName))
                {
                    parameterValue = contextualParameters[poParameter.SourceName];
                }
                else if (poParameter.Direction != ParameterDirection.ReturnValue)
                {
                    PropertyInfo parameterSourceProperty = dataSource.GetType().GetProperty(poParameter.SourceName);
                    if (parameterSourceProperty.GetValue(dataSource, null) == null)
                    {
                        continue;
                    }
                    parameterValue = parameterSourceProperty.GetValue(dataSource, null);
                }
                SqlParameter parameter = ReadParameter(poParameter, parameterValue);
                if (parameter.Direction == ParameterDirection.ReturnValue)
                {
                    returnParameter = parameter;
                }
                else
                {
                }
                command.Parameters.Add(parameter);
            }
            command.Transaction = attachedTransaction;
            command.Connection = attachedTransaction.Connection;
            command.ExecuteNonQuery();
            return (int)returnParameter.Value;
        }

        #endregion

        private SqlParameter ReadParameter(PersistenceOperatorParameter poParameter, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + poParameter.Name;
            parameter.DbType = poParameter.DbType;
            if (!NullFinder.IsNull(poParameter.Size, "System.Int32"))
                parameter.Size = poParameter.Size;
            parameter.Direction = poParameter.Direction;
            parameter.IsNullable = true;
            if (!NullFinder.IsNull(poParameter.Precision, "System.Byte"))
                parameter.Precision = poParameter.Precision;
            if (!NullFinder.IsNull(poParameter.Scale, "System.Byte"))
                parameter.Scale = poParameter.Scale;
            if ((parameter.Direction != ParameterDirection.ReturnValue) && !NullFinder.IsNull(value, value.GetType().FullName))
            {
                parameter.Value = value;
            }
            return parameter;
        }
    }

    public class PersistenceOperatorParameter
    {
        private string name;
        private string sourceName;
        private DbType dbType = DbType.String;
        private int size = int.MinValue;
        private ParameterDirection direction = ParameterDirection.Input;
        private byte precision = byte.MinValue;
        private byte scale = byte.MinValue;

        public PersistenceOperatorParameter() { }
        public PersistenceOperatorParameter(
            string name,
            string sourceName)
        {
            this.name = name;
            this.sourceName = sourceName;
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlAttribute]
        public string SourceName
        {
            get { return sourceName; }
            set { sourceName = value; }
        }

        [XmlAttribute]
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        [XmlAttribute]
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        [XmlAttribute]
        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        [XmlAttribute]
        public byte Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        [XmlAttribute]
        public byte Scale
        {
            get { return scale; }
            set { scale = value; }
        }

    }

}
