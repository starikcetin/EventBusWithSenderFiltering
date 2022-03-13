using Albert.NsCodeGen.Common;
using Albert.NsCodeGen.EventBus;
using UnityEngine;

namespace Albert.NsCodeGen.BarGame
{
    public class BarGameJumpEmitter : MonoBehaviour
    {
        void Start()
        {
            EventBus<JumpedEvent>.Emit(this, new JumpedEvent());
        }
    }
}
