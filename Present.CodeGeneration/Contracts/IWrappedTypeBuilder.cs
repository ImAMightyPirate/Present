﻿// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    /// <summary>
    /// Contract for the wrapped type builder.
    /// </summary>
    public interface IWrappedTypeBuilder
    {
        /// <summary>
        /// Creates a wrapper class for a .NET type and outputs
        /// the code file.
        /// </summary>
        /// <param name="options">The program options.</param>
        void Wrap(IWrapOptions options);
    }
}