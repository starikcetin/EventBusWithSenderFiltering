using Albert.NsHardCode.Common;
using Albert.NsHardCode.EventBus;
using UnityEngine;

namespace Albert.NsHardCode.BarGame
{
    public class BarGameJumpEmitter : MonoBehaviour
    {
        void Start()
        {
            EventBus<JumpedEvent>.Emit(this, new JumpedEvent());
        }
    }
}
