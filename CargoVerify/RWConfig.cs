using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CargoVerify
{
    /// <summary>
    /// 读写配置文件
    /// </summary>
    public class RWConfig
    {
        private static RWConfig instance = new RWConfig();
        private string filePath;
        private RWConfig()
        {
            filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            filePath = System.IO.Path.Combine(filePath, "config.xml");
            //判断配置文件是否存在
            if (!System.IO.File.Exists(filePath))
            {
                filePath = string.Empty;
            }
        }

        public static RWConfig Instance
        {
            get { return instance; }
        }

        public string GetValue(string name)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);
            return  xml.FirstChild.SelectSingleNode(name).InnerText;            
        }
        public string GetValue(string name, string subName)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);

            return xml.FirstChild.SelectSingleNode(name).SelectSingleNode(subName).InnerText;
        }

        public void SetValue(string name, string subName, string value)
        {
            if (string.IsNullOrEmpty(filePath))
                return;
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);
            try
            {
                xml.FirstChild.SelectSingleNode(name).SelectSingleNode(subName).InnerText = value;
                xml.Save(filePath);
            }
            catch (Exception ex)
            { }
        }

    }
}
