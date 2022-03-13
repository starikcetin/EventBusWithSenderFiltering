using System.Collections.Generic;

namespace Albert.NsCodeGen.EventBus
{
    public static class EventBus<TEvent>
    {
        private static readonly Dictionary<EventContext, List<EventListener<TEvent>>> Listeners
            = new Dictionary<EventContext, List<EventListener<TEvent>>>();

        public static void AddListener(EventListener<TEvent> listener, params EventContext[] contexts)
        {
            foreach (var context in contexts)
            {
                EnsureContextListExists(context);
                Listeners[context].Add(listener);
            }
        }

        public static void RemoveListener(EventListener<TEvent> listener, params EventContext[] contexts)
        {
            foreach (var context in contexts)
            {
                EnsureContextListExists(context);
                Listeners[context].Remove(listener);
            }
        }

        public static void Emit(object sender, TEvent e)
        {
            var senderEscapedNs = sender.GetEscapedNamespace();
            var senderContext = EventContext.GetContextOfEscapedNamespace(senderEscapedNs);

            foreach (var context in senderContext.GetSelfAndAncestors())
            {
                EnsureContextListExists(context);
                foreach (var listener in Listeners[context])
                {
                    listener.Invoke(sender, e);
                }
            }
        }

        private static void EnsureContextListExists(EventContext context)
        {
            if (!Listeners.ContainsKey(context))
            {
                Listeners.Add(context, new List<EventListener<TEvent>>());
            }
        }
    }
}
