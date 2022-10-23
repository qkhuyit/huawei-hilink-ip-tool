using HuaweiHilinkIpTool.Huawei.Models.ResponseModels;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace HuaweiHilinkIpTool.Huawei
{
    public abstract class AbstractHuaweiHilink
    {
        protected readonly String API_ENDPOINT_SESSION_TOKEN = "/api/webserver/SesTokInfo";
        protected readonly string API_ENDPOINT_NETMODE = "/api/net/net-mode";
        protected readonly string API_ENDPOINT_DEVICE_INFOMATION = "/api/device/information";
        protected readonly string API_ENDPOINT_USER_LOGOUT = "/api/user/logout";
        protected readonly string API_ENDPOINT_MONITORING = "/api/monitoring/status";

        protected readonly HuaweiHilinkConf _conf;

        protected AbstractHuaweiHilink(HuaweiHilinkConf conf)
        {
            _conf = conf;
        }

        protected async Task<SessionTokenResponse?> GetSessionToken()
        {
            SessionTokenResponse ses = null;
            try
            {
                using (StringReader reader = new StringReader(new WebClient().DownloadString($"http://{_conf.Ip}/api/webserver/SesTokInfo")))
                {
                    ses = (SessionTokenResponse)new XmlSerializer(typeof(SessionTokenResponse)).Deserialize(reader);
                }
                return ses;
            }
            catch
            {
                return null;
            }
        }

        protected string EncryptPassword(string username, string password, string token)
        {
            password = SHA256(password);
            password = Base64(password);

            string final = username + password + token;
            final = SHA256(final);
            final = Base64(final);

            return final;
        }

        protected string SHA256(string str)
        {
            SHA256Managed crypt = new();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(str));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        private static string Base64(string str)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}