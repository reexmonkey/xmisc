using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace reexmonkey.xmisc.core.system.net.extensions
{
    public static class NetworkExtensions
    {
        public static IEnumerable<UnicastIPAddressInformation> GetUnicastIPAddressInformation(this IEnumerable<NetworkInterface> interfaces)
            => interfaces.SelectMany(x => x.GetIPProperties().UnicastAddresses);

        public static IEnumerable<IPAddress> GetHostIPv4Addresses()
        {
            var hostname = Dns.GetHostName();
            var host = Dns.GetHostEntry(hostname);
            return host.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork);
        }

        public static IPAddress GetIPv4SubnetMask(this IPAddress address, IEnumerable<UnicastIPAddressInformation> unicastIPAddressInformation)
        {
            foreach (var unicastAddresses in unicastIPAddressInformation)
            {
                if (unicastAddresses.Address.AddressFamily == AddressFamily.InterNetwork
                    && address.Equals(unicastAddresses.Address)) return unicastAddresses.IPv4Mask;
            }
            return default(IPAddress);
        }

        public static IPAddress AsIPAddress(this string address)
            => (IPAddress.TryParse(address, out IPAddress ip))
                ? ip
                : default(IPAddress);

        public static IPAddress GetRootIPv4Address(
            this IEnumerable<IPAddress> addresses,
            IEnumerable<UnicastIPAddressInformation> unicastIPAddressInformation,
            IPAddress rootmask)
            => addresses.FirstOrDefault(x => x.GetIPv4SubnetMask(unicastIPAddressInformation).Equals(rootmask));

        public static IPAddress GetRootIPv4Address(this IEnumerable<IPAddress> addresses, IPAddress rootmask)
        {
            var unicastInfos = NetworkInterface.GetAllNetworkInterfaces().GetUnicastIPAddressInformation();
            return addresses.GetRootIPv4Address(unicastInfos, rootmask);
        }

        public static IPAddress GetHostIPv6Address()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetworkV6);
        }

        public static string ToString(this IPEndPoint endpoint, char delimiter) => endpoint.ToString().Replace(':', delimiter);

        public static string Format(this int port, string protocol) => $"{port.ToString()}/{protocol}";
    }
}
