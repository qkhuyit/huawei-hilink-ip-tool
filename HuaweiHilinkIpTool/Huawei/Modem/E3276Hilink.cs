using HuaweiHilinkIpTool.Huawei.Models.ResponseModels;

namespace HuaweiHilinkIpTool.Huawei.Modem
{
    public class E3276Hilink : AbstractHuaweiHilink, HuaweiHilink
    {
        public E3276Hilink(HuaweiHilinkConf conf) : base(conf)
        {
        }

        public Task DeepResetIp()
        {
            throw new NotImplementedException();
        }

        public Task FastResetIp()
        {
            throw new NotImplementedException();
        }

        public Task<DeviceBasicInformationResponse> GetDeviceInfomation()
        {
            throw new NotImplementedException();
        }

        public bool IsLogged()
        {
            throw new NotImplementedException();
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }

        public Task<MonitoringStatusResponse> Monitoring()
        {
            throw new NotImplementedException();
        }
    }
}