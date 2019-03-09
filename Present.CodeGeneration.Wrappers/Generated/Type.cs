// <copyright>
// This file was automatically generated with ♥ by Present.NET.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// </copyright>

namespace Present
{
    public partial interface IType
    {
        System.Type GetType(System.String typeName, System.Boolean throwOnError, System.Boolean ignoreCase);
        System.Type GetType(System.String typeName, System.Boolean throwOnError);
        System.Type GetType(System.String typeName);
        System.Type GetTypeFromProgID(System.String progID, System.String server, System.Boolean throwOnError);
        System.Type GetTypeFromCLSID(System.Guid clsid, System.String server, System.Boolean throwOnError);
        System.Type GetTypeFromHandle(System.RuntimeTypeHandle handle);
        System.TypeCode GetTypeCode(System.Type type);
        System.Type GetTypeFromCLSID(System.Guid clsid);
        System.Type GetTypeFromCLSID(System.Guid clsid, System.Boolean throwOnError);
        System.Type GetTypeFromCLSID(System.Guid clsid, System.String server);
        System.Type GetTypeFromProgID(System.String progID);
        System.Type GetTypeFromProgID(System.String progID, System.Boolean throwOnError);
        System.Type GetTypeFromProgID(System.String progID, System.String server);
        System.Type MakeGenericMethodParameter(System.Int32 position);
        System.Type ReflectionOnlyGetType(System.String typeName, System.Boolean throwIfNotFound, System.Boolean ignoreCase);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Present.NET", "")]
    public partial class Type : IType
    {
        public System.Type GetType(System.String typeName, System.Boolean throwOnError, System.Boolean ignoreCase)
        {
            return System.Type.GetType(typeName, throwOnError, ignoreCase);
        }

        public System.Type GetType(System.String typeName, System.Boolean throwOnError)
        {
            return System.Type.GetType(typeName, throwOnError);
        }

        public System.Type GetType(System.String typeName)
        {
            return System.Type.GetType(typeName);
        }

        public System.Type GetTypeFromProgID(System.String progID, System.String server, System.Boolean throwOnError)
        {
            return System.Type.GetTypeFromProgID(progID, server, throwOnError);
        }

        public System.Type GetTypeFromCLSID(System.Guid clsid, System.String server, System.Boolean throwOnError)
        {
            return System.Type.GetTypeFromCLSID(clsid, server, throwOnError);
        }

        public System.Type GetTypeFromHandle(System.RuntimeTypeHandle handle)
        {
            return System.Type.GetTypeFromHandle(handle);
        }

        public System.TypeCode GetTypeCode(System.Type type)
        {
            return System.Type.GetTypeCode(type);
        }

        public System.Type GetTypeFromCLSID(System.Guid clsid)
        {
            return System.Type.GetTypeFromCLSID(clsid);
        }

        public System.Type GetTypeFromCLSID(System.Guid clsid, System.Boolean throwOnError)
        {
            return System.Type.GetTypeFromCLSID(clsid, throwOnError);
        }

        public System.Type GetTypeFromCLSID(System.Guid clsid, System.String server)
        {
            return System.Type.GetTypeFromCLSID(clsid, server);
        }

        public System.Type GetTypeFromProgID(System.String progID)
        {
            return System.Type.GetTypeFromProgID(progID);
        }

        public System.Type GetTypeFromProgID(System.String progID, System.Boolean throwOnError)
        {
            return System.Type.GetTypeFromProgID(progID, throwOnError);
        }

        public System.Type GetTypeFromProgID(System.String progID, System.String server)
        {
            return System.Type.GetTypeFromProgID(progID, server);
        }

        public System.Type MakeGenericMethodParameter(System.Int32 position)
        {
            return System.Type.MakeGenericMethodParameter(position);
        }

        public System.Type ReflectionOnlyGetType(System.String typeName, System.Boolean throwIfNotFound, System.Boolean ignoreCase)
        {
            return System.Type.ReflectionOnlyGetType(typeName, throwIfNotFound, ignoreCase);
        }
    }
}