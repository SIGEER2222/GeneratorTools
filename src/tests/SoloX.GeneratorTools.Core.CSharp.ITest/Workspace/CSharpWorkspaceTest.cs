// ----------------------------------------------------------------------
// <copyright file="CSharpWorkspaceTest.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using SoloX.GeneratorTools.Core.CSharp.Workspace.Impl;
using Xunit;

namespace SoloX.GeneratorTools.Core.CSharp.ITest.Workspace
{
    public class CSharpWorkspaceTest
    {
        [Fact]
        public void ProjectLoadTest()
        {
            var projectFile = @"..\..\..\..\SoloX.GeneratorTools.Core.CSharp.Sample1\SoloX.GeneratorTools.Core.CSharp.Sample1.csproj";

            var ws = new CSharpWorkspace(new CSharpFactory(), new CSharpLoader());
            ws.RegisterProject(projectFile);

            Assert.Equal(1, ws.Projects.Count);

            Assert.NotEmpty(ws.Files);

            ws.DeepLoad();
        }

        [Fact]
        public void ProjectLoadWithDependenciesTest()
        {
            var projectFile = @"..\..\..\..\SoloX.GeneratorTools.Core.CSharp.Sample2\SoloX.GeneratorTools.Core.CSharp.Sample2.csproj";

            var ws = new CSharpWorkspace(new CSharpFactory(), new CSharpLoader());
            ws.RegisterProject(projectFile);

            Assert.Equal(2, ws.Projects.Count);

            Assert.NotEmpty(ws.Files);

            ws.DeepLoad();
        }
    }
}
