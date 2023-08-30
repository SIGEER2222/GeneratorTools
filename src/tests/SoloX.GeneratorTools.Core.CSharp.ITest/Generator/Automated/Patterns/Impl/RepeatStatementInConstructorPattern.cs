﻿// ----------------------------------------------------------------------
// <copyright file="RepeatStatementInConstructorPattern.cs" company="Xavier Solau">
// Copyright © 2021 Xavier Solau.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using SoloX.GeneratorTools.Core.CSharp.Generator.Attributes;
using SoloX.GeneratorTools.Core.CSharp.ITest.Generator.Automated.Patterns.Itf;

namespace SoloX.GeneratorTools.Core.CSharp.ITest.Generator.Automated.Patterns.Impl
{
    [Pattern<MultiSelector>]
    [Repeat(Pattern = nameof(ISimplePattern), Prefix = "I")]
    public class RepeatStatementInConstructorPattern : ISimplePattern
    {
        [RepeatStatements(Pattern = nameof(ISimplePattern.PatternProperty))]
        public RepeatStatementInConstructorPattern()
        {
            var txt = string.Empty;
            txt = txt + PatternProperty.ToString();
        }

        [Repeat(Pattern = nameof(ISimplePattern.PatternProperty))]
        public object PatternProperty { get; set; }
    }
}