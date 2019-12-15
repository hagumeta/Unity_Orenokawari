using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage
{
    internal interface IPlayerTouchGimick
    {
        void OnPlayerTouched(Transform player);
        void OnPlayerReleased(Transform player);
    }
}