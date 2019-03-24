/// <copyright>
/// This file was automatically generated with ♥ by Present.NET.
/// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.
/// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
/// 
/// Type: System.IO.Directory, System.IO.FileSystem, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
/// Framework: .NETCoreApp,Version=v2.1
/// </copyright>

namespace Present.IO
{
    public partial interface IDirectory
    {
        System.IO.DirectoryInfo GetParent(System.String path);
        System.IO.DirectoryInfo CreateDirectory(System.String path);
        System.Boolean Exists(System.String path);
        void SetCreationTime(System.String path, System.DateTime creationTime);
        void SetCreationTimeUtc(System.String path, System.DateTime creationTimeUtc);
        System.DateTime GetCreationTime(System.String path);
        System.DateTime GetCreationTimeUtc(System.String path);
        void SetLastWriteTime(System.String path, System.DateTime lastWriteTime);
        void SetLastWriteTimeUtc(System.String path, System.DateTime lastWriteTimeUtc);
        System.DateTime GetLastWriteTime(System.String path);
        System.DateTime GetLastWriteTimeUtc(System.String path);
        void SetLastAccessTime(System.String path, System.DateTime lastAccessTime);
        void SetLastAccessTimeUtc(System.String path, System.DateTime lastAccessTimeUtc);
        System.DateTime GetLastAccessTime(System.String path);
        System.DateTime GetLastAccessTimeUtc(System.String path);
        System.String[] GetFiles(System.String path);
        System.String[] GetFiles(System.String path, System.String searchPattern);
        System.String[] GetFiles(System.String path, System.String searchPattern, System.IO.SearchOption searchOption);
        System.String[] GetDirectories(System.String path);
        System.String[] GetDirectories(System.String path, System.String searchPattern);
        System.String[] GetDirectories(System.String path, System.String searchPattern, System.IO.SearchOption searchOption);
        System.String[] GetFileSystemEntries(System.String path);
        System.String[] GetFileSystemEntries(System.String path, System.String searchPattern);
        System.String[] GetFileSystemEntries(System.String path, System.String searchPattern, System.IO.SearchOption searchOption);
        System.String GetDirectoryRoot(System.String path);
        System.String GetCurrentDirectory();
        void SetCurrentDirectory(System.String path);
        void Move(System.String sourceDirName, System.String destDirName);
        void Delete(System.String path);
        void Delete(System.String path, System.Boolean recursive);
        System.String[] GetLogicalDrives();
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Present.NET", "")]
    public partial class Directory : IDirectory
    {
        public System.IO.DirectoryInfo GetParent(System.String path)
        {
            return System.IO.Directory.GetParent(path);
        }

        public System.IO.DirectoryInfo CreateDirectory(System.String path)
        {
            return System.IO.Directory.CreateDirectory(path);
        }

        public System.Boolean Exists(System.String path)
        {
            return System.IO.Directory.Exists(path);
        }

        public void SetCreationTime(System.String path, System.DateTime creationTime)
        {
            System.IO.Directory.SetCreationTime(path, creationTime);
        }

        public void SetCreationTimeUtc(System.String path, System.DateTime creationTimeUtc)
        {
            System.IO.Directory.SetCreationTimeUtc(path, creationTimeUtc);
        }

        public System.DateTime GetCreationTime(System.String path)
        {
            return System.IO.Directory.GetCreationTime(path);
        }

        public System.DateTime GetCreationTimeUtc(System.String path)
        {
            return System.IO.Directory.GetCreationTimeUtc(path);
        }

        public void SetLastWriteTime(System.String path, System.DateTime lastWriteTime)
        {
            System.IO.Directory.SetLastWriteTime(path, lastWriteTime);
        }

        public void SetLastWriteTimeUtc(System.String path, System.DateTime lastWriteTimeUtc)
        {
            System.IO.Directory.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        public System.DateTime GetLastWriteTime(System.String path)
        {
            return System.IO.Directory.GetLastWriteTime(path);
        }

        public System.DateTime GetLastWriteTimeUtc(System.String path)
        {
            return System.IO.Directory.GetLastWriteTimeUtc(path);
        }

        public void SetLastAccessTime(System.String path, System.DateTime lastAccessTime)
        {
            System.IO.Directory.SetLastAccessTime(path, lastAccessTime);
        }

        public void SetLastAccessTimeUtc(System.String path, System.DateTime lastAccessTimeUtc)
        {
            System.IO.Directory.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
        }

        public System.DateTime GetLastAccessTime(System.String path)
        {
            return System.IO.Directory.GetLastAccessTime(path);
        }

        public System.DateTime GetLastAccessTimeUtc(System.String path)
        {
            return System.IO.Directory.GetLastAccessTimeUtc(path);
        }

        public System.String[] GetFiles(System.String path)
        {
            return System.IO.Directory.GetFiles(path);
        }

        public System.String[] GetFiles(System.String path, System.String searchPattern)
        {
            return System.IO.Directory.GetFiles(path, searchPattern);
        }

        public System.String[] GetFiles(System.String path, System.String searchPattern, System.IO.SearchOption searchOption)
        {
            return System.IO.Directory.GetFiles(path, searchPattern, searchOption);
        }

        public System.String[] GetDirectories(System.String path)
        {
            return System.IO.Directory.GetDirectories(path);
        }

        public System.String[] GetDirectories(System.String path, System.String searchPattern)
        {
            return System.IO.Directory.GetDirectories(path, searchPattern);
        }

        public System.String[] GetDirectories(System.String path, System.String searchPattern, System.IO.SearchOption searchOption)
        {
            return System.IO.Directory.GetDirectories(path, searchPattern, searchOption);
        }

        public System.String[] GetFileSystemEntries(System.String path)
        {
            return System.IO.Directory.GetFileSystemEntries(path);
        }

        public System.String[] GetFileSystemEntries(System.String path, System.String searchPattern)
        {
            return System.IO.Directory.GetFileSystemEntries(path, searchPattern);
        }

        public System.String[] GetFileSystemEntries(System.String path, System.String searchPattern, System.IO.SearchOption searchOption)
        {
            return System.IO.Directory.GetFileSystemEntries(path, searchPattern, searchOption);
        }

        public System.String GetDirectoryRoot(System.String path)
        {
            return System.IO.Directory.GetDirectoryRoot(path);
        }

        public System.String GetCurrentDirectory()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

        public void SetCurrentDirectory(System.String path)
        {
            System.IO.Directory.SetCurrentDirectory(path);
        }

        public void Move(System.String sourceDirName, System.String destDirName)
        {
            System.IO.Directory.Move(sourceDirName, destDirName);
        }

        public void Delete(System.String path)
        {
            System.IO.Directory.Delete(path);
        }

        public void Delete(System.String path, System.Boolean recursive)
        {
            System.IO.Directory.Delete(path, recursive);
        }

        public System.String[] GetLogicalDrives()
        {
            return System.IO.Directory.GetLogicalDrives();
        }
    }
}