using reexmonkey.xmisc.core.text.extensions;
using System.Text;
using Xunit;

namespace reexmonkey.xmisc.core.text.tests.extensions
{
    public class StringExtensionTests
    {
        [Fact]
        public void TestExtractHexDigits()
        {
            var data = "xxxx0123456789ABCDEFxxxx";
            var expected = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            var result = data.ExtractHexDigits();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestFindAndPrepend()
        {
            var data = "The king is asleep";
            var expected = "The lion king is asleep";
            var result = data.FindAndPrepend("lion ", @"\bking\b", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestFindAndAppend()
        {
            var data = "The lion is asleep";
            var expected = "The lion king is asleep";
            var result = data.FindAndAppend(" king", @"\blion\b", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestFoldLines()
        {
            var encoding = new UTF8Encoding(false);
            var samplebytes = encoding.GetBytes("DESCRIPTION:This is a long text that may not fit on on the same line and hence needs to be folded.");
            var samplestring = encoding.GetString(samplebytes);
            var firstchunk = samplebytes.Extract(0, 75).Combine(encoding.GetBytes("\r\n"));
            var secondchunk = encoding.GetBytes(" ").Combine(samplebytes.Extract(75, samplebytes.Length - 75));
            var expectedstring = encoding.GetString(firstchunk.Combine(secondchunk));

            var result = samplestring.FoldLines(75, encoding);
            Assert.Equal(expectedstring, result);
        }

        [Fact]
        public void TestUnfoldLines()
        {
            var encoding = new UTF8Encoding(false);
            var samplebytes = encoding.GetBytes("DESCRIPTION:This is a long text that may not fit on on the same line and he\r\n nce needs to be folded.");
            var samplestring = encoding.GetString(samplebytes);
            var expectedbytes = encoding.GetBytes("DESCRIPTION:This is a long text that may not fit on on the same line and hence needs to be folded.");
            var expectedstring = encoding.GetString(expectedbytes);

            var result = samplestring.UnfoldLines();
            Assert.Equal(expectedstring, result);
        }
    }
}
