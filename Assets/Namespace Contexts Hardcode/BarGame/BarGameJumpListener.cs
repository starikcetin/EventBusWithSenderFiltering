using Albert.NsHardCode.Common;
using Albert.NsHardCode.EventBus;
using UnityEngine;

namespace Albert.NsHardCode.BarGame
{
    public class BarGameJumpListener : MonoBehaviour
    {
        void OnEnable()
        {
            EventBus<JumpedEvent>.AddListener(OnJumped, "Albert.NsHardCode.BarGame");
        }

        void OnDisable()
        {
            EventBus<JumpedEvent>.RemoveListener(OnJumped, "Albert.NsHardCode.BarGame");
        }

        private void OnJumped(object sender, JumpedEvent e)
        {
            Debug.Log($"{nameof(BarGameJumpListener)} received {nameof(JumpedEvent)} from {sender}");
        }
    }
}
