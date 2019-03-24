﻿// ----------------------------------------------------------------------
// <copyright file="ImplementationGenerator.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using SoloX.GeneratorTools.Core.CSharp.Generator.Walker;
using SoloX.GeneratorTools.Core.CSharp.Generator.Writer.Impl;
using SoloX.GeneratorTools.Core.CSharp.Model;
using SoloX.GeneratorTools.Core.CSharp.Workspace;
using SoloX.GeneratorTools.Core.Generator;

namespace SoloX.GeneratorTools.Core.CSharp.Generator
{
    /// <summary>
    /// Entity implementation generator example.
    /// </summary>
    public class ImplementationGenerator
    {
        private readonly IGenerator generator;
        private readonly IInterfaceDeclaration itfPattern;
        private readonly IGenericDeclaration implPattern;
        private readonly string projectNameSpace;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationGenerator"/> class.
        /// </summary>
        /// <param name="generator">The generator to use to generate the output.</param>
        /// <param name="projectNameSpace">The project name space where to generate the classes.</param>
        /// <param name="itfPattern">The interface pattern to use for the generator.</param>
        /// <param name="implPattern">The implementation pattern to use for the generator.</param>
        public ImplementationGenerator(IGenerator generator, string projectNameSpace, IInterfaceDeclaration itfPattern, IGenericDeclaration implPattern)
        {
            this.generator = generator;
            this.itfPattern = itfPattern;
            this.implPattern = implPattern;
            this.projectNameSpace = projectNameSpace;
        }

        /// <summary>
        /// Generate the implementation of the given interface declaration.
        /// </summary>
        /// <param name="itfDeclaration">The interface declaration to implement.</param>
        public void Generate(IInterfaceDeclaration itfDeclaration)
        {
            var propertyWriter = new NodeWriter(
                this.itfPattern,
                itfDeclaration);

            var implName = GetEntityName(itfDeclaration.Name);
            var implNS = $"{this.projectNameSpace}.Model.Impl";
            this.generator.Generate(@"Model/Impl", implName, writer =>
            {
                var generatorWalker = new ImplementationGeneratorWalker(
                    writer,
                    this.itfPattern,
                    this.implPattern,
                    itfDeclaration,
                    implName,
                    implNS,
                    new WriterSelector(propertyWriter));

                generatorWalker.Visit(this.implPattern.SyntaxNode.SyntaxTree.GetRoot());
            });
        }

        private static string GetEntityName(string name)
        {
            if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
            {
                return name.Substring(1);
            }
            else
            {
                return $"{name}Entity";
            }
        }
    }
}
