using Newtonsoft.Json;
using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Huawei.Models.ResponseModels
{
    [XmlRoot("response")]
    public class SessionTokenResponse
    {
        public string SesInfo { get; set; }
        public string TokInfo { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}