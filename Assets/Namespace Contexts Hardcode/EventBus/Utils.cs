using System.Collections.Generic;
using Albert.Common;

namespace Albert.NsHardCode.EventBus
{
    public static class Utils
    {
        public static IEnumerable<string> PeelNamespace(this string ns) => ns.PeelRight(".");
    }
}
