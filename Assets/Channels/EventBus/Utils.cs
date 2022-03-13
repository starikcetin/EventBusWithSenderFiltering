using System;
using System.Collections.Generic;
using Albert.Common;

namespace Albert.Channels.EventBus
{
    public static class Utils
    {
        public static string GetEscapedNamespace(this string ns) => ns.Replace(".", Constants.NamespaceSeparatorEscape);
        public static string GetEscapedNamespace(this Type type) => type.Namespace.GetEscapedNamespace();
        public static string GetEscapedNamespace(this object obj) => obj.GetType().GetEscapedNamespace();

        public static IEnumerable<string> PeelUnescapedNamespace(this string ns) => ns.PeelRight(".");
        public static IEnumerable<string> PeelEscapedNamespace(this string ns) => ns.PeelRight(Constants.NamespaceSeparatorEscape);
    }
}
