using HuaweiHilinkIpTool.Common.Exceptions;
using HuaweiHilinkIpTool.Huawei.Models.ResponseModels;
using System.Xml;

namespace HuaweiHilinkIpTool.Common.Xml
{
    public static class XmlUtils
    {
        public static T XmlToResponse<T>(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            if (((XmlNode)xmlDocument).SelectSingleNode("response") != null)
                return XmlConverter.LoadFromXMLResponse<T>(xml);
            if (((XmlNode)xmlDocument).SelectSingleNode("error") != null)
                throw new RequestErrorException(XmlConverter.LoadFromXMLString<ErrorResponse>(xml).Code);
            throw new Exception($"Format missing, {xml}");
        }
    }
}