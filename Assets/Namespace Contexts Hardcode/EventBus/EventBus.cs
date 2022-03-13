using System.Collections.Generic;
using System.Linq;

namespace Albert.NsHardCode.EventBus
{
    public static class EventBus<TEvent>
    {
        private const string RootContext = ".RootContext";

        private static readonly Dictionary<string, List<EventListener<TEvent>>> Listeners
            = new Dictionary<string, List<EventListener<TEvent>>>();

        public static void AddListener(EventListener<TEvent> listener, params string[] namespaceContexts)
        {
            if (namespaceContexts.Length == 0)
            {
                EnsureNamespaceContextListExists(RootContext);
                Listeners[RootContext].Add(listener);
            }
            else
            {
                foreach (var namespaceContext in namespaceContexts)
                {
                    EnsureNamespaceContextListExists(namespaceContext);
                    Listeners[namespaceContext].Add(listener);
                }
            }
        }

        public static void RemoveListener(EventListener<TEvent> listener, params string[] namespaceContexts)
        {
            if (namespaceContexts.Length == 0)
            {
                EnsureNamespaceContextListExists(RootContext);
                Listeners[RootContext].Remove(listener);
            }
            else
            {
                foreach (var namespaceContext in namespaceContexts)
                {
                    EnsureNamespaceContextListExists(namespaceContext);
                    Listeners[namespaceContext].Remove(listener);
                }
            }
        }

        public static void Emit(object sender, TEvent e)
        {
            var senderNamespace = sender.GetType().Namespace;
            var senderSelfAndAncestorNamespaces = senderNamespace.PeelNamespace().Append(RootContext);

            foreach (var namespaceContext in senderSelfAndAncestorNamespaces)
            {
                EnsureNamespaceContextListExists(namespaceContext);
                foreach (var listener in Listeners[namespaceContext])
                {
                    listener.Invoke(sender, e);
                }
            }
        }

        private static void EnsureNamespaceContextListExists(string context)
        {
            if (!Listeners.ContainsKey(context))
            {
                Listeners.Add(context, new List<EventListener<TEvent>>());
            }
        }
    }
}
