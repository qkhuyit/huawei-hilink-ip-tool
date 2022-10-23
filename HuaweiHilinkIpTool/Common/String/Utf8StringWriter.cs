using System.Text;

namespace HuaweiHilinkIpTool.Common.String
{
    public class Utf8StringWriter : StringWriter
    {
        public virtual Encoding Encoding => Encoding.UTF8;
    }
}