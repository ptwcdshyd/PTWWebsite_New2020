using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using PTW.DBAccess;

namespace PTW.DBAccess
{
    public class CustomXmlHelper
    {
        public const string DefaultViewName = "Default View";
        public string ViewName { get; set; }

        public CustomXmlHelper()
        {
            this.ViewName = DefaultViewName;
        }

        public virtual string GetXml()
        {
            try
            {
                return this.GetXml(this.ViewName);
            }
            catch { throw; }
        }

        public virtual string GetXml(string viewName)
        {
            try
            {
                switch (viewName)
                {
                    case CustomXmlHelper.DefaultViewName:
                        return this.GetDefaultViewXml();
                    default:
                        return string.Empty;
                }
            }
            catch { throw; }
        }

        protected string GetDefaultViewXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                StringBuilder xmlData = new StringBuilder();

                // To omit xml declaration.
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true
                };

                // To omit namespace.
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);

                using (XmlWriter writer = XmlWriter.Create(xmlData, settings))
                {
                    serializer.Serialize(writer, this, namespaces);
                    writer.Close();
                }
                return xmlData.ToString();
                //return "N" + xmlData.ToString();
            }
            catch { throw; }
        }


        //
        public string CustomImagesXml(List<IFormFile> files)
        {
            StringBuilder xml = new StringBuilder("Images>");
            foreach (IFormFile file in files)
            {
                
                xml.Append("<Image>");
                xml.Append(string.Format("<ImageName>{0}</ImageName><ImageSize>{1}</ImageSize><Type>{2}</Type>", file.FileName,file.Length,file.ContentType));
                xml.Append("</Image>");
            }
            xml.Append("</Images>");
            return xml.ToString();
        }

    }
}
