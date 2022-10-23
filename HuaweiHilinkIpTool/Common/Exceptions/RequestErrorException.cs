namespace HuaweiHilinkIpTool.Common.Exceptions
{
    public class RequestErrorException : Exception
    {
        private readonly Dictionary<string, string> _message = new Dictionary<string, string>()
        {
            ["100002"] = "No support",
            ["100003"] = "Access denied",
            ["100004"] =  "Busy",
            ["108001"] =  "Wrong username",
            ["108002"] = "Wrong password",
            ["108003"] = "Already logged in",
            ["120001"] = "Voice busy",
            ["125001"] = "Wrong __RequestVerificationToken header"
        };

        public string Code { get; set; }
        public RequestErrorException(string code) => this.Code = code;
    }
}