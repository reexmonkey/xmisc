using reexmonkey.xmisc.core.system.net.http.extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using xmisc.core.system.net.http.tests.fixtures;
using Xunit;

namespace reexmonkey.xmisc.core.system.net.http.tests.extensions
{
    public class HttpPostClientExtensionTests
    {
        [Fact]
        public void TestJsonClientPost()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var response = Fixture.Client.Post(address, Fixture.FavoriteTvSeries, Fixture.JilTextSerializer);

            //assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestJsonClientPostAsync()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var response = await Fixture.Client.PostAsync(address, Fixture.FavoriteTvSeries, Fixture.JilTextSerializer);

            //assert
            Assert.True(response.IsSuccessStatusCode);
        }

        
        public void TestJsonStreamClientPost()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var response = Fixture.Client.Post(address, Fixture.FavoriteTvSeries, Fixture.JilStreamSerializer);

            //assert
            Assert.True(response.IsSuccessStatusCode);
        }

       
        public async Task TestJsonStreamClientPostAsync()
        {
            //arrange
            var address = "questions".ToUri();

            //act
            var response = await Fixture.Client.PostAsync(address, Fixture.FavoriteTvSeries, Fixture.JilStreamSerializer);

            //assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}