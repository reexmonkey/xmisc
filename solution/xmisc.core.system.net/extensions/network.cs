using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.extensions
{
    public static class NetworkExtensions
    {
        public static string LoopbackIPv4 = "127.0.0.1";

        public static string LoopbackIPv6 = "::1";

        public static string GoogleIPv4Host = "8.8.8.8";

        public static string GoogleIPv6Host = "2001:4860:4860::8888";

        private static IPAddress[] GetIPAddresses(string hostNameOrAddress) => Dns.GetHostAddresses(hostNameOrAddress);

        private static IPAddress GetIPAddress(string remoteHost, int port, AddressFamily family)
        {
            using (var socket = new Socket(family, SocketType.Dgram, 0))
            {
                socket.Connect(remoteHost, port);
                var endpoint = socket.LocalEndPoint as IPEndPoint;
                return endpoint.Address;
            }
        }

        private static Task<IPAddress[]> GetIPAddressesAsync(string hostNameOrAddress, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            return Dns.GetHostAddressesAsync(hostNameOrAddress);
        }

        private async static Task<IPAddress> GetIPAddressAsync(string remoteHost, int port, AddressFamily family, CancellationToken token = default)
        {
            using (var socket = new Socket(family, SocketType.Dgram, 0))
            {
                await socket.ConnectAsync(remoteHost, port).ConfigureAwait(false);
                token.ThrowIfCancellationRequested();
                var endpoint = socket.LocalEndPoint as IPEndPoint;
                return endpoint.Address;
            }
        }

        public static UnicastIPAddressInformation[] GetUnicastIPAddressInformation(this IEnumerable<NetworkInterface> interfaces)
    => interfaces.SelectMany(x => x.GetIPProperties().UnicastAddresses).ToArray();

        public static IPAddress GetIPv4SubnetMask(this IPAddress address, IEnumerable<UnicastIPAddressInformation> unicastIPAddressInformation)
        {
            foreach (var unicastAddresses in unicastIPAddressInformation)
            {
                if (unicastAddresses.Address.AddressFamily == AddressFamily.InterNetwork && address.Equals(unicastAddresses.Address))
                    return unicastAddresses.IPv4Mask;
            }
            return default;
        }

        public static IPAddress AsIPAddress(this string address)
            => IPAddress.TryParse(address, out IPAddress value) ? value : default;

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

        public static string GetDnsHostName() => Dns.GetHostName();

        public static bool NetworkAvailable() => NetworkInterface.GetIsNetworkAvailable();

        public static IPAddress GetLocalIPv4Address(string remoteHost, int port, Func<bool> networkAvailableFunc)
            => networkAvailableFunc() ? GetIPAddress(remoteHost, port, AddressFamily.InterNetwork) : LoopbackIPv4.AsIPAddress();

        public static IPAddress GetLocalIPv6Address(string remoteHost, int port, Func<bool> networkAvailableFunc)
            => networkAvailableFunc() ? GetIPAddress(remoteHost, port, AddressFamily.InterNetworkV6) : LoopbackIPv6.AsIPAddress();

        public static IPAddress[] GetLocalIPv4Addresses(string hostNameOrAddress)
            => GetIPAddresses(hostNameOrAddress).Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();

        public static IPAddress[] GetLocalIPv6Addresses(string hostNameOrAddress)
            => GetIPAddresses(hostNameOrAddress).Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).ToArray();

        public static string ToString(this IPEndPoint endpoint, char delimiter) => endpoint.ToString().Replace(':', delimiter);

        public static string Format(this int port, string protocol) => $"{port.ToString()}/{protocol}";

        public async static Task<IPAddress> GetLocalIPv4AddressAsync(string remoteHost, int port, Func<bool> networkAvailableFunc, CancellationToken token = default)
        {
            return networkAvailableFunc()
                ? await GetIPAddressAsync(remoteHost, port, AddressFamily.InterNetwork, token).ConfigureAwait(false)
                : LoopbackIPv4.AsIPAddress();
        }

        public async static Task<IPAddress> GetLocalIPv6AddressAsync(string remoteHost, int port, Func<bool> networkAvailableFunc, CancellationToken token = default)
        {
            return networkAvailableFunc()
                ? await GetIPAddressAsync(remoteHost, port, AddressFamily.InterNetwork, token).ConfigureAwait(false)
                : LoopbackIPv4.AsIPAddress();
        }

        public async static Task<IPAddress[]> GetLocalIPv4AddressesAsync(string hostNameOrAddress, CancellationToken token = default)
        {
            var addresses = await GetIPAddressesAsync(hostNameOrAddress, token).ConfigureAwait(false);
            return addresses.Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
        }

        public static async Task<IPAddress[]> GetLocalIPv6AddressesAsync(string hostNameOrAddress, CancellationToken token = default)
        {
            var addresses = await GetIPAddressesAsync(hostNameOrAddress, token).ConfigureAwait(false);
            return addresses.Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).ToArray();
        }
    }
}
