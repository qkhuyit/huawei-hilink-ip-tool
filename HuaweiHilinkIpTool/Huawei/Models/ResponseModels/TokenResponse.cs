using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Huawei.Models.ResponseModels
{
    public class TokenResponse
    {
        [XmlElement(ElementName = "token")]
        public string Token { get; set; }
    }
}