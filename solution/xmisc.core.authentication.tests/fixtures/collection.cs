using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace xmisc.core.authentication.tests.fixtures
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection: ICollectionFixture<Fixture>
    {
    }
}
