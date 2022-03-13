﻿using Albert.NsHardCode.Common;
using Albert.NsHardCode.EventBus;
using UnityEngine;

namespace Albert.NsHardCode.FooGame
{
    public class FooGameJumpEmitter : MonoBehaviour
    {
        void Start()
        {
            EventBus<JumpedEvent>.Emit(this, new JumpedEvent());
        }
    }
}
