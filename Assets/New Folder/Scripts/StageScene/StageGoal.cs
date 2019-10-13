using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage
{
    public class StageGoal : MonoBehaviour
    {

        [SerializeField] private GameObject clearEffectObject;
        [SerializeField] private GameObject unEnabledObject;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject other = collision.gameObject;

            if (other.tag == "Player")
            {
                var player = other.GetComponent<Stickman_PlayerOperationablePlatformActor>();
                if (player != null)
                {
                    player.StageCleard();
                }

                this.clearEffectObject.SetActive(true);
                this.unEnabledObject.SetActive(false);
            }
        }
    }
}