using Albert.NsCodeGen.Common;
using Albert.NsCodeGen.EventBus;
using UnityEngine;

namespace Albert.NsCodeGen.BarGame
{
    public class BarGameJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, EventContext.Albert_NsCodeGen_BarGame);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, EventContext.Albert_NsCodeGen_BarGame);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(BarGameJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
