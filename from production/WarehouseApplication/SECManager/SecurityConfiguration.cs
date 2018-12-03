using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WarehouseApplication.SECManager
{
    [XmlRoot("SecurityResourceConfiguration")]
    public class SecurityResourceConfigurationInfo
    {
        private List<SecuredResourceContainerInfo> securedResourceContainers = new List<SecuredResourceContainerInfo>();
        private List<SecurityRoleInfo> securityRoles = new List<SecurityRoleInfo>();

        public SecurityResourceConfigurationInfo() { }

        [XmlArray("SecuredResourceContainers")]
        [XmlArrayItem("SecuredResourceContainer")]
        public List<SecuredResourceContainerInfo> SecuredResourceContainers
        {
            get { return securedResourceContainers; }
        }

        [XmlArray("SecurityRoles")]
        [XmlArrayItem("SecurityRole")]
        public List<SecurityRoleInfo> SecurityRoles
        {
            get { return securityRoles; }
        }
    }

    public enum ResourceContainerType
    {
        ContentPage,MasterPage,UserControl
    }

    public class SecuredResourceContainerInfo
    {
        private ResourceContainerType type;
        private string name;
        private List<SecuredResourceInfo> securedResources = new List<SecuredResourceInfo>();

        public SecuredResourceContainerInfo() { }
        public SecuredResourceContainerInfo(ResourceContainerType type, string name)
        {
            this.type = type;
            this.name = name;
        }

        [XmlAttribute]
        public ResourceContainerType Type
        {
            get { return type; }
            set { type = value; }
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlArray("SecuredResources")]
        [XmlArrayItem("SecuredResource")]
        public List<SecuredResourceInfo> SecuredResources
        {
            get { return securedResources; }
        }
    }

    public class SecuredResourceInfo
    {
        private string scope;
        private string name;
        private List<ConfigurationOptionInfo> configurationOptions = new List<ConfigurationOptionInfo>();

        public SecuredResourceInfo() { }
        public SecuredResourceInfo(string scope, string name)
        {
            this.scope = scope;
            this.name = name;
        }

        [XmlAttribute]
        public string Scope
        {
            get { return scope; }
            set { scope = value; }
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlArray("ConfigurationOptions")]
        [XmlArrayItem("ConfigurationOption")]
        public List<ConfigurationOptionInfo> ConfigurationOptions
        {
            get { return configurationOptions; }
        }
    }

    public class ConfigurationOptionInfo
    {
        private Int32 optionId;
        private string property;
        private string value;
        private Int32 level;

        public ConfigurationOptionInfo() { }
        public ConfigurationOptionInfo(Int32 optionId, string property, string value, Int32 level)
        {
            this.optionId = optionId;
            this.property = property;
            this.value = value;
            this.level = level;
        }

        [XmlAttribute]
        public Int32 OptionId
        {
            get { return optionId; }
            set { optionId = value; }
        }

        [XmlAttribute]
        public string Property
        {
            get { return property; }
            set { property = value; }
        }

        [XmlAttribute]
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        [XmlAttribute]
        public Int32 Level
        {
            get { return level; }
            set { level = value; }
        }
    }

    public class SecurityRoleInfo
    {
        private string name;
        private List<GrantedResourceContainerInfo> grantedResourceContainers = new List<GrantedResourceContainerInfo>();

        public SecurityRoleInfo() { }
        public SecurityRoleInfo(string name)
        {
            this.name = name;
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlArray("GrantedResourceContainers")]
        [XmlArrayItem("GrantedResourceContainer")]
        public List<GrantedResourceContainerInfo> GrantedResourceContainers
        {
            get { return grantedResourceContainers; }
        }
    }

    public class GrantedResourceContainerInfo
    {
        private string name;
        private List<GrantedResourceInfo> grantedResources = new List<GrantedResourceInfo>();
        public GrantedResourceContainerInfo() { }
        public GrantedResourceContainerInfo(string name)
        {
            this.name = name;
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlArray("GrantedResources")]
        [XmlArrayItem("GrantedResource")]
        public List<GrantedResourceInfo> GrantedResources
        {
            get { return grantedResources; }
        }
    }

    public class GrantedResourceInfo
    {
        private string scope;
        private string name;
        private Int32 option;

        public GrantedResourceInfo() { }
        public GrantedResourceInfo(string scope, string name, Int32 option)
        {
            this.scope = scope;
            this.name = name;
            this.option = option;
        }

        [XmlAttribute]
        public String Scope
        {
            get { return scope; }
            set { scope = value; }
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlAttribute]
        public Int32 Option
        {
            get { return option; }
            set { option = value; }
        }
    }
}