using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Huawei.Models.RequestModels
{
    public class MobileDataSwitchRequest
    {
        [XmlElement(ElementName = "dataswitch")]
        public int Dataswitch { get; set; }

        public string token => "316986";
    }
}