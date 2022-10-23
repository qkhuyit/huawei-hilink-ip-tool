namespace HuaweiHilinkIpTool.Huawei.Models.ResponseModels
{
    public class NetworkInformationResponse : IResponse
    {
        public int State { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public int Numeric { get; set; }

        public int Rat { get; set; }
    }
}