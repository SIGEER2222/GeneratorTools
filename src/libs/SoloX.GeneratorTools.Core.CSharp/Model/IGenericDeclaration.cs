﻿// ----------------------------------------------------------------------
// <copyright file="IGenericDeclaration.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SoloX.GeneratorTools.Core.CSharp.Model
{
    /// <summary>
    /// Generic declaration interface.
    /// </summary>
    public interface IGenericDeclaration : IDeclaration
    {
        /// <summary>
        /// Gets the generic parameters syntax node.
        /// </summary>
        TypeParameterListSyntax TypeParameterListSyntax { get; }
    }
}
