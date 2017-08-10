using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    /// <summary>
    /// Provides common extensions to the <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrives the assembly of a provided type declaration.
        /// </summary>
        /// <param name="type">The type declaration, whose assembly shall be retrieved.</param>
        /// <returns>The assembly of the type declaration.</returns>
        public static Assembly GetAssembly(this Type type) => Assembly.GetAssembly(type);

        /// <summary>
        /// Retrieves the assembly of an object .
        /// </summary>
        /// <param name="o">The object, whose assembly shall be retrieved.</param>
        /// <returns>The assembly of the type declaration.</returns>
        public static Assembly GetAssembly(this object o) => Assembly.GetAssembly(o.GetType());

        /// <summary>
        /// Retrieves the assembly of a given type.
        /// </summary>
        /// <typeparam name="T">The type whose assembly shall be retrieved.</typeparam>
        /// <returns>The assembly of the type declaration.</returns>
        public static Assembly GetAssembly<T>() => Assembly.GetAssembly(typeof(T));

        /// <summary>
        /// Gets the file path to a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly whose file path shall be determined.</param>
        /// <returns>The file path to the given assembly.</returns>
        public static string GetFilePath(this Assembly assembly) => Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);

        /// <summary>
        /// Gets the directory path to a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly whose directory path shall be determined.</param>
        /// <returns>The directory path to the given assembly.</returns>
        public static string GetDirectoryPath(this Assembly assembly) => Path.GetDirectoryName(assembly.GetFilePath());

        /// <summary>
        /// Gets the file path to the assembly of a given type declaration.
        /// </summary>
        /// <param name="type">The type declaration, whose assembly file path shall be determined.</param>
        /// <returns>The file path to the assembly of the given type declaration.</returns>
        public static string GetAssemblyFilePath(this Type type) => type.GetAssembly().GetFilePath();

        /// <summary>
        /// Gets the file path to the assembly of a given object.
        /// </summary>
        /// <param name="o">The object, whose assembly file path shall be determined.</param>
        /// <returns>The file path to the assembly of the given object.</returns>
        public static string GetAssemblyFilePath(this object o) => o.GetAssembly().GetFilePath();

        /// <summary>
        /// Gets the file path to the assembly of a given type.
        /// </summary>
        /// <typeparam name="T">The type, whose assembly file path shall be determined.</typeparam>
        /// <returns>The file path to the assembly of the given type.</returns>
        public static string GetAssemblyFilePath<T>() => GetAssembly<T>().GetFilePath();

        /// <summary>
        /// Gets the file version information of a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly whose file version information shall be determined.</param>
        /// <returns>The file version information of the given assembly.</returns>
        public static FileVersionInfo GetFileVersionInfo(this Assembly assembly) => FileVersionInfo.GetVersionInfo(assembly.GetFilePath());

        /// <summary>
        /// Gets the version information of a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly, whose version information shall be determined.</param>
        /// <returns>The version information of the assembly.</returns>
        public static Version GetVersionInfo(this Assembly assembly) => assembly.GetName().Version;

        /// <summary>
        /// Gets the file paths to the assemblies of given type declarations.
        /// </summary>
        /// <param name="types">The type declarations, whose assembly paths shall be determined.</param>
        /// <returns>The file paths to the assemblies of the given type declarations. </returns>
        public static IEnumerable<string> GetAssemblyFilePaths(this Type[] types) => types.Select(x => x.GetAssemblyFilePath());


        /// <summary>
        /// Gets the file paths to the assemblies of given objects.
        /// </summary>
        /// <param name="objects">The objects, whose assembly paths shall be determined.</param>
        /// <returns>The file paths to the assemblies of the given objects.</returns>
        public static IEnumerable<string> GetAssembyFilePaths(this object[] objects) => objects.Select(o => o.GetAssemblyFilePath());

        /// <summary>
        /// Gets the <see cref="AssemblyName"/> objects referenced by the assembly of a given type declaration.
        /// </summary>
        /// <param name="type">The type declaration, whose assembly references other assemblies.</param>
        /// <returns>The <see cref="AssemblyName"/> objects referenced by the assembly of the given type declaration.</returns>
        public static AssemblyName[] GetReferencedAssemblyNames(this Type type) => type.GetAssembly().GetReferencedAssemblies();

        /// <summary>
        /// Gets the <see cref="AssemblyName"/> objects referenced by the assembly of a given object.
        /// </summary>
        /// <param name="o">The object, whose assemby references other assemblies.</param>
        /// <returns>The <see cref="AssemblyName"/> objects referenced by the assembly of the given object.</returns>
        public static AssemblyName[] GetReferencedAssemblyNames(this object o) => o.GetAssembly().GetReferencedAssemblies();

        /// <summary>
        /// Gets the <see cref="AssemblyName"/> objects referenced by the assembly of a given type.
        /// </summary>
        /// <typeparam name="T">The type, whose assembly references other assemblies.</typeparam>
        /// <returns>The <see cref="AssemblyName"/> objects referenced by the assembly of the given type.</returns>
        public static AssemblyName[] GetReferencedAssemblyNames<T>() => GetAssembly<T>().GetReferencedAssemblies();

        /// <summary>
        /// Gets the friendly name (optionally with a DLL suffix) of an assembly from the given assembly identity.
        /// </summary>
        /// <param name="name">The unique identity of an assembly in full.</param>
        /// <param name="dllsuffix">Determines whether the DLL suffix should be added to the friendly name. 
        /// True if added; otherwise false.</param>
        /// <returns>The friendly name of the assembly identity.</returns>
        public static string ToFriendlyName(this AssemblyName name, bool dllsuffix = true) => dllsuffix ? name.Name : name.Name + ".dll";
    }
}