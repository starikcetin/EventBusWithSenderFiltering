using Albert.NsCodeGen.EventBus;
using UnityEngine;

namespace Albert.NsCodeGen.Common
{
    public class GlobalJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, EventContext.All);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, EventContext.All);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(GlobalJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
