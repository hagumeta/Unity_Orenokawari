﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Stage.Gimicks
{
    public abstract class Item : MonoBehaviour, IPlayerTouchGimick
    {
        public void OnPlayerTouched(Transform player)
        {
            this.GettedItem();
        }
        abstract protected void GettedItem();
    }
}