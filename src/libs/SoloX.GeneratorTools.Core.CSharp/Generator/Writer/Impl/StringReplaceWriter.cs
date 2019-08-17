﻿// ----------------------------------------------------------------------
// <copyright file="StringReplaceWriter.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using SoloX.GeneratorTools.Core.Generator.Writer;

namespace SoloX.GeneratorTools.Core.CSharp.Generator.Writer.Impl
{
    /// <summary>
    /// String replace writer.
    /// </summary>
    public class StringReplaceWriter : INodeWriter
    {
        private string oldString;
        private IReadOnlyList<string> newStringList;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringReplaceWriter"/> class.
        /// </summary>
        /// <param name="oldString">The string to be replaced.</param>
        /// <param name="newString">The string to use instead.</param>
        public StringReplaceWriter(string oldString, string newString)
        {
            this.oldString = oldString;
            this.newStringList = new[] { newString };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringReplaceWriter"/> class.
        /// </summary>
        /// <remarks>The pattern string substitution will be repeated for every new string in the newStringList.</remarks>
        /// <param name="oldString">The string to be replaced.</param>
        /// <param name="newStringList">The string list to use instead.</param>
        public StringReplaceWriter(string oldString, IReadOnlyList<string> newStringList)
        {
            this.oldString = oldString;
            this.newStringList = newStringList;
        }

        /// <inheritdoc/>
        public bool Write(SyntaxNode node, Action<string> write)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"The argument {nameof(node)} was null.");
            }

            var txt = node.ToFullString();

            if (txt.Contains(this.oldString))
            {
                foreach (var newString in this.newStringList)
                {
                    write(txt.Replace(this.oldString, newString));
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Write(SyntaxToken token, Action<string> write)
        {
            var txt = token.ToFullString();

            if (txt.Contains(this.oldString))
            {
                foreach (var newString in this.newStringList)
                {
                    write(txt.Replace(this.oldString, newString));
                }

                return true;
            }

            return false;
        }
    }
}
