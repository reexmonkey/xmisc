using reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System.Collections.Generic;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.tests.fixtures
{
    public class Fixture
    {
        public static readonly string NamespaceId = "reexmonkey.xmisc.backbone.identifiers";
        public static Encoding Encoding = new UnicodeEncoding();
        public static IComparer<SequentialGuid> SequentialGuidComparer = new SequentialGuidForSqlServerComparer();
        public static IComparer<Md5Guid> Md5GuidComparer = new Md5GuidForSqlServerComparer();
        public static IComparer<Sha1Guid> Sha1GuidComparer = new Sha1GuidForSqlServerComparer();
    }
}
