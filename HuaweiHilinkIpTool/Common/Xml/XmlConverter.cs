using HuaweiHilinkIpTool.Common.String;
using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Common.Xml
{
    public static class XmlConverter
    {
        public static string ToXMLRequest<T>(this T entity)
        {
            using (Utf8StringWriter utf8StringWriter = new Utf8StringWriter())
            {
                XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces();
                serializerNamespaces.Add(string.Empty, string.Empty);
                XmlAttributes attributes = new XmlAttributes();
                XmlAttributeOverrides xOver = new XmlAttributeOverrides();
                attributes.XmlType = new XmlTypeAttribute()
                {
                    TypeName = "request"
                };
                xOver.Add(entity.GetType(), attributes);
                XmlSerializerCache.Create(entity.GetType(), xOver).Serialize((TextWriter)utf8StringWriter, (object)entity, serializerNamespaces);
                return ((object)utf8StringWriter).ToString();
            }
        }

        public static T LoadFromXMLString<T>(string xmlText)
        {
            using (StringReader stringReader = new StringReader(xmlText))
                return (T)XmlSerializerCache.Create(typeof(T)).Deserialize((TextReader)stringReader);
        }

        public static T LoadFromXMLResponse<T>(string xmlText)
        {
            using (StringReader stringReader = new StringReader(xmlText))
            {
                XmlAttributes attributes = new XmlAttributes();
                XmlAttributeOverrides xOver = new XmlAttributeOverrides();
                attributes.XmlType = new XmlTypeAttribute()
                {
                    TypeName = "response"
                };
                xOver.Add(typeof(T), attributes);
                return (T)XmlSerializerCache.Create(typeof(T), xOver).Deserialize((TextReader)stringReader);
            }
        }
    }
}