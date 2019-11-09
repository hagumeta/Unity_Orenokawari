using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Stage.Manager;
using Game.Stage.GameEvents;

namespace Game.Stage.Objects.Items
{
    public class Coin : Item
    {
        [SerializeField] protected CoinGetEvent coinGetEvent;
        [SerializeField] protected GameObject gettedEffect;
        [SerializeField] protected GameObject coinObject;

        private bool getted = false;
        protected override void GettedItem()
        {
            if (!this.getted) {
                var collision = this.GetComponent<Collider2D>();
                collision.enabled = false;

                this.gettedEffect.SetActive(true);
                this.coinObject.SetActive(false);
                this.GetCoin();
                this.getted = true;
                Destroy(this.gameObject, 10f);
            }
        }

        protected virtual void GetCoin()
        {
            this.coinGetEvent.Raise(1);
        }
    }
}