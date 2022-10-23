using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Huawei.Models.ResponseModels
{
    [XmlType("error")]
    public class ErrorResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }
}