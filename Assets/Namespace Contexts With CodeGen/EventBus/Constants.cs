namespace Albert.NsCodeGen.EventBus
{
    public static class Constants
    {
        public const string OptionName = "Namespace Contexts With CodeGen";
        public const string Indent = "    ";
        public const string NamespaceSeparatorEscape = "_";
        public const string UnescapedNamespacePrefixFilter = "Albert";
        public const string RelativeEventContextsFilePath = OptionName + "/__Generated/" + nameof(EventContext) + "_Generated.cs";
    }
}
