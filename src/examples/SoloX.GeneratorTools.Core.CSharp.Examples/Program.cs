﻿// ----------------------------------------------------------------------
// <copyright file="Program.cs" company="SoloX Software">
// Copyright (c) SoloX Software. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoloX.GeneratorTools.Core.CSharp.Workspace;
using SoloX.GeneratorTools.Core.CSharp.Workspace.Impl;

namespace SoloX.GeneratorTools.Core.CSharp.Examples
{
    /// <summary>
    /// Example program entry point.
    /// </summary>
    public sealed class Program : IDisposable
    {
        private readonly ILogger<Program> logger;

        private Program()
        {
            // Setup service provider.
            IServiceCollection sc = new ServiceCollection();

            sc.AddLogging(b => b.AddConsole());

            sc.AddSingleton<ICSharpFactory, CSharpFactory>();
            sc.AddSingleton<ICSharpLoader, CSharpLoader>();
            sc.AddTransient<ICSharpWorkspace, CSharpWorkspace>();
            sc.AddTransient<GeneratorExample, GeneratorExample>();
            this.Service = sc.BuildServiceProvider();

            this.logger = this.Service.GetService<ILogger<Program>>();
        }

        private ServiceProvider Service { get; }

        /// <summary>
        /// Main program entry point.
        /// </summary>
        public static void Main()
        {
            new Program().Run();
            Console.ReadLine();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Service.Dispose();
        }

        private void Run()
        {
            // Set the project folder/file we want to work on.
            var prjFolder = "../../../../SoloX.GeneratorTools.Core.CSharp.Examples.Sample";
            var prjFile = Path.Combine(prjFolder, "SoloX.GeneratorTools.Core.CSharp.Examples.Sample.csproj");

            // Set the project default namespace.
            var projectNameSpace = "SoloX.GeneratorTools.Core.CSharp.Examples.Sample";

            // Get the generator example that will generate .
            var generator = this.Service.GetService<GeneratorExample>();

            // Generate implementation in the given project file.
            generator.Generate(prjFile, projectNameSpace);
        }
    }
}
