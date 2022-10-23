namespace HuaweiHilinkIpTool.Huawei.Models.ResponseModels
{
    public class MonitoringStatusResponse
    {
        public int ConnectionStatus { get; set; }

        public int CurrentNetworkType { get; set; }

        public int CurrentServiceDomain { get; set; }

        public string PrimaryDns { get; set; }

        public string SecondaryDns { get; set; }

        public string ServiceStatus { get; set; }

        public int SimStatus { get; set; }

        public string SignalIcon { get; set; }


        public string CurrentNetworkTypeName()
        {
            switch (CurrentNetworkType)
            {
                case 1:
                    return "GSM";
                case 2:
                    return "GPRS";
                case 3:
                    return "EDGE";
                case 4:
                case 41:
                    return "WCDMA";
                case 5:
                case 42:
                case 62:
                    return "HSDPA";
                case 6:
                case 43:
                case 63:
                    return "HSUPA";
                case 7:
                case 44:
                case 64:
                    return "HSPA";
                case 8:
                case 61:
                    return "TDSCDMA";
                case 9:
                case 17:
                case 18:
                case 45:
                case 46:
                case 65:
                    return "HSPA+";
                case 19:
                case 101:
                    return "LTE";
                case 23:
                    return "CDMA1x";
                default:
                    return "None";
            }
        }

        public string ConnectionStatusName()
        {
            switch (ConnectionStatus)
            {
                case 900:
                    return "Connecting";
                case 901:
                    return "Connected";
                case 902:
                    return "Disconnected";
                case 903:
                    return "Disconnecting";
                case 906:
                    return "Connection error";
                default:
                    return "No Connection";
            }
        }
    }
}