using Albert.Channels.Common;
using Albert.Channels.EventBus;
using UnityEngine;

namespace Albert.Channels.BarGame
{
    public class BarGameJumpEmitter : MonoBehaviour
    {
        void Start()
        {
            EventBus<JumpedEvent>.Emit(this, new JumpedEvent(), EventChannel.BarGame);
        }
    }
}
