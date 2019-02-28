﻿// ----------------------------------------------------------------------
// <copyright file="DeclarationResolver.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using SoloX.GeneratorTools.Core.CSharp.Model;
using SoloX.GeneratorTools.Core.CSharp.Model.Impl;
using SoloX.GeneratorTools.Core.CSharp.Model.Use;
using SoloX.GeneratorTools.Core.CSharp.Workspace;

namespace SoloX.GeneratorTools.Core.CSharp.Model.Resolver.Impl
{
    /// <summary>
    /// Declaration resolver implementation.
    /// </summary>
    public class DeclarationResolver : IDeclarationResolver
    {
        private readonly Action<IDeclarationResolver, IDeclaration> loader;

        private readonly Dictionary<string, List<IDeclaration>> declarationMap = new Dictionary<string, List<IDeclaration>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclarationResolver"/> class.
        /// </summary>
        /// <param name="declarations">The declaration list the resolver is based on.</param>
        /// <param name="loader">The loader delegate to load a declaration is needed.</param>
        public DeclarationResolver(
            IEnumerable<IDeclaration> declarations, Action<IDeclarationResolver, IDeclaration> loader)
        {
            this.loader = loader;

            this.Setup(declarations);
        }

        /// <inheritdoc/>
        public IGenericDeclaration Resolve(
            string identifier, IReadOnlyList<IDeclarationUse> genericParameters, IDeclaration declarationContext)
        {
            var declarations = this.FindDeclarations(identifier, declarationContext);
            if (declarations != null)
            {
                var tParamCount = genericParameters == null ? 0 : genericParameters.Count;
                foreach (var declarationItem in declarations)
                {
                    // Make sure the declaration is loaded
                    this.loader(this, declarationItem);

                    if (declarationItem is IGenericDeclaration gd && tParamCount == gd.GenericParameters.Count)
                    {
                        // TODO take into account the type parameter constraints.
                        return gd;
                    }
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public IDeclaration Resolve(string identifier, IDeclaration declarationContext)
        {
            var declarations = this.FindDeclarations(identifier, declarationContext);
            if (declarations != null)
            {
                foreach (var declarationItem in declarations)
                {
                    // Make sure the declaration is loaded
                    this.loader(this, declarationItem);

                    if (declarationItem is IGenericDeclaration gd)
                    {
                        if (gd.GenericParameters.Count == 0)
                        {
                            return gd;
                        }
                    }
                    else
                    {
                        return declarationItem;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Load all declarations.
        /// </summary>
        internal void Load()
        {
            foreach (var declarationItem in this.declarationMap)
            {
                var declarationList = declarationItem.Value;
                foreach (var declaration in declarationList)
                {
                    this.loader(this, declaration);
                }
            }
        }

        /// <summary>
        /// Find all declarations matching the given lookup name.
        /// </summary>
        /// <param name="fullName">The full name to lookup.</param>
        /// <returns>The declaration list.</returns>
        internal IEnumerable<IDeclaration> Find(string fullName)
        {
            return this.declarationMap.TryGetValue(fullName, out var declarations) ? declarations : null;
        }

        private IEnumerable<IDeclaration> FindDeclarations(
            string identifier, IDeclaration declarationContext)
        {
            List<IDeclaration> declarations;
            foreach (var usingDirective in declarationContext.UsingDirectives)
            {
                var lookupName = ADeclaration.GetFullName(usingDirective, identifier);

                if (this.declarationMap.TryGetValue(lookupName, out declarations))
                {
                    return declarations;
                }
            }

            if (this.declarationMap.TryGetValue(
                ADeclaration.GetFullName(declarationContext.DeclarationNameSpace, identifier),
                out declarations))
            {
                return declarations;
            }

            if (this.declarationMap.TryGetValue(identifier, out declarations))
            {
                return declarations;
            }

            return null;
        }

        private void Setup(IEnumerable<IDeclaration> declarations)
        {
            foreach (var declaration in declarations)
            {
                var fullName = ADeclaration.GetFullName(declaration.DeclarationNameSpace, declaration.Name);

                if (!this.declarationMap.TryGetValue(fullName, out var list))
                {
                    list = new List<IDeclaration>();
                    this.declarationMap.Add(fullName, list);
                }

                list.Add(declaration);
            }
        }
    }
}
