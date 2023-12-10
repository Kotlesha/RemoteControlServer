using RemoteControlServer.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace RemoteControlServer.Controllers.ServerClasses
{
    internal class TcpServer
    {
        public TcpListener TcpListener { get; private set; }
        public CancellationTokenSource Cts { get; private set; }
        public int BufferSize { get; private set; }

        private Func<string, bool> askPermission;

        public TcpServer(int port, Func<string, bool> askPermission, int bufferSize = 8_096)
        {
            TcpListener = new(IPAddress.Any, port);
            Cts = new();
            BufferSize = bufferSize;
            this.askPermission = askPermission;
        }

        public async Task StartAsync()
        {
            TcpListener.Start();

            while (!Cts.IsCancellationRequested)
            {
                TcpClient client;
                try
                {
                    client = await TcpListener.AcceptTcpClientAsync();
                }
                catch (SocketException) { return; }
                HandleClientAsync(client);
            }
        }

        public async Task HandleClientAsync(TcpClient tcpClient)
        {
            using (tcpClient)
            using (NetworkStream stream = tcpClient.GetStream())
            {
                byte[] buffer = new byte[BufferSize];

                while (!Cts.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, Cts.Token);
                    string jsonRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Request request;
                    try { request = JsonSerializer.Deserialize<Request>(jsonRequest); }
                    catch { continue; }
                    Response response = await ProcessingRequests.GetResponseOnRequest(request, askPermission);
                    string jsonResponse = JsonSerializer.Serialize(response);
                    byte[] bytesWrite = Encoding.UTF8.GetBytes(jsonResponse);
                    await stream.WriteAsync(bytesWrite, Cts.Token);
                    await stream.FlushAsync();
                }
            }
        }

        public void Stop()
        {
            Cts.Cancel();
            Cts = new();
            TcpListener.Stop();
        }
    }
}
