using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlXml;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WarehouseApplication.DALManager
{
    public class SQLXmlCatalogSource : ICatalogSource
    {
        #region Constants
        public static string NullFilter = "NullFilter";
        public static string SelectionPlaceholder = "__SELECT_UNIQUE__";
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
        public SQLXmlCatalogSource(
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
            return GetCatalog(GetXml(catalogXPathTemplate[NullFilter].Template));
        }

        public List<IDataIdentifier> GetCatalog(IDataFilter filter)
        {
            string catalogFilterTemplate = catalogXPathTemplate[filter.TemplateName].Template;
            return GetCatalog(GetXml(filter.GetFilterExpression(catalogFilterTemplate)));
        }

        public IDataFilter GetFilter()
        {
            return GetFilter(NullFilter);
        }

        public IDataFilter GetFilter(string templateName)
        {
            SQLXmlDALManagerFactory.NamedXPathTemplate template = catalogXPathTemplate[templateName];
            XPathTemplateFilter filter = new XPathTemplateFilter(template);
            return filter;
        }

        #endregion

        #region ISource Members

        public System.Xml.XmlDocument OpenData(IDataIdentifier id)
        {
            //TODO - Review this later.
            string nullDataFilter = dataXPathTemplate[NullFilter].Template;
            string query = nullDataFilter.Replace(SelectionPlaceholder, ((GuidIdentifier)id).ID.ToString());
            return GetXmlDocument(GetXml(query));
        }

        public System.Xml.XmlDocument OpenData(IDataIdentifier id, IDataFilter filter)
        {
            //TODO - Review this later.
            string dataFilterTemplate = dataXPathTemplate[filter.TemplateName].Template;
            string selection = dataFilterTemplate.Replace(SelectionPlaceholder, ((GuidIdentifier)id).ID.ToString());
            return GetXmlDocument(GetXml(filter.GetFilterExpression(selection)));
        }

        public void UpdateData(System.Xml.XmlDocument updatedData, IDataIdentifier id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private Stream GetXml(string filterTemplate)
        {
            SqlXmlCommand catalogCommand = new SqlXmlCommand(connString);
            catalogCommand.CommandType = SqlXmlCommandType.XPath;
            catalogCommand.CommandText = filterTemplate;
            catalogCommand.RootTag = dataRootTag;
            catalogCommand.Namespaces = dataNamespaces;
            catalogCommand.SchemaPath = catalogSchemaFile;
            return catalogCommand.ExecuteStream();
            /* For the time being
            Stream xmlStream = File.OpenRead(@"D:\Reusable Components\DataAccessLogic\ProofOfConcept\ProofOfConcept\TestPUNCatalog.xml");
            return xmlStream;
             */
        }

        private List<IDataIdentifier> GetCatalog(Stream catalogStream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(catalogStream);
            List<IDataIdentifier> identifiers = new List<IDataIdentifier>();
            foreach (XmlNode catalogNode in document.DocumentElement.ChildNodes)
            {
                XmlDocument preview = new XmlDocument();
                preview.LoadXml(
                    string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><Catalog>{0}</Catalog>",
                    catalogNode.OuterXml)
                    );

                XmlNamespaceManager nsm = new XmlNamespaceManager(preview.NameTable);
                NamespaceParser nsp = new NamespaceParser(dataNamespaces);
                nsm.AddNamespace("y0", nsp.Url);
                XmlNodeList selectedGuidNodes = preview.SelectNodes(string.Format("/Catalog{0}", idXPath), nsm);
                XmlNode guidNode = selectedGuidNodes[0];
                string guid = guidNode.ChildNodes[0].Value;

                IDataIdentifier identifier = new GuidIdentifier(new Guid(guid), preview);
                identifiers.Add(identifier);
            }
            return identifiers;
        }

        private XmlDocument GetXmlDocument(Stream dataStream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(dataStream);
            return document;
        }
        #endregion
    }

    public class NamespaceParser
    {
        private string prefix;
        private string url;

        public NamespaceParser(string namespaceDeclaration)
        {
            string[] parts = namespaceDeclaration.Split('=');
            url = parts[1].Trim();
            url = url.Substring(1, url.Length - 2);
            string prefixContainer = parts[0].Trim();
            string[] prefixParts = prefixContainer.Split(':');
            if (prefixParts.Length == 1)
            {
                prefix = string.Empty;
            }
            else
            {
                prefix = prefixParts[1].Trim();
            }
        }

        public NamespaceParser(string prefix, string url)
        {
            this.prefix = prefix;
            this.url = url;
        }

        public string NamespaceDeclaration
        {
            get { return string.Format("{0}:{1}", prefix, url); }
        }

        public string Prefix
        {
            get { return prefix; }
        }

        public string Url
        {
            get { return url; }
        }
    }
}
