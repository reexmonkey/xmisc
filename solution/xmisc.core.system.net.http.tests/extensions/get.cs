using System.Collections.Generic;
using reexmonkey.xmisc.core.system.net.http.extensions;
using xmisc.core.system.net.http.tests.fixtures;
using Xunit;
using reexmonkey.xmisc.backbone.io.jil.serializers;
using System.Threading.Tasks;

namespace xmisc.core.system.net.http.tests.extensions
{
    public class HttpGetClientExtensionTests
    {

        [Fact]
        public void TestJsonClientGet()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var polls = Fixture.ApiBlueprintClient.Get<List<Poll>>(address, Fixture.JilTextSerializer);

            //assert
            Assert.NotEmpty(polls);
        }

        [Fact]
        public void TestJsonStreamClientGet()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var polls = Fixture.ApiBlueprintClient.Get<List<Poll>>(address, Fixture.JilStreamSerializer);

            //assert
            Assert.NotEmpty(polls);
        }

        [Fact]
        public async Task TestJsonClientGetAsync()
        {
            //arrange
            var address = "questions".ToUri();
            
            //act
            var polls = await Fixture.ApiBlueprintClient.GetAsync<List<Poll>>(address, Fixture.JilTextSerializer);

            //assert
            Assert.NotEmpty(polls);
        }

        [Fact]
        public async Task TestJsonStreamClientGetAsync()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var polls = await Fixture.ApiBlueprintClient.GetAsync<List<Poll>>(address, Fixture.JilStreamSerializer);

            //assert
            Assert.NotEmpty(polls);
        }
    }
}
