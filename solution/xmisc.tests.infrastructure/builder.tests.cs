using System;
using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.infrastructure.concretes.operations;
using reexjungle.xmisc.infrastructure.contracts;
using Xunit;

namespace xmisc.tests.infrastructure
{
    public class BuilderTests
    {
        private readonly Factory factory;
        public BuilderTests()
        {
            factory = new Factory();

            factory.Register(1, () => new Person
            {
                Name = "John",
                Age = 20,
                LicenseKey = Guid.NewGuid()
            });

            factory.Register(2, () => new Person
            {
                Name = "Mary",
                Age = 15
            });

            factory.Register(3, () => new Person
            {
                Name = "Peter",
                Age = 30
            });
        }

        [Fact]
        public void ShouldBuildStringCacheKeyFromSingleInstance()
        {
            var builder = new StringCacheKeyBuilder();

            var p1 = factory.Create<Person>(1);
            var r1 = builder.Build(p1, x => x.Name);
            Assert.Equal(r1, "{Person:{Name:John}}");

            var r2 = builder.Build(p1, x => x.Name,  x => x.Age);
            Assert.Equal(r2, "{Person:{Name:John}{Age:20}}");

        }

        [Fact]
        public void ShouldBuildStringCacheKeyFromMultipleInstances()
        {
            var p1 = factory.Create<Person>(1);
            var p2 = factory.Create<Person>(2);
            var p3 = factory.Create<Person>(3);

            var builder = new StringCacheKeyBuilder();

            //multiple instances
            var r1 = builder.Build(new[] {p2, p3, p1, p3, p1, p2}, x => x.Name, x => x.Age);
            Assert.Equal(r1, "{Person:{Name:Mary}{Age:15}}{Person:{Name:Peter}{Age:30}}{Person:{Name:John}{Age:20}}");

            //multiple selectors

            var r2 = builder.Build(new[] { p2, p3, p1, p3, p1, p2 }, x => x.Name, x => x.Age, x => x.Age, x => x.Name);
            Assert.Equal(r2, "{Person:{Name:Mary}{Age:15}}{Person:{Name:Peter}{Age:30}}{Person:{Name:John}{Age:20}}");

            //reverse order of instances
            var r3 = builder.Build(new[] { p3, p2, p1 }, x => x.Name, x => x.Age);
            Assert.Equal(r3, "{Person:{Name:Peter}{Age:30}}{Person:{Name:Mary}{Age:15}}{Person:{Name:John}{Age:20}}");

            //reverse order of selectors
            var r4 = builder.Build(new[] { p3, p2, p1 }, x => x.Age, x => x.Name);
            Assert.Equal(r4, "{Person:{Age:30}{Name:Peter}}{Person:{Age:15}{Name:Mary}}{Person:{Age:20}{Name:John}}");

        }

        [Fact]
        public void ShouldCreateGuidCacheKeyFromSingleInstance()
        {
            var builder = new GuidCacheKeyBuilder();
            
            var r1 = builder.Build(factory.Create<Person>(1), x => x.Name);
            Assert.Equal(r1, new Guid("e25ded62-5d9e-16d4-4a01-f3af1b81efa0"));

            var r2 = builder.Build(factory.Create<Person>(1), x => x.Name, x => x.Age);
            Assert.Equal(r2, new Guid("7b904096-8c84-b383-f14a-f926fb606603"));

        }

        [Fact]
        public void ShouldBuildGuidCacheKeyFromMultipleInstances()
        {
            var p1 = factory.Create<Person>(1);
            var p2 = factory.Create<Person>(2);
            var p3 = factory.Create<Person>(3);

            var builder = new GuidCacheKeyBuilder();

            //base
            var r0 = builder.Build(new[] { p1, p2, p3}, x => x.Name, x => x.Age);
            Assert.Equal(r0, new Guid("43addd55-40f7-e3bd-0242-ef3d40968878"));

            //multiple instances => same key
            var r1 = builder.Build(new[] { p1, p2, p3, p3, p1, p2 }, x => x.Name, x => x.Age);
            Assert.Equal(r1, r0);

            //multiple selectors => same key
            var r2 = builder.Build(new[] { p1, p2, p3 }, x => x.Name, x => x.Age, x => x.Age, x => x.Name);
            Assert.Equal(r2, r0);

            //reverse order of instances
            var r3 = builder.Build(new[] { p3, p2, p1 }, x => x.Name, x => x.Age);
            Assert.Equal(r3, r0);

            //reverse order of selectors
            var r4 = builder.Build(new[] { p3, p2, p1 }, x => x.Age, x => x.Name);
            Assert.Equal(r4, r0);

        }
    }
}
