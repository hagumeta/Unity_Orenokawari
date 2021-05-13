using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Stage.Actor.Stickman
{
    [RequireComponent(typeof(Collider2D))]
    public class ActDeathPressed : MonoBehaviour
    {
        [SerializeField] protected Stickman_PlayerOperationablePlatformActor Stickman;

        private const float TIME_PRESSED_FOR_DEATH = 0.02f;
        private const DeathType DEATH_TYPE = DeathType.PressedDeath;

        private float pressedTime = 0f;

        private void OnCollisionStay2D(Collision2D collision)
        {
            this.pressedTime += Time.deltaTime;
            if (this.pressedTime >= TIME_PRESSED_FOR_DEATH)
            {
                this.Stickman.Death(DEATH_TYPE);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            this.pressedTime = 0f;
        }
    }
}
