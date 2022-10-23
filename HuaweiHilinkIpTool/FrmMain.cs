using HuaweiHilinkIpTool.Common.Network;
using HuaweiHilinkIpTool.Huawei;
using HuaweiHilinkIpTool.Huawei.Modem;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HuaweiHilinkIpTool
{
    public partial class FrmMain : Form
    {
        private HuaweiHilink _hilink;

        public FrmMain()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTickHandler);
            timer.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUserName.Text) && !String.IsNullOrEmpty(txtPassword.Text))
            {
                var conf = new HuaweiHilinkConf()
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text
                };

                NetworkInterface networkInterface = ((IEnumerable<NetworkInterface>)NetworkInterface.GetAllNetworkInterfaces()).Where<NetworkInterface>((Func<NetworkInterface, bool>)(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)).FirstOrDefault<NetworkInterface>((Func<NetworkInterface, bool>)(t => t.Description.Contains("NDIS")));

                var address = networkInterface
                        .GetIPProperties()
                        .GatewayAddresses
                        .FirstOrDefault<GatewayIPAddressInformation>((Func<GatewayIPAddressInformation, bool>)(a => a.Address.AddressFamily == AddressFamily.InterNetwork))?.Address?.ToString();
                conf.Ip = address ?? "192.168.9.1";
                _hilink = new E8372Hilink(conf);
                _hilink.Login();
            } else
            {

            }
        }

        private void timerTickHandler(object sender, EventArgs e)
        {
            btnLogin.Text = _hilink != null && _hilink.IsLogged() ? "You are logged in" : "Login";
            btnLogin.Enabled = _hilink != null && _hilink.IsLogged() ? false : true;
            LoadDeviceInfomation();
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            btnReset.Text = "Reseting ...";
            btnReset.Enabled = false;
            try
            {
                await _hilink.DeepResetIp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnReset.Enabled = true;
            btnReset.Text = "Reset IP Address Now";
        }

        private async Task LoadDeviceInfomation()
        {
            try
            {
                if (_hilink == null)
                {
                    lblStatus.Text = "Disconnected";
                    return;
                }

                var deviceInfo = await _hilink.GetDeviceInfomation();
                lblStatus.Text = "Connected";
                lblDeviceName.Text = deviceInfo.DeviceName;

                var monitoring = await _hilink.Monitoring();
                lblNetworkType.Text = monitoring.CurrentNetworkTypeName();
                lblStatus.Text = monitoring.ConnectionStatusName();


                var ipInfo = await NetworkHelper.GetIpInfo();
                lblIp.Text = ipInfo.Query;
                lblLocation.Text = ipInfo.RegionName;
                lblIsp.Text = ipInfo.Isp;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Disconnected";
            }
        }
    }
}