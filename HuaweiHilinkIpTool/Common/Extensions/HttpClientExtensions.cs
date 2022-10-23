using HuaweiHilinkIpTool.Common.Xml;
using Newtonsoft.Json.Linq;
using System.Text;

namespace HuaweiHilinkIpTool.Common.Extensions
{
    public static class HttpClientExtensions
    {

        private static string AddHttpPrefix(string url)
        {
            return url.StartsWith("http") ? url : $"http://{url}";
        }

        public static async Task<TResult> PostAsXmlAsync<TResult>(this HttpClient httpClient, string requestUri, object? content = null)
        {
            var uri = AddHttpPrefix(requestUri);
            var data = content == null
                ? (HttpContent)new StringContent(string.Empty)
                : (HttpContent)new StringContent(content.ToXMLRequest<object>(), Encoding.UTF8, "text/xml");

            HttpResponseMessage response = await httpClient.PostAsync(uri, data);
            response.EnsureSuccessStatusCode();
            var xmlContent = await response.Content.ReadAsStringAsync();

            //Update header
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("Set-Cookie", out values) && values.Any())
            {
                httpClient.DefaultRequestHeaders.Remove("Cookie");
                httpClient.DefaultRequestHeaders.Add("Cookie", values.First());
            }

            IEnumerable<string> values2;
            if (response.Headers.TryGetValues("__RequestVerificationToken", out values2) && values2.Any())
            {
                httpClient.DefaultRequestHeaders.Remove("__RequestVerificationToken");
                httpClient.DefaultRequestHeaders.Add("__RequestVerificationToken", values2.First());
            }

            return XmlUtils.XmlToResponse<TResult>(xmlContent);
        }

        public static async Task<TResult> PostRawXmlAsync<TResult>(this HttpClient httpClient, string requestUri, string xml = "")
        {
            var uri = AddHttpPrefix(requestUri);
            var data = (HttpContent)new StringContent(xml, Encoding.UTF8, "text/xml");

            HttpResponseMessage response = await httpClient.PostAsync(uri, data);
            response.EnsureSuccessStatusCode();
            var xmlContent = await response.Content.ReadAsStringAsync();

            //Update header
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("Set-Cookie", out values) && values.Any())
            {
                httpClient.DefaultRequestHeaders.Remove("Cookie");
                httpClient.DefaultRequestHeaders.Add("Cookie", values.First());
            }

            IEnumerable<string> values2;
            if (response.Headers.TryGetValues("__RequestVerificationToken", out values2) && values2.Any())
            {
                httpClient.DefaultRequestHeaders.Remove("__RequestVerificationToken");
                httpClient.DefaultRequestHeaders.Add("__RequestVerificationToken", values2.First());
            }

            return XmlUtils.XmlToResponse<TResult>(xmlContent);
        }

        public static async Task<TResult> GetAsXmlAsync<TResult>(this HttpClient httpClient, string requestUri)
        {
            var uri = AddHttpPrefix(requestUri);
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var xmlContent = await response.Content.ReadAsStringAsync();

            //Update header
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("Set-Cookie", out values) && values.Any())
            {
                httpClient.DefaultRequestHeaders.Remove("Cookie");
                httpClient.DefaultRequestHeaders.Add("Cookie", values.First());
            }

            IEnumerable<string> values2;
            if (response.Headers.TryGetValues("__RequestVerificationToken", out values2) && values2.Any())
            {
                httpClient.DefaultRequestHeaders.Remove("__RequestVerificationToken");
                httpClient.DefaultRequestHeaders.Add("__RequestVerificationToken", values2.First());
            }

            return XmlUtils.XmlToResponse<TResult>(xmlContent);
        }
    }
}