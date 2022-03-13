using Albert.NsHardCode.EventBus;
using UnityEngine;

namespace Albert.NsHardCode.Common
{
    public class GlobalJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(GlobalJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
