using Albert.Channels.EventBus;
using UnityEngine;

namespace Albert.Channels.Common
{
    public class GlobalJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, EventChannel.All);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, EventChannel.All);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(GlobalJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
