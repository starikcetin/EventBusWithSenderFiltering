using System;
using System.Collections.Generic;
using Albert.Common;

namespace Albert.Channels.EventBus
{
    public static class EventBus<TEvent>
    {
        private static readonly Dictionary<EventChannel, List<EventListener<TEvent>>> Listeners
            = new Dictionary<EventChannel, List<EventListener<TEvent>>>();

        public static void AddListener(EventListener<TEvent> listener, EventChannel channel)
        {
            EnsureContextListExists(channel);
            Listeners[channel].Add(listener);
        }

        public static void RemoveListener(EventListener<TEvent> listener, EventChannel channel)
        {
            EnsureContextListExists(channel);
            Listeners[channel].Remove(listener);
        }

        public static void Emit(object sender, TEvent e, EventChannel source)
        {
            if (IsChannelComposite(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source), source, "Composite channels can only be used by listeners.");
            }

            foreach (var (channel, listeners) in Listeners)
            {
                if (ChannelIncludesSource(channel, source))
                {
                    foreach (var listener in listeners)
                    {
                        listener.Invoke(sender, e);
                    }
                }
            }
        }

        private static bool IsChannelComposite(EventChannel source)
        {
            return false == ((int)source).IsPowerOfTwo();
        }

        private static void EnsureContextListExists(EventChannel channel)
        {
            if (!Listeners.ContainsKey(channel))
            {
                Listeners.Add(channel, new List<EventListener<TEvent>>());
            }
        }

        private static bool ChannelIncludesSource(EventChannel channel, EventChannel source)
        {
            return (channel & source) != 0;
        }
    }
}
