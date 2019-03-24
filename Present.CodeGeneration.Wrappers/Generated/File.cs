/// <copyright>
/// This file was automatically generated with ♥ by Present.NET.
/// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.
/// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
/// Type: System.IO.File, System.IO.FileSystem, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
/// Framework: .NETCoreApp,Version=v2.1
/// </copyright>

namespace Present.IO
{
    public partial interface IFile
    {
        void Copy(System.String sourceFileName, System.String destFileName);
        void Copy(System.String sourceFileName, System.String destFileName, System.Boolean overwrite);
        void Delete(System.String path);
        System.Boolean Exists(System.String path);
        void SetCreationTime(System.String path, System.DateTime creationTime);
        void SetCreationTimeUtc(System.String path, System.DateTime creationTimeUtc);
        System.DateTime GetCreationTime(System.String path);
        System.DateTime GetCreationTimeUtc(System.String path);
        void SetLastAccessTime(System.String path, System.DateTime lastAccessTime);
        void SetLastAccessTimeUtc(System.String path, System.DateTime lastAccessTimeUtc);
        System.DateTime GetLastAccessTime(System.String path);
        System.DateTime GetLastAccessTimeUtc(System.String path);
        void SetLastWriteTime(System.String path, System.DateTime lastWriteTime);
        void SetLastWriteTimeUtc(System.String path, System.DateTime lastWriteTimeUtc);
        System.DateTime GetLastWriteTime(System.String path);
        System.DateTime GetLastWriteTimeUtc(System.String path);
        System.IO.FileAttributes GetAttributes(System.String path);
        void SetAttributes(System.String path, System.IO.FileAttributes fileAttributes);
        System.String ReadAllText(System.String path);
        System.String ReadAllText(System.String path, System.Text.Encoding encoding);
        void WriteAllText(System.String path, System.String contents);
        void WriteAllText(System.String path, System.String contents, System.Text.Encoding encoding);
        System.Byte[] ReadAllBytes(System.String path);
        void WriteAllBytes(System.String path, System.Byte[] bytes);
        System.String[] ReadAllLines(System.String path);
        System.String[] ReadAllLines(System.String path, System.Text.Encoding encoding);
        void WriteAllLines(System.String path, System.String[] contents);
        void WriteAllLines(System.String path, System.String[] contents, System.Text.Encoding encoding);
        void AppendAllText(System.String path, System.String contents);
        void AppendAllText(System.String path, System.String contents, System.Text.Encoding encoding);
        void Replace(System.String sourceFileName, System.String destinationFileName, System.String destinationBackupFileName);
        void Replace(System.String sourceFileName, System.String destinationFileName, System.String destinationBackupFileName, System.Boolean ignoreMetadataErrors);
        void Move(System.String sourceFileName, System.String destFileName);
        void Encrypt(System.String path);
        void Decrypt(System.String path);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Present.NET", "")]
    public partial class File : IFile
    {
        public void Copy(System.String sourceFileName, System.String destFileName)
        {
            System.IO.File.Copy(sourceFileName, destFileName);
        }

        public void Copy(System.String sourceFileName, System.String destFileName, System.Boolean overwrite)
        {
            System.IO.File.Copy(sourceFileName, destFileName, overwrite);
        }

        public void Delete(System.String path)
        {
            System.IO.File.Delete(path);
        }

        public System.Boolean Exists(System.String path)
        {
            return System.IO.File.Exists(path);
        }

        public void SetCreationTime(System.String path, System.DateTime creationTime)
        {
            System.IO.File.SetCreationTime(path, creationTime);
        }

        public void SetCreationTimeUtc(System.String path, System.DateTime creationTimeUtc)
        {
            System.IO.File.SetCreationTimeUtc(path, creationTimeUtc);
        }

        public System.DateTime GetCreationTime(System.String path)
        {
            return System.IO.File.GetCreationTime(path);
        }

        public System.DateTime GetCreationTimeUtc(System.String path)
        {
            return System.IO.File.GetCreationTimeUtc(path);
        }

        public void SetLastAccessTime(System.String path, System.DateTime lastAccessTime)
        {
            System.IO.File.SetLastAccessTime(path, lastAccessTime);
        }

        public void SetLastAccessTimeUtc(System.String path, System.DateTime lastAccessTimeUtc)
        {
            System.IO.File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
        }

        public System.DateTime GetLastAccessTime(System.String path)
        {
            return System.IO.File.GetLastAccessTime(path);
        }

        public System.DateTime GetLastAccessTimeUtc(System.String path)
        {
            return System.IO.File.GetLastAccessTimeUtc(path);
        }

        public void SetLastWriteTime(System.String path, System.DateTime lastWriteTime)
        {
            System.IO.File.SetLastWriteTime(path, lastWriteTime);
        }

        public void SetLastWriteTimeUtc(System.String path, System.DateTime lastWriteTimeUtc)
        {
            System.IO.File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        public System.DateTime GetLastWriteTime(System.String path)
        {
            return System.IO.File.GetLastWriteTime(path);
        }

        public System.DateTime GetLastWriteTimeUtc(System.String path)
        {
            return System.IO.File.GetLastWriteTimeUtc(path);
        }

        public System.IO.FileAttributes GetAttributes(System.String path)
        {
            return System.IO.File.GetAttributes(path);
        }

        public void SetAttributes(System.String path, System.IO.FileAttributes fileAttributes)
        {
            System.IO.File.SetAttributes(path, fileAttributes);
        }

        public System.String ReadAllText(System.String path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public System.String ReadAllText(System.String path, System.Text.Encoding encoding)
        {
            return System.IO.File.ReadAllText(path, encoding);
        }

        public void WriteAllText(System.String path, System.String contents)
        {
            System.IO.File.WriteAllText(path, contents);
        }

        public void WriteAllText(System.String path, System.String contents, System.Text.Encoding encoding)
        {
            System.IO.File.WriteAllText(path, contents, encoding);
        }

        public System.Byte[] ReadAllBytes(System.String path)
        {
            return System.IO.File.ReadAllBytes(path);
        }

        public void WriteAllBytes(System.String path, System.Byte[] bytes)
        {
            System.IO.File.WriteAllBytes(path, bytes);
        }

        public System.String[] ReadAllLines(System.String path)
        {
            return System.IO.File.ReadAllLines(path);
        }

        public System.String[] ReadAllLines(System.String path, System.Text.Encoding encoding)
        {
            return System.IO.File.ReadAllLines(path, encoding);
        }

        public void WriteAllLines(System.String path, System.String[] contents)
        {
            System.IO.File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(System.String path, System.String[] contents, System.Text.Encoding encoding)
        {
            System.IO.File.WriteAllLines(path, contents, encoding);
        }

        public void AppendAllText(System.String path, System.String contents)
        {
            System.IO.File.AppendAllText(path, contents);
        }

        public void AppendAllText(System.String path, System.String contents, System.Text.Encoding encoding)
        {
            System.IO.File.AppendAllText(path, contents, encoding);
        }

        public void Replace(System.String sourceFileName, System.String destinationFileName, System.String destinationBackupFileName)
        {
            System.IO.File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
        }

        public void Replace(System.String sourceFileName, System.String destinationFileName, System.String destinationBackupFileName, System.Boolean ignoreMetadataErrors)
        {
            System.IO.File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        }

        public void Move(System.String sourceFileName, System.String destFileName)
        {
            System.IO.File.Move(sourceFileName, destFileName);
        }

        public void Encrypt(System.String path)
        {
            System.IO.File.Encrypt(path);
        }

        public void Decrypt(System.String path)
        {
            System.IO.File.Decrypt(path);
        }
    }
}