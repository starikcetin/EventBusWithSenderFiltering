using Albert.NsCodeGen.Common;
using Albert.NsCodeGen.EventBus;
using UnityEngine;

namespace Albert.NsCodeGen.FooGame
{
    public class FooGameJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, EventContext.Albert_NsCodeGen_FooGame);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, EventContext.Albert_NsCodeGen_FooGame);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(FooGameJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
