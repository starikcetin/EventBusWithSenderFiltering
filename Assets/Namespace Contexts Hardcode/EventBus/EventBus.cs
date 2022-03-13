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
            void AddListenerToContext(string context)
            {
                EnsureNamespaceContextListExists(context);
                Listeners[context].Add(listener);
            }

            if (namespaceContexts.Length == 0)
            {
                AddListenerToContext(RootContext);
            }
            else
            {
                foreach (var namespaceContext in namespaceContexts)
                {
                    AddListenerToContext(namespaceContext);
                }
            }
        }

        public static void RemoveListener(EventListener<TEvent> listener, params string[] namespaceContexts)
        {
            void RemoveListenerFromContext(string context)
            {
                EnsureNamespaceContextListExists(context);
                Listeners[context].Add(listener);
            }

            if (namespaceContexts.Length == 0)
            {
                RemoveListenerFromContext(RootContext);
            }
            else
            {
                foreach (var namespaceContext in namespaceContexts)
                {
                    RemoveListenerFromContext(namespaceContext);
                }
            }
        }

        public static void Emit(object sender, TEvent e)
        {
            var senderNamespace = sender.GetType().Namespace;
            var senderAndAncestorsNamespaces = senderNamespace.PeelNamespace().Append(RootContext);

            foreach (var namespaceContext in senderAndAncestorsNamespaces)
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
