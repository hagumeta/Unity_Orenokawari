using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Actor;

namespace Game.Stage.Objects
{
    public class StageCheckPoint : MonoBehaviour
    {
        private bool isEnabled;
        protected bool IsEnabled
        {
            set
            {
                if (value)
                {
                    foreach (var checkpoint in FindObjectsOfType<StageCheckPoint>())
                    {
                        checkpoint.IsEnabled = false;
                    }
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    this.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().color = Color.white;
                }
                this.isEnabled = value;
            }
            get => this.isEnabled;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (this.isEnabled){ return; }

            GameObject other = collision.gameObject;
            if (other.tag == "Player")
            {
                var player = other.GetComponent<Stickman_PlayerOperationablePlatformActor>();
                if (player != null) {
                    this.IsEnabled = true;
                }
            }
        }

        
    }
}