using System;

namespace Albert.Channels.EventBus
{
    [Flags]
    public enum EventChannel
    {
        FooGame = 1 << 0,
        BarGame = 1 << 1,
        SomeLib = 1 << 2,
        AnotherLib = 1 << 3,
        
        Games = FooGame | BarGame,
        Framework = SomeLib | AnotherLib,

        All = ~0
    }
}
