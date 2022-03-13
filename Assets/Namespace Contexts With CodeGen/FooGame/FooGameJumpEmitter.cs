using Albert.NsCodeGen.Common;
using Albert.NsCodeGen.EventBus;
using UnityEngine;

namespace Albert.NsCodeGen.FooGame
{
    public class FooGameJumpEmitter : MonoBehaviour
    {
        void Start()
        {
            EventBus<JumpedEvent>.Emit(this, new JumpedEvent());
        }
    }
}
