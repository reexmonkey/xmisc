using System.Collections.Generic;
using FizzWare.NBuilder;
using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.foundation.contracts;
using reexjungle.xmisc.infrastructure.concretes.operations;
using Xunit;

namespace xmisc.tests.infrastructure
{
    public class GeneratorTests
    {
       
        [Fact]
        public void CheckNonStandardFpiGneration()
        {
            var rndGenerator = new RandomGenerator();
            var generator = new FpiKeyGenerator(
                new ContentGenerator<ApprovalStatus>(() => Pick<ApprovalStatus>.RandomItemFrom(new[]
                {
                    ApprovalStatus.Informal, 
                    ApprovalStatus.None
                })),

                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "RXJG",
                    "GOGL",
                    "MSFT",
                    "YHOO"
                })),

                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "DTD",
                    "XSL",
                    "XML",
                    "JSON"
                })),
                new ContentGenerator<string>(() => rndGenerator.Phrase(20)),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "EN",
                    "FR",
                    "DE",
                    "ES",
                    "IT",
                    "PL",
                    "RO"
                })));

            Assert.NotEqual(generator.GetNext(), null);
        }

        [Fact]
        public void CheckMultipleNonStandardFpiGeneration()
        {
            var rndGenerator = new RandomGenerator();
            var generator = new FpiKeyGenerator(
                new ContentGenerator<ApprovalStatus>(() => Pick<ApprovalStatus>.RandomItemFrom(new[]
                {
                    ApprovalStatus.Informal, 
                    ApprovalStatus.None
                })),

                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "RXJG",
                    "GOGL",
                    "MSFT",
                    "YHOO"
                })),

                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "DTD",
                    "XSL",
                    "XML",
                    "JSON"
                })),
                new ContentGenerator<string>(() => rndGenerator.Phrase(20)),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "EN",
                    "FR",
                    "DE",
                    "ES",
                    "IT",
                    "PL",
                    "RO"
                })));

            var fpis = new List<Fpi>();
            for (var i = 0; i < 5; i++)
            {
                fpis.Add(generator.GetNext());
            }

            Assert.NotEqual(fpis, null);
            Assert.Equal(fpis.Count, 5);
        }

        [Fact]
        public void CheckStandardFpiGneration()
        {
            var rndGenerator = new RandomGenerator();
            var generator = new FpiKeyGenerator(
                new ContentGenerator<ApprovalStatus>(() => ApprovalStatus.Standard),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "RXJG",
                    "GOGL",
                    "MSFT",
                    "YHOO"
                })),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "DTD",
                    "XSL",
                    "XML",
                    "JSON"
                })),
                new ContentGenerator<string>(() => rndGenerator.Phrase(20)),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "EN",
                    "FR",
                    "DE",
                    "ES",
                    "IT",
                    "PL",
                    "RO"
                })),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "IEEE",
                    "ICANN",
                    "W3C",
                    "ISO"
                })));

            Assert.NotEqual(generator.GetNext(), null);
        }

        [Fact]
        public void CheckMultipleStandardFpiGeneration()
        {
            var rndGenerator = new RandomGenerator();
            var generator = new FpiKeyGenerator(
                new ContentGenerator<ApprovalStatus>(() => ApprovalStatus.Standard),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "RXJG",
                    "GOGL",
                    "MSFT",
                    "YHOO"
                })),

                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "DTD",
                    "XSL",
                    "XML",
                    "JSON"
                })),
                new ContentGenerator<string>(() => rndGenerator.Phrase(20)),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "EN",
                    "FR",
                    "DE",
                    "ES",
                    "IT",
                    "PL",
                    "RO"
                })),
                new ContentGenerator<string>(() => Pick<string>.RandomItemFrom(new[]
                {
                    "IEEE",
                    "ICANN",
                    "W3C",
                    "ISO"
                })));

            var fpis = new List<Fpi>();
            for (var i = 0; i < 5; i++)
            {
                fpis.Add(generator.GetNext());
            }

            Assert.NotEqual(fpis, null);
            Assert.Equal(fpis.Count, 5);
        }
    }
}
