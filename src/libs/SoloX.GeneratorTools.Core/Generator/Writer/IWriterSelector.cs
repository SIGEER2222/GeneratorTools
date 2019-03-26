﻿// ----------------------------------------------------------------------
// <copyright file="IWriterSelector.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace SoloX.GeneratorTools.Core.Generator.Writer
{
    /// <summary>
    /// Writer selector interface.
    /// </summary>
    public interface IWriterSelector
    {
        /// <summary>
        /// Select and process the writer for the given node.
        /// </summary>
        /// <param name="node">The node to write.</param>
        /// <param name="write">The write delegate.</param>
        /// <returns>The selected Writer.</returns>
        bool SelectAndProcessWriter(SyntaxNode node, Action<string> write);
    }
}
