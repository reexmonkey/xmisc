using reexmonkey.xmisc.backbone.identifiers.concretes.helpers;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.tests.fixtures
{
    public class Fixture
    {
        public static readonly Encoding Encoding = new UnicodeEncoding();
        public static readonly IComparer<SequentialGuid> SequentialGuidComparer = new SequentialGuidForSqlServerComparer();
        public static readonly IComparer<Md5Guid> Md5GuidComparer = new Md5GuidForSqlServerComparer();
        public static readonly IComparer<Sha1Guid> Sha1GuidComparer = new Sha1GuidForSqlServerComparer();
    }
}