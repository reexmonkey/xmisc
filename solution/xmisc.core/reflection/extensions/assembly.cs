using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    public static class AssemblyExtensions
    {
        public static Assembly GetAssembly(this Type type) => Assembly.GetAssembly(type);

        public static Assembly GetAssembly(this object o) => Assembly.GetAssembly(o.GetType());

        public static Assembly GetAssembly<T>() => Assembly.GetAssembly(typeof(T));

        public static string GetFilePath(this Assembly assembly) => Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);

        public static string GetDirectoryPath(this Assembly assembly) => Path.GetDirectoryName(assembly.GetFilePath());

        public static string GetAssemblyFilePath(this Type type) => type.GetAssembly().GetFilePath();

        public static string GetAssemblyFilePath(this object o) => o.GetAssembly().GetFilePath();

        public static string GetAssemblyFilePath<T>() => GetAssembly<T>().GetFilePath();

        public static FileVersionInfo GetFileVersionInfo(this Assembly assembly) => FileVersionInfo.GetVersionInfo(assembly.GetFilePath());

        public static Version GetVersionInfo(this Assembly assembly) => assembly.GetName().Version;

        public static IEnumerable<string> GetAssembyFilePaths(this Type[] types) => types.Select(x => x.GetAssemblyFilePath());

        public static IEnumerable<string> GetAssembyFilePaths(this object[] objects) => objects.Select(o => o.GetAssemblyFilePath());

        public static AssemblyName[] GetReferencedAssemblyNames(this Type type) => type.GetAssembly().GetReferencedAssemblies();

        public static AssemblyName[] GetReferencedAssemblyNames(this object o) => o.GetAssembly().GetReferencedAssemblies();

        public static AssemblyName[] GetReferencedAssemblyNames<T>() => GetAssembly<T>().GetReferencedAssemblies();

        public static string ToFriendlyName(this AssemblyName name, bool dllsuffix = true) => dllsuffix ? name.Name : name.Name + ".dll";
    }
}