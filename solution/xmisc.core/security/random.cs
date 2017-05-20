using System;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.core.security
{
    public static class RandomNumberGeneratorExtensions
    {
        public static byte[] Generate(this RandomNumberGenerator generator, int buffersize)
        {
            var buffer = new byte[buffersize];
            generator.GetBytes(buffer);
            return buffer;
        }


        public static short GenerateInt16(this RandomNumberGenerator generator)
        {
            var buffer = new byte[16];
            generator.GetBytes(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        public static ushort GenerateUInt16(this RandomNumberGenerator generator)
        {
            var buffer = new byte[16];
            generator.GetBytes(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }


        public static int GenerateInt32(this RandomNumberGenerator generator)
        {
            var buffer = new byte[32];
            generator.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static uint GenerateUInt32(this RandomNumberGenerator generator)
        {
            var buffer = new byte[32];
            generator.GetBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static long GenerateInt64(this RandomNumberGenerator generator)
        {
            var buffer = new byte[64];
            generator.GetBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static ulong GenerateUInt64(this RandomNumberGenerator generator)
        {
            var buffer = new byte[64];
            generator.GetBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static float GenerateSingle(this RandomNumberGenerator generator)
        {
            var buffer = new byte[32];
            generator.GetBytes(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static double GenerateDouble(this RandomNumberGenerator generator)
        {
            var buffer = new byte[64];
            generator.GetBytes(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
