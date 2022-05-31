﻿// ----------------------------------------------------------------------
// <copyright file="RepeatAttributedClass.cs" company="Xavier Solau">
// Copyright © 2021 Xavier Solau.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using SoloX.GeneratorTools.Core.CSharp.Generator.Attributes;

namespace SoloX.GeneratorTools.Core.CSharp.UTest.Resources.Model.Basic
{
    [Repeat(RepeatPattern = "Pattern", RepeatPatternPrefix = "Prefix")]
    public class RepeatAttributedClass
    {
    }
}