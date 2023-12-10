using System.Net;
using RemoteControlServer.Controllers.ServerClasses;

namespace RemoteControlServer.Views
{
    public partial class StartServerForm : Form
    {
        private static TcpServer server;

        public StartServerForm()
        {
            InitializeComponent();
            ipAddressTextBox.Text = IpAddressOperations.GetIpAddress();
        }

        private void StartServerForm_Load(object sender, EventArgs e) => ActiveControl = portNumberNumericUpDown;

        private async void StartServer_Click(object sender, EventArgs e)
        {
            StartServer.Enabled = false;
            StopServer.Enabled = true;
            server = new((int)portNumberNumericUpDown.Value, AskPermission);
            await server.StartAsync();
        }

        private bool AskPermission(string description)
        {
            var result = MessageBox.Show(description, "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            return result == DialogResult.OK;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            StartServer.Enabled = true;
            StopServer.Enabled = false;
            server.Stop();
        }
    }
}