namespace Albert.Channels.EventBus
{
    public static class Constants
    {
        public const string OptionName = "1 - Namespace Contexts With CodeGen";
        public const string Indent = "    ";
        public const string NamespaceSeparatorEscape = "_";
        public const string UnescapedNamespacePrefixFilter = "Albert";
        public const string RelativeEventContextsFilePath = OptionName + "/__Generated/" + nameof(EventChannel) + "_Generated.cs";
    }
}
