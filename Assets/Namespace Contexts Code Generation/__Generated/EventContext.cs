using System.Collections.Generic;
using System.Linq;

namespace Albert.NsCodeGen
{
    public partial class EventContext
    {
        //
        // Static
        //

        private static readonly Dictionary<string, EventContext> EscapedNsToContextMap = new Dictionary<string, EventContext>();

        public static EventContext All = new EventContext(null, string.Empty);

        public static EventContext GetContextOfEscapedNamespace(string escapedNs) => EscapedNsToContextMap[escapedNs];

        //
        // Instance
        //

        public readonly EventContext ParentContext;

        private EventContext(EventContext parentContext, string escapedNamespace)
        {
            ParentContext = parentContext;
            EscapedNsToContextMap.Add(escapedNamespace, this);
        }

        public IEnumerable<EventContext> GetSelfAndAncestors()
        {
            var ancestors = ParentContext?.GetSelfAndAncestors() ?? Enumerable.Empty<EventContext>();
            return ancestors.Prepend(this);
        }
    }
}
