using System.Globalization;
using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Common.Xml
{
    public static class XmlSerializerCache
    {
        private static readonly Dictionary<string, XmlSerializer> cache = new Dictionary<string, XmlSerializer>();

        public static XmlSerializer Create(Type type, XmlAttributeOverrides xOver)
        {
            string key = string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0}", (object)type);
            if (!XmlSerializerCache.cache.ContainsKey(key))
                XmlSerializerCache.cache.Add(key, new XmlSerializer(type, xOver));
            return XmlSerializerCache.cache[key];
        }

        public static XmlSerializer Create(Type type)
        {
            string key = string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0}", (object)type);
            if (!XmlSerializerCache.cache.ContainsKey(key))
                XmlSerializerCache.cache.Add(key, new XmlSerializer(type));
            return XmlSerializerCache.cache[key];
        }
    }
}