using HuaweiHilinkIpTool.Common.Extensions;
using HuaweiHilinkIpTool.Huawei.Models.RequestModels;
using HuaweiHilinkIpTool.Huawei.Models.ResponseModels;
using System.Net;

namespace HuaweiHilinkIpTool.Huawei.Modem
{
    public class E8372Hilink : AbstractHuaweiHilink, HuaweiHilink
    {
        public E8372Hilink(HuaweiHilinkConf conf) : base(conf)
        {
        }

        public async Task DeepResetIp()
        {
            var firstMode = NetmodeType.Only3G;
            var secondMode = NetmodeType.Auto;
            string uri = $"http://{_conf.Ip}{API_ENDPOINT_NETMODE}";
            NetModeResponse netmode = await _conf.client.GetAsXmlAsync<NetModeResponse>(uri);

            if (!secondMode.Equals(netmode.NetworkMode))
            {
                firstMode = NetmodeType.Auto;
                secondMode = NetmodeType.Only3G;
            }

            await Task.Delay(1000);

            await _conf.client.PostAsXmlAsync<SuccessResponse>(uri, (object)new SwitchNetModeRequest()
            {
                NetworkMode = firstMode,
                NetworkBand = netmode.NetworkBand,
                LTEBand = netmode.LTEBand
            });

            while (true)
            {
                await Task.Delay(1000);
                netmode = await _conf.client.GetAsXmlAsync<NetModeResponse>(uri);
                if (firstMode.Equals(netmode.NetworkMode))
                    break;
            }

            await Task.Delay(1000);

            await _conf.client.PostAsXmlAsync<SuccessResponse>(uri, (object)new SwitchNetModeRequest()
            {
                NetworkMode = secondMode,
                NetworkBand = netmode.NetworkBand,
                LTEBand = netmode.LTEBand
            });

        }

        public async Task FastResetIp()
        {
        }

        public async Task<DeviceBasicInformationResponse> GetDeviceInfomation()
        {
            string uri = $"http://{_conf.Ip}{API_ENDPOINT_DEVICE_INFOMATION}";
            var deviceInfo = await _conf.client.GetAsXmlAsync<DeviceBasicInformationResponse>(uri);
            return deviceInfo;
        }

        public bool IsLogged()
        {
            return _conf.client != null;
        }

        public async Task Login()
        {
            try
            {
                var session = await GetSessionToken();
                using (WebClient client = new())
                {
                    client.Headers.Add("__RequestVerificationToken", session.TokInfo);
                    client.Headers.Add(HttpRequestHeader.Cookie, $"SessionID={session.SesInfo}");
                    string data = $"<?xml version:'1.0' encoding='UTF - 8'?>"
                        + $"<request>"
                        + $"<Username>{_conf.UserName}</Username>"
                        + $"<Password>{EncryptPassword(_conf.UserName, _conf.Password, session.TokInfo)}</Password>"
                        + $"<password_type>4</password_type>"
                        + $"</request>";

                    client.UploadString($"http://{_conf.Ip}/api/user/login", data);

                    if(_conf.client == null)
                        _conf.client = new HttpClient();
                    _conf.client.DefaultRequestHeaders.Add("Cookie", client.ResponseHeaders.Get("Set-Cookie"));
                    _conf.client.DefaultRequestHeaders.Add("__RequestVerificationToken", client.ResponseHeaders.Get("__RequestVerificationTokenone"));
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task Logout()
        {
            try
            {
                string data = $"<?xml version:'1.0' encoding='UTF - 8'?>"
                       + $"<request>"
                       + $"<Logout>1</Logout>"
                       + $"</request>";
                _conf.client.PostRawXmlAsync<SuccessResponse>($"http://{_conf.Ip}{API_ENDPOINT_USER_LOGOUT}", data);
            }
            catch (Exception ex)
            {
                _conf.client.DefaultRequestHeaders.Clear();
            }

        }

        public async Task<MonitoringStatusResponse> Monitoring()
        {
            string uri = $"http://{_conf.Ip}{API_ENDPOINT_MONITORING}";
            var deviceInfo = await _conf.client.GetAsXmlAsync<MonitoringStatusResponse>(uri);
            return deviceInfo;
        }
    }
}