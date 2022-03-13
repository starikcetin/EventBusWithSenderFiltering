using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Albert.Common;
using Albert.NsCodeGen.EventBus;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Albert.NsCodeGen.Editor
{
    public static class EventContextsGenerator
    {
        private static string AbsoluteEventContextsFilePath => Path.Combine(Application.dataPath, Constants.RelativeEventContextsFilePath);

        [MenuItem("Albert/" + Constants.OptionName + "/Force Generate")]
        [DidReloadScripts]
        private static void DidReloadScripts()
        {
            var escapedNamespaces = GetEscapedNamespaces().ToArray();
            var contents = GenerateFileContent(escapedNamespaces);
            WriteFile(contents);
        }

        private static IEnumerable<string> GetEscapedNamespaces()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Select(type => type.Namespace)
                .Where(ns => ns != null && ns.StartsWith(Constants.UnescapedNamespacePrefixFilter))
                .SelectMany(Utils.PeelUnescapedNamespace)
                .Distinct()
                .OrderByIdentity()
                .Select(Utils.GetEscapedNamespace);
        }

        private static string GenerateFileContent(IList<string> escapedNamespaces)
        {
            var indentLevel = 0;
            var currentIndent = string.Empty;
            var sb = new StringBuilder();

            static string GetIndent(int level) => Constants.Indent.Repeat(level);
            void Indent() => currentIndent = GetIndent(++indentLevel);
            void Outdent() => currentIndent = GetIndent(--indentLevel);
            void AppendLine(string line) => sb.AppendLine($"{currentIndent}{line}");

            // HEADER
            AppendLine("// -------------------------------------------");
            AppendLine("// THIS FILE IS AUTO-GENERATED. DO NOT MODIFY.");
            AppendLine("// -------------------------------------------");
            AppendLine("// Resharper disable all");

            // NS
            AppendLine($"namespace {nameof(Albert)}.{nameof(NsCodeGen)} {{");
            Indent();

            // CLASS
            AppendLine($"public partial class {nameof(EventContext)} {{");
            Indent();

            // STATIC FIELDS
            foreach (var escapedNs in escapedNamespaces)
            {
                AppendLine($"public static readonly {nameof(EventContext)} {escapedNs};");
            }

            // STATIC CTOR
            AppendLine($"static {nameof(EventContext)}() {{");
            Indent();

            // STATIC FIELD INITIALIZERS
            foreach (var escapedNs in escapedNamespaces)
            {
                var parentEscapedNs = escapedNs.Contains(Constants.NamespaceSeparatorEscape)
                    ? escapedNs.BeforeLast(Constants.NamespaceSeparatorEscape)
                    : nameof(EventContext.All);
                AppendLine($"{escapedNs} = new {nameof(EventContext)}({parentEscapedNs}, \"{escapedNs}\");");
            }

            // STATIC CTOR
            Outdent();
            AppendLine("}");

            // CLASS
            Outdent();
            AppendLine("}");

            // NS
            Outdent();
            AppendLine("}");

            return sb.ToString();
        }

        private static void WriteFile(string contents)
        {
            var directoryName = Path.GetDirectoryName(AbsoluteEventContextsFilePath);
            if (directoryName == null)
            {
                Debug.LogError($"Directory name is null. Path: {AbsoluteEventContextsFilePath}");
            }
            else
            {
                Directory.CreateDirectory(directoryName);
                File.WriteAllText(AbsoluteEventContextsFilePath, contents);
            }
        }
    }
}
