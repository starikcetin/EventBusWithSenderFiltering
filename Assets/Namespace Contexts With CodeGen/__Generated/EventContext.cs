using System.Collections.Generic;

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

        public IEnumerable<EventContext> SelfAndAncestors()
        {
            yield return this;
            if (ParentContext != null)
            {
                foreach (var parentSelfAndAncestor in ParentContext.SelfAndAncestors())
                {
                    yield return parentSelfAndAncestor;
                }
            }
        }
    }
}
