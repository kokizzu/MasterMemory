﻿using Microsoft.CodeAnalysis;


namespace MasterMemory.SourceGenerator;

readonly record struct MasterMemoryGeneratorOptions(string? Namespace, string PrefixClassName, bool IsReturnNullIfKeyNotFound)
{
    public static MasterMemoryGeneratorOptions FromAttribute(AttributeData attributeData)
    {
        var args = attributeData.NamedArguments;

        var ns = args.FirstOrDefault(x => x.Key == nameof(Namespace)).Value.Value as string ?? null;
        var prefix = args.FirstOrDefault(x => x.Key == nameof(PrefixClassName)).Value.Value as string ?? "";
        var isReturnNull = args.FirstOrDefault(x => x.Key == nameof(IsReturnNullIfKeyNotFound)).Value.Value as bool? ?? null;

        return new MasterMemoryGeneratorOptions(ns, prefix, isReturnNull ?? false);
    }

    public static void EmitAttribute(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource("MasterMemory.MasterMemoryGeneratorOptions.g.cs", """
// <auto-generated />
#pragma warning disable
#nullable enable

using System;

namespace MasterMemory
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    internal sealed class MasterMemoryGeneratorOptionsAttribute : Attribute
    {
        public string? Namespace { get; set; } = null;
        public string PrefixClassName { get; set; } = "";
        public bool IsReturnNullIfKeyNotFound { get; set; } = false;
    }
}
""");
    }
}