using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Web;

namespace WarehouseApplication.DALManager
{
    public class SQLXmlDALManagerFactory
    {
        private static SQLXmlDALManagerFactory singleton = null;

        public static IDALManager CreateDALManager(string dataName)
        {
            if (singleton == null)
                singleton = new SQLXmlDALManagerFactory();
            SQLDALManagerConfiguration dalManagerConfiguration = singleton.dalManagerConfigurations[dataName];
            Dictionary<string, NamedXPathTemplate> catalogXPathTemplates = new Dictionary<string, NamedXPathTemplate>();
            foreach (NamedXPathTemplate namedTemplate in dalManagerConfiguration.SourceConfiguration.CatalogXPathTemplate)
            {
                catalogXPathTemplates.Add(namedTemplate.Key, namedTemplate);
            }
            Dictionary<string, NamedXPathTemplate> dataXPathTemplates = new Dictionary<string, NamedXPathTemplate>();
            foreach (NamedXPathTemplate namedTemplate in dalManagerConfiguration.SourceConfiguration.DataXPathTemplate)
            {
                dataXPathTemplates.Add(namedTemplate.Key, namedTemplate);
            }
            if (dalManagerConfiguration.SourceConfiguration.ConnString == null)
            {
                dalManagerConfiguration.SourceConfiguration.ConnString =
                    ConfigurationManager.ConnectionStrings["WarehouseApplicationConnectionLocal"].ConnectionString;
            }

            SQLDataCatalogSource catalogSource = new SQLDataCatalogSource(
               dalManagerConfiguration.SourceConfiguration.ConnString,
               dalManagerConfiguration.SourceConfiguration.CatalogSchemaFile,
               dalManagerConfiguration.SourceConfiguration.DataSchemaFile,
               dalManagerConfiguration.SourceConfiguration.DataRootTag,
               dalManagerConfiguration.SourceConfiguration.DataNamespaces,
               dalManagerConfiguration.SourceConfiguration.IDXPath,
               catalogXPathTemplates,
               dataXPathTemplates);
            return new SQLDALManager(catalogSource);
        }

        private Dictionary<string, SQLDALManagerConfiguration> dalManagerConfigurations;

        private SQLXmlDALManagerFactory()
        {
            string dalConfigurationfile = HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["DALConfigurationFile"];
            XmlSerializer s = new XmlSerializer(typeof(List<SQLXmlDALManagerFactory.SQLDALManagerConfiguration>));
            List<SQLXmlDALManagerFactory.SQLDALManagerConfiguration> dalConfigurations =
                (List<SQLXmlDALManagerFactory.SQLDALManagerConfiguration>)s.Deserialize(File.OpenRead(dalConfigurationfile));
            dalManagerConfigurations = new Dictionary<string, SQLDALManagerConfiguration>();
            foreach (SQLDALManagerConfiguration dalConfiguration in dalConfigurations)
            {
                dalManagerConfigurations.Add(dalConfiguration.DataName, dalConfiguration);
            }
        }

        public class XPathTemplateParameter
        {
            private string name;
            private string caption;
            private string type;
            private string defaultValue;
            private FilterConditionType conditionType;

            public XPathTemplateParameter() { }
            public XPathTemplateParameter(string name, string caption, string type, string defaultValue, FilterConditionType conditionType)
            {
                this.name = name;
                this.caption = caption;
                this.type = type;
                this.defaultValue = defaultValue;
                this.conditionType = conditionType;
            }

            public string Name { get { return name; } set { name = value; } }
            public string Caption { get { return caption; } set { caption = value; } }
            public string Type { get { return type; } set { type = value; } }
            public string DefaultValue { get { return defaultValue; } set { defaultValue = value; } }
            public FilterConditionType ConditionType { get { return conditionType; } set { conditionType = value; } }
        }

        public class NamedXPathTemplate
        {
            private string key;
            private string template;
            private List<XPathTemplateParameter> parameters;
            private string filterFormatter;
            public NamedXPathTemplate() { }
            public NamedXPathTemplate(string key, string template, List<XPathTemplateParameter> parameters, string filterFormatter)
            {
                this.key = key;
                this.template = template;
                this.parameters = parameters;
                this.filterFormatter = filterFormatter;
            }

            public string Key
            {
                get { return key; }
                set { key = value; }
            }

            public string Template
            {
                get { return template; }
                set { template = value; }
            }

            public List<XPathTemplateParameter> Parameters
            {
                get { return parameters; }
                set { parameters = value; }
            }

            public String FilterFormatter
            {
                get { return filterFormatter; }
                set { filterFormatter = value; }
            }
        }

        public class SQLCatalogSourceConfiguration
        {
            private string catalogSchemaFile;
            private string dataSchemaFile;
            private string connString;
            private string dataNamespaces;
            private string dataRootTag;
            private string idXPath;
            private List<NamedXPathTemplate> catalogXPathTemplate;
            private List<NamedXPathTemplate> dataXPathTemplate;

            public SQLCatalogSourceConfiguration() { }

            public string CatalogSchemaFile
            {
                get { return catalogSchemaFile; }
                set { catalogSchemaFile = value; }
            }

            public string DataSchemaFile
            {
                get { return dataSchemaFile; }
                set { dataSchemaFile = value; }
            }

            public string ConnString
            {
                get { return connString; }
                set { connString = value; }
            }

            public string DataNamespaces
            {
                get { return dataNamespaces; }
                set { dataNamespaces = value; }
            }

            public string DataRootTag
            {
                get { return dataRootTag; }
                set { dataRootTag = value; }
            }

            public string IDXPath
            {
                get { return idXPath; }
                set { idXPath = value; }
            }

            public List<NamedXPathTemplate> CatalogXPathTemplate
            {
                get { return catalogXPathTemplate; }
                set { catalogXPathTemplate = value; }
            }

            public List<NamedXPathTemplate> DataXPathTemplate
            {
                get { return dataXPathTemplate; }
                set { dataXPathTemplate = value; }
            }
        }

        public class SQLDALManagerConfiguration
        {
            private SQLCatalogSourceConfiguration sourceConfiguration;
            private string dataName;

            public SQLDALManagerConfiguration() { }

            public string DataName
            {
                get { return dataName; }
                set { dataName = value; }
            }

            public SQLCatalogSourceConfiguration SourceConfiguration
            {
                get { return sourceConfiguration; }
                set { sourceConfiguration = value; }
            }
        }
    }

    class SQLDALManager : IDALManager
    {
        #region Member Variables
        private ICatalogSource catalogSource;
        private Dictionary<Guid, DataUpdateLock> updateLocks;
        private List<IDataIdentifier> locked;
        #endregion

        #region Constructors
        public SQLDALManager(ICatalogSource catalogSource)
        {
            this.catalogSource = catalogSource;
            updateLocks = new Dictionary<Guid, DataUpdateLock>();
            locked = new List<IDataIdentifier>();
        }
        #endregion

        #region IDALManager Members

        public List<IDataIdentifier> GetCatalog()
        {
            return catalogSource.GetCatalog();
        }

        public List<IDataIdentifier> GetCatalog(IDataFilter filter)
        {
            return catalogSource.GetCatalog(filter);
        }

        public XmlDocument OpenData(IDataIdentifier id)
        {
            return catalogSource.OpenData(id);
        }

        public XmlDocument OpenData(IDataIdentifier id, IDataFilter filter)
        {
            return catalogSource.OpenData(id, filter);
        }

        public DataUpdateLock GetUpdateLock(IDataIdentifier id)
        {
            lock (locked)
            {
                if (locked.Contains(id))
                    return null;
                DataUpdateLock updateLock = new DataUpdateLock(Guid.NewGuid(), id, DateTime.Now.AddMinutes(1));
                updateLocks.Add(updateLock.Id, updateLock);
                locked.Add(id);
                updateLock.OnLockExpired += new LockExpiryEvent(OnLockExpired);
                return updateLock;
            }

        }

        public void UpdateData(XmlDocument updatedData, DataUpdateLock dataUpdateLock)
        {
            lock (updateLocks)
            {
                if (!updateLocks.ContainsKey(dataUpdateLock.Id))
                    throw new InvalidUpdateRequest("Update Denied: Data has not been locked, or lock has expired");
                catalogSource.UpdateData(updatedData, dataUpdateLock.Data);
            }
        }

        public IDataFilter GetCatalogFilter()
        {
            return catalogSource.GetFilter();
        }

        public IDataFilter GetCatalogFilter(string templateName)
        {
            return catalogSource.GetFilter(templateName);
        }

        public IDataFilter GetDataFilter()
        {
            return catalogSource.GetFilter();
        }

        public IDataFilter GetDataFilter(string templateName)
        {
            return catalogSource.GetFilter(templateName);
        }

        #endregion

        #region Lock Administration

        void OnLockExpired(object sender, LockExpiryEventArgs e)
        {
            lock (locked)
            {
                DataUpdateLock updateLock = updateLocks[e.UpdateLockId];
                updateLocks.Remove(e.UpdateLockId);
                locked.Remove(updateLock.Data);
            }
        }

        #endregion

        #region IDALManager Members



        #endregion
    }
}
