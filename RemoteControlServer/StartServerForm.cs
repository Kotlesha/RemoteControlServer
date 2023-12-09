using System.Net;

namespace RemoteControlServer
{
    public partial class StartServerForm : Form
    {
        private static TcpServer server;

        public StartServerForm()
        {
            InitializeComponent();
            ipAdressTextBox.Text = TcpServer.GetIpAdress();
        }

        private void StartServerForm_Load(object sender, EventArgs e) => ActiveControl = portNumberNumericUpDown;

        private async void StartServer_Click(object sender, EventArgs e)
        {
            StartServer.Enabled = false;
            StopServer.Enabled = true;
            server = new((int)portNumberNumericUpDown.Value, AskPermission);
            await server.StartAsync();
        }

        private bool AskPermission(string name)
        {
            var result = MessageBox.Show($"� ��� ����� ����������� ������������ {name}. ������ �� ��� �������� ��� ������ ��� ����, ����� ���������� �� ���������� ������ ������", "������", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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