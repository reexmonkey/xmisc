using NodaTime;
using reexmonkey.xmisc.core.security;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using xmisc.backbone.identity.concretes.infrastructure;

namespace xmisc.backbone.identity.concretes.extensions
{
    /// <summary>
    /// Extends GUID features
    /// </summary>
    public static class GuidExtensions
    {
        private static readonly DateTimeZone UtcZone = DateTimeZoneProviders.Tzdb["UTC"];
        private static readonly ZonedDateTime Epoch = new ZonedDateTime(new LocalDateTime(1582, 10, 15, 0, 0, 0), UtcZone, Offset.Zero);
        private static readonly object Mutex = new object();
        private static ulong last;
        private static ushort sequence = new RNGCryptoServiceProvider().GenerateUInt16();
        private static readonly Random Randomizer = new Random();

        private static ulong GetVersion1Timestamp()
        {
            var clock = new ZonedClock(SystemClock.Instance, UtcZone, CalendarSystem.Iso);
            var timestamp = (ulong)((clock.GetCurrentInstant() - Epoch.ToInstant()).TotalNanoseconds / 100d);
            lock (Mutex)
            {
                if (last == timestamp) sequence++;
                last = timestamp;
            }
            return timestamp;
        }

        private static byte[] GeneratePhysicalNode()
        {
            var node = new byte[6];
            var address = NetworkInterface
                .GetAllNetworkInterfaces()
                .Select(x => x.GetPhysicalAddress())
                .FirstOrDefault();
            var bytes = address?.GetAddressBytes();
            if (bytes != null) Array.Copy(bytes, 0, node, 0, node.Length);
            return node;
        }

        private static State CreateState(byte[] node)
            => new State(GetVersion1Timestamp(), sequence, node[0], node[1], node[2], node[3], node[4], node[5]);

        public static Guid AsVersion1Guid(this Guid guid)
        {
            var source = Encoding.UTF8.GetBytes(Environment.MachineName + Environment.OSVersion.ToString());
            var hash = source.GetSaltedHash(new RNGCryptoServiceProvider(), 20, new SHA1CryptoServiceProvider());

            var node = hash.RandomSelect(new byte[6], Randomizer); //new RNGCryptoServiceProvider().Generate(6);
            node[0] |= 1; //multicast bit set to one, in order to avoid conflict with IEEE 802 network cards
            return guid.AsVersion1Guid(CreateState(node));
        }

        private static Guid AsVersion1Guid(this Guid guid, State state)
        {
            if (guid == Guid.Empty) return Guid.Empty;

            var guidbytes = guid.ToByteArray();

            //Assign time - low: 0-3
            Array.Copy(BitConverter.GetBytes(state.Timestamp & 0xFFFFFFFF), 0, guidbytes, 0, 4);

            //Assign times - mid: 4-5
            Array.Copy(BitConverter.GetBytes((ushort)((state.Timestamp >> 32) & 0xFFFF)), 0, guidbytes, 4, 2);

            //Assign time - hi and version: 6-7
            Array.Copy(BitConverter.GetBytes((ushort)((state.Timestamp >> 48) & 0X0FFF | (1 << 12))), 0, guidbytes, 6, 2);

            //Assign clock sequence - low: 8
            guidbytes[8] = (byte)(state.Sequence & 0xFF);

            //clock sequence hi and reserved: 9
            guidbytes[9] = (byte)(((guidbytes[8] & 0x3F00) >> 8) | 0x80);

            guidbytes[10] = state.N0;
            guidbytes[11] = state.N1;
            guidbytes[12] = state.N2;
            guidbytes[13] = state.N3;
            guidbytes[14] = state.N4;
            guidbytes[15] = state.N5;

            return new Guid(guidbytes);
        }

        private static T[] RandomSelect<T>(this T[] source, T[] dest, Random randomizer)
        {
            for (int i = 0; i < dest.Length; i++)
            {
                var k = randomizer.Next(0, source.Length);
                dest[i] = source[k];
            }
            return dest;
        }
    }
}