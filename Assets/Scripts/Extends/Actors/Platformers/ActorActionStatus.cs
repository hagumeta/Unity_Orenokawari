using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extends.Actors.Platformers
{
    [System.Serializable]
    public class PlatformerActionStatus
    {
        public float JumpSpeed, RunSpeed;
        public float OnWallFallSpeed, WallJumpSpeed, WallCatchTime, AdditionalGravity;
        public bool CanWallJump;
        public float WallJumpCoolTime = 0.2f;
    }
}