﻿// ----------------------------------------------------------------------
// <copyright file="ICSharpFactory.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace SoloX.GeneratorTools.Core.CSharp.Workspace
{
    /// <summary>
    /// CSharp Project or File factory.
    /// </summary>
    public interface ICSharpFactory
    {
        /// <summary>
        /// Create a ICSharpProject.
        /// </summary>
        /// <param name="file">The project file.</param>
        /// <returns>The created project object.</returns>
        ICSharpProject CreateProject(string file);

        /// <summary>
        /// Create a ICSharpFile.
        /// </summary>
        /// <param name="file">The CSharp file.</param>
        /// <returns>The created file object.</returns>
        ICSharpFile CreateFile(string file);
    }
}
