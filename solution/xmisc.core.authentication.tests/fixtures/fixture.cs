using reexmonkey.xmisc.core.reflection.extensions;
using System.IO;
using System.Reflection;
using System.Text;
using xmisc.core.authentication.tests.extensions;

namespace xmisc.core.authentication.tests.fixtures
{
    public class Fixture
    {
        protected const string INPUT = "input";
        protected const string KEYS = "keys";

        public static string PRIVATE_KEY_SET = "private-keys.json";
        public static string PUBLIC_KEY_SET_RSA = "public-keys-rsa.json";
        public static string PUBLIC_KEY_RSA = "public-key-rsa.json";

        public string GetHomeDirectoryPath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetFilePath());
            var di = Directory.CreateDirectory(path);
            return path;
        }

        protected DirectoryInfo GetProjectDirectory()
        {
            return new DirectoryInfo(GetHomeDirectoryPath())?.Parent?.Parent?.Parent;
        }

        protected DirectoryInfo GetInputDirectory()
        {
            var projectDir = GetProjectDirectory();
            var path = projectDir != null ? Path.Combine(projectDir.FullName, INPUT) : INPUT;
            return new DirectoryInfo(path);
        }

        protected DirectoryInfo GetKeysDirectory()
        {
            var projectDir = GetProjectDirectory();
            var path = projectDir != null ? Path.Combine(projectDir.FullName, INPUT, KEYS) : Path.Combine(INPUT, KEYS);
            return new DirectoryInfo(path);
        }

        protected string GetGetKeysFilePath(string filename)
        {
            var di = GetKeysDirectory();
            return di != null ? Path.Combine(di.FullName, filename) : $"{Path.GetRandomFileName()}.xml";
        }

        public string LoadKeyFile(string filename)
        {
            var fi = new FileInfo(GetGetKeysFilePath(filename));
            return fi.ReadFile(new UTF8Encoding(false, true));
        }
    }
}