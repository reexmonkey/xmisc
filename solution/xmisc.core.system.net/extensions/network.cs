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

    /// <summary>
    /// Network related extension methods.
    /// </summary>
    public static class NetworkExtensions
    {
        /// <summary>
        /// The IPv4 loopback address.
        /// </summary>
        public static readonly string LoopbackIPv4 = "127.0.0.1";

        /// <summary>
        /// The IPv6 loopback address.
        /// </summary>
        public static readonly string LoopbackIPv6 = "::1";

        /// <summary>
        /// Google Public DNS IPv4 address.
        /// </summary>
        public static readonly string GoogleIPv4Host = "8.8.8.8";

        /// <summary>
        /// Google Public DNS IPv6 address.
        /// </summary>
        public static readonly string GoogleIPv6Host = "2001:4860:4860::8888";

        private static IPAddress[] GetIPAddresses(string hostNameOrAddress) => Dns.GetHostAddresses(hostNameOrAddress);

        private static IPAddress GetIPAddress(string remoteHost, int port, AddressFamily family)
        {
            using var socket = new Socket(family, SocketType.Dgram, 0);
            socket.Connect(remoteHost, port);
            var endpoint = socket.LocalEndPoint as IPEndPoint;
            return endpoint.Address;
        }

        private static Task<IPAddress[]> GetIPAddressesAsync(string hostNameOrAddress, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            return Dns.GetHostAddressesAsync(hostNameOrAddress);
        }

        private async static Task<IPAddress> GetIPAddressAsync(string remoteHost, int port, AddressFamily family, CancellationToken token = default)
        {
            using var socket = new Socket(family, SocketType.Dgram, 0);
            await socket.ConnectAsync(remoteHost, port).ConfigureAwait(false);
            token.ThrowIfCancellationRequested();
            var endpoint = socket.LocalEndPoint as IPEndPoint;
            return endpoint.Address;
        }

        /// <summary>
        /// Gets the unicast IP address information from a collection of network interfaces.
        /// </summary>
        /// <param name="interfaces">The collection of network interfaces.</param>
        /// <returns>The unicast IP address information.</returns>
        public static UnicastIPAddressInformation[] GetUnicastIPAddressInformation(this IEnumerable<NetworkInterface> interfaces)
    => interfaces.SelectMany(x => x.GetIPProperties().UnicastAddresses).ToArray();

        /// <summary>
        /// Retrieves the IPv4 subnet mask associated with the specified IP address from a collection of unicast address
        /// information.
        /// </summary>
        /// <remarks>Only IPv4 addresses are considered. If the specified address does not match any entry
        /// in the collection, the method returns null.</remarks>
        /// <param name="address">The IPv4 address for which to obtain the subnet mask.</param>
        /// <param name="unicastIPAddressInformation">A collection of unicast IP address information objects to search for the subnet mask corresponding to the
        /// specified address.</param>
        /// <returns>An IPAddress representing the IPv4 subnet mask for the specified address, or null if no matching address is
        /// found.</returns>
        public static IPAddress GetIPv4SubnetMask(this IPAddress address, IEnumerable<UnicastIPAddressInformation> unicastIPAddressInformation)
        {
            foreach (var unicastAddresses in unicastIPAddressInformation)
            {
                if (unicastAddresses.Address.AddressFamily == AddressFamily.InterNetwork && address.Equals(unicastAddresses.Address))
                    return unicastAddresses.IPv4Mask;
            }
            return default;
        }

        /// <summary>
        /// Retrieves the IPAddress representation of the specified string address.
        /// </summary>
        /// <param name="address">The string representation of the IP address.</param>
        /// <returns>The IPAddress representation of the specified string address, or null if the address is invalid.</returns>
        public static IPAddress AsIPAddress(this string address)
            => IPAddress.TryParse(address, out IPAddress value) ? value : default;

        /// <summary>
        /// Retrieves the IPv4 address from a collection of IP addresses that matches the specified root subnet mask.
        /// </summary>
        /// <param name="addresses">The collection of IP addresses to search.</param>
        /// <param name="unicastIPAddressInformation">A collection of unicast IP address information objects to use for subnet mask matching.</param>
        /// <param name="rootmask">The root subnet mask to match against.</param>
        /// <returns>The matching IPv4 address, or null if no match is found.</returns>
        public static IPAddress GetRootIPv4Address(
            this IEnumerable<IPAddress> addresses,
            IEnumerable<UnicastIPAddressInformation> unicastIPAddressInformation,
            IPAddress rootmask)
            => addresses.FirstOrDefault(x => x.GetIPv4SubnetMask(unicastIPAddressInformation).Equals(rootmask));

        /// <summary>
        /// Finds the first IPv4 address from the specified collection that matches the given root network mask.
        /// </summary>
        /// <remarks>This method uses the system's network interface information to assist in determining
        /// the root IPv4 address. Only IPv4 addresses are considered; IPv6 addresses are ignored.</remarks>
        /// <param name="addresses">A collection of IP addresses to search for a matching root IPv4 address.</param>
        /// <param name="rootmask">The IPv4 network mask used to determine the root network segment. Must not be null.</param>
        /// <returns>An IPv4 address from the collection that matches the specified root network mask; or null if no matching
        /// address is found.</returns>
        public static IPAddress GetRootIPv4Address(this IEnumerable<IPAddress> addresses, IPAddress rootmask)
        {
            var unicastInfos = NetworkInterface.GetAllNetworkInterfaces().GetUnicastIPAddressInformation();
            return addresses.GetRootIPv4Address(unicastInfos, rootmask);
        }

        /// <summary>
        /// Retrieves the host machine's primary IPv6 address or null if none is found.
        /// </summary>
        /// <returns>The primary IPv6 address of the host machine, or null if none is found.</returns>
        public static IPAddress GetHostIPv6Address()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetworkV6);
        }

        /// <summary>
        /// Retrieves the DNS host name of the local computer.
        /// </summary>
        /// <returns>A string containing the DNS host name of the local computer.</returns>
        public static string GetDnsHostName() => Dns.GetHostName();

        /// <summary>
        /// Indicates whether any network connection is available.
        /// </summary>
        /// <remarks>This method checks for the availability of any network connection, including both
        /// wired and wireless connections. It does not guarantee that a specific network or the Internet is reachable,
        /// only that some network interface is marked as available.</remarks>
        /// <returns>true if a network connection is available; otherwise, false.</returns>
        public static bool NetworkAvailable() => NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Retrieves the local IPv4 address used to connect to a specified remote host and port.
        /// </summary>
        /// <param name="remoteHost">The remote host name or IP address.</param>
        /// <param name="port">The remote port number.</param>
        /// <param name="networkAvailableFunc">A function that checks for network availability.</param>
        /// <returns>The local IPv4 address used to connect to the specified remote host and port.</returns>
        public static IPAddress GetLocalIPv4Address(string remoteHost, int port, Func<bool> networkAvailableFunc)
            => networkAvailableFunc() ? GetIPAddress(remoteHost, port, AddressFamily.InterNetwork) : LoopbackIPv4.AsIPAddress();

        /// <summary>
        /// Retrieves the local IPv6 address that would be used to connect to the specified remote host and port.
        /// </summary>
        /// <param name="remoteHost">The DNS name or IP address of the remote host to which a connection is intended.</param>
        /// <param name="port">The port number on the remote host to connect to. Must be between 0 and 65535.</param>
        /// <param name="networkAvailableFunc">A delegate that returns <see langword="true"/> if the network is available; otherwise, <see
        /// langword="false"/>. Used to determine whether to attempt address resolution.</param>
        /// <returns>An <see cref="IPAddress"/> representing the local IPv6 address used for the connection. If the network is
        /// unavailable, returns the IPv6 loopback address.</returns>
        public static IPAddress GetLocalIPv6Address(string remoteHost, int port, Func<bool> networkAvailableFunc)
            => networkAvailableFunc() ? GetIPAddress(remoteHost, port, AddressFamily.InterNetworkV6) : LoopbackIPv6.AsIPAddress();

        /// <summary>
        /// Retrieves all local IPv4 addresses associated with the specified host name or IP address.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
        /// <returns>An array of <see cref="IPAddress"/> instances representing the local IPv4 addresses.</returns>
        public static IPAddress[] GetLocalIPv4Addresses(string hostNameOrAddress)
            => GetIPAddresses(hostNameOrAddress).Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();

        /// <summary>
        /// Retrieves all IPv6 addresses associated with the specified host name or IP address.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or IP address to resolve. This value cannot be null or empty.</param>
        /// <returns>An array of <see cref="System.Net.IPAddress"/> objects containing the IPv6 addresses for the specified host.
        /// The array is empty if no IPv6 addresses are found.</returns>
        public static IPAddress[] GetLocalIPv6Addresses(string hostNameOrAddress)
            => GetIPAddresses(hostNameOrAddress).Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).ToArray();

        /// <summary>
        /// Formats the IPEndPoint as a string using the specified delimiter between the address and port.
        /// </summary>
        /// <param name="endpoint">The IP endpoint to format.</param>
        /// <param name="delimiter">The delimiter to use between the address and port.</param>
        /// <returns>A string representation of the IPEndPoint with the specified delimiter.</returns>
        public static string ToString(this IPEndPoint endpoint, char delimiter) => endpoint.ToString().Replace(':', delimiter);

        /// <summary>
        /// Formats the specified port and protocol as a string in the form "port/protocol".
        /// </summary>
        /// <param name="port">The network port number to include in the formatted string.</param>
        /// <param name="protocol">The protocol name to include after the port. Cannot be null.</param>
        /// <returns>A string that combines the port and protocol in the format "port/protocol".</returns>
        public static string Format(this int port, string protocol) => $"{port.ToString()}/{protocol}";

        /// <summary>
        /// Retrieves the local IPv4 address used to connect to a specified remote host and port asynchronously.
        /// </summary>
        /// <param name="remoteHost">The remote host to connect to.</param>
        /// <param name="port">The port to connect to on the remote host.</param>
        /// <param name="networkAvailableFunc">A function that determines if the network is available.</param>
        /// <param name="token">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the local IPv4 <see cref="IPAddress"/> used for the connection, or the loopback IPv4 address if the network is unavailable.</returns>
        public async static Task<IPAddress> GetLocalIPv4AddressAsync(string remoteHost, int port, Func<bool> networkAvailableFunc, CancellationToken token = default)
        {
            return networkAvailableFunc()
                ? await GetIPAddressAsync(remoteHost, port, AddressFamily.InterNetwork, token).ConfigureAwait(false)
                : LoopbackIPv4.AsIPAddress();
        }

        /// <summary>
        /// Retrieves the local IPv6 address that would be used to connect to the specified remote host and port asynchronously.
        /// </summary>
        /// <param name="remoteHost">The remote host to connect to.</param>
        /// <param name="port">The port to connect to on the remote host.</param>
        /// <param name="networkAvailableFunc">A function that determines if the network is available.</param>
        /// <param name="token">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the local IPv6 <see cref="IPAddress"/> used for the connection, or the loopback IPv4 address if the network is unavailable.</returns>
        public async static Task<IPAddress> GetLocalIPv6AddressAsync(string remoteHost, int port, Func<bool> networkAvailableFunc, CancellationToken token = default)
        {
            return networkAvailableFunc()
                ? await GetIPAddressAsync(remoteHost, port, AddressFamily.InterNetwork, token).ConfigureAwait(false)
                : LoopbackIPv4.AsIPAddress();
        }

        /// <summary>
        /// Retrieves all local IPv4 addresses associated with the specified host name or IP address asynchronously.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
        /// <param name="token">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an array of local IPv4 addresses.</returns>
        public async static Task<IPAddress[]> GetLocalIPv4AddressesAsync(string hostNameOrAddress, CancellationToken token = default)
        {
            var addresses = await GetIPAddressesAsync(hostNameOrAddress, token).ConfigureAwait(false);
            return addresses.Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
        }

        /// <summary>
        /// Asynchronously retrieves all IPv6 addresses associated with the specified host name or IP address.
        /// </summary>
        /// <remarks>Only addresses with the AddressFamily set to InterNetworkV6 are included in the
        /// result. The method does not return IPv4 addresses.</remarks>
        /// <param name="hostNameOrAddress">The host name or IP address to resolve. This value cannot be null or empty.</param>
        /// <param name="token">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an array of IPv6 addresses
        /// associated with the specified host. The array is empty if no IPv6 addresses are found.</returns>
        public static async Task<IPAddress[]> GetLocalIPv6AddressesAsync(string hostNameOrAddress, CancellationToken token = default)
        {
            var addresses = await GetIPAddressesAsync(hostNameOrAddress, token).ConfigureAwait(false);
            return addresses.Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).ToArray();
        }
    }
}
