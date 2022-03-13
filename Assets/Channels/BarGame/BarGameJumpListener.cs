using Albert.Channels.Common;
using Albert.Channels.EventBus;
using UnityEngine;

namespace Albert.Channels.BarGame
{
    public class BarGameJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, EventChannel.BarGame);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, EventChannel.BarGame);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(BarGameJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
