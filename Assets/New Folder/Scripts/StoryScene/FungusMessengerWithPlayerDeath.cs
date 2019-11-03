using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extends.FungusSupporter;
using Game.Stage.GameEvents;

namespace Game.Story
{
    public class FungusMessengerWithPlayerDeath : FungusMessenger, IPlayerDeathEventListener
    {

        [SerializeField] protected bool useDeathTypeAsMessage = false;

        public void OnEventRaised(DeathType deathType)
        {
            if (this.useDeathTypeAsMessage)
            {
                this.message = deathType.ToString();
            }
            this.SendMessage();
        }

        public void OnEventRaised()
        {
        }
    }
}