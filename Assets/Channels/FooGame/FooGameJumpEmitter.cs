using Albert.Channels.Common;
using Albert.Channels.EventBus;
using UnityEngine;

namespace Albert.Channels.FooGame
{
    public class FooGameJumpEmitter : MonoBehaviour
    {
        void Start()
        {
            EventBus<JumpedEvent>.Emit(this, new JumpedEvent(), EventChannel.FooGame);
        }
    }
}
