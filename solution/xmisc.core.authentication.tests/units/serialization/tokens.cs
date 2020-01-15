using reexmonkey.xmisc.core.authentication.tokens;
using reexmonkey.xmisc.core.authentication.types;
using xmisc.core.authentication.tests.fixtures;
using Xunit;
using Xunit.Abstractions;

namespace xmisc.core.authentication.tests.units.serialization
{
    [Collection(nameof(TestCollection))]
    public class TokenSerializationTests
    {
        private readonly ITestOutputHelper console;
        private readonly Fixture fixture;

        public TokenSerializationTests(Fixture fixture, ITestOutputHelper console)
        {
            this.fixture = fixture;
            this.console = console;
        }

        [Fact]
        public void ShouldDeserializeFromJws()
        {
            // arrange
            const string content = "eyJ0eXAiOiJKV1QiLA0KICJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFtcGxlLmNvbS9pc19yb290Ijp0cnVlfQ.dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk";

            //act
            var jws = Jws.Parse(content);

            //assert
            Assert.NotNull(jws);
            Assert.NotNull(jws.Header);
            Assert.Equal("JWT", jws.Header.Typ);
            Assert.Equal(JwsAlg.HS256, jws.Header.Alg);
            Assert.Null(jws.Header.Kid);
            Assert.Null(jws.Header.X5t);

            Assert.NotNull(jws.Payload.Claims);
            Assert.Equal("joe", jws.Payload.Claims[Payload.Iss]);
            Assert.Equal("1300819380", jws.Payload.Claims[Payload.Exp]);
            Assert.Equal("true", jws.Payload.Claims["http://example.com/is_root"]);

            Assert.NotNull(jws.Signature);
            Assert.Equal(new byte[] { 116, 24, 223, 180, 151, 153, 224, 37, 79, 250, 96, 125, 216, 173, 187, 186, 22, 212, 37, 77, 105, 214, 
                191, 240, 91, 88, 5, 88, 83, 132, 141, 121 }, jws.Signature);
        }

        [Fact]
        public void ShouldDeserializeFromJwe()
        {
            // arrange
            const string content = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZHQ00ifQ.OKOawDo13gRp2ojaHV7LFpZcgV7T6DVZKTyKOMTYUmKoTCVJRgckCL9kiMT03JGeipsEdY3mx_etLbbWSrFr05kLzcSr4qKAq7YN7e9jwQRb23nfa6c9d - StnImGyFDbSv04uVuxIp5Zms1gNxKKK2Da14B8S4rzVRltdYwam_lDp5XnZAYpQdb76FdIKLaVmqgfwX7XWRxv2322i - vDxRfqNzo_tETKzpVLzfiwQyeyPGLBIO56YJ7eObdv0je81860ppamavo35UgoRdbYaBcoh9QcfylQr66oc6vFWXRcZ_ZT2LawVCWTIy3brGPi6UklfCpIMfIjf7iGdXKHzg.48V1_ALb6US04U3b.5eym8TW_c8SuK0ltJ3rpYIzOeDQz7TALvtu6UG9oMo4vpzs9tX_EFShS8iB7j6jiSdiwkIr3ajwQzaBtQD_A.XFBoMYUZodetZdvTiFvSkQ";

            //act
            var jwe = Jwe.Parse(content);

            //assert
            Assert.NotNull(jwe);
            Assert.NotNull(jwe.Header);
            Assert.Equal(JweAlg.RSA_OAEP, jwe.Header.Alg);
            Assert.Equal("A256GCM", jwe.Header.Enc);

            Assert.Null(jwe.Header.Kid);
            Assert.Null(jwe.Header.X5t);

            Assert.NotNull(jwe.EncryptedKey);
            Assert.Equal(new byte[] {56, 163, 154, 192, 58, 53, 222, 4, 105, 218, 136, 218, 29, 94, 203,
                22, 150, 92, 129, 94, 211, 232, 53, 89, 41, 60, 138, 56, 196, 216,
                82, 98, 168, 76, 37, 73, 70, 7, 36, 8, 191, 100, 136, 196, 244, 220,
                145, 158, 138, 155, 4, 117, 141, 230, 199, 247, 173, 45, 182, 214,
                74, 177, 107, 211, 153, 11, 205, 196, 171, 226, 162, 128, 171, 182,
                13, 237, 239, 99, 193, 4, 91, 219, 121, 223, 107, 167, 61, 119, 228,
                173, 156, 137, 134, 200, 80, 219, 74, 253, 56, 185, 91, 177, 34, 158,
                89, 154, 205, 96, 55, 18, 138, 43, 96, 218, 215, 128, 124, 75, 138,
                243, 85, 25, 109, 117, 140, 26, 155, 249, 67, 167, 149, 231, 100, 6,
                41, 65, 214, 251, 232, 87, 72, 40, 182, 149, 154, 168, 31, 193, 126,
                215, 89, 28, 111, 219, 125, 182, 139, 235, 195, 197, 23, 234, 55, 58,
                63, 180, 68, 202, 206, 149, 75, 205, 248, 176, 67, 39, 178, 60, 98,
                193, 32, 238, 122, 96, 158, 222, 57, 183, 111, 210, 55, 188, 215,
                206, 180, 166, 150, 166, 106, 250, 55, 229, 72, 40, 69, 214, 216,
                104, 23, 40, 135, 212, 28, 127, 41, 80, 175, 174, 168, 115, 171, 197,
                89, 116, 92, 103, 246, 83, 216, 182, 176, 84, 37, 147, 35, 45, 219,
                172, 99, 226, 233, 73, 37, 124, 42, 72, 49, 242, 35, 127, 184, 134,
                117, 114, 135, 206 }, jwe.EncryptedKey);

            Assert.NotNull(jwe.InitializationVector);
            Assert.Equal(new byte[] { 227, 197, 117, 252, 2, 219, 233, 68, 180, 225, 77, 219 }, jwe.InitializationVector);

            Assert.NotNull(jwe.Ciphertext);
            Assert.Equal(new byte[] { 229, 236, 166, 241, 53, 191, 115, 196, 174, 43, 73, 109, 39, 122,
                233, 96, 140, 206, 120, 52, 51, 237, 48, 11, 190, 219, 186, 80, 111,
                104, 50, 142, 47, 167, 59, 61, 181, 127, 196, 21, 40, 82, 242, 32,
                123, 143, 168, 226, 73, 216, 176, 144, 138, 247, 106, 60, 16, 205, 160, 109, 64, 63, 192}, jwe.Ciphertext);
            
            Assert.NotNull(jwe.AuthenticationTag);
            Assert.Equal(new byte[] { 92, 80, 104, 49, 133, 25, 161, 215, 173, 101, 219, 211, 136, 91, 210, 145 }, jwe.AuthenticationTag);
        }
    }
}