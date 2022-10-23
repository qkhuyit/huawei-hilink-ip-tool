using HuaweiHilinkIpTool.Huawei.Models.ResponseModels;

namespace HuaweiHilinkIpTool.Huawei
{
    public interface HuaweiHilink
    {
        Task Login();

        Task DeepResetIp();

        Task FastResetIp();

        Task<DeviceBasicInformationResponse> GetDeviceInfomation();

        Task<MonitoringStatusResponse> Monitoring();

        bool IsLogged();
    }
}