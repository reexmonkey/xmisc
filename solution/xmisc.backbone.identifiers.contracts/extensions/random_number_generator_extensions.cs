using System;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.extensions
{

    /// <summary>
    /// Provides extension methods for the RandomNumberGenerator class to simplify the generation of random bytes and
    /// unsigned 16-bit integers.   
    /// </summary>
    /// <remarks>These extension methods offer convenient ways to generate random data using an existing
    /// RandomNumberGenerator instance. They are intended to improve code readability and reduce boilerplate when
    /// working with cryptographic random number generation.</remarks>
    public static class RandomNumberGeneratorExtensions
    {
        /// <summary>
        /// Generates a byte array filled with cryptographically strong random values using the specified random number
        /// generator.
        /// </summary>
        /// <param name="generator">The random number generator to use for producing random bytes. Cannot be null.</param>
        /// <param name="buffersize">The number of random bytes to generate. Must be greater than zero.</param>
        /// <returns>A byte array containing cryptographically strong random values of the specified length.</returns>
        public static byte[] Populate(this RandomNumberGenerator generator, int buffersize)
        {
            var buffer = new byte[buffersize];
            generator.GetBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Generates a random 16-bit unsigned integer using the specified random number generator.
        /// </summary>
        /// <param name="generator">The random number generator to use for producing the random value. Cannot be null.</param>
        /// <returns>A randomly generated 16-bit unsigned integer.</returns>
        public static ushort GenerateUInt16(this RandomNumberGenerator generator)
        {
            var generated = generator.Populate(sizeof(ushort));
            return BitConverter.ToUInt16(generated, 0);
        }
    }
}
