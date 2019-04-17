﻿// ----------------------------------------------------------------------
// <copyright file="INodeWriter.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SoloX.GeneratorTools.Core.Generator.Writer
{
    /// <summary>
    /// Interface to write a property or other nodes.
    /// </summary>
    public interface INodeWriter
    {
        /// <summary>
        /// Write the code from the given node.
        /// </summary>
        /// <param name="node">The node to write.</param>
        /// <param name="write">The write delegate.</param>
        /// <returns>True if the node is written.</returns>
        bool Write(SyntaxNode node, Action<string> write);

        /// <summary>
        /// Write the code from the given token.
        /// </summary>
        /// <param name="token">The token to write.</param>
        /// <param name="write">The write delegate.</param>
        /// <returns>True if the node is written.</returns>
        bool Write(SyntaxToken token, Action<string> write);
    }
}
