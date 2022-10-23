using HuaweiHilinkIpTool.Common.Network.Models;

namespace HuaweiHilinkIpTool.Common.Network
{
    public static class NetworkHelper
    {
        public static async Task<IpInfoModel> GetIpInfo()
        {
            using(HttpClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync("http://ip-api.com/json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IpInfoModel>(responseBody);
            }
        }
    }
}