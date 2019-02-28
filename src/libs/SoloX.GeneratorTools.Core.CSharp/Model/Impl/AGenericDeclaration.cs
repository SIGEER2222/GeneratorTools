﻿// ----------------------------------------------------------------------
// <copyright file="AGenericDeclaration.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SoloX.GeneratorTools.Core.CSharp.Model.Impl.Walker;
using SoloX.GeneratorTools.Core.CSharp.Model.Resolver;
using SoloX.GeneratorTools.Core.CSharp.Model.Use;
using SoloX.GeneratorTools.Core.CSharp.Model.Use.Impl.Walker;

namespace SoloX.GeneratorTools.Core.CSharp.Model.Impl
{
    /// <summary>
    /// Base abstract generic declaration implementation.
    /// </summary>
    public abstract class AGenericDeclaration : ADeclaration, IGenericDeclaration
    {
        private readonly List<IGenericDeclaration> extendedBy = new List<IGenericDeclaration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AGenericDeclaration"/> class.
        /// </summary>
        /// <param name="nameSpace">The class declaration name space.</param>
        /// <param name="name">The declaration name.</param>
        /// <param name="syntaxNode">The declaration syntax node.</param>
        /// <param name="typeParameterListSyntax">The type parameter list syntax node.</param>
        /// <param name="usingDirectives">The current using directive available for this class.</param>
        protected AGenericDeclaration(string nameSpace, string name, CSharpSyntaxNode syntaxNode, TypeParameterListSyntax typeParameterListSyntax, IReadOnlyList<string> usingDirectives)
            : base(nameSpace, name, syntaxNode, usingDirectives)
        {
            this.TypeParameterListSyntax = typeParameterListSyntax;
        }

        /// <inheritdoc/>
        public TypeParameterListSyntax TypeParameterListSyntax { get; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IGenericParameterDeclaration> GenericParameters { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IDeclarationUse> Extends { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IGenericDeclaration> ExtendedBy => this.extendedBy;

        /// <inheritdoc/>
        public IReadOnlyCollection<IMemberDeclaration> Members { get; private set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (this.Extends?.Any() ?? false)
            {
                return $"{base.ToString()}: {string.Join(", ", this.Extends?.Select(e => e.ToString()))}";
            }

            return base.ToString();
        }

        /// <summary>
        /// Load the generic parameters from the type parameter list node.
        /// </summary>
        protected void LoadGenericParameters()
        {
            var parameterList = this.TypeParameterListSyntax;
            if (parameterList != null)
            {
                var parameterSet = new List<IGenericParameterDeclaration>();
                foreach (var parameter in parameterList.Parameters)
                {
                    parameterSet.Add(new GenericParameterDeclaration(parameter.Identifier.Text, parameter));
                }

                this.GenericParameters = parameterSet;
            }
            else
            {
                this.GenericParameters = Array.Empty<IGenericParameterDeclaration>();
            }
        }

        /// <summary>
        /// Load extends statement list.
        /// </summary>
        /// <param name="resolver">The resolver to resolve dependencies.</param>
        /// <param name="baseListSyntax">The base list syntax.</param>
        protected void LoadExtends(IDeclarationResolver resolver, BaseListSyntax baseListSyntax)
        {
            if (baseListSyntax != null)
            {
                var baseWalker = new DeclarationUseWalker(resolver, this);
                var uses = new List<IDeclarationUse>();

                foreach (var node in baseListSyntax.ChildNodes())
                {
                    var use = baseWalker.Visit(node);

                    if (use.Declaration is AGenericDeclaration agd)
                    {
                        agd.AddExtendedBy(this);
                    }

                    uses.Add(use);
                }

                this.Extends = uses;
            }
            else
            {
                this.Extends = Array.Empty<IDeclarationUse>();
            }
        }

        /// <summary>
        /// Load member list.
        /// </summary>
        /// <param name="resolver">The resolver to resolve dependencies.</param>
        protected void LoadMembers(IDeclarationResolver resolver)
        {
            var memberList = new List<IMemberDeclaration>();
            var membersWalker = new MembersWalker(resolver, this, memberList);

            membersWalker.Visit(this.SyntaxNode);

            this.Members = memberList.Any() ? memberList.ToArray() : Array.Empty<IMemberDeclaration>();
        }

        private void AddExtendedBy(AGenericDeclaration declaration)
        {
            this.extendedBy.Add(declaration);
        }
    }
}
