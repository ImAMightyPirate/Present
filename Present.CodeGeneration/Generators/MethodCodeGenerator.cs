// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using System.Collections.Generic;
    using System.Reflection;
    using Constants;
    using Contracts;
    using EnsureThat;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Responsible for generating the Roslyn definition for a method.
    /// </summary>
    public class MethodCodeGenerator : IMethodCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn method definition from a <see cref="MethodInfo"/> object.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The generated method declaration and method body.</returns>
        public (MethodDeclarationSyntax methodDeclaration, BlockSyntax methodBody) Generate(MethodInfo method)
        {
            Ensure.That(method).IsNotNull();

            var parameterSyntaxes = new List<ParameterSyntax>();
            var argumentSyntaxes = new List<ArgumentSyntax>();

            foreach (var parameter in method.GetParameters())
            {
                var parameterSyntax = SyntaxFactory
                    .Parameter(SyntaxFactory.Identifier(parameter.Name))
                    .WithType(SyntaxFactory.ParseTypeName(parameter.ParameterType.FullName));

                parameterSyntaxes.Add(parameterSyntax);

                var argumentSyntax = SyntaxFactory
                    .Argument(SyntaxFactory.IdentifierName(parameter.Name));

                argumentSyntaxes.Add(argumentSyntax);
            }

            var argumentList = SyntaxFactory.SeparatedList(argumentSyntaxes);

            BlockSyntax methodBody;
            string returnTypeName;

            if (method.ReturnType == typeof(void))
            {
                var invocationSyntax = this.GenerateExpressionStatement(method, argumentList);
                methodBody = SyntaxFactory.Block(invocationSyntax);
                returnTypeName = LanguageKeyword.Void;
            }
            else
            {
                var returnSyntax = SyntaxFactory.ReturnStatement(
                    SyntaxFactory.Token(SyntaxKind.ReturnKeyword),
                    this.GenerateExpressionStatement(method, argumentList).Expression,
                    SyntaxFactory.Token(SyntaxKind.SemicolonToken));

                methodBody = SyntaxFactory.Block(returnSyntax);
                returnTypeName = method.ReturnType.FullName;
            }

            var methodDeclaration = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.ParseTypeName(returnTypeName), method.Name)
                .AddParameterListParameters(parameterSyntaxes.ToArray());

            return (methodDeclaration, methodBody);
        }

        private ExpressionStatementSyntax GenerateExpressionStatement(MethodInfo method, SeparatedSyntaxList<ArgumentSyntax> argumentList)
        {
            return SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName(method.DeclaringType.FullName),
                        SyntaxFactory.IdentifierName(method.Name)),
                    SyntaxFactory.ArgumentList(argumentList)));
        }
    }
}
