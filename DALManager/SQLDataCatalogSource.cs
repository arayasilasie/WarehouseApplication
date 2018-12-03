using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Serialization;

namespace WarehouseApplication.DALManager
{
    class SQLDataCatalogSource : ICatalogSource
    {
        #region Constants
        public static string NullFilter = "NullFilter";
        #endregion

        #region Member Variables
        private string catalogSchemaFile;
        private string dataSchemaFile;
        private string connString;
        private string dataNamespaces;
        private string dataRootTag;
        private string idXPath;
        private Dictionary<string, SQLXmlDALManagerFactory.NamedXPathTemplate> catalogXPathTemplate;
        private Dictionary<string, SQLXmlDALManagerFactory.NamedXPathTemplate> dataXPathTemplate;
        #endregion

        #region Cosntructors
        public SQLDataCatalogSource(
            string connString,
            string catalogSchemaFile,
            string dataSchemaFile,
            string dataRootTag,
            string dataNamespaces,
            string idXPath,
            Dictionary<string, SQLXmlDALManagerFactory.NamedXPathTemplate> catalogXPathTemplate,
            Dictionary<string, SQLXmlDALManagerFactory.NamedXPathTemplate> dataXPathTemplate)
        {
            this.connString = connString;
            this.catalogSchemaFile = catalogSchemaFile;
            this.dataSchemaFile = dataSchemaFile;
            this.dataRootTag = dataRootTag;
            this.dataNamespaces = dataNamespaces;
            this.idXPath = idXPath;
            this.catalogXPathTemplate = catalogXPathTemplate;
            this.dataXPathTemplate = dataXPathTemplate;
        }
        #endregion

        #region ICatalogSource Members

        public List<IDataIdentifier> GetCatalog()
        {
            return GetCatalog(GetXml(catalogXPathTemplate[NullFilter].Template, GetFilter()));
        }

        public List<IDataIdentifier> GetCatalog(IDataFilter filter)
        {
            string catalogFilterTemplate = catalogXPathTemplate[filter.TemplateName].Template;
            return GetCatalog(GetXml(catalogFilterTemplate, filter));
        }

        public IDataFilter GetFilter()
        {
            return GetFilter(NullFilter);
        }

        public IDataFilter GetFilter(string templateName)
        {
            SQLXmlDALManagerFactory.NamedXPathTemplate template = catalogXPathTemplate[templateName];
            SQLTemplateFilter filter = new SQLTemplateFilter(template);
            return filter;
        }

        #endregion

        #region ISource Members

        public System.Xml.XmlDocument OpenData(IDataIdentifier id)
        {
            /*
            //TODO - Review this later.
            string nullDataFilter = dataXPathTemplate[NullFilter].Template;
            string query = nullDataFilter.Replace(SelectionPlaceholder, ((GuidIdentifier)id).ID.ToString());
            return GetXmlDocument(GetXml(query));
             */
            return null;
        }

        public System.Xml.XmlDocument OpenData(IDataIdentifier id, IDataFilter filter)
        {
            /*
            //TODO - Review this later.
            string dataFilterTemplate = dataXPathTemplate[filter.TemplateName].Template;
            string selection = dataFilterTemplate.Replace(SelectionPlaceholder, ((GuidIdentifier)id).ID.ToString());
            return GetXmlDocument(GetXml(filter.GetFilterExpression(selection)));
             */
            return null;
        }

        public void UpdateData(System.Xml.XmlDocument updatedData, IDataIdentifier id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private XmlDocument GetXml(string filterTemplate, IDataFilter filter)
        {
            SqlConnection connection = new SqlConnection(connString);
            SqlCommand command = new SqlCommand(filterTemplate, connection);
            command.CommandTimeout = 120;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            string filterExpression = filter.GetFilterExpression(filterTemplate);
            XmlSerializer s = new XmlSerializer(typeof(List<SQLDataFilterParameter>));
            List<SQLDataFilterParameter> filterParameters = (List<SQLDataFilterParameter>)s.Deserialize(new StringReader(filterExpression));
            foreach (SQLDataFilterParameter filterParameter in filterParameters)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + filterParameter.Name;
                if (filterParameter.Value != null && filterParameter.Value != string.Empty)
                {
                    parameter.DbType = filterParameter.Type;
                    parameter.Value = NullFinder.Parse(filterParameter.Value, filterParameter.ValueType);
                }
                //parameter.Size = filterParameter.Size;
                parameter.Direction = filterParameter.Direction;
                parameter.IsNullable = true;
                //parameter.Precision = filterParameter.Precision;
                //parameter.Scale = filterParameter.Scale;
                command.Parameters.Add(parameter);
            }
            connection.Open();

            XmlReader reader = command.ExecuteXmlReader();
            XmlDocument document = new XmlDocument();
            document.Load(reader);
            reader.Close();
            connection.Close();
            return document;

            //Stream xmlStream = File.OpenRead(connString.Trim());
            //return XmlReader.Create(xmlStream);

        }

        private List<IDataIdentifier> GetCatalog(XmlDocument catalogDocument)
        {
            List<IDataIdentifier> identifiers = new List<IDataIdentifier>();
            foreach (XmlNode catalogNode in catalogDocument.DocumentElement.ChildNodes)
            {
                XmlDocument preview = new XmlDocument();
                preview.LoadXml(
                    string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><Catalog>{0}</Catalog>",
                    catalogNode.OuterXml)
                    );
                //XmlNamespaceManager nsm = new XmlNamespaceManager(preview.NameTable);
                //NamespaceParser nsp = new NamespaceParser(dataNamespaces);
                //nsm.AddNamespace("y0", nsp.Url); 
                XmlNodeList selectedGuidNodes = preview.SelectNodes(string.Format("/Catalog{0}", idXPath));
                XmlNode guidNode = selectedGuidNodes[0];
                string guid = guidNode.ChildNodes[0].Value;

                IDataIdentifier identifier = new GuidIdentifier(new Guid(guid), preview);
                identifiers.Add(identifier);
            }
            return identifiers;
        }
        #endregion
    }

    public class SQLDataFilterParameter
    {
        private string name;
        private DbType type;
        private ParameterDirection direction;
        private int size;
        private byte precision;
        private byte scale;
        private string value;
        private string valueType;
        public SQLDataFilterParameter() { }
        public SQLDataFilterParameter(
            string name,
            DbType type,
            int size,
            ParameterDirection direction,
            byte precision,
            byte scale,
            string value,
            string valueType)
        {
            this.name = name;
            this.type = type;
            this.size = size;
            this.direction = direction;
            this.precision = precision;
            this.scale = scale;
            this.value = value;
            this.valueType = valueType;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DbType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public byte Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        public byte Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public string ValueType
        {
            get { return valueType; }
            set { valueType = value; }
        }
    }
}
