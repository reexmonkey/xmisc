using reexmonkey.xmisc.core.authentication.keys;
using reexmonkey.xmisc.core.authentication.types;
using xmisc.core.authentication.tests.fixtures;
using Xunit;
using Xunit.Abstractions;

namespace xmisc.core.authentication.tests.units.serialization
{
    [Collection(nameof(TestCollection))]
    public class KeySerializationTests
    {
        private readonly ITestOutputHelper console;
        private readonly Fixture fixture;

        public KeySerializationTests(Fixture fixture, ITestOutputHelper console)
        {
            this.fixture = fixture;
            this.console = console;
        }

        [Fact]
        public void ShouldDeserializeFromJwk()
        {
            // arrange
            var content = fixture.LoadKeyFile(Fixture.PUBLIC_KEY_RSA);

            //act
            var jwk = RsaPublicJwk.Parse(content);

            //assert
            Assert.NotNull(jwk);
            Assert.Equal(Kty.RSA, jwk.Kty);
            Assert.Equal("RS256", JwsAlg.RS256.ToString("G"));
            Assert.NotEmpty(jwk.X5c);
            Assert.Equal("MIIDRTCCAi2gAwIBAgIQ02VvFO51GopGDV2rhEoj3TANBgkqhkiG9w0BAQsFADAsMSowKAYDVQQDEyFUaGVPcHRpbWFsQ2xvdWRTaWduaW5nQ2VydFB3Q1Byb2QwIBcNMTkwOTAxMDQwMDAwWhgPMjA1MDEyMzEwNDAwMDBaMCwxKjAoBgNVBAMTIVRoZU9wdGltYWxDbG91ZFNpZ25pbmdDZXJ0UHdDUHJvZDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJrl33zbaiUmDI1ykl+q2VoogZXZqvzhu1X530Cz1sJJkkxwuQS4bNTGKc36ZZTFcgQYmg74sgCc9CvqMU1fyuejAPEu1XdF/pj7ufI+s7HtPtlUilFs2KUPGhS4qq5Hdgpz0AoGZZoFS/zAf3OX5hcoWjKNpgw5Vd0HBjHcyLdcNT/LrakY1Tk/qVA0RUF9f4LRnUjieiUasqjtdsgxUmSUYMcNT21v+Pe4hpjvfoxJ8i3H/UK6W0UrrZRiQSPx/h8/fpi/XKNOcMqOu52EzO49KLl2ii3oYlbuXpwJ/RzoMEF6tA0X9U6lhtktgGBAaYSsIcbl1CIp3oUgWCxenuECAwEAAaNhMF8wXQYDVR0BBFYwVIAQ2sd+x03iPJAgaY0W6GSYn6EuMCwxKjAoBgNVBAMTIVRoZU9wdGltYWxDbG91ZFNpZ25pbmdDZXJ0UHdDUHJvZIIQ02VvFO51GopGDV2rhEoj3TANBgkqhkiG9w0BAQsFAAOCAQEAD/zdqTtUEXYs4TduqijvF1YwZ613Rxo+e7qzKDWO7O0YIDKzXKqjqdUmQSD/7QiMvmaOYF6uSfjyhpVsHSA/D0S0Vz64tsLL1iLe4axD0jwF8Z8/MDgazvfmp+8zui3+RsGr3/VvPLn+XQkxv5OCglgW7I8n0qJT/KK4z6q6bbt8bj8NPWejDTFg4+Dyk4YYF6qgYVGM9c4jjni6Iexvmv3pjJFsLYvN2jqthYxcY4po23YmhNSim2fDV+jRX9Nh8Dg9fB92TaklleftfZEH89cQIhcepAdWGAb6dgp/ZSv6FWoueik6OOiI6QwkOqsq122CkkFVuA2OL1qdwF45kQ==", jwk.X5c[0]);
            Assert.Equal("ieGRNwpIczLOL6y1WJrNVmxKMXY", jwk.X5t);
            Assert.Equal("AQAB", jwk.E);
            Assert.Equal("muXffNtqJSYMjXKSX6rZWiiBldmq_OG7VfnfQLPWwkmSTHC5BLhs1MYpzfpllMVyBBiaDviyAJz0K-oxTV_K56MA8S7Vd0X-mPu58j6zse0-2VSKUWzYpQ8aFLiqrkd2CnPQCgZlmgVL_MB_c5fmFyhaMo2mDDlV3QcGMdzIt1w1P8utqRjVOT-pUDRFQX1_gtGdSOJ6JRqyqO12yDFSZJRgxw1PbW_497iGmO9-jEnyLcf9QrpbRSutlGJBI_H-Hz9-mL9co05wyo67nYTM7j0ouXaKLehiVu5enAn9HOgwQXq0DRf1TqWG2S2AYEBphKwhxuXUIinehSBYLF6e4Q", jwk.N);
        }

        [Fact]
        public void ShouldDeserializeFromPublicKeySet()
        {
            // arrange
            var content = fixture.LoadKeyFile(Fixture.PUBLIC_KEY_SET_RSA);

            //act
            var jwks = JwkSet.Parse(content);

            //assert
            Assert.NotNull(jwks);
            Assert.NotEmpty(jwks.Keys);
        }

        [Fact]
        public void ShouldDeserializeFromPrivateKeySet()
        {
            // arrange
            var content = fixture.LoadKeyFile(Fixture.PRIVATE_KEY_SET);

            //act
            var jwks = JwkSet.Parse(content);

            //assert
            Assert.NotNull(jwks);
            Assert.NotEmpty(jwks.Keys);
            Assert.True(jwks.Keys[0].Kty == Kty.EC);
            Assert.True(jwks.Keys[1].Kty == Kty.RSA);
        }

        [Fact]
        public void ShouldSerializeJwkToJson()
        {
            // arrange
            var content = fixture.LoadKeyFile(Fixture.PUBLIC_KEY_RSA);
            var jwk = RsaPublicJwk.Parse(content);

            //act
            var json = jwk.ToString();

            //assert
            Assert.NotNull(json);
            console.WriteLine(json);
        }

        [Fact]
        public void ShouldSerializeToPublicKeySetToJson()
        {
            // arrange
            var content = fixture.LoadKeyFile(Fixture.PUBLIC_KEY_SET_RSA);
            var jwks = JwkSet.Parse(content);

            //act
            var json = jwks.ToString();

            //assert
            Assert.NotNull(json);
            console.WriteLine(json);
        }

        [Fact]
        public void ShouldSerializeToPrivateKeySetToJson()
        {
            // arrange
            var content = fixture.LoadKeyFile(Fixture.PRIVATE_KEY_SET);
            var jwks = JwkSet.Parse(content);

            //act
            var json = jwks.ToString();

            //assert
            Assert.NotNull(json);
            console.WriteLine(json);
        }
    }
}