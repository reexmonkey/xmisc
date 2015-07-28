using reexjungle.xmisc.infrastructure.concretes.operations;
using Xunit;

namespace xmisc.tests.infrastructure
{
    public class FactoryTests
    {
        private readonly Factory factory;

        public FactoryTests()
        {
            factory = new Factory();
        }

        [Fact]
        public void CheckFactories()
        {
            factory.Register(() => new Person());
            var p1 = factory.Create<Person>();

            Assert.NotEqual(p1, null);
            Assert.Equal(p1.Name, default(string));
            Assert.Equal(p1.Age, default(uint));
            Assert.Equal(p1.Salary, default(decimal));

            factory.Register(() => new Person("Dummy", 1u, 5000));

            var p2 = factory.Create<Person>();
            Assert.Equal(p2, p1);

            factory.Unregister<Person>();

            factory.Register(() => new Person("Dummy", 1u, 5000));
            p2 = factory.Create<Person>();
            Assert.NotEqual(p2, p1);

            factory.Register(() => new Person("Dummy", 1u, 5000));
            var p3 = factory.Create<Person>();

            Assert.NotEqual(p3, null);
            Assert.Equal(p3.Name, "Dummy");
            Assert.Equal(p3.Age, 1u);
            Assert.Equal(p3.Salary, 5000);

            var p4 = factory.Create<Person>();
            Assert.Equal(p3, p4);

            factory.Register("c0", () => new Person());
            factory.Register("c1", () => new Person("Dummy", 1u));
            factory.Register("c2", () => new Person("Dummy", 5000m));
            factory.Register("c3", () => new Person("Dummy", 1u, 5000m));

            var p5 = factory.Create<Person>("c0");
            var p6 = factory.Create<Person>("c1");
            var p7 = factory.Create<Person>("c2");
            var p8 = factory.Create<Person>("c3");

            Assert.Equal(p1, p5);
            Assert.Equal(p3, p8);
            Assert.Equal(p6.Salary, default(decimal));
            Assert.Equal(p7.Age, default(uint));

            factory.Unregister("c3");

            var p9 = factory.Create<Person>("c3");
            Assert.Equal(p9, null);

            p9 = p8;
            factory.Register(1, () => p9);
            var p10 = factory.Create<Person>(1);

            p9 = null;
            var p11 = factory.Create<Person>(1);
            Assert.NotEqual(p10, null);
            Assert.Equal(p11, null);
        }
    }
}