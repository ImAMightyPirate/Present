/// <copyright>
/// This file was automatically generated with ♥ by Present.NET.
/// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.
/// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
/// Type: System.IO.Path, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
/// Framework: .NETCoreApp,Version=v2.1
/// </copyright>

namespace Present.IO
{
    public partial interface IPath
    {
        System.String ChangeExtension(System.String path, System.String extension);
        System.String GetDirectoryName(System.String path);
        System.String GetExtension(System.String path);
        System.String GetFileName(System.String path);
        System.String GetFileNameWithoutExtension(System.String path);
        System.String GetRandomFileName();
        System.Boolean IsPathFullyQualified(System.String path);
        System.Boolean HasExtension(System.String path);
        System.String Combine(System.String path1, System.String path2);
        System.String Combine(System.String path1, System.String path2, System.String path3);
        System.String Combine(System.String path1, System.String path2, System.String path3, System.String path4);
        System.String Combine(System.String[] paths);
        System.String GetRelativePath(System.String relativeTo, System.String path);
        System.Char[] GetInvalidFileNameChars();
        System.Char[] GetInvalidPathChars();
        System.String GetFullPath(System.String path);
        System.String GetFullPath(System.String path, System.String basePath);
        System.String GetTempPath();
        System.String GetTempFileName();
        System.Boolean IsPathRooted(System.String path);
        System.String GetPathRoot(System.String path);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Present.NET", "")]
    public partial class Path : IPath
    {
        public System.String ChangeExtension(System.String path, System.String extension)
        {
            return System.IO.Path.ChangeExtension(path, extension);
        }

        public System.String GetDirectoryName(System.String path)
        {
            return System.IO.Path.GetDirectoryName(path);
        }

        public System.String GetExtension(System.String path)
        {
            return System.IO.Path.GetExtension(path);
        }

        public System.String GetFileName(System.String path)
        {
            return System.IO.Path.GetFileName(path);
        }

        public System.String GetFileNameWithoutExtension(System.String path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public System.String GetRandomFileName()
        {
            return System.IO.Path.GetRandomFileName();
        }

        public System.Boolean IsPathFullyQualified(System.String path)
        {
            return System.IO.Path.IsPathFullyQualified(path);
        }

        public System.Boolean HasExtension(System.String path)
        {
            return System.IO.Path.HasExtension(path);
        }

        public System.String Combine(System.String path1, System.String path2)
        {
            return System.IO.Path.Combine(path1, path2);
        }

        public System.String Combine(System.String path1, System.String path2, System.String path3)
        {
            return System.IO.Path.Combine(path1, path2, path3);
        }

        public System.String Combine(System.String path1, System.String path2, System.String path3, System.String path4)
        {
            return System.IO.Path.Combine(path1, path2, path3, path4);
        }

        public System.String Combine(System.String[] paths)
        {
            return System.IO.Path.Combine(paths);
        }

        public System.String GetRelativePath(System.String relativeTo, System.String path)
        {
            return System.IO.Path.GetRelativePath(relativeTo, path);
        }

        public System.Char[] GetInvalidFileNameChars()
        {
            return System.IO.Path.GetInvalidFileNameChars();
        }

        public System.Char[] GetInvalidPathChars()
        {
            return System.IO.Path.GetInvalidPathChars();
        }

        public System.String GetFullPath(System.String path)
        {
            return System.IO.Path.GetFullPath(path);
        }

        public System.String GetFullPath(System.String path, System.String basePath)
        {
            return System.IO.Path.GetFullPath(path, basePath);
        }

        public System.String GetTempPath()
        {
            return System.IO.Path.GetTempPath();
        }

        public System.String GetTempFileName()
        {
            return System.IO.Path.GetTempFileName();
        }

        public System.Boolean IsPathRooted(System.String path)
        {
            return System.IO.Path.IsPathRooted(path);
        }

        public System.String GetPathRoot(System.String path)
        {
            return System.IO.Path.GetPathRoot(path);
        }
    }
}