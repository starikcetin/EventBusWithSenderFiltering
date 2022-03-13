using Albert.NsHardCode.Common;
using Albert.NsHardCode.EventBus;
using UnityEngine;

namespace Albert.NsHardCode.FooGame
{
    public class FooGameJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, "Albert.NsHardCode.FooGame");
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, "Albert.NsHardCode.FooGame");
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(FooGameJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
