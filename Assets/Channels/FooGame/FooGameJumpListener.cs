using Albert.Channels.Common;
using Albert.Channels.EventBus;
using UnityEngine;

namespace Albert.Channels.FooGame
{
    public class FooGameJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, EventChannel.FooGame);
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, EventChannel.FooGame);
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(FooGameJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
