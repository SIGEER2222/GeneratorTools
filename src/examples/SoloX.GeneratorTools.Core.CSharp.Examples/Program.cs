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

            sc.AddCSharpToolsGenerator();
            sc.AddTransient<ModelGeneratorExample, ModelGeneratorExample>();
            sc.AddTransient<EntityGeneratorExample, EntityGeneratorExample>();
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

            // Get the entity generator example and generate the entity implementations in the given C# project.
            this.Service.GetService<EntityGeneratorExample>().Generate(prjFile, projectNameSpace);

            // Get the model generator example and generate the model implementations in the given C# project.
            this.Service.GetService<ModelGeneratorExample>().Generate(prjFile, projectNameSpace);
        }
    }
}
