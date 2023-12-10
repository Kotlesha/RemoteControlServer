using System.Net;
using System.Net.Sockets;

namespace RemoteControlServer.Controllers.ServerClasses
{
    internal static class IpAddressOperations
    {
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
            string ipAddress = ipAddresses.First(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            return ipAddress;
        }
    }
}
